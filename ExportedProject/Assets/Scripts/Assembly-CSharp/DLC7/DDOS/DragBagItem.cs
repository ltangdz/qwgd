using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLC7.DDOS
{
	public abstract class DragBagItem<T> : MonoBehaviour
	{
		private RectTransform m_rt;

		private T _itemData;

		private Vector3 _screenPos;

		public string _groupKey;

		private List<Collider2D> _touchList;

		public T ItemData
		{
			get
			{
				return _itemData;
			}
			set
			{
				_itemData = value;
			}
		}

		public List<Collider2D> TouchList
		{
			get
			{
				if (_touchList == null)
				{
					_touchList = new List<Collider2D>();
				}
				return _touchList;
			}
		}

		public abstract void InitUI(T t);

		public abstract void DragEnd(DragBagGrid<T> bagGrid, List<Collider2D> touchList);

		public void SetContent(T data1)
		{
			SetFront();
			InitUI(data1);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!TouchList.Contains(other))
			{
				TouchList.Add(other);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (TouchList.Contains(other))
			{
				TouchList.Remove(other);
			}
		}

		private void OnDragStart(string groupKey, PointerEventData eventData, T data, string sourceId)
		{
			if (!(groupKey != _groupKey))
			{
				SetContent(data);
				base.transform.SetAsLastSibling();
			}
		}

		private void OnDraging(string groupKey, PointerEventData eventData, T data, string sourceId)
		{
			if (!(groupKey != _groupKey))
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition.z = _screenPos.z;
				Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
				base.transform.position = position;
			}
		}

		private void OnDragEnd(string arg1, PointerEventData arg2, T arg3, DragBagGrid<T> arg4)
		{
			if (!(arg1 != _groupKey))
			{
				DragEnd(arg4, TouchList);
				HideDialog();
			}
		}

		private void OnEnable()
		{
			_screenPos = Camera.main.WorldToScreenPoint(base.transform.position);
			m_rt = base.gameObject.GetComponent<RectTransform>();
			BagDragManager<T>.Instance.onDragStart += OnDragStart;
			BagDragManager<T>.Instance.onDraging += OnDraging;
			BagDragManager<T>.Instance.onDragEnd += OnDragEnd;
		}

		private void OnDisable()
		{
			BagDragManager<T>.Instance.onDragStart -= OnDragStart;
			BagDragManager<T>.Instance.onDraging -= OnDraging;
			BagDragManager<T>.Instance.onDragEnd -= OnDragEnd;
		}

		private void SetFront()
		{
			base.transform.SetAsLastSibling();
		}

		public void HideDialog()
		{
			base.transform.position = new Vector3(1196f, -140f, 0f);
		}
	}
}
