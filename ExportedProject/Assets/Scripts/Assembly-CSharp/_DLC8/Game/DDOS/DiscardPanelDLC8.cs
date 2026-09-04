using DLC7.DDOS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _DLC8.Game.DDOS
{
	public class DiscardPanelDLC8 : DDosMonoBehaviourDLC8
	{
		private CanvasGroup _canvasGroup;

		public void TryDiscard(BagGridDLC8 bagGridDlc8)
		{
			Debug.Log("TryDiscard：" + bagGridDlc8.name);
			if (bagGridDlc8.DataItem.IsEffectCard())
			{
				bagGridDlc8.RemoveCard();
				return;
			}
			int num = bagGridDlc8.DataItem.Lv * 2;
			base.DdosEventManagerDlc8.NoticeSound(DdosSound.DISCARD);
			bagGridDlc8.RemoveCard();
			base.DdosEventManagerDlc8.NoticeChangeEnergy(num);
		}

		private void Start()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		private void Awake()
		{
			BagDragManager<CardDLC8> instance = BagDragManager<CardDLC8>.Instance;
			instance.onDragStart += DragStart;
			instance.onDragEnd += DragEnd;
		}

		private void OnDestroy()
		{
			BagDragManager<CardDLC8> instance = BagDragManager<CardDLC8>.Instance;
			instance.onDragStart -= DragStart;
			instance.onDragEnd -= DragEnd;
		}

		private void DragStart(string arg1, PointerEventData arg2, CardDLC8 arg3, string arg4)
		{
			_canvasGroup.alpha = 1f;
		}

		private void DragEnd(string arg1, PointerEventData arg2, CardDLC8 arg3, DragBagGrid<CardDLC8> arg4)
		{
			_canvasGroup.alpha = 0.1f;
		}
	}
}
