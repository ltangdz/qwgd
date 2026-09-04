using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLC7.DDOS
{
	public abstract class DragBagGrid<T> : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		[SerializeField]
		private bool _isDraged;

		private bool _isEnter;

		private bool _isDragSelf;

		private string _guid;

		private T _dataItem;

		protected string _groupKey;

		private DragInType _dragInType;

		private bool _isDrag;

		public T DataItem
		{
			get
			{
				return _dataItem;
			}
			set
			{
				_dataItem = value;
			}
		}

		public string GroupKey
		{
			get
			{
				return _groupKey;
			}
			set
			{
				_groupKey = value;
			}
		}

		protected abstract void InitUI();

		protected abstract void StartDrag();

		protected abstract void EndDrag();

		protected abstract bool CanDrag();

		public void Init(T key, string groupKey)
		{
			_groupKey = groupKey;
			_dataItem = key;
			InitUI();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_dataItem != null && CanDrag())
			{
				BagDragManager<T>.Instance.Draging(_groupKey, eventData, _dataItem, _guid);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (_dataItem != null && CanDrag())
			{
				_isDragSelf = true;
				StartDrag();
				BagDragManager<T>.Instance.DragStart(_groupKey, eventData, _dataItem, _guid);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_dataItem != null && CanDrag())
			{
				EndDrag();
				BagDragManager<T>.Instance.DragEnd(_groupKey, eventData, _dataItem, this);
			}
		}

		private void Awake()
		{
			_guid = Guid.NewGuid().ToString();
		}
	}
}
