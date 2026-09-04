using System;
using System.Collections.Generic;
using System.Globalization;

namespace _DLC8.Game.DDOS
{
	public class WavesDLC8
	{
		public float interval;

		public int level;

		public List<WaveDLC8> waves;

		public WavesDLC8()
		{
			waves = new List<WaveDLC8>();
		}

		public static WavesDLC8 Init(string str, int level)
		{
			string[] array = str.Split('*');
			if (array.Length == 0 || array.Length < 2 || array[0] == "0")
			{
				return null;
			}
			string[] array2 = array[1].Split('_');
			WavesDLC8 wavesDLC = new WavesDLC8();
			wavesDLC.level = level;
			wavesDLC.interval = Convert.ToSingle(array[0], CultureInfo.InvariantCulture);
			for (int i = 0; i < array2.Length; i++)
			{
				WaveDLC8 waveDLC = WaveDLC8.Init(array2[i]);
				if (waveDLC != null)
				{
					wavesDLC.waves.Add(waveDLC);
				}
			}
			if (wavesDLC.waves.Count == 0)
			{
				return null;
			}
			return wavesDLC;
		}
	}
}
