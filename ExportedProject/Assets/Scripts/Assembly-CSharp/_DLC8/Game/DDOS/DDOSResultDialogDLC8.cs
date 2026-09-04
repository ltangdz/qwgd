using DG.Tweening;
using DLC7;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class DDOSResultDialogDLC8 : DDosMonoBehaviourDLC8
	{
		[Header("成功界面")]
		public GameObject winObj;

		public GameObject progressObj;

		public Text progressText;

		public AlubaLoading1 loading;

		public GameObject successObj;

		public Button successButton;

		public GameObject failObj;

		public Button failButton;

		public RectTransform topRT;

		public RectTransform bottomRT;

		public Text bottomText;

		private int _progress;

		private bool _isLoading;

		public void Show(bool isSuccess)
		{
			bottomText.DOFade(0f, 0f);
			topRT.DOAnchorPosY(0f, 0.2f);
			base.transform.SetAsLastSibling();
			if (isSuccess)
			{
				base.DdosEventManagerDlc8.NoticeSound(DdosSound.WIN);
			}
			else
			{
				base.DdosEventManagerDlc8.NoticeSound(DdosSound.FAIL);
			}
			bottomRT.DOAnchorPosY(0f, 0.2f).OnComplete(delegate
			{
				if (isSuccess)
				{
					failObj.SetActive(value: false);
					ShowSuccess();
				}
				else
				{
					winObj.SetActive(value: false);
					bottomText.DOFade(1f, 0.38f);
					ShowFail();
				}
			});
		}

		private void ShowSuccess()
		{
			winObj.SetActive(value: true);
			progressObj.SetActive(value: true);
			winObj.GetComponent<CanvasGroup>().DOFade(1f, 0.38f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					loading.BeginLoad();
					_isLoading = true;
					DOTween.To(() => _progress, delegate(int x)
					{
						_progress = x;
					}, 100, 8.4f).SetEase(Ease.Linear).OnComplete(delegate
					{
						progressObj.SetActive(value: false);
						successObj.SetActive(value: true);
						successButton.onClick.AddListener(GameFinalSuccess);
					});
				});
		}

		private void GameFinalSuccess()
		{
			DLCEventManager.Instance.NoticeGameSuccess();
		}

		private void FixedUpdate()
		{
			if (_isLoading)
			{
				if (_progress == 100)
				{
					_isLoading = true;
				}
				progressText.text = $"{_progress}%";
			}
		}

		private void ShowFail()
		{
			failObj.SetActive(value: true);
			failButton = failObj.GetComponent<Button>();
			failButton.interactable = false;
			CanvasGroup canvasGroup = failObj.GetComponent<CanvasGroup>();
			canvasGroup.DOFade(1f, 0.38f).SetEase(Ease.Linear).OnComplete(delegate
			{
				Sequence sequence = DOTween.Sequence();
				sequence.Append(canvasGroup.DOFade(0.8f, 0.38f)).SetEase(Ease.Linear);
				sequence.Append(canvasGroup.DOFade(1f, 0.38f)).SetEase(Ease.Linear);
				sequence.SetLoops(-1);
				sequence.Play();
				failButton.interactable = true;
				failButton.onClick.AddListener(ShowFailDialog);
			});
		}

		private void ShowFailDialog()
		{
			Object.Instantiate(Resources.Load<HackerFailDialog>(DLCNameUtil.Instance.GetFailDialogName()), base.transform.root);
		}
	}
}
