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

		public HSB(uint hue, double saturation, double brightness)
		{
			Hue = hue;
			Saturation = saturation;
			Brightness = brightness;
		}
	}
}
