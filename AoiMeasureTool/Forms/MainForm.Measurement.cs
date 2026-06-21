using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void ShowMeasureDistanceWorkspace()
        {
            if (tabControlMain == null || _tabPageMeasureDistance == null)
            {
                return;
            }

            if (!tabControlMain.TabPages.Contains(_tabPageMeasureDistance))
            {
                tabControlMain.TabPages.Clear();
                tabControlMain.TabPages.Add(_tabPageMeasureDistance);
            }

            tabControlMain.SelectedTab = _tabPageMeasureDistance;
            RefreshMeasureDistancePreview();
        }

        private void ShowMultiImageConfirmWorkspace()
        {
            if (tabControlMain == null || _tabPageMultiImageConfirm == null)
            {
                return;
            }

            if (!tabControlMain.TabPages.Contains(_tabPageMultiImageConfirm))
            {
                tabControlMain.TabPages.Clear();
                tabControlMain.TabPages.Add(_tabPageMultiImageConfirm);
            }

            tabControlMain.SelectedTab = _tabPageMultiImageConfirm;
        }

        private void InitializeMeasureDistanceControls()
        {
            _tabPageMeasureDistance = new TabPage
            {
                BackColor = Color.FromArgb(248, 249, 250),
                Location = new Point(4, 26),
                Name = "tabPageMeasureDistance",
                Padding = new Padding(3),
                Size = new Size(1032, 656),
                TabIndex = 2,
                Text = "框選量測的距離"
            };

            _tabPageMultiImageConfirm = new TabPage
            {
                BackColor = Color.FromArgb(248, 249, 250),
                Location = new Point(4, 26),
                Name = "tabPageMultiImageConfirm",
                Padding = new Padding(3),
                Size = new Size(1032, 656),
                TabIndex = 3,
                Text = "多影像確認結果"
            };

            _pictureBoxMultiImageConfirm = new PictureBox
            {
                BackColor = Color.FromArgb(239, 241, 243),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 20),
                Size = new Size(496, 306),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            _tabPageMultiImageConfirm.Controls.Add(_pictureBoxMultiImageConfirm);

            var panelMeasureSource = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 20),
                Size = new Size(330, 238)
            };

            var labelMeasureSource = new Label
            {
                AutoSize = true,
                Location = new Point(16, 16),
                Text = "前處理影像來源"
            };

            _comboBoxMeasureSource = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(16, 42),
                Size = new Size(280, 20)
            };
            _comboBoxMeasureSource.Items.AddRange(new object[] { "前處理 1", "前處理 2", "前處理 3", "前處理 4" });
            _comboBoxMeasureSource.SelectedIndex = 0;
            _comboBoxMeasureSource.SelectedIndexChanged += MeasureSource_SelectedIndexChanged;

            _buttonSaveMeasurePoint = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(16, 126),
                Size = new Size(280, 40),
                Text = "保存量測點",
                UseVisualStyleBackColor = false
            };
            _buttonSaveMeasurePoint.Click += SaveMeasurePoint_Click;

            _buttonClearMeasurePoint = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(16, 172),
                Size = new Size(280, 40),
                Text = "清除量測點",
                UseVisualStyleBackColor = false
            };
            _buttonClearMeasurePoint.Click += ClearMeasurePoint_Click;

            _buttonSaveMeasureRecords = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(366, 206),
                Size = new Size(646, 42),
                Text = "保存紀錄",
                UseVisualStyleBackColor = false
            };
            _buttonSaveMeasureRecords.Click += SaveMeasureRecords_Click;

            _buttonParallelMeasure = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(16, 76),
                Size = new Size(136, 36),
                Text = "平行",
                UseVisualStyleBackColor = false
            };
            _buttonParallelMeasure.Click += ParallelMeasure_Click;

            _buttonPerpendicularMeasure = new Button
            {
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(160, 76),
                Size = new Size(136, 36),
                Text = "垂直",
                UseVisualStyleBackColor = false
            };
            _buttonPerpendicularMeasure.Click += PerpendicularMeasure_Click;

            _labelMeasureStatus = new Label
            {
                AutoSize = true,
                Location = new Point(16, 220),
                ForeColor = Color.FromArgb(130, 134, 138),
                Text = "點兩下影像建立兩點量測，保存後寫入表格"
            };

            panelMeasureSource.Controls.Add(labelMeasureSource);
            panelMeasureSource.Controls.Add(_comboBoxMeasureSource);
            panelMeasureSource.Controls.Add(_buttonParallelMeasure);
            panelMeasureSource.Controls.Add(_buttonPerpendicularMeasure);
            panelMeasureSource.Controls.Add(_buttonSaveMeasurePoint);
            panelMeasureSource.Controls.Add(_buttonClearMeasurePoint);
            panelMeasureSource.Controls.Add(_labelMeasureStatus);

            _panelMeasurePreview = new Panel
            {
                BackColor = Color.FromArgb(239, 241, 243),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 264),
                Size = new Size(992, 352)
            };

            _pictureBoxMeasurePreview = new PictureBox
            {
                BackColor = Color.FromArgb(239, 241, 243),
                Location = new Point(0, 0),
                Size = new Size(990, 350),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            _pictureBoxMeasurePreview.MouseDown += PictureBoxMeasurePreview_MouseDown;
            _pictureBoxMeasurePreview.MouseMove += PictureBoxMeasurePreview_MouseMove;
            _pictureBoxMeasurePreview.MouseUp += PictureBoxMeasurePreview_MouseUp;
            _pictureBoxMeasurePreview.MouseWheel += PictureBoxMeasurePreview_MouseWheel;
            _pictureBoxMeasurePreview.MouseEnter += PictureBoxMeasurePreview_MouseEnter;
            _pictureBoxMeasurePreview.Paint += PictureBoxMeasurePreview_Paint;
            _panelMeasurePreview.Controls.Add(_pictureBoxMeasurePreview);

            _dataGridViewMeasureRecords = new DataGridView
            {
                Location = new Point(366, 20),
                Size = new Size(646, 178),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _dataGridViewMeasureRecords.Columns.Add("colIndex", "編號");
            _dataGridViewMeasureRecords.Columns.Add("colX1", "X1");
            _dataGridViewMeasureRecords.Columns.Add("colY1", "Y1");
            _dataGridViewMeasureRecords.Columns.Add("colX2", "X2");
            _dataGridViewMeasureRecords.Columns.Add("colY2", "Y2");
            _dataGridViewMeasureRecords.Columns.Add("colLtX", "LT X");
            _dataGridViewMeasureRecords.Columns.Add("colLtY", "LT Y");
            _dataGridViewMeasureRecords.Columns.Add("colRtX", "RT X");
            _dataGridViewMeasureRecords.Columns.Add("colRtY", "RT Y");
            _dataGridViewMeasureRecords.Columns.Add("colRefLen", "Ref Len");
            _dataGridViewMeasureRecords.Columns.Add("colCx", "Center X");
            _dataGridViewMeasureRecords.Columns.Add("colCy", "Center Y");
            _dataGridViewMeasureRecords.Columns.Add("colLocalX1", "Local X1");
            _dataGridViewMeasureRecords.Columns.Add("colLocalY1", "Local Y1");
            _dataGridViewMeasureRecords.Columns.Add("colLocalX2", "Local X2");
            _dataGridViewMeasureRecords.Columns.Add("colLocalY2", "Local Y2");
            _dataGridViewMeasureRecords.Columns.Add("colDistance", "Distance(px)");
            _dataGridViewMeasureRecords.Columns.Add("colSource", "來源");
            _dataGridViewMeasureRecords.Columns.Add("colMode", "方向");
            _dataGridViewMeasureRecords.MouseDown += MeasureRecords_MouseDown;

            _measureRecordMenu = new ContextMenuStrip();
            _measureDeleteMenuItem = new ToolStripMenuItem("刪除");
            _measureDeleteMenuItem.Click += MeasureDeleteMenuItem_Click;
            _measureRecordMenu.Items.Add(_measureDeleteMenuItem);
            _dataGridViewMeasureRecords.ContextMenuStrip = _measureRecordMenu;
            _dataGridViewMeasureRecords.SelectionChanged += MeasureRecords_SelectionChanged;

            _measureBlinkTimer = new System.Windows.Forms.Timer();
            _measureBlinkTimer.Interval = 180;
            _measureBlinkTimer.Tick += MeasureBlinkTimer_Tick;

            _tabPageMeasureDistance = tabPageMeasureDistance;
            _tabPageMultiImageConfirm = tabPageMultiImageConfirm;
            _panelMultiImageConfirmViewport = panelMultiImageConfirmViewport;
            _pictureBoxMultiImageConfirm = pictureBoxMultiImageConfirm;
            _buttonLoadMultiImageFolder = buttonLoadMultiImageFolder;
            _buttonMultiImagePrev = buttonMultiImagePrev;
            _buttonMultiImageNext = buttonMultiImageNext;
            _buttonLoadMultiImageFolder?.BringToFront();
            _buttonMultiImagePrev?.BringToFront();
            _buttonMultiImageNext?.BringToFront();
            if (_panelMultiImageConfirmViewport != null)
            {
                _panelMultiImageConfirmViewport.MouseDown += PictureBoxMultiImageConfirm_MouseDown;
                _panelMultiImageConfirmViewport.MouseMove += PictureBoxMultiImageConfirm_MouseMove;
                _panelMultiImageConfirmViewport.MouseUp += PictureBoxMultiImageConfirm_MouseUp;
                _panelMultiImageConfirmViewport.MouseWheel += PictureBoxMultiImageConfirm_MouseWheel;
                _panelMultiImageConfirmViewport.MouseEnter += PictureBoxMultiImageConfirm_MouseEnter;
                _panelMultiImageConfirmViewport.Paint += PictureBoxMultiImageConfirm_Paint;
            }
            if (_tabPageMultiImageConfirm != null)
            {
                _tabPageMultiImageConfirm.MouseWheel += PictureBoxMultiImageConfirm_MouseWheel;
                _tabPageMultiImageConfirm.MouseEnter += PictureBoxMultiImageConfirm_MouseEnter;
            }
            if (_pictureBoxMultiImageConfirm != null)
            {
                _pictureBoxMultiImageConfirm.Visible = false;
            }
            UpdateMultiImageNavigationButtons();

            _tabPageMeasureDistance.Controls.Add(panelMeasureSource);
            _tabPageMeasureDistance.Controls.Add(_dataGridViewMeasureRecords);
            _tabPageMeasureDistance.Controls.Add(_buttonSaveMeasureRecords);
            _tabPageMeasureDistance.Controls.Add(_panelMeasurePreview);
            UpdateMeasureSourceAvailability();
            UpdateMeasureDirectionButtons();
        }

        private void RefreshMeasureDistancePreview()
        {
            if (_pictureBoxMeasurePreview == null)
            {
                return;
            }

            var sourceBitmap = GetMeasureSourceBitmap();
            sourceBitmap = AnnotateMeasurePreviewBitmap(sourceBitmap);
            SetPictureBoxImage(_pictureBoxMeasurePreview, sourceBitmap == null ? null : new Bitmap(sourceBitmap));
            FitMeasureImageToViewport();
            _pictureBoxMeasurePreview.Invalidate();
            UpdateMeasureSourceAvailability();
        }

        private void LoadMultiImageFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "請選擇包含多影像確認結果圖片的資料夾";

                if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    return;
                }

                var imagePath = FindMultiImageConfirmImage(dialog.SelectedPath);
                if (string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show(this, "找不到符合 1.* 的圖片檔。", "多影像確認結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                LoadMultiImageConfirmFolder(dialog.SelectedPath, imagePath);
            }
        }

        private void LoadMultiImageConfirmFolder(string folderPath, string selectedImagePath)
        {
            _multiImageConfirmImagePaths.Clear();
            if (System.IO.Directory.Exists(folderPath))
            {
                var candidates = System.IO.Directory.GetFiles(folderPath, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                foreach (var candidate in candidates)
                {
                    var extension = System.IO.Path.GetExtension(candidate);
                    if (string.IsNullOrEmpty(extension))
                    {
                        continue;
                    }

                    var normalizedExtension = extension.ToLowerInvariant();
                    if (normalizedExtension == ".ini" || normalizedExtension == ".txt" || normalizedExtension == ".db")
                    {
                        continue;
                    }

                    _multiImageConfirmImagePaths.Add(candidate);
                }

                _multiImageConfirmImagePaths.Sort(CompareMultiImageFileNames);
            }

            _multiImageConfirmImageIndex = _multiImageConfirmImagePaths.FindIndex(path => string.Equals(path, selectedImagePath, StringComparison.OrdinalIgnoreCase));
            if (_multiImageConfirmImageIndex < 0 && _multiImageConfirmImagePaths.Count > 0)
            {
                _multiImageConfirmImageIndex = 0;
            }

            ShowCurrentMultiImageConfirmImage();
            UpdateMultiImageNavigationButtons();
            UpdateMultiImageStatusLabel();
        }

        private void ShowCurrentMultiImageConfirmImage()
        {
            if (_multiImageConfirmImageIndex < 0 || _multiImageConfirmImageIndex >= _multiImageConfirmImagePaths.Count)
            {
                return;
            }

            ShowMultiImageConfirmImage(_multiImageConfirmImagePaths[_multiImageConfirmImageIndex]);
            UpdateMultiImageStatusLabel();
        }

        private void UpdateMultiImageNavigationButtons()
        {
            var hasImages = _multiImageConfirmImagePaths.Count > 0;
            if (_buttonMultiImagePrev != null)
            {
                _buttonMultiImagePrev.Enabled = hasImages;
            }

            if (_buttonMultiImageNext != null)
            {
                _buttonMultiImageNext.Enabled = hasImages;
            }
        }

        private void UpdateMultiImageStatusLabel()
        {
            if (labelMultiImageStatus == null)
            {
                return;
            }

            if (_multiImageConfirmImagePaths.Count == 0 || _multiImageConfirmImageIndex < 0)
            {
                labelMultiImageStatus.Text = "0 / 0";
                return;
            }

            labelMultiImageStatus.Text = string.Format("{0} / {1}", _multiImageConfirmImageIndex + 1, _multiImageConfirmImagePaths.Count);
        }

        private string FindMultiImageConfirmImage(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !System.IO.Directory.Exists(folderPath))
            {
                return null;
            }

            var candidates = System.IO.Directory.GetFiles(folderPath, "*.*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var candidate in candidates)
            {
                var extension = System.IO.Path.GetExtension(candidate);
                if (string.IsNullOrEmpty(extension))
                {
                    continue;
                }

                var normalizedExtension = extension.ToLowerInvariant();
                if (normalizedExtension == ".ini" || normalizedExtension == ".txt" || normalizedExtension == ".db")
                {
                    continue;
                }

                return candidate;
            }

            return null;
        }

        private static int CompareMultiImageFileNames(string left, string right)
        {
            var leftName = System.IO.Path.GetFileNameWithoutExtension(left) ?? string.Empty;
            var rightName = System.IO.Path.GetFileNameWithoutExtension(right) ?? string.Empty;

            var leftMatch = Regex.Match(leftName, @"\d+");
            var rightMatch = Regex.Match(rightName, @"\d+");
            if (leftMatch.Success && rightMatch.Success)
            {
                long leftNumber;
                long rightNumber;
                if (long.TryParse(leftMatch.Value, out leftNumber) && long.TryParse(rightMatch.Value, out rightNumber))
                {
                    var numberCompare = leftNumber.CompareTo(rightNumber);
                    if (numberCompare != 0)
                    {
                        return numberCompare;
                    }
                }
            }

            return string.Compare(leftName, rightName, StringComparison.OrdinalIgnoreCase);
        }

        private void ShowMultiImageConfirmImage(string imagePath)
        {
            if (_pictureBoxMultiImageConfirm == null || string.IsNullOrWhiteSpace(imagePath))
            {
                return;
            }

            Bitmap bitmap = null;
            try
            {
                using (var temp = new Bitmap(imagePath))
                {
                    bitmap = new Bitmap(temp);
                }
            }
            catch
            {
                bitmap?.Dispose();
                MessageBox.Show(this, "圖片載入失敗。", "多影像確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _multiImageConfirmBitmap?.Dispose();
            _multiImageConfirmBitmap = bitmap;
            _pictureBoxMultiImageConfirm.Image = null;
            FitMultiImageConfirmToViewport();
            _panelMultiImageConfirmViewport.Invalidate();
        }

        private void FitMultiImageConfirmToViewport()
        {
            if (_multiImageConfirmBitmap == null || _panelMultiImageConfirmViewport == null)
            {
                return;
            }

            var panelWidth = Math.Max(1, _panelMultiImageConfirmViewport.ClientSize.Width);
            var panelHeight = Math.Max(1, _panelMultiImageConfirmViewport.ClientSize.Height);
            var scaleX = (float)panelWidth / _multiImageConfirmBitmap.Width;
            var scaleY = (float)panelHeight / _multiImageConfirmBitmap.Height;
            _multiImageConfirmFitScale = Math.Max(0.01f, Math.Min(scaleX, scaleY));
            _multiImageConfirmImageScale = _multiImageConfirmFitScale;
            _multiImageConfirmPanning = false;
            CenterMultiImageConfirmImage();
        }

        private void CenterMultiImageConfirmImage()
        {
            if (_multiImageConfirmBitmap == null || _panelMultiImageConfirmViewport == null)
            {
                return;
            }

            var scaledWidth = _multiImageConfirmBitmap.Width * _multiImageConfirmImageScale;
            var scaledHeight = _multiImageConfirmBitmap.Height * _multiImageConfirmImageScale;
            _multiImageConfirmOffsetX = Math.Max(0, (_panelMultiImageConfirmViewport.ClientSize.Width - scaledWidth) / 2f);
            _multiImageConfirmOffsetY = Math.Max(0, (_panelMultiImageConfirmViewport.ClientSize.Height - scaledHeight) / 2f);
        }

        private void ConstrainMultiImageConfirmImagePosition()
        {
            if (_multiImageConfirmBitmap == null || _panelMultiImageConfirmViewport == null)
            {
                return;
            }

            var scaledWidth = _multiImageConfirmBitmap.Width * _multiImageConfirmImageScale;
            var scaledHeight = _multiImageConfirmBitmap.Height * _multiImageConfirmImageScale;
            var minLeft = _panelMultiImageConfirmViewport.ClientSize.Width - scaledWidth;
            var minTop = _panelMultiImageConfirmViewport.ClientSize.Height - scaledHeight;

            if (scaledWidth <= _panelMultiImageConfirmViewport.ClientSize.Width)
            {
                _multiImageConfirmOffsetX = Math.Max(0, (_panelMultiImageConfirmViewport.ClientSize.Width - scaledWidth) / 2f);
            }
            else
            {
                _multiImageConfirmOffsetX = Math.Max(minLeft, Math.Min(0, _multiImageConfirmOffsetX));
            }

            if (scaledHeight <= _panelMultiImageConfirmViewport.ClientSize.Height)
            {
                _multiImageConfirmOffsetY = Math.Max(0, (_panelMultiImageConfirmViewport.ClientSize.Height - scaledHeight) / 2f);
            }
            else
            {
                _multiImageConfirmOffsetY = Math.Max(minTop, Math.Min(0, _multiImageConfirmOffsetY));
            }
        }

        private void PictureBoxMultiImageConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            if (_multiImageConfirmBitmap == null)
            {
                return;
            }

            var sourceControl = sender as Control ?? _pictureBoxMultiImageConfirm;
            if (e.Button == MouseButtons.Right)
            {
                _multiImageConfirmPanning = true;
                _lastMultiImageConfirmMousePosition = _panelMultiImageConfirmViewport.PointToClient(sourceControl.PointToScreen(e.Location));
                _panelMultiImageConfirmViewport.Cursor = Cursors.SizeAll;
                _panelMultiImageConfirmViewport.Capture = true;
            }
        }

        private void PictureBoxMultiImageConfirm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_multiImageConfirmPanning)
            {
                return;
            }

            var sourceControl = sender as Control ?? _pictureBoxMultiImageConfirm;
            var currentPosition = _panelMultiImageConfirmViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            _multiImageConfirmOffsetX += currentPosition.X - _lastMultiImageConfirmMousePosition.X;
            _multiImageConfirmOffsetY += currentPosition.Y - _lastMultiImageConfirmMousePosition.Y;
            _lastMultiImageConfirmMousePosition = currentPosition;
            ConstrainMultiImageConfirmImagePosition();
            _panelMultiImageConfirmViewport.Invalidate();
        }

        private void PictureBoxMultiImageConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _multiImageConfirmPanning = false;
            _panelMultiImageConfirmViewport.Cursor = Cursors.Default;
            _panelMultiImageConfirmViewport.Capture = false;
        }

        private void PictureBoxMultiImageConfirm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_multiImageConfirmBitmap == null)
            {
                return;
            }

            var sourceControl = sender as Control ?? _pictureBoxMultiImageConfirm;
            var mousePosition = _panelMultiImageConfirmViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - _multiImageConfirmOffsetX) / _multiImageConfirmImageScale;
            var imageY = (mousePosition.Y - _multiImageConfirmOffsetY) / _multiImageConfirmImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _multiImageConfirmFitScale * 0.25f;
            var maximumScale = _multiImageConfirmFitScale * 20f;
            var newScale = Math.Max(minimumScale, Math.Min(maximumScale, _multiImageConfirmImageScale * zoomFactor));
            _multiImageConfirmImageScale = newScale;
            _multiImageConfirmOffsetX = mousePosition.X - imageX * _multiImageConfirmImageScale;
            _multiImageConfirmOffsetY = mousePosition.Y - imageY * _multiImageConfirmImageScale;
            ConstrainMultiImageConfirmImagePosition();
            _panelMultiImageConfirmViewport.Invalidate();
        }

        private void PictureBoxMultiImageConfirm_MouseEnter(object sender, EventArgs e)
        {
            if (_panelMultiImageConfirmViewport != null)
            {
                _panelMultiImageConfirmViewport.Focus();
            }
        }

        private void MultiImageConfirmPrev_Click(object sender, EventArgs e)
        {
            if (_multiImageConfirmImagePaths.Count == 0)
            {
                return;
            }

            _multiImageConfirmImageIndex--;
            if (_multiImageConfirmImageIndex < 0)
            {
                _multiImageConfirmImageIndex = _multiImageConfirmImagePaths.Count - 1;
            }

            ShowCurrentMultiImageConfirmImage();
        }

        private void MultiImageConfirmNext_Click(object sender, EventArgs e)
        {
            if (_multiImageConfirmImagePaths.Count == 0)
            {
                return;
            }

            _multiImageConfirmImageIndex++;
            if (_multiImageConfirmImageIndex >= _multiImageConfirmImagePaths.Count)
            {
                _multiImageConfirmImageIndex = 0;
            }

            ShowCurrentMultiImageConfirmImage();
        }

        private void PictureBoxMultiImageConfirm_Paint(object sender, PaintEventArgs e)
        {
            if (_multiImageConfirmBitmap == null)
            {
                return;
            }

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var destRect = new RectangleF(
                _multiImageConfirmOffsetX,
                _multiImageConfirmOffsetY,
                _multiImageConfirmBitmap.Width * _multiImageConfirmImageScale,
                _multiImageConfirmBitmap.Height * _multiImageConfirmImageScale);
            e.Graphics.DrawImage(_multiImageConfirmBitmap, destRect);

            var imageRect = GetMultiImageConfirmImageDisplayRect();
            if (imageRect != Rectangle.Empty)
            {
                DrawMultiImageConfirmReferenceRoi(e.Graphics, imageRect);
            }

            var referenceCandidate = GetMultiImageConfirmReferenceCandidate();
            if (referenceCandidate != null && imageRect != Rectangle.Empty)
            {
                DrawMultiImageConfirmReferenceBaseline(e.Graphics, referenceCandidate, imageRect);
            }

            var measureRecords = GetMultiImageConfirmMeasureRecords(referenceCandidate);
            if (measureRecords.Count == 0)
            {
                return;
            }

            if (imageRect == Rectangle.Empty)
            {
                return;
            }

            foreach (var record in measureRecords)
            {
                var color = MeasurementOverlayService.GetSourceColor(record.SourceName);
                using (var pen = new Pen(color, 2f))
                using (var brush = new SolidBrush(color))
                {
                    MeasurementOverlayService.DrawMeasureRecord(
                        e.Graphics,
                        pen,
                        brush,
                        GetMultiImageConfirmDisplayPoint(record.StartPoint, imageRect),
                        GetMultiImageConfirmDisplayPoint(record.EndPoint, imageRect));
                }
            }
        }

        private List<MeasureRecord> GetMultiImageConfirmMeasureRecords(ReferenceCornerCandidate referenceCandidate)
        {
            var productKey = GetCurrentProductKeyOrDefault();
            if (string.IsNullOrWhiteSpace(productKey) || !_measureProfiles.ContainsKey(productKey))
            {
                return new List<MeasureRecord>();
            }

            var records = CloneMeasureRecords(_measureProfiles[productKey]);
            if (referenceCandidate == null)
            {
                return records;
            }

            var reprojectedRecords = new List<MeasureRecord>(records.Count);
            foreach (var record in records)
            {
                reprojectedRecords.Add(MeasurementRecordService.ReprojectForCurrentReference(record, referenceCandidate));
            }

            return reprojectedRecords;
        }

        private ReferenceCornerCandidate GetMultiImageConfirmReferenceCandidate()
        {
            if (_multiImageConfirmBitmap == null)
            {
                return null;
            }

            using (var sourceMat = BitmapConverter.ToMat(_multiImageConfirmBitmap))
            using (var grayMat = new OpenCvSharp.Mat())
            {
                if (sourceMat.Empty())
                {
                    return null;
                }

                if (sourceMat.Channels() == 1)
                {
                    sourceMat.CopyTo(grayMat);
                }
                else
                {
                    OpenCvSharp.Cv2.CvtColor(sourceMat, grayMat, OpenCvSharp.ColorConversionCodes.BGR2GRAY);
                }

                var roi = GetMultiImageConfirmReferenceRoi(grayMat.Size());
                if (roi.Width <= 0 || roi.Height <= 0)
                {
                    return null;
                }

                using (var binaryMat = BuildMultiImageConfirmReferenceBinary(grayMat))
                {
                    if (binaryMat == null || binaryMat.Empty())
                    {
                        return null;
                    }

                    var center = new System.Drawing.Point(
                        roi.Left + roi.Width / 2,
                        roi.Top + roi.Height / 2);
                    return ReferenceCornerDetectionService.FindCandidate(binaryMat, roi, center);
                }
            }
        }

        private OpenCvSharp.Mat BuildMultiImageConfirmReferenceBinary(OpenCvSharp.Mat grayMat)
        {
            if (grayMat == null || grayMat.Empty())
            {
                return null;
            }

            PreprocessParam preprocessParam;
            if (TryGetReferenceCornerPreprocessParam(out preprocessParam))
            {
                return PreprocessPipelineService.Build(grayMat, preprocessParam);
            }

            return null;
        }

        private Rectangle GetMultiImageConfirmReferenceRoi(OpenCvSharp.Size imageSize)
        {
            var roi = _referenceRoiRectangle;
            if (roi.Width <= 0 || roi.Height <= 0)
            {
                return Rectangle.Empty;
            }

            var maxWidth = Math.Max(0, imageSize.Width);
            var maxHeight = Math.Max(0, imageSize.Height);
            if (maxWidth <= 0 || maxHeight <= 0)
            {
                return Rectangle.Empty;
            }

            var left = Math.Max(0, Math.Min(roi.Left, maxWidth - 1));
            var top = Math.Max(0, Math.Min(roi.Top, maxHeight - 1));
            var right = Math.Max(left + 1, Math.Min(roi.Right, maxWidth));
            var bottom = Math.Max(top + 1, Math.Min(roi.Bottom, maxHeight));
            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        private void DrawMultiImageConfirmReferenceRoi(Graphics graphics, Rectangle imageRect)
        {
            if (graphics == null || _multiImageConfirmBitmap == null)
            {
                return;
            }

            var roi = GetMultiImageConfirmReferenceRoi(new OpenCvSharp.Size(_multiImageConfirmBitmap.Width, _multiImageConfirmBitmap.Height));
            if (roi.Width <= 0 || roi.Height <= 0)
            {
                return;
            }

            var topLeft = GetMultiImageConfirmDisplayPoint(roi.Location, imageRect);
            var bottomRight = GetMultiImageConfirmDisplayPoint(new Point(roi.Right, roi.Bottom), imageRect);
            var displayRoi = Rectangle.FromLTRB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
            if (displayRoi.Width <= 0 || displayRoi.Height <= 0)
            {
                return;
            }

            using (var fillBrush = new SolidBrush(Color.FromArgb(55, 50, 205, 50)))
            using (var borderPen = new Pen(Color.LimeGreen, 2f))
            {
                graphics.FillRectangle(fillBrush, displayRoi);
                graphics.DrawRectangle(borderPen, displayRoi);
            }
        }

        private void DrawMultiImageConfirmReferenceBaseline(Graphics graphics, ReferenceCornerCandidate candidate, Rectangle imageRect)
        {
            if (graphics == null || candidate == null || imageRect == Rectangle.Empty)
            {
                return;
            }

            using (var topEdgePen = new Pen(Color.LimeGreen, 2f))
            using (var pointBrush = new SolidBrush(Color.Yellow))
            using (var pointOutlinePen = new Pen(Color.Black, 2f))
            {
                var displayTopLeft = GetMultiImageConfirmDisplayPoint(candidate.TopLeft, imageRect);
                var displayTopRight = GetMultiImageConfirmDisplayPoint(candidate.TopRight, imageRect);
                var displayCenter = GetMultiImageConfirmDisplayPoint(candidate.CenterPoint, imageRect);

                graphics.DrawLine(topEdgePen, displayTopLeft, displayTopRight);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, displayTopLeft);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, displayTopRight);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, displayCenter);
            }
        }

        private Rectangle GetMultiImageConfirmImageDisplayRect()
        {
            if (_multiImageConfirmBitmap == null || _panelMultiImageConfirmViewport == null)
            {
                return Rectangle.Empty;
            }

            var width = Math.Max(1, (int)Math.Round(_multiImageConfirmBitmap.Width * _multiImageConfirmImageScale));
            var height = Math.Max(1, (int)Math.Round(_multiImageConfirmBitmap.Height * _multiImageConfirmImageScale));
            return new Rectangle(
                (int)Math.Round(_multiImageConfirmOffsetX),
                (int)Math.Round(_multiImageConfirmOffsetY),
                width,
                height);
        }

        private Point GetMultiImageConfirmDisplayPoint(Point imagePoint, Rectangle imageRect)
        {
            if (_multiImageConfirmBitmap == null)
            {
                return imagePoint;
            }

            return MeasurementOverlayService.ToDisplayPoint(imagePoint, imageRect, _multiImageConfirmBitmap.Size);
        }

        private Bitmap AnnotateMeasurePreviewBitmap(Bitmap sourceBitmap)
        {
            if (sourceBitmap == null)
            {
                return null;
            }

            if (!_referenceCornerFound || _referenceCornerCandidate == null)
            {
                return new Bitmap(sourceBitmap);
            }

            var annotated = new Bitmap(sourceBitmap);
            using (var graphics = Graphics.FromImage(annotated))
            using (var topEdgePen = new Pen(Color.LimeGreen, 2f))
            using (var pointBrush = new SolidBrush(Color.Yellow))
            using (var pointOutlinePen = new Pen(Color.Black, 2f))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.DrawLine(topEdgePen, _referenceCornerCandidate.TopLeft, _referenceCornerCandidate.TopRight);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, _referenceCornerCandidate.TopLeft);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, _referenceCornerCandidate.TopRight);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, _referenceCornerCandidate.CenterPoint);
            }

            return annotated;
        }

        private Bitmap GetMeasureSourceBitmap()
        {
            if (_comboBoxMeasureSource == null || _preprocessPictureBoxes == null)
            {
                return pictureBoxImage.Image == null ? null : new Bitmap(pictureBoxImage.Image);
            }

            var index = _comboBoxMeasureSource.SelectedIndex;
            if (index >= 0 && index < _preprocessPictureBoxes.Length && _preprocessPictureBoxes[index].Image != null)
            {
                return new Bitmap(_preprocessPictureBoxes[index].Image);
            }

            return pictureBoxImage.Image == null ? null : new Bitmap(pictureBoxImage.Image);
        }

        private bool IsSelectedMeasureSourceAvailable()
        {
            if (_comboBoxMeasureSource == null || _preprocessPictureBoxes == null)
            {
                return false;
            }

            var index = _comboBoxMeasureSource.SelectedIndex;
            return index >= 0
                && index < _preprocessPictureBoxes.Length
                && _preprocessPictureBoxes[index].Image != null;
        }

        private void UpdateMeasureSourceAvailability()
        {
            _measureSourceAvailable = IsSelectedMeasureSourceAvailable();

            if (_buttonSaveMeasurePoint != null)
            {
                _buttonSaveMeasurePoint.Enabled = _measureSourceAvailable;
            }

            if (_buttonClearMeasurePoint != null)
            {
                _buttonClearMeasurePoint.Enabled = _measureSourceAvailable || _measureRecords.Count > 0 || _measurePoints.Count > 0;
            }

            if (_buttonSaveMeasureRecords != null)
            {
                _buttonSaveMeasureRecords.Enabled = _measureRecords.Count > 0;
            }

            if (_buttonParallelMeasure != null)
            {
                _buttonParallelMeasure.Enabled = _measureSourceAvailable && _referenceCornerFound && _referenceCornerCandidate != null;
            }

            if (_buttonPerpendicularMeasure != null)
            {
                _buttonPerpendicularMeasure.Enabled = _measureSourceAvailable && _referenceCornerFound && _referenceCornerCandidate != null;
            }

            if (_labelMeasureStatus != null)
            {
                _labelMeasureStatus.Text = _measureSourceAvailable
                    ? "點兩下影像建立兩點量測，保存後寫入表格"
                    : "目前沒有可用的前處理影像，無法設定量測點";
            }

            UpdateMeasureDirectionButtons();
        }

        private void PictureBoxMeasurePreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_measureSourceAvailable)
            {
                return;
            }

            if (e.Button == MouseButtons.Right && _pictureBoxMeasurePreview.Image != null)
            {
                _measurePreviewPanning = true;
                _lastMeasureMousePosition = _panelMeasurePreview.PointToClient(_pictureBoxMeasurePreview.PointToScreen(e.Location));
                _pictureBoxMeasurePreview.Cursor = Cursors.SizeAll;
                _pictureBoxMeasurePreview.Capture = true;
                return;
            }

            if (e.Button != MouseButtons.Left || _pictureBoxMeasurePreview.Image == null)
            {
                return;
            }

            var imagePoint = GetMeasureImagePointFromMouse(e.Location);
            if (imagePoint == Point.Empty)
            {
                return;
            }

            if (!_isMeasureSelecting)
            {
                _measurePoints.Clear();
                _measurePoints.Add(imagePoint);
                _isMeasureSelecting = true;
                _labelMeasureStatus.Text = "請再點第二個點";
            }
            else
            {
                if (_measurePoints.Count == 1)
                {
                    _measurePoints.Add(ConstrainMeasurePoint(_measurePoints[0], imagePoint));
                }
                else
                {
                    _measurePoints[1] = ConstrainMeasurePoint(_measurePoints[0], imagePoint);
                }

                _isMeasureSelecting = false;
                UpdateMeasureStatusForCurrentPoints();
                _pictureBoxMeasurePreview.Invalidate();
            }
        }

        private void PictureBoxMeasurePreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_measurePreviewPanning)
            {
                return;
            }

            var currentPosition = _panelMeasurePreview.PointToClient(_pictureBoxMeasurePreview.PointToScreen(e.Location));
            _pictureBoxMeasurePreview.Left += currentPosition.X - _lastMeasureMousePosition.X;
            _pictureBoxMeasurePreview.Top += currentPosition.Y - _lastMeasureMousePosition.Y;
            _lastMeasureMousePosition = currentPosition;
            ConstrainMeasureImagePosition();
        }

        private void PictureBoxMeasurePreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _measurePreviewPanning = false;
            _pictureBoxMeasurePreview.Cursor = Cursors.Default;
            _pictureBoxMeasurePreview.Capture = false;
        }

        private void PictureBoxMeasurePreview_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_pictureBoxMeasurePreview.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = _panelMeasurePreview.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - _pictureBoxMeasurePreview.Left) / _measureImageScale;
            var imageY = (mousePosition.Y - _pictureBoxMeasurePreview.Top) / _measureImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _measureFitScale * 0.25f;
            var maximumScale = _measureFitScale * 20f;
            _measureImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _measureImageScale * zoomFactor));

            var width = Math.Max(1, (int)Math.Round(_pictureBoxMeasurePreview.Image.Width * _measureImageScale));
            var height = Math.Max(1, (int)Math.Round(_pictureBoxMeasurePreview.Image.Height * _measureImageScale));
            _pictureBoxMeasurePreview.Size = new Size(width, height);
            _pictureBoxMeasurePreview.Left = (int)Math.Round(mousePosition.X - imageX * _measureImageScale);
            _pictureBoxMeasurePreview.Top = (int)Math.Round(mousePosition.Y - imageY * _measureImageScale);
            ConstrainMeasureImagePosition();
        }

        private void PictureBoxMeasurePreview_MouseEnter(object sender, EventArgs e)
        {
            _pictureBoxMeasurePreview.Focus();
        }

        private void PictureBoxMeasurePreview_Paint(object sender, PaintEventArgs e)
        {
            if (_measureRecords.Count == 0 && _measurePoints.Count < 1)
            {
                return;
            }

            var imageRect = GetMeasureImageDisplayRect();
            if (imageRect == Rectangle.Empty)
            {
                return;
            }

            foreach (var record in _measureRecords)
            {
                var color = MeasurementOverlayService.GetSourceColor(record.SourceName);
                var isBlinkTarget = ReferenceEquals(record, _measureBlinkRecord) && _measureBlinkRemainingTicks > 0;
                var penWidth = isBlinkTarget ? 4f : 2f;
                var drawColor = isBlinkTarget ? Color.Yellow : color;
                using (var pen = new Pen(drawColor, penWidth))
                using (var brush = new SolidBrush(drawColor))
                {
                    MeasurementOverlayService.DrawMeasureRecord(
                        e.Graphics,
                        pen,
                        brush,
                        GetMeasureDisplayPoint(record.StartPoint),
                        GetMeasureDisplayPoint(record.EndPoint));
                }
            }

            if (_measurePoints.Count >= 1)
            {
                var currentColor = MeasurementOverlayService.GetSourceColor(GetSelectedMeasureSourceName());
                using (var pen = new Pen(currentColor, 2f))
                using (var brush = new SolidBrush(currentColor))
                {
                    var p1 = GetMeasureDisplayPoint(_measurePoints[0]);
                    e.Graphics.FillEllipse(brush, p1.X - 4, p1.Y - 4, 8, 8);
                    if (_measurePoints.Count >= 2)
                    {
                        var p2 = GetMeasureDisplayPoint(_measurePoints[1]);
                        e.Graphics.DrawLine(pen, p1, p2);
                        e.Graphics.FillEllipse(brush, p2.X - 4, p2.Y - 4, 8, 8);
                    }
                }
            }
        }

        private void SaveMeasurePoint_Click(object sender, EventArgs e)
        {
            if (_measurePoints.Count < 2)
            {
                MessageBox.Show(this, "請先在影像上點選兩個點。", "量測距離", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_referenceCornerCandidate == null)
            {
                MessageBox.Show(this, "請先完成左上角與右上角基準的辨識，才能保存相對量測資料。", "量測距離", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var p1 = _measurePoints[0];
            var p2 = _measurePoints[1];
            var record = MeasurementRecordService.CreateRecord(
                p1,
                p2,
                _referenceCornerCandidate,
                GetSelectedMeasureSourceName(),
                GetMeasureDirectionName());
            _measureRecords.Add(record);
            AddMeasureRecordRow(record);

            _labelMeasureStatus.Text = "已保存量測點，並寫入表格。";
            _pictureBoxMeasurePreview.Invalidate();
            UpdateMeasureSourceAvailability();
            SaveCurrentAppSettings();
        }

        private void ClearMeasurePoint_Click(object sender, EventArgs e)
        {
            _measurePoints.Clear();
            _isMeasureSelecting = false;
            _labelMeasureStatus.Text = "點兩下影像建立兩點量測，保存後寫入表格";
            _pictureBoxMeasurePreview.Invalidate();
            UpdateMeasureSourceAvailability();
        }

        private void SaveMeasureRecords_Click(object sender, EventArgs e)
        {
            PersistActiveProductProfile();
            SaveCurrentAppSettings();
            _labelMeasureStatus.Text = "已保存表格紀錄到 setting.ini";
        }

        private void MeasureSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            _measurePoints.Clear();
            _isMeasureSelecting = false;
            RefreshMeasureDistancePreview();
            UpdateMeasureSourceAvailability();
            UpdateMeasureDirectionButtons();
        }

        private void ParallelMeasure_Click(object sender, EventArgs e)
        {
            _measureDirectionMode = MeasureDirectionMode.Parallel;
            UpdateMeasureDirectionButtons();
        }

        private void PerpendicularMeasure_Click(object sender, EventArgs e)
        {
            _measureDirectionMode = MeasureDirectionMode.Perpendicular;
            UpdateMeasureDirectionButtons();
        }

        private void MeasureRecords_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
            {
                return;
            }

            var hit = _dataGridViewMeasureRecords.HitTest(e.X, e.Y);
            if (hit.RowIndex < 0)
            {
                return;
            }

            _dataGridViewMeasureRecords.ClearSelection();
            _dataGridViewMeasureRecords.Rows[hit.RowIndex].Selected = true;
            _dataGridViewMeasureRecords.CurrentCell = _dataGridViewMeasureRecords.Rows[hit.RowIndex].Cells[0];
        }

        private void MeasureRecords_SelectionChanged(object sender, EventArgs e)
        {
            if (_dataGridViewMeasureRecords == null || _dataGridViewMeasureRecords.SelectedRows.Count == 0)
            {
                return;
            }

            var row = _dataGridViewMeasureRecords.SelectedRows[0];
            if (!(row.Tag is MeasureRecord record))
            {
                return;
            }

            _measureBlinkRecord = record;
            _measureBlinkRemainingTicks = 6;
            _measureBlinkTimer.Stop();
            _measureBlinkTimer.Start();
            if (_pictureBoxMeasurePreview != null)
            {
                _pictureBoxMeasurePreview.Invalidate();
            }
        }

        private void MeasureBlinkTimer_Tick(object sender, EventArgs e)
        {
            if (_measureBlinkRemainingTicks > 0)
            {
                _measureBlinkRemainingTicks--;
                if (_pictureBoxMeasurePreview != null)
                {
                    _pictureBoxMeasurePreview.Invalidate();
                }
                return;
            }

            _measureBlinkTimer.Stop();
            _measureBlinkRecord = null;
            if (_pictureBoxMeasurePreview != null)
            {
                _pictureBoxMeasurePreview.Invalidate();
            }
        }

        private void MeasureDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (_dataGridViewMeasureRecords.SelectedRows.Count == 0)
            {
                return;
            }

            var row = _dataGridViewMeasureRecords.SelectedRows[0];
            if (row.Tag is MeasureRecord record)
            {
                if (ReferenceEquals(_measureBlinkRecord, record))
                {
                    _measureBlinkTimer.Stop();
                    _measureBlinkRecord = null;
                    _measureBlinkRemainingTicks = 0;
                }
                _measureRecords.Remove(record);
            }
            if (!row.IsNewRow)
            {
                _dataGridViewMeasureRecords.Rows.Remove(row);
            }

            ReindexMeasureRows();
            RefreshMeasureDistancePreview();
            UpdateMeasureSourceAvailability();
            SaveCurrentAppSettings();
        }

        private void ClearAllMeasureRecords()
        {
            _measureBlinkTimer?.Stop();
            _measureBlinkRecord = null;
            _measureBlinkRemainingTicks = 0;
            _measurePoints.Clear();
            _measureRecords.Clear();
            _isMeasureSelecting = false;
            if (_dataGridViewMeasureRecords != null)
            {
                _dataGridViewMeasureRecords.Rows.Clear();
            }
            if (_pictureBoxMeasurePreview != null)
            {
                _pictureBoxMeasurePreview.Invalidate();
            }

            UpdateMeasureSourceAvailability();
        }

        private static List<MeasureRecord> CloneMeasureRecords(List<MeasureRecord> records)
        {
            return ProfileDataCloner.CloneMeasureRecords(records);
        }

        private void ApplyMeasureRecords(List<MeasureRecord> records)
        {
            _measurePoints.Clear();
            _measureRecords.Clear();
            _isMeasureSelecting = false;

            if (_dataGridViewMeasureRecords != null)
            {
                _dataGridViewMeasureRecords.Rows.Clear();
            }

            if (records == null)
            {
                return;
            }

            foreach (var record in records)
            {
                if (record == null)
                {
                    continue;
                }

                var displayRecord = MeasurementRecordService.ReprojectForCurrentReference(record, _referenceCornerCandidate);
                _measureRecords.Add(displayRecord);
            }

            if (_dataGridViewMeasureRecords != null)
            {
                for (var i = 0; i < _measureRecords.Count; i++)
                {
                    AddMeasureRecordRow(_measureRecords[i]);
                }
            }

            if (_pictureBoxMeasurePreview != null)
            {
                _pictureBoxMeasurePreview.Invalidate();
            }
        }

        private string GetSelectedMeasureSourceName()
        {
            if (_comboBoxMeasureSource == null || _comboBoxMeasureSource.SelectedIndex < 0)
            {
                return string.Empty;
            }

            return string.Format("前處理 {0}", _comboBoxMeasureSource.SelectedIndex + 1);
        }

        private string GetMeasureDirectionName()
        {
            switch (_measureDirectionMode)
            {
                case MeasureDirectionMode.Parallel:
                    return "平行";
                case MeasureDirectionMode.Perpendicular:
                    return "垂直";
                default:
                    return "未選";
            }
        }

        private void UpdateMeasureDirectionButtons()
        {
            if (_buttonParallelMeasure == null || _buttonPerpendicularMeasure == null)
            {
                return;
            }

            var directionEnabled = _measureSourceAvailable && _referenceCornerFound && _referenceCornerCandidate != null;
            _buttonParallelMeasure.Enabled = directionEnabled;
            _buttonPerpendicularMeasure.Enabled = directionEnabled;
            _buttonParallelMeasure.BackColor = _measureDirectionMode == MeasureDirectionMode.Parallel
                ? Color.FromArgb(200, 233, 200)
                : Color.FromArgb(224, 228, 231);
            _buttonPerpendicularMeasure.BackColor = _measureDirectionMode == MeasureDirectionMode.Perpendicular
                ? Color.FromArgb(200, 233, 200)
                : Color.FromArgb(224, 228, 231);
        }

        private Point ConstrainMeasurePoint(Point startPoint, Point rawPoint)
        {
            if (!_referenceCornerFound)
            {
                return rawPoint;
            }

            return MeasurementOverlayService.ConstrainPoint(startPoint, rawPoint, _referenceCornerCandidate, _measureDirectionMode);
        }

        private Point GetMeasureImagePointFromMouse(Point displayPoint)
        {
            if (_pictureBoxMeasurePreview.Image == null)
            {
                return Point.Empty;
            }

            var imageRect = GetMeasureImageDisplayRect();
            if (imageRect.Width <= 0 || imageRect.Height <= 0)
            {
                return Point.Empty;
            }

            return MeasurementOverlayService.ToImagePoint(displayPoint, imageRect, _pictureBoxMeasurePreview.Image.Size);
        }

        private Point GetMeasureDisplayPoint(Point imagePoint)
        {
            if (_pictureBoxMeasurePreview.Image == null)
            {
                return imagePoint;
            }

            var imageRect = GetMeasureImageDisplayRect();
            if (imageRect.Width <= 0 || imageRect.Height <= 0)
            {
                return imagePoint;
            }

            return MeasurementOverlayService.ToDisplayPoint(imagePoint, imageRect, _pictureBoxMeasurePreview.Image.Size);
        }

        private void FitMeasureImageToViewport()
        {
            if (_pictureBoxMeasurePreview.Image == null)
            {
                return;
            }

            MeasurementCoordinateService.FitToViewport(
                _pictureBoxMeasurePreview,
                _panelMeasurePreview,
                ref _measureImageScale,
                ref _measureFitScale);
        }

        private void ApplyMeasureImageTransform(bool centerImage)
        {
            MeasurementCoordinateService.ApplyTransform(_pictureBoxMeasurePreview, _panelMeasurePreview, _measureImageScale, centerImage);
        }

        private void ConstrainMeasureImagePosition()
        {
            if (_pictureBoxMeasurePreview.Image == null)
            {
                return;
            }

            MeasurementCoordinateService.ConstrainPosition(_pictureBoxMeasurePreview, _panelMeasurePreview);
        }

        private Rectangle GetMeasureImageDisplayRect()
        {
            if (_pictureBoxMeasurePreview.Image == null || _pictureBoxMeasurePreview.ClientSize.Width <= 0 || _pictureBoxMeasurePreview.ClientSize.Height <= 0)
            {
                return Rectangle.Empty;
            }

            return MeasurementOverlayService.GetImageDisplayRect(
                _pictureBoxMeasurePreview.ClientSize,
                _pictureBoxMeasurePreview.Image.Size);
        }

        private void ReindexMeasureRows()
        {
            for (var i = 0; i < _dataGridViewMeasureRecords.Rows.Count; i++)
            {
                _dataGridViewMeasureRecords.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void UpdateMeasureStatusForCurrentPoints()
        {
            if (_measurePoints.Count < 2)
            {
                return;
            }

            var distance = MeasurementRecordService.GetDistance(_measurePoints);
            _labelMeasureStatus.Text = string.Format("已選取兩點，距離約 {0:0.##} px", distance);
        }

        private void AddMeasureRecordRow(MeasureRecord record)
        {
            if (_dataGridViewMeasureRecords == null || record == null)
            {
                return;
            }

            var rowIndex = _dataGridViewMeasureRecords.Rows.Add(
                _dataGridViewMeasureRecords.Rows.Count + 1,
                record.StartPoint.X,
                record.StartPoint.Y,
                record.EndPoint.X,
                record.EndPoint.Y,
                record.ReferenceTopLeft.X,
                record.ReferenceTopLeft.Y,
                record.ReferenceTopRight.X,
                record.ReferenceTopRight.Y,
                record.ReferenceLength.ToString("0.###", CultureInfo.InvariantCulture),
                record.CenterPoint.X,
                record.CenterPoint.Y,
                record.LocalStartPoint.X.ToString("0.###", CultureInfo.InvariantCulture),
                record.LocalStartPoint.Y.ToString("0.###", CultureInfo.InvariantCulture),
                record.LocalEndPoint.X.ToString("0.###", CultureInfo.InvariantCulture),
                record.LocalEndPoint.Y.ToString("0.###", CultureInfo.InvariantCulture),
                record.Distance.ToString("0.##"),
                record.SourceName,
                record.DirectionName);
            _dataGridViewMeasureRecords.Rows[rowIndex].Tag = record;
        }
    }
}
