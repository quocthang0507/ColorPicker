using System;
using System.Drawing;

namespace ColorSystems
{
	/// <summary>
	/// RGBA color values are an extension of RGB color values with an alpha channel - which specifies the opacity for a color.
	/// An RGBA color value is specified with: rgba(red, green, blue, alpha). The alpha parameter is a number between 0.0 (fully transparent) and 1.0 (fully opaque).
	/// </summary>
	public class RGBA : RGB
	{
		public override byte Red { get => base.Red; set => base.Red = value; }
		public override byte Green { get => base.Green; set => base.Green = value; }
		public override byte Blue { get => base.Blue; set => base.Blue = value; }
		public double Alpha { get; set; }

		public RGBA(byte red, byte green, byte blue, double alpha) : base(red, green, blue)
		{
			this.Alpha = alpha;
		}

		public RGB ToRgb()
		{
			byte r, g, b;
			r = Convert.ToByte((1 - Alpha) * Red + Alpha * Red);
			g = Convert.ToByte((1 - Alpha) * Green + Alpha * Green);
			b = Convert.ToByte((1 - Alpha) * Blue + Alpha * Blue);
			return new RGB(r, g, b);
		}

		public override Color ToColor()
		{
			RGB rgb = ToRgb();
			return Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
		}

	}
}
