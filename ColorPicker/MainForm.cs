using ColorSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class MainForm : Form
	{
		private RGBA RGBA;
		private RGB RGB;
		private CMYK CMYK;
		private HSB HSB;
		private HSL HSL;

		public MainForm()
		{
			InitializeComponent();
			new Thread(() => RegisterControls()).Start();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
		}

		private void RegisterControls()
		{
			var trackBarControls = GetAll(this, typeof(TrackBar)).ToList();
			var tbxControls = GetAll(this, typeof(TextBox)).ToList();

			foreach (TrackBar trackBar in trackBarControls)
			{
				var tbxName = trackBar.Name.Replace("track", "tbx");
				var textBox = tbxControls.First(c => c.Name == tbxName);
				if (textBox == null)
					throw new NullReferenceException();

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

		private IEnumerable<Control> GetAll(Control control, Type type)
		{
			var controls = control.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, type))
									  .Concat(controls)
									  .Where(c => c.GetType() == type);
		}
	}
}
