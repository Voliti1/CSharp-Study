using IEG3268_Dll;
using log4net;
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
        internal bool isAxisMoving = false;

        internal string currentUbarState = "Operate";

        private CurrentStateGUI currentUI;
        private MaintGUI maintui;
        private Setting settingUI;

        public MainGUI()
        {
            InitializeComponent();

            
        //grpbox_Tower.Enabled = false;
        //btn_Auto.Enabled = false;
        //btn_Manual.Enabled = false;

        //btn_Auto.FlatStyle = FlatStyle.Flat;
        //btn_Auto.FlatAppearance.BorderSize = 0;
        //btn_Manual.FlatStyle = FlatStyle.Flat;
        //btn_Manual.FlatAppearance.BorderSize = 0;

        //LogView.View = View.Details;
        //LogView.FullRowSelect = true;
        //LogView.GridLines = true;
        //LogView.OwnerDraw = false;

        //LogView.Columns.Clear();
        //LogView.Columns.Add("시간", 90, HorizontalAlignment.Center);
        //LogView.Columns.Add("레벨", 70, HorizontalAlignment.Center);
        //LogView.Columns.Add("메시지", 400, HorizontalAlignment.Left);

            SystemConnect();
            servoMotorON();
            isServoMotorOn = true;
            setBasicPoint();

            currentUI = new CurrentStateGUI(this);
            maintui = new MaintGUI(this);
            settingUI = new Setting(this);

            Mainpnl_CurrentStateGUI();

            timer1.Interval = 200;
            timer1.Start();

            WriteSystemLog("INFO", "시스템 초기화 완료 (초기 모드: AUTO)");
        }

        // 파일 로그(log4net) 저장과 하단 lbl_SystemLog 라벨 업데이트를 동시에 수행하는 전용 메서드
        public void WriteSystemLog(string level, string message)
        {
            //// 크로스 스레드 발생 시 UI 스레드로 안전하게 위임
            //if (LogView.InvokeRequired)
            //{
            //    LogView.Invoke(new Action(() => WriteSystemLog(level, message)));
            //    return;
            //}

            //// 1. log4net 파일 저장
            //switch (level.ToUpper())
            //{
            //    case "INFO": log.Info(message); break;
            //    case "WARN": log.Warn(message); break;
            //    case "ERROR": log.Error(message); break;
            //    default: log.Info(message); break;
            //}

            //string logTime = DateTime.Now.ToString("HH:mm:ss");
            //string upperLevel = level.ToUpper();

            //// 2. ListView 행(Row) 객체 생성 및 데이터 삽입
            //ListViewItem item = new ListViewItem(logTime);
            //item.SubItems.Add(upperLevel);
            //item.SubItems.Add(message);

            //// 3. SEMI 표준 적용: 중요도에 따라 한 줄 전체 배경색/글자색 반전
            //switch (upperLevel)
            //{
            //    case "INFO":
            //        item.BackColor = Color.White;
            //        item.ForeColor = Color.Black;
            //        break;

            //    case "WARN":
            //        item.BackColor = Color.Orange;
            //        item.ForeColor = Color.Black;
            //        break;

            //    case "ERROR":
            //    case "FATAL":
            //        item.BackColor = Color.Red;
            //        item.ForeColor = Color.White;
            //        break;

            //    default:
            //        item.BackColor = Color.White;
            //        item.ForeColor = Color.Black;
            //        break;
            //}

            //// 4. 메모리 관리 (최대 500개 유지)
            //if (LogView.Items.Count >= 500)
            //{
            //    LogView.Items.RemoveAt(0);
            //}

            //// 5. 리스트뷰에 아이템 최종 추가 및 강제 화면 새로고침(Invalidate) 후 스크롤 다운
            //LogView.Items.Add(item);
            //LogView.Invalidate(); // 변경 사항을 화면에 즉시 다시 그리도록 명령
            //item.EnsureVisible();
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
                    currentUbarState = "Operate";

                    // 상단버튼 활성화
                    btn_Operate.Enabled = true;
                    btn_Maint.Enabled = true;

                    // 모드에 맞게 버튼 색상 스타일 출력
                    UpdateModeButtonStyles();

                    // 모든 챔버 문 초기 닫기 출력
                    EtherCAT_M.Digital_Output(5, false);
                    EtherCAT_M.Digital_Output(4, true);
                    EtherCAT_M.Digital_Output(8, false);
                    EtherCAT_M.Digital_Output(7, true);
                    EtherCAT_M.Digital_Output(11, false);
                    EtherCAT_M.Digital_Output(10, true);
                    WriteSystemLog("INFO", "장비 초기화 세팅: 모든 챔버 도어 CLOSE 명령 출력");

                    // 황색등 점등
                    EtherCAT_M.Digital_Output(1, true);
                    WriteSystemLog("INFO", "타워램프 상태 변경: 황색등(Yellow) ON (장비 대기)");
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
        private void btn_Operate_Click(object sender, EventArgs e)
        {
            if (currentUbarState == "Operate") return; // 중복 제어 차단

            currentUbarState = "Operate";

            UpdateModeButtonStyles();

            ForceStopAllChambers();

            Mainpnl_CurrentStateGUI();

            EtherCAT_M.Digital_Output(1, true);
        }

        private void btn_maint_Click(object sender, EventArgs e)
        {
            if (currentUbarState == "Maint") return; // 중복 제어 차단

            currentUbarState = "Maint";

            UpdateModeButtonStyles();

            ForceStopAllChambers();

            Mainpnl_MaintGUI();

            EtherCAT_M.Digital_Output(1, true);
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

            log.Debug("인터록 완료: 모든 공정 인터록 및 GUI 상태 리셋 완료");
        }

        private void btnMode_Paint(object sender, PaintEventArgs e)
        {
            // 화면 렌더링 영역은 부하 조절 및 가독성을 위해 로그 생략
            Button btn = (Button)sender;
            bool isActive = (currentUbarState == "Operate" && btn == btn_Operate) ||
                            (currentUbarState == "Maint" && btn == btn_Maint);

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
            bool isManual = (currentUbarState == "Maint");

            btn_Operate.BackColor = !isManual ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            btn_Operate.ForeColor = !isManual ? Color.White : Color.DimGray;
            btn_Operate.Invalidate();

            btn_Maint.BackColor = isManual ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            btn_Maint.ForeColor = isManual ? Color.White : Color.DimGray;
            btn_Maint.Invalidate();
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

        internal void servoMotorON()
        {
            EtherCAT_M.Axis1_ON();
            EtherCAT_M.Axis2_ON();
        }

        internal void servoMotorOFF()
        {
            EtherCAT_M.Axis1_OFF();
            EtherCAT_M.Axis2_OFF();
        }
        internal void setBasicPoint()
        {
            EtherCAT_M.Axis1_UD_Homming(); //상하 원점복귀
            EtherCAT_M.Axis2_LR_Homming(); //좌우 원점복귀
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(this);
            setting.Show();
        }

        private void Mainpnl_CurrentStateGUI() {
            Mainpnl.Controls.Clear();
            currentUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(currentUI);
        }

        private void Mainpnl_MaintGUI()
        {
            Mainpnl.Controls.Clear();
            maintui.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(maintui);
        }

        internal void timer1_Tick(object sender, EventArgs e)
        {
            if (!isConnect || EtherCAT_M == null) return;
            if (isAxisMoving) return;

            try
            {
                string currentUDPos = EtherCAT_M.Axis1_is_PosData();
                string currentLRPos = EtherCAT_M.Axis2_is_PosData();

                if (maintui != null)
                {
                    maintui.UpdateAxisPosition(currentUDPos, currentLRPos);
                }
            }
            catch (Exception ex)
            {
                log.Error("UI 모니터링 타이머 처리 중 예외 발생: ", ex);
            }
        }
    }
}