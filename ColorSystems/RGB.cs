using System;
using System.Drawing;

namespace ColorSystems
{
	public class RGB
	{
		public virtual byte Red { get; set; }
		public virtual byte Green { get; set; }
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

		public Color ToColor()
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
	}
}
