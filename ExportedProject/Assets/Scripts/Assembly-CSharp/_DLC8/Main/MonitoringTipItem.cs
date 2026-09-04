using DG.Tweening;
using DLC7;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

namespace _DLC8.Main
{
	public class MonitoringTipItem : MonoBehaviour
	{
		public Text tipText;

		public Image iconImage;

		public DlcContentSizeFitter fitter;

		private Sequence _flickerSequence;

		public float InitData(PublicOpinionInfo publicOpinionInfo, bool isAnimation)
		{
			string value = I18N.instance.getValue(publicOpinionInfo.newsInfo);
			int num = ((I18N.instance.gameLang == LanguageCode.EN) ? 30 : 15);
			float num2 = (isAnimation ? ((float)value.Length * 1f / (float)num) : 0.1f);
			if (isAnimation)
			{
				_flickerSequence = DOTween.Sequence();
				_flickerSequence.Append(iconImage.DOFade(1f, 0.4f).SetEase(Ease.Linear));
				_flickerSequence.Append(iconImage.DOFade(0f, 0.4f).SetEase(Ease.Linear));
				_flickerSequence.SetLoops(-1);
				_flickerSequence.Play();
			}
			if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
			{
				tipText.DOText(value.Replace(" ", "\u00a0"), num2).SetEase(Ease.Linear).OnComplete(delegate
				{
					fitter.Reset();
					tipText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
				});
			}
			else
			{
				tipText.DOText(value, num2).SetEase(Ease.Linear).OnComplete(delegate
				{
					fitter.Reset();
					tipText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
				});
			}
			Invoke("CancelAnimation", num2);
			return num2;
		}

		private void CancelAnimation()
		{
			_flickerSequence.Kill();
			_flickerSequence = null;
			iconImage.DOFade(1f, 0.2f).SetEase(Ease.Linear);
		}
	}
}
