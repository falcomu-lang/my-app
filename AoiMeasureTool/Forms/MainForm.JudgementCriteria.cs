using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private Button _buttonJudgementCriteria;

        private void InitializeJudgementCriteriaControls()
        {
            EnsureJudgementCriteriaSidebarButton();
            EnsureJudgementCriteriaTab();
        }

        private void EnsureJudgementCriteriaSidebarButton()
        {
            if (panelSidebar == null || _buttonJudgementCriteria != null)
            {
                return;
            }

            _buttonJudgementCriteria = new Button
            {
                Name = "buttonJudgementCriteria",
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft JhengHei UI", 12F),
                Location = new Point(16, 396),
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(208, 48),
                TabIndex = 7,
                Text = "+   良品判斷條件",
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            _buttonJudgementCriteria.FlatAppearance.BorderSize = 0;
            _buttonJudgementCriteria.Click += JudgementCriteriaButton_Click;

            panelSidebar.Controls.Add(_buttonJudgementCriteria);
            _buttonJudgementCriteria.BringToFront();
            labelOpenCvStatus?.BringToFront();
        }

        private void EnsureJudgementCriteriaTab()
        {
            if (_tabPageJudgementCriteria != null)
            {
                return;
            }

            _tabPageJudgementCriteria = new TabPage
            {
                Name = "tabPageJudgementCriteria",
                Text = "良品判斷條件",
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(3),
                Size = new Size(1032, 656)
            };

            tabControlMain.Controls.Add(_tabPageJudgementCriteria);
        }
    }
}
