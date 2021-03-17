using ColorSystems;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class FloatingPickerForm : Form
	{
		public FloatingPickerForm()
		{
			InitializeComponent();
			panelColor.Paint += control_Paint;
		}

		private void mouseMoveTimer_Tick(object sender, EventArgs e)
		{
			// Di chuyển form
			Location = Cursor.Position;

			// Lấy pixel và mã màu
			var pixelColor = Picker2.GetColorAt(Cursor.Position);
			panelColor.BackColor = pixelColor;

			lblHexColor.Text = "#" + new RGB(pixelColor);
		}

		private void FloatingPickerForm_Load(object sender, EventArgs e)
		{
			mouseMoveTimer.Start();
		}

		private void FloatingPickerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			mouseMoveTimer.Stop();
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

		private void control_Paint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			RectangleF Rect = new(0, 0, Width, Height);
			using GraphicsPath GraphPath = GetRoundPath(Rect, 5);
			Region = new Region(GraphPath);
			using Pen pen = new(Color.CadetBlue, 1.75f);
			pen.Alignment = PenAlignment.Inset;
			e.Graphics.DrawPath(pen, GraphPath);
		}

		/// <summary>
		/// https://stackoverflow.com/a/28486964
		/// </summary>
		/// <param name="Rect"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		private GraphicsPath GetRoundPath(RectangleF Rect, int radius)
		{
			float r2 = radius / 2f;
			GraphicsPath GraphPath = new();
			GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
			GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
			GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
			GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
			GraphPath.AddArc(Rect.X + Rect.Width - radius,
							 Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
			GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
			GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
			GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
			GraphPath.CloseFigure();
			return GraphPath;
		}
	}
}
