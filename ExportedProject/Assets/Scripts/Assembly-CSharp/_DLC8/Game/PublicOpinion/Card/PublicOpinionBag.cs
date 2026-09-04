using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;

namespace _DLC8.Game.PublicOpinion.Card
{
	public class PublicOpinionBag : DragBagGrid<PublicOpinionInfo>
	{
		public List<PublicOpinionInfo> infos = new List<PublicOpinionInfo>();

		public PositionType bagType;

		public PublicOpinionTrollBox trollBox;

		public List<PublicOpinionCard> cards = new List<PublicOpinionCard>();

		public List<PublicOpinionCardIdle> cardIdles = new List<PublicOpinionCardIdle>();

		private PublicOpinionCardControl _cardControl;

		public int lastCount;

		public Transform content;

		private Vector2 pos1;

		private Vector2 offPos;

		private void OnDisable()
		{
			infos.Clear();
			for (int i = 0; i < cards.Count; i++)
			{
				Object.Destroy(cards[i].gameObject);
			}
			for (int j = 0; j < cardIdles.Count; j++)
			{
				Object.Destroy(cardIdles[j].gameObject);
			}
		}

		protected override void InitUI()
		{
		}

		protected override void StartDrag()
		{
			if (bagType == PositionType.IDLE)
			{
				if (cardIdles.Count > 0)
				{
					cardIdles[cardIdles.Count - 1].GetComponent<CanvasGroup>().DOFade(0f, 0f);
				}
			}
			else if (cards.Count > 0)
			{
				cards[cards.Count - 1].GetComponent<CanvasGroup>().DOFade(0f, 0f);
			}
		}

		protected override void EndDrag()
		{
		}

		protected override bool CanDrag()
		{
			if (infos.Count == 0)
			{
				return false;
			}
			base.DataItem = infos[infos.Count - 1];
			return true;
		}

		public void Cancel()
		{
			if (bagType == PositionType.IDLE)
			{
				if (cardIdles.Count > 0)
				{
					cardIdles[cardIdles.Count - 1].GetComponent<CanvasGroup>().DOFade(1f, 0f);
				}
			}
			else if (cards.Count > 0)
			{
				cards[cards.Count - 1].GetComponent<CanvasGroup>().DOFade(1f, 0f);
			}
		}

		public void DragOK()
		{
			Debug.Log("DragOK");
			infos.Remove(base.DataItem);
			base.DataItem = null;
			ResetUI();
		}

		public void PutIntoBag(PublicOpinionInfo info)
		{
			info.positionType = bagType;
			infos.Add(info);
			ResetUI();
		}

		public void InitData(PublicOpinionCardControl control)
		{
			switch (bagType)
			{
			case PositionType.IDLE:
				pos1 = Vector2.zero;
				offPos = new Vector2(0f, -20f);
				break;
			case PositionType.UP:
				pos1 = new Vector2(235f, -145f);
				offPos = new Vector2(20f, -80f);
				break;
			case PositionType.DOWN:
				pos1 = new Vector2(-235f, -145f);
				offPos = new Vector2(-20f, -80f);
				break;
			}
			_cardControl = control;
			if (bagType == PositionType.IDLE)
			{
				infos = control.cardInfos;
			}
			lastCount = infos.Count;
			ResetUI();
		}

		public void ResetUI()
		{
			int count = infos.Count;
			int num = lastCount - count;
			if (infos.Count > 0)
			{
				base.DataItem = infos[infos.Count - 1];
			}
			Init(base.DataItem, "PublicOpinion");
			List<PublicOpinionInfo> range = infos;
			if (bagType == PositionType.IDLE)
			{
				int num2 = Mathf.Min(infos.Count, 3);
				range = infos.GetRange(infos.Count - num2, num2);
				if (range.Count > 0)
				{
					PublicOpinionInfo publicOpinionInfo = range[range.Count - 1];
					if (publicOpinionInfo.trollType == -1)
					{
						trollBox.Clear();
					}
					else
					{
						trollBox.Init(publicOpinionInfo.trollType, publicOpinionInfo.trollTrigger);
					}
				}
				else
				{
					trollBox.Clear();
				}
			}
			for (int i = 0; i < content.childCount; i++)
			{
				Object.Destroy(content.GetChild(i).gameObject);
			}
			cardIdles.Clear();
			cards.Clear();
			for (int num3 = 0; num3 < range.Count; num3++)
			{
				PublicOpinionInfo news = range[num3];
				switch (bagType)
				{
				}
				if (bagType == PositionType.IDLE)
				{
					PublicOpinionCardIdle publicOpinionCardIdle = Object.Instantiate(_cardControl.cardIdlePrefab, content);
					publicOpinionCardIdle.Init(news);
					publicOpinionCardIdle.GetComponent<RectTransform>().anchoredPosition = pos1 + num3 * offPos;
					if (range.Count == 3)
					{
						publicOpinionCardIdle.transform.DOScale((num > 0) ? 0.7f : (0.9f + (float)num3 * 0.1f), 0f).SetEase(Ease.Linear);
						publicOpinionCardIdle.transform.DOScale(0.8f + (float)num3 * 0.1f, (num < 0 && count >= 3) ? 0f : 0.2f).SetEase(Ease.Linear);
					}
					else if (range.Count == 2)
					{
						publicOpinionCardIdle.transform.DOScale((num > 0) ? 0.8f : (1f + (float)num3 * 0.1f), 0f).SetEase(Ease.Linear);
						publicOpinionCardIdle.transform.DOScale(0.9f + (float)num3 * 0.1f, (num < 0 && count >= 3) ? 0f : 0.2f).SetEase(Ease.Linear);
					}
					else if (range.Count == 1)
					{
						publicOpinionCardIdle.transform.DOScale(1f, 0f).SetEase(Ease.Linear);
					}
					cardIdles.Add(publicOpinionCardIdle);
					publicOpinionCardIdle.GetComponent<CanvasGroup>().DOFade((num3 == range.Count - 1) ? 1f : 0.7f, 0f);
				}
				else
				{
					PublicOpinionCard publicOpinionCard = Object.Instantiate(_cardControl.cardPrefab, content);
					publicOpinionCard.Init(news, bagType);
					publicOpinionCard.transform.GetComponent<RectTransform>().localPosition = pos1 + num3 * offPos;
					cards.Add(publicOpinionCard);
				}
			}
			lastCount = infos.Count;
		}
	}
}
