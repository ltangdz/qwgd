using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Aluba;
using DLC7;
using Honeti;
using Steamworks;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main.Rank
{
	public class LaborerRankGroup : MonoBehaviour
	{
		public List<LaborerRankItem> defaultRankItems;

		public Text rankText;

		public Text titleText;

		public Text scoreText;

		public Text nameText;

		private List<LaborerRankItem> _allRankItemList = new List<LaborerRankItem>();

		public LaborerRankItem rankItemPrefab;

		public Transform rankContent;

		public Text uploadText;

		public GameObject loadingGroup;

		public FrameAnimation2D animation2d;

		private List<LaborerRankData> _rankDataList = new List<LaborerRankData>();

		private CityGameType _cityGameType = CityGameType.DDOS;

		private LaborerRankData _userRankData;

		private SteamLeaderboard _steamLeaderboard = new SteamLeaderboard();

		private bool _isTimeRank;

		private bool _isShowLoading;

		public void Show(CityGameType gameType, LevelRecord levelRecord)
		{
			titleText.text = string.Format("{0}{1}", I18N.instance.getValue("^Groomusic0402"), I18N.instance.getValue("^110009_common_102"));
			Debug.LogError("bLoggedOn:" + SteamUser.BLoggedOn());
			string rankName = SingletonAutoMono<DLC8DataController>.GetInstance().GetRankName(gameType);
			ShowLoading();
			_isTimeRank = gameType != CityGameType.DDOS;
			_userRankData = new LaborerRankData();
			try
			{
				_userRankData.Init(0, SteamFriends.GetPersonaName(), 0, _isTimeRank);
				if (levelRecord.GameType != CityGameType.DDOS && levelRecord.GameType != CityGameType.PUBLIC_OPINION)
				{
					rankName += levelRecord.MapLevel * levelRecord.Level + levelRecord.Level;
				}
				_steamLeaderboard.Init(rankName, _isTimeRank, delegate(bool result)
				{
					if (result)
					{
						_steamLeaderboard.DownloadUser(delegate(List<LaborerRankData> data)
						{
							if (data.Count > 0)
							{
								_userRankData.rank = data[0].rank;
								_userRankData.scoreString = data[0].scoreString;
								_userRankData.score = data[0].score;
								InitUser();
							}
							_steamLeaderboard.Init(rankName, _isTimeRank, delegate
							{
								_steamLeaderboard.DownloadRank(50, delegate(List<LaborerRankData> list)
								{
									HideLoading();
									_rankDataList.Clear();
									_rankDataList.AddRange(list);
									InitList();
								});
							});
						});
					}
					else
					{
						HideLoading();
					}
				});
			}
			catch (Exception)
			{
				HideLoading();
			}
			if (_allRankItemList.Count == 0)
			{
				for (int num = 0; num < defaultRankItems.Count; num++)
				{
					_allRankItemList.Add(defaultRankItems[num]);
				}
			}
			InitUser();
			InitList();
		}

		private void ShowLoading()
		{
			loadingGroup.SetActive(value: true);
			animation2d.Play();
			_isShowLoading = true;
			StartCoroutine("LoadingTextAnimation");
		}

		private void HideLoading()
		{
			_isShowLoading = false;
			if (base.gameObject.activeInHierarchy)
			{
				StopCoroutine("LoadingTextAnimation");
			}
			loadingGroup.SetActive(value: false);
		}

		private IEnumerator LoadingTextAnimation()
		{
			StringBuilder builder = new StringBuilder(I18N.instance.getValue("^110009_common_91"));
			uploadText.text = builder.ToString();
			yield return new WaitForSeconds(0.5f);
			while (_isShowLoading)
			{
				for (int i = 0; i < 4; i++)
				{
					builder.Append(".");
					uploadText.text = builder.ToString();
					yield return new WaitForSeconds(0.5f);
				}
			}
		}

		private void InitList()
		{
			int num = _rankDataList.Count - _allRankItemList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					LaborerRankItem item = UnityEngine.Object.Instantiate(rankItemPrefab, rankContent);
					_allRankItemList.Add(item);
				}
			}
			for (int j = 0; j < _allRankItemList.Count; j++)
			{
				LaborerRankItem laborerRankItem = _allRankItemList[j];
				if (_rankDataList.Count > j)
				{
					LaborerRankData data = _rankDataList[j];
					laborerRankItem.Init(data);
					laborerRankItem.gameObject.SetActive(value: true);
				}
				else
				{
					laborerRankItem.gameObject.SetActive(value: false);
				}
			}
		}

		private void InitUser()
		{
			nameText.text = _userRankData.nameString;
			rankText.text = ((_userRankData.rank == 0) ? I18N.instance.getValue("^110009_common_93") : _userRankData.rank.ToString());
			if (_userRankData.score == 0)
			{
				scoreText.text = I18N.instance.getValue("^110009_common_94");
			}
			else
			{
				scoreText.text = _userRankData.scoreString;
			}
		}
	}
}
