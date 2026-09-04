using System;
using UnityEngine.EventSystems;

namespace DLC7.DDOS
{
	public class BagDragManager<T>
	{
		private static BagDragManager<T> _instance;

		public static BagDragManager<T> Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new BagDragManager<T>();
				}
				return _instance;
			}
		}

		public event Action<string, PointerEventData, T, string> onDraging;

		public event Action<string, PointerEventData, T, DragBagGrid<T>> onDragEnd;

		public event Action<string, PointerEventData, T, string> onDragStart;

		public void Draging(string groupKey, PointerEventData p, T d, string sourceId)
		{
			if (this.onDraging != null)
			{
				this.onDraging(groupKey, p, d, sourceId);
			}
		}

		public void DragStart(string groupKey, PointerEventData p, T d, string sourceId)
		{
			if (this.onDragStart != null)
			{
				this.onDragStart(groupKey, p, d, sourceId);
			}
		}

		public void DragEnd(string groupKey, PointerEventData p, T d, DragBagGrid<T> from)
		{
			if (this.onDragEnd != null)
			{
				this.onDragEnd(groupKey, p, d, from);
			}
		}
	}
}
