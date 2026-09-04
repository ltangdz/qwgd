using DG.Tweening;
using DLC7.DDOS;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.Titan.Voice
{
	public class VoicePrintItem : DragBagGrid<VoicePrintModel>, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public Image voicePrintImage;

		public bool isWaitingArea;

		private VoicePrintEvent _eventManager;

		public VoicePrintEvent EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = VoicePrintEvent.Instance;
				}
				return _eventManager;
			}
		}

		protected override void InitUI()
		{
			SaveData(base.DataItem);
		}

		private void Awake()
		{
			base.GroupKey = "VoicePrint";
		}

		protected override void StartDrag()
		{
			voicePrintImage.DOFade(0.3f, 0f);
		}

		protected override void EndDrag()
		{
		}

		public void WaitingReset()
		{
			if (base.DataItem != null)
			{
				base.DataItem.isUsed = false;
				voicePrintImage.DOFade(1f, 0f);
			}
		}

		public void SaveData(VoicePrintModel sourceItemDataItem)
		{
			base.DataItem = sourceItemDataItem;
			if (sourceItemDataItem == null)
			{
				voicePrintImage.DOFade(0f, 0f);
				return;
			}
			voicePrintImage.sprite = EventManager.GetSprite(sourceItemDataItem.pathName);
			if (isWaitingArea)
			{
				voicePrintImage.DOFade(base.DataItem.isUsed ? 0f : 1f, 0f);
			}
			else
			{
				voicePrintImage.DOFade(1f, 0f);
			}
		}

		protected override bool CanDrag()
		{
			if (isWaitingArea && (base.DataItem == null || base.DataItem.isUsed))
			{
				return false;
			}
			return true;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (isWaitingArea)
			{
				base.transform.DOScale(1.05f, 0f);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (isWaitingArea)
			{
				base.transform.DOScale(1f, 0f);
			}
		}
	}
}
