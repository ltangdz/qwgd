using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class CityMap : MonoBehaviour
	{
		public string cityName;

		public List<Image> bgImages;

		public Transform buttonContent;

		private List<LaborerLevelButton> _gameButtonList = new List<LaborerLevelButton>();

		public MapLockGroup lockGroup;

		public Text progressText;

		private int _level;

		private Sequence _flickerSequence;

		private bool _isSelected;

		private CityMapData _cityMapData;

		private DLC8DataController _dlc8Controller;

		private bool _isUnlock;

		public LaborerLevelButton laborerLevelButtonPrefab;

		private LaborerMapEnum _mapEnum;

		private ArchiveData _archiveData;

		private List<LaborerLevelButton> _buttonList = new List<LaborerLevelButton>();

		private int _mapProgress;

		public List<LaborerLevelButton> ButtonList => _buttonList;

		private void Start()
		{
			_dlc8Controller = SingletonAutoMono<DLC8DataController>.GetInstance();
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			_mapEnum = _archiveData.GetLaborerMapEnum(cityName);
			_cityMapData = _dlc8Controller.CityMapDataDic[cityName];
			_isUnlock = _dlc8Controller.ArchiveData.IsUnlockMap(cityName);
			if (_isUnlock)
			{
				bgImages[0].GetComponent<CanvasGroup>().DOFade(1f, 0f);
				bgImages[1].DOFade(0f, 0f);
				lockGroup.gameObject.SetActive(value: false);
				base.transform.SetAsLastSibling();
				InitLevelButton();
			}
			else
			{
				bgImages[1].DOFade(1f, 0f);
				bgImages[0].GetComponent<CanvasGroup>().DOFade(0f, 0f);
				lockGroup.Init(_cityMapData);
			}
			_mapProgress = _archiveData.GetMapProgress(cityName);
			progressText.text = $"{_mapProgress}%";
			GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
			GetComponent<Button>().onClick.AddListener(ClickMap);
		}

		public void InitLevelButton()
		{
			for (int i = 0; i < 4; i++)
			{
				LaborerLevelButton item = Object.Instantiate(laborerLevelButtonPrefab, buttonContent);
				_buttonList.Add(item);
			}
			for (int j = 0; j < _buttonList.Count; j++)
			{
				CityGameType gameType = (CityGameType)j;
				LevelRecord newestLevelRecord = _archiveData.GetNewestLevelRecord(_mapEnum, gameType);
				_buttonList[j].Show(gameType, _cityMapData.levelCost, newestLevelRecord, isHideAnimation: false, bgImages[0].sprite, _buttonList);
			}
		}

		private void ClickMap()
		{
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			if (arg1 == DLC8CommonEvent.UNLOCK_MAP && arg2 == (int)_mapEnum)
			{
				bgImages[0].GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.Linear);
				bgImages[1].DOFade(0f, 1f).SetEase(Ease.Linear);
				Invoke("InitLevelButton", 0.5f);
			}
			if (arg1 == DLC8CommonEvent.CLOSE_CONTENT)
			{
				DOTween.To(() => _mapProgress, delegate(int x)
				{
					_mapProgress = x;
				}, _archiveData.GetMapProgress(cityName), 0.3f).SetEase(Ease.Linear).OnUpdate(delegate
				{
					progressText.text = $"{_mapProgress}%";
				});
			}
		}
	}
}
