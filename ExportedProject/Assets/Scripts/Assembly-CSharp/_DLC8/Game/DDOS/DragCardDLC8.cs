using System.Collections.Generic;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class DragCardDLC8 : DragBagItem<CardDLC8>
	{
		private CardDLC8 _cardDlc8Data;

		public Image frameImage;

		public Image contentImage;

		private DDOSManagerDLC8 _ddosManagerDlc8;

		public DDOSManagerDLC8 DdosManagerDlc8
		{
			get
			{
				if (_ddosManagerDlc8 == null)
				{
					_ddosManagerDlc8 = DDOSManagerDLC8.Instance;
				}
				return _ddosManagerDlc8;
			}
		}

		public CardDLC8 CardDlc8Data => _cardDlc8Data;

		private void Awake()
		{
			_groupKey = "Card";
		}

		public override void InitUI(CardDLC8 t)
		{
			_cardDlc8Data = t;
			DdosManagerDlc8.InitImage(_cardDlc8Data.FramePath, frameImage);
			DdosManagerDlc8.InitImage(_cardDlc8Data.ContentPath, contentImage);
		}

		public override void DragEnd(DragBagGrid<CardDLC8> bagGrid, List<Collider2D> collider2Ds)
		{
			int num = -1;
			float num2 = -1f;
			TouchTagDLC8 touchTagDLC = TouchTagDLC8.NONE;
			BagGridDLC8 bagGridDLC = (BagGridDLC8)bagGrid;
			float num3 = Mathf.Abs(Vector2.Distance(bagGridDLC.transform.position, base.transform.position));
			bool flag = false;
			for (int i = 0; i < collider2Ds.Count; i++)
			{
				GameObject gameObject = collider2Ds[i].gameObject;
				if (gameObject.CompareTag("bag"))
				{
					if (gameObject == bagGridDLC.gameObject)
					{
						flag = true;
						continue;
					}
					touchTagDLC = TouchTagDLC8.BAG;
					float num4 = Mathf.Abs(Vector2.Distance(base.transform.position, gameObject.transform.position));
					if (num == -1)
					{
						num = i;
						num2 = num4;
					}
					else if (num4 < num2)
					{
						num = i;
						num2 = num4;
					}
				}
				else if (gameObject.CompareTag("Trash"))
				{
					touchTagDLC = TouchTagDLC8.TRASH;
				}
			}
			switch (touchTagDLC)
			{
			case TouchTagDLC8.BAG:
				if (flag && (num == -1 || num2 > num3))
				{
					bagGridDLC.Cancel();
				}
				else
				{
					collider2Ds[num].gameObject.GetComponent<BagGridDLC8>().TrySave(bagGridDLC);
				}
				break;
			case TouchTagDLC8.TRASH:
				collider2Ds[0].gameObject.GetComponent<DiscardPanelDLC8>().TryDiscard(bagGridDLC);
				break;
			}
		}
	}
}
