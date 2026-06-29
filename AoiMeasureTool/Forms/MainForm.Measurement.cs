using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Forms;
using Cv2 = OpenCvSharp.Cv2;
using CvMat = OpenCvSharp.Mat;
using ImreadModes = OpenCvSharp.ImreadModes;
using ColorConversionCodes = OpenCvSharp.ColorConversionCodes;
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
            EnsureMultiImagePreviewSourceItems();
        }

        private void EnsureMultiImagePreviewSourceItems()
        {
            if (comboBoxMultiImagePreviewSource == null)
            {
                return;
            }

            if (comboBoxMultiImagePreviewSource.Items.Count > 0)
            {
                return;
            }

            comboBoxMultiImagePreviewSource.Items.Add("前處理影像 1");
            comboBoxMultiImagePreviewSource.Items.Add("前處理影像 2");
            comboBoxMultiImagePreviewSource.Items.Add("前處理影像 3");
            comboBoxMultiImagePreviewSource.Items.Add("前處理影像 4");
            comboBoxMultiImagePreviewSource.Items.Add("雙門檻");
            comboBoxMultiImagePreviewSource.SelectedIndex = 0;
        }

        private void InitializeMeasureDistanceControls()
        {
            _tabPageMeasureDistance = tabPageMeasureDistance;
            _tabPageMultiImageConfirm = tabPageMultiImageConfirm;
            _panelMeasurePreview = panelMeasurePreview;
            _pictureBoxMeasurePreview = pictureBoxMeasurePreview;
            _dataGridViewMeasureRecords = dataGridViewMeasureRecords;
            _buttonSaveMeasurePoint = buttonSaveMeasurePoint;
            _buttonClearMeasurePoint = buttonClearMeasurePoint;
            _buttonSaveMeasureRecords = buttonSaveMeasureRecords;
            _buttonParallelMeasure = buttonParallelMeasure;
            _buttonPerpendicularMeasure = buttonPerpendicularMeasure;
            _comboBoxMeasureSource = comboBoxMeasureSource;
            _labelMeasureStatus = labelMeasureStatus;
            _panelMultiImageConfirmViewport = panelMultiImageConfirmViewport;
            _pictureBoxMultiImageConfirm = pictureBoxMultiImageConfirm;
            _buttonLoadMultiImageFolder = buttonLoadMultiImageFolder;
            _buttonMultiImagePrev = buttonMultiImagePrev;
            _buttonMultiImageNext = buttonMultiImageNext;
            _buttonMultiImageLineSequence = buttonMultiImageLineSequence;
            _comboBoxMultiImageLineDisplayMode = comboBoxMultiImageLineDisplayMode;
            _dataGridViewMultiImageInfo = dataGridViewMultiImageInfo;
            _dataGridViewMultiImageJudgementResult = InitializeMultiImageJudgementResultGrid();
            groupBoxMultiImagePreviewSource = groupBoxMultiImagePreviewSource ?? this.groupBoxMultiImagePreviewSource;
            comboBoxMultiImagePreviewSource = comboBoxMultiImagePreviewSource ?? this.comboBoxMultiImagePreviewSource;
            buttonLoadMultiImagePreprocess = buttonLoadMultiImagePreprocess ?? this.buttonLoadMultiImagePreprocess;
            buttonLoadMultiImageOriginal = buttonLoadMultiImageOriginal ?? this.buttonLoadMultiImageOriginal;
            if (_comboBoxMeasureSource != null)
            {
                _comboBoxMeasureSource.Items.Clear();
                _comboBoxMeasureSource.Items.AddRange(new object[] { "前處理 1", "前處理 2", "前處理 3", "前處理 4", "雙門檻" });
                _comboBoxMeasureSource.SelectedIndex = 0;
                _comboBoxMeasureSource.SelectedIndexChanged += MeasureSource_SelectedIndexChanged;
            }
            if (_buttonSaveMeasurePoint != null)
            {
                _buttonSaveMeasurePoint.Click += SaveMeasurePoint_Click;
            }
            if (_buttonClearMeasurePoint != null)
            {
                _buttonClearMeasurePoint.Click += ClearMeasurePoint_Click;
            }
            if (_buttonSaveMeasureRecords != null)
            {
                _buttonSaveMeasureRecords.Click += SaveMeasureRecords_Click;
            }
            if (_buttonParallelMeasure != null)
            {
                _buttonParallelMeasure.Click += ParallelMeasure_Click;
            }
            if (_buttonPerpendicularMeasure != null)
            {
                _buttonPerpendicularMeasure.Click += PerpendicularMeasure_Click;
            }
            if (_pictureBoxMeasurePreview != null)
            {
                _pictureBoxMeasurePreview.MouseDown += PictureBoxMeasurePreview_MouseDown;
                _pictureBoxMeasurePreview.MouseMove += PictureBoxMeasurePreview_MouseMove;
                _pictureBoxMeasurePreview.MouseUp += PictureBoxMeasurePreview_MouseUp;
                _pictureBoxMeasurePreview.MouseWheel += PictureBoxMeasurePreview_MouseWheel;
                _pictureBoxMeasurePreview.MouseEnter += PictureBoxMeasurePreview_MouseEnter;
                _pictureBoxMeasurePreview.Paint += PictureBoxMeasurePreview_Paint;
            }
            if (_dataGridViewMeasureRecords != null)
            {
                _dataGridViewMeasureRecords.ReadOnly = true;
                _dataGridViewMeasureRecords.AllowUserToAddRows = false;
                _dataGridViewMeasureRecords.AllowUserToDeleteRows = false;
                _dataGridViewMeasureRecords.RowHeadersVisible = false;
                _dataGridViewMeasureRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (_dataGridViewMeasureRecords.Columns.Count == 0)
                {
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
                }
                _dataGridViewMeasureRecords.MouseDown += MeasureRecords_MouseDown;
                _measureRecordMenu = new ContextMenuStrip();
                _measureEditMenuItem = new ToolStripMenuItem("編輯");
                _measureEditMenuItem.Click += MeasureEditMenuItem_Click;
                _measureDeleteMenuItem = new ToolStripMenuItem("刪除");
                _measureDeleteMenuItem.Click += MeasureDeleteMenuItem_Click;
                _measureRecordMenu.Items.Add(_measureEditMenuItem);
                _measureRecordMenu.Items.Add(_measureDeleteMenuItem);
                _dataGridViewMeasureRecords.ContextMenuStrip = _measureRecordMenu;
                _dataGridViewMeasureRecords.SelectionChanged += MeasureRecords_SelectionChanged;
            }
            _measureBlinkTimer = new System.Windows.Forms.Timer();
            _measureBlinkTimer.Interval = 180;
            _measureBlinkTimer.Tick += MeasureBlinkTimer_Tick;
            _multiImageLineSequenceTimer = new System.Windows.Forms.Timer();
            _multiImageLineSequenceTimer.Interval = 180;
            _multiImageLineSequenceTimer.Tick += MultiImageLineSequenceTimer_Tick;
            if (groupBoxMultiImagePreviewSource != null)
            {
                groupBoxMultiImagePreviewSource.Text = "預覽來源";
            }
            if (buttonLoadMultiImagePreprocess != null)
            {
                buttonLoadMultiImagePreprocess.Text = "讀取前處理影像";
            }
            if (buttonLoadMultiImageOriginal != null)
            {
                buttonLoadMultiImageOriginal.Text = "原始影像";
            }
            if (comboBoxMultiImagePreviewSource != null)
            {
                comboBoxMultiImagePreviewSource.Items.Clear();
                comboBoxMultiImagePreviewSource.Items.AddRange(new object[]
                {
                    "前處理影像 1",
                    "前處理影像 2",
                    "前處理影像 3",
                    "前處理影像 4",
                    "雙門檻"
                });
                comboBoxMultiImagePreviewSource.SelectedIndex = 0;
            }
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
            if (_buttonMultiImageLineSequence != null)
            {
                _buttonMultiImageLineSequence.Click += MultiImageLineSequence_Click;
            }
            if (_comboBoxMultiImageLineDisplayMode != null)
            {
                _comboBoxMultiImageLineDisplayMode.Items.Clear();
                _comboBoxMultiImageLineDisplayMode.Items.Add("設定的線段");
                _comboBoxMultiImageLineDisplayMode.Items.Add("找出的線段");
                _comboBoxMultiImageLineDisplayMode.Items.Add("都不要顯示");
                _comboBoxMultiImageLineDisplayMode.SelectedIndex = 1;
                _comboBoxMultiImageLineDisplayMode.SelectedIndexChanged += MultiImageLineDisplayMode_SelectedIndexChanged;
            }
            InitializeMultiImageInfoGrid();
            UpdateMultiImageNavigationButtons();
            UpdateMeasureSourceAvailability();
            UpdateMeasureDirectionButtons();
            UpdateMultiImageInfoTable();
            RefreshMultiImageJudgementResultTable();
        }

        private DataGridView InitializeMultiImageJudgementResultGrid()
        {
            if (tabPageMultiImageJudgementResult == null)
            {
                return null;
            }

            var grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.FromArgb(239, 241, 243);
            grid.BorderStyle = BorderStyle.None;
            if (grid.Columns.Count == 0)
            {
                grid.Columns.Add("colName", "規則名稱");
                grid.Columns.Add("colCalc", "計算值");
                grid.Columns.Add("colJudge", "判定");
            }

            tabPageMultiImageJudgementResult.Controls.Clear();
            tabPageMultiImageJudgementResult.Controls.Add(grid);
            return grid;
        }

        private void InitializeMultiImageInfoGrid()
        {
            if (_dataGridViewMultiImageInfo == null)
            {
                return;
            }

            _dataGridViewMultiImageInfo.ReadOnly = true;
            _dataGridViewMultiImageInfo.AllowUserToAddRows = false;
            _dataGridViewMultiImageInfo.AllowUserToDeleteRows = false;
            _dataGridViewMultiImageInfo.AllowUserToResizeColumns = false;
            _dataGridViewMultiImageInfo.AllowUserToResizeRows = false;
            _dataGridViewMultiImageInfo.RowHeadersVisible = false;
            _dataGridViewMultiImageInfo.ColumnHeadersVisible = false;
            _dataGridViewMultiImageInfo.MultiSelect = false;
            _dataGridViewMultiImageInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridViewMultiImageInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (_dataGridViewMultiImageInfo.Columns.Count == 0)
            {
                _dataGridViewMultiImageInfo.Columns.Add("colItem", "項目");
                _dataGridViewMultiImageInfo.Columns.Add("colValue", "內容");
            }

            _dataGridViewMultiImageInfo.Columns[0].FillWeight = 42f;
            _dataGridViewMultiImageInfo.Columns[1].FillWeight = 58f;
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

        private void MultiImagePreviewSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_multiImageConfirmShowingPreprocess || _multiImageConfirmImageIndex < 0)
            {
                return;
            }

            ShowCurrentMultiImageConfirmImage();
        }

        private void LoadMultiImagePreprocess_Click(object sender, EventArgs e)
        {
            _multiImageConfirmShowingPreprocess = true;
            ShowCurrentMultiImageConfirmImage();
        }

        private void LoadMultiImageOriginal_Click(object sender, EventArgs e)
        {
            _multiImageConfirmShowingPreprocess = false;
            ShowCurrentMultiImageConfirmImage();
        }

        private void LoadMultiImageConfirmFolder(string folderPath, string selectedImagePath)
        {
            _multiImageConfirmImagePaths.Clear();
            _multiImageConfirmProductKey = ResolveMultiImageConfirmProductKey(folderPath, selectedImagePath);
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

            _multiImageConfirmShowingPreprocess = false;
            ShowCurrentMultiImageConfirmImage();
            UpdateMultiImageNavigationButtons();
            UpdateMultiImageStatusLabel();
            UpdateMultiImageInfoTable();
            RefreshMultiImageJudgementResultTable();
        }

        private string ResolveMultiImageConfirmProductKey(string folderPath, string selectedImagePath)
        {
            return GetCurrentProductKeyOrDefault();
        }

        private void ShowCurrentMultiImageConfirmImage()
        {
            if (_multiImageConfirmImageIndex < 0 || _multiImageConfirmImageIndex >= _multiImageConfirmImagePaths.Count)
            {
                return;
            }

            var imagePath = _multiImageConfirmImagePaths[_multiImageConfirmImageIndex];
            if (_multiImageConfirmShowingPreprocess)
            {
                ShowMultiImageConfirmPreprocessImage(imagePath, comboBoxMultiImagePreviewSource.SelectedIndex);
            }
            else
            {
                ShowMultiImageConfirmImage(imagePath);
            }
            UpdateMultiImageStatusLabel();
            UpdateMultiImageInfoTable();
            RefreshMultiImageJudgementResultTable();
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

            if (_buttonMultiImageLineSequence != null)
            {
                _buttonMultiImageLineSequence.Enabled = hasImages;
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

        private void UpdateMultiImageInfoTable()
        {
            if (_dataGridViewMultiImageInfo == null)
            {
                return;
            }

            _dataGridViewMultiImageInfo.Rows.Clear();

            var referenceCandidate = GetMultiImageConfirmReferenceCandidate();
            var allPointsInsideRoi = AreAllMultiImageConfirmPointsInsideRoi(referenceCandidate);
            var hasReferenceCornerAndBaseline = referenceCandidate != null;
            var canJudge = allPointsInsideRoi && hasReferenceCornerAndBaseline;

            AddMultiImageInfoRow("是否可以判斷", canJudge ? "可以判斷" : "不可判斷");
            AddMultiImageInfoRow("所有點都在 ROI 內", allPointsInsideRoi ? "是" : "否");
            AddMultiImageInfoRow("是否找到參考角點與基準線", hasReferenceCornerAndBaseline ? "是" : "否");

            var lineMeasurements = BuildMultiImageConfirmLineMeasurements(referenceCandidate);
            if (lineMeasurements.Count == 0)
            {
                AddMultiImageInfoRow("線段量測", "無可用結果");
                return;
            }

            AddMultiImageInfoRow("線段量測", string.Empty);
            for (var i = 0; i < lineMeasurements.Count; i++)
            {
                var measurement = lineMeasurements[i];
                var label = string.Format("線段 {0}", i + 1);
                AddMultiImageInfoRow(label, measurement.IsValid
                    ? string.Format("{0:0.##} mm ({1:0.##} px)", measurement.MillimeterDistance, measurement.Distance)
                    : "不可判斷");
            }
        }

        private void RefreshMultiImageJudgementResultTable()
        {
            if (_dataGridViewMultiImageJudgementResult == null)
            {
                return;
            }

            _dataGridViewMultiImageJudgementResult.Rows.Clear();
            var results = BuildMultiImageJudgementResults();
            for (var i = 0; i < results.Count; i++)
            {
                var result = results[i];
                _dataGridViewMultiImageJudgementResult.Rows.Add(
                    result.Name ?? string.Empty,
                    result.CalculationValueText ?? string.Empty,
                    result.JudgementText ?? string.Empty);
            }
        }

        private List<MultiImageJudgementResultRow> BuildMultiImageJudgementResults()
        {
            var rows = new List<MultiImageJudgementResultRow>();
            if (_judgementCriteriaRules == null || _judgementCriteriaRules.Count == 0)
            {
                return rows;
            }

            var lineMeasurements = BuildMultiImageConfirmLineMeasurements(GetMultiImageConfirmReferenceCandidate());
            if (lineMeasurements.Count == 0)
            {
                for (var i = 0; i < _judgementCriteriaRules.Count; i++)
                {
                    rows.Add(new MultiImageJudgementResultRow
                    {
                        Name = _judgementCriteriaRules[i]?.Name ?? string.Empty,
                        CalculationValueText = string.Empty,
                        JudgementText = "不可判斷"
                    });
                }

                return rows;
            }

            var values = new Dictionary<int, double>();
            for (var i = 0; i < lineMeasurements.Count; i++)
            {
                if (lineMeasurements[i] != null && lineMeasurements[i].IsValid)
                {
                    values[i + 1] = lineMeasurements[i].MillimeterDistance;
                }
            }

            foreach (var rule in _judgementCriteriaRules)
            {
                rows.Add(EvaluateJudgementRule(rule, values));
            }

            return rows;
        }

        private MultiImageJudgementResultRow EvaluateJudgementRule(JudgementCriterionRule rule, IReadOnlyDictionary<int, double> lineValues)
        {
            var row = new MultiImageJudgementResultRow
            {
                Name = rule?.Name ?? string.Empty
            };

            var calcValueA = EvaluateJudgementCalculation(
                rule == null ? null : rule.CalculationExpression,
                lineValues,
                out var calcTextA);
            var judgementA = EvaluateJudgementSpec(calcValueA, rule == null ? null : rule.SpecExpression);
            if (judgementA == JudgementSpecResult.Pass)
            {
                row.CalculationValueText = calcTextA;
                row.JudgementText = "A規";
                return row;
            }

            var hasBRule = !string.IsNullOrWhiteSpace(rule == null ? null : rule.CalculationExpressionB) ||
                !string.IsNullOrWhiteSpace(rule == null ? null : rule.SpecExpressionB);
            if (!hasBRule)
            {
                row.CalculationValueText = calcTextA;
                row.JudgementText = judgementA == JudgementSpecResult.Invalid ? "不可判斷" : "C規";
                return row;
            }

            var calcValueB = EvaluateJudgementCalculation(
                rule == null ? null : rule.CalculationExpressionB,
                lineValues,
                out var calcTextB);
            var judgementB = EvaluateJudgementSpec(calcValueB, rule == null ? null : rule.SpecExpressionB);
            row.CalculationValueText = string.Format("A:{0} / B:{1}", calcTextA, calcTextB);
            if (judgementB == JudgementSpecResult.Pass)
            {
                row.JudgementText = "B規";
                return row;
            }

            row.JudgementText =
                judgementA == JudgementSpecResult.Invalid || judgementB == JudgementSpecResult.Invalid
                    ? "不可判斷"
                    : "C規";
            return row;
        }

        private static double? EvaluateJudgementCalculation(string expression, IReadOnlyDictionary<int, double> lineValues, out string text)
        {
            text = string.Empty;
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            var missingValue = false;
            var resolved = Regex.Replace(expression, @"\((\d+)\)", match =>
            {
                var index = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                double value;
                if (lineValues != null && lineValues.TryGetValue(index, out value))
                {
                    return value.ToString(CultureInfo.InvariantCulture);
                }

                missingValue = true;
                return match.Value;
            });

            if (missingValue)
            {
                text = resolved;
                return null;
            }

            try
            {
                var table = new DataTable();
                var result = table.Compute(resolved, string.Empty);
                var numeric = Convert.ToDouble(result, CultureInfo.InvariantCulture);
                text = numeric.ToString("0.##", CultureInfo.InvariantCulture);
                return numeric;
            }
            catch
            {
                text = resolved;
                return null;
            }
        }

        private static JudgementSpecResult EvaluateJudgementSpec(double? value, string specExpression)
        {
            if (!value.HasValue)
            {
                return JudgementSpecResult.Invalid;
            }

            if (string.IsNullOrWhiteSpace(specExpression))
            {
                return JudgementSpecResult.Invalid;
            }

            var normalized = specExpression.Replace(" ", string.Empty);
            if (TryParseRangeSpec(normalized, value.Value, out var inRange))
            {
                return inRange ? JudgementSpecResult.Pass : JudgementSpecResult.Fail;
            }

            if (TryParseComparisonSpec(normalized, value.Value, out var pass))
            {
                return pass ? JudgementSpecResult.Pass : JudgementSpecResult.Fail;
            }

            return JudgementSpecResult.Invalid;
        }

        private static bool TryParseRangeSpec(string spec, double value, out bool pass)
        {
            pass = false;
            var ascendingMatch = Regex.Match(
                spec,
                @"^(-?\d+(?:\.\d+)?)(<=|<)x(<=|<)(-?\d+(?:\.\d+)?)$");
            if (ascendingMatch.Success)
            {
                var lower = double.Parse(ascendingMatch.Groups[1].Value, CultureInfo.InvariantCulture);
                var lowerOperator = ascendingMatch.Groups[2].Value == "<" ? ">" : ">=";
                var upperOperator = ascendingMatch.Groups[3].Value;
                var upper = double.Parse(ascendingMatch.Groups[4].Value, CultureInfo.InvariantCulture);

                pass = EvaluateComparison(value, lowerOperator, lower) &&
                    EvaluateComparison(value, upperOperator, upper);
                return true;
            }

            var descendingMatch = Regex.Match(
                spec,
                @"^(-?\d+(?:\.\d+)?)(>=|>)x(>=|>)(-?\d+(?:\.\d+)?)$");
            if (!descendingMatch.Success)
            {
                return false;
            }

            var upperBound = double.Parse(descendingMatch.Groups[1].Value, CultureInfo.InvariantCulture);
            var upperOperatorFromLeft = descendingMatch.Groups[2].Value == ">" ? "<" : "<=";
            var lowerOperatorFromRight = descendingMatch.Groups[3].Value == ">" ? ">" : ">=";
            var lowerBound = double.Parse(descendingMatch.Groups[4].Value, CultureInfo.InvariantCulture);

            pass = EvaluateComparison(value, upperOperatorFromLeft, upperBound) &&
                EvaluateComparison(value, lowerOperatorFromRight, lowerBound);
            return true;
        }

        private static bool TryParseComparisonSpec(string spec, double value, out bool pass)
        {
            pass = false;
            var directMatch = Regex.Match(spec, @"^x(<=|>=|<|>)(-?\d+(?:\.\d+)?)$");
            if (directMatch.Success)
            {
                var directOperator = directMatch.Groups[1].Value;
                var directTarget = double.Parse(directMatch.Groups[2].Value, CultureInfo.InvariantCulture);
                pass = EvaluateComparison(value, directOperator, directTarget);
                return true;
            }

            var reverseMatch = Regex.Match(spec, @"^(-?\d+(?:\.\d+)?)(<=|>=|<|>)x$");
            if (!reverseMatch.Success)
            {
                return false;
            }

            var reverseTarget = double.Parse(reverseMatch.Groups[1].Value, CultureInfo.InvariantCulture);
            var reverseOperator = ReverseComparisonOperator(reverseMatch.Groups[2].Value);
            pass = EvaluateComparison(value, reverseOperator, reverseTarget);
            return true;
        }

        private static bool EvaluateComparison(double value, string op, double target)
        {
            switch (op)
            {
                case "<":
                    return value < target;
                case "<=":
                    return value <= target;
                case ">":
                    return value > target;
                case ">=":
                    return value >= target;
                default:
                    return false;
            }
        }

        private static string ReverseComparisonOperator(string op)
        {
            switch (op)
            {
                case "<":
                    return ">";
                case "<=":
                    return ">=";
                case ">":
                    return "<";
                case ">=":
                    return "<=";
                default:
                    return op;
            }
        }

        private sealed class MultiImageJudgementResultRow
        {
            public string Name { get; set; }
            public string CalculationValueText { get; set; }
            public string JudgementText { get; set; }
        }

        private enum JudgementSpecResult
        {
            Invalid = 0,
            Pass = 1,
            Fail = 2
        }

        private void AddMultiImageInfoRow(string item, string value)
        {
            if (_dataGridViewMultiImageInfo == null)
            {
                return;
            }

            _dataGridViewMultiImageInfo.Rows.Add(item, value);
        }

        private bool AreAllMultiImageConfirmPointsInsideRoi(ReferenceCornerCandidate referenceCandidate)
        {
            if (referenceCandidate == null)
            {
                return false;
            }

            var productKey = string.IsNullOrWhiteSpace(_multiImageConfirmProductKey)
                ? GetCurrentProductKeyOrDefault()
                : _multiImageConfirmProductKey;
            var referenceSnapshot = GetReferenceCornerSnapshotForProduct(productKey);
            if (referenceSnapshot == null || !referenceSnapshot.RoiSaved)
            {
                return false;
            }

            var roi = ReferenceCornerSelectionService.NormalizeRectangle(referenceSnapshot.Roi);
            if (roi.Width <= 0 || roi.Height <= 0)
            {
                return false;
            }

            var points = new List<Point>
            {
                referenceCandidate.TopLeft,
                referenceCandidate.TopRight,
                referenceCandidate.CenterPoint
            };

            var records = GetMultiImageConfirmMeasureRecords(referenceCandidate);
            foreach (var record in records)
            {
                points.Add(record.StartPoint);
                points.Add(record.EndPoint);
                points.Add(record.CenterPoint);
                points.Add(record.ReferenceTopLeft);
                points.Add(record.ReferenceTopRight);
            }

            foreach (var point in points)
            {
                if (!IsPointInsideRectangleInclusive(point, roi))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsPointInsideRectangleInclusive(Point point, Rectangle rectangle)
        {
            return point.X >= rectangle.Left
                && point.X <= rectangle.Right
                && point.Y >= rectangle.Top
                && point.Y <= rectangle.Bottom;
        }

        private sealed class MultiImageLineMeasurementResult
        {
            public bool IsValid { get; set; }
            public Point StartPoint { get; set; }
            public Point EndPoint { get; set; }
            public double Distance { get; set; }
            public double MillimeterDistance { get; set; }

            public static MultiImageLineMeasurementResult Invalid()
            {
                return new MultiImageLineMeasurementResult();
            }
        }

        private List<MultiImageLineMeasurementResult> BuildMultiImageConfirmLineMeasurements(ReferenceCornerCandidate referenceCandidate)
        {
            var results = new List<MultiImageLineMeasurementResult>();
            var records = GetMultiImageConfirmMeasureRecords(referenceCandidate);
            if (records.Count == 0)
            {
                return results;
            }

            var imagePath = GetCurrentMultiImageConfirmImagePath();
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return results;
            }

            using (var sourceGray = LoadMultiImageConfirmGrayImage(imagePath))
            {
                if (sourceGray == null || sourceGray.Empty())
                {
                    return results;
                }

                for (var i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    var sourceIndex = GetMeasureSourceIndexFromName(record.SourceName);
                    var preprocessParam = TryGetMultiImageConfirmPreprocessParam(sourceIndex, out var param) ? param : null;
                    if (preprocessParam == null || !preprocessParam.Enabled)
                    {
                        results.Add(MultiImageLineMeasurementResult.Invalid());
                        continue;
                    }

                    using (var binary = PreprocessPipelineService.Build(sourceGray, preprocessParam))
                    {
                        results.Add(AnalyzeMultiImageLineMeasurement(
                            binary,
                            record.StartPoint,
                            record.EndPoint,
                            _innerSettings.CcdXPrecision,
                            _innerSettings.CcdYPrecision));
                    }
                }
            }

            return results;
        }

        private string GetCurrentMultiImageConfirmImagePath()
        {
            if (_multiImageConfirmImageIndex < 0 || _multiImageConfirmImageIndex >= _multiImageConfirmImagePaths.Count)
            {
                return null;
            }

            return _multiImageConfirmImagePaths[_multiImageConfirmImageIndex];
        }

        private OpenCvSharp.Mat LoadMultiImageConfirmGrayImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return null;
            }

            using (var sourceMat = Cv2.ImRead(imagePath, ImreadModes.Color))
            {
                if (sourceMat.Empty())
                {
                    return null;
                }

                var grayMat = new OpenCvSharp.Mat();
                if (sourceMat.Channels() == 1)
                {
                    sourceMat.CopyTo(grayMat);
                }
                else
                {
                    OpenCvSharp.Cv2.CvtColor(sourceMat, grayMat, OpenCvSharp.ColorConversionCodes.BGR2GRAY);
                }

                return grayMat;
            }
        }

        private static int GetMeasureSourceIndexFromName(string sourceName)
        {
            if (string.IsNullOrWhiteSpace(sourceName))
            {
                return -1;
            }

            if (sourceName.IndexOf("雙門檻", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return 4;
            }

            var match = Regex.Match(sourceName, @"\d+");
            int index;
            if (match.Success && int.TryParse(match.Value, out index))
            {
                return Math.Max(0, index - 1);
            }

            return -1;
        }

        private static MultiImageLineMeasurementResult AnalyzeMultiImageLineMeasurement(
            OpenCvSharp.Mat binaryMat,
            Point startPoint,
            Point endPoint,
            double ccdXPrecision,
            double ccdYPrecision)
        {
            if (binaryMat == null || binaryMat.Empty())
            {
                return MultiImageLineMeasurementResult.Invalid();
            }

            var samplePoints = SampleLinePoints(startPoint, endPoint);
            if (samplePoints.Count < 2)
            {
                return MultiImageLineMeasurementResult.Invalid();
            }

            var whiteThreshold = 127;
            var bestRunStart = -1;
            var bestRunLength = 0;
            var currentRunStart = -1;

            for (var i = 0; i < samplePoints.Count; i++)
            {
                var point = samplePoints[i];
                if (point.X < 0 || point.Y < 0 || point.X >= binaryMat.Width || point.Y >= binaryMat.Height)
                {
                    if (currentRunStart >= 0)
                    {
                        var currentRunLength = i - currentRunStart;
                        if (currentRunLength > bestRunLength)
                        {
                            bestRunStart = currentRunStart;
                            bestRunLength = currentRunLength;
                        }
                        currentRunStart = -1;
                    }
                    continue;
                }

                var isWhite = binaryMat.At<byte>(point.Y, point.X) > whiteThreshold;
                if (isWhite)
                {
                    if (currentRunStart < 0)
                    {
                        currentRunStart = i;
                    }
                }
                else if (currentRunStart >= 0)
                {
                    var currentRunLength = i - currentRunStart;
                    if (currentRunLength > bestRunLength)
                    {
                        bestRunStart = currentRunStart;
                        bestRunLength = currentRunLength;
                    }
                    currentRunStart = -1;
                }
            }

            if (currentRunStart >= 0)
            {
                var currentRunLength = samplePoints.Count - currentRunStart;
                if (currentRunLength > bestRunLength)
                {
                    bestRunStart = currentRunStart;
                    bestRunLength = currentRunLength;
                }
            }

            if (bestRunStart < 0 || bestRunLength < 2)
            {
                return MultiImageLineMeasurementResult.Invalid();
            }

            var firstPoint = samplePoints[bestRunStart];
            var lastWhitePoint = samplePoints[bestRunStart + bestRunLength - 1];
            var endPointIndex = bestRunStart + bestRunLength;
            var lastPoint = endPointIndex < samplePoints.Count
                ? samplePoints[endPointIndex]
                : lastWhitePoint;
            var deltaX = lastPoint.X - firstPoint.X;
            var deltaY = lastPoint.Y - firstPoint.Y;
            var distance = Math.Sqrt(
                Math.Pow(deltaX, 2) +
                Math.Pow(deltaY, 2));
            var millimeterDistance = Math.Sqrt(
                Math.Pow(deltaX * ccdXPrecision, 2) +
                Math.Pow(deltaY * ccdYPrecision, 2));
            return new MultiImageLineMeasurementResult
            {
                IsValid = true,
                StartPoint = firstPoint,
                EndPoint = lastPoint,
                Distance = distance,
                MillimeterDistance = millimeterDistance
            };
        }

        private static List<Point> SampleLinePoints(Point startPoint, Point endPoint)
        {
            var points = new List<Point>();
            var dx = Math.Abs(endPoint.X - startPoint.X);
            var dy = Math.Abs(endPoint.Y - startPoint.Y);
            var sx = startPoint.X < endPoint.X ? 1 : -1;
            var sy = startPoint.Y < endPoint.Y ? 1 : -1;
            var err = dx - dy;
            var current = startPoint;

            while (true)
            {
                points.Add(current);
                if (current == endPoint)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    current = new Point(current.X + sx, current.Y);
                }

                if (e2 < dx)
                {
                    err += dx;
                    current = new Point(current.X, current.Y + sy);
                }
            }

            return points;
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

            SetMultiImageConfirmBitmap(bitmap, bitmap.Size, true);
        }

        private bool TryGetMultiImageConfirmPreprocessParam(int preprocessIndex, out PreprocessParam preprocessParam)
        {
            preprocessParam = null;
            if (preprocessIndex == 4)
            {
                var dualSnapshot = CaptureDualThresholdSnapshot();
                preprocessParam = new PreprocessParam
                {
                    Enabled = dualSnapshot.Enabled,
                    WhiteObject = true,
                    Threshold = dualSnapshot.LowerThreshold,
                    UpperThreshold = dualSnapshot.UpperThreshold,
                    UseDualThreshold = true,
                    ErodeIterations = dualSnapshot.ErodeIterations,
                    DilateIterations = dualSnapshot.DilateIterations,
                    OpenIterations = dualSnapshot.OpenIterations,
                    CloseIterations = dualSnapshot.CloseIterations
                };
                return true;
            }

            if (preprocessIndex < 0 || preprocessIndex > 3)
            {
                return false;
            }

            var productKey = string.IsNullOrWhiteSpace(_multiImageConfirmProductKey)
                ? GetCurrentProductKeyOrDefault()
                : _multiImageConfirmProductKey;
            var snapshots = GetPreprocessSnapshotsForProduct(productKey);
            if (snapshots == null || preprocessIndex >= snapshots.Length)
            {
                return false;
            }

            var snapshot = snapshots[preprocessIndex];
            if (snapshot == null)
            {
                return false;
            }

            preprocessParam = new PreprocessParam
            {
                Enabled = snapshot.Enabled,
                WhiteObject = preprocessIndex < 2,
                Threshold = snapshot.Threshold,
                ErodeIterations = snapshot.ErodeIterations,
                DilateIterations = snapshot.DilateIterations,
                OpenIterations = snapshot.OpenIterations,
                CloseIterations = snapshot.CloseIterations
            };
            return true;
        }

        private void ShowMultiImageConfirmPreprocessImage(string imagePath, int preprocessIndex)
        {
            if (_pictureBoxMultiImageConfirm == null || string.IsNullOrWhiteSpace(imagePath))
            {
                return;
            }

            if (preprocessIndex < 0 || preprocessIndex > 4)
            {
                preprocessIndex = 0;
            }

            PreprocessParam preprocessParam;
            if (!TryGetMultiImageConfirmPreprocessParam(preprocessIndex, out preprocessParam))
            {
                MessageBox.Show(this, "找不到此產品的前處理參數。", "多影像確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap bitmap = null;
            try
            {
                using (var sourceMat = Cv2.ImRead(imagePath, ImreadModes.Color))
                {
                    if (sourceMat.Empty())
                    {
                        return;
                    }

                    _multiImageConfirmSourceImageSize = new Size(sourceMat.Width, sourceMat.Height);
                    using (var grayMat = new CvMat())
                    {
                        Cv2.CvtColor(sourceMat, grayMat, ColorConversionCodes.BGR2GRAY);
                        using (var processedMat = PreprocessPipelineService.Build(grayMat, preprocessParam))
                        {
                            if (processedMat == null || processedMat.Empty())
                            {
                                return;
                            }

                            bitmap = BitmapConverter.ToBitmap(processedMat);
                        }
                    }
                }
            }
            catch
            {
                bitmap?.Dispose();
                MessageBox.Show(this, "無法讀取前處理影像。", "多影像確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetMultiImageConfirmBitmap(bitmap, _multiImageConfirmSourceImageSize, true);
        }

        private void SetMultiImageConfirmBitmap(Bitmap bitmap, Size sourceImageSize, bool preserveView)
        {
            if (bitmap == null)
            {
                return;
            }

            var hasExistingView =
                preserveView &&
                _multiImageConfirmBitmap != null &&
                _panelMultiImageConfirmViewport != null &&
                _multiImageConfirmImageScale > 0f &&
                _multiImageConfirmFitScale > 0f;

            var viewportCenter = PointF.Empty;
            var zoomRatio = 1f;
            if (hasExistingView)
            {
                var viewportMidX = _panelMultiImageConfirmViewport.ClientSize.Width / 2f;
                var viewportMidY = _panelMultiImageConfirmViewport.ClientSize.Height / 2f;
                viewportCenter = new PointF(
                    (viewportMidX - _multiImageConfirmOffsetX) / _multiImageConfirmImageScale,
                    (viewportMidY - _multiImageConfirmOffsetY) / _multiImageConfirmImageScale);
                zoomRatio = _multiImageConfirmImageScale / _multiImageConfirmFitScale;
            }

            _multiImageConfirmBitmap?.Dispose();
            _multiImageConfirmBitmap = bitmap;
            _multiImageConfirmSourceImageSize = sourceImageSize.Width > 0 && sourceImageSize.Height > 0
                ? sourceImageSize
                : bitmap.Size;
            InvalidateMultiImageLineMeasurementCache();
            _pictureBoxMultiImageConfirm.Image = null;

            if (!hasExistingView)
            {
                FitMultiImageConfirmToViewport();
                _panelMultiImageConfirmViewport.Invalidate();
                return;
            }

            var panelWidth = Math.Max(1, _panelMultiImageConfirmViewport.ClientSize.Width);
            var panelHeight = Math.Max(1, _panelMultiImageConfirmViewport.ClientSize.Height);
            var scaleX = (float)panelWidth / _multiImageConfirmBitmap.Width;
            var scaleY = (float)panelHeight / _multiImageConfirmBitmap.Height;
            _multiImageConfirmFitScale = Math.Max(0.01f, Math.Min(scaleX, scaleY));

            var minimumScale = _multiImageConfirmFitScale * 0.25f;
            var maximumScale = _multiImageConfirmFitScale * 20f;
            _multiImageConfirmImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _multiImageConfirmFitScale * zoomRatio));

            var viewportMidpointX = _panelMultiImageConfirmViewport.ClientSize.Width / 2f;
            var viewportMidpointY = _panelMultiImageConfirmViewport.ClientSize.Height / 2f;
            _multiImageConfirmOffsetX = viewportMidpointX - viewportCenter.X * _multiImageConfirmImageScale;
            _multiImageConfirmOffsetY = viewportMidpointY - viewportCenter.Y * _multiImageConfirmImageScale;
            _multiImageConfirmPanning = false;
            ConstrainMultiImageConfirmImagePosition();
            _panelMultiImageConfirmViewport.Invalidate();
        }

        private Size GetMultiImageConfirmOverlayImageSize()
        {
            if (_multiImageConfirmSourceImageSize.Width > 0 && _multiImageConfirmSourceImageSize.Height > 0)
            {
                return _multiImageConfirmSourceImageSize;
            }

            return _multiImageConfirmBitmap == null ? Size.Empty : _multiImageConfirmBitmap.Size;
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

        private void MultiImageLineSequence_Click(object sender, EventArgs e)
        {
            if (_multiImageConfirmImagePaths.Count == 0)
            {
                return;
            }

            _multiImageLineSequenceVisible = true;
            _multiImageLineSequenceRemainingTicks = 17;
            _multiImageLineSequenceTimer.Stop();
            _multiImageLineSequenceTimer.Start();
            _panelMultiImageConfirmViewport?.Invalidate();
        }

        private void MultiImageLineDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_comboBoxMultiImageLineDisplayMode == null)
            {
                return;
            }

            _multiImageLineDisplayMode = (MultiImageLineDisplayMode)Math.Max(0, Math.Min(2, _comboBoxMultiImageLineDisplayMode.SelectedIndex));
            _panelMultiImageConfirmViewport?.Invalidate();
        }

        private void InvalidateMultiImageLineMeasurementCache()
        {
            _multiImageLineMeasurementCache.Clear();
        }

        private void MultiImageLineSequenceTimer_Tick(object sender, EventArgs e)
        {
            if (_multiImageLineSequenceRemainingTicks > 0)
            {
                _multiImageLineSequenceRemainingTicks--;
                _panelMultiImageConfirmViewport?.Invalidate();
                return;
            }

            _multiImageLineSequenceTimer.Stop();
            _multiImageLineSequenceVisible = false;
            _panelMultiImageConfirmViewport?.Invalidate();
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

            for (var i = 0; i < measureRecords.Count; i++)
            {
                var record = measureRecords[i];
                var color = MeasurementOverlayService.GetSourceColor(record.SourceName);
                using (var pen = new Pen(color, 2f))
                using (var brush = new SolidBrush(color))
                {
                    if (_multiImageLineDisplayMode == MultiImageLineDisplayMode.Hidden)
                    {
                        continue;
                    }

                    Point startPoint;
                    Point endPoint;
                    if (_multiImageLineDisplayMode == MultiImageLineDisplayMode.SourceLines)
                    {
                        startPoint = GetMultiImageConfirmDisplayPoint(record.StartPoint, imageRect);
                        endPoint = GetMultiImageConfirmDisplayPoint(record.EndPoint, imageRect);
                        MeasurementOverlayService.DrawMeasureRecord(e.Graphics, pen, brush, startPoint, endPoint);
                    }
                    else
                    {
                        var lineResult = GetCachedMultiImageLineMeasurement(record);
                        if (lineResult == null || !lineResult.IsValid)
                        {
                            continue;
                        }

                        startPoint = GetMultiImageConfirmDisplayPoint(lineResult.StartPoint, imageRect);
                        endPoint = GetMultiImageConfirmDisplayPoint(lineResult.EndPoint, imageRect);
                        MeasurementOverlayService.DrawMeasureRecord(e.Graphics, pen, brush, startPoint, endPoint);
                    }

                    if (_multiImageLineSequenceVisible && _multiImageLineDisplayMode != MultiImageLineDisplayMode.Hidden)
                    {
                        DrawMultiImageLineSequenceLabel(e.Graphics, i + 1, startPoint);
                    }
                }
            }
        }

        private void DrawMultiImageConfirmedLineMeasurement(Graphics graphics, MeasureRecord record, Rectangle imageRect)
        {
            if (graphics == null || record == null || imageRect == Rectangle.Empty)
            {
                return;
            }

            var lineResult = GetCachedMultiImageLineMeasurement(record);
            if (lineResult == null || !lineResult.IsValid)
            {
                return;
            }

            var startPoint = GetMultiImageConfirmDisplayPoint(lineResult.StartPoint, imageRect);
            var endPoint = GetMultiImageConfirmDisplayPoint(lineResult.EndPoint, imageRect);
            var color = Color.LimeGreen;
            using (var pen = new Pen(color, 2f))
            using (var brush = new SolidBrush(color))
            {
                MeasurementOverlayService.DrawMeasureRecord(graphics, pen, brush, startPoint, endPoint);
            }
        }

        private MultiImageLineMeasurementResult GetCachedMultiImageLineMeasurement(MeasureRecord record)
        {
            if (record == null)
            {
                return null;
            }

            var imagePath = GetCurrentMultiImageConfirmImagePath();
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return null;
            }

            var cacheKey = string.Format(
                "{0}|{1}|{2}|{3},{4}|{5},{6}",
                imagePath,
                (int)_multiImageLineDisplayMode,
                record.SourceName ?? string.Empty,
                record.StartPoint.X,
                record.StartPoint.Y,
                record.EndPoint.X,
                record.EndPoint.Y);

            List<MultiImageLineMeasurementResult> cached;
            if (_multiImageLineMeasurementCache.TryGetValue(cacheKey, out cached) && cached.Count > 0)
            {
                return cached[0];
            }

            var result = AnalyzeMultiImageLineMeasurementForCurrentRecord(record);
            _multiImageLineMeasurementCache[cacheKey] = new List<MultiImageLineMeasurementResult> { result };
            return result;
        }

        private MultiImageLineMeasurementResult AnalyzeMultiImageLineMeasurementForCurrentRecord(MeasureRecord record)
        {
            if (record == null)
            {
                return null;
            }

            var imagePath = GetCurrentMultiImageConfirmImagePath();
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return null;
            }

            var sourceIndex = GetMeasureSourceIndexFromName(record.SourceName);
            if (sourceIndex < 0)
            {
                return null;
            }

            PreprocessParam preprocessParam;
            if (!TryGetMultiImageConfirmPreprocessParam(sourceIndex, out preprocessParam) || preprocessParam == null || !preprocessParam.Enabled)
            {
                return null;
            }

            using (var sourceGray = LoadMultiImageConfirmGrayImage(imagePath))
            {
                if (sourceGray == null || sourceGray.Empty())
                {
                    return null;
                }

                using (var binary = PreprocessPipelineService.Build(sourceGray, preprocessParam))
                {
                    return AnalyzeMultiImageLineMeasurement(
                        binary,
                        record.StartPoint,
                        record.EndPoint,
                        _innerSettings.CcdXPrecision,
                        _innerSettings.CcdYPrecision);
                }
            }
        }

        private void DrawMultiImageLineSequenceLabel(Graphics graphics, int index, Point startPoint)
        {
            if (graphics == null || index <= 0)
            {
                return;
            }

            using (var font = new Font("Microsoft JhengHei UI", 11f, FontStyle.Bold))
            using (var backgroundBrush = new SolidBrush(Color.FromArgb(210, Color.White)))
            using (var textBrush = new SolidBrush(Color.Black))
            using (var borderPen = new Pen(Color.Black, 1f))
            {
                var text = index.ToString();
                var size = graphics.MeasureString(text, font);
                var rect = new Rectangle(
                    startPoint.X + 6,
                    startPoint.Y - (int)Math.Ceiling(size.Height) - 4,
                    (int)Math.Ceiling(size.Width) + 8,
                    (int)Math.Ceiling(size.Height) + 4);
                graphics.FillRectangle(backgroundBrush, rect);
                graphics.DrawRectangle(borderPen, rect);
                graphics.DrawString(text, font, textBrush, rect.X + 4, rect.Y + 1);
            }
        }

        private List<MeasureRecord> GetMultiImageConfirmMeasureRecords(ReferenceCornerCandidate referenceCandidate)
        {
            var productKey = string.IsNullOrWhiteSpace(_multiImageConfirmProductKey)
                ? GetCurrentProductKeyOrDefault()
                : _multiImageConfirmProductKey;

            List<MeasureRecord> records = null;
            if (!string.IsNullOrWhiteSpace(productKey) &&
                string.Equals(productKey, GetCurrentProductKeyOrDefault(), StringComparison.OrdinalIgnoreCase) &&
                _measureRecords.Count > 0)
            {
                records = CloneMeasureRecords(_measureRecords);
            }
            else if (!string.IsNullOrWhiteSpace(productKey))
            {
                List<MeasureRecord> profileRecords;
                if (_measureProfiles.TryGetValue(productKey, out profileRecords) && profileRecords.Count > 0)
                {
                    records = CloneMeasureRecords(profileRecords);
                }
            }

            if ((records == null || records.Count == 0) && _measureRecords.Count > 0)
            {
                records = CloneMeasureRecords(_measureRecords);
            }

            if (records == null || records.Count == 0)
            {
                return new List<MeasureRecord>();
            }

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
            if (_multiImageConfirmBitmap == null ||
                _multiImageConfirmImageIndex < 0 ||
                _multiImageConfirmImageIndex >= _multiImageConfirmImagePaths.Count)
            {
                return null;
            }

            using (var sourceMat = Cv2.ImRead(_multiImageConfirmImagePaths[_multiImageConfirmImageIndex], ImreadModes.Color))
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

            var productKey = string.IsNullOrWhiteSpace(_multiImageConfirmProductKey)
                ? GetCurrentProductKeyOrDefault()
                : _multiImageConfirmProductKey;
            var referenceSnapshot = GetReferenceCornerSnapshotForProduct(productKey);
            if (referenceSnapshot == null || !referenceSnapshot.Enabled)
            {
                return null;
            }

            return BuildMultiImageConfirmReferenceBinary(grayMat, productKey, referenceSnapshot.SourceIndex);
        }

        private Rectangle GetMultiImageConfirmReferenceRoi(OpenCvSharp.Size imageSize)
        {
            var productKey = string.IsNullOrWhiteSpace(_multiImageConfirmProductKey)
                ? GetCurrentProductKeyOrDefault()
                : _multiImageConfirmProductKey;
            var referenceSnapshot = GetReferenceCornerSnapshotForProduct(productKey);
            if (referenceSnapshot == null || !referenceSnapshot.RoiSaved)
            {
                return Rectangle.Empty;
            }

            var roi = ReferenceCornerSelectionService.NormalizeRectangle(referenceSnapshot.Roi);
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

        private OpenCvSharp.Mat BuildMultiImageConfirmReferenceBinary(OpenCvSharp.Mat grayMat, string productKey, int referenceSourceIndex)
        {
            var snapshots = GetPreprocessSnapshotsForProduct(productKey);
            if (snapshots == null || referenceSourceIndex < 0 || referenceSourceIndex >= snapshots.Length)
            {
                return null;
            }

            var snapshot = snapshots[referenceSourceIndex];
            if (snapshot == null || !snapshot.Enabled)
            {
                return null;
            }

            var preprocessParam = new PreprocessParam
            {
                Enabled = snapshot.Enabled,
                WhiteObject = referenceSourceIndex < 2,
                Threshold = snapshot.Threshold,
                ErodeIterations = snapshot.ErodeIterations,
                DilateIterations = snapshot.DilateIterations,
                OpenIterations = snapshot.OpenIterations,
                CloseIterations = snapshot.CloseIterations
            };
            return PreprocessPipelineService.Build(grayMat, preprocessParam);
        }

        private void DrawMultiImageConfirmReferenceRoi(Graphics graphics, Rectangle imageRect)
        {
            if (graphics == null || _multiImageConfirmBitmap == null)
            {
                return;
            }

            var overlayImageSize = GetMultiImageConfirmOverlayImageSize();
            var roi = GetMultiImageConfirmReferenceRoi(new OpenCvSharp.Size(overlayImageSize.Width, overlayImageSize.Height));
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

            return MeasurementOverlayService.ToDisplayPoint(imagePoint, imageRect, GetMultiImageConfirmOverlayImageSize());
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
            if (_comboBoxMeasureSource == null)
            {
                return pictureBoxImage.Image == null ? null : new Bitmap(pictureBoxImage.Image);
            }

            var index = _comboBoxMeasureSource.SelectedIndex;
            if (index == 4 && _pictureBoxDualThresholdPreview != null && _pictureBoxDualThresholdPreview.Image != null)
            {
                return new Bitmap(_pictureBoxDualThresholdPreview.Image);
            }

            if (_preprocessPictureBoxes == null)
            {
                return pictureBoxImage.Image == null ? null : new Bitmap(pictureBoxImage.Image);
            }

            if (index >= 0 && index < _preprocessPictureBoxes.Length && _preprocessPictureBoxes[index].Image != null)
            {
                return new Bitmap(_preprocessPictureBoxes[index].Image);
            }

            return pictureBoxImage.Image == null ? null : new Bitmap(pictureBoxImage.Image);
        }

        private bool IsSelectedMeasureSourceAvailable()
        {
            if (_comboBoxMeasureSource == null)
            {
                return false;
            }

            var index = _comboBoxMeasureSource.SelectedIndex;
            if (index == 4)
            {
                return _pictureBoxDualThresholdPreview != null && _pictureBoxDualThresholdPreview.Image != null;
            }

            if (_preprocessPictureBoxes == null)
            {
                return false;
            }

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
                _buttonParallelMeasure.Enabled = _measureSourceAvailable && _referenceCornerFound && _referenceCornerCandidate != null && !_isEditingMeasureRecord;
            }

            if (_buttonPerpendicularMeasure != null)
            {
                _buttonPerpendicularMeasure.Enabled = _measureSourceAvailable && _referenceCornerFound && _referenceCornerCandidate != null && !_isEditingMeasureRecord;
            }

            if (_labelMeasureStatus != null)
            {
                _labelMeasureStatus.Text = _isEditingMeasureRecord
                    ? "請重新點選兩個點，完成後按確認修改，取消則不變更"
                    : (_measureSourceAvailable
                        ? "點兩下影像建立兩點量測，保存後寫入表格"
                        : "目前沒有可用的前處理影像，無法設定量測點");
            }

            UpdateMeasureDirectionButtons();
        }

        private void PictureBoxMeasurePreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_measureSourceAvailable && !_isEditingMeasureRecord)
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
                _labelMeasureStatus.Text = _isEditingMeasureRecord ? "請再點第二個點，完成後按確認修改" : "請再點第二個點";
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
            if (_isEditingMeasureRecord)
            {
                ConfirmMeasureRecordEdit();
                return;
            }

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
            if (_isEditingMeasureRecord)
            {
                CancelMeasureRecordEdit();
                return;
            }

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
            if (_isEditingMeasureRecord)
            {
                CancelMeasureRecordEdit();
            }
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
            var selectedRow = GetSelectedMeasureRecordRow();
            if (selectedRow == null)
            {
                return;
            }

            var confirm = MessageBox.Show(
                this,
                "刪除這條量測線段會影響後續計算與已保存的量測結果，確定要刪除嗎？",
                "刪除量測線段",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            if (selectedRow.Tag is MeasureRecord record)
            {
                if (ReferenceEquals(_measureBlinkRecord, record))
                {
                    _measureBlinkTimer.Stop();
                    _measureBlinkRecord = null;
                    _measureBlinkRemainingTicks = 0;
                }
                _measureRecords.Remove(record);
            }
            if (!selectedRow.IsNewRow)
            {
                _dataGridViewMeasureRecords.Rows.Remove(selectedRow);
            }

            ReindexMeasureRows();
            RefreshMeasureDistancePreview();
            UpdateMeasureSourceAvailability();
            SaveCurrentAppSettings();
        }

        private void MeasureEditMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRow = GetSelectedMeasureRecordRow();
            if (selectedRow == null)
            {
                return;
            }

            if (!(selectedRow.Tag is MeasureRecord record))
            {
                return;
            }

            using (var dialog = new MeasureDirectionDialog(_measureDirectionMode))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                BeginMeasureRecordEdit(record, selectedRow, dialog.SelectedMode);
            }
        }

        private void BeginMeasureRecordEdit(MeasureRecord record, DataGridViewRow row, MeasureDirectionMode mode)
        {
            if (record == null || row == null)
            {
                return;
            }

            _editingMeasureRecord = record;
            _editingMeasureRow = row;
            _isEditingMeasureRecord = true;
            _measureDirectionMode = mode;
            _measurePoints.Clear();
            _isMeasureSelecting = false;
            _buttonSaveMeasurePoint.Text = "確認修改";
            _buttonClearMeasurePoint.Text = "取消修改";
            _labelMeasureStatus.Text = "請重新點選兩個點，完成後按確認修改，取消則不變更";
            _pictureBoxMeasurePreview.Invalidate();
            UpdateMeasureDirectionButtons();
        }

        private void ConfirmMeasureRecordEdit()
        {
            if (!_isEditingMeasureRecord || _editingMeasureRecord == null || _editingMeasureRow == null)
            {
                return;
            }

            if (_measurePoints.Count < 2)
            {
                MessageBox.Show(this, "請先重新點選兩個點。", "量測距離", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_referenceCornerCandidate == null)
            {
                MessageBox.Show(this, "請先完成左上角與右上角基準的辨識，才能修改量測資料。", "量測距離", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var updatedRecord = MeasurementRecordService.CreateRecord(
                _measurePoints[0],
                _measurePoints[1],
                _referenceCornerCandidate,
                _editingMeasureRecord.SourceName,
                GetMeasureDirectionName());

            var listIndex = _measureRecords.IndexOf(_editingMeasureRecord);
            if (listIndex >= 0)
            {
                _measureRecords[listIndex] = updatedRecord;
            }

            _editingMeasureRow.Tag = updatedRecord;
            UpdateMeasureRecordRow(_editingMeasureRow, updatedRecord);

            if (ReferenceEquals(_measureBlinkRecord, _editingMeasureRecord))
            {
                _measureBlinkRecord = updatedRecord;
            }

            ExitMeasureRecordEdit("已修改量測線段。");
            RefreshMeasureDistancePreview();
            UpdateMeasureSourceAvailability();
            SaveCurrentAppSettings();
        }

        private void CancelMeasureRecordEdit()
        {
            if (!_isEditingMeasureRecord)
            {
                return;
            }

            ExitMeasureRecordEdit("已取消修改。");
        }

        private void ExitMeasureRecordEdit(string statusText)
        {
            _isEditingMeasureRecord = false;
            _editingMeasureRecord = null;
            _editingMeasureRow = null;
            _measurePoints.Clear();
            _isMeasureSelecting = false;
            _buttonSaveMeasurePoint.Text = "保存量測點";
            _buttonClearMeasurePoint.Text = "清除量測點";
            _labelMeasureStatus.Text = statusText;
            _pictureBoxMeasurePreview.Invalidate();
            UpdateMeasureDirectionButtons();
        }

        private void ClearAllMeasureRecords()
        {
            if (_isEditingMeasureRecord)
            {
                CancelMeasureRecordEdit();
            }
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
            if (_isEditingMeasureRecord)
            {
                CancelMeasureRecordEdit();
            }
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

            if (_comboBoxMeasureSource.SelectedIndex == 4)
            {
                return "雙門檻";
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

        private void UpdateMeasureRecordRow(DataGridViewRow row, MeasureRecord record)
        {
            if (row == null || record == null)
            {
                return;
            }

            row.Cells[1].Value = record.StartPoint.X;
            row.Cells[2].Value = record.StartPoint.Y;
            row.Cells[3].Value = record.EndPoint.X;
            row.Cells[4].Value = record.EndPoint.Y;
            row.Cells[5].Value = record.ReferenceTopLeft.X;
            row.Cells[6].Value = record.ReferenceTopLeft.Y;
            row.Cells[7].Value = record.ReferenceTopRight.X;
            row.Cells[8].Value = record.ReferenceTopRight.Y;
            row.Cells[9].Value = record.ReferenceLength.ToString("0.###", CultureInfo.InvariantCulture);
            row.Cells[10].Value = record.CenterPoint.X;
            row.Cells[11].Value = record.CenterPoint.Y;
            row.Cells[12].Value = record.LocalStartPoint.X.ToString("0.###", CultureInfo.InvariantCulture);
            row.Cells[13].Value = record.LocalStartPoint.Y.ToString("0.###", CultureInfo.InvariantCulture);
            row.Cells[14].Value = record.LocalEndPoint.X.ToString("0.###", CultureInfo.InvariantCulture);
            row.Cells[15].Value = record.LocalEndPoint.Y.ToString("0.###", CultureInfo.InvariantCulture);
            row.Cells[16].Value = record.Distance.ToString("0.##");
            row.Cells[17].Value = record.SourceName;
            row.Cells[18].Value = record.DirectionName;
        }

        private DataGridViewRow GetSelectedMeasureRecordRow()
        {
            if (_dataGridViewMeasureRecords == null || _dataGridViewMeasureRecords.SelectedRows.Count == 0)
            {
                return null;
            }

            return _dataGridViewMeasureRecords.SelectedRows[0];
        }
    }
}
