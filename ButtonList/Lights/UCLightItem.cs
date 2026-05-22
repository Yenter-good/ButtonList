using ButtonList.Helper;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace ButtonList.Lights
{
    public partial class UCLightItem : PictureBox
    {
        public UCLightItem()
        {
            InitializeComponent();

        }

        private Graphics _graphics;
        private Bitmap _pressBitmap;
        private Bitmap _normalBitmap;
        private Bitmap _idleBitmap;

        private Rectangle _destRect = new Rectangle();
        private Rectangle _sourceRect = new Rectangle();
        private bool _isPress;
        private bool _inited;


        public int ButtonSize { get; set; } = 100;

        public Bitmap Icon { get; set; }

        public Bitmap PressImage { get; set; }

        public Bitmap NormalImage { get; set; }

        internal InternalLightData LightData { get; set; }

        public event EventHandler PopMenu;
        public event EventHandler<LightStateChangedArgs> LightStateChanged;
        public LightStateChangedArgs _args;

        public bool IsPress
        {
            get => _isPress; set
            {
                _isPress = value;
                if (value)
                    this.Image = _pressBitmap;
                else
                    this.Image = _normalBitmap;
            }
        }

        internal void Init(InternalLightData lightData = null)
        {
            this._inited = true;

            this.DoubleBuffered = true;
            this.LightData = lightData;
            this._isPress = false;

            this.Size = new Size(ButtonSize, ButtonSize);
            _pressBitmap = new Bitmap(ButtonSize, ButtonSize);
            _normalBitmap = new Bitmap(ButtonSize, ButtonSize);
            _idleBitmap = new Bitmap(ButtonSize, ButtonSize);

            if (lightData != null)
                this.Icon = lightData.IconEntry.Icon;

            this.Draw();
            this.MouseDown -= LightItem_MouseDown;
            this.MouseDown += LightItem_MouseDown;
            this.MouseLeave -= UCLightItem_MouseLeave;
            this.MouseLeave += UCLightItem_MouseLeave;
            this.MouseEnter -= UCLightItem_MouseEnter;
            this.MouseEnter += UCLightItem_MouseEnter;
            if (LightData == null)
                this.Image = _idleBitmap;
            else
                this.Image = _normalBitmap;

            _args = new LightStateChangedArgs();
            this._inited = false;
        }

        private void UCLightItem_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.Hide(this);
        }

        private void UCLightItem_MouseEnter(object sender, EventArgs e)
        {
            if (this.LightData != null)
                this.toolTip1.Show(this.LightData.Tooltip, this, 3);
        }

        private void LightItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == MouseButtons.Left)
            {
                if (LightData == null)
                {
                    this.Image = _idleBitmap;
                    return;
                }
                _isPress = !_isPress;

                if (IsPress)
                    this.Image = _pressBitmap;
                else
                    this.Image = _normalBitmap;

                _args.Index = (int)this.Tag;
                _args.Type = this.LightData.Type;
                if (_args.Type == LightDataType.Serial)
                    _args.IsPositiveControl = this.LightData.IsPositiveControl;
                else
                    _args.CanValue = this.LightData.CanData;
                _args.IsActivated = _isPress;
                this.LightData.IsActivated = _isPress;

                if (!_inited)
                    LightStateChanged?.Invoke(this, _args);

            }
            else if (e.Button == MouseButtons.Right)
            {
                PopMenu?.Invoke(this, null);
            }
        }

        private void Draw()
        {
            if (this.LightData != null)
                DrawIcon();
            else
                DrawIdle();
        }

        private void DrawIdle()
        {
            _destRect.X = 0;
            _destRect.Y = 0;
            _destRect.Width = 0;
            _destRect.Height = 0;

            _graphics = Graphics.FromImage(_idleBitmap);
            _sourceRect.Width = PressImage.Width;
            _sourceRect.Height = PressImage.Height;
            _destRect.Width = ButtonSize;
            _destRect.Height = ButtonSize;
            _graphics.DrawImage(NormalImage, _destRect, _sourceRect, GraphicsUnit.Pixel);
        }

        private void DrawIcon()
        {
            _idleBitmap.Dispose();
            _idleBitmap = null;

            _destRect.X = 0;
            _destRect.Y = 0;
            _destRect.Width = 0;
            _destRect.Height = 0;

            _graphics = Graphics.FromImage(_normalBitmap);
            _sourceRect.Width = PressImage.Width;
            _sourceRect.Height = PressImage.Height;
            _destRect.Width = ButtonSize;
            _destRect.Height = ButtonSize;
            _graphics.DrawImage(NormalImage, _destRect, _sourceRect, GraphicsUnit.Pixel);

            var grayIcon = Icon.ToGray();
            _sourceRect.Width = Icon.Width;
            _sourceRect.Height = Icon.Height;
            _destRect.Width = ButtonSize - ButtonSize / 5;
            _destRect.Height = ButtonSize - ButtonSize / 5;
            _destRect.X = ButtonSize / 10;
            _destRect.Y = ButtonSize / 10;
            _graphics.DrawImage(grayIcon, _destRect, _sourceRect, GraphicsUnit.Pixel);

            _graphics = Graphics.FromImage(_pressBitmap);
            _sourceRect.Width = PressImage.Width;
            _sourceRect.Height = PressImage.Height;
            _destRect.X = 0;
            _destRect.Y = 0;
            _destRect.Width = ButtonSize;
            _destRect.Height = ButtonSize;
            _graphics.DrawImage(PressImage, _destRect, _sourceRect, GraphicsUnit.Pixel);

            _sourceRect.Width = Icon.Width;
            _sourceRect.Height = Icon.Height;
            _destRect.Width = ButtonSize - ButtonSize / 5;
            _destRect.Height = ButtonSize - ButtonSize / 5;
            _destRect.X = ButtonSize / 10;
            _destRect.Y = ButtonSize / 10;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            using (GraphicsPath path = CreateRoundedRectanglePath(_destRect, ButtonSize / 10))
            {
                Region originalClip = _graphics.Clip; // 保存当前的裁剪区域（可选，用于恢复）
                _graphics.SetClip(path);
                _graphics.FillRectangle(Brushes.Black, _destRect);
                _graphics.DrawImage(Icon, _destRect, _sourceRect, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 辅助方法：创建圆角矩形路径
        /// </summary>
        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);

            // 定义四个角的圆弧区域
            Rectangle arc = new Rectangle(rect.Location, size);

            // 左上角 (180度开始，扫过90度)
            path.AddArc(arc, 180, 90);

            // 右上角
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // 右下角
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // 左下角
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            // 闭合路径（自动连接直线）
            path.CloseFigure();

            return path;
        }
    }
}
