using IEG3268_Dll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace SCT_Form
{
    public partial class Form1 : Form
    {
        // log4net 로거 객체 선언
        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));

        IEG3268 EtherCAT_M = new IEG3268();

        private bool isGreenLightOn = false;
        private bool isChamALampOn = false;
        private bool isChamBLampOn = false;
        private bool isChamCLampOn = false;
        private bool isChamADoorOpen = false;
        private bool isChamBDoorOpen = false;
        private bool isChamCDoorOpen = false;
        private string currentState = "AUTO";

        public Form1()
        {
            InitializeComponent();

            grpbox_Cham_A_Manual.Enabled = false;
            grpbox_Cham_B_Manual.Enabled = false;
            grpbox_Cham_C_Manual.Enabled = false;
            grpbox_Tower.Enabled = false;
            btn_Auto.Enabled = false;
            btn_Manual.Enabled = false;

            btn_Auto.FlatStyle = FlatStyle.Flat;
            btn_Auto.FlatAppearance.BorderSize = 0;
            btn_Manual.FlatStyle = FlatStyle.Flat;
            btn_Manual.FlatAppearance.BorderSize = 0;

            LogView.View = View.Details;
            LogView.FullRowSelect = true;
            LogView.GridLines = true;
            LogView.OwnerDraw = false;

            LogView.Columns.Clear();
            LogView.Columns.Add("시간", 90, HorizontalAlignment.Center);
            LogView.Columns.Add("레벨", 70, HorizontalAlignment.Center);
            LogView.Columns.Add("메시지", 400, HorizontalAlignment.Left);

            WriteSystemLog("INFO", "시스템 초기화 완료 (초기 모드: AUTO)");
        }

        // 💡 파일 로그(log4net) 저장과 하단 lbl_SystemLog 라벨 업데이트를 동시에 수행하는 전용 메서드
        private void WriteSystemLog(string level, string message)
        {
            // 크로스 스레드 발생 시 UI 스레드로 안전하게 위임
            if (LogView.InvokeRequired)
            {
                LogView.Invoke(new Action(() => WriteSystemLog(level, message)));
                return;
            }

            // 1. log4net 파일 저장
            switch (level.ToUpper())
            {
                case "INFO": log.Info(message); break;
                case "WARN": log.Warn(message); break;
                case "ERROR": log.Error(message); break;
                default: log.Info(message); break;
            }

            string logTime = DateTime.Now.ToString("HH:mm:ss");
            string upperLevel = level.ToUpper();

            // 2. ListView 행(Row) 객체 생성 및 데이터 삽입
            ListViewItem item = new ListViewItem(logTime);
            item.SubItems.Add(upperLevel);
            item.SubItems.Add(message);

            // 3. SEMI 표준 적용: 중요도에 따라 한 줄 전체 배경색/글자색 반전
            switch (upperLevel)
            {
                case "INFO":
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                    break;

                case "WARN":
                    item.BackColor = Color.Orange;
                    item.ForeColor = Color.Black; // 주황 배경엔 검은 글씨가 가독성이 좋습니다.
                    break;

                case "ERROR":
                case "FATAL":
                    item.BackColor = Color.Red;
                    item.ForeColor = Color.White; // 빨간 배경엔 흰 글씨가 대비가 확실합니다.
                    break;

                default:
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                    break;
            }

            // 4. 메모리 관리 (최대 500개 유지)
            if (LogView.Items.Count >= 500)
            {
                LogView.Items.RemoveAt(0);
            }

            // 5. 리스트뷰에 아이템 최종 추가 및 강제 화면 새로고침(Invalidate) 후 스크롤 다운
            LogView.Items.Add(item);
            LogView.Invalidate(); // 변경 사항을 화면에 즉시 다시 그리도록 명령
            item.EnsureVisible();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "EtherCAT 마스터 연결 시도 중...");
            try
            {
                if (EtherCAT_M.CIFX_50RE_Connect() == true)
                {
                    WriteSystemLog("INFO", "EtherCAT 마스터 연결 성공 (Connect OK)");
                    label2.Text = "Connect OK";

                    EtherCAT_M.ReadData_Send_Start(300);
                    EtherCAT_M.ReadData_Timer_Start();
                    // 타이머 시작 같은 개발 분석용 세부 로직은 라벨을 가리지 않고 파일에만 남김
                    log.Debug("EtherCAT 데이터 리드 타이머 시작 (주기: 300ms)");

                    panel_Connection.BackColor = Color.DodgerBlue;

                    // 플래그 초기화
                    isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                    isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;

                    btn_Auto.Enabled = true;
                    btn_Manual.Enabled = true;

                    // 모든 챔버 문 초기 닫기 출력
                    EtherCAT_M.Digital_Output(5, false);
                    EtherCAT_M.Digital_Output(4, true);
                    EtherCAT_M.Digital_Output(8, false);
                    EtherCAT_M.Digital_Output(7, true);
                    EtherCAT_M.Digital_Output(11, false);
                    EtherCAT_M.Digital_Output(10, true);
                    WriteSystemLog("INFO", "장비 초기화 세팅: 모든 챔버 도어 CLOSE 명령 출력");

                    Color idleColor = Color.LightCyan;
                    pnl_ChamA.BackColor = idleColor;
                    pnl_ChamB.BackColor = idleColor;
                    pnl_ChamC.BackColor = idleColor;

                    Color color2 = Color.LightGray;
                    pnl_Cham_A_Door.BackColor = color2;
                    pnl_Cham_A_Lamp.BackColor = color2;
                    pnl_Cham_B_Door.BackColor = color2;
                    pnl_Cham_B_Lamp.BackColor = color2;
                    pnl_Cham_C_Door.BackColor = color2;
                    pnl_Cham_C_Lamp.BackColor = color2;

                    // 황색등 점등
                    EtherCAT_M.Digital_Output(1, true);
                    WriteSystemLog("INFO", "타워램프 상태 변경: 황색등(Yellow) ON (장비 대기)");
                }
                else
                {
                    WriteSystemLog("WARN", "EtherCAT 마스터 연결 실패 (하드웨어 감지 안 됨)");
                    label2.Text = "NG";
                    panel_Connection.BackColor = Color.Yellow;
                }
            }
            catch (Exception ex)
            {
                log.Error("EtherCAT 연결 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"연결 예외 오류: {ex.Message}");
            }
        }

        private void DisConnect_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "EtherCAT 마스터 연결 해제 시도 중...");
            try
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.CIFX_50RE_Disconnect();

                WriteSystemLog("INFO", "EtherCAT 마스터 연결 해제 완료 (Disconnect)");
                label2.Text = "Disconnect";
                panel_Connection.BackColor = Color.Red;

                isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;
                isGreenLightOn = false;

                btn_Auto.Enabled = false;
                btn_Manual.Enabled = false;

                btn_Auto.BackColor = Color.FromArgb(60, 60, 60);
                btn_Manual.BackColor = Color.FromArgb(60, 60, 60);
                btn_Auto.Invalidate();
                btn_Manual.Invalidate();

                Color grayOffline = SystemColors.ControlDark;
                pnl_ChamA.BackColor = grayOffline;
                pnl_ChamB.BackColor = grayOffline;
                pnl_ChamC.BackColor = grayOffline;
                pnl_Cham_A_Door.BackColor = grayOffline;
                pnl_Cham_A_Lamp.BackColor = grayOffline;
                pnl_Cham_B_Door.BackColor = grayOffline;
                pnl_Cham_B_Lamp.BackColor = grayOffline;
                pnl_Cham_C_Door.BackColor = grayOffline;
                pnl_Cham_C_Lamp.BackColor = grayOffline;
            }
            catch (Exception ex)
            {
                log.Error("EtherCAT 해제 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"해제 예외 오류: {ex.Message}");
            }
        }

        // --- 타워 램프 제어 영역 ---
        private void RedLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) ON");
        }

        private void RedLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) OFF");
        }

        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) ON");
        }

        private void YellowLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) OFF");
        }

        private void GreenLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, true);
            isGreenLightOn = true;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) ON");
        }

        private void GreenLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, false);
            isGreenLightOn = false;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) OFF");
        }

        private void AllLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
            EtherCAT_M.Digital_Output(1, true);
            EtherCAT_M.Digital_Output(2, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 ON");
        }

        private void AllLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 OFF");
        }

        // --- Chamber A 제어 영역 ---
        private void btn_Cham_A_Door_OPEN_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber A 도어 OPEN 명령 요청");
            if (isChamALampOn)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber A 공정 중(Lamp ON) 도어 오픈 거부");
                MessageBox.Show("Chamber A가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(5, true);
            EtherCAT_M.Digital_Output(4, false);

            pnl_Cham_A_Door.BackColor = Color.Red;
            pnl_ChamA.BackColor = Color.Orange;
            isChamADoorOpen = true;
            WriteSystemLog("INFO", "Chamber A 도어 OPEN 완료");
        }

        private void btn_Cham_A_Door_CLOSE_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber A 도어 CLOSE 명령 요청");
            if (!isChamADoorOpen) return;

            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(4, true);

            pnl_Cham_A_Door.BackColor = Color.LightGray;
            pnl_ChamA.BackColor = Color.LightCyan;
            isChamADoorOpen = false;
            WriteSystemLog("INFO", "Chamber A 도어 CLOSE 완료");
        }

        private void btn_Cham_A_Lamp_ON_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber A 램프 ON 명령 요청");
            if (isChamADoorOpen)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber A 도어 Open 상태로 램프 가동 거부");
                MessageBox.Show("Chamber A의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(3, true);
            pnl_Cham_A_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamA.BackColor = Color.LimeGreen;
            isChamALampOn = true;
            WriteSystemLog("INFO", "Chamber A 램프 ON 완료 (박막생성 공정 시작)");

            if (isGreenLightOn == false)
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
                WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
            }
        }

        private void btn_Cham_A_Lamp_OFF_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber A 램프 OFF 명령 요청");
            if (!isChamALampOn) return;

            EtherCAT_M.Digital_Output(3, false);
            pnl_Cham_A_Lamp.BackColor = Color.LightGray;
            isChamALampOn = false;
            WriteSystemLog("INFO", "Chamber A 램프 OFF 완료 (박막생성 공정 종료)");

            if (isChamADoorOpen) pnl_ChamA.BackColor = Color.Orange;
            else pnl_ChamA.BackColor = Color.LightCyan;

            if (isChamBLampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(1, true);
                isGreenLightOn = false;
                WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
            }
        }

        // --- Chamber B 제어 영역 ---
        private void btn_Cham_B_Door_OPEN_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber B 도어 OPEN 명령 요청");
            if (isChamBLampOn)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber B 공정 중(Lamp ON) 도어 오픈 거부");
                MessageBox.Show("Chamber B가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(8, true);
            EtherCAT_M.Digital_Output(7, false);

            pnl_Cham_B_Door.BackColor = Color.Red;
            pnl_ChamB.BackColor = Color.Orange;
            isChamBDoorOpen = true;
            WriteSystemLog("INFO", "Chamber B 도어 OPEN 완료");
        }

        private void btn_Cham_B_Door_CLOSE_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber B 도어 CLOSE 명령 요청");
            if (!isChamBDoorOpen) return;

            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(7, true);

            pnl_Cham_B_Door.BackColor = Color.LightGray;
            pnl_ChamB.BackColor = Color.LightCyan;
            isChamBDoorOpen = false;
            WriteSystemLog("INFO", "Chamber B 도어 CLOSE 완료");
        }

        private void btn_Cham_B_Lamp_ON_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber B 램프 ON 명령 요청");
            if (isChamBDoorOpen)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber B 도어 Open 상태로 램프 가동 거부");
                MessageBox.Show("Chamber B의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(6, true);
            pnl_Cham_B_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamB.BackColor = Color.LimeGreen;
            isChamBLampOn = true;
            WriteSystemLog("INFO", "Chamber B 램프 ON 완료 (CMP 공정 시작)");

            if (isGreenLightOn == false)
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
                WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
            }
        }

        private void btn_Cham_B_Lamp_OFF_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber B 램프 OFF 명령 요청");
            if (!isChamBLampOn) return;

            EtherCAT_M.Digital_Output(6, false);
            pnl_Cham_B_Lamp.BackColor = Color.LightGray;
            isChamBLampOn = false;
            WriteSystemLog("INFO", "Chamber B 램프 OFF 완료 (CMP 공정 종료)");

            if (isChamBDoorOpen) pnl_ChamB.BackColor = Color.Orange;
            else pnl_ChamB.BackColor = Color.LightCyan;

            if (isChamALampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
                WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
            }
        }

        // --- Chamber C 제어 영역 ---
        private void btn_Cham_C_Door_OPEN_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber C 도어 OPEN 명령 요청");
            if (isChamCLampOn)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber C 공정 중(Lamp ON) 도어 오픈 거부");
                MessageBox.Show("Chamber C가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(11, true);
            EtherCAT_M.Digital_Output(10, false);
            pnl_Cham_C_Door.BackColor = Color.Red;
            pnl_ChamC.BackColor = Color.Orange;
            isChamCDoorOpen = true;
            WriteSystemLog("INFO", "Chamber C 도어 OPEN 완료");
        }

        private void btn_Cham_C_Door_CLOSE_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber C 도어 CLOSE 명령 요청");
            if (!isChamCDoorOpen) return;

            EtherCAT_M.Digital_Output(11, false);
            EtherCAT_M.Digital_Output(10, true);
            pnl_Cham_C_Door.BackColor = Color.LightGray;
            pnl_ChamC.BackColor = Color.LightCyan;
            isChamCDoorOpen = false;
            WriteSystemLog("INFO", "Chamber C 도어 CLOSE 완료");
        }

        private void btn_Cham_C_Lamp_ON_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber C 램프 ON 명령 요청");
            if (isChamCDoorOpen)
            {
                WriteSystemLog("WARN", "인터록 차단: Chamber C 도어 Open 상태로 램프 가동 거부");
                MessageBox.Show("Chamber C의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(9, true);
            pnl_Cham_C_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamC.BackColor = Color.LimeGreen;
            isChamCLampOn = true;
            WriteSystemLog("INFO", "Chamber C 램프 ON 완료 (세정 공정 시작)");

            if (isGreenLightOn == false)
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
                WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
            }
        }

        private void btn_Cham_C_Lamp_OFF_Click(object sender, EventArgs e)
        {
            WriteSystemLog("INFO", "수동 제어: Chamber C 램프 OFF 명령 요청");
            if (!isChamCLampOn) return;

            EtherCAT_M.Digital_Output(9, false);
            pnl_Cham_C_Lamp.BackColor = Color.LightGray;
            isChamCLampOn = false;
            WriteSystemLog("INFO", "Chamber C 램프 OFF 완료 (세정 공정 종료)");

            if (isChamCDoorOpen) pnl_ChamC.BackColor = Color.Orange;
            else pnl_ChamC.BackColor = Color.LightCyan;

            if (isChamALampOn == false && isChamBLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
                WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
            }
        }

        // --- 프로그램 종료 처리 ---
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteSystemLog("INFO", "Application 중단 감지: 안전 시퀀스(Abnormal Stop) 가동");
            try
            {
                // 타워등, 챔버등, 실린더 오프 가동
                EtherCAT_M.Digital_Output(0, false);
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(3, false);
                EtherCAT_M.Digital_Output(6, false);
                EtherCAT_M.Digital_Output(9, false);

                EtherCAT_M.Digital_Output(5, false);
                EtherCAT_M.Digital_Output(4, true);
                EtherCAT_M.Digital_Output(8, false);
                EtherCAT_M.Digital_Output(7, true);
                EtherCAT_M.Digital_Output(11, false);
                EtherCAT_M.Digital_Output(10, true);
                WriteSystemLog("INFO", "안전 셧다운 완료: 모든 램프 소등 및 도어 폐쇄 완료");

                EtherCAT_M.CIFX_50RE_Disconnect();
                WriteSystemLog("INFO", "EtherCAT 마스터 통신 채널 정상 해제 완료");
            }
            catch (Exception ex)
            {
                log.Fatal("폼 종료 안전 제어 중 예외 오류: ", ex);
            }
        }

        // --- 상단 모드 변경 조작 ---
        private void btn_auto_Click(object sender, EventArgs e)
        {
            if (currentState == "AUTO") return; // 중복 제어 차단

            WriteSystemLog("INFO", "설비 구동 모드 변경 요청: MANUAL ➡️ AUTO");
            currentState = "AUTO";
            UpdateModeButtonStyles();

            ForceStopAllChambers();

            grpbox_Cham_A_Manual.Enabled = false;
            grpbox_Cham_B_Manual.Enabled = false;
            grpbox_Cham_C_Manual.Enabled = false;
            grpbox_Tower.Enabled = false;

            MessageBox.Show("AUTO 모드로 전환됨: 모든 수동 동작이 중단되었습니다.");
            EtherCAT_M.Digital_Output(1, true);
            WriteSystemLog("INFO", "구동 모드 변경 완료: AUTO 모드가 활성화되었습니다.");
        }

        private void btn_manual_Click(object sender, EventArgs e)
        {
            if (currentState == "MANUAL") return; // 중복 제어 차단

            WriteSystemLog("INFO", "설비 구동 모드 변경 요청: AUTO ➡️ MANUAL");
            currentState = "MANUAL";
            UpdateModeButtonStyles();

            ForceStopAllChambers();

            grpbox_Cham_A_Manual.Enabled = true;
            grpbox_Cham_B_Manual.Enabled = true;
            grpbox_Cham_C_Manual.Enabled = true;
            grpbox_Tower.Enabled = true;

            MessageBox.Show("MANUAL 모드로 전환됨: 모든 자동 동작이 중단되었습니다.");
            EtherCAT_M.Digital_Output(1, true);
            WriteSystemLog("INFO", "구동 모드 변경 완료: MANUAL 모드가 활성화되었습니다.");
        }

        private void ForceStopAllChambers()
        {
            WriteSystemLog("INFO", "모드 변경에 따른 전 공정 인터록(Force Stop) 가동");
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            EtherCAT_M.Digital_Output(3, false);
            EtherCAT_M.Digital_Output(6, false);
            EtherCAT_M.Digital_Output(9, false);

            isChamALampOn = false;
            isChamBLampOn = false;
            isChamCLampOn = false;

            Color idleColor = Color.LightGray;
            pnl_Cham_A_Lamp.BackColor = idleColor;
            pnl_Cham_B_Lamp.BackColor = idleColor;
            pnl_Cham_C_Lamp.BackColor = idleColor;

            pnl_ChamA.BackColor = Color.LightCyan;
            pnl_ChamB.BackColor = Color.LightCyan;
            pnl_ChamC.BackColor = Color.LightCyan;
            log.Debug("인터록 완료: 모든 공정 인터록 및 GUI 상태 리셋 완료");
        }

        private void btnMode_Paint(object sender, PaintEventArgs e)
        {
            // 화면 렌더링 영역은 부하 조절 및 가독성을 위해 로그 생략
            Button btn = (Button)sender;
            bool isActive = (currentState == "AUTO" && btn == btn_Auto) ||
                            (currentState == "MANUAL" && btn == btn_Manual);

            Color highlight = isActive ? Color.Gray : Color.White;
            Color shadow = isActive ? Color.White : Color.Gray;
            int borderThickness = isActive ? 2 : 1;

            ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid);
        }

        private void UpdateModeButtonStyles()
        {
            bool isManual = (currentState == "MANUAL");

            btn_Auto.BackColor = !isManual ? Color.DarkBlue : Color.FromArgb(60, 60, 60);
            btn_Auto.ForeColor = !isManual ? Color.White : Color.DimGray;
            btn_Auto.Invalidate();

            btn_Manual.BackColor = isManual ? Color.DarkBlue : Color.FromArgb(60, 60, 60);
            btn_Manual.ForeColor = isManual ? Color.White : Color.DimGray;
            btn_Manual.Invalidate();
        }

        private void btnErrorTest_Click(object sender, EventArgs e)
        {
            WriteSystemLog("ERROR", "설비 알람 발생: 하부 배기 Fan RPM 저하 (Abnormal Stop 조치 요망)");
        }

        private void btnWarnTest_Click(object sender, EventArgs e)
        {
            WriteSystemLog("WARN", "인터록 경고: Chamber A 도어 Open 상태에서 가스 공급 명령 차단");
        }
    }
}