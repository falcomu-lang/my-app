using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using Cv2 = OpenCvSharp.Cv2;
using ColorConversionCodes = OpenCvSharp.ColorConversionCodes;
using CvMat = OpenCvSharp.Mat;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void ShowReferenceCornerWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageReferenceCorner);

            tabControlMain.SelectedTab = tabPageReferenceCorner;
            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }
            UpdateReferenceCornerPreview();
        }

        private ReferenceCornerSnapshot CaptureCurrentReferenceCornerSnapshot()
        {
            var snapshot = ReferenceCornerSelectionService.CaptureSnapshot(
                _referenceCornerEnabled,
                _referenceSourceIndex,
                _referencePointMode,
                _referenceRoiRectangle,
                _referenceRoiSaved,
                _referenceCornerFound);
            snapshot.ProtrusionMinWidth = _numericReferenceProtrusionMinWidth == null ? 20 : (int)_numericReferenceProtrusionMinWidth.Value;
            snapshot.ProtrusionMinHeight = _numericReferenceProtrusionMinHeight == null ? 5 : (int)_numericReferenceProtrusionMinHeight.Value;
            snapshot.ProtrusionWidthIncreaseThreshold = _numericReferenceProtrusionWidthIncreaseThreshold == null ? 8 : (int)_numericReferenceProtrusionWidthIncreaseThreshold.Value;
            snapshot.ProtrusionConsecutiveRows = _numericReferenceProtrusionConsecutiveRows == null ? 3 : (int)_numericReferenceProtrusionConsecutiveRows.Value;
            return snapshot;
        }

        private void ApplyReferenceCornerSnapshot(ReferenceCornerSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return;
            }

            _isApplyingReferenceCornerState = true;
            try
            {
                _referenceRoiRectangle = Rectangle.Empty;
                _referenceRoiSaved = false;
                _referenceCornerFound = false;
                _referenceCornerCandidate = null;
                _referenceCornerEnabled = snapshot.Enabled;
                _referenceSourceIndex = Math.Max(0, Math.Min(GetReferenceSourceCount() - 1, snapshot.SourceIndex));
                _referencePointMode = snapshot.PointMode;
                if (_comboBoxReferencePointMode == null || _comboBoxReferencePointMode.Items.Count <= 0 || (int)_referencePointMode < 0 || (int)_referencePointMode > _comboBoxReferencePointMode.Items.Count - 1)
                {
                    _referencePointMode = ReferenceCornerPointMode.ContourNearest;
                }
                _referenceRoiRectangle = ReferenceCornerSelectionService.NormalizeRectangle(snapshot.Roi);
                _referenceRoiSaved = snapshot.RoiSaved && _referenceRoiRectangle.Width > 0 && _referenceRoiRectangle.Height > 0;
                _referenceCornerFound = snapshot.CornerFound && _referenceRoiSaved;
                ApplyReferenceCornerParameterSnapshot(snapshot);
                if (!_referenceRoiSaved)
                {
                    _referenceRoiRectangle = Rectangle.Empty;
                }
                ApplyReferenceCornerUiState();
            }
            finally
            {
                _isApplyingReferenceCornerState = false;
            }
        }

        private static ReferenceCornerSnapshot CloneReferenceCornerSnapshot(ReferenceCornerSnapshot snapshot)
        {
            return ProfileDataCloner.CloneReferenceCornerSnapshot(snapshot);
        }

        private void InitializeReferenceCornerControls()
        {
            EnsureReferenceCornerPointModeControls();
            comboBoxReferenceSource.Items.Clear();
            comboBoxReferenceSource.Items.Add("Preprocess 1");
            comboBoxReferenceSource.Items.Add("Preprocess 2");
            comboBoxReferenceSource.Items.Add("Preprocess 3");
            comboBoxReferenceSource.Items.Add("Preprocess 4");
            comboBoxReferenceSource.Items.Add("Dual Threshold");
            comboBoxReferenceSource.SelectedIndex = 0;
            _comboBoxReferencePointMode.Items.Clear();
            _comboBoxReferencePointMode.Items.Add("Contour nearest");
            _comboBoxReferencePointMode.Items.Add("ROI top edge");
            _comboBoxReferencePointMode.Items.Add("Incomplete protrusion");
            _comboBoxReferencePointMode.SelectedIndex = 0;
            _referenceSourceIndex = 0;
            _referencePointMode = ReferenceCornerPointMode.ContourNearest;
            _referenceCornerEnabled = false;
            EnsureReferenceCornerProtrusionParameterControls();
            ApplyReferenceCornerUiState();
        }

        private void EnsureReferenceCornerProtrusionParameterControls()
        {
            if (panelReferenceCornerControls == null)
            {
                return;
            }

            panelReferenceCornerControls.Height = Math.Max(panelReferenceCornerControls.Height, 320);
            panelReferenceCornerControls.AutoScroll = true;

            if (_labelReferenceProtrusionMinWidth == null)
            {
                _labelReferenceProtrusionMinWidth = new Label { AutoSize = true, Location = new Point(16, 166), Text = "Min protrusion width" };
                panelReferenceCornerControls.Controls.Add(_labelReferenceProtrusionMinWidth);
            }

            if (_numericReferenceProtrusionMinWidth == null)
            {
                _numericReferenceProtrusionMinWidth = CreateReferenceParameterNumeric(16, 186, 1, 999, 20, ReferenceCornerParameterChanged);
                panelReferenceCornerControls.Controls.Add(_numericReferenceProtrusionMinWidth);
            }

            if (_labelReferenceProtrusionMinHeight == null)
            {
                _labelReferenceProtrusionMinHeight = new Label { AutoSize = true, Location = new Point(150, 166), Text = "Min protrusion height" };
                panelReferenceCornerControls.Controls.Add(_labelReferenceProtrusionMinHeight);
            }

            if (_numericReferenceProtrusionMinHeight == null)
            {
                _numericReferenceProtrusionMinHeight = CreateReferenceParameterNumeric(150, 186, 1, 999, 5, ReferenceCornerParameterChanged);
                panelReferenceCornerControls.Controls.Add(_numericReferenceProtrusionMinHeight);
            }

            if (_labelReferenceProtrusionWidthIncreaseThreshold == null)
            {
                _labelReferenceProtrusionWidthIncreaseThreshold = new Label { AutoSize = true, Location = new Point(16, 214), Text = "Width increase threshold" };
                panelReferenceCornerControls.Controls.Add(_labelReferenceProtrusionWidthIncreaseThreshold);
            }

            if (_numericReferenceProtrusionWidthIncreaseThreshold == null)
            {
                _numericReferenceProtrusionWidthIncreaseThreshold = CreateReferenceParameterNumeric(16, 234, 0, 999, 8, ReferenceCornerParameterChanged);
                panelReferenceCornerControls.Controls.Add(_numericReferenceProtrusionWidthIncreaseThreshold);
            }

            if (_labelReferenceProtrusionConsecutiveRows == null)
            {
                _labelReferenceProtrusionConsecutiveRows = new Label { AutoSize = true, Location = new Point(150, 214), Text = "Consecutive rows" };
                panelReferenceCornerControls.Controls.Add(_labelReferenceProtrusionConsecutiveRows);
            }

            if (_numericReferenceProtrusionConsecutiveRows == null)
            {
                _numericReferenceProtrusionConsecutiveRows = CreateReferenceParameterNumeric(150, 234, 1, 20, 3, ReferenceCornerParameterChanged);
                panelReferenceCornerControls.Controls.Add(_numericReferenceProtrusionConsecutiveRows);
            }

            buttonSaveReferenceRoi.Location = new Point(16, 270);
        }

        private static NumericUpDown CreateReferenceParameterNumeric(int left, int top, decimal minimum, decimal maximum, decimal value, EventHandler handler)
        {
            var control = new NumericUpDown
            {
                Location = new Point(left, top),
                Size = new Size(108, 24),
                Minimum = minimum,
                Maximum = maximum,
                Value = Math.Max(minimum, Math.Min(maximum, value))
            };
            control.ValueChanged += handler;
            return control;
        }

        private void ApplyReferenceCornerParameterSnapshot(ReferenceCornerSnapshot snapshot)
        {
            if (snapshot == null)
            {
                snapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
            }

            if (_numericReferenceProtrusionMinWidth != null)
            {
                _numericReferenceProtrusionMinWidth.Value = ClampNumericUpDown(_numericReferenceProtrusionMinWidth, snapshot.ProtrusionMinWidth);
            }

            if (_numericReferenceProtrusionMinHeight != null)
            {
                _numericReferenceProtrusionMinHeight.Value = ClampNumericUpDown(_numericReferenceProtrusionMinHeight, snapshot.ProtrusionMinHeight);
            }

            if (_numericReferenceProtrusionWidthIncreaseThreshold != null)
            {
                _numericReferenceProtrusionWidthIncreaseThreshold.Value = ClampNumericUpDown(_numericReferenceProtrusionWidthIncreaseThreshold, snapshot.ProtrusionWidthIncreaseThreshold);
            }

            if (_numericReferenceProtrusionConsecutiveRows != null)
            {
                _numericReferenceProtrusionConsecutiveRows.Value = ClampNumericUpDown(_numericReferenceProtrusionConsecutiveRows, snapshot.ProtrusionConsecutiveRows);
            }
        }

        private void ReferenceCornerParameterChanged(object sender, EventArgs e)
        {
            if (_isApplyingReferenceCornerState)
            {
                return;
            }

            PersistReferenceCornerState();
            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }
            UpdateReferenceCornerPreview();
        }

        private void EnsureReferenceCornerPointModeControls()
        {
            if (panelReferenceCornerControls == null)
            {
                return;
            }

            if (_labelReferencePointMode == null)
            {
                _labelReferencePointMode = new Label
                {
                    AutoSize = true,
                    Location = new Point(16, 112),
                    Text = "Point mode"
                };
                panelReferenceCornerControls.Controls.Add(_labelReferencePointMode);
            }

            if (_comboBoxReferencePointMode == null)
            {
                _comboBoxReferencePointMode = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(16, 134),
                    Size = new Size(280, 25)
                };
                _comboBoxReferencePointMode.SelectedIndexChanged += ReferencePointMode_SelectedIndexChanged;
                panelReferenceCornerControls.Controls.Add(_comboBoxReferencePointMode);
                _comboBoxReferencePointMode.BringToFront();
            }

            buttonSaveReferenceRoi.Location = new Point(16, 174);
        }

        private void ApplyReferenceCornerUiState()
        {
            if (labelReferenceCornerStatus != null)
            {
                labelReferenceCornerStatus.Visible = false;
            }

            if (checkBoxReferenceCornerEnabled != null)
            {
                checkBoxReferenceCornerEnabled.Checked = _referenceCornerEnabled;
            }

            if (comboBoxReferenceSource != null && comboBoxReferenceSource.Items.Count > 0)
            {
                comboBoxReferenceSource.SelectedIndex = Math.Max(0, Math.Min(comboBoxReferenceSource.Items.Count - 1, _referenceSourceIndex));
                comboBoxReferenceSource.Enabled = _referenceCornerEnabled;
            }

            if (_comboBoxReferencePointMode != null && _comboBoxReferencePointMode.Items.Count > 0)
            {
                _comboBoxReferencePointMode.SelectedIndex = (int)_referencePointMode;
                _comboBoxReferencePointMode.Enabled = _referenceCornerEnabled;
            }

            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }
            UpdateReferenceCornerPreview();
            UpdateReferenceRoiSaveButtonState();
        }

        private void ReferenceCornerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_isApplyingReferenceCornerState)
            {
                return;
            }

            _referenceCornerEnabled = checkBoxReferenceCornerEnabled.Checked;
            PersistReferenceCornerState();
            ApplyReferenceCornerUiState();
        }

        private void ReferenceSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isApplyingReferenceCornerState)
            {
                return;
            }

            if (comboBoxReferenceSource.SelectedIndex < 0)
            {
                return;
            }

            _referenceSourceIndex = comboBoxReferenceSource.SelectedIndex;
            _referenceRoiRectangle = Rectangle.Empty;
            _referenceRoiSaved = false;
            _referenceCornerFound = false;
            _referenceCornerCandidate = null;
            PersistReferenceCornerState();
            UpdateReferenceCornerPreview();
            UpdateReferenceRoiSaveButtonState();
        }

        private void ReferencePointMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isApplyingReferenceCornerState || _comboBoxReferencePointMode == null || _comboBoxReferencePointMode.SelectedIndex < 0)
            {
                return;
            }

            _referencePointMode = (ReferenceCornerPointMode)_comboBoxReferencePointMode.SelectedIndex;
            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }

            PersistReferenceCornerState();
            UpdateReferenceCornerPreview();
        }

        private void UpdateReferenceCornerPreview()
        {
            if (pictureBoxReferencePreview == null)
            {
                return;
            }

            var image = GetSelectedReferencePreviewBitmap();
            SetPictureBoxImage(pictureBoxReferencePreview, image);
            FitReferenceImageToViewport();
            pictureBoxReferencePreview.Invalidate();
        }

        private void PersistReferenceCornerState()
        {
            if (string.IsNullOrWhiteSpace(_activeProductKey))
            {
                return;
            }

            _referenceCornerProfiles[_activeProductKey] = CloneReferenceCornerSnapshot(CaptureCurrentReferenceCornerSnapshot());

            if (_isApplyingProductState)
            {
                return;
            }

            SaveCurrentAppSettings();
        }

        private void UpdateReferenceCornerFoundState(bool found, bool previousFound)
        {
            _referenceCornerFound = found;
            if (found != previousFound)
            {
                PersistReferenceCornerState();
            }
        }

        private static void DrawReferencePoint(Graphics graphics, Brush brush, Pen outlinePen, int x, int y)
        {
            const int radius = 6;
            graphics.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
            graphics.DrawEllipse(outlinePen, x - radius, y - radius, radius * 2, radius * 2);
        }

        private static void DrawReferencePoint(Graphics graphics, Brush brush, Pen outlinePen, Point point)
        {
            DrawReferencePoint(graphics, brush, outlinePen, point.X, point.Y);
        }

        private Bitmap GetSelectedReferencePreviewBitmap()
        {
            if (IsDualThresholdReferenceSource())
            {
                if (_pictureBoxDualThresholdPreview == null || _pictureBoxDualThresholdPreview.Image == null)
                {
                    return null;
                }

                var dualPreview = new Bitmap(_pictureBoxDualThresholdPreview.Image);
                var annotatedDualPreview = TryAnnotateReferenceCornerPreview(dualPreview);
                dualPreview.Dispose();
                return annotatedDualPreview;
            }

            if (_preprocessPictureBoxes == null || _referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessPictureBoxes.Length)
            {
                return null;
            }

            var source = _preprocessPictureBoxes[_referenceSourceIndex].Image;
            if (source == null)
            {
                return null;
            }

            var preview = new Bitmap(source);
            var annotatedPreview = TryAnnotateReferenceCornerPreview(preview);
            preview.Dispose();
            return annotatedPreview;
        }

        private Bitmap TryAnnotateReferenceCornerPreview(Bitmap sourceBitmap)
        {
            if (sourceBitmap == null || _referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                return sourceBitmap == null ? null : new Bitmap(sourceBitmap);
            }

            using (var binaryMat = GetReferenceCornerDetectionBinary())
            {
                if (binaryMat == null || binaryMat.Empty())
                {
                    return new Bitmap(sourceBitmap);
                }

                var roiCenter = new Point(
                    _referenceRoiRectangle.Left + _referenceRoiRectangle.Width / 2,
                    _referenceRoiRectangle.Top + _referenceRoiRectangle.Height / 2);

                var bestCandidate = ReferenceCornerDetectionService.FindCandidate(binaryMat, _referenceRoiRectangle, roiCenter, CaptureCurrentReferenceCornerSnapshot());
                _referenceCornerCandidate = bestCandidate;
                UpdateReferenceCornerFoundState(bestCandidate != null, _referenceCornerFound);
            }

            return new Bitmap(sourceBitmap);
        }

        private void RefreshReferenceCornerCandidate()
        {
            _referenceCornerCandidate = null;
            var previousFound = _referenceCornerFound;

            if (!HasReferencePreviewSource())
            {
                UpdateReferenceCornerFoundState(false, previousFound);
                return;
            }

            if (_referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                UpdateReferenceCornerFoundState(false, previousFound);
                return;
            }

            using (var binaryMat = GetReferenceCornerDetectionBinary())
            {
                if (binaryMat == null || binaryMat.Empty())
                {
                    UpdateReferenceCornerFoundState(false, previousFound);
                    return;
                }

                var roiCenter = new Point(
                    _referenceRoiRectangle.Left + _referenceRoiRectangle.Width / 2,
                    _referenceRoiRectangle.Top + _referenceRoiRectangle.Height / 2);

                var bestCandidate = ReferenceCornerDetectionService.FindCandidate(binaryMat, _referenceRoiRectangle, roiCenter, CaptureCurrentReferenceCornerSnapshot());
                _referenceCornerCandidate = bestCandidate;
                UpdateReferenceCornerFoundState(bestCandidate != null, previousFound);
            }
        }

        private CvMat GetReferenceCornerDetectionBinary()
        {
            if (IsDualThresholdReferenceSource())
            {
                var dualBinary = BuildDualThresholdBinary();
                if (dualBinary == null || dualBinary.Empty())
                {
                    dualBinary?.Dispose();
                    return null;
                }

                return dualBinary;
            }

            if (_preprocessImages == null || _referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessImages.Length)
            {
                return null;
            }

            var source = _preprocessImages[_referenceSourceIndex];
            if (source == null || source.Empty())
            {
                return null;
            }

            return source.Clone();
        }

        private bool HasReferencePreviewSource()
        {
            if (IsDualThresholdReferenceSource())
            {
                return _pictureBoxDualThresholdPreview != null && _pictureBoxDualThresholdPreview.Image != null;
            }

            return _preprocessPictureBoxes != null
                && _referenceSourceIndex >= 0
                && _referenceSourceIndex < _preprocessPictureBoxes.Length
                && _preprocessPictureBoxes[_referenceSourceIndex].Image != null;
        }

        private bool IsDualThresholdReferenceSource()
        {
            return _referenceSourceIndex == GetReferenceSourceCount() - 1;
        }

        private int GetReferenceSourceCount()
        {
            return (_preprocessPictureBoxes?.Length ?? 4) + 1;
        }

        private Point GetReferenceDisplayPoint(Point imagePoint)
        {
            if (pictureBoxReferencePreview.Image == null || _referenceImageScale <= 0)
            {
                return imagePoint;
            }

            return ReferenceCornerPreviewService.ToDisplayPoint(imagePoint, _referenceImageScale);
        }

        private void DrawReferenceCornerOverlayOnPreview(Graphics graphics, ReferenceCornerCandidate candidate)
        {
            if (pictureBoxReferencePreview.Image == null || pictureBoxReferencePreview.Width <= 0 || pictureBoxReferencePreview.Height <= 0)
            {
                return;
            }

            var imageWidth = pictureBoxReferencePreview.Image.Width;
            var imageHeight = pictureBoxReferencePreview.Image.Height;
            if (imageWidth <= 0 || imageHeight <= 0)
            {
                return;
            }

            using (var boxPen = new Pen(Color.LimeGreen, 2f))
            using (var pointBrush = new SolidBrush(Color.Yellow))
            using (var pointOutlinePen = new Pen(Color.Black, 2f))
            using (var topEdgePen = new Pen(Color.LimeGreen, 2f))
            {
                var vertices = candidate.RotatedRect.Points();
                var displayVertices = new Point[vertices.Length];
                for (var i = 0; i < vertices.Length; i++)
                {
                    displayVertices[i] = GetReferenceDisplayPoint(new Point((int)Math.Round(vertices[i].X), (int)Math.Round(vertices[i].Y)));
                }

                graphics.DrawPolygon(boxPen, displayVertices);
                graphics.DrawLine(topEdgePen, GetReferenceDisplayPoint(candidate.TopLeft), GetReferenceDisplayPoint(candidate.TopRight));
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, GetReferenceDisplayPoint(candidate.TopLeft));
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, GetReferenceDisplayPoint(candidate.TopRight));
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, GetReferenceDisplayPoint(candidate.CenterPoint));
            }
        }

        private Point GetReferenceImagePoint(Point displayPoint)
        {
            if (pictureBoxReferencePreview.Image == null || _referenceImageScale <= 0)
            {
                return displayPoint;
            }

            return ReferenceCornerPreviewService.ToImagePoint(displayPoint, _referenceImageScale);
        }

        private Rectangle GetReferenceDisplayRectangle(Rectangle imageRect)
        {
            if (pictureBoxReferencePreview.Image == null || _referenceImageScale <= 0)
            {
                return imageRect;
            }

            return ReferenceCornerPreviewService.ToDisplayRectangle(imageRect, _referenceImageScale);
        }

        private void FitReferenceImageToViewport()
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                pictureBoxReferencePreview.Location = Point.Empty;
                pictureBoxReferencePreview.Size = panelReferencePreview.ClientSize;
                _referenceImageScale = 1f;
                _referenceFitScale = 1f;
                return;
            }

            ReferenceCornerPreviewService.FitToViewport(
                pictureBoxReferencePreview,
                panelReferencePreview,
                ref _referenceImageScale,
                ref _referenceFitScale);
        }

        private void ReferencePreview_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxReferencePreview.Focus();
        }

        private void ReferencePreview_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelReferencePreview.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - pictureBoxReferencePreview.Left) / _referenceImageScale;
            var imageY = (mousePosition.Y - pictureBoxReferencePreview.Top) / _referenceImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _referenceFitScale * 0.25f;
            var maximumScale = _referenceFitScale * 20f;
            _referenceImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _referenceImageScale * zoomFactor));
            pictureBoxReferencePreview.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Width * _referenceImageScale)),
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Height * _referenceImageScale)));
            pictureBoxReferencePreview.Left = (int)Math.Round(mousePosition.X - imageX * _referenceImageScale);
            pictureBoxReferencePreview.Top = (int)Math.Round(mousePosition.Y - imageY * _referenceImageScale);
            ConstrainReferenceImagePosition();
            pictureBoxReferencePreview.Invalidate();
        }

        private void ReferencePreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left && _referenceCornerEnabled)
            {
                _isSelectingReferenceRoi = true;
                _referenceRoiStart = GetReferenceImagePoint(e.Location);
                _referenceRoiRectangle = new Rectangle(_referenceRoiStart, Size.Empty);
                _referenceRoiSaved = false;
                _referenceCornerFound = false;
                _referenceCornerCandidate = null;
                PersistReferenceCornerState();
                if (labelReferenceCornerStatus != null)
                {
                    labelReferenceCornerStatus.Visible = false;
                }
                pictureBoxReferencePreview.Capture = true;
                UpdateReferenceRoiSaveButtonState();
                pictureBoxReferencePreview.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _referencePreviewPanning = true;
                _lastReferenceMousePosition = panelReferencePreview.PointToClient(pictureBoxReferencePreview.PointToScreen(e.Location));
                pictureBoxReferencePreview.Cursor = Cursors.SizeAll;
                pictureBoxReferencePreview.Capture = true;
            }
        }

        private void ReferencePreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelectingReferenceRoi)
            {
                var currentPoint = GetReferenceImagePoint(e.Location);
                _referenceRoiRectangle = ReferenceCornerSelectionService.NormalizeRectangle(new Rectangle(
                    _referenceRoiStart,
                    new Size(currentPoint.X - _referenceRoiStart.X, currentPoint.Y - _referenceRoiStart.Y)));
                PersistReferenceCornerState();
                pictureBoxReferencePreview.Invalidate();
                return;
            }
            if (!_referencePreviewPanning)
            {
                return;
            }
            var currentPosition = panelReferencePreview.PointToClient(pictureBoxReferencePreview.PointToScreen(e.Location));
            pictureBoxReferencePreview.Left += currentPosition.X - _lastReferenceMousePosition.X;
            pictureBoxReferencePreview.Top += currentPosition.Y - _lastReferenceMousePosition.Y;
            _lastReferenceMousePosition = currentPosition;
            ConstrainReferenceImagePosition();
            pictureBoxReferencePreview.Invalidate();
        }

        private void ReferencePreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isSelectingReferenceRoi = false;
                pictureBoxReferencePreview.Capture = false;
                if (labelReferenceCornerStatus != null)
                {
                    labelReferenceCornerStatus.Visible = false;
                }
                pictureBoxReferencePreview.Invalidate();
                UpdateReferenceRoiSaveButtonState();
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                _referencePreviewPanning = false;
                pictureBoxReferencePreview.Cursor = Cursors.Default;
                pictureBoxReferencePreview.Capture = false;
            }
        }

        private void ReferencePreview_Paint(object sender, PaintEventArgs e)
        {
            if (_referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                return;
            }
            using (var pen = new Pen(Color.LimeGreen, 2f))
            using (var brush = new SolidBrush(Color.FromArgb(45, Color.LimeGreen)))
            {
                var displayRoi = GetReferenceDisplayRectangle(_referenceRoiRectangle);
                e.Graphics.FillRectangle(brush, displayRoi);
                e.Graphics.DrawRectangle(pen, displayRoi);
            }
            if (_referenceCornerFound && _referenceCornerCandidate != null)
            {
                DrawReferenceCornerOverlayOnPreview(e.Graphics, _referenceCornerCandidate);
            }
        }

        private void SaveReferenceRoi_Click(object sender, EventArgs e)
        {
            if (_referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                MessageBox.Show(this, "Please select and save a ROI first.", "Reference Corner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RefreshReferenceCornerCandidate();
            _referenceRoiSaved = true;
            PersistReferenceCornerState();
            UpdateReferenceRoiSaveButtonState();
            if (labelReferenceCornerStatus != null)
            {
                labelReferenceCornerStatus.Visible = false;
            }
            pictureBoxReferencePreview.Invalidate();
        }

        private void UpdateReferenceRoiSaveButtonState()
        {
            if (buttonSaveReferenceRoi == null)
            {
                return;
            }
            buttonSaveReferenceRoi.Enabled = _referenceCornerEnabled && _referenceRoiRectangle.Width > 0 && _referenceRoiRectangle.Height > 0;
            buttonSaveReferenceRoi.Text = _referenceRoiSaved ? "?靽? ROI 蝭?" : "靽? ROI 蝭?";
        }

        private void ConstrainReferenceImagePosition()
        {
            ReferenceCornerPreviewService.ConstrainPosition(pictureBoxReferencePreview, panelReferencePreview);
        }
    }
}




