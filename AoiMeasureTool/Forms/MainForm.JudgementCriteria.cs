using System;
using System.Collections.Generic;
using System.Drawing;
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
            _buttonJudgementMoveUp = buttonJudgementMoveUp;
            _buttonJudgementMoveDown = buttonJudgementMoveDown;
            _dataGridViewJudgementCriteria = dataGridViewJudgementCriteria;

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
                _dataGridViewJudgementCriteria.SelectionChanged += JudgementCriteriaGrid_SelectionChanged;
            }

            UpdateJudgementControlsForEditMode(false);
            UpdateJudgementMoveButtons();
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
            RefreshMultiImageJudgementResultTable();
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
            RefreshMultiImageJudgementResultTable();
            MessageBox.Show(this, "判斷條件已儲存。", "良品判斷條件", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void JudgementSyntaxHelpButton_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            using (var textBox = new TextBox())
            {
                form.Text = "良品判斷條件語法說明";
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(720, 520);
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowIcon = false;
                form.ShowInTaskbar = false;

                textBox.Dock = DockStyle.Fill;
                textBox.Multiline = true;
                textBox.ReadOnly = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                textBox.BorderStyle = BorderStyle.None;
                textBox.BackColor = Color.White;
                textBox.Font = new Font("Microsoft JhengHei UI", 10F);
                textBox.Text = BuildJudgementSyntaxHelpTextV2();

                form.Controls.Add(textBox);
                form.ShowDialog(this);
            }
        }

        private static string BuildJudgementSyntaxHelpText()
        {
            var lines = new List<string>
            {
                "良品判斷條件語法說明",
                "",
                "1. 基本欄位",
                "   - (1), (2), (3) 代表量測結果編號",
                "   - 可直接放入加減乘除運算式",
                "",
                "2. 聚合函式",
                "   - max((1)(2)(3))",
                "   - max((1),(2),(3))",
                "   - min((1)(2)(3))",
                "   - min((1),(2),(3))",
                "   - 單一子式缺值時會略過該子式",
                "   - 如果所有子式都缺值，才會顯示不可判斷",
                "",
                "3. 巢狀寫法",
                "   - max(((2)-(1))((4)-(3)))",
                "   - min(((2)-(1)),((4)-(3)))",
                "",
                "4. 範例",
                "   - max((1)(2)(3)) + 1.5",
                "   - min(((2)-(1))((4)-(3))) / 2",
                "",
                "5. 判斷式",
                "   - x<5",
                "   - x<=5",
                "   - 5<x<10",
                "   - 10>=x>=5"
            };

            return string.Join(Environment.NewLine, lines);
        }

        private static string BuildJudgementSyntaxHelpTextV2()
        {
            return string.Join(Environment.NewLine, new[]
            {
                "良品判斷條件語法說明",
                "",
                "1. 參數格式",
                "   - (1), (2), (3) 表示各量測段編號",
                "   - 可直接使用單一段，也可組合多段計算",
                "",
                "2. 聚合函數",
                "   - max((1)(2)(3))",
                "   - max((1),(2),(3))",
                "   - min((1)(2)(3))",
                "   - min((1),(2),(3))",
                "   - 缺值會忽略，不會讓整個式子失效",
                "   - 例如 max(((2)-(1)),((4)-(3))) 中，若 2 無法量測，則 (2)-(1) 會被忽略",
                "",
                "3. 差值與巢狀寫法",
                "   - max(((2)-(1))((4)-(3)))",
                "   - min(((2)-(1)),((4)-(3)))",
                "",
                "4. 混合運算",
                "   - max((1)(2)(3)) + 1.5",
                "   - min(((2)-(1))((4)-(3))) / 2",
                "",
                "5. 判斷式",
                "   - x<5",
                "   - x<=5",
                "   - 5<x<10",
                "   - 10>=x>=5"
            });
        }

        private void JudgementMoveUpButton_Click(object sender, EventArgs e)
        {
            MoveSelectedJudgementRule(-1);
        }

        private void JudgementMoveDownButton_Click(object sender, EventArgs e)
        {
            MoveSelectedJudgementRule(1);
        }

        private void MoveSelectedJudgementRule(int direction)
        {
            if (_dataGridViewJudgementCriteria == null || _dataGridViewJudgementCriteria.SelectedRows.Count == 0)
            {
                return;
            }

            var currentIndex = _dataGridViewJudgementCriteria.SelectedRows[0].Index;
            var targetIndex = currentIndex + direction;
            if (currentIndex < 0 || currentIndex >= _judgementCriteriaRules.Count)
            {
                return;
            }

            if (targetIndex < 0 || targetIndex >= _judgementCriteriaRules.Count)
            {
                return;
            }

            var movedRule = _judgementCriteriaRules[currentIndex];
            _judgementCriteriaRules[currentIndex] = _judgementCriteriaRules[targetIndex];
            _judgementCriteriaRules[targetIndex] = movedRule;

            if (_judgementCriteriaEditingIndex == currentIndex)
            {
                _judgementCriteriaEditingIndex = targetIndex;
            }
            else if (_judgementCriteriaEditingIndex == targetIndex)
            {
                _judgementCriteriaEditingIndex = currentIndex;
            }

            RefreshJudgementCriteriaView();
            SelectJudgementCriteriaRow(targetIndex);
            SaveCurrentAppSettings();
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

            RefreshMultiImageJudgementResultTable();
            UpdateJudgementMoveButtons();
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
                UpdateJudgementMoveButtons();
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
            UpdateJudgementMoveButtons();
        }

        private void JudgementCriteriaGrid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateJudgementMoveButtons();
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
            UpdateJudgementMoveButtons();
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
        private void SelectJudgementCriteriaRow(int rowIndex)
        {
            if (_dataGridViewJudgementCriteria == null || rowIndex < 0 || rowIndex >= _dataGridViewJudgementCriteria.Rows.Count)
            {
                UpdateJudgementMoveButtons();
                return;
            }

            _dataGridViewJudgementCriteria.ClearSelection();
            _dataGridViewJudgementCriteria.Rows[rowIndex].Selected = true;
            _dataGridViewJudgementCriteria.CurrentCell = _dataGridViewJudgementCriteria.Rows[rowIndex].Cells[0];
            UpdateJudgementMoveButtons();
        }

        private void UpdateJudgementMoveButtons()
        {
            var hasSelection = _dataGridViewJudgementCriteria != null && _dataGridViewJudgementCriteria.SelectedRows.Count > 0;
            var selectedIndex = hasSelection ? _dataGridViewJudgementCriteria.SelectedRows[0].Index : -1;

            if (_buttonJudgementMoveUp != null)
            {
                _buttonJudgementMoveUp.Enabled = hasSelection && selectedIndex > 0;
            }

            if (_buttonJudgementMoveDown != null)
            {
                _buttonJudgementMoveDown.Enabled = hasSelection && selectedIndex >= 0 && selectedIndex < _judgementCriteriaRules.Count - 1;
            }
        }
    }
}
