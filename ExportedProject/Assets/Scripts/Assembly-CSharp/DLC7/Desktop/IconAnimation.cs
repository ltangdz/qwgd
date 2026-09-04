using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Desktop
{
	public class IconAnimation : MonoBehaviour
	{
		public Image image;

		public Image textImage;

		private void Start()
		{
			RectTransform component = image.GetComponent<RectTransform>();
			float y = component.anchoredPosition.y;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(component.DOAnchorPosY(y - 8f, 3f).SetEase(Ease.Linear));
			sequence.Append(component.DOAnchorPosY(y, 3f).SetEase(Ease.Linear));
			sequence.SetLoops(-1);
			sequence.Play();
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(image.DOFade(0.6f, Random.Range(3f, 3.5f)).SetEase(Ease.Linear));
			sequence2.Append(image.DOFade(1f, Random.Range(3f, 3.5f)).SetEase(Ease.Linear));
			sequence2.SetLoops(-1);
			sequence2.Play();
			Sequence sequence3 = DOTween.Sequence();
			sequence3.Append(textImage.DOFade(0.3f, Random.Range(2.5f, 3f)).SetEase(Ease.Linear));
			sequence3.Append(textImage.DOFade(0.6f, Random.Range(2.5f, 3f)).SetEase(Ease.Linear));
			sequence3.SetLoops(-1);
			sequence3.Play();
		}
	}
}
