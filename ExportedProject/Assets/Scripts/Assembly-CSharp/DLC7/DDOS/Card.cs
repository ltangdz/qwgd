using System;
using System.Collections.Generic;
using UnityEngine;

namespace DLC7.DDOS
{
	[Serializable]
	public class Card
	{
		private int _lv;

		private int _attack;

		private int _queenBuff;

		private IntensifyType _intensify;

		private float _interval;

		private int _extraAttack;

		private float _discardEnergy;

		private float _curInterval = 1f;

		private string _framePath;

		private string _contentPath;

		private CardType _type;

		public bool isFlood;

		public int Lv
		{
			get
			{
				return _lv;
			}
			set
			{
				_lv = value;
			}
		}

		public int Attack
		{
			get
			{
				return _attack;
			}
			set
			{
				_attack = value;
			}
		}

		public float CurInterval
		{
			get
			{
				return _curInterval;
			}
			set
			{
				_curInterval = value;
			}
		}

		public IntensifyType Intensify
		{
			get
			{
				return _intensify;
			}
			set
			{
				_intensify = value;
				InitImagePath();
			}
		}

		public float Interval
		{
			get
			{
				return _interval;
			}
			set
			{
				_interval = value;
			}
		}

		public int ExtraAttack
		{
			get
			{
				return _extraAttack;
			}
			set
			{
				_extraAttack = value;
			}
		}

		public float DiscardEnergy
		{
			get
			{
				return _discardEnergy;
			}
			set
			{
				_discardEnergy = value;
			}
		}

		public CardType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		public int QueenBuff => _queenBuff;

		public string FramePath => _framePath;

		public string ContentPath => _contentPath;

		public void InitSkillCard(CardType cardType)
		{
			_type = cardType;
			_lv = 1;
			InitImagePath();
		}

		public bool IsEffectCard()
		{
			if (Type == CardType.QUEEN || Type == CardType.ATTAKER)
			{
				return false;
			}
			return true;
		}

		public bool IsQueenEffectCard()
		{
			if (Type == CardType.CARD_TRANSFER_QUEEN || Type == CardType.CARD_OVERCLOCK_QUEEN)
			{
				return true;
			}
			return false;
		}

		public bool IsAttackEffectCard()
		{
			if (!IsEffectCard())
			{
				return false;
			}
			return !IsQueenEffectCard();
		}

		public void InitData(BuyPanelType buyPanelType, int lv, DDOSManager ddosManager)
		{
			isFlood = false;
			InitType(buyPanelType);
			_lv = lv;
			InitImagePath();
			InitAttribute(ddosManager);
		}

		public void Upgrade(DDOSManager ddosManager)
		{
			List<Dictionary<string, string>> list = ddosManager.attackerDatas;
			if (_type == CardType.QUEEN)
			{
				list = ddosManager.queeenDatas;
			}
			int count = list.Count;
			if (_lv < count)
			{
				_lv++;
				InitImagePath();
				InitAttribute(ddosManager);
			}
		}

		private void InitAttribute(DDOSManager ddosManager)
		{
			if (Type == CardType.QUEEN)
			{
				InitQueen(ddosManager);
			}
			else if (Type == CardType.ATTAKER)
			{
				InitAttacker(ddosManager);
			}
		}

		private void InitQueen(DDOSManager ddosManager)
		{
			Dictionary<string, string> dictionary = ddosManager.queeenDatas[Lv - 1];
			_attack = Convert.ToInt32(dictionary["Produce"]);
			_queenBuff = Convert.ToInt32(dictionary["Buff"]);
		}

		private void InitAttacker(DDOSManager ddosManager)
		{
			Dictionary<string, string> dictionary = ddosManager.attackerDatas[Lv - 1];
			Attack = Convert.ToInt32(dictionary["Att"]);
		}

		private void InitImagePath()
		{
			_contentPath = GetCardContentPath(_type, _lv);
			switch (_type)
			{
			case CardType.QUEEN:
				_framePath = "zhuji_00";
				break;
			case CardType.ATTAKER:
				_framePath = "rq_27";
				if (_intensify == IntensifyType.FLASH)
				{
					_framePath = "rq_28";
				}
				else if (_intensify == IntensifyType.BUG)
				{
					_framePath = "rq_29";
				}
				else if (_intensify == IntensifyType.ICE)
				{
					_framePath = "rq_26";
				}
				break;
			default:
				_framePath = $"kapian_0{(int)(_type - 1)}";
				break;
			}
		}

		public static string GetCardContentPath(CardType type, int lv)
		{
			switch (type)
			{
			case CardType.ATTAKER:
				if (lv <= 9)
				{
					return $"gongji_0{((lv > 21) ? 21 : lv)}";
				}
				return $"gongji_{lv}";
			case CardType.QUEEN:
			{
				int num = lv / 3 + 1;
				return $"zhuji_0{((num > 7) ? 7 : num)}";
			}
			default:
				return "";
			}
		}

		private void InitType(BuyPanelType buyPanelType)
		{
			switch (buyPanelType)
			{
			case BuyPanelType.QUEEN:
				_type = CardType.QUEEN;
				break;
			case BuyPanelType.ATTACKER:
				_type = CardType.ATTAKER;
				break;
			default:
				_type = (CardType)UnityEngine.Random.Range(2, 8);
				break;
			}
		}

		public int GetDamaged()
		{
			return _attack + _extraAttack;
		}
	}
}
