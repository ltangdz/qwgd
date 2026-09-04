namespace DLC7.DDOS
{
	public class Wave
	{
		public int lv;

		public int count;

		public Wave(int lv, int count)
		{
			this.lv = lv;
			this.count = count;
		}

		public static Wave Init(string waveStr)
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
			return new Wave(int.Parse(array[0]), int.Parse(array[1]));
		}
	}
}
