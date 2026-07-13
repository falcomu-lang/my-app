using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeDetectionParameterSummaryControls()
        {
            _tabPageDetectionParameterSummary = tabPageDetectionParameterSummary;
            _textBoxDetectionMainParameterName = textBoxDetectionMainParameterName;
            _buttonDetectionMainParameterConfirm = buttonDetectionMainParameterConfirm;
            _buttonDetectionMainParameterMoveUp = buttonDetectionMainParameterMoveUp;
            _buttonDetectionMainParameterMoveDown = buttonDetectionMainParameterMoveDown;
            _buttonDetectionMainParameterSaveOrder = buttonDetectionMainParameterSaveOrder;
            _listBoxDetectionMainParameter = listBoxDetectionMainParameter;
            _buttonDetectionSubParameter1MoveUp = buttonDetectionSubParameter1MoveUp;
            _buttonDetectionSubParameter1MoveDown = buttonDetectionSubParameter1MoveDown;
            _buttonDetectionSubParameter1SaveOrder = buttonDetectionSubParameter1SaveOrder;
            _listBoxDetectionSubParameter1 = listBoxDetectionSubParameter1;
            _checkBoxDetectionSubParameter1Enabled = checkBoxDetectionSubParameter1Enabled;
            _buttonDetectionSubParameter2MoveUp = buttonDetectionSubParameter2MoveUp;
            _buttonDetectionSubParameter2MoveDown = buttonDetectionSubParameter2MoveDown;
            _buttonDetectionSubParameter2SaveOrder = buttonDetectionSubParameter2SaveOrder;
            _listBoxDetectionSubParameter2 = listBoxDetectionSubParameter2;
            _checkBoxDetectionSubParameter2Enabled = checkBoxDetectionSubParameter2Enabled;
            _buttonDetectionSubParameter3MoveUp = buttonDetectionSubParameter3MoveUp;
            _buttonDetectionSubParameter3MoveDown = buttonDetectionSubParameter3MoveDown;
            _buttonDetectionSubParameter3SaveOrder = buttonDetectionSubParameter3SaveOrder;
            _listBoxDetectionSubParameter3 = listBoxDetectionSubParameter3;
            _checkBoxDetectionSubParameter3Enabled = checkBoxDetectionSubParameter3Enabled;
            _buttonDetectionSaveParameterReference = buttonDetectionSaveParameterReference;

            if (_buttonDetectionMainParameterConfirm != null)
            {
                _buttonDetectionMainParameterConfirm.Click += DetectionMainParameterConfirmButton_Click;
            }

            if (_buttonDetectionMainParameterMoveUp != null)
            {
                _buttonDetectionMainParameterMoveUp.Click += DetectionMainParameterMoveUpButton_Click;
            }

            if (_buttonDetectionMainParameterMoveDown != null)
            {
                _buttonDetectionMainParameterMoveDown.Click += DetectionMainParameterMoveDownButton_Click;
            }

            if (_buttonDetectionMainParameterSaveOrder != null)
            {
                _buttonDetectionMainParameterSaveOrder.Click += DetectionMainParameterSaveOrderButton_Click;
            }

            if (_listBoxDetectionMainParameter != null)
            {
                _listBoxDetectionMainParameter.SelectedIndexChanged += DetectionMainParameterListBox_SelectedIndexChanged;
            }

            if (_buttonDetectionSubParameter1MoveUp != null)
            {
                _buttonDetectionSubParameter1MoveUp.Click += DetectionSubParameter1MoveUpButton_Click;
            }

            if (_buttonDetectionSubParameter1MoveDown != null)
            {
                _buttonDetectionSubParameter1MoveDown.Click += DetectionSubParameter1MoveDownButton_Click;
            }

            if (_buttonDetectionSubParameter1SaveOrder != null)
            {
                _buttonDetectionSubParameter1SaveOrder.Click += DetectionSubParameter1SaveOrderButton_Click;
            }

            if (_buttonDetectionSubParameter2MoveUp != null)
            {
                _buttonDetectionSubParameter2MoveUp.Click += DetectionSubParameter2MoveUpButton_Click;
            }

            if (_buttonDetectionSubParameter2MoveDown != null)
            {
                _buttonDetectionSubParameter2MoveDown.Click += DetectionSubParameter2MoveDownButton_Click;
            }

            if (_buttonDetectionSubParameter2SaveOrder != null)
            {
                _buttonDetectionSubParameter2SaveOrder.Click += DetectionSubParameter2SaveOrderButton_Click;
            }

            if (_buttonDetectionSubParameter3MoveUp != null)
            {
                _buttonDetectionSubParameter3MoveUp.Click += DetectionSubParameter3MoveUpButton_Click;
            }

            if (_buttonDetectionSubParameter3MoveDown != null)
            {
                _buttonDetectionSubParameter3MoveDown.Click += DetectionSubParameter3MoveDownButton_Click;
            }

            if (_buttonDetectionSubParameter3SaveOrder != null)
            {
                _buttonDetectionSubParameter3SaveOrder.Click += DetectionSubParameter3SaveOrderButton_Click;
            }

            if (_buttonDetectionSaveParameterReference != null)
            {
                _buttonDetectionSaveParameterReference.Click += DetectionSaveParameterReferenceButton_Click;
            }

            LoadDetectionParameterReferenceList();
            LoadDetectionSubParameter1List();
        }

        private void DetectionMainParameterConfirmButton_Click(object sender, EventArgs e)
        {
            var parameterName = _textBoxDetectionMainParameterName == null
                ? string.Empty
                : _textBoxDetectionMainParameterName.Text.Trim();

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                return;
            }

            foreach (var existingName in _detectionMainParameters)
            {
                if (string.Equals(existingName, parameterName, StringComparison.OrdinalIgnoreCase))
                {
                    if (_listBoxDetectionMainParameter != null)
                    {
                        _listBoxDetectionMainParameter.SelectedItem = existingName;
                    }

                    if (_textBoxDetectionMainParameterName != null)
                    {
                        _textBoxDetectionMainParameterName.SelectAll();
                        _textBoxDetectionMainParameterName.Focus();
                    }

                    return;
                }
            }

            _detectionMainParameters.Add(parameterName);
            SaveDetectionParameterReferenceList();
            RefreshDetectionMainParameterList();

            if (_textBoxDetectionMainParameterName != null)
            {
                _textBoxDetectionMainParameterName.Clear();
                _textBoxDetectionMainParameterName.Focus();
            }
        }

        private void LoadDetectionParameterReferenceList()
        {
            _detectionMainParameters.Clear();
            _detectionParameterReferences.Clear();
            _detectionMainParameters.AddRange(
                _parameterReferenceListRepository.Load(GetParameterReferenceListPath()));
            foreach (var pair in _parameterReferenceListRepository.LoadReferences(GetParameterReferenceListPath()))
            {
                _detectionParameterReferences[pair.Key] = pair.Value;
            }

            RefreshDetectionMainParameterList();
            RefreshContinuousInspectionMainParameterItems();
            ApplyDetectionParameterReferenceSelection();
        }

        private void SaveDetectionParameterReferenceList()
        {
            _parameterReferenceListRepository.Save(
                GetParameterReferenceListPath(),
                _detectionMainParameters,
                _detectionParameterReferences);
        }

        private void RefreshDetectionMainParameterList()
        {
            if (_listBoxDetectionMainParameter == null)
            {
                return;
            }

            _listBoxDetectionMainParameter.BeginUpdate();
            try
            {
                _listBoxDetectionMainParameter.Items.Clear();
                foreach (var parameterName in _detectionMainParameters)
                {
                    _listBoxDetectionMainParameter.Items.Add(parameterName);
                }
            }
            finally
            {
                _listBoxDetectionMainParameter.EndUpdate();
            }
        }

        private void DetectionMainParameterMoveUpButton_Click(object sender, EventArgs e)
        {
            MoveDetectionMainParameter(-1);
        }

        private void DetectionMainParameterMoveDownButton_Click(object sender, EventArgs e)
        {
            MoveDetectionMainParameter(1);
        }

        private void DetectionMainParameterSaveOrderButton_Click(object sender, EventArgs e)
        {
            SaveDetectionParameterReferenceList();
            MessageBox.Show(this, "Main parameter order saved.", "Detection Parameter Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DetectionMainParameterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyDetectionParameterReferenceSelection();
        }

        private void MoveDetectionMainParameter(int direction)
        {
            if (_listBoxDetectionMainParameter == null)
            {
                return;
            }

            var selectedIndex = _listBoxDetectionMainParameter.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }

            var targetIndex = selectedIndex + direction;
            if (targetIndex < 0 || targetIndex >= _detectionMainParameters.Count)
            {
                return;
            }

            var movedItem = _detectionMainParameters[selectedIndex];
            _detectionMainParameters.RemoveAt(selectedIndex);
            _detectionMainParameters.Insert(targetIndex, movedItem);
            RefreshDetectionMainParameterList();
            _listBoxDetectionMainParameter.SelectedIndex = targetIndex;
        }

        private string GetParameterReferenceListPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameterReferenceList.ini");
        }

        private void ApplyDetectionParameterReferenceSelection()
        {
            var selectedMainParameter = _listBoxDetectionMainParameter == null
                ? null
                : _listBoxDetectionMainParameter.SelectedItem as string;

            DetectionParameterReference reference = null;
            if (!string.IsNullOrWhiteSpace(selectedMainParameter))
            {
                _detectionParameterReferences.TryGetValue(selectedMainParameter, out reference);
            }

            if (reference == null)
            {
                reference = new DetectionParameterReference
                {
                    MainParameterName = selectedMainParameter,
                    InnerSettingsProfileIndex = 0
                };
                if (!string.IsNullOrWhiteSpace(selectedMainParameter))
                {
                    _detectionParameterReferences[selectedMainParameter] = reference;
                    SaveDetectionParameterReferenceList();
                }
            }
            else if (reference.InnerSettingsProfileIndex < 0)
            {
                reference.InnerSettingsProfileIndex = 0;
                SaveDetectionParameterReferenceList();
            }

            ApplyDetectionSubParameterSelection(_checkBoxDetectionSubParameter1Enabled, _listBoxDetectionSubParameter1, reference?.SubParameter1);
            ApplyDetectionSubParameterSelection(_checkBoxDetectionSubParameter2Enabled, _listBoxDetectionSubParameter2, reference?.SubParameter2);
            ApplyDetectionSubParameterSelection(_checkBoxDetectionSubParameter3Enabled, _listBoxDetectionSubParameter3, reference?.SubParameter3);
        }

        private void LoadDetectionSubParameter1List()
        {
            var items = new List<string>(_detectionSubParameter1Items);
            if (items.Count == 0)
            {
                items = LoadSettingListSortItems();
            }

            if (items.Count == 0)
            {
                items = LoadSettingSectionNames();
            }

            _detectionSubParameter1Items.Clear();
            _detectionSubParameter1Items.AddRange(items);
            RefreshDetectionSubParameter1List();
        }

        private void RefreshDetectionSubParameter1List()
        {
            RefreshDetectionSubParameterListBox(_listBoxDetectionSubParameter1);
            RefreshDetectionSubParameterListBox(_listBoxDetectionSubParameter2);
            RefreshDetectionSubParameterListBox(_listBoxDetectionSubParameter3);
        }

        private void DetectionSubParameter1MoveUpButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter1, -1);
        }

        private void DetectionSubParameter1MoveDownButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter1, 1);
        }

        private void DetectionSubParameter1SaveOrderButton_Click(object sender, EventArgs e)
        {
            SaveDetectionSubParameterOrder();
        }

        private void DetectionSubParameter2MoveUpButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter2, -1);
        }

        private void DetectionSubParameter2MoveDownButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter2, 1);
        }

        private void DetectionSubParameter2SaveOrderButton_Click(object sender, EventArgs e)
        {
            SaveDetectionSubParameterOrder();
        }

        private void DetectionSubParameter3MoveUpButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter3, -1);
        }

        private void DetectionSubParameter3MoveDownButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter(_listBoxDetectionSubParameter3, 1);
        }

        private void DetectionSubParameter3SaveOrderButton_Click(object sender, EventArgs e)
        {
            SaveDetectionSubParameterOrder();
        }

        private void DetectionSaveParameterReferenceButton_Click(object sender, EventArgs e)
        {
            var selectedMainParameter = _listBoxDetectionMainParameter == null
                ? null
                : _listBoxDetectionMainParameter.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(selectedMainParameter))
            {
                return;
            }

            _detectionParameterReferences[selectedMainParameter] = new DetectionParameterReference
            {
                MainParameterName = selectedMainParameter,
                SubParameter1 = GetDetectionSubParameterValue(_checkBoxDetectionSubParameter1Enabled, _listBoxDetectionSubParameter1),
                SubParameter2 = GetDetectionSubParameterValue(_checkBoxDetectionSubParameter2Enabled, _listBoxDetectionSubParameter2),
                SubParameter3 = GetDetectionSubParameterValue(_checkBoxDetectionSubParameter3Enabled, _listBoxDetectionSubParameter3),
                InnerSettingsProfileIndex = GetSelectedInnerSettingsProfileIndex()
            };

            SaveDetectionParameterReferenceList();
            MessageBox.Show(this, "Reference saved.", "Detection Parameter Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MoveDetectionSubParameter(ListBox sourceListBox, int direction)
        {
            if (sourceListBox == null)
            {
                return;
            }

            var selectedIndex = sourceListBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }

            var targetIndex = selectedIndex + direction;
            if (targetIndex < 0 || targetIndex >= _detectionSubParameter1Items.Count)
            {
                return;
            }

            var movedItem = _detectionSubParameter1Items[selectedIndex];
            _detectionSubParameter1Items.RemoveAt(selectedIndex);
            _detectionSubParameter1Items.Insert(targetIndex, movedItem);
            RefreshDetectionSubParameter1List();
            SelectDetectionSubParameterIndex(targetIndex);
        }

        private void SaveDetectionSubParameterOrder()
        {
            SaveSettingListSortItems(_detectionSubParameter1Items);
            MessageBox.Show(this, "Sub-parameter order saved.", "Detection Parameter Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GetSelectedInnerSettingsProfileIndex()
        {
            if (_comboBoxImageViewerCameraProfile == null)
            {
                return 0;
            }

            return _comboBoxImageViewerCameraProfile.SelectedIndex < 0 ? 0 : _comboBoxImageViewerCameraProfile.SelectedIndex;
        }

        private static string GetDetectionSubParameterValue(CheckBox checkBox, ListBox listBox)
        {
            if (checkBox == null || listBox == null || !checkBox.Checked)
            {
                return string.Empty;
            }

            return listBox.SelectedItem as string ?? string.Empty;
        }

        private void RefreshDetectionSubParameterListBox(ListBox listBox)
        {
            if (listBox == null)
            {
                return;
            }

            var selectedItem = listBox.SelectedItem as string;

            listBox.BeginUpdate();
            try
            {
                listBox.Items.Clear();
                foreach (var item in _detectionSubParameter1Items)
                {
                    listBox.Items.Add(item);
                }
            }
            finally
            {
                listBox.EndUpdate();
            }

            if (!string.IsNullOrWhiteSpace(selectedItem))
            {
                for (var i = 0; i < listBox.Items.Count; i++)
                {
                    if (string.Equals(listBox.Items[i] as string, selectedItem, StringComparison.OrdinalIgnoreCase))
                    {
                        listBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private static void ApplyDetectionSubParameterSelection(CheckBox checkBox, ListBox listBox, string value)
        {
            if (checkBox != null)
            {
                checkBox.Checked = !string.IsNullOrWhiteSpace(value);
            }

            if (listBox == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                listBox.ClearSelected();
                return;
            }

            for (var i = 0; i < listBox.Items.Count; i++)
            {
                if (string.Equals(listBox.Items[i] as string, value, StringComparison.OrdinalIgnoreCase))
                {
                    listBox.SelectedIndex = i;
                    return;
                }
            }

            listBox.ClearSelected();
        }

        private void SelectDetectionSubParameterIndex(int selectedIndex)
        {
            SelectDetectionSubParameterIndex(_listBoxDetectionSubParameter1, selectedIndex);
            SelectDetectionSubParameterIndex(_listBoxDetectionSubParameter2, selectedIndex);
            SelectDetectionSubParameterIndex(_listBoxDetectionSubParameter3, selectedIndex);
        }

        private static void SelectDetectionSubParameterIndex(ListBox listBox, int selectedIndex)
        {
            if (listBox == null)
            {
                return;
            }

            if (selectedIndex >= 0 && selectedIndex < listBox.Items.Count)
            {
                listBox.SelectedIndex = selectedIndex;
            }
        }

        private List<string> LoadSettingListSortItems()
        {
            var items = new List<string>();
            if (!File.Exists(_settingsPath))
            {
                return items;
            }

            var inListSort = false;
            foreach (var rawLine in File.ReadAllLines(_settingsPath))
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    var section = line.Substring(1, line.Length - 2).Trim();
                    inListSort = string.Equals(section, "listSort", StringComparison.OrdinalIgnoreCase);
                    continue;
                }

                if (!inListSort)
                {
                    continue;
                }

                var equalsIndex = line.IndexOf('=');
                if (equalsIndex <= 0)
                {
                    continue;
                }

                var value = line.Substring(equalsIndex + 1).Trim();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    items.Add(value);
                }
            }

            return items;
        }

        private List<string> LoadSettingSectionNames()
        {
            var items = new List<string>();
            if (!File.Exists(_settingsPath))
            {
                return items;
            }

            foreach (var rawLine in File.ReadAllLines(_settingsPath))
            {
                var line = rawLine.Trim();
                if (!line.StartsWith("[") || !line.EndsWith("]"))
                {
                    continue;
                }

                var section = line.Substring(1, line.Length - 2).Trim();
                if (string.IsNullOrWhiteSpace(section) ||
                    string.Equals(section, "listSort", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var exists = false;
                foreach (var item in items)
                {
                    if (string.Equals(item, section, StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    items.Add(section);
                }
            }

            return items;
        }

        private void SaveSettingListSortItems(List<string> items)
        {
            var lines = File.Exists(_settingsPath)
                ? File.ReadAllLines(_settingsPath).ToList()
                : new List<string>();

            var startIndex = -1;
            var endIndex = lines.Count;
            for (var i = 0; i < lines.Count; i++)
            {
                var trimmed = lines[i].Trim();
                if (!trimmed.StartsWith("[") || !trimmed.EndsWith("]"))
                {
                    continue;
                }

                var section = trimmed.Substring(1, trimmed.Length - 2).Trim();
                if (startIndex < 0)
                {
                    if (string.Equals(section, "listSort", StringComparison.OrdinalIgnoreCase))
                    {
                        startIndex = i;
                    }
                }
                else
                {
                    endIndex = i;
                    break;
                }
            }

            var replacement = new List<string> { "[listSort]" };
            for (var i = 0; i < items.Count; i++)
            {
                replacement.Add("Item" + (i + 1) + "=" + items[i]);
            }

            if (startIndex >= 0)
            {
                lines.RemoveRange(startIndex, endIndex - startIndex);
                lines.InsertRange(startIndex, replacement);
            }
            else
            {
                if (lines.Count > 0 && !string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
                {
                    lines.Add(string.Empty);
                }

                lines.AddRange(replacement);
            }

            File.WriteAllLines(_settingsPath, lines);
        }
    }
}
