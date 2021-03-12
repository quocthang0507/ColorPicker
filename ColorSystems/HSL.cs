using System.Drawing;

namespace ColorSystems
{
	/// <summary>
	/// The HSL color space, also called HLS or HSI , stands for:
	/// Hue : the color type;
	/// Saturation : variation of the color depending on the lightness;
	/// Lightness (also Luminance or Luminosity or Intensity).
	/// </summary>
	public class HSL
	{
		/// <summary>
		/// Ranges from 0 to 360° in most applications (each value corresponds to one color : 0 is red, 45 is a shade of orange and 55 is a shade of yellow).
		/// </summary>
		public uint Hue { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% (from the center of the black & white axis).
		/// </summary>
		public double Saturation { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% (from black to white).
		/// </summary>
		public double Lightness { get; set; }

		public HSL(RGB rgb)
		{
			HSL hsl = rgb.ToHsl();
			Hue = hsl.Hue;
			Saturation = hsl.Saturation;
			Lightness = hsl.Lightness;
		}

		public HSL(uint hue, double saturation, double lightness)
		{
			Hue = hue;
			Saturation = saturation;
			Lightness = lightness;
		}

		public RGB ToRgb()
		{
			byte r, g, b;
			double p2;
			if (Lightness <= 0.5) p2 = Lightness * (1 + Saturation);
			else p2 = Lightness + Saturation - Lightness * Saturation;

			double p1 = 2 * Lightness - p2;
			double double_r, double_g, double_b;
			if (Saturation == 0)
			{
				double_r = Lightness;
				double_g = Lightness;
				double_b = Lightness;
			}
			else
			{
				double_r = QqhToRgb(p1, p2, Hue + 120);
				double_g = QqhToRgb(p1, p2, Hue);
				double_b = QqhToRgb(p1, p2, Hue - 120);
			}

			// Convert RGB to the 0 to 255 range.
			r = (byte)(double_r * 255.0);
			g = (byte)(double_g * 255.0);
			b = (byte)(double_b * 255.0);
			return new RGB(r, g, b);
		}

		public Color ToColor()
		{
			RGB rgb = ToRgb();
			return Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
		}

		private static double QqhToRgb(double q1, double q2, double hue)
		{
			if (hue > 360) hue -= 360;
			else if (hue < 0) hue += 360;

			if (hue < 60) return q1 + (q2 - q1) * hue / 60;
			if (hue < 180) return q2;
			if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
			return q1;
		}
	}
}
