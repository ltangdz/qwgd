using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Card
{
	public class WorkCardGroup : MonoBehaviour
	{
		public WorkCard workCard;

		public GameObject content;

		public CanvasGroup canvasGroup;

		public CompleteCard completeCard;

		public Button closeButton;

		public Button nextButton;

		public Button lastButton;

		public Button downloadButton;

		[Header("第一次展示用的")]
		public RectTransform firstCardRT;

		public CompleteCard firstCompleteCard;

		public WorkCard firstWorkCard;

		public Button firstButton;

		private float _workCardScaleY = 0.7f;

		private float _completeCardScaleY = 0.24f;

		private int _index;

		private bool _isAnimation;

		private ArchiveData _archiveData;

		private PrintPrefabType _completeCardType;

		private int _firstIndex;

		private bool _firstAnimation;

		private Transform _buttonTransform;

		public ArchiveData ArchiveData
		{
			get
			{
				if (_archiveData == null)
				{
					_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
				}
				return _archiveData;
			}
		}

		public void ShowFirst(Transform buttonTransform)
		{
			_buttonTransform = buttonTransform;
			firstButton.interactable = true;
			firstButton.onClick.AddListener(FirstNext);
			base.gameObject.SetActive(value: true);
			content.SetActive(value: false);
			StageClearType stageClearState = ArchiveData.GetStageClearState();
			_completeCardType = PrintPrefabType.STAGE_CLEAR_NORMAL;
			firstCompleteCard.gameObject.SetActive(value: true);
			if (stageClearState == StageClearType.PERFECT)
			{
				_completeCardType = PrintPrefabType.STAGE_CLEAR_PERFECT;
			}
			firstCompleteCard.Show(_completeCardType, isPrint: false);
			firstCardRT.DOScaleY(0.29f, 0.5f);
		}

		private void FirstNext()
		{
			if (_firstAnimation || _firstIndex > 3)
			{
				return;
			}
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(61);
			_firstAnimation = true;
			_firstIndex++;
			if (_firstIndex == 1)
			{
				firstCardRT.DOScaleY(0f, 0.5f).OnComplete(delegate
				{
					firstWorkCard.Show(PrintPrefabType.WORK_CARD_FRONT, isPrint: false);
					firstWorkCard.transform.DOScaleX(1f, 0f).OnComplete(delegate
					{
						firstWorkCard.transform.DOScaleY(1f, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
						{
							_firstAnimation = false;
						});
					});
				});
			}
			else if (_firstIndex == 2)
			{
				firstWorkCard.transform.DOScaleY(0f, 0.5f).OnComplete(delegate
				{
					firstWorkCard.Show(PrintPrefabType.WORK_CARD_BACK, isPrint: false);
					firstWorkCard.transform.DOScaleX(1f, 0f).OnComplete(delegate
					{
						firstWorkCard.transform.DOScaleY(1f, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
						{
							_firstAnimation = false;
						});
					});
				});
			}
			else
			{
				firstWorkCard.transform.DOMove(_buttonTransform.position, 0.5f).SetEase(Ease.Linear);
				firstWorkCard.transform.DOScale(0f, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
				{
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_CLEAR_STAGE_BUTTON, 0);
					base.gameObject.SetActive(value: false);
				});
			}
		}

		private void Next()
		{
			if (_isAnimation)
			{
				return;
			}
			_isAnimation = true;
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(61);
			GetCardTransform().DOScaleY(0f, 0.38f).OnComplete(delegate
			{
				_index++;
				if (_index > 2)
				{
					_index = 0;
				}
				NoticeShowPrintPrefab();
				Invoke("ShowCard", 0.38f);
			});
		}

		private void Last()
		{
			if (_isAnimation)
			{
				return;
			}
			_isAnimation = true;
			GetCardTransform().DOScaleY(0f, 0.38f).OnComplete(delegate
			{
				_index--;
				if (_index < 0)
				{
					_index = 2;
				}
				NoticeShowPrintPrefab();
				Invoke("ShowCard", 0.38f);
			});
		}

		private void NoticeShowPrintPrefab()
		{
			PrintPrefabType printPrefabType = _completeCardType;
			if (_index == 1)
			{
				printPrefabType = PrintPrefabType.WORK_CARD_FRONT;
			}
			else if (_index == 2)
			{
				printPrefabType = PrintPrefabType.WORK_CARD_BACK;
			}
			Debug.LogError("NoticeShowPrintPrefab:" + printPrefabType);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CHANGE_PRINT_PREFAB, (int)printPrefabType);
		}

		private void ShowCard()
		{
			GetCardTransform().DOScaleY((_index == 0) ? _completeCardScaleY : _workCardScaleY, 0.38f).OnComplete(delegate
			{
				_isAnimation = false;
			});
		}

		private Transform GetCardTransform()
		{
			if (_index == 0)
			{
				return completeCard.transform;
			}
			workCard.Show((_index != 1) ? PrintPrefabType.WORK_CARD_BACK : PrintPrefabType.WORK_CARD_FRONT, isPrint: false);
			return workCard.transform;
		}

		private void Download()
		{
			if (!_isAnimation)
			{
				Debug.LogError("Download");
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.DOWNLOAD_PRINT_PREFAB, 0);
			}
		}

		public void Show()
		{
			firstButton.gameObject.SetActive(value: false);
			firstButton.interactable = false;
			base.gameObject.SetActive(value: true);
			canvasGroup.DOFade(1f, 0.38f).SetEase(Ease.Linear);
			content.transform.DOScale(1f, 0.38f).OnComplete(delegate
			{
				_isAnimation = false;
			});
			content.SetActive(value: true);
			firstCardRT.DOScaleY(0f, 0f);
			completeCard.transform.DOScaleY(_completeCardScaleY, 0f);
			StageClearType stageClearState = ArchiveData.GetStageClearState();
			_completeCardType = PrintPrefabType.STAGE_CLEAR_NORMAL;
			if (stageClearState == StageClearType.PERFECT)
			{
				_completeCardType = PrintPrefabType.STAGE_CLEAR_PERFECT;
			}
			completeCard.Show(_completeCardType, isPrint: false);
			NoticeShowPrintPrefab();
		}

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
			nextButton.onClick.AddListener(Next);
			lastButton.onClick.AddListener(Last);
			downloadButton.onClick.AddListener(Download);
		}

		private void Close()
		{
			if (!_isAnimation)
			{
				_isAnimation = true;
				canvasGroup.DOFade(0f, 0.38f).SetEase(Ease.Linear).OnComplete(delegate
				{
					base.gameObject.SetActive(value: false);
				});
				content.transform.DOScale(0f, 0.38f);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CLOSE_PRINT_CANVAS, 0);
			}
		}
	}
}
