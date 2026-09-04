using System;
using System.Collections;
using System.Collections.Generic;
using DLC7.Titan;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class DDOSGameController : DDosMonoBehaviour
	{
		public List<BagGrid> bagGrids;

		public List<BagGrid> attackerGrids;

		public SpriteAtlas ddosAtlas;

		public SpriteAtlas ddosShaderTextureAtlas;

		public Text safeLevelText;

		public GameObject exitWindows;

		public Button exitButton;

		public Button cancelButton;

		[Header("游戏数值")]
		public string cardDamageStr;

		public string queenStr;

		public string wallStr;

		public string enemyBaseStr;

		public string hardStr1;

		public string hardStr2;

		public string hardStr3;

		public List<DDosLevel> levels;

		public Transform coinTransform;

		[SerializeField]
		private int _lv = 1;

		public List<GameObject> teachGroups;

		public Button _teachButton;

		public GameObject enemyArea;

		public DDOSResultDialog resultDialog;

		private int _curTeachIndex;

		private string _safeLvString = "";

		private GameResult _gameResult = GameResult.GAMING;

		private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

		private GameManager _gameManager;

		private long _startTime;

		public Button SkipButton;

		private string[] soundStrs = new string[15]
		{
			"ClickCoin", "CoinShow", "EnemyDead", "GetCard", "hecheng", "lajitong", "NoBuy", "OurHurt", "Win", "Shoot",
			"Normal", "Fire", "Ice", "Palsy", "Fail"
		};

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		public string SafeLvString
		{
			get
			{
				if (string.IsNullOrEmpty(_safeLvString))
				{
					_safeLvString = I18N.instance.getValue("^110008_game_90");
				}
				return _safeLvString;
			}
		}

		public void InitData(int lv)
		{
			_lv = lv;
			InitDDosManagerData();
			Invoke("InitAttackerRound", 1f);
			InvokeRepeating("QueenTransfer", 3f, 0.3f);
			SaveText(1);
			ShowTeaching();
			if (SkipButton != null)
			{
				SkipButton.onClick.AddListener(Skip);
			}
			exitButton.onClick.AddListener(delegate
			{
				exitWindows.gameObject.SetActive(value: false);
				base.DdosEventManager.NoticeGameResult(GameResult.SUCCESS);
			});
			cancelButton.onClick.AddListener(delegate
			{
				exitWindows.gameObject.SetActive(value: false);
			});
		}

		private AudioClip LoadSound(DdosSound type)
		{
			string text = soundStrs[(int)type];
			if (soundDic.ContainsKey(text))
			{
				return soundDic[text];
			}
			AudioClip audioClip = Resources.Load<AudioClip>($"_DLC7/Sound/DDOS/{text}");
			soundDic[text] = audioClip;
			return audioClip;
		}

		private void ShowTeaching()
		{
			_curTeachIndex = ((_lv != 1) ? (3 + _lv) : 0);
			if (_lv == 1)
			{
				_curTeachIndex = 0;
			}
			else
			{
				_curTeachIndex = 5;
			}
			teachGroups[_curTeachIndex].SetActive(value: true);
			_teachButton.onClick.AddListener(TeachingNext);
			_startTime = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		}

		private void TeachingNext()
		{
			if (_lv == 1)
			{
				if (_curTeachIndex < 4)
				{
					_curTeachIndex++;
					teachGroups[_curTeachIndex - 1].SetActive(value: false);
					teachGroups[_curTeachIndex].SetActive(value: true);
				}
				else
				{
					_teachButton.gameObject.SetActive(value: false);
					Invoke("StartGame", 2f);
				}
			}
			else
			{
				_teachButton.gameObject.SetActive(value: false);
				Invoke("StartGame", 2f);
			}
		}

		private void StartGame()
		{
			StartCoroutine("Waves");
		}

		private void SaveText(int level)
		{
			base.DdosManager.Lv = level;
			safeLevelText.text = $"{SafeLvString}:{level}";
		}

		private void QueenTransfer()
		{
			List<BagGrid> list = new List<BagGrid>();
			for (int i = 0; i < attackerGrids.Count; i++)
			{
				BagGrid bagGrid = attackerGrids[i];
				CardItem curCardItem = bagGrid.CurCardItem;
				if (!(curCardItem == null))
				{
					curCardItem.QueenTransfer(0);
					if (curCardItem.Card.Type == CardType.QUEEN && curCardItem.Card.Intensify == IntensifyType.TRANSFER)
					{
						list.Add(bagGrid);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				BagGrid bagGrid2 = list[j];
				CardItem curCardItem2 = bagGrid2.CurCardItem;
				if (curCardItem2.Card.Type != CardType.QUEEN)
				{
					continue;
				}
				int queenBuff = curCardItem2.Card.QueenBuff;
				if (curCardItem2 == null || curCardItem2.Card.Intensify != IntensifyType.TRANSFER)
				{
					continue;
				}
				List<BagGrid> roundGrids = bagGrid2.RoundGrids;
				for (int k = 0; k < roundGrids.Count; k++)
				{
					CardItem curCardItem3 = roundGrids[k].CurCardItem;
					if (!(curCardItem3 == null))
					{
						curCardItem3.QueenTransfer(queenBuff);
					}
				}
			}
		}

		private IEnumerator Waves()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(3f);
			DDosLevel level = base.DdosManager.Level;
			List<int> enemyIds = level.enemyIds;
			List<List<Waves>> levelWavesList = level.wavesList;
			List<Dictionary<string, string>> enemyDataList = base.DdosManager.enemyBaseDatas;
			for (int i = 0; i < levelWavesList.Count; i++)
			{
				if (i != 0)
				{
					yield return waitForSeconds;
				}
				List<Waves> lvWaves = levelWavesList[i];
				SaveText(i + 1);
				base.DdosEventManager.NoticeLevel(i + 1);
				bool hasBoss = (i + 1) % 3 == 0;
				for (int l = 0; l < lvWaves.Count; l++)
				{
					Waves waves = lvWaves[l];
					float interval = waves.interval;
					List<Wave> levelWavesWaves = waves.waves;
					bool isLast3 = l == lvWaves.Count - 1;
					for (int j = 0; j < levelWavesWaves.Count; j++)
					{
						Wave wave = levelWavesWaves[j];
						int waveLv = wave.lv;
						int waveCount = wave.count;
						bool isLast4 = j == levelWavesWaves.Count - 1;
						for (int k = 0; k < waveCount; k++)
						{
							float num = UnityEngine.Random.Range(0.1f, 0.3f);
							interval -= num;
							Dictionary<string, string> enemyDic = enemyDataList[waveLv - 1];
							Enemy component = base.DdosManager.SpawnPool.Spawn("DDOSEnemy").GetComponent<Enemy>();
							int index = UnityEngine.Random.Range(0, enemyIds.Count);
							bool flag = k == waveCount - 1;
							bool isBoss = hasBoss && isLast4 && flag && isLast3;
							component.InitData(enemyDic, isBoss, (EnemyType)enemyIds[index], enemyArea.transform);
							base.DdosManager.Enemies.Add(component);
							if (_gameResult == GameResult.SUCCESS)
							{
								StopCoroutine("Waves");
								yield break;
							}
							if (_gameResult == GameResult.FAIL)
							{
								StopCoroutine("Waves");
								if (component != null)
								{
									component.Win();
								}
							}
							yield return new WaitForSeconds(UnityEngine.Random.Range(0.8f, 1.3f));
						}
					}
					yield return new WaitForSeconds(interval);
				}
			}
		}

		private void InitLv(int lv)
		{
		}

		private void InitAttackerRound()
		{
			base.DdosManager.bagGrids = bagGrids;
			base.DdosManager.ddosAtlas = ddosAtlas;
			base.DdosManager.DdosTextureAtlas = ddosAtlas;
			for (int i = 0; i < attackerGrids.Count; i++)
			{
				BagGrid bagGrid = attackerGrids[i];
				for (int j = 0; j < attackerGrids.Count; j++)
				{
					BagGrid bagGrid2 = attackerGrids[j];
					if (i != j && bagGrid.AreaRect.Overlaps(bagGrid2.AreaRect))
					{
						bagGrid.RoundGrids.Add(bagGrid2);
					}
				}
			}
		}

		private void InitDDosManagerData()
		{
			base.DdosManager.InitCardDamaged(cardDamageStr, queenStr, wallStr, enemyBaseStr, hardStr1, hardStr2, hardStr3, levels, _lv, enemyArea.transform, coinTransform);
		}

		private void Skip()
		{
			exitWindows.gameObject.SetActive(value: true);
		}

		private void Awake()
		{
			DLCEventManager.Instance.onNoticeBackGame += NoticeBackGame;
			DLCEventManager.Instance.onNoticeGameSuccess += NoticeGameSuccess;
			base.DdosEventManager.onNoticeGameResult += NoticeGameResult;
			base.DdosEventManager.onNoticeSound += NoticeSound;
		}

		private void OnDestroy()
		{
			DLCEventManager.Instance.onNoticeBackGame -= NoticeBackGame;
			DLCEventManager.Instance.onNoticeGameSuccess -= NoticeGameSuccess;
			base.DdosEventManager.onNoticeGameResult -= NoticeGameResult;
			base.DdosEventManager.onNoticeSound -= NoticeSound;
			base.DdosManager.Destroy();
		}

		private void NoticeSound(DdosSound obj)
		{
			AudioClip audioClip = LoadSound(obj);
			GameManager.soundManager.PlayAudioClip(audioClip);
		}

		private void ShowResultDialog()
		{
			bool flag = _gameResult == GameResult.SUCCESS;
			float num = (float)(Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds) - _startTime) / 60f;
			Debug.Log("ShowResultDialog游戏时间：" + num);
			if (!flag)
			{
				Debug.Log("失败");
				if (_lv == 3)
				{
					GameManager.player.playerdata.HackerDlc7["ddos"] = false;
				}
				List<Enemy> enemies = base.DdosManager.Enemies;
				for (int i = 0; i < enemies.Count; i++)
				{
					Enemy enemy = enemies[i];
					if (enemy != null)
					{
						enemy.Win();
					}
				}
			}
			else
			{
				if (base.DdosManager.Level.lv == 3)
				{
					UnityEngine.Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), base.transform).InitData("^110008_game_127", delegate
					{
						GetComponentInParent<DDOSGameCanvas>().ShowTitan();
					});
					return;
				}
				Debug.Log("成功");
				if (_lv == 1 && num < 4f)
				{
					GameManager.UnlockAchievements("ddos");
				}
			}
			resultDialog.gameObject.SetActive(value: true);
			resultDialog.Show(flag);
		}

		private void NoticeGameResult(GameResult obj)
		{
			Debug.Log("controller NoticeGameResult:" + obj);
			if (_gameResult == GameResult.GAMING)
			{
				_gameResult = obj;
				if (obj == GameResult.SUCCESS)
				{
					GameManager.player.playerdata.dlc7Invades[_lv - 1] = 2;
					GameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
				}
				Invoke("ShowResultDialog", 1.5f);
			}
		}

		private void NoticeGameSuccess()
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().musicManager.Stop();
			if (base.DdosManager.Level.lv == 3)
			{
				UnityEngine.Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), base.transform).InitData(I18N.instance.getValue("^110008_game_127"), delegate
				{
					GetComponentInParent<DDOSGameCanvas>().ShowTitan();
				});
			}
			else
			{
				SceneManager.LoadScene("homeDLC7");
			}
		}

		private void NoticeBackGame()
		{
			SceneManager.LoadScene("homeDLC7");
		}
	}
}
