using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class VoicePrintAnimation : MonoBehaviour
	{
		private Image[] _images;

		public bool isSpeak = true;

		private void Start()
		{
			_images = GetComponentsInChildren<Image>();
			InvokeRepeating("ChangeSound", 0f, 0.2f);
		}

		private void ChangeSound()
		{
			for (int i = 0; i < _images.Length; i++)
			{
				float endValue = (isSpeak ? Random.Range(0.2f, 0.8f) : Random.Range(0.1f, 0.13f));
				_images[i].transform.DOScaleY(endValue, 0.2f).SetEase(Ease.OutBounce);
			}
		}
	}
}
