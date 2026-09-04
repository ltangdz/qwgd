using System.Collections.Generic;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.Voice
{
	public class DragVoiceItemDlc8 : DragBagItem<VoicePrintModelDLC8>
	{
		private Image _contentImage;

		private VoicePrintEvent _eventManager;

		public Image ContentImage
		{
			get
			{
				if (_contentImage == null)
				{
					_contentImage = GetComponent<Image>();
				}
				return _contentImage;
			}
		}

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

		public override void InitUI(VoicePrintModelDLC8 t)
		{
			ContentImage.sprite = EventManager.GetSprite(t.pathName);
		}

		public override void DragEnd(DragBagGrid<VoicePrintModelDLC8> bagGrid, List<Collider2D> collider2Ds)
		{
			bool flag = bagGrid.CompareTag("Waiting");
			VoicePrintItemDLC8 voicePrintItemDLC = (VoicePrintItemDLC8)bagGrid;
			int num = -1;
			float num2 = -1f;
			bool flag2 = false;
			for (int i = 0; i < collider2Ds.Count; i++)
			{
				GameObject gameObject = collider2Ds[i].gameObject;
				if ((flag && gameObject.CompareTag("Waiting")) || (!flag && gameObject.CompareTag("Waiting")))
				{
					flag2 = true;
					break;
				}
				float num3 = Mathf.Abs(Vector2.Distance(base.transform.position, gameObject.transform.position));
				if (num == -1)
				{
					num = i;
					num2 = num3;
				}
				else if (num3 < num2)
				{
					num = i;
					num2 = num3;
				}
			}
			VoicePrintModelDLC8 dataItem = voicePrintItemDLC.DataItem;
			if (flag2 || num == -1)
			{
				EventManager.NoticeUsed(dataItem.sourceName, dataItem.pathName, isUsed: false);
				if (!flag)
				{
					voicePrintItemDLC.SaveData(null);
				}
				return;
			}
			VoicePrintItemDLC8 component = collider2Ds[num].GetComponent<VoicePrintItemDLC8>();
			if (component.DataItem == null)
			{
				component.SaveData(dataItem);
				if (!flag)
				{
					voicePrintItemDLC.SaveData(null);
				}
			}
			else
			{
				VoicePrintModelDLC8 dataItem2 = component.DataItem;
				if (flag)
				{
					component.SaveData(dataItem);
					EventManager.NoticeUsed(dataItem2.sourceName, dataItem2.pathName, isUsed: false);
				}
				else
				{
					voicePrintItemDLC.SaveData(dataItem2);
					component.SaveData(dataItem);
				}
			}
			EventManager.NoticeUsed(dataItem.sourceName, dataItem.pathName, isUsed: true);
		}
	}
}
