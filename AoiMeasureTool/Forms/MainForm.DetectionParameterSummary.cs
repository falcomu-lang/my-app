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
            _detectionMainParameters.AddRange(
                _parameterReferenceListRepository.Load(GetParameterReferenceListPath()));
            RefreshDetectionMainParameterList();
        }

        private void SaveDetectionParameterReferenceList()
        {
            _parameterReferenceListRepository.Save(
                GetParameterReferenceListPath(),
                _detectionMainParameters);
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
            MessageBox.Show(this, "主參數順序已保存。", "檢測參數整理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (_listBoxDetectionSubParameter1 == null)
            {
                return;
            }

            _listBoxDetectionSubParameter1.BeginUpdate();
            try
            {
                _listBoxDetectionSubParameter1.Items.Clear();
                foreach (var item in _detectionSubParameter1Items)
                {
                    _listBoxDetectionSubParameter1.Items.Add(item);
                }
            }
            finally
            {
                _listBoxDetectionSubParameter1.EndUpdate();
            }
        }

        private void DetectionSubParameter1MoveUpButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter1(-1);
        }

        private void DetectionSubParameter1MoveDownButton_Click(object sender, EventArgs e)
        {
            MoveDetectionSubParameter1(1);
        }

        private void DetectionSubParameter1SaveOrderButton_Click(object sender, EventArgs e)
        {
            SaveSettingListSortItems(_detectionSubParameter1Items);
            MessageBox.Show(this, "子參數1順序已保存。", "檢測參數整理", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MoveDetectionSubParameter1(int direction)
        {
            if (_listBoxDetectionSubParameter1 == null)
            {
                return;
            }

            var selectedIndex = _listBoxDetectionSubParameter1.SelectedIndex;
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
            _listBoxDetectionSubParameter1.SelectedIndex = targetIndex;
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
