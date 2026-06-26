using System.Collections.Generic;
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

            if (_dataGridViewJudgementCriteria != null && _dataGridViewJudgementCriteria.Columns.Count == 0)
            {
                _dataGridViewJudgementCriteria.Columns.Add("colOrder", "順序");
                _dataGridViewJudgementCriteria.Columns.Add("colName", "規則名稱");
                _dataGridViewJudgementCriteria.Columns.Add("colCalc", "A 規計算式");
                _dataGridViewJudgementCriteria.Columns.Add("colSpec", "A 規規格");
                _dataGridViewJudgementCriteria.Columns.Add("colCalcB", "B 規計算式");
                _dataGridViewJudgementCriteria.Columns.Add("colSpecB", "B 規規格");
            }
        }

        private void JudgementAddButton_Click(object sender, System.EventArgs e)
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

        private void JudgementResetButton_Click(object sender, System.EventArgs e)
        {
            ClearJudgementCriteriaInputs();
        }

        private void JudgementSaveButton_Click(object sender, System.EventArgs e)
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
    }
}
