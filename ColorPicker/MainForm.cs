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

		/// <summary>
		/// Đăng ký sự kiện từng cặp trackBar và textBox một
		/// </summary>
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
					//else if (CheckWhichGroupBoxOwns(trackBar, gbxRGBA))
					//{
					//	FromRGBAToUpdateAll();
					//}
					//else if (CheckWhichGroupBoxOwns(trackBar, gbxCMYK))
					//{
					//	FromCMYKToUpdateAll();
					//}
					//else if (CheckWhichGroupBoxOwns(trackBar, gbxHSB))
					//{
					//	FromHSBToUpdateAll();
					//}
					//else if (CheckWhichGroupBoxOwns(trackBar, gbxHSL))
					//{
					//	FromHSLToUpdateAll();
					//}
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

		/// <summary>
		/// Lấy tất cả các control con nằm trong control cha bằng đệ quy
		/// </summary>
		/// <param name="parentControl">Control cha</param>
		/// <param name="typeOfChild">Kiểu của control con cần tìm</param>
		/// <returns></returns>
		private IEnumerable<Control> GetAll(Control parentControl, Type typeOfChild)
		{
			var controls = parentControl.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, typeOfChild))
									  .Concat(controls)
									  .Where(c => c.GetType() == typeOfChild);
		}

		/// <summary>
		/// Từ RGB cập nhật các mã màu còn lại
		/// </summary>
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

			panelColor.BackColor = RGB.ToColor();
			lblHexColor.Text = $"#{RGB.ToHex()}";
		}

		/// <summary>
		/// Từ RGBA cập nhật các mã màu còn lại
		/// </summary>
		private void FromRGBAToUpdateAll()
		{
			RGBA = new RGBA((byte)trackRedGBA.Value, (byte)trackRGreenBA.Value, (byte)trackRGBlueA.Value, (byte)trackRGBAlpha.Value);

			RGB = RGBA.ToRgb();
			UpdateColorSystem(typeof(RGB).Name);
			CMYK = new CMYK(RGB);
			UpdateColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateColorSystem(typeof(HSB).Name);
		}

		/// <summary>
		/// Từ CMYK cập nhật các mã màu còn lại
		/// </summary>
		private void FromCMYKToUpdateAll()
		{
			CMYK = new CMYK(trackCyanMYK.Value, trackCMagentaYK.Value, trackCMYellowK.Value, trackCMYblacK.Value);

			RGB = CMYK.ToRgb();
			UpdateColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateColorSystem(typeof(RGBA).Name);
			HSL = new HSL(RGB);
			UpdateColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateColorSystem(typeof(HSB).Name);
		}

		/// <summary>
		/// Từ HSL cập nhật các mã màu còn lại
		/// </summary>
		private void FromHSLToUpdateAll()
		{
			HSL = new HSL((uint)trackHueSL.Value, trackHSaturationL.Value, trackHSLightness.Value);

			RGB = HSL.ToRgb();
			UpdateColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateColorSystem(typeof(CMYK).Name);
			HSB = new HSB(RGB);
			UpdateColorSystem(typeof(HSB).Name);
		}

		/// <summary>
		/// Từ HSB cập nhật các mã màu còn lại
		/// </summary>
		private void FromHSBToUpdateAll()
		{
			HSB = new HSB((uint)trackHueSB.Value, trackHSaturationB.Value, trackHSBrightness.Value);

			RGB = HSB.ToRgb();
			UpdateColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateColorSystem(typeof(HSL).Name);
		}

		/// <summary>
		/// Cập nhật các trackBar khác dựa trên mã màu sysColorName bị thay đổi
		/// </summary>
		/// <param name="sysColorName">Mã màu bị thay đổi</param>
		private void UpdateColorSystem(string sysColorName)
		{
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
		}

		/// <summary>
		/// Kiểm tra control thuộc groupbox nào
		/// </summary>
		/// <param name="control">Control con</param>
		/// <param name="groupBox">Groupbox cha</param>
		/// <returns></returns>
		private bool CheckWhichGroupBoxOwns(Control control, GroupBox groupBox)
		{
			var controls = GetAll(groupBox, control.GetType());
			return controls.Contains(control);
		}
	}
}
