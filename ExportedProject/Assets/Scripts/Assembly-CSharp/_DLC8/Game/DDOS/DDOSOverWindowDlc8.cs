using System.Collections;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main;

namespace _DLC8.Game.DDOS
{
	public class DDOSOverWindowDlc8 : DialogAnimation
	{
		public RectTransform bestScoreGroup;

		public Text bestScoreText;

		public Button closeButton;

		public Image iconImage;

		public Text dataText;

		public Text resourceText;

		public Image tipFrame;

		public CanvasGroup newRecordCanvasGroup;

		public Text recordText;

		private int _curScore;

		private bool _isNewRecord;

		private int _bugCount;

		private int _dataCount;

		public override void CloseOver()
		{
		}

		public void Show(int curScore, bool isNewRecord, int bugCount, int dataCount)
		{
			base.gameObject.SetActive(value: true);
			_curScore = curScore;
			_isNewRecord = isNewRecord;
			_bugCount = bugCount;
			_dataCount = dataCount;
			ShowAnimation();
		}

		private IEnumerator StartTextAnimation()
		{
			iconImage.transform.DOScale(1f, 0.3f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.2f);
			bestScoreGroup.transform.DOScale(1f, 0.3f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.2f);
			tipFrame.transform.DOScale(1f, 0.3f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.2f);
			bestScoreText.DOText(string.Format("{0}:", I18N.instance.getValue("^110009_common_40")), 0.1f).SetEase(Ease.Linear).OnComplete(delegate
			{
				int tempScore = 0;
				if (_curScore > 0)
				{
					SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(64);
				}
				DOTween.To(() => tempScore, delegate(int x)
				{
					tempScore = x;
				}, _curScore, 0.5f).SetEase(Ease.Linear).OnUpdate(delegate
				{
					bestScoreText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_40"), _curScore);
				})
					.OnComplete(delegate
					{
						if (_isNewRecord)
						{
							SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(63);
							newRecordCanvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear);
						}
					});
			});
			yield return new WaitForSeconds(0.8f);
			resourceText.DOText(string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_42"), _bugCount), 0.2f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			dataText.DOText(string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_43"), _dataCount), 0.2f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.3f);
			closeButton.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					closeButton.interactable = true;
				});
		}

		public override void WillClose()
		{
		}

		public override void ShowOver()
		{
			StartCoroutine("StartTextAnimation");
		}

		public override void WillShow()
		{
			bestScoreText.text = "";
			dataText.text = "";
			resourceText.text = "";
			bestScoreGroup.DOScale(new Vector3(0f, 1f, 1f), 0f);
			iconImage.transform.DOScale(0f, 0f);
			closeButton.interactable = false;
			closeButton.GetComponent<CanvasGroup>().alpha = 0f;
		}
	}
}
