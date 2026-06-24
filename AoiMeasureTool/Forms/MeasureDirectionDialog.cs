using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal sealed class MeasureDirectionDialog : Form
    {
        private readonly RadioButton _parallelRadio;
        private readonly RadioButton _perpendicularRadio;

        public MeasureDirectionDialog(MeasureDirectionMode initialMode)
        {
            Text = "選擇量測方向";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(320, 170);

            var label = new Label
            {
                Left = 16,
                Top = 16,
                Width = 280,
                Height = 40,
                Text = "請先選擇平行或垂直，接著重新點選兩個點。"
            };

            _parallelRadio = new RadioButton
            {
                Left = 24,
                Top = 64,
                Width = 120,
                Text = "平行"
            };

            _perpendicularRadio = new RadioButton
            {
                Left = 160,
                Top = 64,
                Width = 120,
                Text = "垂直"
            };

            if (initialMode == MeasureDirectionMode.Perpendicular)
            {
                _perpendicularRadio.Checked = true;
            }
            else
            {
                _parallelRadio.Checked = true;
            }

            var okButton = new Button
            {
                Text = "開始編輯",
                Left = 140,
                Top = 108,
                Width = 80,
                DialogResult = DialogResult.OK
            };

            var cancelButton = new Button
            {
                Text = "取消",
                Left = 226,
                Top = 108,
                Width = 80,
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(label);
            Controls.Add(_parallelRadio);
            Controls.Add(_perpendicularRadio);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        public MeasureDirectionMode SelectedMode
        {
            get { return _perpendicularRadio.Checked ? MeasureDirectionMode.Perpendicular : MeasureDirectionMode.Parallel; }
        }
    }
}
