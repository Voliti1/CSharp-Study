# 자동 웨이퍼 프로세스 시퀀서 설계

날짜: 2026-07-05
범위: 웨이퍼 1장을 FOUP A → (Process Recipe에 정의된 PM 스텝들을 순서대로) → FOUP B 로 자동으로 처리하는 시퀀서. 여러 웨이퍼 동시 처리는 이번 범위 밖(추후 별도 설계).

## 배경

`Features/Operation/CurrentStateGUI.cs`의 `btn_Start`/`btn_Pause`/`btn_Continue`/`btn_Abort`는 현재 실제 장비를 전혀 움직이지 않고, `Process Time`을 3등분해서 진행률 바만 채우는 시뮬레이션이다(`chamberProcessTimer`, `ChamberProcessState`, `AdvanceChamberProcess` 등). 이 버튼들을 실제 자동화 시퀀서로 교체한다.

로봇 축 이동(`MainGUI.MoveAxis1UDTo`/`MoveAxis2LRTo`), 실린더/문/램프/흡착(`EtherCAT_M.Digital_Output`), 센서 확인(`EtherCAT_M.Digital_Input`) 로직은 이미 `Features/Maint/MaintGUI.cs`의 수동 버튼 핸들러 안에 구현되어 있다. 이 설계는 그 로직을 재사용 가능한 형태로 `MainGUI`로 옮기고, 그 위에 스텝 시퀀서를 얹는다.

## 1. 공용 하드웨어 액션 계층 (MainGUI로 리팩토링)

`MaintGUI.cs`의 문 열기/닫기/램프 ON-OFF 핸들러는 하드웨어 제어(Digital_Output)와 화면 갱신(패널 색상)이 한 메서드에 섞여 있어 재사용이 불가능하다. 하드웨어 제어 부분만 `MainGUI`에 `internal` 메서드로 추출한다:

- `OpenChamberDoor(string module)` / `CloseChamberDoor(string module)` — 모듈별 DO 인덱스는 아래 `ModuleProfile` 표 참조. 내부에서 `SetChamberDoorStatus(module, isOpen)`도 함께 호출해 기존 상태 추적과 일치시킨다.
- `SetChamberLamp(string module, bool on)` — 램프 DO + 기존 타워램프 자동전환 로직(`Digital_Output(1/2, ...)`) 재사용.
- `MoveCylinderFront()` / `MoveCylinderBack()` — 이미 `btn_moveFront_Click`/`btn_moveBack_Click`에 있는 `Digital_Output(12/13, ...)` 호출을 그대로 옮김.
- `SetWaferSuction(bool on)` — `Digital_Output(14, on)` (Inhalation).
- `IsCylinderBack()` / `IsCylinderForward()` — 후진/전진 센서 판정 (전진은 기존 `IsRobotCylinderForward` 로직 재사용, 후진은 반대 조건 추가).
- `IsChamberDoorOpen(string module)` / `IsChamberDoorClosed(string module)` — 문 센서 판정.
- `IsRobotFacingModule(string module)` — `Axis2_is_PosData()` 현재값과 해당 모듈의 LR 목표값을 허용오차(±500 count, 조정 가능) 이내로 비교. 여러 웨이퍼를 동시에 굴릴 때 "로봇이 지금 이 챔버 쪽을 보고 있는지"를 판단하는 데 재사용하기 위해 지금부터 좌표 비교 방식으로 구현한다(사용자 확인 완료).

`MaintGUI.cs`의 기존 버튼 핸들러들은 이 메서드들을 호출하도록 바꾸고, 패널 색상 갱신 등 화면 전용 코드만 핸들러에 남긴다. 동작 자체는 바뀌지 않는다(순수 리팩토링).

### 모듈/포지션 상수표 (기존 MaintGUI.cs 상수 그대로 이전)

| Module | LR Target | UD Down | UD Up | Door Open DO | Door Close DO | Door Up/Down 센서 | Lamp DO |
|---|---|---|---|---|---|---|---|
| PM A | -59064 | 806931 | 1156931 | (5,true)+(4,false) | (5,false)+(4,true) | 6 / 7 | 3 |
| PM B | -190823 | 806931 | 1156931 | (8,true)+(7,false) | (8,false)+(7,true) | 8 / 9 | 6 |
| PM C | -322000 | 806931 | 1156931 | (11,true)+(10,false) | (11,false)+(10,true) | 10 / 11 | 9 |

| FOUP | LR Target | Wafer1 Down | Wafer1 Up |
|---|---|---|---|
| FOUP A | 13140 | 100379 | 302380 |
| FOUP B | -395093 | 100379 | 302380 |

실린더: Front = DO(12,true)+DO(13,false), 감지 input 13(front)/12(back). InOn/InOff = DO(14, true/false).

## 2. 데이터 모델

```csharp
private enum AutoStepKind { Action, WaitSensor, WaitElapsed }

private class AutoStep
{
    public AutoStepKind Kind;
    public string Description;       // 화면에 표시할 현재 동작 설명
    public Action Execute;           // Action 스텝 실행 (1회)
    public Func<bool> IsSatisfied;   // WaitSensor/WaitElapsed 조건 검사 (매 tick)
    public int TimeoutSeconds;       // WaitSensor 전용, 0이면 타임아웃 없음(WaitElapsed)
    public int ElapsedSeconds;       // 진행률 표시용 (WaitElapsed만 사용)
    public int TotalSeconds;         // 진행률 표시용 (WaitElapsed만 사용)
}
```

`AutoStepKind.Action`은 tick에서 만나면 `Execute()`를 실행하고 곧바로 다음 스텝으로 넘어간다(같은 tick 안에서 연속 Action 여러 개를 실행해도 무방 — 실제 이동 호출 자체가 이미 동기/블로킹이므로 tick 하나에 여러 Action이 몰려도 안전).

## 3. 스텝 시퀀스 생성

`BuildAutoSequence(ProcessRecipeData recipe)`가 `List<AutoStep>`을 생성한다. 의사코드:

```
steps.AddRange(PickFromFoup("FOUP A", waferSlot: 1))

for i, step in recipe.Steps:
    module = Normalize(step.Recipe.Module)
    steps.AddRange(PlaceIntoModule(module))
    steps.Add(ActionStep: SetChamberLamp(module, true))
    steps.Add(WaitElapsed(step.Recipe.ProcessTime))
    steps.Add(ActionStep: SetChamberLamp(module, false))
    steps.AddRange(PickFromModule(module))

steps.AddRange(PlaceIntoFoup("FOUP B", waferSlot: 1))
```

`PickFromFoup(foup, slot)`:
1. Action: `MoveAxis2LRTo(foup.LR)`
2. Action: `MoveAxis1UDTo(foup.WaferDown[slot])`
3. Action: `MoveCylinderFront()`
4. Action: `MoveAxis1UDTo(foup.WaferUp[slot])`
5. Action: `SetWaferSuction(true)`
6. Action: `MoveCylinderBack()`
7. WaitSensor(10s): `IsCylinderBack()`

`PlaceIntoModule(module)`:
1. Action: `MoveAxis2LRTo(module.LR)`
2. Action: `MoveAxis1UDTo(module.UDUp)`
3. WaitSensor(10s): `IsRobotFacingModule(module)` — 좌표가 이미 목표값이므로 사실상 즉시 통과하지만, 향후 다중 웨이퍼 대비 동일 로직 재사용
4. Action: `OpenChamberDoor(module)`
5. WaitSensor(10s): `IsChamberDoorOpen(module)`
6. Action: `MoveCylinderFront()`
7. Action: `SetWaferSuction(false)`
8. Action: `MoveAxis1UDTo(module.UDDown)`
9. Action: `MoveCylinderBack()`
10. WaitSensor(10s): `IsCylinderBack()`
11. Action: `CloseChamberDoor(module)`
12. WaitSensor(10s): `IsChamberDoorClosed(module)`

`PickFromModule(module)` (Place의 역순, Pick과 동일 상하 순서):
1. WaitSensor(10s): `IsRobotFacingModule(module)`
2. Action: `OpenChamberDoor(module)`
3. WaitSensor(10s): `IsChamberDoorOpen(module)`
4. Action: `MoveCylinderFront()`
5. Action: `MoveAxis1UDTo(module.UDDown)`
6. Action: `MoveAxis1UDTo(module.UDUp)`
7. Action: `SetWaferSuction(true)`
8. Action: `MoveCylinderBack()`
9. WaitSensor(10s): `IsCylinderBack()`
10. Action: `CloseChamberDoor(module)`

`PlaceIntoFoup(foup, slot)` (마지막 스텝 이후에만 실행):
1. Action: `MoveAxis2LRTo(foup.LR)`
2. Action: `MoveAxis1UDTo(foup.WaferUp[slot])`
3. Action: `MoveCylinderFront()`
4. Action: `SetWaferSuction(false)`
5. Action: `MoveAxis1UDTo(foup.WaferDown[slot])`
6. Action: `MoveCylinderBack()`
7. WaitSensor(10s): `IsCylinderBack()`

## 4. 상태 머신 실행 (`chamberProcessTimer` 재사용)

- `btn_Start_Click`: Recipe 검증 후 `BuildAutoSequence` 호출, `autoSteps` 리스트 + `currentAutoStepIndex = 0` 세팅, 첫 스텝이 Action이면 즉시 실행 후 다음 Action들도 연쇄 실행(WaitStep을 만날 때까지), 타이머 시작.
- `chamberProcessTimer_Tick`: 현재 스텝이 `WaitElapsed`면 `ElapsedSeconds++` 후 도달 여부 확인 → 도달 시 다음 스텝(들)으로 진행. `WaitSensor`면 `IsSatisfied()` 확인, 아니면 대기 카운터++, `TimeoutSeconds` 초과 시 Abort 처리(경고 메시지 + 로그) 후 시퀀스 중단. 조건 만족 시 다음 스텝(들) 연쇄 실행.
- **Pause**: `isProcessRecipePaused = true`로 tick에서 카운터 증가/조건 검사 자체를 건너뜀(진행 중인 하드웨어 동작을 되돌리지 않고, 그 다음 판정만 멈춘다).
- **Continue**: 카운터/조건 검사 재개.
- **Abort**: 타이머 정지, 상태를 Aborted로 표시, 로그 남김. 하드웨어 원복 동작 없음(사용자 확인 완료) — 복구는 Maint 화면에서 수동으로.
- **타임아웃**: `WaitSensor` 전용, 10초 고정(상수화, 필요시 조정 가능하게 필드로 분리). 초과 시 Abort와 동일 처리 + "센서 응답 없음" 경고.

## 5. 화면 반영

기존 `ChamberProcessState`의 `MessageLabel`/`StatusLabel`/progress bar를 재사용하되 의미를 바꾼다:
- `MessageLabel.Text` = 현재 `AutoStep.Description` (예: "PM A 웨이퍼 로딩 중", "PM A 공정 진행 중 12/60s", "PM A 웨이퍼 언로딩 중", "PM B로 이송 중")
- progress bar는 `WaitElapsed`(램프 ON 구간) 진행 중일 때만 `ElapsedSeconds/TotalSeconds`로 채우고, 그 외 스텝에서는 0 또는 이전 값 유지
- `lbl_PMx_stepnum` 등 기존 "n/3 step" 표시는 "현재 시퀀스 스텝 인덱스 / 전체 스텝 수"로 대체

## 6. 테스트 방침

실제 EtherCAT 하드웨어 의존적인 코드라 자동화 단위 테스트는 어렵다. 대신:
- `BuildAutoSequence`가 생성하는 `AutoStep` 리스트의 순서/개수/Description을 검증하는 순수 로직 테스트는 가능(하드웨어 호출은 mocking 필요 — `MainGUI`의 하드웨어 액션 메서드들을 델리게이트로 주입 가능하게 설계하면 이 테스트가 쉬워짐)
- 최종 검증은 실제 장비에서 웨이퍼 1장으로 FOUP A → PM A → FOUP B 전체 왕복을 수동 관찰

## 범위 밖 (다음 단계)

- 웨이퍼 여러 장 동시 처리(FOUP 슬롯 2~5번, 여러 웨이퍼가 동시에 여러 PM에 있는 상태 스케줄링)
- Pause 중 하드웨어 안전 정지, Abort 시 자동 복구 시퀀스
- WaitSensor 타임아웃 시간을 설정 화면에서 조정 가능하게 노출
