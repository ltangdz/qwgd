using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLC7.SignalLight
{
	public class TitanLightQuestionIcon : TitanLightBase, IDragHandler, IEventSystemHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private float _startX;

		private RectTransform _RT;

		public TitanLightQuestion parent;

		public float _parentWidth;

		private bool _isCanDrag;

		private float _maxDistance;

		private int _step;

		private bool _firstStart;

		private void Start()
		{
			_RT = base.transform.GetComponent<RectTransform>();
			_isCanDrag = true;
			_parentWidth /= 2f;
			_maxDistance = _parentWidth / 5f * 4f;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isCanDrag)
			{
				float num = eventData.position.x - _startX;
				if (num >= _maxDistance)
				{
					num = _parentWidth;
					parent.Selected(isLeft: false);
					_isCanDrag = false;
				}
				else if (num <= -1f * _maxDistance)
				{
					num = _parentWidth * -1f;
					parent.Selected(isLeft: true);
					_isCanDrag = false;
				}
				_RT.DOAnchorPosX(num, 0f);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_isCanDrag)
			{
				_RT.DOAnchorPosX(0f, 0f);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_step == 0 && !_firstStart)
			{
				_firstStart = true;
				_isCanDrag = true;
				base.EventManager.NoticeStartGame();
			}
			_startX = eventData.position.x;
			_ = _isCanDrag;
		}

		protected override void NoticeStartGame()
		{
			_isCanDrag = true;
		}

		protected override void NoticeSelectedResult(int step, bool isSuccess)
		{
		}

		protected override void NoticeSuccess(int number)
		{
			_isCanDrag = false;
			Invoke("ResetData", 1f);
		}

		protected override void NoticeFail(int step)
		{
			ResetData();
		}

		protected override void NoticeResetGame()
		{
			ResetData();
		}

		protected override void NoticeIdle(int obj)
		{
			_firstStart = false;
			_step = obj;
			ResetData();
		}

		private void ResetData()
		{
			_isCanDrag = false;
			_RT.DOAnchorPosX(0f, 0f);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			base.transform.DOScale(1.15f, 0f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			base.transform.DOScale(1f, 0f);
		}
	}
}
