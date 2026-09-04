using System;
using System.Collections;
using System.Text;
using Aluba;
using DG.Tweening;
using Honeti;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class MainMapDataGroup : MonoBehaviour
	{
		public Text totalText;

		public Text totalAddText;

		public Text personalAddText;

		public Text personalText;

		public Button tipButton;

		public CanvasGroup tipCanvasGroup;

		public Transform progressGroup;

		public Image yellowImage;

		public Image greenImage;

		public Text uploadText;

		public Text progressNumberText;

		private long _personalCount;

		private long _totalCount;

		private ArchiveData _archiveData;

		private bool _isShowTip;

		private float _numberProgress;

		private Sequence _uploadTextSequence;

		private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);

		private void Start()
		{
			progressGroup.DOScaleX(0f, 0f);
			greenImage.DOFade(0f, 0f);
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			_personalCount = _archiveData.PersonData;
			_totalCount = _archiveData.TotalData;
			tipButton.onClick.AddListener(ShowTip);
			AddDataCountAnimation();
		}

		private void ShowTip()
		{
			if (!_isShowTip)
			{
				_isShowTip = true;
				SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
				Sequence sequence = DOTween.Sequence();
				sequence.Append(tipCanvasGroup.DOFade(1f, 0.5f));
				sequence.AppendInterval(2f);
				sequence.Append(tipCanvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
				{
					_isShowTip = false;
				}));
				sequence.Play();
			}
		}

		private IEnumerator UploadTextAnimation()
		{
			StringBuilder builder = new StringBuilder(I18N.instance.getValue("^110009_common_74"));
			uploadText.text = builder.ToString();
			yield return _waitForSeconds;
			for (int i = 0; i < 4; i++)
			{
				builder.Append(".");
				uploadText.text = builder.ToString();
				yield return _waitForSeconds;
			}
			uploadText.text = I18N.instance.getValue("^110009_common_75");
			yield return new WaitForSeconds(1f);
			uploadText.text = "";
			progressNumberText.text = "";
			progressGroup.DOScale(0f, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
			{
				AddDataCountAnimation();
			});
		}

		private void StartBalance()
		{
			yellowImage.fillAmount = 0f;
			yellowImage.DOFade(1f, 0f);
			greenImage.DOFade(0f, 0f);
			_numberProgress = 0f;
			progressNumberText.text = "0%";
			personalText.DOFade(0f, 0f);
			progressGroup.DOScale(1f, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
			{
				DOTween.To(() => _numberProgress, delegate(float x)
				{
					_numberProgress = x;
				}, 1f, 1f).OnUpdate(delegate
				{
					yellowImage.fillAmount = _numberProgress;
					progressNumberText.text = $"{Mathf.FloorToInt(_numberProgress * 100f)}%";
				}).OnComplete(delegate
				{
					yellowImage.DOFade(0f, 0f);
					greenImage.DOFade(1f, 0f);
				});
				StartCoroutine("UploadTextAnimation");
			});
		}

		private void AddDataCountAnimation()
		{
			SteamStats steamStats = new SteamStats();
			steamStats.Init("stat_data_count", delegate(long score)
			{
				Debug.Log("stat_data_count:" + score);
				long num2 = score - _archiveData.TotalData;
				if (num2 <= 0)
				{
					num2 = 0L;
				}
				else
				{
					totalAddText.DOFade(0f, 0f);
					totalAddText.text = $"+{num2.ToString()}";
					totalAddText.DOFade(1f, 0.38f).SetEase(Ease.Linear);
					_archiveData.TotalData = score;
					float duration2 = Mathf.Min(num2, 2f);
					DOTween.To(() => _totalCount, delegate(long x)
					{
						_totalCount = x;
					}, _archiveData.TotalData, duration2).SetEase(Ease.Linear).OnUpdate(delegate
					{
						totalText.text = _totalCount.ToString();
					})
						.OnComplete(delegate
						{
							totalAddText.DOFade(0f, 0.38f).SetEase(Ease.Linear);
						});
				}
			});
			personalText.DOFade(1f, 0f);
			totalText.text = _archiveData.TotalData.ToString();
			personalText.text = string.Format("{0}{1}", _archiveData.PersonData, I18N.instance.getValue("^110009_common_21"));
			long num = _archiveData.PersonData - _personalCount;
			if (num <= 0)
			{
				num = 0L;
				return;
			}
			try
			{
				steamStats.UpdateState("stat_data_count", (int)_archiveData.PersonData);
			}
			catch (Exception)
			{
				Debug.LogError("stat_data_count");
			}
			personalText.DOFade(0f, 0f);
			personalAddText.text = $"+{num.ToString()}";
			personalText.DOFade(1f, 0.38f).SetEase(Ease.Linear);
			if (num > 0)
			{
				personalAddText.DOFade(1f, 0.1f).SetEase(Ease.Linear);
			}
			float duration = Mathf.Min(num, 2f);
			DOTween.To(() => _personalCount, delegate(long x)
			{
				_personalCount = x;
			}, _archiveData.PersonData, duration).SetEase(Ease.Linear).OnUpdate(delegate
			{
				personalText.text = string.Format("{0}{1}", _personalCount, I18N.instance.getValue("^110009_common_21"));
			})
				.OnComplete(delegate
				{
					personalAddText.DOFade(0f, 0.38f).SetEase(Ease.Linear);
				});
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeControllerGameOver += NoticeControllerGameOver;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeControllerGameOver -= NoticeControllerGameOver;
		}

		private void NoticeControllerGameOver(LevelRecord obj)
		{
			StartBalance();
		}
	}
}
