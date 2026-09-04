using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DLC7.DDOS
{
	[Serializable]
	public class DDosLevel
	{
		public int lv;

		public int enemyHp;

		public List<int> cardIds;

		public List<int> enemyIds;

		public string coinStr;

		public List<List<Waves>> wavesList;

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
			wavesList = new List<List<Waves>>();
			for (int i = 0; i < wavesStrList.Count; i++)
			{
				List<Waves> list = new List<Waves>();
				string[] array = wavesStrList[i]["LevelWave"].Split('$');
				for (int j = 0; j < array.Length; j++)
				{
					Waves waves = Waves.Init(array[j], i + 1);
					if (waves != null)
					{
						list.Add(waves);
					}
				}
				wavesList.Add(list);
			}
		}
	}
}
