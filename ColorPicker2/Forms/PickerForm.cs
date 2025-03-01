using ColorLib;
using HookLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker2
{
    public partial class PickerForm : Form
    {
        private const int PWidth = 30;
        private const int PHeight = 30;
        private Color pixelColor;
        private readonly UsingHookKey hook;

        public PickerForm()
        {
            InitializeComponent();
            hook = new UsingHookKey
            {
                UpdateColor = () => this.Invoke((MethodInvoker)delegate { Close(); })
            };
        }

        private void PickerForm_Load(object sender, EventArgs e)
        {
            mouseMoveTimer.Start();
        }

        private void mouseMoveTimer_Tick(object sender, EventArgs e)
        {
            // Di chuyển form
            DesktopLocation = GetNewLocation(Cursor.Position);

            // Lấy pixel và mã màu
            pixelColor = Picker.GetPixelColor(Cursor.Position);
            panelColor.BackColor = pixelColor;

            lblHexColor.Text = "#" + new RGB(pixelColor);
        }

        private void PickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mouseMoveTimer.Stop();
            MainForm.Instance.ReceiveColor(pixelColor);
        }

        private Point GetNewLocation(Point oldLocation)
        {
            Point A = Point.Add(oldLocation, new Size(PWidth, PHeight));
            // Gọi A, B, C và D là 4 góc của form, bắt đầu từ A(0, 0) và đi theo chiều kim đồng hồ
            Point B = new Point(A.X + Width, A.Y);
            Point C = new Point(B.X, B.Y + Height);
            Point D = new Point(A.X, A.Y + Height);
            // Xác định 4 điểm có nằm trong màn hình không
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            if (!screen.Contains(B) && !screen.Contains(C))
            {
                A = Point.Subtract(oldLocation, new Size(Width + PWidth, Height + PHeight));
            }
            else if (!screen.Contains(C) && !screen.Contains(D))
            {
                A = Point.Subtract(oldLocation, new Size(Width + PWidth, 0));
            }
            return A;
        }

        /// <summary>
        /// https://stackoverflow.com/a/3526775
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
