using ColorSystems;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class PickerForm : Form
	{
		public PickerForm()
		{
			InitializeComponent();
		}

		private void mouseMoveTimer_Tick(object sender, EventArgs e)
		{
			Point cursor = new Point();
			Picker2.GetCursorPos(ref cursor);

			var color = Picker2.GetColorAt(cursor);
			this.BackColor = color;
			lblHexColor.Text = "#" + new RGB(color);
		}

		private void PickerForm_Load(object sender, EventArgs e)
		{
			mouseMoveTimer.Start();
		}

		private void PickerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			mouseMoveTimer.Stop();
		}
	}
}
