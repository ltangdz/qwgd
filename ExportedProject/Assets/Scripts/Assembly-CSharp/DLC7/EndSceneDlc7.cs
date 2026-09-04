using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DG.Tweening;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class EndSceneDlc7 : MonoBehaviour
	{
		public List<CanvasGroup> canvasGroups;

		public Image blackImage;

		public List<FrameAnimation2D> catAnimation;

		public CanvasGroup catCanvasGroup;

		public List<RectTransform> group1Transforms;

		public MaskTween maskTween;

		public List<FrameAnimation2D> eyes;

		private List<Dictionary<string, string>> _songDataList;

		public Text captionText;

		private void Start()
		{
			captionText.DOFade(0f, 0f);
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			component.floatBox.GetComponent<CanvasGroup>().DOFade(0f, 0f);
			component.floatBox.SetActive(value: false);
			maskTween.ResetTweenParam();
			catCanvasGroup.DOFade(0f, 0f);
			maskTween.ChangeAlpha(0f, 0f);
			blackImage.DOFade(0f, 2f).OnComplete(delegate
			{
				StartCoroutine("StartAnimation");
			});
			string value = "[{\"Key\":\"^110008_common_104\",\"Time\":\"3.028\",\"Interval\":\"\"},{\"Key\":\"^110008_common_105\",\"Time\":\"4.896\",\"Interval\":\"\"},{\"Key\":\"^110008_common_106\",\"Time\":\"3.642\",\"Interval\":\"\"},{\"Key\":\"^110008_common_107\",\"Time\":\"4.164\",\"Interval\":\"\"},{\"Key\":\"^110008_common_108\",\"Time\":\"5.674\",\"Interval\":\"\"},{\"Key\":\"^110008_common_109\",\"Time\":\"3.431\",\"Interval\":\"\"},{\"Key\":\"^110008_common_110\",\"Time\":\"5.257\",\"Interval\":\"\"},{\"Key\":\"\",\"Time\":\"\",\"Interval\":\"9.376\"},{\"Key\":\"^110008_common_111\",\"Time\":\"3.163\",\"Interval\":\"\"},{\"Key\":\"^110008_common_106\",\"Time\":\"4.063\",\"Interval\":\"\"},{\"Key\":\"^110008_common_107\",\"Time\":\"3.168\",\"Interval\":\"\"},{\"Key\":\"^110008_common_108\",\"Time\":\"6.655\",\"Interval\":\"\"},{\"Key\":\"^110008_common_109\",\"Time\":\"3.348\",\"Interval\":\"\"},{\"Key\":\"^110008_common_110\",\"Time\":\"4.499\",\"Interval\":\"\"},{\"Key\":\"\",\"Time\":\"\",\"Interval\":\"5\"},{\"Key\":\"^110008_other_297\",\"Time\":\"4\",\"Interval\":\"\"}]";
			_songDataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(value);
			StartCoroutine("ShowCaption");
		}

		private IEnumerator ShowCaption()
		{
			float showTime = 0.2f;
			WaitForSeconds waitForSeconds = new WaitForSeconds(showTime);
			if (I18N.instance.gameLang == LanguageCode.EN)
			{
				yield return new WaitForSeconds(5f);
			}
			for (int i = 0; i < _songDataList.Count; i++)
			{
				Dictionary<string, string> dictionary = _songDataList[i];
				string value = dictionary["Time"];
				string text = dictionary["Key"];
				string text2 = (string.IsNullOrEmpty(text) ? "" : I18N.instance.getValue(text));
				float num = (string.IsNullOrEmpty(value) ? 0f : Convert.ToSingle(value, CultureInfo.InvariantCulture));
				string value2 = dictionary["Interval"];
				float num2 = (string.IsNullOrEmpty(value2) ? 0f : Convert.ToSingle(value2, CultureInfo.InvariantCulture));
				float totalTime = num + num2;
				captionText.text = text2;
				captionText.DOFade(1f, showTime).SetEase(Ease.Linear);
				yield return waitForSeconds;
				yield return new WaitForSeconds(totalTime - showTime * 2f);
				captionText.DOFade(0f, showTime).SetEase(Ease.Linear);
				yield return waitForSeconds;
				captionText.text = "";
			}
		}

		private IEnumerator StartAnimation()
		{
			for (int i = 0; i < canvasGroups.Count; i++)
			{
				CanvasGroup canvasGroup = canvasGroups[i];
				if (i == 0)
				{
					group1Transforms[0].DOAnchorPosX(54f, 0.3f);
					yield return new WaitForSeconds(0.3f);
					group1Transforms[1].DOAnchorPosX(751f, 0.3f);
					yield return new WaitForSeconds(0.7f);
				}
				else
				{
					canvasGroup.DOFade(1f, 1f).SetEase(Ease.Linear);
					yield return new WaitForSeconds(1f);
				}
				yield return new WaitForSeconds(7f);
				if (i == 0)
				{
					StartCoroutine("StartCatAnimation");
				}
				canvasGroup.DOFade(0f, 1f).SetEase(Ease.Linear);
				yield return new WaitForSeconds(1f);
			}
			blackImage.DOFade(1f, 2f);
			yield return new WaitForSeconds(2f);
			StringBuilder stringBuilder = new StringBuilder();
			if (I18N.instance.gameLang == LanguageCode.CN)
			{
				stringBuilder.Append("Dialog/DLC7endVideoCN");
			}
			else if (I18N.instance.gameLang == LanguageCode.TC)
			{
				stringBuilder.Append("Dialog/DLC7endVideoTW");
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				stringBuilder.Append("Dialog/DLC7endVideoEN");
			}
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>(stringBuilder.ToString()), base.transform);
		}

		private IEnumerator StartCatAnimation()
		{
			yield return new WaitForSeconds(2f);
			FrameAnimation2D catAnimation0 = catAnimation[0];
			catAnimation0.gameObject.SetActive(value: true);
			catCanvasGroup.DOFade(1f, 2f);
			maskTween.ChangeAlpha(1f, 2f);
			yield return new WaitForSeconds(7f);
			FrameAnimation2D catAnimation1 = catAnimation[1];
			catAnimation0.gameObject.SetActive(value: false);
			catAnimation1.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(7f);
			FrameAnimation2D catAnimation2 = catAnimation[2];
			catAnimation1.gameObject.SetActive(value: false);
			catAnimation2.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(6.5f);
			catCanvasGroup.DOFade(0f, 2f);
			maskTween.ChangeAlpha(0f, 2f);
			FrameAnimation2D catAnimation3 = catAnimation[3];
			yield return new WaitForSeconds(4f);
			catCanvasGroup.DOFade(1f, 2f);
			maskTween.ChangeAlpha(1f, 2f);
			catAnimation2.gameObject.SetActive(value: false);
			catAnimation3.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(4f);
			catCanvasGroup.DOFade(0f, 2f);
			maskTween.ChangeAlpha(0f, 2f);
			yield return new WaitForSeconds(9.5f);
			catCanvasGroup.DOFade(1f, 2f);
			maskTween.ChangeAlpha(1f, 2f);
			FrameAnimation2D catAnimation4 = catAnimation[4];
			catAnimation3.gameObject.SetActive(value: false);
			catAnimation4.gameObject.SetActive(value: true);
			maskTween.PlayMaskScaleTween(15f, delegate
			{
			});
			yield return new WaitForSeconds(1f);
			FrameAnimation2D catAnimation5 = catAnimation[5];
			catAnimation4.gameObject.SetActive(value: false);
			catAnimation5.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(3f);
			yield return new WaitForSeconds(14f);
			FrameAnimation2D catAnimation6 = catAnimation[6];
			catAnimation5.gameObject.SetActive(value: false);
			catAnimation6.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.4f);
			FrameAnimation2D frameAnimation2D = catAnimation[7];
			catAnimation6.gameObject.SetActive(value: false);
			frameAnimation2D.gameObject.SetActive(value: true);
		}

		private void Awake()
		{
			FrameAnimationEvent.Instance.frameFinished += FrameFinished;
		}

		private void OnDestroy()
		{
			FrameAnimationEvent.Instance.frameFinished -= FrameFinished;
		}

		private void FrameFinished(string arg1, int arg2, int maxCount)
		{
			Debug.Log(arg1);
			if (arg1 == "eye" && arg2 == maxCount - 1)
			{
				for (int i = 0; i < eyes.Count; i++)
				{
					Transform target = eyes[i].transform;
					Sequence sequence = DOTween.Sequence();
					sequence.Append(target.DORotate(new Vector3(0f, 0f, -720f), 5f).SetEase(Ease.Linear));
					sequence.SetLoops(-1);
					sequence.Play();
					Sequence sequence2 = DOTween.Sequence();
					sequence2.Append(target.DOScale(1.1f, 0.5f).SetEase(Ease.Linear));
					sequence2.Append(target.DOScale(1f, 0.5f).SetEase(Ease.Linear));
					sequence2.SetLoops(-1);
					sequence2.Play();
				}
			}
		}
	}
}
