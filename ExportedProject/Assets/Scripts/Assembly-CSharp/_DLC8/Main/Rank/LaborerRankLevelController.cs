using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main.Rank
{
	public class LaborerRankLevelController : LaborerBaseContentDialog
	{
		public Image iconImage;

		public RectTransform contentRT;

		public Text titleText;

		public Transform content;

		public LaborerLevelItem levelItemPrefab;

		public LaborerRankGroup rankGroup;

		public Button closeButton;

		public Sprite[] iconSprites;

		private bool _isNoticeClose;

		private List<LaborerLevelItem> _items = new List<LaborerLevelItem>();

		private CityGameType _gameType;

		public CityGameType GameType => _gameType;

		public void CloseAnimation()
		{
			GameObject o = base.gameObject;
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleX(0f, 0.3f).OnComplete(delegate
				{
					contentRT.DOScaleY(0f, 0f);
					o.transform.DOScale(0f, 0f);
					Object.Destroy(o);
				});
			});
		}

		public void ShowAnimation()
		{
			base.gameObject.transform.DOScale(1f, 0f);
			contentRT.DOScale(0f, 0f);
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0f);
			contentRT.DOScaleX(2f / contentRT.sizeDelta.x, 0f);
			contentRT.DOScaleX(1f, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleY(1f, 0.3f).OnComplete(delegate
				{
				});
			});
		}

		public void Show(CityGameType gameType)
		{
			closeButton.onClick.AddListener(Hide);
			List<LevelRecord> levelRecordListByCityGameType = base.ArchiveData.GetLevelRecordListByCityGameType(gameType);
			_gameType = gameType;
			titleText.text = I18N.instance.getValue(base.DataController.GetGameNameKey(_gameType));
			iconImage.sprite = iconSprites[(int)_gameType];
			_items.Clear();
			for (int i = 0; i < content.childCount; i++)
			{
				Object.Destroy(content.GetChild(i).gameObject);
			}
			for (int j = 0; j < levelRecordListByCityGameType.Count; j++)
			{
				LevelRecord levelRecord = levelRecordListByCityGameType[j];
				if (!levelRecord.isUnlock)
				{
					continue;
				}
				LaborerLevelItem laborerLevelItem = Object.Instantiate(levelItemPrefab, content);
				int count = _items.Count;
				_items.Add(laborerLevelItem);
				laborerLevelItem.InitData(levelRecord, delegate(LaborerLevelItem item)
				{
					for (int k = 0; k < _items.Count; k++)
					{
						LaborerLevelItem laborerLevelItem2 = _items[k];
						if (laborerLevelItem2 == item)
						{
							rankGroup.Show(gameType, laborerLevelItem.LevelRecord);
						}
						else
						{
							laborerLevelItem2.Enter(isEnter: false);
						}
					}
				});
				if (count == 0)
				{
					laborerLevelItem.Enter(isEnter: true);
					rankGroup.Show(gameType, laborerLevelItem.LevelRecord);
				}
			}
			ShowAnimation();
		}

		public void Hide()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLOSE_DIALOG);
			CloseAnimation();
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			if (arg1 == DLC8CommonEvent.START_GAME)
			{
				_isNoticeClose = false;
				Hide();
			}
			else
			{
				_isNoticeClose = true;
			}
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
			NoticeCloseContent();
		}
	}
}
