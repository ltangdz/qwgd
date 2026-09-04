using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _DLC8.Game.DDOS
{
	[Serializable]
	public class DDosLevelDLC8
	{
		public int lv;

		public int enemyHp;

		public List<int> cardIds;

		public List<int> enemyIds;

		public string coinStr;

		public List<List<WavesDLC8>> wavesList;

		public List<List<WavesDLC8>> doubleWavesList;

		private List<int> _coinList;

		public List<int> CoinList
		{
			get
			{
				if (_coinList == null)
				{
					_coinList = new List<int>();
					List<Dictionary<string, string>> list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(coinStr);
					for (int i = 0; i < list.Count; i++)
					{
						_coinList.Add(Convert.ToInt32(list[i]["DropMoney"]));
					}
				}
				return _coinList;
			}
		}

		public void InitWaves(List<Dictionary<string, string>> wavesStrList)
		{
			wavesList = new List<List<WavesDLC8>>();
			for (int i = 0; i < wavesStrList.Count; i++)
			{
				List<WavesDLC8> list = new List<WavesDLC8>();
				string[] array = wavesStrList[i]["LevelWave"].Split('$');
				for (int j = 0; j < array.Length; j++)
				{
					WavesDLC8 wavesDLC = WavesDLC8.Init(array[j], i + 1);
					if (wavesDLC != null)
					{
						list.Add(wavesDLC);
					}
				}
				wavesList.Add(list);
			}
		}

		public void InitDoubleWaves(List<Dictionary<string, string>> doubleWavesStrList)
		{
			doubleWavesList = new List<List<WavesDLC8>>();
			for (int i = 0; i < doubleWavesStrList.Count; i++)
			{
				List<WavesDLC8> list = new List<WavesDLC8>();
				string[] array = doubleWavesStrList[i]["LevelWave"].Split('$');
				for (int j = 0; j < array.Length; j++)
				{
					WavesDLC8 wavesDLC = WavesDLC8.Init(array[j], i + 1);
					if (wavesDLC != null)
					{
						list.Add(wavesDLC);
					}
				}
				doubleWavesList.Add(list);
			}
		}
	}
}
