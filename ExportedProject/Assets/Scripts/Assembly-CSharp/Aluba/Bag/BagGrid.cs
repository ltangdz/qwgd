using UnityEngine;
using UnityEngine.EventSystems;

namespace Aluba.Bag
{
	public abstract class BagGrid<T> : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		public BagGridType bagGridType;

		public DragShow dragShow = DragShow.SHOW;

		public bool isFixed;

		public BagGrid<T> firstBagGrid;

		private bool _isDrag;

		private bool _isEnter;

		private BagDragItem<T> _bagDragItem;

		private T _data;

		public T Data
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		protected abstract void IsEnterUI();

		private void ItemBeginDrag(BagDragItem<T> obj)
		{
			_bagDragItem = obj;
		}

		public void OnDrag(PointerEventData eventData)
		{
			BagEventManager<T>.Instance.Drag(eventData, _data, this);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_data != null)
			{
				_isDrag = true;
				BagEventManager<T>.Instance.DragStart(eventData, _data, this);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			BagEventManager<T>.Instance.DragEnd(eventData, _data, this);
		}

		private void DragEnd(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
			_isDrag = false;
			_bagDragItem = null;
		}

		private void Drag(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
		}

		private void DragStart(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (_isDrag)
			{
				_isEnter = false;
				IsEnterUI();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (_isDrag)
			{
				_isEnter = true;
				IsEnterUI();
			}
		}

		private void Awake()
		{
			BagEventManager<T>.Instance.onItemBeginDrag += ItemBeginDrag;
			BagEventManager<T>.Instance.onDragStart += DragStart;
			BagEventManager<T>.Instance.onDrag += Drag;
			BagEventManager<T>.Instance.onDragEnd += DragEnd;
		}

		private void OnDestroy()
		{
			BagEventManager<T>.Instance.onItemBeginDrag -= ItemBeginDrag;
			BagEventManager<T>.Instance.onDragStart -= DragStart;
			BagEventManager<T>.Instance.onDrag -= Drag;
			BagEventManager<T>.Instance.onDragEnd -= DragEnd;
		}
	}
}
