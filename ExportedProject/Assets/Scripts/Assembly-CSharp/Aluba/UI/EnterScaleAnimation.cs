using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aluba.UI
{
	public class EnterScaleAnimation : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		private RectTransform _rt;

		public Vector3 scaleVector3 = new Vector3(1.2f, 1.2f, 1.2f);

		private Vector2 _pivot;

		private Vector2 _anchorMax;

		private Vector2 _anchorMin;

		private Vector2 _halfVector2 = new Vector2(0.5f, 0.5f);

		private Vector3 _localScale;

		private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;

		public void Start()
		{
			_rt = GetComponent<RectTransform>();
			_pivot = _rt.pivot;
			_localScale = _rt.localScale;
			_anchorMax = _rt.anchorMax;
			_anchorMin = _rt.anchorMin;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_rt.pivot = _halfVector2;
			_rt.anchorMax = _halfVector2;
			_rt.anchorMin = _halfVector2;
			_rt.DOScale(scaleVector3, 0.1f).SetEase(Ease.Linear);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_rt.pivot = _pivot;
			_rt.anchorMax = _anchorMax;
			_rt.anchorMin = _anchorMin;
			_rt.DOScale(_localScale, 0.1f).SetEase(Ease.Linear);
		}
	}
}
