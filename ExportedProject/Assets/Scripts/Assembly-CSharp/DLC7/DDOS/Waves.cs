using System;
using System.Collections.Generic;
using System.Globalization;

namespace DLC7.DDOS
{
	public class Waves
	{
		public float interval;

		public int level;

		public List<Wave> waves;

		public Waves()
		{
			waves = new List<Wave>();
		}

		public static Waves Init(string str, int level)
		{
			string[] array = str.Split('*');
			if (array.Length == 0 || array.Length < 2 || array[0] == "0")
			{
				return null;
			}
			string[] array2 = array[1].Split('_');
			Waves waves = new Waves();
			waves.level = level;
			waves.interval = Convert.ToSingle(array[0], CultureInfo.InvariantCulture);
			for (int i = 0; i < array2.Length; i++)
			{
				Wave wave = Wave.Init(array2[i]);
				if (wave != null)
				{
					waves.waves.Add(wave);
				}
			}
			if (waves.waves.Count == 0)
			{
				return null;
			}
			return waves;
		}
	}
}
