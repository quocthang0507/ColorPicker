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
		private bool shouldFireEvents = true;

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
					if (CheckWhichGroupBoxOwns(trackBar, gbxRGB))
					{
						FromRGBToUpdateAll();
					}
				}

				void TextBox_TextChanged(object sender, EventArgs e)
				{
					if (textBox.Focused)
					{
						int value = Convert.ToInt32(textBox.Text);
						trackBar.Value = value;
					}

				}
				// Xóa control đã tìm được để lần sau tìm nhanh hơn
				tbxControls.Remove(textBox);
			}

		}

		private IEnumerable<Control> GetAll(Control control, Type type)
		{
			var controls = control.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, type))
									  .Concat(controls)
									  .Where(c => c.GetType() == type);
		}

		private void UpdateColorSystems()
		{
			//RGBA = new RGBA((byte)trackRedGBA.Value, (byte)trackRGreenBA.Value, (byte)trackRGBlueA.Value, (byte)trackRGBAlpha.Value);
			//RGB = new RGB((byte)trackRedGB.Value, (byte)trackRGreenB.Value, (byte)trackRGBlue.Value);
			//CMYK = new CMYK(trackCyanMYK.Value, trackCMagentaYK.Value, trackCMYellowK.Value, trackCMYblacK.Value);
			//HSB = new HSB((uint)trackHueSB.Value, trackHSaturationB.Value, trackHSBrightness.Value);
			//HSL = new HSL((uint)trackHueSL.Value, trackHSaturationL.Value, trackHSLightness.Value);

		}

		private void FromRGBToUpdateAll()
		{
			RGB = new RGB((byte)trackRedGB.Value, (byte)trackRGreenB.Value, (byte)trackRGBlue.Value);
			RGBA = new RGBA(RGB);
			UpdateColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateColorSystem(typeof(HSB).Name);
		}

		private void UpdateColorSystem(string sysColorName)
		{
			shouldFireEvents = false;
			switch (sysColorName)
			{
				case "RGBA":
					trackRedGBA.Value = RGBA.Red;
					trackRGreenBA.Value = RGBA.Green;
					trackRGBlueA.Value = RGBA.Blue;
					trackRGBAlpha.Value = (int)RGBA.Alpha;
					break;
				case "RGB":
					trackRedGB.Value = RGB.Red;
					trackRGreenB.Value = RGB.Green;
					trackRGBlue.Value = RGB.Blue;
					break;
				case "CMYK":
					trackCyanMYK.Value = (int)(100 * CMYK.Cyan);
					trackCMagentaYK.Value = (int)(100 * CMYK.Magenta);
					trackCMYellowK.Value = (int)(100 * CMYK.Yellow);
					trackCMYblacK.Value = (int)(100 * CMYK.Black);
					break;
				case "HSL":
					trackHueSL.Value = (int)HSL.Hue;
					trackHSaturationL.Value = (int)(100 * HSL.Saturation);
					trackHSLightness.Value = (int)(100 * HSL.Lightness);
					break;
				case "HSB":
					trackHueSB.Value = (int)HSB.Hue;
					trackHSaturationB.Value = (int)(100 * HSB.Saturation);
					trackHSBrightness.Value = (int)(100 * HSB.Brightness);
					break;
				default:
					break;
			}
			shouldFireEvents = true;
		}

		private bool CheckWhichGroupBoxOwns(Control control, GroupBox groupBox)
		{
			var controls = GetAll(groupBox, control.GetType());
			return controls.Contains(control);
		}
	}
}
