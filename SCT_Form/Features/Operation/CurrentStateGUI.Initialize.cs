using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    // Start/Pause/Continue/Abort/Initialize 버튼의 활성화 여부 계산과, Initialize 버튼의
    // "실린더 후진 확인 → 축 원점복귀 → 챔버 도어 닫힘 확인" 안전 초기화 시퀀스.
    public partial class CurrentStateGUI
    {
        public void UpdateControlButtons()
        {
            if (main == null) return;

            // 1. 초기화 중이거나 연결 해제 상태인 경우 모든 버튼 비활성화
            if (isInitializing || !main.isConnect || main.EtherCAT_M == null)
            {
                btn_Start.Enabled = false;
                btn_Pause.Enabled = false;
                btn_Continue.Enabled = false;
                btn_Abort.Enabled = false;
                btn_Initialize.Enabled = !isInitializing && main.isConnect; // 초기화 중이 아니고 연결된 경우에만 활성화 가능
                return;
            }

            // 2. 센서 상태 읽기 및 범위 체크 (10000 ~ -10000)
            long udPos = 0;
            long lrPos = 0;
            bool hasUd = long.TryParse(main.EtherCAT_M.Axis1_is_PosData(), out udPos);
            bool hasLr = long.TryParse(main.EtherCAT_M.Axis2_is_PosData(), out lrPos);

            bool isUdOutOfRange = !hasUd || udPos < -10000 || udPos > 10000;
            bool isLrOutOfRange = !hasLr || lrPos < -10000 || lrPos > 10000;
            bool isCylinderNotSafe = main.IsCylinderForward() || !main.IsCylinderBack();
            bool isAnyDoorOpen = main.IsChamberDoorOpen("PM A") || main.IsChamberDoorOpen("PM B") || main.IsChamberDoorOpen("PM C");

            // 3. 가동 상태에 따른 활성화 처리
            if (isProcessRecipeRunning)
            {
                // 가동 중 상태
                btn_Start.Enabled = false;
                btn_Initialize.Enabled = false;

                if (isProcessRecipePaused)
                {
                    btn_Pause.Enabled = false;
                    btn_Continue.Enabled = true;
                    btn_Abort.Enabled = true;
                }
                else
                {
                    btn_Pause.Enabled = true;
                    btn_Continue.Enabled = false;
                    btn_Abort.Enabled = true;
                }
            }
            else
            {
                // 정지/대기 상태
                btn_Pause.Enabled = false;
                btn_Continue.Enabled = false;
                btn_Abort.Enabled = false;
                btn_Initialize.Enabled = true; // 정상 대기 상태에서도 임의로 초기화(원점 복귀)를 누를 수 있도록 활성화
                btn_Start.Enabled = true;      // 대기 상태에서는 항상 Start와 Initialize가 활성화되며, 시작 클릭 시점에 안전 검증을 수행합니다.
            }
        }

        private async void btn_Initialize_Click(object sender, EventArgs e)
        {
            if (isInitializing) return;

            isProcessCompleted = false;
            isInitializing = true;
            UpdateControlButtons(); // 모든 버튼 즉시 비활성화
            ResetAllChamberProcessDisplays(); // PM 정보 표시 패널 디폴트로 초기화

            try
            {
                main.WriteSystemLog("INFO", "장비 초기화(Initialize) 시퀀스 시작");

                // 1단계: 무조건 실린더 후진 먼저 체크하고 후진시키기
                if (!main.IsCylinderBack())
                {
                    main.WriteSystemLog("WARN", "장비 초기화: 실린더가 후진상태가 아닙니다. 후진을 시작합니다.");
                    main.MoveCylinderBack();

                    int cylinderTimeoutMs = 10000;
                    int cylinderElapsedMs = 0;
                    while (!main.IsCylinderBack() && cylinderElapsedMs < cylinderTimeoutMs)
                    {
                        await Task.Delay(100);
                        cylinderElapsedMs += 100;
                    }

                    if (!main.IsCylinderBack())
                    {
                        main.WriteSystemLog("ERROR", "장비 초기화 실패: 실린더 후진 센서 확인 불가");
                        MessageBox.Show("실린더 후진 확인에 실패했습니다. 실린더 상태 및 센서를 확인하십시오.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                main.WriteSystemLog("INFO", "장비 초기화: 실린더 후진 확인 완료");

                // 2단계: 상하 원점복귀, 좌우 원점복귀
                main.WriteSystemLog("INFO", "장비 초기화: 축 원점복귀(UD & LR)를 시작합니다.");
                main.HomeAxis1UD();
                main.HomeAxis2LR();

                int axisTimeoutMs = 60000;
                int axisElapsedMs = 0;
                while (axisElapsedMs < axisTimeoutMs)
                {
                    bool udAtHome = main.IsAxis1AtPosition(0, 10000);
                    bool lrAtHome = main.IsAxis2AtPosition(0, 10000);

                    if (udAtHome && lrAtHome) break;

                    await Task.Delay(200);
                    axisElapsedMs += 200;
                }

                if (!main.IsAxis1AtPosition(0, 10000) || !main.IsAxis2AtPosition(0, 10000))
                {
                    main.WriteSystemLog("ERROR", "장비 초기화 실패: 축 원점복귀(Homing) 확인 불가 또는 타임아웃");
                    MessageBox.Show("축 원점복귀에 실패했습니다. 서보 드라이브 상태를 확인하십시오.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                main.WriteSystemLog("INFO", "장비 초기화: 축 원점복귀 확인 완료");

                // 3단계: PM 문(Chamber Door) 열림 여부 센서로 체크해서 닫기
                main.WriteSystemLog("INFO", "장비 초기화: 챔버 도어 상태 검사 및 닫기 시작");
                bool doorCloseTriggered = false;
                foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                {
                    if (main.IsChamberDoorOpen(pm))
                    {
                        main.WriteSystemLog("WARN", $"장비 초기화: {pm} 도어가 열려있습니다. 도어를 닫습니다.");
                        main.CloseChamberDoor(pm);
                        doorCloseTriggered = true;
                    }
                }

                if (doorCloseTriggered)
                {
                    int doorTimeoutMs = 30000;
                    int doorElapsedMs = 0;
                    while (doorElapsedMs < doorTimeoutMs)
                    {
                        bool allClosed = true;
                        foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                        {
                            if (main.IsChamberDoorOpen(pm) || !main.IsChamberDoorClosed(pm))
                            {
                                allClosed = false;
                            }
                        }

                        if (allClosed) break;

                        await Task.Delay(200);
                        doorElapsedMs += 200;
                    }

                    bool finalClosed = true;
                    foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                    {
                        if (!main.IsChamberDoorClosed(pm))
                        {
                            finalClosed = false;
                            main.WriteSystemLog("ERROR", $"장비 초기화 실패: {pm} 도어 닫힘 상태가 확인되지 않았습니다.");
                        }
                    }

                    if (!finalClosed)
                    {
                        MessageBox.Show("챔버 도어가 닫히지 않아 초기화를 중단합니다.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                main.WriteSystemLog("INFO", "장비 초기화 완료: 장비가 정상 정렬 상태입니다.");
            }
            catch (Exception ex)
            {
                main.WriteSystemLog("ERROR", $"장비 초기화 중 예외 오류 발생: {ex.Message}");
                MessageBox.Show($"초기화 중 오류가 발생했습니다:\r\n{ex.Message}", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isInitializing = false;
                UpdateControlButtons(); // 최종 버튼 상태로 갱신
            }
        }
    }
}
