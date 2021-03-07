using System;
using System.Collections.Generic;
using System.Text;

namespace ColorSystems
{
	public class CMYK
	{
		public double Cyan { get; set; }
		public double Magenta { get; set; }
		public double Yellow { get; set; }
		public double Black { get; set; }

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
	}
}
