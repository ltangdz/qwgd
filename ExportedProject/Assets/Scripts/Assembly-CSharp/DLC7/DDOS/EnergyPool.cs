using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class EnergyPool : DDosMonoBehaviour
	{
		public Image bgImage;

		public Image progressImage;

		private float _maxEnergy = 100f;

		private float _curEnergy;

		private bool isHasGrid;

		private List<CardType> _cardTypes;

		public List<CardType> CardTypes
		{
			get
			{
				if (_cardTypes.IsNullOrEmpty())
				{
					_cardTypes = new List<CardType>();
					List<int> cardIds = base.DdosManager.Level.cardIds;
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
			base.DdosEventManager.onNoticeChangeEnergy += NoticeEnergy;
		}

		private void Start()
		{
			_curEnergy = 0f;
			PlayAnimation();
		}

		private void OnDestroy()
		{
			base.DdosEventManager.onNoticeChangeEnergy -= NoticeEnergy;
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
				if (base.DdosManager.CanBuyItem() == null)
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
			if (base.DdosManager.Level.lv == 3 && base.DdosManager.Lv > 8 && Random.Range(1, 100) <= 40)
			{
				num = 7;
			}
			if (num < CardTypes.Count)
			{
				SendCardByType(CardTypes[num]);
			}
		}

		public void SendCardByType(CardType type)
		{
			BagGrid bagGrid = base.DdosManager.CanBuyItem();
			if (!(bagGrid == null))
			{
				if (_curEnergy >= 100f)
				{
					bagGrid.LockBag();
					Card card = new Card();
					card.InitSkillCard(type);
					BuyCardEffect component = base.DdosManager.SpawnPool.Spawn("BuyCardEffect").GetComponent<BuyCardEffect>();
					component.transform.position = base.transform.position;
					component.Move(card, bagGrid);
					base.DdosEventManager.NoticeSound(DdosSound.GET_CARD);
				}
				_curEnergy = (base.DdosManager.isTest ? 99 : 0);
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
