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
			Location = GetLocationForForm(Cursor.Position);

			// Lấy pixel và mã màu
			pixelColor = Picker2.GetColorAt(Cursor.Position);
			panelColor.BackColor = pixelColor;

			lblHexColor.Text = "#" + new RGB(pixelColor);
		}

		private void PickerForm_FormClosing(object sender, FormClosingEventArgs e)
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
	}
}
