using System.Collections.Generic;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan.Voice
{
	public class DragVoiceItem : DragBagItem<VoicePrintModel>
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

		public override void InitUI(VoicePrintModel t)
		{
			ContentImage.sprite = EventManager.GetSprite(t.pathName);
		}

		public override void DragEnd(DragBagGrid<VoicePrintModel> bagGrid, List<Collider2D> collider2Ds)
		{
			bool flag = bagGrid.CompareTag("Waiting");
			VoicePrintItem voicePrintItem = (VoicePrintItem)bagGrid;
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
			VoicePrintModel dataItem = voicePrintItem.DataItem;
			if (flag2 || num == -1)
			{
				EventManager.NoticeUsed(dataItem.sourceName, dataItem.pathName, isUsed: false);
				if (!flag)
				{
					voicePrintItem.SaveData(null);
				}
				return;
			}
			EventManager.NoticeUsed(dataItem.sourceName, dataItem.pathName, isUsed: true);
			VoicePrintItem component = collider2Ds[num].GetComponent<VoicePrintItem>();
			if (component.DataItem == null)
			{
				component.SaveData(dataItem);
				if (!flag)
				{
					voicePrintItem.SaveData(null);
				}
				return;
			}
			VoicePrintModel dataItem2 = component.DataItem;
			if (flag)
			{
				component.SaveData(dataItem);
				EventManager.NoticeUsed(dataItem2.sourceName, dataItem2.pathName, isUsed: false);
			}
			else
			{
				voicePrintItem.SaveData(dataItem2);
				component.SaveData(dataItem);
			}
		}
	}
}
