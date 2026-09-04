namespace _DLC8.Game.DDOS
{
	public class WaveDLC8
	{
		public int lv;

		public int count;

		public WaveDLC8(int lv, int count)
		{
			this.lv = lv;
			this.count = count;
		}

		public static WaveDLC8 Init(string waveStr)
		{
			string[] array = waveStr.Split(',');
			if (array.Length == 0 || array.Length < 2)
			{
				return null;
			}
			if (array[1] == "0")
			{
				return null;
			}
			return new WaveDLC8(int.Parse(array[0]), int.Parse(array[1]));
		}
	}
}
