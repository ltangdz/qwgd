using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Newtonsoft.Json;
using PathologicalGames;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class DDOSManagerDLC8
	{
		private static DDOSManagerDLC8 _instance;

		public List<BagGridDLC8> bagGrids;

		public SpriteAtlas ddosAtlas;

		private SpriteAtlas _ddosTextureAtlas;

		public List<Dictionary<string, string>> attackerDatas;

		public List<Dictionary<string, string>> queeenDatas;

		public List<Dictionary<string, string>> wallDatas;

		public List<Dictionary<string, string>> enemyBaseDatas;

		private DDOSEventManagerDLC8 _eventManagerDlc8;

		private SpawnPool _spawnPool;

		private ObscuredInt _coin;

		private DDosLevelDLC8 _levelDlc8;

		private List<EnemyDLC8> _enemies;

		private Transform _enemyArea;

		private Transform _coinTransform;

		public bool isTest;

		private int _lv = 1;

		public static DDOSManagerDLC8 Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DDOSManagerDLC8();
				}
				return _instance;
			}
		}

		public int Coin
		{
			get
			{
				return _coin;
			}
			set
			{
				_coin = value;
				EventManagerDlc8.NoticChangeCoin();
			}
		}

		public DDOSEventManagerDLC8 EventManagerDlc8
		{
			get
			{
				if (_eventManagerDlc8 == null)
				{
					_eventManagerDlc8 = DDOSEventManagerDLC8.Instance;
				}
				return _eventManagerDlc8;
			}
		}

		public SpriteAtlas DdosTextureAtlas
		{
			get
			{
				return _ddosTextureAtlas;
			}
			set
			{
				_ddosTextureAtlas = value;
			}
		}

		public int Lv
		{
			get
			{
				return _lv;
			}
			set
			{
				_lv = value;
			}
		}

		public SpawnPool SpawnPool => _spawnPool;

		public DDosLevelDLC8 LevelDlc8 => _levelDlc8;

		public Transform EnemyArea => _enemyArea;

		public Transform CoinTransform => _coinTransform;

		public List<EnemyDLC8> Enemies
		{
			get
			{
				if (_enemies == null)
				{
					_enemies = new List<EnemyDLC8>();
				}
				return _enemies;
			}
		}

		public void Destroy()
		{
			_instance = null;
		}

		public DDOSManagerDLC8()
		{
			_eventManagerDlc8 = DDOSEventManagerDLC8.Instance;
			_spawnPool = PoolManager.Pools["DDOSDLC8"];
			isTest = false;
		}

		public void InitCardDamaged(string attacker, string queenStr, string wallStr, string enemyBaseStr, string lv1, string lv2, string lv3, List<DDosLevelDLC8> levels, int lv, Transform enemyArea, Transform coinTransform, string doubleWaves)
		{
			Debug.LogError("InitCardDamaged");
			_enemyArea = enemyArea;
			attackerDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(attacker);
			queeenDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(queenStr);
			wallDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(wallStr);
			enemyBaseDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(enemyBaseStr);
			_coinTransform = coinTransform;
			_coin = (isTest ? 1000000 : 5);
			_lv = 1;
			_levelDlc8 = levels[lv - 1];
			string value = lv3;
			switch (lv)
			{
			case 1:
				value = lv1;
				break;
			case 2:
				value = lv2;
				break;
			}
			_levelDlc8.InitWaves(JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(value));
			_levelDlc8.InitDoubleWaves(JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(doubleWaves));
		}

		public int EnemyDrop()
		{
			return _levelDlc8.CoinList[_lv - 1];
		}

		public BagGridDLC8 CanBuyItem()
		{
			for (int i = 0; i < bagGrids.Count; i++)
			{
				BagGridDLC8 bagGridDLC = bagGrids[i];
				if (!bagGridDLC.IsLock && bagGridDLC.DataItem == null)
				{
					return bagGridDLC;
				}
			}
			return null;
		}

		public float CountHpPercentage(int curHp, int MaxHp)
		{
			return (float)curHp / (float)MaxHp;
		}

		public void InitImage(string path, Image image)
		{
			if (string.IsNullOrEmpty(path))
			{
				image.transform.localScale = Vector2.zero;
				return;
			}
			image.transform.localScale = Vector2.one;
			image.sprite = ddosAtlas.GetSprite(path);
			image.SetNativeSize();
		}
	}
}
