namespace ColorSystems
{
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
	}
}
