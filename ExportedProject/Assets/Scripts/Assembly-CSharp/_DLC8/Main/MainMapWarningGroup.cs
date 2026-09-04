using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Main
{
	public class MainMapWarningGroup : MonoBehaviour
	{
		public Image image;

		private Sequence _sequence;

		public void Show()
		{
			base.gameObject.SetActive(value: true);
			if (_sequence != null)
			{
				_sequence.Kill();
				_sequence = null;
			}
			_sequence = DOTween.Sequence();
			_sequence.Append(image.transform.DOScale(0.75f, 0.38f).SetEase(Ease.Linear));
			_sequence.Append(image.transform.DOScale(0.8f, 0.38f).SetEase(Ease.Linear));
			_sequence.SetLoops(-1).Play();
		}

		public void Hide()
		{
			_sequence.Kill();
			_sequence = null;
			base.gameObject.SetActive(value: false);
		}
	}
}
