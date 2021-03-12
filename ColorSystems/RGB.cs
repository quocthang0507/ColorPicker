using System;
using System.Drawing;
using System.Text;

namespace ColorSystems
{
	/// <summary>
	/// The RGB (Red, Green, Blue) color model is the most known, and the most used every day. It defines a color space in terms of three components:
	/// Red, which ranges from 0-255;
	/// Green, which ranges from 0-255;
	/// Blue, which ranges from 0-255.
	/// </summary>
	public class RGB
	{
		/// <summary>
		/// Red, which ranges from 0-255
		/// </summary>
		public virtual byte Red { get; set; }
		/// <summary>
		/// Green, which ranges from 0-255
		/// </summary>
		public virtual byte Green { get; set; }
		/// <summary>
		/// Blue, which ranges from 0-255
		/// </summary>
		public virtual byte Blue { get; set; }
		public string RedInHex { get { return Red.ToString("X"); } }
		public string GreenInHex { get { return Green.ToString("X"); } }
		public string BlueInHex { get { return Blue.ToString("X"); } }

		public RGB(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public RGB(string redInHex, string greenInHex, string blueInHex)
		{
			Red = byte.Parse(redInHex, System.Globalization.NumberStyles.HexNumber);
			Green = byte.Parse(greenInHex, System.Globalization.NumberStyles.HexNumber);
			Blue = byte.Parse(blueInHex, System.Globalization.NumberStyles.HexNumber);
		}

		public virtual Color ToColor()
		{
			return Color.FromArgb(Red, Green, Blue);
		}

		public CMYK ToCmyk()
		{
			double black = Math.Min(1.0 - Red / 255.0, Math.Min(1.0 - Green / 255.0, 1.0 - Blue / 255.0));
			double cyan = (1.0 - (Red / 255.0) - black) / (1.0 - black);
			double magenta = (1.0 - (Green / 255.0) - black) / (1.0 - black);
			double yellow = (1.0 - (Blue / 255.0) - black) / (1.0 - black);
			return new CMYK(cyan, magenta, yellow, black);
		}

		public string ToHex()
		{
			StringBuilder hex = new StringBuilder(6);
			hex.AppendFormat("{0:x2}", Red);
			hex.AppendFormat("{0:x2}", Green);
			hex.AppendFormat("{0:x2}", Blue);
			return hex.ToString();
		}

		public HSL ToHsl()
		{
			// Convert RGB to a 0.0 to 1.0 range.
			double double_r = Red / 255.0;
			double double_g = Green / 255.0;
			double double_b = Blue / 255.0;

			// Get the maximum and minimum RGB components.
			double max = double_r;
			if (max < double_g) max = double_g;
			if (max < double_b) max = double_b;

			double min = double_r;
			if (min > double_g) min = double_g;
			if (min > double_b) min = double_b;

			double diff = max - min;
			double h, s, l;
			l = (max + min) / 2;
			if (Math.Abs(diff) < 0.00001)
			{
				s = 0;
				h = 0;  // H is really undefined.
			}
			else
			{
				if (l <= 0.5) s = diff / (max + min);
				else s = diff / (2 - max - min);

				double r_dist = (max - double_r) / diff;
				double g_dist = (max - double_g) / diff;
				double b_dist = (max - double_b) / diff;

				if (double_r == max) h = b_dist - g_dist;
				else if (double_g == max) h = 2 + r_dist - b_dist;
				else h = 4 + g_dist - r_dist;

				h *= 60;
				if (h < 0) h += 360;
			}
			return new HSL(Convert.ToUInt32(h), s, l);
		}

		public HSB ToHsb()
		{
			byte r = Red, g = Green, b = Blue;
			double rabs, gabs, babs, rr, gg, bb, h = 0, s, v, diff;
			rabs = r / 255;
			gabs = g / 255;
			babs = b / 255;
			v = Math.Max(rabs, Math.Max(gabs, babs));
			diff = v - Math.Min(rabs, Math.Min(gabs, babs));
			double diffc(double c)
			{
				return (v - c) / 6 / diff + (1 / 2);
			};
			double percentRoundFn(double num)
			{
				return Math.Round(num * 100) / 100;
			}
			if (diff == 0)
			{
				h = 0;
				s = 0;
			}
			else
			{
				s = diff / v;
				rr = diffc(rabs);
				gg = diffc(gabs);
				bb = diffc(babs);

				if (rabs == v)
				{
					h = bb - gg;
				}
				else if (gabs == v)
				{
					h = (1 / 3) + rr - bb;
				}
				else if (babs == v)
				{
					h = (2 / 3) + gg - rr;
				}
				if (h < 0)
				{
					h += 1;
				}
				else if (h > 1)
				{
					h -= 1;
				}
			}
			return new HSB(
			Convert.ToUInt32(Math.Round(h * 360)),
			percentRoundFn(s),
			percentRoundFn(v));
		}

		public RGBA ToRgba()
		{
			return new RGBA(Red, Green, Blue, 1.0);
		}
	}
}
