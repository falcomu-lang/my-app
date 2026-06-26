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
            if (_tabPageJudgementCriteria == null)
            {
                return;
            }

            _tabPageJudgementCriteria.Controls.Clear();

            var rootPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(16)
            };
            rootPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            rootPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var inputPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 4,
                RowCount = 5,
                Margin = new Padding(0, 0, 0, 12)
            };
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120f));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120f));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            inputPanel.Controls.Add(CreateLabel("Rule Name"), 0, 0);
            inputPanel.Controls.Add(CreateLabel("A Calculation"), 2, 0);
            inputPanel.Controls.Add(CreateLabel("A Specification"), 0, 1);
            inputPanel.Controls.Add(CreateLabel("B Calculation"), 2, 2);
            inputPanel.Controls.Add(CreateLabel("B Specification"), 0, 3);

            _textJudgementName = new TextBox { Dock = DockStyle.Fill };
            _textJudgementCalculation = new TextBox { Dock = DockStyle.Fill };
            _textJudgementSpec = new TextBox { Dock = DockStyle.Fill };
            _textJudgementCalculationB = new TextBox { Dock = DockStyle.Fill };
            _textJudgementSpecB = new TextBox { Dock = DockStyle.Fill };

            inputPanel.Controls.Add(_textJudgementName, 1, 0);
            inputPanel.Controls.Add(_textJudgementCalculation, 3, 0);
            inputPanel.Controls.Add(_textJudgementSpec, 1, 1);
            inputPanel.SetColumnSpan(_textJudgementSpec, 3);
            inputPanel.Controls.Add(_textJudgementCalculationB, 3, 2);
            inputPanel.Controls.Add(_textJudgementSpecB, 1, 3);
            inputPanel.SetColumnSpan(_textJudgementSpecB, 3);

            var actionPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0, 12, 0, 0)
            };

            _buttonJudgementAdd = new Button { Text = "Add", Width = 100, Height = 36 };
            _buttonJudgementReset = new Button { Text = "Reset", Width = 100, Height = 36 };
            _buttonJudgementAdd.Click += JudgementAddButton_Click;
            _buttonJudgementReset.Click += JudgementResetButton_Click;

            actionPanel.Controls.Add(_buttonJudgementAdd);
            actionPanel.Controls.Add(_buttonJudgementReset);

            inputPanel.Controls.Add(actionPanel, 1, 2);
            inputPanel.SetColumnSpan(actionPanel, 3);

            _dataGridViewJudgementCriteria = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            _dataGridViewJudgementCriteria.Columns.Add("colOrder", "Order");
            _dataGridViewJudgementCriteria.Columns.Add("colName", "Rule Name");
            _dataGridViewJudgementCriteria.Columns.Add("colCalc", "A Calculation");
            _dataGridViewJudgementCriteria.Columns.Add("colSpec", "A Specification");
            _dataGridViewJudgementCriteria.Columns.Add("colCalcB", "B Calculation");
            _dataGridViewJudgementCriteria.Columns.Add("colSpecB", "B Specification");

            _buttonJudgementSave = new Button
            {
                Text = "Save Conditions",
                Width = 160,
                Height = 38,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _buttonJudgementSave.Click += JudgementSaveButton_Click;

            var savePanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 44
            };
            savePanel.Controls.Add(_buttonJudgementSave);
            savePanel.Resize += (sender, args) =>
            {
                if (_buttonJudgementSave != null)
                {
                    _buttonJudgementSave.Left = savePanel.ClientSize.Width - _buttonJudgementSave.Width;
                    _buttonJudgementSave.Top = 3;
                }
            };

            rootPanel.Controls.Add(inputPanel, 0, 0);
            rootPanel.Controls.Add(_dataGridViewJudgementCriteria, 0, 1);
            rootPanel.Controls.Add(savePanel, 0, 2);

            _tabPageJudgementCriteria.Controls.Add(rootPanel);
            RefreshJudgementCriteriaView();
        }

        private static Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void JudgementAddButton_Click(object sender, EventArgs e)
        {
            var name = _textJudgementName == null ? string.Empty : _textJudgementName.Text.Trim();
            var calc = _textJudgementCalculation == null ? string.Empty : _textJudgementCalculation.Text.Trim();
            var spec = _textJudgementSpec == null ? string.Empty : _textJudgementSpec.Text.Trim();
            var calcB = _textJudgementCalculationB == null ? string.Empty : _textJudgementCalculationB.Text.Trim();
            var specB = _textJudgementSpecB == null ? string.Empty : _textJudgementSpecB.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(calc) && string.IsNullOrWhiteSpace(spec))
            {
                return;
            }

            _judgementCriteriaRules.Add(new JudgementCriterionRule
            {
                Name = name,
                CalculationExpression = calc,
                SpecExpression = spec,
                CalculationExpressionB = calcB,
                SpecExpressionB = specB
            });

            RefreshJudgementCriteriaView();
            ClearJudgementCriteriaInputs();
        }

        private void JudgementResetButton_Click(object sender, EventArgs e)
        {
            ClearJudgementCriteriaInputs();
        }

        private void JudgementSaveButton_Click(object sender, EventArgs e)
        {
            PersistActiveProductProfile();
            SaveCurrentAppSettings();
            MessageBox.Show(this, "Judgement conditions saved.", "Judgement Conditions", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (_textJudgementName != null)
            {
                _textJudgementName.Clear();
            }

            if (_textJudgementCalculation != null)
            {
                _textJudgementCalculation.Clear();
            }

            if (_textJudgementSpec != null)
            {
                _textJudgementSpec.Clear();
            }

            if (_textJudgementCalculationB != null)
            {
                _textJudgementCalculationB.Clear();
            }

            if (_textJudgementSpecB != null)
            {
                _textJudgementSpecB.Clear();
            }

            _textJudgementName?.Focus();
        }
    }
}
