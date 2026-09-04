using UnityEngine;
using UnityEngine.EventSystems;

namespace DLC7.DDOS
{
	public class DiscardPanel : DDosMonoBehaviour
	{
		private CanvasGroup _canvasGroup;

		public void TryDiscard(BagGrid bagGrid)
		{
			Debug.Log("TryDiscard：" + bagGrid.name);
			if (bagGrid.DataItem.IsEffectCard())
			{
				bagGrid.RemoveCard();
				return;
			}
			int num = bagGrid.DataItem.Lv * 2;
			base.DdosEventManager.NoticeSound(DdosSound.DISCARD);
			bagGrid.RemoveCard();
			base.DdosEventManager.NoticeChangeEnergy(num);
		}

		private void Start()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		private void Awake()
		{
			BagDragManager<Card> instance = BagDragManager<Card>.Instance;
			instance.onDragStart += DragStart;
			instance.onDragEnd += DragEnd;
		}

		private void OnDestroy()
		{
			BagDragManager<Card> instance = BagDragManager<Card>.Instance;
			instance.onDragStart -= DragStart;
			instance.onDragEnd -= DragEnd;
		}

		private void DragStart(string arg1, PointerEventData arg2, Card arg3, string arg4)
		{
			_canvasGroup.alpha = 1f;
		}

		private void DragEnd(string arg1, PointerEventData arg2, Card arg3, DragBagGrid<Card> arg4)
		{
			_canvasGroup.alpha = 0.1f;
		}
	}
}
