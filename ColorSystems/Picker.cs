﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ColorSystems
{
	public class Picker
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindowDC(IntPtr window);
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetPixel(IntPtr dc, int x, int y);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int ReleaseDC(IntPtr window, IntPtr dc);

		public static Color GetColorAt(int x, int y)
		{
			IntPtr desk = GetDesktopWindow();
			IntPtr dc = GetWindowDC(desk);
			int a = (int)GetPixel(dc, x, y);
			ReleaseDC(desk, dc);
			return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
		}
	}
}
