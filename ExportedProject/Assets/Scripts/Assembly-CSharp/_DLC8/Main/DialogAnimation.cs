using DG.Tweening;
using UnityEngine;

namespace _DLC8.Main
{
	public abstract class DialogAnimation : MonoBehaviour
	{
		public RectTransform contentRT;

		public abstract void CloseOver();

		public abstract void WillClose();

		public abstract void ShowOver();

		public abstract void WillShow();

		public void CloseAnimation()
		{
			WillClose();
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleX(0f, 0.3f).OnComplete(delegate
				{
					contentRT.DOScaleY(0f, 0f);
					base.gameObject.transform.DOScale(0f, 0f);
					CloseOver();
				});
			});
		}

		public void ShowAnimation()
		{
			WillShow();
			base.gameObject.transform.DOScale(1f, 0f);
			contentRT.DOScale(0f, 0f);
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0f);
			contentRT.DOScaleX(2f / contentRT.sizeDelta.x, 0f);
			contentRT.DOScaleX(1f, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleY(1f, 0.3f).OnComplete(delegate
				{
					ShowOver();
				});
			});
		}
	}
}
