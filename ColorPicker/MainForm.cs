using ColorSystems;
using System;
using System.Collections.Generic;
using System.Drawing;
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
		private KnownColor[] allColors;
		private bool userChanged = true;

		public MainForm()
		{
			InitializeComponent();
			new Thread(() =>
			{
				RegisterControls();
				ShowKnownColors();
			}).Start();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateFromRGBToAll();
		}

		#region Events

		/// <summary>
		/// https://stackoverflow.com/a/2555062
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LbxColors_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			Graphics graphics = e.Graphics;
			KnownColor knownColor = allColors[e.Index];
			string colorName = Enum.GetName(typeof(KnownColor), knownColor);
			Color backgroundColor = Color.FromName(colorName);
			Color textColor = ColorHelper.GetContrastColor(backgroundColor);

			// draw the background color you want
			// mine is set to olive, change it to whatever you want
			graphics.FillRectangle(new SolidBrush(backgroundColor), e.Bounds);

			// draw the text of the list item, not doing this will only show
			// the background color
			// you will need to get the text of item to display
			graphics.DrawString($"{colorName} (#{ColorHelper.GetHexFromColor(backgroundColor)})", e.Font, new SolidBrush(textColor), new PointF(e.Bounds.X, e.Bounds.Y + 3));

			e.DrawFocusRectangle();
		}

		private void lbxColors_SelectedIndexChanged(object sender, EventArgs e)
		{
			KnownColor knownColor = allColors[lbxColors.SelectedIndex];
			string colorName = Enum.GetName(typeof(KnownColor), knownColor);
			Color color = Color.FromName(colorName);
			RGB = new RGB(color);
			//////////////////////////
			userChanged = false;
			trackRedGB.Value = RGB.Red;
			trackRGreenB.Value = RGB.Green;
			trackRGBlue.Value = RGB.Blue;
			UpdateFromRGBToAll();
			userChanged = true;
			//////////////////////////
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(RGB.ToHex());
			MessageBox.Show("Copied to clipboard successfully", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// https://social.msdn.microsoft.com/Forums/windows/en-US/60767912-6ea4-4ff6-acb5-44002bd94e82/how-to-change-border-color-of-groupbox-in-cnet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gbxRGB_Paint(object sender, PaintEventArgs e)
		{
			GroupBox box = (GroupBox)sender;
			e.Graphics.Clear(SystemColors.Control);

			// Vẽ lại viền
			Size tSize = TextRenderer.MeasureText(box.Text, box.Font);
			Rectangle borderRect = e.ClipRectangle;
			borderRect.Y += tSize.Height / 2;
			borderRect.Height -= tSize.Height / 2;
			ControlPaint.DrawBorder(e.Graphics, borderRect, Color.Black, ButtonBorderStyle.Solid);

			Rectangle textRect = e.ClipRectangle;
			textRect.X += 5;
			textRect.Width = tSize.Width;
			textRect.Height = tSize.Height;
			e.Graphics.FillRectangle(new SolidBrush(BackColor), textRect);

			// Ghi chữ
			e.Graphics.DrawString(box.Text, box.Font, Brushes.Black, 5, 0);
		}

		private void btnPick_Click(object sender, EventArgs e)
		{

		}

		#endregion

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
					if (userChanged)
					{
						if (CheckWhichGroupBoxOwns(trackBar, gbxRGB))
						{
							UpdateFromRGBToAll();
						}
						else if (CheckWhichGroupBoxOwns(trackBar, gbxRGBA))
						{
							UpdateFromRGBAToAll();
						}
						else if (CheckWhichGroupBoxOwns(trackBar, gbxCMYK))
						{
							UpdateFromCMYKToAll();
						}
					}
					//else if (CheckWhichGroupBoxOwns(trackBar, gbxHSB))
					//{
					//	UpdateFromHSBToAll();
					//}
					//else
					//{
					//	UpdateFromHSLToAll();
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
			return controls.SelectMany(ctrl => GetAll(ctrl, typeOfChild)).Concat(controls).Where(c => c.GetType() == typeOfChild);
		}

		/// <summary>
		/// Hiển thị thông tin màu đang được chọn
		/// </summary>
		private void ShowPreview()
		{
			lblHexColor.BackColor = RGB.ToColor();
			lblHexColor.Text = $"#{RGB.ToHex()}";
			lblHexColor.ForeColor = ColorHelper.GetContrastColor(lblHexColor.BackColor);
		}

		/// <summary>
		/// Từ RGB cập nhật các mã màu còn lại
		/// </summary>
		private void UpdateFromRGBToAll()
		{
			if (userChanged)
				RGB = new RGB((byte)trackRedGB.Value, (byte)trackRGreenB.Value, (byte)trackRGBlue.Value);

			RGBA = new RGBA(RGB);
			UpdateByColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateByColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateByColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateByColorSystem(typeof(HSB).Name);

			ShowPreview();
		}

		/// <summary>
		/// Từ RGBA cập nhật các mã màu còn lại
		/// </summary>
		private void UpdateFromRGBAToAll()
		{
			if (userChanged)
				RGBA = new RGBA((byte)trackRedGBA.Value, (byte)trackRGreenBA.Value, (byte)trackRGBlueA.Value, trackRGBAlpha.Value / 100f);

			RGB = RGBA.ToRgb();
			UpdateByColorSystem(typeof(RGB).Name);
			CMYK = new CMYK(RGB);
			UpdateByColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateByColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateByColorSystem(typeof(HSB).Name);

			ShowPreview();
		}

		/// <summary>
		/// Từ CMYK cập nhật các mã màu còn lại
		/// </summary>
		private void UpdateFromCMYKToAll()
		{
			if (userChanged)
				CMYK = new CMYK(trackCyanMYK.Value / 100f, trackCMagentaYK.Value / 100f, trackCMYellowK.Value / 100f, trackCMYblacK.Value / 100f);

			RGB = CMYK.ToRgb();
			UpdateByColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateByColorSystem(typeof(RGBA).Name);
			HSL = new HSL(RGB);
			UpdateByColorSystem(typeof(HSL).Name);
			HSB = new HSB(RGB);
			UpdateByColorSystem(typeof(HSB).Name);

			ShowPreview();
		}

		/*
		/// <summary>
		/// Từ HSL cập nhật các mã màu còn lại
		/// </summary>
		private void UpdateFromHSLToAll()
		{
			if (userChanged)
				HSL = new HSL((uint)trackHueSL.Value, trackHSaturationL.Value / 100f, trackHSLightness.Value / 100f);

			RGB = HSL.ToRgb();
			UpdateByColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateByColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateByColorSystem(typeof(CMYK).Name);
			HSB = new HSB(RGB);
			UpdateByColorSystem(typeof(HSB).Name);

			ShowPreview();
		}

		/// <summary>
		/// Từ HSB cập nhật các mã màu còn lại
		/// </summary>
		private void UpdateFromHSBToAll()
		{
			if (userChanged)
				HSB = new HSB((uint)trackHueSB.Value, trackHSaturationB.Value / 100f, trackHSBrightness.Value / 100f);

			RGB = HSB.ToRgb();
			UpdateByColorSystem(typeof(RGB).Name);
			RGBA = new RGBA(RGB);
			UpdateByColorSystem(typeof(RGBA).Name);
			CMYK = new CMYK(RGB);
			UpdateByColorSystem(typeof(CMYK).Name);
			HSL = new HSL(RGB);
			UpdateByColorSystem(typeof(HSL).Name);

			ShowPreview();
		}
		*/

		/// <summary>
		/// Cập nhật các trackBar khác dựa trên mã màu sysColorName bị thay đổi
		/// </summary>
		/// <param name="sysColorName">Mã màu bị thay đổi</param>
		private void UpdateByColorSystem(string sysColorName)
		{
			// Ngăn sự kiện trackBarValueChanged xảy ra
			userChanged = false;
			switch (sysColorName)
			{
				case "RGBA":
					trackRedGBA.Value = RGBA.Red;
					trackRGreenBA.Value = RGBA.Green;
					trackRGBlueA.Value = RGBA.Blue;
					trackRGBAlpha.Value = (int)(100 * RGBA.Alpha);
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
			userChanged = true;
		}

		/// <summary>
		/// Kiểm tra control thuộc groupbox này không
		/// </summary>
		/// <param name="control">Control con</param>
		/// <param name="groupBox">Groupbox cha</param>
		/// <returns></returns>
		private bool CheckWhichGroupBoxOwns(Control control, GroupBox groupBox)
		{
			var controls = GetAll(groupBox, control.GetType());
			return controls.Contains(control);
		}

		/// <summary>
		/// Hiển thị listbox màu đã biết trong thư viện .NET
		/// </summary>
		private void ShowKnownColors()
		{
			Array colorsArray = Enum.GetValues(typeof(KnownColor));
			allColors = new KnownColor[colorsArray.Length];
			Array.Copy(colorsArray, allColors, colorsArray.Length);
			foreach (var color in allColors)
			{
				lbxColors.Items.Add(Color.FromName(color.ToString()));
			}
		}

	}
}
