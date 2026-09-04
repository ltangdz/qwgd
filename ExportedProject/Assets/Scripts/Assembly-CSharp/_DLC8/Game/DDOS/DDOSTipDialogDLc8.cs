using System;
using Aluba;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using Honeti;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Game.DDOS
{
	public class DDOSTipDialogDLc8 : MonoBehaviour
	{
		[Header("游戏结束界面")]
		public GameObject gameOverObj;

		public Text bestScoreText;

		public Text resourceText;

		public Text dbText;

		public Button gameOverButton;

		[Header("等级提升界面")]
		public GameObject levelUpObj;

		public Text levelText;

		public Text levelTipText;

		public Button levelUpButton;

		private DDOSGameControllerDLC8 _controller;

		private UnityAction _callback;

		private string[] _levelStrings = new string[5] { "C", "B", "A", "S", "Ω" };

		private SteamLeaderboard _leaderboard = new SteamLeaderboard();

		private void Start()
		{
			gameOverButton.onClick.AddListener(ClickButton);
			levelUpButton.onClick.AddListener(ClickButton);
		}

		private void ClickButton()
		{
			_callback?.Invoke();
			base.gameObject.SetActive(value: false);
		}

		public void ShowLevelUpTip(UnityAction callback)
		{
			_callback = callback;
			CityMapData dDOSCityMapData = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData();
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			int level = dDOSCityMapData.level;
			if (level != archiveData.DdosCurLevel)
			{
				gameOverObj.SetActive(value: false);
				levelUpObj.SetActive(value: true);
				base.gameObject.SetActive(value: true);
				levelText.text = _levelStrings[level];
				archiveData.DdosCurLevel = level;
				levelTipText.text = string.Format(I18N.instance.getValue("^110009_common_44"), dDOSCityMapData.cardMaxLevel);
			}
			else
			{
				callback?.Invoke();
			}
		}

		public void ShowGameOver(ObscuredInt deadCount, UnityAction callback)
		{
			DDOSOverWindowDlc8 component = gameOverObj.GetComponent<DDOSOverWindowDlc8>();
			levelUpObj.SetActive(value: false);
			base.gameObject.SetActive(value: true);
			_callback = callback;
			CityMapData dDOSCityMapData = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData();
			int num = Mathf.CeilToInt((float)((int)deadCount * dDOSCityMapData.bugCount) / 10f);
			int num2 = Mathf.CeilToInt((float)((int)deadCount * dDOSCityMapData.dataCount) / 10f);
			int score = Mathf.CeilToInt((int)deadCount * dDOSCityMapData.Score);
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			archiveData.ChangeResourceCount(num);
			archiveData.AddPersonData(num2);
			if (archiveData.DdosLevel.FirstScore == -1)
			{
				archiveData.DdosLevel.FirstScore = Mathf.Max(archiveData.DdosLevel.FirstScore, score);
			}
			_ = archiveData.DdosLevel.BestScore;
			archiveData.DdosLevel.BestScore = Mathf.Max(archiveData.DdosLevel.BestScore, score);
			if (score < 49000)
			{
				try
				{
					_leaderboard.Init("DDOSScore", isTime: false, delegate
					{
						_leaderboard.UpdateScore(score, null);
					});
				}
				catch (Exception ex)
				{
					Debug.LogError("_leaderboard:" + ex);
				}
			}
			if (score >= 12333)
			{
				DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.DDOS_40000);
			}
			component.Show(score, score > archiveData.DdosLevel.BestScore, num, num2);
		}
	}
}
