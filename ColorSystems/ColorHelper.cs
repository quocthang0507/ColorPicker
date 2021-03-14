using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ColorSystems
{
	public class ColorHelper
	{
		/// <summary>
		/// https://www.codeproject.com/Articles/16565/Determining-Ideal-Text-Color-Based-on-Specified-Ba
		/// </summary>
		/// <param name="colorA"></param>
		/// <returns></returns>
		public static Color GetContrastColor(Color colorA)
		{
			int nThreshold = 105;
			int bgDelta = Convert.ToInt32((colorA.R * 0.299) + (colorA.G * 0.587) +
										  (colorA.B * 0.114));

			Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
			return foreColor;
		}
	}
}
