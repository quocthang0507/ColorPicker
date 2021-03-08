using System;
using System.Threading;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class MainForm : Form
	{
		private TextBox textBox;
		private TrackBar trackBar;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			textBox = tbxRedGBA;
			trackBar = trackRedGBA;
			RegisterControls();
		}

		private void RegisterControls()
		{
			trackBar.ValueChanged += TrackBar_ValueChanged;
			textBox.KeyPress += TextBox_KeyPress;
			textBox.TextChanged += TextBox_TextChanged;

			void TextBox_KeyPress(object sender, KeyPressEventArgs e)
			{
				if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
					e.Handled = true;
			}

			void TrackBar_ValueChanged(object sender, EventArgs e)
			{
				textBox.Text = trackBar.Value.ToString();
			}

			void TextBox_TextChanged(object sender, EventArgs e)
			{
				if (textBox.Focused)
				{
					int value = Convert.ToInt32(textBox.Text);
					trackBar.Value = value;
				}
			}
		}

	}
}
