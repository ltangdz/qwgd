using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class MoveLoop : MonoBehaviour
	{
		public float moveOff = 15f;

		public float interval = 1f;

		private void Start()
		{
			RectTransform component = GetComponent<Image>().GetComponent<RectTransform>();
			float y = component.anchoredPosition.y;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(component.DOAnchorPosY(y + moveOff, interval).SetEase(Ease.Linear));
			sequence.Append(component.DOAnchorPosY(y, interval).SetEase(Ease.Linear));
			sequence.SetLoops(-1);
			sequence.Play();
		}
	}
}
