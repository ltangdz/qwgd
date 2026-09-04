using System;
using System.Collections;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using Honeti;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;
using _DLC8.Main;

namespace _DLC8.Game.DDOS
{
	public class DDOSGameControllerDLC8 : DDosMonoBehaviourDLC8
	{
		public List<BagGridDLC8> bagGrids;

		public List<BagGridDLC8> attackerGrids;

		public SpriteAtlas ddosAtlas;

		public SpriteAtlas ddosShaderTextureAtlas;

		public Text safeLevelText;

		public Text bestScoreText;

		public Text waveCountText;

		public Text curScoreText;

		public Button exitButton;

		public DDOSTipDialogDLc8 tipDialog;

		public CanvasGroup warningGroup;

		[Header("游戏数值")]
		public string cardDamageStr;

		public string queenStr;

		public string wallStr;

		public string enemyBaseStr;

		public string hardStr1;

		public string hardStr2;

		public string hardStr3;

		public List<string> doubleWaves;

		public List<DDosLevelDLC8> levels;

		public DanielEmail emailGroup;

		public Transform coinTransform;

		[SerializeField]
		private int _lv = 1;

		public List<GameObject> teachGroups;

		public Button _teachButton;

		public GameObject enemyArea;

		[FormerlySerializedAs("resultDialog")]
		public DDOSResultDialogDLC8 resultDialogDlc8;

		private int _curTeachIndex;

		private int _waves;

		private string _safeLvString = "";

		private GameResult _gameResult = GameResult.GAMING;

		private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

		private GameManager _gameManager;

		private long _startTime;

		private bool _isDouble;

		private ObscuredInt _deadCount = 0;

		private bool _isShowResultDialog;

		public Button skipButton;

		private CityMapData _cityMapData;

		private string _curScoreString;

		private string _maxScoreString;

		private SteamLeaderboard _leaderboard = new SteamLeaderboard();

		private int[] _teachCountList = new int[3] { 5, 2, 1 };

		private int _teachCount;

		private int _curWaveNumber;

		private bool _isStart;

		private string[] soundStrs = new string[15]
		{
			"ClickCoin", "CoinShow", "EnemyDead", "GetCard", "hecheng", "lajitong", "NoBuy", "OurHurt", "Win", "Shoot",
			"Normal", "Fire", "Ice", "Palsy", "Fail"
		};

		public string CurScoreString
		{
			get
			{
				if (string.IsNullOrEmpty(_curScoreString))
				{
					_curScoreString = I18N.instance.getValue("^110009_common_40");
				}
				return _curScoreString;
			}
		}

		public string MAXScoreString
		{
			get
			{
				if (string.IsNullOrEmpty(_maxScoreString))
				{
					_maxScoreString = I18N.instance.getValue("^110009_common_11");
				}
				return _maxScoreString;
			}
		}

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

		private void Start()
		{
			GameManager.musicManager.PlayMusicLoop(24);
			exitButton.onClick.AddListener(Exit);
			SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.DdosLevel.MapLevel = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.DDOSMaxMapIndex();
			_cityMapData = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData();
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
			InitData(_cityMapData.ddosLevel);
		}

		private void Exit()
		{
			if (_isStart)
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
				base.DdosEventManagerDlc8.NoticeGameResult(GameResult.SUCCESS);
				_gameResult = GameResult.SUCCESS;
			}
		}

		public void InitData(int lv)
		{
			Debug.Log("InitData：" + lv);
			_lv = lv;
			InitWaveText();
			string[] array = new string[5] { "C", "B", "A", "S", "Ω" };
			bestScoreText.text = $"{MAXScoreString}:{0}";
			curScoreText.text = $"{CurScoreString}:{0}";
			safeLevelText.text = $"{SafeLvString}:{array[SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.DdosLevel.MapLevel]}";
			InitDDosManagerData();
			Invoke("InitAttackerRound", 1f);
			InvokeRepeating("QueenTransfer", 3f, 0.3f);
			ShowTeaching();
			if (skipButton != null)
			{
				skipButton.onClick.AddListener(Skip);
			}
		}

		private void InitWaveText()
		{
			if (base.DdosManagerDlc8.Lv > _curWaveNumber)
			{
				_curWaveNumber = base.DdosManagerDlc8.Lv;
				if (_curWaveNumber > 1)
				{
					warningGroup.alpha = 0f;
					RectTransform component = warningGroup.transform.GetComponent<RectTransform>();
					component.anchoredPosition = new Vector2(0f, 195f);
					Sequence sequence = DOTween.Sequence();
					sequence.Append(warningGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear));
					sequence.AppendInterval(2f);
					sequence.Append(warningGroup.DOFade(0f, 0.5f).SetEase(Ease.Linear));
					sequence.Play();
					component.DOAnchorPosY(368f, 3f).SetEase(Ease.Linear).OnComplete(delegate
					{
						waveCountText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_162"), base.DdosManagerDlc8.Lv);
					});
				}
			}
			else
			{
				waveCountText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_162"), base.DdosManagerDlc8.Lv);
			}
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
			_teachCount = 0;
			if (SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.UnlockedDdosTeachList[_lv - 1])
			{
				_teachButton.gameObject.SetActive(value: false);
				Invoke("StartGame", 2f);
				return;
			}
			if (_lv == 1)
			{
				_curTeachIndex = 0;
			}
			else if (_lv == 2)
			{
				_curTeachIndex = 5;
			}
			else if (_lv == 3)
			{
				_curTeachIndex = 7;
			}
			teachGroups[_curTeachIndex].SetActive(value: true);
			_teachButton.onClick.AddListener(TeachingNext);
			_startTime = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		}

		private void TeachingNext()
		{
			int num = _teachCountList[_lv - 1];
			if (_teachCount < num)
			{
				if (_curTeachIndex - 1 >= 0)
				{
					teachGroups[_curTeachIndex - 1].SetActive(value: false);
				}
				teachGroups[_curTeachIndex].SetActive(value: true);
				_curTeachIndex++;
				_teachCount++;
			}
			else
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.UnlockedDdosTeachList[_lv - 1] = true;
				_teachButton.gameObject.SetActive(value: false);
				Invoke("StartGame", 2f);
			}
		}

		private void StartGame()
		{
			if (!SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.danielEmailFinishedList[0])
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.danielEmailFinishedList[0] = true;
				emailGroup.ShowAnimation();
				emailGroup.closeCallback = delegate
				{
					StartCoroutine("Waves");
				};
			}
			else
			{
				tipDialog.ShowLevelUpTip(delegate
				{
					tipDialog.gameObject.SetActive(value: false);
					StartCoroutine("Waves");
				});
			}
		}

		private void QueenTransfer()
		{
			List<BagGridDLC8> list = new List<BagGridDLC8>();
			for (int i = 0; i < attackerGrids.Count; i++)
			{
				BagGridDLC8 bagGridDLC = attackerGrids[i];
				CardItemDLC8 curCardItemDlc = bagGridDLC.CurCardItemDlc8;
				if (!(curCardItemDlc == null))
				{
					curCardItemDlc.QueenTransfer(0);
					if (curCardItemDlc.CardDlc8.Type == CardType.QUEEN && curCardItemDlc.CardDlc8.Intensify == IntensifyType.TRANSFER)
					{
						list.Add(bagGridDLC);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				BagGridDLC8 bagGridDLC2 = list[j];
				CardItemDLC8 curCardItemDlc2 = bagGridDLC2.CurCardItemDlc8;
				if (curCardItemDlc2.CardDlc8.Type != CardType.QUEEN)
				{
					continue;
				}
				int queenBuff = curCardItemDlc2.CardDlc8.QueenBuff;
				if (curCardItemDlc2 == null || curCardItemDlc2.CardDlc8.Intensify != IntensifyType.TRANSFER)
				{
					continue;
				}
				List<BagGridDLC8> roundGrids = bagGridDLC2.RoundGrids;
				for (int k = 0; k < roundGrids.Count; k++)
				{
					CardItemDLC8 curCardItemDlc3 = roundGrids[k].CurCardItemDlc8;
					if (!(curCardItemDlc3 == null))
					{
						curCardItemDlc3.QueenTransfer(queenBuff);
					}
				}
			}
		}

		private IEnumerator Waves()
		{
			_isStart = true;
			DDosLevelDLC8 levelDlc = base.DdosManagerDlc8.LevelDlc8;
			List<int> enemyIds = levelDlc.enemyIds;
			List<List<WavesDLC8>> list = (_isDouble ? levelDlc.doubleWavesList : levelDlc.wavesList);
			List<Dictionary<string, string>> enemyDataList = base.DdosManagerDlc8.enemyBaseDatas;
			int num = 0;
			for (int i = 0; i < list[_waves].Count; i++)
			{
				List<WaveDLC8> waves = list[_waves][i].waves;
				for (int j = 0; j < waves.Count; j++)
				{
					WaveDLC8 waveDLC = waves[j];
					num += waveDLC.count;
				}
			}
			if (_gameResult == GameResult.FAIL || _gameResult == GameResult.SUCCESS)
			{
				StopCoroutine("Waves");
				yield break;
			}
			base.DdosEventManagerDlc8.NoticeGameWaves(_isDouble ? GameWavesType.DOUBLE : GameWavesType.START, num);
			List<WavesDLC8> lvWaves = list[_waves];
			base.DdosManagerDlc8.Lv = _waves + 1;
			base.DdosEventManagerDlc8.NoticeLevel(_waves + 1);
			yield return new WaitForSeconds(1f);
			InitWaveText();
			Debug.LogError("当前波次等级：" + (_waves + 1));
			bool hasBoss = (_waves + 1) % 3 == 0;
			if (_isDouble)
			{
				yield return new WaitForSeconds(3f);
			}
			else if (_waves != 0)
			{
				yield return new WaitForSeconds(2f);
			}
			for (int l = 0; l < lvWaves.Count; l++)
			{
				if (_gameResult == GameResult.FAIL || _gameResult == GameResult.SUCCESS)
				{
					StopCoroutine("Waves");
					break;
				}
				WavesDLC8 wavesDLC = lvWaves[l];
				float interval = wavesDLC.interval;
				List<WaveDLC8> levelWavesWaves = wavesDLC.waves;
				bool isLast3 = l == lvWaves.Count - 1;
				for (int k = 0; k < levelWavesWaves.Count; k++)
				{
					if (_gameResult == GameResult.FAIL || _gameResult == GameResult.SUCCESS)
					{
						StopCoroutine("Waves");
						yield break;
					}
					WaveDLC8 waveDLC2 = levelWavesWaves[k];
					int waveLv = waveDLC2.lv;
					int waveCount = waveDLC2.count;
					bool isLast4 = k == levelWavesWaves.Count - 1;
					for (int m = 0; m < waveCount; m++)
					{
						if (_gameResult == GameResult.SUCCESS)
						{
							StopCoroutine("Waves");
							yield break;
						}
						if (_gameResult == GameResult.FAIL)
						{
							StopCoroutine("Waves");
							yield break;
						}
						float num2 = UnityEngine.Random.Range(0.1f, 0.3f);
						interval -= num2;
						if (interval < 0f)
						{
							interval = 0f;
						}
						Dictionary<string, string> enemyDic = enemyDataList[waveLv - 1];
						if (_gameResult != GameResult.SUCCESS && _gameResult != GameResult.FAIL)
						{
							EnemyDLC8 component = base.DdosManagerDlc8.SpawnPool.Spawn("DDOSEnemyDLC8").GetComponent<EnemyDLC8>();
							int index = UnityEngine.Random.Range(0, enemyIds.Count);
							bool flag = m == waveCount - 1;
							bool isBoss = hasBoss && isLast4 && flag && isLast3;
							component.InitData(enemyDic, isBoss, (EnemyType)enemyIds[index], enemyArea.transform);
							base.DdosManagerDlc8.Enemies.Add(component);
							if (_gameResult == GameResult.FAIL)
							{
								component.Win();
							}
							yield return new WaitForSeconds((_isDouble && interval == 0f) ? 0.1f : UnityEngine.Random.Range(0.7f, 1.1f));
						}
					}
				}
				yield return new WaitForSeconds(interval);
			}
		}

		private void InitAttackerRound()
		{
			base.DdosManagerDlc8.bagGrids = bagGrids;
			base.DdosManagerDlc8.ddosAtlas = ddosAtlas;
			base.DdosManagerDlc8.DdosTextureAtlas = ddosAtlas;
			for (int i = 0; i < attackerGrids.Count; i++)
			{
				BagGridDLC8 bagGridDLC = attackerGrids[i];
				for (int j = 0; j < attackerGrids.Count; j++)
				{
					BagGridDLC8 bagGridDLC2 = attackerGrids[j];
					if (i != j && bagGridDLC.AreaRect.Overlaps(bagGridDLC2.AreaRect))
					{
						bagGridDLC.RoundGrids.Add(bagGridDLC2);
					}
				}
			}
		}

		private void InitDDosManagerData()
		{
			base.DdosManagerDlc8.InitCardDamaged(cardDamageStr, queenStr, wallStr, enemyBaseStr, hardStr1, hardStr2, hardStr3, levels, _lv, enemyArea.transform, coinTransform, doubleWaves[_lv - 1]);
		}

		private void Skip()
		{
			base.DdosEventManagerDlc8.NoticeGameResult(GameResult.SUCCESS);
		}

		private void Awake()
		{
			base.DdosEventManagerDlc8.onNoticeGameResult += NoticeGameResult;
			base.DdosEventManagerDlc8.onNoticeGameWaves += NoticeGameWaves;
			base.DdosEventManagerDlc8.onNoticeSound += NoticeSound;
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticeGameResult -= NoticeGameResult;
			base.DdosEventManagerDlc8.onNoticeGameWaves -= NoticeGameWaves;
			base.DdosEventManagerDlc8.onNoticeSound -= NoticeSound;
			base.DdosManagerDlc8.Destroy();
		}

		private void NoticeGameWaves(GameWavesType arg1, int arg2)
		{
			switch (arg1)
			{
			case GameWavesType.ENEMY_DEAD:
				if (!_isShowResultDialog)
				{
					++_deadCount;
					int score = _cityMapData.Score;
					int num = Mathf.CeilToInt((float)((int)_deadCount * _cityMapData.bugCount) / 10f);
					curScoreText.text = $"{CurScoreString}:{(score * (int)_deadCount).ToString()}";
					bestScoreText.text = $"{MAXScoreString}:{num}";
				}
				break;
			case GameWavesType.FINISH_NORAML:
				if (!_isShowResultDialog)
				{
					_isDouble = true;
					StartCoroutine("Waves");
				}
				break;
			case GameWavesType.FINISH_DOUBLE:
				if (!_isShowResultDialog)
				{
					if (base.DdosManagerDlc8.LevelDlc8.wavesList.Count > _waves + 1)
					{
						_waves++;
					}
					_isDouble = false;
					StartCoroutine("Waves");
				}
				break;
			}
		}

		private void NoticeSound(DdosSound obj)
		{
			AudioClip audioClip = LoadSound(obj);
			GameManager.soundManager.PlayAudioClip(audioClip);
		}

		private void ShowResultDialog()
		{
			tipDialog.ShowGameOver(_deadCount, delegate
			{
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CLOSE_CONTENT, 0);
				SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
				DLC8EventManager.Instance.NoticeControllerGameOver(SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.DdosLevel);
				UnityEngine.Object.Destroy(base.gameObject);
			});
		}

		private void NoticeGameResult(GameResult obj)
		{
			if (!_isShowResultDialog)
			{
				_isShowResultDialog = true;
				ShowResultDialog();
			}
		}
	}
}
