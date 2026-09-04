using UnityEngine;
using UnityEngine.EventSystems;

namespace Aluba.Bag
{
	public abstract class BagDragItem<T> : MonoBehaviour
	{
		private RectTransform m_rt;

		private T _itemData;

		private Vector3 _screenPos;

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

		public abstract void InitUI(T t);

		public void SetContent(T data1)
		{
			SetFront();
			InitUI(data1);
		}

		private void OnDragEnd(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
			HideDialog();
		}

		private void OnDrag(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = _screenPos.z;
			Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
			base.transform.position = position;
		}

		private void OnDragStart(PointerEventData arg1, T arg2, BagGrid<T> arg3)
		{
			BagEventManager<T>.Instance.ItemBeginDrag(this);
			SetContent(arg2);
			base.transform.SetAsLastSibling();
		}

		private void Start()
		{
			_screenPos = Camera.main.WorldToScreenPoint(base.transform.position);
			m_rt = base.gameObject.GetComponent<RectTransform>();
			BagEventManager<T>.Instance.onDragStart += OnDragStart;
			BagEventManager<T>.Instance.onDrag += OnDrag;
			BagEventManager<T>.Instance.onDragEnd += OnDragEnd;
		}

		private void OnDisable()
		{
			BagEventManager<T>.Instance.onDragStart -= OnDragStart;
			BagEventManager<T>.Instance.onDrag -= OnDrag;
			BagEventManager<T>.Instance.onDragEnd -= OnDragEnd;
		}

		private void SetFront()
		{
			base.transform.SetAsLastSibling();
		}

		public void HideDialog()
		{
			base.transform.position = new Vector3(10000f, 10000f, 0f);
		}
	}
}
