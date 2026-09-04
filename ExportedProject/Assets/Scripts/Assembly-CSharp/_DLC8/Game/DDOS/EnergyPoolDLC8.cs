using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class EnergyPoolDLC8 : DDosMonoBehaviourDLC8
	{
		public Image bgImage;

		public Image progressImage;

		private float _maxEnergy = 100f;

		private float _curEnergy;

		private bool isHasGrid;

		private List<CardType> _cardTypes;

		private int _fireCount;

		public List<CardType> CardTypes
		{
			get
			{
				if (_cardTypes.IsNullOrEmpty())
				{
					_cardTypes = new List<CardType>();
					List<int> cardIds = base.DdosManagerDlc8.LevelDlc8.cardIds;
					for (int i = 0; i < cardIds.Count; i++)
					{
						_cardTypes.Add((CardType)(cardIds[i] + 1));
					}
				}
				return _cardTypes;
			}
		}

		private void Awake()
		{
			base.DdosEventManagerDlc8.onNoticeChangeEnergy += NoticeEnergy;
		}

		private void Start()
		{
			_curEnergy = 0f;
			PlayAnimation();
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticeChangeEnergy -= NoticeEnergy;
		}

		private void NoticeEnergy(float energy)
		{
			_curEnergy += energy;
			if (_curEnergy > 100f)
			{
				_curEnergy = 100f;
			}
			if (_curEnergy == 100f)
			{
				if (base.DdosManagerDlc8.CanBuyItem() == null)
				{
					InvokeRepeating("SendCard", 0f, 1f);
				}
				else
				{
					SendCard();
				}
			}
			PlayAnimation();
		}

		private void SendCard()
		{
			int count = CardTypes.Count;
			if (count == 0)
			{
				Debug.Log("卡片关卡数据错误");
				return;
			}
			int num = Random.Range(0, count);
			if (base.DdosManagerDlc8.LevelDlc8.lv == 3 && base.DdosManagerDlc8.Lv > 8 && Random.Range(1, 100) <= 40)
			{
				num = 7;
			}
			if (num >= CardTypes.Count)
			{
				return;
			}
			CardType cardType = CardTypes[num];
			if (cardType == CardType.CARD_BUG)
			{
				if (_fireCount < 5)
				{
					_fireCount++;
				}
				else
				{
					cardType = ((Random.Range(0, 2) == 1) ? CardType.CARD_OVERCLOCK_QUEEN : CardType.CARD_FLOOD);
				}
			}
			SendCardByType(cardType);
		}

		public void SendCardByType(CardType type)
		{
			BagGridDLC8 bagGridDLC = base.DdosManagerDlc8.CanBuyItem();
			if (!(bagGridDLC == null))
			{
				if (_curEnergy >= 100f)
				{
					bagGridDLC.LockBag();
					CardDLC8 cardDLC = new CardDLC8();
					cardDLC.InitSkillCard(type);
					BuyCardEffectDLC8 component = base.DdosManagerDlc8.SpawnPool.Spawn("BuyCardEffectDLC8").GetComponent<BuyCardEffectDLC8>();
					component.transform.position = base.transform.position;
					component.Move(cardDLC, bagGridDLC);
					base.DdosEventManagerDlc8.NoticeSound(DdosSound.GET_CARD);
				}
				_curEnergy = (base.DdosManagerDlc8.isTest ? 99 : 0);
				PlayAnimation();
				CancelInvoke("SendCard");
			}
		}

		public void PlayAnimation()
		{
			progressImage.DOFillAmount(_curEnergy / _maxEnergy, 0.1f).SetEase(Ease.Linear);
		}
	}
}
