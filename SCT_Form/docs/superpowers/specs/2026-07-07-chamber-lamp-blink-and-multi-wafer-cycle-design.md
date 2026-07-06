# 챔버등 종료 깜빡임 + FOUP 5슬롯 순차 처리 설계

날짜: 2026-07-07
범위: (1) PM 프로세스 종료 시 챔버등이 즉시 꺼지던 것을 1초 간격 On/Off 5회 깜빡인 뒤 Off로 종료하도록 변경. (2) 자동 웨이퍼 시퀀서가 FOUP A 슬롯 1번 웨이퍼만 처리하던 것을 슬롯 1~5번까지 순차로 5사이클 처리하도록 확장. `2026-07-05-auto-wafer-process-design.md`의 "범위 밖" 항목(웨이퍼 여러 장 처리) 중 순차 처리분을 구현한다. 동시/병렬 처리는 여전히 범위 밖.

## 1. 챔버등 종료 깜빡임

### 배경
`AutoSequenceBuilder.AddProcessWait`이 PM 프로세스 종료 시 `main.SetChamberLamp(module, false)`를 호출해 즉시 소등한다. 이 지점은 `CurrentStateGUI.UpdateAutoProcessTimeDisplay`가 PM 정보창 Time을 "종료"로 표시하는 시점(`elapsedSeconds >= totalSeconds`)과 같은 tick이다.

### 변경
`MainGUI.cs`:
- 기존 `SetChamberLamp(string module, bool on)`의 하드웨어 출력 로직은 `SetChamberLampOutput(string module, bool on)`으로 이름만 유지한 채 그대로 두고, 공개 `SetChamberLamp`는 호출 시 해당 모듈의 진행 중인 깜빡임 타이머를 먼저 취소(`StopChamberLampBlink`)한 뒤 `SetChamberLampOutput`을 호출하도록 감싼다. (수동 램프 on/off, 공정 시작 시 on 호출과 깜빡임이 서로 충돌하지 않게 하기 위함)
- 신규 `internal void BlinkChamberLamp(string module)`: 모듈별 `System.Windows.Forms.Timer`(1000ms interval)를 새로 만들어 tick마다 On/Off를 토글, 총 10회 토글(On→Off 5회 반복, 약 10초) 후 자동으로 `Stop()`+`Dispose()`하고 Off 상태로 종료. 시작 즉시 첫 On을 적용한다.
- 모듈별 활성 타이머는 `Dictionary<string, System.Windows.Forms.Timer>` (`chamberLampBlinkTimers`)로 추적. `StopChamberLampBlink(module)`가 있으면 Stop+Dispose+Remove.
- `SetAllChamberLamps(bool on)` (Abort/안전복귀에서 전체 소등 시 사용)은 내부에서 PM A/B/C 각각에 대해 `SetChamberLamp(module, on)`을 호출하도록 변경 — 강제 소등 시 진행 중이던 깜빡임이 확실히 취소되고 Off가 우선되게 한다.

`AutoSequenceBuilder.cs`:
- `AddProcessWait`의 `() => main.SetChamberLamp(module, false)` 호출을 `() => main.BlinkChamberLamp(module)`로 교체.

### 동작
- 깜빡임은 백그라운드 타이머로 진행되며, 자동 시퀀스의 다음 스텝(로봇이 웨이퍼를 꺼내러 오는 door open 등)은 깜빡임과 무관하게 즉시 진행된다(블로킹 없음).
- 사용자 Abort(`btn_Abort_Click` → `MainGUI.SafeAbortAndHome`) 시 `SetAllChamberLamps(false)`가 호출되어 진행 중이던 깜빡임이 취소되고 즉시 Off로 강제된다.

## 2. FOUP 5슬롯 순차 처리

### 배경
`EquipmentLayout.FoupProfile`은 이미 `Wafer1Up/Down`~`Wafer5Up/Down` 5슬롯 좌표를 갖고 있고, `CurrentStateGUI`도 `pnl_FOUP_A_1`~`_5`/`pnl_FOUP_B_1`~`_5` UI 패널과 `SetFoupSlotState(foup, slot, hasWafer)`를 이미 지원한다. 하지만 `AutoSequenceBuilder.AddPickFromFoup`/`AddPlaceIntoFoup`는 슬롯 1번을 하드코딩하고 있어 `Build()`가 웨이퍼 1장(슬롯 1)만 처리한다.

### 변경
`AutoSequenceBuilder.cs`:
- `AddPickFromFoup`/`AddPlaceIntoFoup`에 `int slot` 파라미터를 추가. `profile.Wafer1Up`/`Wafer1Down` 직접 참조를 슬롯 번호로 매핑하는 신규 private 헬퍼로 교체:
  ```csharp
  private static long GetWaferUp(EquipmentLayout.FoupProfile profile, int slot)
  private static long GetWaferDown(EquipmentLayout.FoupProfile profile, int slot)
  ```
  (`switch (slot) { case 1: return profile.Wafer1Up; ... case 5: return profile.Wafer5Up; default: throw ArgumentOutOfRangeException }`)
- `setFoupSlotState?.Invoke(foup, 1, ...)`의 하드코딩된 `1`을 실제 `slot` 값으로 교체.
- `Build()`를 5회 루프로 감싼다:
  ```
  const int WaferSlotCount = 5;
  for (int slot = 1; slot <= WaferSlotCount; slot++)
  {
      AddPickFromFoup(steps, main, FoupASource, slot, firstModule, ..., setFoupSlotState);
      foreach (recipeStep in recipeSteps) { AddPlaceIntoModule(...); AddProcessWait(...); AddPickFromModule(...); }
      AddPlaceIntoFoup(steps, main, FoupBDestination, slot, lastModule, ..., setFoupSlotState);
  }
  ```
  스텝 Description에 슬롯 번호를 포함시켜 로그에서 "몇 번째 웨이퍼" 진행인지 구분 가능하게 한다 (예: "PM A load from FOUP A (slot 2)").
- FOUP A 슬롯 N → FOUP B 같은 슬롯 N으로 매핑 (A1→B1 ... A5→B5).
- 5슬롯 모두 사용자가 한 번 선택한 동일한 Process Recipe(PM 경로)를 그대로 반복 적용한다. 슬롯별 다른 레시피는 범위 밖.

### 동작
- `Build()`가 반환하는 것은 5사이클 분량이 이어붙은 하나의 연속된 `AutoStep` 리스트다. `WaferAutoSequencer`는 이를 순서대로 실행할 뿐이므로, "슬롯 1 웨이퍼 프로세스 종료 인식 → 슬롯 2로 복귀해 다음 작업 시작"은 별도 인식 로직 없이 리스트 순서만으로 자연스럽게 성립한다.
- `WaferAutoSequencer`, `CurrentStateGUI`(Pause/Abort 버튼), `EquipmentLayout`은 변경 없음. Pause/Abort는 5사이클 전체를 하나의 실행으로 취급한다 — 예: 슬롯 3 진행 중 Abort하면 슬롯 4/5는 실행되지 않는다.

## 범위 밖
- FOUP 슬롯에 웨이퍼가 실제로 있는지 확인하는 점유 센서 체크 (5슬롯 모두 채워져 있다고 가정 — 기존 슬롯 1 로직도 점유 확인 없이 그대로 진행하던 것과 동일한 신뢰 모델)
- 슬롯별 다른 Process Recipe 지정
- 여러 웨이퍼 동시/병렬 처리(한 슬롯이 PM에서 공정 중일 때 다음 슬롯을 미리 로딩하는 등의 스케줄링)
- PM 정보창에 "웨이퍼 n/5" 같은 진행 상황 표시 추가
