using System.Collections;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;
using _DLC8.Game.PublicOpinion;

namespace _DLC8.Main
{
	public class MainMapAttentionGroup : MonoBehaviour
	{
		public Image progressImage;

		public Image progressBgImage;

		public Text progressText;

		public ScrollRect scrollRect;

		public Transform scrollContent;

		public MonitoringTipItem itemPrefab;

		private float[] _percentages = new float[3] { 0.35f, 0.7f, 1f };

		private long _totalPerson;

		private long _negativePerson;

		private Color[] _colors = new Color[3]
		{
			Color.white,
			new Color(0.41568628f, 29f / 51f, 52f / 85f, 1f),
			new Color(0.5921569f, 13f / 51f, 0.21960784f, 1f)
		};

		private ArchiveData _archiveData;

		private bool _isShowAnimation;

		private void Start()
		{
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			StartBalance();
			int count = _archiveData.AttentionIds.Count;
			List<PublicOpinionInfo> list = new List<PublicOpinionInfo>();
			Dictionary<int, PublicOpinionInfo> otherData = SingletonAutoMono<DLC8DataController>.GetInstance().PublicOpinionInfoDataManager.otherData;
			for (int i = 0; i < count; i++)
			{
				int key = _archiveData.AttentionIds[i];
				if (otherData.ContainsKey(key))
				{
					list.Add(otherData[key]);
				}
			}
			StartCoroutine(AddNewHotNews(list));
		}

		public int ColorIndex(float progress)
		{
			int num = 0;
			if (progress <= _percentages[0])
			{
				return 0;
			}
			if (progress <= _percentages[1])
			{
				return 1;
			}
			return 2;
		}

		public void StartBalance()
		{
			long num = 0L;
			_totalPerson = 0L;
			foreach (PublicOpinionInitData value in _archiveData.PublicOpinionMapDataDic.Values)
			{
				num += value.negative;
				_totalPerson += value.total;
			}
			DOTween.To(() => _negativePerson, delegate(long x)
			{
				_negativePerson = x;
			}, num, 0.5f).SetEase(Ease.Linear).OnUpdate(delegate
			{
				float num2 = (float)_negativePerson * 1f / (float)_totalPerson;
				progressImage.fillAmount = num2;
				progressImage.color = _colors[ColorIndex(num2)];
				progressText.text = $"{Mathf.FloorToInt(num2 * 100f)}%";
			});
		}

		private void FirstSuccessBalance()
		{
			float num = Random.Range(0f, 1f);
			List<PublicOpinionInfo> list = new List<PublicOpinionInfo>();
			Dictionary<int, PublicOpinionInfo> otherData = SingletonAutoMono<DLC8DataController>.GetInstance().PublicOpinionInfoDataManager.otherData;
			int count = SingletonAutoMono<DLC8DataController>.GetInstance().HotNewsIdList.Count;
			if (count <= 0)
			{
				return;
			}
			int num2 = 0;
			if (num > 0.2f && num <= 0.3f)
			{
				num2 = 1;
			}
			else if ((double)num >= 0.5)
			{
				num2 = Mathf.Min(count, 2);
			}
			for (int i = 0; i < num2; i++)
			{
				int num3 = SingletonAutoMono<DLC8DataController>.GetInstance().HotNewsIdList[Random.Range(0, SingletonAutoMono<DLC8DataController>.GetInstance().HotNewsIdList.Count)];
				SingletonAutoMono<DLC8DataController>.GetInstance().HotNewsIdList.Remove(num3);
				if (otherData.ContainsKey(num3))
				{
					list.Add(otherData[num3]);
					_archiveData.AttentionIds.Add(num3);
				}
			}
			StartCoroutine(AddNewHotNews(list, isAnimation: true));
		}

		private IEnumerator AddNewHotNews(List<PublicOpinionInfo> infos, bool isAnimation = false)
		{
			for (int i = 0; i < infos.Count; i++)
			{
				PublicOpinionInfo publicOpinionInfo = infos[i];
				float num = Object.Instantiate(itemPrefab, scrollContent).InitData(publicOpinionInfo, isAnimation);
				yield return new WaitForSeconds(num + (isAnimation ? 0.5f : 0f));
			}
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
			DLC8EventManager.Instance.onNoticeControllerGameOver += NoticeControllerGameOver;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			switch (arg1)
			{
			case DLC8CommonEvent.FIRST_FINISH_GAME:
				_archiveData.ChangePublicOpinionData();
				FirstSuccessBalance();
				break;
			case DLC8CommonEvent.CLOSE_CONTENT:
				StartBalance();
				break;
			}
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeControllerGameOver -= NoticeControllerGameOver;
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
		}

		private void NoticeControllerGameOver(LevelRecord obj)
		{
			StartBalance();
		}
	}
}
