using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class DragCard : DragBagItem<Card>
	{
		private Card _cardData;

		public Image frameImage;

		public Image contentImage;

		private DDOSManager _ddosManager;

		public DDOSManager DdosManager
		{
			get
			{
				if (_ddosManager == null)
				{
					_ddosManager = DDOSManager.Instance;
				}
				return _ddosManager;
			}
		}

		public Card CardData => _cardData;

		private void Awake()
		{
			_groupKey = "Card";
		}

		public override void InitUI(Card t)
		{
			_cardData = t;
			DdosManager.InitImage(_cardData.FramePath, frameImage);
			DdosManager.InitImage(_cardData.ContentPath, contentImage);
		}

		public override void DragEnd(DragBagGrid<Card> bagGrid, List<Collider2D> collider2Ds)
		{
			int num = -1;
			float num2 = -1f;
			TouchTag touchTag = TouchTag.NONE;
			BagGrid bagGrid2 = (BagGrid)bagGrid;
			float num3 = Mathf.Abs(Vector2.Distance(bagGrid2.transform.position, base.transform.position));
			bool flag = false;
			for (int i = 0; i < collider2Ds.Count; i++)
			{
				GameObject gameObject = collider2Ds[i].gameObject;
				if (gameObject.CompareTag("bag"))
				{
					if (gameObject == bagGrid2.gameObject)
					{
						flag = true;
						continue;
					}
					touchTag = TouchTag.BAG;
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
					touchTag = TouchTag.TRASH;
				}
			}
			switch (touchTag)
			{
			case TouchTag.BAG:
				if (flag && (num == -1 || num2 > num3))
				{
					bagGrid2.Cancel();
				}
				else
				{
					collider2Ds[num].gameObject.GetComponent<BagGrid>().TrySave(bagGrid2);
				}
				break;
			case TouchTag.TRASH:
				collider2Ds[0].gameObject.GetComponent<DiscardPanel>().TryDiscard(bagGrid2);
				break;
			}
		}
	}
}
