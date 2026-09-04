using System;
using UnityEngine.EventSystems;

namespace Aluba.Bag
{
	public class BagEventManager<T>
	{
		private static BagEventManager<T> _instance;

		public static BagEventManager<T> Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new BagEventManager<T>();
				}
				return _instance;
			}
		}

		public event Action<PointerEventData, T, BagGrid<T>> onDrag;

		public event Action<PointerEventData, T, BagGrid<T>> onDragEnd;

		public event Action<PointerEventData, T, BagGrid<T>> onDragStart;

		public event Action<BagDragItem<T>> onItemBeginDrag;

		public void ItemBeginDrag(BagDragItem<T> p)
		{
			if (this.onItemBeginDrag != null)
			{
				this.onItemBeginDrag(p);
			}
		}

		public void Drag(PointerEventData p, T d, BagGrid<T> sourceGrid)
		{
			if (this.onDrag != null)
			{
				this.onDrag(p, d, sourceGrid);
			}
		}

		public void DragStart(PointerEventData p, T d, BagGrid<T> sourceGrid)
		{
			if (this.onDragStart != null)
			{
				this.onDragStart(p, d, sourceGrid);
			}
		}

		public void DragEnd(PointerEventData p, T d, BagGrid<T> sourceGrid)
		{
			if (this.onDragEnd != null)
			{
				this.onDragEnd(p, d, sourceGrid);
			}
		}
	}
}
