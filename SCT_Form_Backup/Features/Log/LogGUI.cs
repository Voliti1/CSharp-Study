using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class LogGUI : UserControl
    {
        private static readonly string[] ColumnNames = { "Time", "Category", "Level", "Message" };
        private static readonly Random Random = new Random();

        private readonly List<LogFilter> activeFilters = new List<LogFilter>();
        private readonly List<SystemLogEntry> currentDisplayLogs = new List<SystemLogEntry>();
        private MainGUI main;
        private ComboBox cbox_FilterValue1;
        private ComboBox cbox_FilterValue2;
        private ComboBox cbox_FilterValue3;
        private bool showFullLog;
        private bool selectMode;
        private bool sortAscending;

        public LogGUI()
        {
            InitializeComponent();
            InitializeLogScreen();
        }

        public LogGUI(MainGUI mainGUI)
            : this()
        {
            this.main = mainGUI;
            RefreshLogs(true);
        }

        private void InitializeLogScreen()
        {
            LogView.View = View.Details;
            LogView.FullRowSelect = true;
            LogView.GridLines = true;
            LogView.HideSelection = false;
            LogView.MultiSelect = false;
            LogView.Columns.Clear();
            LogView.Columns.Add("Time", 150, HorizontalAlignment.Center);
            LogView.Columns.Add("Category", 130, HorizontalAlignment.Center);
            LogView.Columns.Add("Level", 90, HorizontalAlignment.Center);
            LogView.Columns.Add("Message", 520, HorizontalAlignment.Left);
            LogView.ItemSelectionChanged += LogView_ItemSelectionChanged;

            InitializeLogDetailView();

            InitializeColumnComboBox(cbox_Column1, "Level");
            InitializeColumnComboBox(cbox_Column2, "Category");
            InitializeColumnComboBox(cbox_Column3, "Message");
            cbox_FilterValue1 = CreateFilterValueComboBox(cbox_Column1, txtbox_1);
            cbox_FilterValue2 = CreateFilterValueComboBox(cbox_Column2, txtbox_2);
            cbox_FilterValue3 = CreateFilterValueComboBox(cbox_Column3, txtbox_3);
            UpdateFilterValueControl(cbox_Column1, txtbox_1);
            UpdateFilterValueControl(cbox_Column2, txtbox_2);
            UpdateFilterValueControl(cbox_Column3, txtbox_3);

            btn_Alarm.Click += btn_Alarm_Click;
            btn_FullLog.Click += btn_FullLog_Click;
            btn_SelectMode.Click += btn_SelectMode_Click;
            btn_RandomAlarm.Click += btn_RandomAlarm_Click;

            ApplyLogMode(false);
            ApplySelectMode(false);
            ApplySortDirection(false);
        }

        private void InitializeLogDetailView()
        {
            LogDetailView.View = View.Details;
            LogDetailView.FullRowSelect = true;
            LogDetailView.GridLines = true;
            LogDetailView.HideSelection = false;
            LogDetailView.MultiSelect = false;
            LogDetailView.Columns.Clear();
            LogDetailView.Columns.Add("Field", 120, HorizontalAlignment.Center);
            LogDetailView.Columns.Add("Value", 900, HorizontalAlignment.Left);
        }

        private void InitializeColumnComboBox(ComboBox comboBox, string selectedColumn)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.Clear();
            foreach (string columnName in ColumnNames)
            {
                comboBox.Items.Add(columnName);
            }
            comboBox.SelectedItem = selectedColumn;
        }

        private ComboBox CreateFilterValueComboBox(ComboBox columnComboBox, TextBox textBox)
        {
            ComboBox valueComboBox = new ComboBox();
            valueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            valueComboBox.Dock = DockStyle.Fill;
            valueComboBox.Margin = textBox.Margin;
            valueComboBox.Visible = false;
            valueComboBox.DropDown += FilterValueComboBox_DropDown;

            pnl_Filter.Controls.Add(
                valueComboBox,
                pnl_Filter.GetColumn(textBox),
                pnl_Filter.GetRow(textBox));

            columnComboBox.SelectedIndexChanged += FilterColumnComboBox_SelectedIndexChanged;
            return valueComboBox;
        }

        private void FilterColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox columnComboBox = sender as ComboBox;
            if (columnComboBox == cbox_Column1) UpdateFilterValueControl(cbox_Column1, txtbox_1);
            if (columnComboBox == cbox_Column2) UpdateFilterValueControl(cbox_Column2, txtbox_2);
            if (columnComboBox == cbox_Column3) UpdateFilterValueControl(cbox_Column3, txtbox_3);
        }

        private void FilterValueComboBox_DropDown(object sender, EventArgs e)
        {
            if (sender == cbox_FilterValue1) PopulateFilterValueComboBox(cbox_Column1, cbox_FilterValue1);
            if (sender == cbox_FilterValue2) PopulateFilterValueComboBox(cbox_Column2, cbox_FilterValue2);
            if (sender == cbox_FilterValue3) PopulateFilterValueComboBox(cbox_Column3, cbox_FilterValue3);
        }

        private void UpdateFilterValueControl(ComboBox columnComboBox, TextBox textBox)
        {
            ComboBox valueComboBox = GetFilterValueComboBox(columnComboBox);
            if (valueComboBox == null) return;

            string columnName = Convert.ToString(columnComboBox.SelectedItem);
            bool useComboBox = columnName == "Category" || columnName == "Level";

            textBox.Visible = !useComboBox;
            valueComboBox.Visible = useComboBox;

            if (useComboBox)
            {
                textBox.Clear();
                PopulateFilterValueComboBox(columnComboBox, valueComboBox);
                valueComboBox.BringToFront();
            }
            else
            {
                valueComboBox.SelectedIndex = -1;
                textBox.BringToFront();
            }
        }

        private ComboBox GetFilterValueComboBox(ComboBox columnComboBox)
        {
            if (columnComboBox == cbox_Column1) return cbox_FilterValue1;
            if (columnComboBox == cbox_Column2) return cbox_FilterValue2;
            if (columnComboBox == cbox_Column3) return cbox_FilterValue3;
            return null;
        }

        private void PopulateFilterValueComboBox(ComboBox columnComboBox, ComboBox valueComboBox)
        {
            string selectedValue = Convert.ToString(valueComboBox.SelectedItem);
            string columnName = Convert.ToString(columnComboBox.SelectedItem);

            valueComboBox.BeginUpdate();
            valueComboBox.Items.Clear();

            if (columnName == "Level")
            {
                valueComboBox.Items.AddRange(new object[] { "DEBUG", "INFO", "WARN", "ERROR", "FATAL" });
            }
            else if (columnName == "Category")
            {
                foreach (string category in GetCategoryFilterValues())
                {
                    valueComboBox.Items.Add(category);
                }
            }

            if (!string.IsNullOrWhiteSpace(selectedValue) && valueComboBox.Items.Contains(selectedValue))
            {
                valueComboBox.SelectedItem = selectedValue;
            }

            valueComboBox.EndUpdate();
        }

        private List<string> GetCategoryFilterValues()
        {
            IEnumerable<SystemLogEntry> logs = main == null
                ? currentDisplayLogs
                : main.GetSystemLogSnapshot();

            return logs
                .Select(item => item.Category)
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(item => item)
                .ToList();
        }

        internal void RefreshLogs(bool forceRefresh)
        {
            if (main == null) return;

            HashSet<long> selectedIds = GetSelectedLogIds();
            IEnumerable<SystemLogEntry> logQuery = main.GetSystemLogSnapshot()
                .Where(item => main.ShowDebugLog || item.Level != "DEBUG")
                .Where(item => showFullLog || IsAlarmLog(item))
                .Where(MatchesFilters);

            List<SystemLogEntry> displayLogs = sortAscending
                ? logQuery.OrderBy(item => item.Time).Take(main.MaxDisplayLogCount).ToList()
                : logQuery.OrderByDescending(item => item.Time).Take(main.MaxDisplayLogCount).ToList();

            if (!forceRefresh && IsSameDisplay(displayLogs)) return;

            currentDisplayLogs.Clear();
            currentDisplayLogs.AddRange(displayLogs);

            LogView.BeginUpdate();
            LogView.Items.Clear();
            foreach (SystemLogEntry logEntry in currentDisplayLogs)
            {
                ListViewItem item = CreateLogViewItem(logEntry);
                item.Selected = selectedIds.Contains(logEntry.Id);
                LogView.Items.Add(item);
            }
            LogView.EndUpdate();

            UpdateSelectedLogDetail();
        }

        private bool IsSameDisplay(List<SystemLogEntry> displayLogs)
        {
            if (displayLogs.Count != currentDisplayLogs.Count) return false;

            for (int index = 0; index < displayLogs.Count; index++)
            {
                if (displayLogs[index].Id != currentDisplayLogs[index].Id) return false;
            }

            return true;
        }

        private ListViewItem CreateLogViewItem(SystemLogEntry logEntry)
        {
            ListViewItem item = new ListViewItem(logEntry.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            item.SubItems.Add(logEntry.Category);
            item.SubItems.Add(logEntry.Level);
            item.SubItems.Add(logEntry.Message);
            item.Tag = logEntry.Id;

            if (logEntry.Level == "ERROR" || logEntry.Level == "FATAL")
            {
                item.BackColor = Color.MistyRose;
                item.ForeColor = Color.DarkRed;
            }
            else if (logEntry.Level == "WARN")
            {
                item.BackColor = Color.LemonChiffon;
                item.ForeColor = Color.DarkOrange;
            }
            else
            {
                item.BackColor = Color.White;
                item.ForeColor = Color.Black;
            }

            return item;
        }

        private bool IsAlarmLog(SystemLogEntry logEntry)
        {
            return logEntry.Level == "WARN" || logEntry.Level == "ERROR" || logEntry.Level == "FATAL";
        }

        private bool MatchesFilters(SystemLogEntry logEntry)
        {
            foreach (LogFilter filter in activeFilters)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;

                string cellValue = GetLogColumnValue(logEntry, filter.ColumnName);
                if (cellValue.IndexOf(filter.Value, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private string GetLogColumnValue(SystemLogEntry logEntry, string columnName)
        {
            if (columnName == "Time") return logEntry.Time.ToString("yyyy-MM-dd HH:mm:ss");
            if (columnName == "Category") return logEntry.Category;
            if (columnName == "Level") return logEntry.Level;
            if (columnName == "Message") return logEntry.Message;
            return string.Empty;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void btn_Search_Click_1(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void ApplySearchFilters()
        {
            activeFilters.Clear();
            AddFilter(cbox_Column1, txtbox_1);
            AddFilter(cbox_Column2, txtbox_2);
            AddFilter(cbox_Column3, txtbox_3);
            RefreshLogs(true);
        }

        private void AddFilter(ComboBox comboBox, TextBox textBox)
        {
            string value = GetFilterValue(comboBox, textBox);
            if (string.IsNullOrWhiteSpace(value)) return;

            activeFilters.Add(new LogFilter
            {
                ColumnName = Convert.ToString(comboBox.SelectedItem),
                Value = value
            });
        }

        private string GetFilterValue(ComboBox columnComboBox, TextBox textBox)
        {
            string columnName = Convert.ToString(columnComboBox.SelectedItem);
            if (columnName == "Category" || columnName == "Level")
            {
                ComboBox valueComboBox = GetFilterValueComboBox(columnComboBox);
                return Convert.ToString(valueComboBox == null ? null : valueComboBox.SelectedItem).Trim();
            }

            return (textBox.Text ?? string.Empty).Trim();
        }

        private void btn_ClearFilter_Click(object sender, EventArgs e)
        {
            txtbox_1.Clear();
            txtbox_2.Clear();
            txtbox_3.Clear();
            ClearFilterComboBox(cbox_FilterValue1);
            ClearFilterComboBox(cbox_FilterValue2);
            ClearFilterComboBox(cbox_FilterValue3);
            cbox_Column1.SelectedItem = "Level";
            cbox_Column2.SelectedItem = "Category";
            cbox_Column3.SelectedItem = "Message";
            UpdateFilterValueControl(cbox_Column1, txtbox_1);
            UpdateFilterValueControl(cbox_Column2, txtbox_2);
            UpdateFilterValueControl(cbox_Column3, txtbox_3);
            activeFilters.Clear();
            RefreshLogs(true);
        }

        private void ClearFilterComboBox(ComboBox comboBox)
        {
            if (comboBox == null) return;
            comboBox.SelectedIndex = -1;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            List<SystemLogEntry> exportLogs = GetExportLogs();
            if (exportLogs.Count == 0)
            {
                MessageBox.Show("Export할 로그가 없습니다.", "Log Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV File (*.csv)|*.csv";
                dialog.FileName = GetDefaultExportFileName(exportLogs);
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                File.WriteAllText(dialog.FileName, BuildCsv(exportLogs), Encoding.UTF8);
                main?.WriteSystemLog("Data Export", "INFO", "Log Export 완료: " + dialog.FileName);
            }
        }

        private List<SystemLogEntry> GetExportLogs()
        {
            HashSet<long> selectedIds = GetSelectedLogIds();
            if (selectMode && selectedIds.Count > 0)
            {
                return currentDisplayLogs.Where(item => selectedIds.Contains(item.Id)).ToList();
            }

            return currentDisplayLogs.ToList();
        }

        private string GetDefaultExportFileName(List<SystemLogEntry> exportLogs)
        {
            if (selectMode && GetSelectedLogIds().Count > 0) return "SelectedLog.csv";
            if (activeFilters.Count == 0) return "검색필터내용나열.csv";

            string filterText = string.Join("_", activeFilters.Select(item => item.ColumnName + "-" + item.Value));
            return SanitizeFileName(filterText) + ".csv";
        }

        private string BuildCsv(List<SystemLogEntry> exportLogs)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Time,Category,Level,Message");

            foreach (SystemLogEntry logEntry in exportLogs)
            {
                builder.Append(CsvCell(logEntry.Time.ToString("yyyy-MM-dd HH:mm:ss")));
                builder.Append(",");
                builder.Append(CsvCell(logEntry.Category));
                builder.Append(",");
                builder.Append(CsvCell(logEntry.Level));
                builder.Append(",");
                builder.AppendLine(CsvCell(logEntry.Message));
            }

            return builder.ToString();
        }

        private string CsvCell(string value)
        {
            string safeValue = value ?? string.Empty;
            return "\"" + safeValue.Replace("\"", "\"\"") + "\"";
        }

        private string SanitizeFileName(string fileName)
        {
            string safeName = string.IsNullOrWhiteSpace(fileName) ? "검색필터내용나열" : fileName;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                safeName = safeName.Replace(invalidChar, '_');
            }

            return safeName;
        }

        private void btn_LogDelete_Click(object sender, EventArgs e)
        {
            if (main == null) return;
            if (!main.EnsureAdminSettingAllowed()) return;

            HashSet<long> selectedIds = GetSelectedLogIds();
            if (selectedIds.Count == 0)
            {
                MessageBox.Show("삭제할 로그를 선택하세요.", "Log Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "선택한 로그를 삭제하시겠습니까?",
                "Log Delete",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result != DialogResult.OK) return;

            main.DeleteSystemLogs(selectedIds);
            RefreshLogs(true);
        }

        private void btn_Alarm_Click(object sender, EventArgs e)
        {
            ApplyLogMode(false);
            RefreshLogs(true);
        }

        private void btn_FullLog_Click(object sender, EventArgs e)
        {
            ApplyLogMode(true);
            RefreshLogs(true);
        }

        private void ApplyLogMode(bool fullLog)
        {
            showFullLog = fullLog;
            Log.Text = showFullLog ? "Full Log" : "Alarm Log";

            btn_Alarm.UseVisualStyleBackColor = false;
            btn_FullLog.UseVisualStyleBackColor = false;
            btn_Alarm.BackColor = showFullLog ? Color.FromArgb(60, 60, 60) : Color.SkyBlue;
            btn_FullLog.BackColor = showFullLog ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            btn_Alarm.ForeColor = showFullLog ? Color.Silver : Color.White;
            btn_FullLog.ForeColor = showFullLog ? Color.White : Color.Silver;
        }

        private void btn_SelectMode_Click(object sender, EventArgs e)
        {
            ApplySelectMode(!selectMode);
        }

        private void ApplySelectMode(bool enabled)
        {
            selectMode = enabled;
            LogView.MultiSelect = enabled;
            if (!enabled)
            {
                foreach (ListViewItem item in LogView.SelectedItems.Cast<ListViewItem>().ToList())
                {
                    item.Selected = false;
                }
            }

            btn_SelectMode.Text = enabled ? "Select ON" : "Select Mode";
            btn_SelectMode.BackColor = enabled ? Color.SkyBlue : SystemColors.Control;
            btn_SelectMode.ForeColor = enabled ? Color.White : Color.Black;
        }

        private void LogView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                ShowLogDetailByItem(e.Item);
                return;
            }

            if (LogView.SelectedItems.Count == 0)
            {
                ClearLogDetailView();
            }
        }

        private void UpdateSelectedLogDetail()
        {
            if (LogView.SelectedItems.Count == 0)
            {
                ClearLogDetailView();
                return;
            }

            ShowLogDetailByItem(LogView.SelectedItems[LogView.SelectedItems.Count - 1]);
        }

        private void ShowLogDetailByItem(ListViewItem item)
        {
            if (item == null || !(item.Tag is long)) return;

            long logId = (long)item.Tag;
            SystemLogEntry logEntry = currentDisplayLogs.FirstOrDefault(entry => entry.Id == logId);
            if (logEntry == null) return;

            ShowLogDetail(logEntry);
        }

        private void ShowLogDetail(SystemLogEntry logEntry)
        {
            LogDetailView.BeginUpdate();
            LogDetailView.Items.Clear();
            AddLogDetailRow("Time", logEntry.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            AddLogDetailRow("Category", logEntry.Category);
            AddLogDetailRow("Level", logEntry.Level);
            AddLogDetailRow("Message", logEntry.Message);
            LogDetailView.EndUpdate();
        }

        private void AddLogDetailRow(string fieldName, string value)
        {
            ListViewItem item = new ListViewItem(fieldName);
            item.SubItems.Add(value ?? string.Empty);
            LogDetailView.Items.Add(item);
        }

        private void ClearLogDetailView()
        {
            LogDetailView.Items.Clear();
        }

        private HashSet<long> GetSelectedLogIds()
        {
            HashSet<long> selectedIds = new HashSet<long>();
            foreach (ListViewItem item in LogView.SelectedItems)
            {
                if (item.Tag is long)
                {
                    selectedIds.Add((long)item.Tag);
                }
            }

            return selectedIds;
        }

        private void btn_RandomAlarm_Click(object sender, EventArgs e)
        {
            string[] levels = { "WARN", "ERROR" };
            string[] pmNames = { "PM A", "PM B", "PM C" };
            string[] messages =
            {
                "Random alarm: Chamber pressure warning",
                "Random alarm: Robot axis position error",
                "Random alarm: EtherCAT communication warning",
                "Random alarm: Process timeout detected",
                "Random alarm: Door interlock error"
            };

            string level = levels[Random.Next(levels.Length)];
            string pmName = pmNames[Random.Next(pmNames.Length)];
            string message = messages[Random.Next(messages.Length)];
            main?.RaisePmAlarm(pmName, level, message);
        }

        private class LogFilter
        {
            public string ColumnName { get; set; }
            public string Value { get; set; }
        }

        private void btn_Asc_Click(object sender, EventArgs e)
        {
            ApplySortDirection(!sortAscending);
            RefreshLogs(true);
        }

        private void ApplySortDirection(bool ascending)
        {
            sortAscending = ascending;
            btn_Asc.Text = sortAscending ? "ASC" : "DESC";
            btn_Asc.BackColor = sortAscending ? Color.SkyBlue : SystemColors.Control;
            btn_Asc.ForeColor = sortAscending ? Color.White : Color.Black;
        }
    }

    internal class SystemLogEntry
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }

        public SystemLogEntry Clone()
        {
            return new SystemLogEntry
            {
                Id = Id,
                Time = Time,
                Category = Category,
                Level = Level,
                Message = Message
            };
        }
    }
}
