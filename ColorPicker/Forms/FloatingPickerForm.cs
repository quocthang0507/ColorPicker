using ColorLib;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class FloatingPickerForm : Form
	{
		private const int PWidth = 30;
		private const int PHeight = 30;
		private Color pixelColor;
		private UsingHookKey hook;
		private static FloatingPickerForm singleton;

		public static FloatingPickerForm Instance
		{
			get
			{
				if (singleton == null)
					singleton = new FloatingPickerForm();
				return singleton;
			}
		}

		public FloatingPickerForm()
		{
			InitializeComponent();
			hook = new UsingHookKey();
		}

		private void FloatingPickerForm_Load(object sender, EventArgs e)
		{
			mouseMoveTimer.Start();
		}

		private void mouseMoveTimer_Tick(object sender, EventArgs e)
		{
			// Di chuyển form
			Location = GetLocationForForm(Cursor.Position);

			// Lấy pixel và mã màu
			pixelColor = Picker2.GetColorAt(Cursor.Position);
			panelColor.BackColor = pixelColor;

			lblHexColor.Text = "#" + new RGB(pixelColor);
		}

		private void FloatingPickerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			mouseMoveTimer.Stop();
			MainForm.Instance.ReceiveColor(pixelColor);
		}

		private Point GetLocationForForm(Point point)
		{
			return Point.Add(point, new Size(PWidth, PHeight));
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


		private void RoundedControl_Paint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			RectangleF Rect = new(0, 0, PWidth, PHeight);
			using GraphicsPath GraphPath = GetRoundPath(Rect, 10);
			Region = new Region(GraphPath);
			using Pen pen = new(Color.CadetBlue, 1.75f);
			pen.Alignment = PenAlignment.Inset;
			e.Graphics.DrawPath(pen, GraphPath);
		}

		private void BorderedControl_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.Black, 3), DisplayRectangle);
		}

		/// <summary>
		/// https://stackoverflow.com/a/28486964
		/// </summary>
		/// <param name="Rect"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		private static GraphicsPath GetRoundPath(RectangleF Rect, int radius)
		{
			float r2 = radius / 2f;
			GraphicsPath GraphPath = new();
			GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
			GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
			GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
			GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
			GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
			GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
			GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
			GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
			GraphPath.CloseFigure();
			return GraphPath;
		}

	}
}
