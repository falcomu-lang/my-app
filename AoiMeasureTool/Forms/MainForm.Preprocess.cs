using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializePreprocessControls()
        {
            _preprocessPictureBoxes = new[]
            {
                pictureBoxPreprocess1,
                pictureBoxPreprocess2,
                pictureBoxPreprocess3,
                pictureBoxPreprocess4
            };

            _preprocessEnabledChecks = new[]
            {
                checkBoxPreprocess1Enabled,
                checkBoxPreprocess2Enabled,
                checkBoxPreprocess3Enabled,
                checkBoxPreprocess4Enabled
            };

            _thresholdTrackBars = new[] { trackBarThreshold1, trackBarThreshold2, trackBarThreshold3, trackBarThreshold4 };
            _thresholdInputs = new[] { numericThreshold1, numericThreshold2, numericThreshold3, numericThreshold4 };
            _erodeInputs = new[] { numericErode1, numericErode2, numericErode3, numericErode4 };
            _dilateInputs = new[] { numericDilate1, numericDilate2, numericDilate3, numericDilate4 };
            _openInputs = new[] { numericOpen1, numericOpen2, numericOpen3, numericOpen4 };
            _closeInputs = new[] { numericClose1, numericClose2, numericClose3, numericClose4 };

            for (var i = 0; i < 4; i++)
            {
                SetPreprocessControlsEnabled(i, _preprocessEnabledChecks[i].Checked);
            }
        }

        private void InitializeDualThresholdControls()
        {
            if (tabPageBinarization2 == null)
            {
                return;
            }

            if (pictureBoxDualThresholdOriginal != null &&
                pictureBoxDualThresholdPreview != null &&
                panelDualThresholdOriginalViewport != null &&
                panelDualThresholdPreviewViewport != null &&
                buttonDualThresholdLoadSettings != null &&
                buttonDualThresholdSaveSettings != null)
            {
                _pictureBoxDualThresholdOriginal = pictureBoxDualThresholdOriginal;
                _pictureBoxDualThresholdPreview = pictureBoxDualThresholdPreview;
                _panelDualThresholdOriginalViewport = panelDualThresholdOriginalViewport;
                _panelDualThresholdPreviewViewport = panelDualThresholdPreviewViewport;
                _buttonDualThresholdLoadOriginal = buttonDualThresholdLoadSettings;
                _buttonDualThresholdLoadBinary = buttonDualThresholdSaveSettings;
                _numericDualThresholdLower = numericDualThresholdLower;
                _numericDualThresholdUpper = numericDualThresholdUpper;
                _trackBarDualThresholdLower = trackBarDualThresholdLower;
                _trackBarDualThresholdUpper = trackBarDualThresholdUpper;
                _checkBoxDualThresholdEnabled = checkBoxDualThresholdEnabled;
                _numericDualThresholdErode = numericDualThresholdErode;
                _numericDualThresholdDilate = numericDualThresholdDilate;
                _numericDualThresholdOpen = numericDualThresholdOpen;
                _numericDualThresholdClose = numericDualThresholdClose;

                _panelDualThresholdOriginalViewport.MouseEnter += DualThresholdOriginalViewport_MouseEnter;
                _panelDualThresholdOriginalViewport.MouseWheel += DualThresholdOriginalViewport_MouseWheel;
                _pictureBoxDualThresholdOriginal.MouseEnter += DualThresholdOriginalViewport_MouseEnter;
                _pictureBoxDualThresholdOriginal.MouseWheel += DualThresholdOriginalViewport_MouseWheel;
                _pictureBoxDualThresholdOriginal.MouseDown += DualThresholdOriginal_MouseDown;
                _pictureBoxDualThresholdOriginal.MouseMove += DualThresholdOriginal_MouseMove;
                _pictureBoxDualThresholdOriginal.MouseUp += DualThresholdOriginal_MouseUp;

                _panelDualThresholdPreviewViewport.MouseEnter += DualThresholdPreviewViewport_MouseEnter;
                _panelDualThresholdPreviewViewport.MouseWheel += DualThresholdPreviewViewport_MouseWheel;
                _pictureBoxDualThresholdPreview.MouseEnter += DualThresholdPreviewViewport_MouseEnter;
                _pictureBoxDualThresholdPreview.MouseWheel += DualThresholdPreviewViewport_MouseWheel;
                _pictureBoxDualThresholdPreview.MouseDown += DualThresholdPreview_MouseDown;
                _pictureBoxDualThresholdPreview.MouseMove += DualThresholdPreview_MouseMove;
                _pictureBoxDualThresholdPreview.MouseUp += DualThresholdPreview_MouseUp;

                _buttonDualThresholdLoadOriginal.Click += ButtonLoadSavedSettings_Click;
                _buttonDualThresholdLoadBinary.Click += ButtonSaveCurrentSettings_Click;
                _checkBoxDualThresholdEnabled.CheckedChanged += DualThresholdControl_ValueChanged;
                _trackBarDualThresholdLower.Scroll += DualThresholdTrackBar_Scroll;
                _trackBarDualThresholdUpper.Scroll += DualThresholdTrackBar_Scroll;
                _numericDualThresholdLower.ValueChanged += DualThresholdNumeric_ValueChanged;
                _numericDualThresholdUpper.ValueChanged += DualThresholdNumeric_ValueChanged;
                _numericDualThresholdErode.ValueChanged += DualThresholdControl_ValueChanged;
                _numericDualThresholdDilate.ValueChanged += DualThresholdControl_ValueChanged;
                _numericDualThresholdOpen.ValueChanged += DualThresholdControl_ValueChanged;
                _numericDualThresholdClose.ValueChanged += DualThresholdControl_ValueChanged;

                UpdateDualThresholdOriginalImage();
                UpdateDualThresholdPreview();
                return;
            }

            tabPageBinarization2.SuspendLayout();
            tabPageBinarization2.Controls.Clear();
            tabPageBinarization2.BackColor = tabPageBinarization.BackColor;
            tabPageBinarization2.Text = "二值化處理-2";

            var panelOriginal = CreateDualThresholdImagePanel(
                "原始影像",
                false,
                out _pictureBoxDualThresholdOriginal,
                out _panelDualThresholdOriginalViewport);
            panelOriginal.Location = new Point(20, 20);
            tabPageBinarization2.Controls.Add(panelOriginal);

            var panelPreview = CreateDualThresholdImagePanel(
                "雙門檻結果｜滾輪縮放、左鍵拖曳、右鍵看原圖",
                true,
                out _pictureBoxDualThresholdPreview,
                out _panelDualThresholdPreviewViewport);
            panelPreview.Location = new Point(366, 20);
            tabPageBinarization2.Controls.Add(panelPreview);

            var panelControls = CreateDualThresholdControlPanel();
            panelControls.Location = new Point(712, 20);
            tabPageBinarization2.Controls.Add(panelControls);

            var panelActions = CreateDualThresholdActionPanel();
            panelActions.Location = new Point(20, 574);
            tabPageBinarization2.Controls.Add(panelActions);

            tabPageBinarization2.ResumeLayout(false);
            UpdateDualThresholdOriginalImage();
            UpdateDualThresholdPreview();
        }

        private Panel CreateDualThresholdImagePanel(string title, bool isPreview, out PictureBox pictureBox, out Panel viewport)
        {
            var panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(300, 280)
            };

            var label = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold),
                Location = new Point(10, 8),
                Text = title
            };

            viewport = new Panel
            {
                BackColor = Color.FromArgb(232, 234, 236),
                Location = new Point(10, 34),
                Size = new Size(278, 234),
                TabStop = true
            };

            pictureBox = new PictureBox
            {
                BackColor = Color.FromArgb(232, 234, 236),
                Name = (isPreview ? "DualThresholdPreview" : "DualThresholdOriginal") + "PictureBox",
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = Point.Empty,
                Size = viewport.ClientSize
            };

            viewport.Controls.Add(pictureBox);

            if (isPreview)
            {
                viewport.MouseEnter += DualThresholdPreviewViewport_MouseEnter;
                viewport.MouseWheel += DualThresholdPreviewViewport_MouseWheel;
                pictureBox.MouseEnter += DualThresholdPreviewViewport_MouseEnter;
                pictureBox.MouseWheel += DualThresholdPreviewViewport_MouseWheel;
                pictureBox.MouseDown += DualThresholdPreview_MouseDown;
                pictureBox.MouseMove += DualThresholdPreview_MouseMove;
                pictureBox.MouseUp += DualThresholdPreview_MouseUp;
            }
            else
            {
                viewport.MouseEnter += DualThresholdOriginalViewport_MouseEnter;
                viewport.MouseWheel += DualThresholdOriginalViewport_MouseWheel;
                pictureBox.MouseEnter += DualThresholdOriginalViewport_MouseEnter;
                pictureBox.MouseWheel += DualThresholdOriginalViewport_MouseWheel;
                pictureBox.MouseDown += DualThresholdOriginal_MouseDown;
                pictureBox.MouseMove += DualThresholdOriginal_MouseMove;
                pictureBox.MouseUp += DualThresholdOriginal_MouseUp;
            }

            panel.Controls.Add(label);
            panel.Controls.Add(viewport);
            return panel;
        }

        private Panel CreateDualThresholdControlPanel()
        {
            var panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(300, 280)
            };

            var labelTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold),
                Location = new Point(12, 10),
                Text = "雙門檻設定"
            };

            _checkBoxDualThresholdEnabled = new CheckBox
            {
                AutoSize = true,
                Checked = true,
                Location = new Point(16, 40),
                Text = "啟用"
            };
            _checkBoxDualThresholdEnabled.CheckedChanged += DualThresholdControl_ValueChanged;

            var labelLower = new Label { AutoSize = true, Location = new Point(16, 74), Text = "下門檻" };
            _trackBarDualThresholdLower = new TrackBar
            {
                AutoSize = false,
                Location = new Point(86, 68),
                Size = new Size(140, 30),
                Minimum = 0,
                Maximum = 255,
                TickFrequency = 5,
                Value = 80
            };
            _trackBarDualThresholdLower.Scroll += DualThresholdTrackBar_Scroll;

            _numericDualThresholdLower = new NumericUpDown
            {
                Location = new Point(232, 70),
                Size = new Size(52, 25),
                Minimum = 0,
                Maximum = 255,
                Value = 80
            };
            _numericDualThresholdLower.ValueChanged += DualThresholdNumeric_ValueChanged;

            var labelUpper = new Label { AutoSize = true, Location = new Point(16, 114), Text = "上門檻" };
            _trackBarDualThresholdUpper = new TrackBar
            {
                AutoSize = false,
                Location = new Point(86, 108),
                Size = new Size(140, 30),
                Minimum = 0,
                Maximum = 255,
                TickFrequency = 5,
                Value = 180
            };
            _trackBarDualThresholdUpper.Scroll += DualThresholdTrackBar_Scroll;

            _numericDualThresholdUpper = new NumericUpDown
            {
                Location = new Point(232, 110),
                Size = new Size(52, 25),
                Minimum = 0,
                Maximum = 255,
                Value = 180
            };
            _numericDualThresholdUpper.ValueChanged += DualThresholdNumeric_ValueChanged;

            var labelErode = new Label { AutoSize = true, Location = new Point(16, 160), Text = "侵蝕" };
            _numericDualThresholdErode = CreateMorphologyNumeric(new Point(86, 156));
            var labelDilate = new Label { AutoSize = true, Location = new Point(156, 160), Text = "膨脹" };
            _numericDualThresholdDilate = CreateMorphologyNumeric(new Point(226, 156));
            var labelOpen = new Label { AutoSize = true, Location = new Point(16, 200), Text = "開運算" };
            _numericDualThresholdOpen = CreateMorphologyNumeric(new Point(86, 196));
            var labelClose = new Label { AutoSize = true, Location = new Point(156, 200), Text = "閉運算" };
            _numericDualThresholdClose = CreateMorphologyNumeric(new Point(226, 196));

            panel.Controls.Add(labelTitle);
            panel.Controls.Add(_checkBoxDualThresholdEnabled);
            panel.Controls.Add(labelLower);
            panel.Controls.Add(_trackBarDualThresholdLower);
            panel.Controls.Add(_numericDualThresholdLower);
            panel.Controls.Add(labelUpper);
            panel.Controls.Add(_trackBarDualThresholdUpper);
            panel.Controls.Add(_numericDualThresholdUpper);
            panel.Controls.Add(labelErode);
            panel.Controls.Add(_numericDualThresholdErode);
            panel.Controls.Add(labelDilate);
            panel.Controls.Add(_numericDualThresholdDilate);
            panel.Controls.Add(labelOpen);
            panel.Controls.Add(_numericDualThresholdOpen);
            panel.Controls.Add(labelClose);
            panel.Controls.Add(_numericDualThresholdClose);

            return panel;
        }

        private Panel CreateDualThresholdActionPanel()
        {
            var panel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(370, 58)
            };

            _buttonDualThresholdLoadOriginal = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatAppearance = { BorderSize = 0 },
                FlatStyle = FlatStyle.Flat,
                Location = new Point(12, 9),
                Size = new Size(168, 40),
                Text = "讀取設定",
                UseVisualStyleBackColor = false
            };
            _buttonDualThresholdLoadOriginal.Click += ButtonLoadSavedSettings_Click;

            _buttonDualThresholdLoadBinary = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatAppearance = { BorderSize = 0 },
                FlatStyle = FlatStyle.Flat,
                Location = new Point(190, 9),
                Size = new Size(168, 40),
                Text = "儲存目前設定",
                UseVisualStyleBackColor = false
            };
            _buttonDualThresholdLoadBinary.Click += ButtonSaveCurrentSettings_Click;

            panel.Controls.Add(_buttonDualThresholdLoadOriginal);
            panel.Controls.Add(_buttonDualThresholdLoadBinary);
            return panel;
        }

        private NumericUpDown CreateMorphologyNumeric(Point location)
        {
            var numeric = new NumericUpDown
            {
                Location = location,
                Size = new Size(52, 25),
                Minimum = 0,
                Maximum = 20
            };
            numeric.ValueChanged += DualThresholdControl_ValueChanged;
            return numeric;
        }

        private PreprocessParam CreatePreprocessParam(int index)
        {
            return PreprocessProfileApplier.CreateParam(
                index,
                _preprocessEnabledChecks,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);
        }

        private bool TryGetReferenceCornerPreprocessParam(out PreprocessParam preprocessParam)
        {
            preprocessParam = null;
            if (_preprocessEnabledChecks == null)
            {
                return false;
            }

            if (_referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessEnabledChecks.Length)
            {
                return false;
            }

            var selectedParam = CreatePreprocessParam(_referenceSourceIndex);
            if (selectedParam == null || !selectedParam.Enabled)
            {
                return false;
            }

            preprocessParam = selectedParam;
            return true;
        }

        private void UpdateAllPreprocessImages()
        {
            if (_preprocessPictureBoxes == null)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                UpdatePreprocessImage(i);
            }

            UpdateDualThresholdOriginalImage();
            UpdateDualThresholdPreview(false);
            UpdateReferenceCornerPreview();
        }

        private void UpdatePreprocessImage(int index, bool preserveActiveView = false)
        {
            DisposePreprocessImage(index);

            if (_grayImage == null || _grayImage.Empty() || !_preprocessEnabledChecks[index].Checked)
            {
                SetPictureBoxImage(_preprocessPictureBoxes[index], null);
                if (index == _selectedPreprocessIndex)
                {
                    UpdateActivePreprocessPreview(false);
                }
                return;
            }

            _preprocessImages[index] = PreprocessPipelineService.Build(_grayImage, CreatePreprocessParam(index));
            SetPictureBoxImage(_preprocessPictureBoxes[index], BitmapConverter.ToBitmap(_preprocessImages[index]));
            if (index == _selectedPreprocessIndex)
            {
                UpdateActivePreprocessPreview(preserveActiveView);
            }
        }

        private void PreprocessThumbnail_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control == null || control.Tag == null)
            {
                return;
            }

            _selectedPreprocessIndex = Convert.ToInt32(control.Tag);
            tabControlPreprocess.SelectedIndex = _selectedPreprocessIndex;
            UpdateActivePreprocessPreview(false);
        }

        private void UpdateActivePreprocessPreview(bool preserveView = false)
        {
            if (_preprocessPictureBoxes == null || _showingOriginalInActivePreview)
            {
                return;
            }

            var sourceImage = _preprocessPictureBoxes[_selectedPreprocessIndex].Image;
            if (sourceImage == null)
            {
                SetActivePreviewImage(null);
            }
            else if (preserveView && pictureBoxActivePreprocess.Image != null)
            {
                var savedScale = _activeImageScale;
                var savedLeft = pictureBoxActivePreprocess.Left;
                var savedTop = pictureBoxActivePreprocess.Top;
                SetPictureBoxImage(pictureBoxActivePreprocess, new Bitmap(sourceImage));
                _activeImageScale = savedScale;
                pictureBoxActivePreprocess.Size = new Size(
                    Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale)),
                    Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale)));
                pictureBoxActivePreprocess.Left = savedLeft;
                pictureBoxActivePreprocess.Top = savedTop;
                ConstrainActiveImagePosition();
            }
            else
            {
                SetActivePreviewImage(new Bitmap(sourceImage));
            }

            labelActivePreprocess.Text = string.Format("前處理 {0}", _selectedPreprocessIndex + 1);
        }

        private void ActivePreprocess_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && pictureBoxBinaryOriginal.Image != null)
            {
                _savedActiveImageScale = _activeImageScale;
                _savedActiveImageLeft = pictureBoxActivePreprocess.Left;
                _savedActiveImageTop = pictureBoxActivePreprocess.Top;
                _showingOriginalInActivePreview = true;
                SetActivePreviewImageNoFit(new Bitmap(pictureBoxBinaryOriginal.Image));
                return;
            }

            if (e.Button != MouseButtons.Left || pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            _isDraggingActiveImage = true;
            _lastActiveMousePosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Cursor = Cursors.SizeAll;
            pictureBoxActivePreprocess.Capture = true;
        }

        private void ActivePreprocess_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingActiveImage)
            {
                return;
            }

            var currentPosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Left += currentPosition.X - _lastActiveMousePosition.X;
            pictureBoxActivePreprocess.Top += currentPosition.Y - _lastActiveMousePosition.Y;
            _lastActiveMousePosition = currentPosition;
            ConstrainActiveImagePosition();
        }

        private void ActivePreprocess_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _showingOriginalInActivePreview = false;
                RestoreActivePreviewView();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                _isDraggingActiveImage = false;
                pictureBoxActivePreprocess.Cursor = Cursors.Default;
                pictureBoxActivePreprocess.Capture = false;
            }
        }

        private void ActivePreprocess_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelActiveViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            PreprocessPreviewService.ZoomAt(
                pictureBoxActivePreprocess,
                panelActiveViewport,
                mousePosition,
                e.Delta,
                ref _activeImageScale,
                _activeFitScale);
        }

        private void ActivePreview_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxActivePreprocess.Focus();
        }

        private void SetActivePreviewImage(Bitmap image)
        {
            SetPictureBoxImage(pictureBoxActivePreprocess, image);
            FitActiveImageToViewport();
        }

        private void SetActivePreviewImageNoFit(Bitmap image)
        {
            var oldImage = pictureBoxActivePreprocess.Image;
            pictureBoxActivePreprocess.Image = image;
            oldImage?.Dispose();
        }

        private void RestoreActivePreviewView()
        {
            if (_preprocessPictureBoxes == null)
            {
                return;
            }

            var sourceImage = _preprocessPictureBoxes[_selectedPreprocessIndex].Image;
            if (sourceImage == null)
            {
                SetActivePreviewImage(null);
                return;
            }

            SetPictureBoxImage(pictureBoxActivePreprocess, new Bitmap(sourceImage));
            _activeImageScale = _savedActiveImageScale;
            pictureBoxActivePreprocess.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale)),
                Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale)));
            pictureBoxActivePreprocess.Left = _savedActiveImageLeft;
            pictureBoxActivePreprocess.Top = _savedActiveImageTop;
            ConstrainActiveImagePosition();
        }

        private void FitActiveImageToViewport()
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                pictureBoxActivePreprocess.Location = Point.Empty;
                pictureBoxActivePreprocess.Size = panelActiveViewport.ClientSize;
                _activeImageScale = 1f;
                _activeFitScale = 1f;
                return;
            }

            PreprocessPreviewService.FitToViewport(
                pictureBoxActivePreprocess,
                panelActiveViewport,
                ref _activeImageScale,
                ref _activeFitScale);
        }

        private void ConstrainActiveImagePosition()
        {
            PreprocessPreviewService.ConstrainPosition(pictureBoxActivePreprocess, panelActiveViewport);
        }

        private void DisposePreprocessImage(int index)
        {
            _preprocessImages[index]?.Dispose();
            _preprocessImages[index] = null;
        }

        private void PreprocessEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_preprocessEnabledChecks == null)
            {
                return;
            }

            var index = Array.IndexOf(_preprocessEnabledChecks, sender as CheckBox);
            if (index < 0)
            {
                return;
            }

            SetPreprocessControlsEnabled(index, _preprocessEnabledChecks[index].Checked);
            UpdatePreprocessImage(index, false);
        }

        private void SetPreprocessControlsEnabled(int index, bool enabled)
        {
            _thresholdTrackBars[index].Enabled = enabled;
            _thresholdInputs[index].Enabled = enabled;
            _erodeInputs[index].Enabled = enabled;
            _dilateInputs[index].Enabled = enabled;
            _openInputs[index].Enabled = enabled;
            _closeInputs[index].Enabled = enabled;
        }

        private void ThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdTrackBars == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdTrackBars, sender as TrackBar);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdInputs[index].Value = _thresholdTrackBars[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index, true);
        }

        private void ThresholdValue_ValueChanged(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdInputs == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdInputs, sender as NumericUpDown);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdTrackBars[index].Value = (int)_thresholdInputs[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index, true);
        }

        private void MorphologyValue_ValueChanged(object sender, EventArgs e)
        {
            if (_erodeInputs == null)
            {
                return;
            }

            var input = sender as NumericUpDown;
            var index = Array.IndexOf(_erodeInputs, input);
            if (index < 0) index = Array.IndexOf(_dilateInputs, input);
            if (index < 0) index = Array.IndexOf(_openInputs, input);
            if (index < 0) index = Array.IndexOf(_closeInputs, input);
            if (index >= 0)
            {
                UpdatePreprocessImage(index, true);
            }
        }

        private void UpdateDualThresholdOriginalImage()
        {
            if (_pictureBoxDualThresholdOriginal == null)
            {
                return;
            }

            if (pictureBoxImage.Image == null)
            {
                SetPictureBoxImage(_pictureBoxDualThresholdOriginal, null);
                return;
            }

            SetPictureBoxImage(_pictureBoxDualThresholdOriginal, new Bitmap(pictureBoxImage.Image));
            PreprocessPreviewService.FitToViewport(
                _pictureBoxDualThresholdOriginal,
                _panelDualThresholdOriginalViewport,
                ref _dualThresholdOriginalImageScale,
                ref _dualThresholdOriginalFitScale);
        }

        private void UpdateDualThresholdPreview(bool preserveView = true)
        {
            if (_pictureBoxDualThresholdPreview == null)
            {
                return;
            }

            if (_grayImage == null || _grayImage.Empty() || _checkBoxDualThresholdEnabled == null || !_checkBoxDualThresholdEnabled.Checked)
            {
                SetPictureBoxImage(_pictureBoxDualThresholdPreview, null);
                return;
            }

            if (preserveView && _pictureBoxDualThresholdPreview.Image != null)
            {
                _dualThresholdSavedPreviewImageScale = _dualThresholdPreviewImageScale;
                _dualThresholdSavedPreviewLeft = _pictureBoxDualThresholdPreview.Left;
                _dualThresholdSavedPreviewTop = _pictureBoxDualThresholdPreview.Top;
            }

            using (var binary = BuildDualThresholdBinary())
            {
                if (binary == null || binary.Empty())
                {
                    SetPictureBoxImage(_pictureBoxDualThresholdPreview, null);
                    return;
                }

                SetPictureBoxImage(_pictureBoxDualThresholdPreview, BitmapConverter.ToBitmap(binary));
            }

            if (preserveView && _pictureBoxDualThresholdPreview.Image != null && _dualThresholdSavedPreviewImageScale > 0f)
            {
                _dualThresholdPreviewImageScale = _dualThresholdSavedPreviewImageScale;
                _pictureBoxDualThresholdPreview.Size = new Size(
                    Math.Max(1, (int)Math.Round(_pictureBoxDualThresholdPreview.Image.Width * _dualThresholdPreviewImageScale)),
                    Math.Max(1, (int)Math.Round(_pictureBoxDualThresholdPreview.Image.Height * _dualThresholdPreviewImageScale)));
                _pictureBoxDualThresholdPreview.Left = _dualThresholdSavedPreviewLeft;
                _pictureBoxDualThresholdPreview.Top = _dualThresholdSavedPreviewTop;
                PreprocessPreviewService.ConstrainPosition(_pictureBoxDualThresholdPreview, _panelDualThresholdPreviewViewport);
                return;
            }

            PreprocessPreviewService.FitToViewport(
                _pictureBoxDualThresholdPreview,
                _panelDualThresholdPreviewViewport,
                ref _dualThresholdPreviewImageScale,
                ref _dualThresholdPreviewFitScale);
        }

        private CvMat BuildDualThresholdBinary()
        {
            if (_grayImage == null || _grayImage.Empty() || _checkBoxDualThresholdEnabled == null || !_checkBoxDualThresholdEnabled.Checked)
            {
                return null;
            }

            return PreprocessPipelineService.Build(_grayImage, CreateDualThresholdParam());
        }

        private PreprocessParam CreateDualThresholdParam()
        {
            return new PreprocessParam
            {
                Enabled = _checkBoxDualThresholdEnabled.Checked,
                WhiteObject = true,
                Threshold = (int)_numericDualThresholdLower.Value,
                UpperThreshold = (int)_numericDualThresholdUpper.Value,
                UseDualThreshold = true,
                ErodeIterations = (int)_numericDualThresholdErode.Value,
                DilateIterations = (int)_numericDualThresholdDilate.Value,
                OpenIterations = (int)_numericDualThresholdOpen.Value,
                CloseIterations = (int)_numericDualThresholdClose.Value
            };
        }

        private void DualThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_synchronizingDualThreshold)
            {
                return;
            }

            _synchronizingDualThreshold = true;
            _numericDualThresholdLower.Value = _trackBarDualThresholdLower.Value;
            _numericDualThresholdUpper.Value = _trackBarDualThresholdUpper.Value;
            _synchronizingDualThreshold = false;
            UpdateDualThresholdPreview();
        }

        private void DualThresholdNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (_synchronizingDualThreshold)
            {
                return;
            }

            _synchronizingDualThreshold = true;
            _trackBarDualThresholdLower.Value = (int)_numericDualThresholdLower.Value;
            _trackBarDualThresholdUpper.Value = (int)_numericDualThresholdUpper.Value;
            _synchronizingDualThreshold = false;
            UpdateDualThresholdPreview();
        }

        private void DualThresholdControl_ValueChanged(object sender, EventArgs e)
        {
            UpdateDualThresholdPreview();
        }

        private void DualThresholdLoadOriginal_Click(object sender, EventArgs e)
        {
            if (_pictureBoxDualThresholdPreview == null || pictureBoxImage.Image == null)
            {
                return;
            }

            _dualThresholdSavedPreviewImageScale = _dualThresholdPreviewImageScale;
            _dualThresholdSavedPreviewLeft = _pictureBoxDualThresholdPreview.Left;
            _dualThresholdSavedPreviewTop = _pictureBoxDualThresholdPreview.Top;
            SetPictureBoxImage(_pictureBoxDualThresholdPreview, new Bitmap(pictureBoxImage.Image));
            _dualThresholdPreviewImageScale = _dualThresholdSavedPreviewImageScale;
            _pictureBoxDualThresholdPreview.Size = new Size(
                Math.Max(1, (int)Math.Round(_pictureBoxDualThresholdPreview.Image.Width * _dualThresholdPreviewImageScale)),
                Math.Max(1, (int)Math.Round(_pictureBoxDualThresholdPreview.Image.Height * _dualThresholdPreviewImageScale)));
            _pictureBoxDualThresholdPreview.Left = _dualThresholdSavedPreviewLeft;
            _pictureBoxDualThresholdPreview.Top = _dualThresholdSavedPreviewTop;
            PreprocessPreviewService.ConstrainPosition(_pictureBoxDualThresholdPreview, _panelDualThresholdPreviewViewport);
        }

        private void DualThresholdLoadBinary_Click(object sender, EventArgs e)
        {
            UpdateDualThresholdPreview();
        }

        private void DualThresholdOriginalViewport_MouseEnter(object sender, EventArgs e)
        {
            _pictureBoxDualThresholdOriginal.Focus();
        }

        private void DualThresholdOriginalViewport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_pictureBoxDualThresholdOriginal.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = _panelDualThresholdOriginalViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            PreprocessPreviewService.ZoomAt(
                _pictureBoxDualThresholdOriginal,
                _panelDualThresholdOriginalViewport,
                mousePosition,
                e.Delta,
                ref _dualThresholdOriginalImageScale,
                _dualThresholdOriginalFitScale);
        }

        private void DualThresholdOriginal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _pictureBoxDualThresholdOriginal.Image == null)
            {
                return;
            }

            _dualThresholdOriginalDragging = true;
            _dualThresholdOriginalLastMousePosition = _panelDualThresholdOriginalViewport.PointToClient(_pictureBoxDualThresholdOriginal.PointToScreen(e.Location));
            _pictureBoxDualThresholdOriginal.Cursor = Cursors.SizeAll;
            _pictureBoxDualThresholdOriginal.Capture = true;
        }

        private void DualThresholdOriginal_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dualThresholdOriginalDragging)
            {
                return;
            }

            var currentPosition = _panelDualThresholdOriginalViewport.PointToClient(_pictureBoxDualThresholdOriginal.PointToScreen(e.Location));
            _pictureBoxDualThresholdOriginal.Left += currentPosition.X - _dualThresholdOriginalLastMousePosition.X;
            _pictureBoxDualThresholdOriginal.Top += currentPosition.Y - _dualThresholdOriginalLastMousePosition.Y;
            _dualThresholdOriginalLastMousePosition = currentPosition;
            PreprocessPreviewService.ConstrainPosition(_pictureBoxDualThresholdOriginal, _panelDualThresholdOriginalViewport);
        }

        private void DualThresholdOriginal_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _dualThresholdOriginalDragging = false;
            _pictureBoxDualThresholdOriginal.Cursor = Cursors.Default;
            _pictureBoxDualThresholdOriginal.Capture = false;
        }

        private void DualThresholdPreviewViewport_MouseEnter(object sender, EventArgs e)
        {
            _pictureBoxDualThresholdPreview.Focus();
        }

        private void DualThresholdPreviewViewport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_pictureBoxDualThresholdPreview.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = _panelDualThresholdPreviewViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            PreprocessPreviewService.ZoomAt(
                _pictureBoxDualThresholdPreview,
                _panelDualThresholdPreviewViewport,
                mousePosition,
                e.Delta,
                ref _dualThresholdPreviewImageScale,
                _dualThresholdPreviewFitScale);
        }

        private void DualThresholdPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DualThresholdLoadOriginal_Click(sender, EventArgs.Empty);
                return;
            }

            if (e.Button != MouseButtons.Left || _pictureBoxDualThresholdPreview.Image == null)
            {
                return;
            }

            _dualThresholdPreviewDragging = true;
            _dualThresholdPreviewLastMousePosition = _panelDualThresholdPreviewViewport.PointToClient(_pictureBoxDualThresholdPreview.PointToScreen(e.Location));
            _pictureBoxDualThresholdPreview.Cursor = Cursors.SizeAll;
            _pictureBoxDualThresholdPreview.Capture = true;
        }

        private void DualThresholdPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dualThresholdPreviewDragging)
            {
                return;
            }

            var currentPosition = _panelDualThresholdPreviewViewport.PointToClient(_pictureBoxDualThresholdPreview.PointToScreen(e.Location));
            _pictureBoxDualThresholdPreview.Left += currentPosition.X - _dualThresholdPreviewLastMousePosition.X;
            _pictureBoxDualThresholdPreview.Top += currentPosition.Y - _dualThresholdPreviewLastMousePosition.Y;
            _dualThresholdPreviewLastMousePosition = currentPosition;
            PreprocessPreviewService.ConstrainPosition(_pictureBoxDualThresholdPreview, _panelDualThresholdPreviewViewport);
        }

        private void DualThresholdPreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DualThresholdLoadBinary_Click(sender, EventArgs.Empty);
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _dualThresholdPreviewDragging = false;
            _pictureBoxDualThresholdPreview.Cursor = Cursors.Default;
            _pictureBoxDualThresholdPreview.Capture = false;
        }
    }
}
