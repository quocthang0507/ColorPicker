using System;
using System.Drawing;

namespace ColorSystems
{
	/// <summary>
	/// The HSB (Hue, Saturation, B rightness) color model defines a color space in terms of three constituent components:
	/// Hue : the color type;
	/// Saturation : the intensity of the color;
	/// Brightness (or Value) : the brightness of the color.
	/// </summary>
	public class HSB
	{
		/// <summary>
		/// Ranges from 0 to 360° in most applications. (each value corresponds to one color : 0 is red, 45 is a shade of orange and 55 is a shade of yellow).
		/// </summary>
		public uint Hue { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% (0 means no color, that is a shade of grey between black and white; 100 means intense color).
		/// Also sometimes called the "purity" by analogy to the colorimetric quantities excitation purity.
		/// </summary>
		public double Saturation { get; set; }
		/// <summary>
		/// Ranges from 0 to 100% (0 is always black; depending on the saturation, 100 may be white or a more or less saturated color).
		/// </summary>
		public double Brightness { get; set; }

		public HSB(RGB rgb)
		{
			HSB hsb = rgb.ToHsb();
			Hue = hsb.Hue;
			Saturation = hsb.Saturation;
			Brightness = hsb.Brightness;
		}

		public HSB(uint hue, double saturation, double brightness)
		{
			Hue = hue;
			Saturation = saturation;
			Brightness = brightness;
		}

		public RGB ToRgb()
		{
			byte r, g, b;
			double _Hue = Hue;
			while (_Hue < 0) { _Hue += 360; };
			while (_Hue >= 360) { _Hue -= 360; };
			double R, G, B;
			if (Brightness <= 0)
			{ R = G = B = 0; }
			else if (Saturation <= 0)
			{
				R = G = B = Brightness;
			}
			else
			{
				double hf = _Hue / 60.0;
				int i = (int)Math.Floor(hf);
				double f = hf - i;
				double pv = Brightness * (1 - Saturation);
				double qv = Brightness * (1 - Saturation * f);
				double tv = Brightness * (1 - Saturation * (1 - f));
				switch (i)
				{

					// Red is the dominant color

					case 0:
						R = Brightness;
						G = tv;
						B = pv;
						break;

					// Green is the dominant color

					case 1:
						R = qv;
						G = Brightness;
						B = pv;
						break;
					case 2:
						R = pv;
						G = Brightness;
						B = tv;
						break;

					// Blue is the dominant color

					case 3:
						R = pv;
						G = qv;
						B = Brightness;
						break;
					case 4:
						R = tv;
						G = pv;
						B = Brightness;
						break;

					// Red is the dominant color

					case 5:
						R = Brightness;
						G = pv;
						B = qv;
						break;

					// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

					case 6:
						R = Brightness;
						G = tv;
						B = pv;
						break;
					case -1:
						R = Brightness;
						G = pv;
						B = qv;
						break;

					// The color is not defined, we should throw an error.

					default:
						//LFATAL("i Value error in Pixel conversion, Value is %d", i);
						R = G = B = Brightness; // Just pretend its black/white
						break;
				}
			}
			r = Clamp((int)(R * 255.0));
			g = Clamp((int)(G * 255.0));
			b = Clamp((int)(B * 255.0));
			return new RGB(r, g, b);
		}

		public Color ToColor()
		{
			RGB rgb = ToRgb();
			return Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
		}

		/// <summary>
		/// Clamp a value to 0-255
		/// </summary>
		private byte Clamp(int i)
		{
			if (i < 0) return 0;
			if (i > 255) return 255;
			return Convert.ToByte(i);
		}

	}
}
