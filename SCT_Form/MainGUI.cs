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
    public partial class MainGUI : Form
    {
        // log4net 로거 객체 선언
        private static readonly ILog log = LogManager.GetLogger(typeof(MainGUI));

        internal IEG3268 EtherCAT_M = new IEG3268();

        internal bool isConnect = false;
        internal bool isGreenLightOn = false;
        internal bool isChamALampOn = false;
        internal bool isChamBLampOn = false;
        internal bool isChamCLampOn = false;
        internal bool isChamADoorOpen = false;
        internal bool isChamBDoorOpen = false;
        internal bool isChamCDoorOpen = false;
        internal bool isServoMotorOn = false;
        
        internal string currentState = "AUTO";

        public MainGUI()
        {
            InitializeComponent();

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

            SystemConnect();
            servoMotorON();
            isServoMotorOn = true;
            setBasicPoint();
            Change_btnAuto();

            timer1.Interval = 200;
            timer1.Start();

            WriteSystemLog("INFO", "시스템 초기화 완료 (초기 모드: AUTO)");
        }

        // 파일 로그(log4net) 저장과 하단 lbl_SystemLog 라벨 업데이트를 동시에 수행하는 전용 메서드
        public void WriteSystemLog(string level, string message)
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
                    item.ForeColor = Color.Black;
                    break;

                case "ERROR":
                case "FATAL":
                    item.BackColor = Color.Red;
                    item.ForeColor = Color.White;
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
        private void SystemConnect()
        {
            WriteSystemLog("INFO", "EtherCAT 마스터 연결 시도 중...");
            try
            {
                if (EtherCAT_M.CIFX_50RE_Connect() == true)
                {
                    WriteSystemLog("INFO", "EtherCAT 마스터 연결 성공 (Connect OK)");
                    isConnect = true;

                    EtherCAT_M.ReadData_Send_Start(300);
                    EtherCAT_M.ReadData_Timer_Start();
                    // 타이머 시작 같은 개발 분석용 세부 로직은 라벨을 가리지 않고 파일에만 남김
                    log.Debug("EtherCAT 데이터 리드 타이머 시작 (주기: 300ms)");

                    lbl_CurrentConnect.ForeColor = Color.Lime;

                    // 플래그 초기화
                    isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                    isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;

                    // 연결 시 AUTO 모드 설정
                    currentState = "AUTO";

                    // 상단버튼 활성화
                    btn_Auto.Enabled = true;
                    btn_Manual.Enabled = true;

                    // 모드에 맞게 버튼 색상 스타일 출력
                    UpdateModeButtonStyles();

                    // 수동 조작 UI 비활성화 유지
                    grpbox_Tower.Enabled = false;

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

                    // 황색등 점등
                    EtherCAT_M.Digital_Output(1, true);
                    WriteSystemLog("INFO", "타워램프 상태 변경: 황색등(Yellow) ON (장비 대기)");

                    lbl_UDcurrentPos.Text = EtherCAT_M.Axis1_is_PosData();
                    lbl_LRcurrentPos.Text = EtherCAT_M.Axis2_is_PosData();
                }
                else
                {
                    WriteSystemLog("WARN", "EtherCAT 마스터 연결 실패 (하드웨어 감지 안 됨)");
                }
            }
            catch (Exception ex)
            {
                log.Error("EtherCAT 연결 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"연결 예외 오류: {ex.Message}");
            }
        }
        private void Reconnect_Click(object sender, EventArgs e)
        {
            if (isConnect) return;
            SystemConnect();
            servoMotorON();
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

                setBasicPoint();
                servoMotorOFF();
                isServoMotorOn = false;
                WriteSystemLog("INFO", "안전 셧다운 완료: 로봇 초기 위치 설정 및 서보 모터 종료");

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

            Change_btnAuto();

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

            Change_btnManual();

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
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(0, true);
            WriteSystemLog("ERROR", "설비 알람 발생: 하부 배기 Fan RPM 저하 (Abnormal Stop 조치 요망)");
        }

        private void btnWarnTest_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(0, true);
            WriteSystemLog("WARN", "인터록 경고: Chamber A 도어 Open 상태에서 가스 공급 명령 차단");
        }

        private void servoMotorON()
        {
            EtherCAT_M.Axis1_ON();
            EtherCAT_M.Axis2_ON();
        }

        private void servoMotorOFF()
        {
            EtherCAT_M.Axis1_OFF();
            EtherCAT_M.Axis2_OFF();
        }
        private void setBasicPoint()
        {
            EtherCAT_M.Axis1_UD_Homming(); //상하 원점복귀
            EtherCAT_M.Axis2_LR_Homming(); //좌우 원점복귀
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(this);
            setting.Show();
        }

        private void Change_btnAuto()
        {
            pnl_BottomContainer.Controls.Clear();
            AutoProcessControl autoUI = new AutoProcessControl(this);
            autoUI.Dock = DockStyle.Fill;
            pnl_BottomContainer.Controls.Add(autoUI);
            grpbox_Tower.Visible = false;
            grpbox_Tower.Enabled = false;
        }

        private void Change_btnManual()
        {
            pnl_BottomContainer.Controls.Clear();
            ManualProcessControl manualUI = new ManualProcessControl(this);
            manualUI.Dock = DockStyle.Fill;
            pnl_BottomContainer.Controls.Add(manualUI);
            grpbox_Tower.Visible = true;
            grpbox_Tower.Enabled = true;
        }

        private void btn_ServoON_Click(object sender, EventArgs e)
        {
            servoMotorON();
        }

        private void btn_ServoOFF_Click(object sender, EventArgs e)
        {
            servoMotorOFF();
        }

        private void btn_UDBasic_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Axis1_UD_Homming();
        }

        private void btn_LRBasic_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Axis2_LR_Homming();
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (!EtherCAT_M.Digital_Input(13) && (Int64)nUpDown_MovementDistance.Value >= 0)
            {
                EtherCAT_M.Axis1_UD_POS_Update((Int64)nUpDown_MovementDistance.Value);
                EtherCAT_M.Axis1_UD_Move_Send();
            }
            else
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있거나 입력값이 0 미만입니다.");
            }
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (!EtherCAT_M.Digital_Input(13) && (Int64)nUpDown_MovementDistance.Value >= 0)
            {
                EtherCAT_M.Axis1_UD_POS_Update((Int64)nUpDown_MovementDistance.Value);
                EtherCAT_M.Axis1_UD_Move_Send();
            }
            else
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있거나 입력값이 0 미만입니다.");
            }
        }

        private void btn_MoveLeft_Click(object sender, EventArgs e)
        {
            if (!EtherCAT_M.Digital_Input(13)&& (Int64)nUpDown_MovementDistance.Value >= 0)
            {
                //Int64 currentLRPos = Int64.Parse(lbl_LRcurrentPos.Text);
                //Int64 Pos = currentLRPos + Convert.ToInt64(nUpDown_MovementDistance.Value);
                //EtherCAT_M.Axis2_LR_POS_Update(Pos);
                //EtherCAT_M.Axis2_LR_Move_Send();
                if (Int64.TryParse(lbl_LRcurrentPos.Text, out Int64 currentLRPos))
                {
                    Int64 Pos = currentLRPos + Convert.ToInt64(nUpDown_MovementDistance.Value);
                    EtherCAT_M.Axis2_LR_POS_Update(Pos);
                    EtherCAT_M.Axis2_LR_Move_Send();
                }
                else
                {
                    WriteSystemLog("WARN", "좌우 현재 위치 데이터 정보가 유효하지 않아 이동을 차단합니다.");
                    MessageBox.Show("현재 위치를 읽을 수 없습니다. 원점 복귀(Homming)를 다시 수행하십시오.");
                }
            }
            else
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있거나 입력값이 0 미만입니다.");
            }
        }

        private void btn_MoveRight_Click(object sender, EventArgs e)
        {
            if (!EtherCAT_M.Digital_Input(13)&& (Int64)nUpDown_MovementDistance.Value >= 0)
            {
                //Int64 currentLRPos = Int64.Parse(lbl_LRcurrentPos.Text);
                //Int64 Pos = currentLRPos - Convert.ToInt64(nUpDown_MovementDistance.Value);
                //EtherCAT_M.Axis2_LR_POS_Update(Pos);
                //EtherCAT_M.Axis2_LR_Move_Send();
                if (Int64.TryParse(lbl_LRcurrentPos.Text, out Int64 currentLRPos))
                {
                    Int64 Pos = currentLRPos - Convert.ToInt64(nUpDown_MovementDistance.Value);
                    EtherCAT_M.Axis2_LR_POS_Update(Pos);
                    EtherCAT_M.Axis2_LR_Move_Send();
                }
                else
                {
                    WriteSystemLog("WARN", "좌우 현재 위치 데이터 정보가 유효하지 않아 이동을 차단합니다.");
                    MessageBox.Show("현재 위치를 읽을 수 없습니다. 원점 복귀(Homming)를 다시 수행하십시오.");
                }
            }
            else
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있거나 입력값이 0 미만입니다.");
            }
        }

        private void btn_UDMove_Click(object sender, EventArgs e)
        {

        }

        private void btn_LRMove_Click(object sender, EventArgs e)
        {

        }

        private void btn_InOn_Click(object sender, EventArgs e)
        {

        }

        private void btn_InOFF_Click(object sender, EventArgs e)
        {

        }

        private void btn_ExON_Click(object sender, EventArgs e)
        {

        }

        private void btn_ExOFF_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isConnect || EtherCAT_M == null) return;

            try
            {
                string currentUDPos = EtherCAT_M.Axis1_is_PosData();
                string currentLRPos = EtherCAT_M.Axis2_is_PosData();
                if (!string.IsNullOrEmpty(currentUDPos)) lbl_UDcurrentPos.Text = currentUDPos;
                if (!string.IsNullOrEmpty(currentLRPos)) lbl_LRcurrentPos.Text = currentLRPos;


                // ==========================================
                // 2. 🎛️ 하위 입출력(I/O) 센서 상태 실시간 모니터링 (필요 시 확장)
                // ==========================================
                // 예시: 13번 입력 디지탈 센서 상태를 실시간으로 확인하여 UI 요소 색상이나 라벨 변경
                // bool isEmergencyOrLimit = EtherCAT_M.Digital_Input(13);
                // if (isEmergencyOrLimit) {  
                //     lbl_StatusSensor13.BackColor = Color.Red; 
                // } else { 
                //     lbl_StatusSensor13.BackColor = Color.Green; 
                // }
            }
            catch (Exception ex)
            {
                log.Error("UI 모니터링 타이머 처리 중 예외 발생: ", ex);
            }
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {

        }
    }
}