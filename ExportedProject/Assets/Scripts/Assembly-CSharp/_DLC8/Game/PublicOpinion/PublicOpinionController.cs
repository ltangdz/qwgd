using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionController : LaborerBaseContentDialog
	{
		public YulunPenziDialog trollDialog;

		public Button controlButton;

		public PublicOpinionNewsDialog newsDialog;

		public PublicOpinionDataDialog dataDialog;

		public PublicOpinionCardControl cardControl;

		public List<PublicOpinionMap> maps;

		public PublicOpinionResultNews resultNews;

		public PublicOpinionInfoManager _dataManager;

		public GameObject closeGroup;

		public Button closeBtn;

		public Text closeButtonText;

		public Button mapButton;

		public Button tipButton;

		public Text progressText;

		public Image progressImage;

		public PublicOpinionSubtitles publicOpinionSubtitles;

		public CanvasGroup tipCanvasGroup;

		private bool _isShowProgressTip;

		private List<PublicOpinionInfo> _titanDataList = new List<PublicOpinionInfo>();

		private List<PublicOpinionInfo> _normalDataList = new List<PublicOpinionInfo>();

		private List<PublicOpinionInfo> _titanDataUsedList = new List<PublicOpinionInfo>();

		private List<PublicOpinionInfo> _normalDataUsedList = new List<PublicOpinionInfo>();

		public Color[] progressColors;

		public DanielEmail danielEmail;

		public int allPerson;

		public bool gameSuccess;

		public bool gameOver;

		public float changeTime = 10f;

		public bool isBalancing;

		public CanvasGroup tipGroup;

		private PublicOpinionMap _curMap;

		private bool _isShowTip;

		private float _closeProgress = 0.3f;

		private void Init()
		{
			_dataManager = SingletonAutoMono<DLC8DataController>.GetInstance().PublicOpinionInfoDataManager;
			dataDialog.Init(this);
			mapButton.onClick.AddListener(delegate
			{
				if (!isBalancing)
				{
					ClickMap(null);
				}
			});
			tipButton.onClick.AddListener(ShowProgressTip);
			for (int num = 0; num < maps.Count; num++)
			{
				maps[num].InitController(this);
			}
			for (int num2 = 0; num2 < _dataManager.otherData.Values.Count; num2++)
			{
				PublicOpinionInfo item = _dataManager.otherData.Values.ElementAt(num2);
				_normalDataList.Add(item);
			}
			for (int num3 = 0; num3 < _dataManager.titanData.Values.Count; num3++)
			{
				PublicOpinionInfo item2 = _dataManager.titanData.Values.ElementAt(num3);
				_titanDataList.Add(item2);
			}
			if (!base.ArchiveData.danielEmailFinishedList[1])
			{
				base.ArchiveData.danielEmailFinishedList[1] = true;
				danielEmail.ShowAnimation();
				danielEmail.closeCallback = delegate
				{
					StartCoroutine("StartAnimation");
				};
			}
			else
			{
				StartCoroutine("StartAnimation");
			}
		}

		private void ShowProgressTip()
		{
			if (!_isShowProgressTip)
			{
				_isShowProgressTip = true;
				Sequence sequence = DOTween.Sequence();
				sequence.Append(tipCanvasGroup.DOFade(1f, 0.5f));
				sequence.AppendInterval(2f);
				sequence.Append(tipCanvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
				{
					_isShowProgressTip = false;
				}));
				sequence.Play();
			}
		}

		private void Start()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
			controlButton.interactable = false;
			base.GameManager.musicManager.PlayMusicLoop(12);
			closeBtn.onClick.AddListener(Close);
			controlButton.onClick.AddListener(delegate
			{
				cardControl.Show(RandomCard());
			});
			Invoke("Init", 0.5f);
		}

		private void Close()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_GAMME, 5);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CLOSE_CONTENT, 0);
			Object.Destroy(base.gameObject);
		}

		public IEnumerator StartAnimation()
		{
			trollDialog.GetComponent<RectTransform>().DOAnchorPosX(10f, 0.3f);
			yield return new WaitForSeconds(0.1f);
			newsDialog.GetComponent<RectTransform>().DOAnchorPosX(-10f, 0.3f);
			yield return new WaitForSeconds(0.1f);
			for (int i = 0; i < maps.Count; i++)
			{
				maps[i].gameObject.SetActive(value: true);
			}
			ClickMap(maps[0]);
			yield return new WaitForSeconds(0.1f);
			dataDialog.GetComponent<RectTransform>().DOAnchorPosY(5f, 0.3f);
			yield return new WaitForSeconds(0.53f);
			float num = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NegativeProgress();
			tipGroup.DOFade((!(num <= _closeProgress)) ? 1 : 0, 0.3f);
			closeGroup.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear);
			controlButton.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					controlButton.interactable = true;
				});
		}

		public void Progress(float progress)
		{
			progressText.text = Mathf.FloorToInt(progress * 100f) + "%";
			progressImage.fillAmount = progress;
			if (progress <= 0.3f)
			{
				progressImage.color = progressColors[0];
			}
			else if (progress < 0.75f)
			{
				progressImage.color = progressColors[1];
			}
			else
			{
				progressImage.color = progressColors[2];
			}
		}

		public void StartBalance(List<PublicOpinionInfo> infos)
		{
			closeBtn.interactable = false;
			closeButtonText.color = progressColors[3];
			isBalancing = true;
			resultNews.Init(infos);
			publicOpinionSubtitles.Init(infos);
			newsDialog.StartBalance(infos);
			for (int i = 0; i < maps.Count; i++)
			{
				maps[i].CountVal(infos);
			}
			for (int j = 0; j < infos.Count; j++)
			{
				PublicOpinionInfo publicOpinionInfo = infos[j];
				bool flag = publicOpinionInfo.IsCorrect();
				int trollType = publicOpinionInfo.trollType;
				if (flag && trollType != -1)
				{
					trollDialog.tipList[trollType].AddVal();
				}
			}
			Invoke("CancelBalance", 12f);
		}

		private void CancelBalance()
		{
			closeBtn.interactable = true;
			closeButtonText.color = Color.black;
			isBalancing = false;
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
		}

		public List<PublicOpinionInfo> RandomCard()
		{
			if (_titanDataList.Count == 0)
			{
				_titanDataList.AddRange(_titanDataUsedList);
				_titanDataUsedList.Clear();
			}
			if (_normalDataList.Count < 4)
			{
				_normalDataList.AddRange(_normalDataUsedList);
				_normalDataUsedList.Clear();
			}
			List<PublicOpinionInfo> list = new List<PublicOpinionInfo>();
			for (int i = 0; i < 4; i++)
			{
				PublicOpinionInfo item = _normalDataList[Random.Range(0, _normalDataList.Count)];
				list.Add(item);
				_normalDataUsedList.Add(item);
				_normalDataList.Remove(item);
			}
			PublicOpinionInfo publicOpinionInfo = _titanDataList[Random.Range(0, _titanDataList.Count)];
			publicOpinionInfo.roleNum = 0;
			publicOpinionInfo.positionType = PositionType.IDLE;
			list.Insert(Random.Range(0, 5), publicOpinionInfo);
			_titanDataUsedList.Add(publicOpinionInfo);
			_titanDataList.Remove(publicOpinionInfo);
			return list;
		}

		public void GameResult()
		{
		}

		public void ClickMap(PublicOpinionMap publicOpinionMap)
		{
			_curMap = publicOpinionMap;
			DLC8EventManager.Instance.NoticeSelectedMap(_curMap);
			dataDialog.ShowData(_curMap);
		}

		private void OnDestroy()
		{
			NoticeCloseContent();
		}
	}
}
