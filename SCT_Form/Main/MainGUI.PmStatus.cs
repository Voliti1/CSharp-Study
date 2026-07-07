using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCT_Form
{
    // PM 상태 라벨(정상/알람) 색상 관리. 알람이 뜨면 라벨을 빨간색으로 바꾸고 원래 색을
    // 기억해뒀다가, Alarm Reset 시 그 색으로 복원한다.
    public partial class MainGUI
    {
        private void InitializePmStatusColors()
        {
            pmStatusColors["PM A"] = lbl_PMAStatus.ForeColor;
            pmStatusColors["PM B"] = lbl_PMBStatus.ForeColor;
            pmStatusColors["PM C"] = lbl_PMCStatus.ForeColor;
        }

        internal void RaisePmAlarm(string pmName, string level, string message)
        {
            string normalizedPmName = NormalizePmName(pmName);
            Label statusLabel = GetPmStatusLabel(normalizedPmName);
            if (statusLabel == null) return;

            activePmAlarms.Add(normalizedPmName);
            statusLabel.ForeColor = Color.Red;

            string alarmMessage = normalizedPmName + " " + (message ?? string.Empty);
            WriteSystemLog("Alarm", level, alarmMessage.Trim());

            if (settings != null)
            {
                ApplyTowerLampStatus(settings.AlarmLampStatus);
            }
        }

        internal bool TrySyncPmStatusLabel(Label targetLabel, Color statusColor)
        {
            string pmName = GetPmNameByStatusLabel(targetLabel);
            if (string.IsNullOrEmpty(pmName)) return false;

            pmStatusColors[pmName] = statusColor;
            targetLabel.ForeColor = activePmAlarms.Contains(pmName) ? Color.Red : statusColor;
            return true;
        }

        private void ResetPmAlarms()
        {
            if (isAxisPositionFault)
            {
                isAxisPositionFault = false;
                WriteSystemLog("Alarm", "INFO", "UD 위치 확인 오류(Axis Position Fault) 해제됨");
            }

            if (activePmAlarms.Count == 0)
            {
                WriteSystemLog("Alarm", "INFO", "Alarm Reset 요청: Active PM Alarm 없음");
                return;
            }

            foreach (string pmName in activePmAlarms.ToList())
            {
                Label statusLabel = GetPmStatusLabel(pmName);
                if (statusLabel != null)
                {
                    Color statusColor;
                    statusLabel.ForeColor = pmStatusColors.TryGetValue(pmName, out statusColor) ? statusColor : Color.Gray;
                }
            }

            activePmAlarms.Clear();
            ApplyTowerLampForMode();
            WriteSystemLog("Alarm", "INFO", "Alarm Reset 완료: PM Alarm 표시 해제");
        }

        private string NormalizePmName(string pmName)
        {
            string value = string.IsNullOrWhiteSpace(pmName) ? string.Empty : pmName.Trim().ToUpper();
            if (value == "PMA" || value == "PM A") return "PM A";
            if (value == "PMB" || value == "PM B") return "PM B";
            if (value == "PMC" || value == "PM C") return "PM C";
            return string.Empty;
        }

        private Label GetPmStatusLabel(string pmName)
        {
            if (pmName == "PM A") return lbl_PMAStatus;
            if (pmName == "PM B") return lbl_PMBStatus;
            if (pmName == "PM C") return lbl_PMCStatus;
            return null;
        }

        private string GetPmNameByStatusLabel(Label statusLabel)
        {
            if (statusLabel == lbl_PMAStatus) return "PM A";
            if (statusLabel == lbl_PMBStatus) return "PM B";
            if (statusLabel == lbl_PMCStatus) return "PM C";
            return string.Empty;
        }
    }
}
