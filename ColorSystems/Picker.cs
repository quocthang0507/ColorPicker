using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ColorSystems
{
	/// <summary>
	/// https://stackoverflow.com/a/24759418
	/// </summary>
	public class Picker
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindowDC(IntPtr window);
		[DllImport("gdi32.dll", SetLastError = true)]
		private static extern uint GetPixel(IntPtr dc, int x, int y);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int ReleaseDC(IntPtr window, IntPtr dc);

		public static Color GetColorAt(int x, int y)
		{
			IntPtr desk = GetDesktopWindow();
			IntPtr dc = GetWindowDC(desk);
			int a = (int)GetPixel(dc, x, y);
			ReleaseDC(desk, dc);
			return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
		}
	}

	public class Picker2
	{
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
		private static Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(ref Point lpPoint);

		public static Color GetColorAt(Point location)
		{
			using (Graphics gdest = Graphics.FromImage(screenPixel))
			{
				using Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero);
				IntPtr hSrcDC = gsrc.GetHdc();
				IntPtr hDC = gdest.GetHdc();
				int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
				gdest.ReleaseHdc();
				gsrc.ReleaseHdc();
			}

			return screenPixel.GetPixel(0, 0);
		}
	}
}
