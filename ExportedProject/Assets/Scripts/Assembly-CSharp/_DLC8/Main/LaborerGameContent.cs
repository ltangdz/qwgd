using System;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Time;
using DG.Tweening;
using Honeti;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8.Common;
using _DLC8.Game;
using _DLC8.Game.DDOS;
using _DLC8.Game.Voice;
using _DLC8.Game.WaterPipe;

namespace _DLC8.Main
{
	public class LaborerGameContent : LaborerBaseContentDialog
	{
		public Text titleText;

		public Image iconImage;

		public Button closeButton;

		public Button restartButton;

		public Text areaText;

		public Text areaLevelText;

		public Text bestText;

		public Text curTimeText;

		public Text tipText;

		public Transform content;

		public Sprite[] gameIconSprites;

		public GameObject tipObj;

		public BaseStationManager baseStationPrefab;

		public WaterPipeManager waterPipeManagerPrefab;

		public VoicePrintPanelDLC8 voicePrefab;

		[Header("过关提示")]
		public Button tipConfirmButton;

		public Text timeText;

		public Text dataText;

		public MiniGameOverWindowDlc8 overWindow;

		private WaterPipeManager _curWaterPipeManager;

		private LevelRecord _levelRecord;

		private float _time;

		private int _level;

		private bool _isFinishedGame;

		private SteamLeaderboard _leaderboard = new SteamLeaderboard();

		private string[] _tipStrList = new string[4] { "^110009_common_108", "^110009_common_109", "^110009_common_110", "^110009_common_107" };

		private string _rankName = "";

		public void Show(LevelRecord levelRecord, UnityAction callback)
		{
			base.transform.DOScale(0f, 0f);
			tipObj.SetActive(value: false);
			_isFinishedGame = false;
			_time = 0f;
			string[] array = new string[5] { "C", "B", "A", "S", "Ω" };
			_levelRecord = levelRecord;
			titleText.text = I18N.instance.getValue(base.DataController.GetGameNameKey(_levelRecord.GameType));
			iconImage.sprite = gameIconSprites[(int)_levelRecord.GameType];
			Dictionary<string, CityMapData> cityMapDataDic = base.DataController.CityMapDataDic;
			areaText.text = I18N.instance.getValue(cityMapDataDic[_levelRecord.area].name);
			areaLevelText.text = $"{array[_levelRecord.MapLevel]}-{_levelRecord.Level + 1}";
			int num = 0;
			switch (_levelRecord.GameType)
			{
			case CityGameType.VOICE:
				num = 2;
				break;
			case CityGameType.VIRUS:
			case CityGameType.BASE_STATION:
				num = 5;
				break;
			case CityGameType.WATER_PIPE:
				num = 8;
				break;
			}
			_level = num * _levelRecord.MapLevel + _levelRecord.Level;
			_rankName = SingletonAutoMono<DLC8DataController>.GetInstance().GetRankName(_levelRecord.GameType);
			if (_levelRecord.GameType != CityGameType.DDOS && _levelRecord.GameType != CityGameType.PUBLIC_OPINION)
			{
				_rankName += _level;
			}
			if (_levelRecord.BestScore > 0)
			{
				bestText.text = _levelRecord.GetTimeScoreString(isBestScore: true);
			}
			else
			{
				bestText.text = I18N.instance.getValue("^career_platform0303");
			}
			curTimeText.text = string.Format("{0}'{1}\"", "00", "00");
			closeButton.onClick.AddListener(Close);
			tipConfirmButton.onClick.AddListener(GameOver);
			restartButton.onClick.AddListener(Restart);
			Restart();
			base.gameObject.SetActive(value: true);
			base.transform.DOScale(1f, 0.15f).SetEase(Ease.Linear).OnComplete(delegate
			{
				callback?.Invoke();
			});
		}

		private void AddGamePanel()
		{
			switch (_levelRecord.GameType)
			{
			case CityGameType.VOICE:
				UnityEngine.Object.Instantiate(voicePrefab, content.transform).InitData(_levelRecord);
				break;
			case CityGameType.VIRUS:
				UnityEngine.Object.Instantiate(Resources.Load<ZhadanDialog>($"_DLC8/Virus/Virus{(_level + 1).ToString().PadLeft(2, '0')}"), content.transform);
				break;
			case CityGameType.WATER_PIPE:
				_curWaterPipeManager = UnityEngine.Object.Instantiate(waterPipeManagerPrefab, content.transform);
				_curWaterPipeManager.InitData(_level);
				break;
			case CityGameType.BASE_STATION:
				UnityEngine.Object.Instantiate(baseStationPrefab, content.transform).Init(_level + 1);
				break;
			}
			tipText.text = I18N.instance.getValue(_tipStrList[(int)_levelRecord.GameType]);
		}

		private void Restart()
		{
			if (!_isFinishedGame)
			{
				_ = content.childCount;
				for (int i = 0; i < content.childCount; i++)
				{
					UnityEngine.Object.Destroy(content.GetChild(i).gameObject);
				}
				AddGamePanel();
			}
		}

		private void Update()
		{
			if (!_isFinishedGame)
			{
				_time += SpeedHackProofTime.deltaTime;
				int num = Mathf.FloorToInt(_time);
				string arg = (num / 60).ToString().PadLeft(2, '0');
				string arg2 = (num % 60).ToString().PadLeft(2, '0');
				curTimeText.text = $"{arg}'{arg2}\"";
			}
		}

		private void Close()
		{
			if (_isFinishedGame)
			{
				return;
			}
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLOSE_DIALOG);
			overWindow.transform.DOScaleY(0f, 0.05f).OnComplete(delegate
			{
				if (_curWaterPipeManager != null)
				{
					_curWaterPipeManager.isOver = true;
				}
				base.transform.DOScale(0f, 0.15f).OnComplete(delegate
				{
					UnityEngine.Object.Destroy(base.gameObject);
					_curWaterPipeManager = null;
				});
			});
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
			NoticeCloseContent();
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			if (arg1 != DLC8CommonEvent.FINISH_GAMME)
			{
				return;
			}
			_isFinishedGame = true;
			ObscuredInt time = Mathf.FloorToInt(_time);
			try
			{
				_leaderboard.Init(_rankName, isTime: true, delegate
				{
					_leaderboard.UpdateScore(time, null);
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("ddos分数上传错误:" + ex);
			}
			int num = _levelRecord.FinishedLevel(time);
			tipObj.SetActive(value: true);
			overWindow.Show(curTimeText.text, num.ToString());
		}

		private void GameOver()
		{
			_isFinishedGame = false;
			DLC8EventManager.Instance.NoticeControllerGameOver(_levelRecord);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
			Close();
		}
	}
}
