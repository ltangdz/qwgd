using System.Collections.Generic;
using Newtonsoft.Json;
using PathologicalGames;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class DDOSManager
	{
		private static DDOSManager _instance;

		public List<BagGrid> bagGrids;

		public SpriteAtlas ddosAtlas;

		private SpriteAtlas _ddosTextureAtlas;

		public List<Dictionary<string, string>> attackerDatas;

		public List<Dictionary<string, string>> queeenDatas;

		public List<Dictionary<string, string>> wallDatas;

		public List<Dictionary<string, string>> enemyBaseDatas;

		private DDOSEventManager _eventManager;

		private SpawnPool _spawnPool;

		private int _coin;

		private DDosLevel _level;

		private List<Enemy> _enemies;

		private Transform _enemyArea;

		private Transform _coinTransform;

		public bool isTest;

		private int _lv = 1;

		public static DDOSManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DDOSManager();
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
				EventManager.NoticChangeCoin();
			}
		}

		public DDOSEventManager EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = DDOSEventManager.Instance;
				}
				return _eventManager;
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

		public DDosLevel Level => _level;

		public Transform EnemyArea => _enemyArea;

		public Transform CoinTransform => _coinTransform;

		public List<Enemy> Enemies
		{
			get
			{
				if (_enemies == null)
				{
					_enemies = new List<Enemy>();
				}
				return _enemies;
			}
		}

		public void Destroy()
		{
			_instance = null;
		}

		public DDOSManager()
		{
			_eventManager = DDOSEventManager.Instance;
			_spawnPool = PoolManager.Pools["DDOS"];
			isTest = false;
		}

		public void InitCardDamaged(string attacker, string queenStr, string wallStr, string enemyBaseStr, string lv1, string lv2, string lv3, List<DDosLevel> levels, int lv, Transform enemyArea, Transform coinTransform)
		{
			_enemyArea = enemyArea;
			attackerDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(attacker);
			queeenDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(queenStr);
			wallDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(wallStr);
			enemyBaseDatas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(enemyBaseStr);
			_coinTransform = coinTransform;
			_coin = (isTest ? 1000000 : 5);
			_lv = 1;
			_level = levels[lv - 1];
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
			_level.InitWaves(JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(value));
		}

		public int EnemyDrop()
		{
			return _level.CoinList[_lv - 1];
		}

		public BagGrid CanBuyItem()
		{
			for (int i = 0; i < bagGrids.Count; i++)
			{
				BagGrid bagGrid = bagGrids[i];
				if (!bagGrid.IsLock && bagGrid.DataItem == null)
				{
					return bagGrid;
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
