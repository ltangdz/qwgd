using System;
using System.Collections.Generic;
using Aluba;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class BuyPanelDLC8 : DDosMonoBehaviourDLC8
	{
		[Header("组件")]
		public Button buyButton;

		public Text needCoinText;

		public Image iconImage;

		[Header("基础属性")]
		[SerializeField]
		private ObscuredInt _lv;

		private bool _gameStart;

		private ObscuredInt _needCoin;

		[FormerlySerializedAs("buyPanelType")]
		public BuyPanelTypeDLC8 buyPanelTypeDlc8;

		private void Start()
		{
			_needCoin = 1;
			_lv = 1;
			buyButton.onClick.AddListener(Buy);
			Invoke("InitNeedCoin", 1.5f);
		}

		private void InitNeedCoin()
		{
			if (buyPanelTypeDlc8 == BuyPanelTypeDLC8.QUEEN || buyPanelTypeDlc8 == BuyPanelTypeDLC8.ATTACKER)
			{
				_needCoin = _lv;
				int num = ((base.DdosManagerDlc8.Lv - 3 < 1) ? 1 : (base.DdosManagerDlc8.Lv - 3));
				Debug.Log("buy：" + num);
				int cardMaxLevel = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData().cardMaxLevel;
				string cardContentPath = CardDLC8.GetCardContentPath((buyPanelTypeDlc8 == BuyPanelTypeDLC8.QUEEN) ? CardType.QUEEN : CardType.ATTAKER, Mathf.Min(num, cardMaxLevel));
				base.DdosManagerDlc8.InitImage(cardContentPath, iconImage);
			}
			else if (buyPanelTypeDlc8 == BuyPanelTypeDLC8.HEAL)
			{
				List<Dictionary<string, string>> wallDatas = base.DdosManagerDlc8.wallDatas;
				if (wallDatas.Count == (int)_lv)
				{
					buyButton.interactable = false;
				}
				Dictionary<string, string> dictionary = wallDatas[(int)_lv - 1];
				_needCoin = Convert.ToInt32(dictionary["HpCost"]);
				int maxHp = Convert.ToInt32(dictionary["Hp"]);
				base.DdosEventManagerDlc8.NoticeWallHeal(maxHp);
			}
			needCoinText.text = _needCoin.ToString();
		}

		private void Buy()
		{
			if (!_gameStart)
			{
				return;
			}
			int coin = base.DdosManagerDlc8.Coin;
			if ((int)_needCoin < 0 || coin == 0 || coin < (int)_needCoin)
			{
				base.DdosEventManagerDlc8.NoticeBuyFail(BuyResultType.NO_COIN);
				base.DdosEventManagerDlc8.NoticeSound(DdosSound.NO_MONEY);
			}
			else if (buyPanelTypeDlc8 == BuyPanelTypeDLC8.QUEEN || buyPanelTypeDlc8 == BuyPanelTypeDLC8.ATTACKER)
			{
				BagGridDLC8 bagGridDLC = base.DdosManagerDlc8.CanBuyItem();
				if (bagGridDLC == null)
				{
					base.DdosEventManagerDlc8.NoticeBuyFail(BuyResultType.NO_GRID);
					return;
				}
				base.DdosManagerDlc8.Coin -= _needCoin;
				bagGridDLC.LockBag();
				CardDLC8 cardDLC = new CardDLC8();
				int num = base.DdosManagerDlc8.Lv - 3;
				cardDLC.InitData(buyPanelTypeDlc8, (num < 1) ? 1 : num, base.DdosManagerDlc8);
				BuyCardEffectDLC8 component = base.DdosManagerDlc8.SpawnPool.Spawn("BuyCardEffectDLC8").GetComponent<BuyCardEffectDLC8>();
				component.transform.position = base.transform.position;
				component.Move(cardDLC, bagGridDLC);
				++_lv;
				InitNeedCoin();
			}
			else if (buyPanelTypeDlc8 == BuyPanelTypeDLC8.HEAL)
			{
				base.DdosManagerDlc8.Coin -= _needCoin;
				++_lv;
				InitNeedCoin();
			}
		}

		private void Awake()
		{
			base.DdosEventManagerDlc8.onNoticeLevel += NoticeLevel;
			base.DdosEventManagerDlc8.onNoticeGameWaves += NoticeGameWaves;
		}

		private void NoticeGameWaves(GameWavesType arg1, int arg2)
		{
			_gameStart = true;
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticeLevel -= NoticeLevel;
			base.DdosEventManagerDlc8.onNoticeGameWaves -= NoticeGameWaves;
		}

		private void NoticeLevel(int obj)
		{
			InitNeedCoin();
		}
	}
}
