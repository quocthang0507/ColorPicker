using ColorLib;
using System;
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
			// Point cursor = new();
			// Picker2.GetCursorPos(ref cursor);

			// var pixelColor = Picker2.GetColorAt(cursor);
			var pixelColor = Picker.GetColorAt(Cursor.Position);
			BackColor = pixelColor;

			lblHexColor.Text = "#" + new RGB(pixelColor);
			lblHexColor.ForeColor = ColorHelper.GetContrastColor(pixelColor);
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
