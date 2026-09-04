using System;
using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main;

namespace _DLC8.Game.DDOS
{
	public class MiniGameOverWindowDlc8 : DialogAnimation
	{
		public RectTransform topGroup;

		public Text titleText;

		public Button closeButton;

		public Image iconImage;

		public Text dataText;

		public Text dataTitleText;

		public Text timeText;

		public Text timeTitleText;

		public Text tipText;

		public Image tipFrame;

		private int _curScore;

		private bool _isNewRecord;

		private string _bugCount;

		private string _dataCount;

		public override void CloseOver()
		{
		}

		public void Show(string bugCount, string dataCount)
		{
			base.gameObject.SetActive(value: true);
			_bugCount = bugCount;
			_dataCount = dataCount;
			try
			{
				ShowAnimation();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		private IEnumerator StartTextAnimation()
		{
			yield return new WaitForSeconds(0.15f);
			iconImage.transform.DOScale(1f, 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			topGroup.transform.DOScale(1f, 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			tipFrame.transform.DOScale(1f, 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			titleText.DOText(I18N.instance.getValue("^110009_common_52"), 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			timeTitleText.DOText(I18N.instance.getValue("^110009_common_72"), 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			dataTitleText.DOText(I18N.instance.getValue("^110009_common_43"), 0.15f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			timeText.DOText(_bugCount, 0.2f).SetEase(Ease.Linear);
			dataText.DOText(_dataCount, 0.2f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.1f);
			tipText.DOText(I18N.instance.getValue("^110009_common_76"), 0.2f).SetEase(Ease.Linear);
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
			titleText.text = "";
			dataText.text = "";
			timeText.text = "";
			timeTitleText.text = "";
			dataTitleText.text = "";
			tipText.text = "";
			topGroup.DOScale(new Vector3(0f, 1f, 1f), 0f);
			iconImage.transform.DOScale(0f, 0f);
			closeButton.interactable = false;
			closeButton.GetComponent<CanvasGroup>().alpha = 0f;
		}
	}
}
