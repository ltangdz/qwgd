using System.Collections.Generic;
using UnityEngine;

namespace _DLC8.Game.WaterPipe
{
	public class WaterPipeCollider : MonoBehaviour
	{
		public WaterPipeItem parent;

		private Collider2D _collider2D;

		public List<WaterPipeItem> triggerStayItemList = new List<WaterPipeItem>();

		private void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_collider2D.enabled = true;
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			other.TryGetComponent<WaterPipeCollider>(out var component);
			if (component != null && !triggerStayItemList.Contains(component.parent))
			{
				triggerStayItemList.Add(component.parent);
				parent.Enter(this);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			other.TryGetComponent<WaterPipeCollider>(out var component);
			triggerStayItemList.Remove(component.parent);
			parent.Exit(this);
		}
	}
}
