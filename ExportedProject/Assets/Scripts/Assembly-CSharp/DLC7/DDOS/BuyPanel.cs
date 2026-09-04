using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class BuyPanel : DDosMonoBehaviour
	{
		[Header("组件")]
		public Button buyButton;

		public Text needCoinText;

		public Image iconImage;

		[Header("基础属性")]
		[SerializeField]
		private int _lv;

		private int _needCoin;

		public BuyPanelType buyPanelType;

		private void Start()
		{
			_needCoin = 1;
			_lv = 1;
			buyButton.onClick.AddListener(Buy);
			Invoke("InitNeedCoin", 1.5f);
		}

		private void InitNeedCoin()
		{
			if (buyPanelType == BuyPanelType.QUEEN || buyPanelType == BuyPanelType.ATTACKER)
			{
				_needCoin = _lv;
				string cardContentPath = Card.GetCardContentPath((buyPanelType == BuyPanelType.QUEEN) ? CardType.QUEEN : CardType.ATTAKER, (base.DdosManager.Lv - 3 < 1) ? 1 : (base.DdosManager.Lv - 3));
				base.DdosManager.InitImage(cardContentPath, iconImage);
			}
			else if (buyPanelType == BuyPanelType.HEAL)
			{
				List<Dictionary<string, string>> wallDatas = base.DdosManager.wallDatas;
				if (wallDatas.Count == _lv)
				{
					buyButton.interactable = false;
				}
				Dictionary<string, string> dictionary = wallDatas[_lv - 1];
				_needCoin = Convert.ToInt32(dictionary["HpCost"]);
				int maxHp = Convert.ToInt32(dictionary["Hp"]);
				base.DdosEventManager.NoticeWallHeal(maxHp);
			}
			needCoinText.text = _needCoin.ToString();
		}

		private void Buy()
		{
			int coin = base.DdosManager.Coin;
			if (_needCoin < 0 || coin == 0 || coin < _needCoin)
			{
				base.DdosEventManager.NoticeBuyFail(BuyResultType.NO_COIN);
				base.DdosEventManager.NoticeSound(DdosSound.NO_MONEY);
			}
			else if (buyPanelType == BuyPanelType.QUEEN || buyPanelType == BuyPanelType.ATTACKER)
			{
				BagGrid bagGrid = base.DdosManager.CanBuyItem();
				if (bagGrid == null)
				{
					base.DdosEventManager.NoticeBuyFail(BuyResultType.NO_GRID);
					return;
				}
				base.DdosManager.Coin -= _needCoin;
				bagGrid.LockBag();
				Card card = new Card();
				int num = base.DdosManager.Lv - 3;
				card.InitData(buyPanelType, (num < 1) ? 1 : num, base.DdosManager);
				BuyCardEffect component = base.DdosManager.SpawnPool.Spawn("BuyCardEffect").GetComponent<BuyCardEffect>();
				component.transform.position = base.transform.position;
				component.Move(card, bagGrid);
				_lv++;
				InitNeedCoin();
			}
			else if (buyPanelType == BuyPanelType.HEAL)
			{
				base.DdosManager.Coin -= _needCoin;
				_lv++;
				InitNeedCoin();
			}
		}

		private void Awake()
		{
			base.DdosEventManager.onNoticeLevel += NoticeLevel;
		}

		private void OnDestroy()
		{
			base.DdosEventManager.onNoticeLevel -= NoticeLevel;
		}

		private void NoticeLevel(int obj)
		{
			InitNeedCoin();
		}
	}
}
