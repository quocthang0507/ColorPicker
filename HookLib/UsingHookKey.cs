using System.Linq;
using System.Reflection;

namespace HookLib
{
	/// <summary>
	/// Raise the Keyboard event
	/// Process when the key pressed
	/// </summary>
	public class UsingHookKey
	{
		public MyDelegate UpdateColor { get; set; }
		public delegate void MyDelegate();

		private readonly KeyboardListener keyboardListener;
		private int vkcode;

		public UsingHookKey()
		{
			keyboardListener = new KeyboardListener();
			keyboardListener.KeyDown += keyboardListener_KeyDown;
		}

		/// <summary>
		/// The main process when the key pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void keyboardListener_KeyDown(object sender, RawKeyEventArgs args)
		{
			switch (args.VKCode)
			{
				case VKCodeConst.ESC:
					UpdateColor.Invoke();
					break;
				default:
					break;
			}
			this.vkcode = args.VKCode;
		}

		/// <summary>
		/// Show the variable name instead of it's value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private string LookupVariableName(int value)
		{
			var props = typeof(HookLib.VKCodeConst).GetFields(BindingFlags.Public | BindingFlags.Static);
			var wantedprop = props.FirstOrDefault(prop => (int)prop.GetValue(null) == value);
			return wantedprop.Name;
		}

	}
}
