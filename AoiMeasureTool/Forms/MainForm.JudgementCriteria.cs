using System;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeJudgementCriteriaControls()
        {
            _tabPageJudgementCriteria = tabPageJudgementCriteria;
            _textJudgementName = textBoxJudgementName;
            _textJudgementCalculation = textBoxJudgementCalculation;
            _textJudgementSpec = textBoxJudgementSpec;
            _textJudgementCalculationB = textBoxJudgementCalculationB;
            _textJudgementSpecB = textBoxJudgementSpecB;
            _buttonJudgementAdd = buttonJudgementAdd;
            _buttonJudgementReset = buttonJudgementReset;
            _buttonJudgementSave = buttonJudgementSave;
            _dataGridViewJudgementCriteria = dataGridViewJudgementCriteria;

            if (_buttonJudgementAdd != null)
            {
                _buttonJudgementAdd.Click += JudgementAddButton_Click;
            }

            if (_buttonJudgementReset != null)
            {
                _buttonJudgementReset.Click += JudgementResetButton_Click;
            }

            if (_buttonJudgementSave != null)
            {
                _buttonJudgementSave.Click += JudgementSaveButton_Click;
            }

            if (_dataGridViewJudgementCriteria != null && _dataGridViewJudgementCriteria.Columns.Count == 0)
            {
                _dataGridViewJudgementCriteria.Columns.Add("colOrder", "順序");
                _dataGridViewJudgementCriteria.Columns.Add("colName", "規則名稱");
                _dataGridViewJudgementCriteria.Columns.Add("colCalc", "A 規計算式");
                _dataGridViewJudgementCriteria.Columns.Add("colSpec", "A 規規格");
                _dataGridViewJudgementCriteria.Columns.Add("colCalcB", "B 規計算式");
                _dataGridViewJudgementCriteria.Columns.Add("colSpecB", "B 規規格");
                _dataGridViewJudgementCriteria.ContextMenuStrip = BuildJudgementCriteriaContextMenu();
                _dataGridViewJudgementCriteria.MouseDown += JudgementCriteriaGrid_MouseDown;
                _dataGridViewJudgementCriteria.CellMouseDown += JudgementCriteriaGrid_CellMouseDown;
            }

            UpdateJudgementControlsForEditMode(false);
        }

        private ContextMenuStrip BuildJudgementCriteriaContextMenu()
        {
            if (_judgementCriteriaMenu != null)
            {
                return _judgementCriteriaMenu;
            }

            _judgementCriteriaMenu = new ContextMenuStrip();
            _judgementCriteriaEditMenuItem = new ToolStripMenuItem("修改");
            _judgementCriteriaDeleteMenuItem = new ToolStripMenuItem("刪除");
            _judgementCriteriaEditMenuItem.Click += JudgementCriteriaEditMenuItem_Click;
            _judgementCriteriaDeleteMenuItem.Click += JudgementCriteriaDeleteMenuItem_Click;
            _judgementCriteriaMenu.Items.Add(_judgementCriteriaEditMenuItem);
            _judgementCriteriaMenu.Items.Add(_judgementCriteriaDeleteMenuItem);
            return _judgementCriteriaMenu;
        }

        private void JudgementAddButton_Click(object sender, EventArgs e)
        {
            var name = _textJudgementName == null ? string.Empty : _textJudgementName.Text.Trim();
            var calc = _textJudgementCalculation == null ? string.Empty : _textJudgementCalculation.Text.Trim();
            var spec = _textJudgementSpec == null ? string.Empty : _textJudgementSpec.Text.Trim();
            var calcB = _textJudgementCalculationB == null ? string.Empty : _textJudgementCalculationB.Text.Trim();
            var specB = _textJudgementSpecB == null ? string.Empty : _textJudgementSpecB.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) &&
                string.IsNullOrWhiteSpace(calc) &&
                string.IsNullOrWhiteSpace(spec) &&
                string.IsNullOrWhiteSpace(calcB) &&
                string.IsNullOrWhiteSpace(specB))
            {
                return;
            }

            var rule = new JudgementCriterionRule
            {
                Name = name,
                CalculationExpression = calc,
                SpecExpression = spec,
                CalculationExpressionB = calcB,
                SpecExpressionB = specB
            };

            if (_judgementCriteriaEditingIndex >= 0 && _judgementCriteriaEditingIndex < _judgementCriteriaRules.Count)
            {
                _judgementCriteriaRules[_judgementCriteriaEditingIndex] = rule;
                _judgementCriteriaEditingIndex = -1;
            }
            else
            {
                _judgementCriteriaRules.Add(rule);
            }

            RefreshJudgementCriteriaView();
            ClearJudgementCriteriaInputs();
            UpdateJudgementControlsForEditMode(false);
        }

        private void JudgementResetButton_Click(object sender, EventArgs e)
        {
            if (_judgementCriteriaEditingIndex >= 0)
            {
                _judgementCriteriaEditingIndex = -1;
                UpdateJudgementControlsForEditMode(false);
            }

            ClearJudgementCriteriaInputs();
        }

        private void JudgementSaveButton_Click(object sender, EventArgs e)
        {
            PersistActiveProductProfile();
            SaveCurrentAppSettings();
            MessageBox.Show(this, "判斷條件已儲存。", "良品判斷條件", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshJudgementCriteriaView()
        {
            if (_dataGridViewJudgementCriteria == null)
            {
                return;
            }

            _dataGridViewJudgementCriteria.Rows.Clear();
            for (var i = 0; i < _judgementCriteriaRules.Count; i++)
            {
                var rule = _judgementCriteriaRules[i] ?? new JudgementCriterionRule();
                _dataGridViewJudgementCriteria.Rows.Add(
                    i + 1,
                    rule.Name ?? string.Empty,
                    rule.CalculationExpression ?? string.Empty,
                    rule.SpecExpression ?? string.Empty,
                    rule.CalculationExpressionB ?? string.Empty,
                    rule.SpecExpressionB ?? string.Empty);
            }
        }

        private void ClearJudgementCriteriaInputs()
        {
            _textJudgementName?.Clear();
            _textJudgementCalculation?.Clear();
            _textJudgementSpec?.Clear();
            _textJudgementCalculationB?.Clear();
            _textJudgementSpecB?.Clear();
            _textJudgementName?.Focus();
        }

        private void JudgementCriteriaGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || _dataGridViewJudgementCriteria == null)
            {
                return;
            }

            var hit = _dataGridViewJudgementCriteria.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                _dataGridViewJudgementCriteria.ClearSelection();
                _dataGridViewJudgementCriteria.Rows[hit.RowIndex].Selected = true;
                _dataGridViewJudgementCriteria.CurrentCell = _dataGridViewJudgementCriteria.Rows[hit.RowIndex].Cells[0];
            }
        }

        private void JudgementCriteriaGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || _dataGridViewJudgementCriteria == null || e.RowIndex < 0)
            {
                return;
            }

            _dataGridViewJudgementCriteria.ClearSelection();
            _dataGridViewJudgementCriteria.Rows[e.RowIndex].Selected = true;
            _dataGridViewJudgementCriteria.CurrentCell = _dataGridViewJudgementCriteria.Rows[e.RowIndex].Cells[0];
        }

        private void JudgementCriteriaEditMenuItem_Click(object sender, EventArgs e)
        {
            if (_dataGridViewJudgementCriteria == null || _dataGridViewJudgementCriteria.SelectedRows.Count == 0)
            {
                return;
            }

            var row = _dataGridViewJudgementCriteria.SelectedRows[0];
            if (row == null || row.Index < 0 || row.Index >= _judgementCriteriaRules.Count)
            {
                return;
            }

            var rule = _judgementCriteriaRules[row.Index];
            if (rule == null)
            {
                return;
            }

            _judgementCriteriaEditingIndex = row.Index;
            _textJudgementName.Text = rule.Name ?? string.Empty;
            _textJudgementCalculation.Text = rule.CalculationExpression ?? string.Empty;
            _textJudgementSpec.Text = rule.SpecExpression ?? string.Empty;
            _textJudgementCalculationB.Text = rule.CalculationExpressionB ?? string.Empty;
            _textJudgementSpecB.Text = rule.SpecExpressionB ?? string.Empty;
            UpdateJudgementControlsForEditMode(true);
            _textJudgementName?.Focus();
        }

        private void JudgementCriteriaDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (_dataGridViewJudgementCriteria == null || _dataGridViewJudgementCriteria.SelectedRows.Count == 0)
            {
                return;
            }

            var row = _dataGridViewJudgementCriteria.SelectedRows[0];
            if (row == null || row.Index < 0 || row.Index >= _judgementCriteriaRules.Count)
            {
                return;
            }

            if (MessageBox.Show(this, "確定要刪除這筆判斷條件嗎？", "刪除判斷條件", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            _judgementCriteriaRules.RemoveAt(row.Index);
            if (_judgementCriteriaEditingIndex == row.Index)
            {
                _judgementCriteriaEditingIndex = -1;
                UpdateJudgementControlsForEditMode(false);
                ClearJudgementCriteriaInputs();
            }
            else if (_judgementCriteriaEditingIndex > row.Index)
            {
                _judgementCriteriaEditingIndex--;
            }

            RefreshJudgementCriteriaView();
        }

        private void UpdateJudgementControlsForEditMode(bool isEditing)
        {
            if (_buttonJudgementAdd != null)
            {
                _buttonJudgementAdd.Text = isEditing ? "更新" : "新增";
            }

            if (_buttonJudgementReset != null)
            {
                _buttonJudgementReset.Text = isEditing ? "取消" : "重新輸入";
            }
        }
    }
}
