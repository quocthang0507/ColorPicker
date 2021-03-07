using System;
using System.Drawing;

namespace ColorSystems
{
	/// <summary>
	/// The CMYK color space, also known as CMJN, stands for: Cyan, Magenta, Yellow, and blacK.
	/// </summary>
	public class CMYK
	{
		/// <summary>
		/// Ranges from 0 to 100% in most applications.
		/// </summary>
		public double Cyan { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% in most applications.
		/// </summary>
		public double Magenta { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% in most applications.
		/// </summary>
		public double Yellow { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% in most applications.
		/// </summary>
		public double Black { get; set; }

		public CMYK(RGB rgb)
		{
			CMYK cmyk = rgb.ToCmyk();
			Cyan = cmyk.Cyan;
			Magenta = cmyk.Magenta;
			Yellow = cmyk.Yellow;
			Black = cmyk.Black;
		}

		public CMYK(double cyan, double magenta, double yellow, double black)
		{
			Cyan = cyan;
			Magenta = magenta;
			Yellow = yellow;
			Black = black;
		}

		public RGB ToRgb()
		{
			byte red = Convert.ToByte((1 - Math.Min(1, Cyan * (1 - Black) + Black)) * 255);
			byte green = Convert.ToByte((1 - Math.Min(1, Magenta * (1 - Black) + Black)) * 255);
			byte blue = Convert.ToByte((1 - Math.Min(1, Yellow * (1 - Black) + Black)) * 255);
			return new RGB(red, green, blue);
		}

		public Color ToColor()
		{
			return ToRgb().ToColor();
		}
	}
}
