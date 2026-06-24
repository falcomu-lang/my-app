using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal sealed class MeasureRecordEditForm : Form
    {
        private readonly NumericUpDown _startX;
        private readonly NumericUpDown _startY;
        private readonly NumericUpDown _endX;
        private readonly NumericUpDown _endY;

        public MeasureRecordEditForm(MeasureRecord record)
        {
            Text = "編輯量測線段";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(360, 210);

            var labelStart = new Label { Left = 16, Top = 18, Width = 120, Text = "起點 (X, Y)" };
            var labelEnd = new Label { Left = 16, Top = 64, Width = 120, Text = "終點 (X, Y)" };

            _startX = CreateNumeric(140, 14, record.StartPoint.X);
            _startY = CreateNumeric(250, 14, record.StartPoint.Y);
            _endX = CreateNumeric(140, 60, record.EndPoint.X);
            _endY = CreateNumeric(250, 60, record.EndPoint.Y);

            var okButton = new Button
            {
                Text = "確定",
                Left = 174,
                Top = 156,
                Width = 78,
                DialogResult = DialogResult.OK
            };

            var cancelButton = new Button
            {
                Text = "取消",
                Left = 258,
                Top = 156,
                Width = 78,
                DialogResult = DialogResult.Cancel
            };

            okButton.Click += OkButton_Click;

            Controls.Add(labelStart);
            Controls.Add(labelEnd);
            Controls.Add(_startX);
            Controls.Add(_startY);
            Controls.Add(_endX);
            Controls.Add(_endY);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        public MeasureRecord BuildUpdatedRecord(MeasureRecord original)
        {
            var startPoint = new Point((int)_startX.Value, (int)_startY.Value);
            var endPoint = new Point((int)_endX.Value, (int)_endY.Value);
            var centerX = (startPoint.X + endPoint.X) / 2.0;
            var centerY = (startPoint.Y + endPoint.Y) / 2.0;
            var distance = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            var basis = CreateReferenceBasis(original.ReferenceTopLeft, original.ReferenceTopRight);

            return new MeasureRecord
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                CenterPoint = new Point((int)Math.Round(centerX), (int)Math.Round(centerY)),
                Distance = distance,
                SourceName = original.SourceName,
                DirectionName = original.DirectionName,
                ReferenceTopLeft = original.ReferenceTopLeft,
                ReferenceTopRight = original.ReferenceTopRight,
                ReferenceLength = original.ReferenceLength,
                LocalStartPoint = ToLocalReferencePoint(startPoint, basis, original.ReferenceLength),
                LocalEndPoint = ToLocalReferencePoint(endPoint, basis, original.ReferenceLength)
            };
        }

        private static NumericUpDown CreateNumeric(int left, int top, int value)
        {
            return new NumericUpDown
            {
                Left = left,
                Top = top,
                Width = 90,
                Minimum = -100000,
                Maximum = 100000,
                Value = value
            };
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_startX.Value == _endX.Value && _startY.Value == _endY.Value)
            {
                MessageBox.Show(this, "起點和終點不能相同。", "編輯量測線段", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }

        private static ReferenceBasis CreateReferenceBasis(Point topLeft, Point topRight)
        {
            var dx = topRight.X - topLeft.X;
            var dy = topRight.Y - topLeft.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length < 0.0001)
            {
                length = 1d;
            }

            return new ReferenceBasis
            {
                Anchor = topLeft,
                UnitX = new PointF((float)(dx / length), (float)(dy / length)),
                UnitY = new PointF((float)(-dy / length), (float)(dx / length)),
                Length = length
            };
        }

        private static PointF ToLocalReferencePoint(Point point, ReferenceBasis basis, double sourceReferenceLength)
        {
            var length = sourceReferenceLength > 0 ? sourceReferenceLength : basis.Length;
            var vx = point.X - basis.Anchor.X;
            var vy = point.Y - basis.Anchor.Y;
            return new PointF(
                (float)((vx * basis.UnitX.X + vy * basis.UnitX.Y) / length),
                (float)((vx * basis.UnitY.X + vy * basis.UnitY.Y) / length));
        }
    }
}
