using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public sealed class ZoomablePictureBox : Control
    {
        private Image _image;
        private float _scale = 1f;
        private float _fitScale = 1f;
        private float _offsetX;
        private float _offsetY;
        private bool _isDragging;
        private Point _lastMousePosition;

        public ZoomablePictureBox()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);

            BackColor = Color.FromArgb(239, 241, 243);
            Cursor = Cursors.Default;
            TabStop = true;
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                FitImage();
            }
        }

        public void FitImage()
        {
            if (_image == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
            {
                _scale = 1f;
                _fitScale = 1f;
                _offsetX = 0f;
                _offsetY = 0f;
                Invalidate();
                return;
            }

            var scaleX = ClientSize.Width / (float)_image.Width;
            var scaleY = ClientSize.Height / (float)_image.Height;
            _fitScale = Math.Min(scaleX, scaleY);
            _scale = _fitScale;
            CenterImage();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            if (_image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var width = _image.Width * _scale;
                var height = _image.Height * _scale;
                e.Graphics.DrawImage(_image, _offsetX, _offsetY, width, height);
            }

            using (var borderPen = new Pen(Color.FromArgb(120, 124, 128)))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Focus();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_image == null)
            {
                return;
            }

            var imageX = (e.X - _offsetX) / _scale;
            var imageY = (e.Y - _offsetY) / _scale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minScale = _fitScale * 0.25f;
            var maxScale = _fitScale * 20f;
            var newScale = Math.Max(minScale, Math.Min(maxScale, _scale * zoomFactor));

            _scale = newScale;
            _offsetX = e.X - imageX * _scale;
            _offsetY = e.Y - imageY * _scale;
            ConstrainOffset();
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || _image == null)
            {
                return;
            }

            Focus();
            _isDragging = true;
            _lastMousePosition = e.Location;
            Cursor = Cursors.SizeAll;
            Capture = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_isDragging)
            {
                return;
            }

            _offsetX += e.X - _lastMousePosition.X;
            _offsetY += e.Y - _lastMousePosition.Y;
            _lastMousePosition = e.Location;
            ConstrainOffset();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isDragging = false;
            Cursor = Cursors.Default;
            Capture = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_image != null)
            {
                FitImage();
            }
        }

        private void CenterImage()
        {
            _offsetX = (ClientSize.Width - _image.Width * _scale) / 2f;
            _offsetY = (ClientSize.Height - _image.Height * _scale) / 2f;
        }

        private void ConstrainOffset()
        {
            var imageWidth = _image.Width * _scale;
            var imageHeight = _image.Height * _scale;

            _offsetX = imageWidth <= ClientSize.Width
                ? (ClientSize.Width - imageWidth) / 2f
                : Math.Max(ClientSize.Width - imageWidth, Math.Min(0f, _offsetX));

            _offsetY = imageHeight <= ClientSize.Height
                ? (ClientSize.Height - imageHeight) / 2f
                : Math.Max(ClientSize.Height - imageHeight, Math.Min(0f, _offsetY));
        }
    }
}
