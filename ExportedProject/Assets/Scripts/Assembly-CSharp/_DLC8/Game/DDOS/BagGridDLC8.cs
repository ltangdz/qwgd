using System.Collections.Generic;
using Aluba;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _DLC8.Game.DDOS
{
	public class BagGridDLC8 : DragBagGrid<CardDLC8>, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		[SerializeField]
		private PositionType _positionType;

		[SerializeField]
		private List<BagGridDLC8> _roundGrids;

		private Rect _areaRect;

		private RectTransform _rt;

		private bool _isLock;

		private CardItemDLC8 _curCardItemDlc8;

		private DDOSManagerDLC8 _ddosManagerDlc8;

		private CardTipDLC8 _cardTipDlc8;

		public PositionType PositionType
		{
			get
			{
				return _positionType;
			}
			set
			{
				_positionType = value;
			}
		}

		public List<BagGridDLC8> RoundGrids
		{
			get
			{
				if (_roundGrids == null)
				{
					_roundGrids = new List<BagGridDLC8>();
				}
				return _roundGrids;
			}
			set
			{
				_roundGrids = value;
			}
		}

		public Rect AreaRect
		{
			get
			{
				_areaRect = new Rect(RT.localPosition.x - RT.sizeDelta.x / 2f, RT.localPosition.y - RT.sizeDelta.y / 2f, 150f, 150f);
				return _areaRect;
			}
			set
			{
				_areaRect = value;
			}
		}

		public RectTransform RT
		{
			get
			{
				if (_rt == null)
				{
					_rt = GetComponent<RectTransform>();
				}
				return _rt;
			}
			set
			{
				_rt = value;
			}
		}

		public bool IsLock => _isLock;

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

		public CardItemDLC8 CurCardItemDlc8 => _curCardItemDlc8;

		private void Start()
		{
			base.GroupKey = "Card";
		}

		protected override void InitUI()
		{
		}

		protected override void StartDrag()
		{
			if (_curCardItemDlc8 != null)
			{
				_curCardItemDlc8.StartDrag();
			}
		}

		protected override void EndDrag()
		{
			if (_curCardItemDlc8 != null)
			{
				_curCardItemDlc8.DragEnd();
			}
		}

		protected override bool CanDrag()
		{
			return base.DataItem != null;
		}

		public void Cancel()
		{
		}

		public void TrySave(BagGridDLC8 sourceGridDlc8)
		{
			if (base.DataItem == null)
			{
				if (PositionType == PositionType.BAG)
				{
					if (_isLock)
					{
						sourceGridDlc8.Cancel();
					}
					else
					{
						Save(sourceGridDlc8);
					}
				}
				else if (PositionType == PositionType.ATTACKER)
				{
					if (sourceGridDlc8.DataItem.Type == CardType.QUEEN || sourceGridDlc8.DataItem.Type == CardType.ATTAKER)
					{
						Save(sourceGridDlc8);
					}
					else
					{
						sourceGridDlc8.Cancel();
					}
				}
				return;
			}
			CardDLC8 dataItem = sourceGridDlc8.DataItem;
			if (PositionType == PositionType.BAG)
			{
				if ((dataItem.IsEffectCard() && base.DataItem.IsEffectCard()) || (dataItem.IsEffectCard() && !base.DataItem.IsEffectCard()) || (!dataItem.IsEffectCard() && base.DataItem.IsEffectCard()))
				{
					sourceGridDlc8.Cancel();
				}
				else
				{
					NormalControl(sourceGridDlc8);
				}
			}
			else
			{
				if (PositionType != PositionType.ATTACKER)
				{
					return;
				}
				if (dataItem.IsEffectCard())
				{
					if (base.DataItem.Type == CardType.QUEEN)
					{
						if (dataItem.IsAttackEffectCard())
						{
							sourceGridDlc8.Cancel();
							return;
						}
						if (dataItem.Type == CardType.CARD_OVERCLOCK_QUEEN || dataItem.Type == CardType.CARD_TRANSFER_QUEEN)
						{
							_curCardItemDlc8.PlayEffectAnimation(dataItem.Type);
						}
						sourceGridDlc8.RemoveCard();
					}
					else if (base.DataItem.Type == CardType.ATTAKER)
					{
						if (dataItem.IsQueenEffectCard())
						{
							sourceGridDlc8.Cancel();
							return;
						}
						_curCardItemDlc8.PlayEffectAnimation(dataItem.Type);
						sourceGridDlc8.RemoveCard();
					}
				}
				else
				{
					NormalControl(sourceGridDlc8);
				}
			}
		}

		private void NormalControl(BagGridDLC8 sourceGridDlc8)
		{
			CardDLC8 dataItem = sourceGridDlc8.DataItem;
			if (dataItem.Type != base.DataItem.Type)
			{
				ExchangeCard(sourceGridDlc8);
			}
			else if (dataItem.Type == base.DataItem.Type && !dataItem.IsEffectCard())
			{
				Fusion(sourceGridDlc8);
			}
		}

		private void Save(BagGridDLC8 sourceGridDlc8)
		{
			AddCard(sourceGridDlc8.DataItem);
			sourceGridDlc8.RemoveCard();
		}

		private void Fusion(BagGridDLC8 sourceGridDlc8)
		{
			CardDLC8 dataItem = sourceGridDlc8.DataItem;
			int cardMaxLevel = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData().cardMaxLevel;
			if (dataItem.Lv == cardMaxLevel || base.DataItem.Lv == cardMaxLevel)
			{
				sourceGridDlc8.Cancel();
				return;
			}
			if (dataItem.IsEffectCard() || dataItem.Lv != base.DataItem.Lv || dataItem.Type != base.DataItem.Type)
			{
				sourceGridDlc8.Cancel();
				return;
			}
			IntensifyType intensify = dataItem.Intensify;
			if (base.DataItem.Intensify == IntensifyType.NONE || intensify != IntensifyType.NONE)
			{
				base.DataItem.Intensify = intensify;
			}
			_curCardItemDlc8.Upgrade(base.DataItem.Intensify);
			sourceGridDlc8.RemoveCard();
		}

		private void ExchangeCard(BagGridDLC8 sourceGridDlc8)
		{
			CardDLC8 dataItem = base.DataItem;
			CardDLC8 dataItem2 = sourceGridDlc8.DataItem;
			sourceGridDlc8.AddCard(dataItem);
			AddCard(dataItem2);
		}

		public void RemoveCard()
		{
			base.DataItem = null;
			if (_curCardItemDlc8 != null)
			{
				DdosManagerDlc8.SpawnPool.Despawn(_curCardItemDlc8.transform);
				_curCardItemDlc8 = null;
			}
		}

		public void LockBag()
		{
			_isLock = true;
		}

		public void AddCard(CardDLC8 cardDlc8)
		{
			if (base.DataItem != null)
			{
				RemoveCard();
			}
			_isLock = false;
			CardItemDLC8 component = DdosManagerDlc8.SpawnPool.Spawn("CardItemDLC8", Vector2.zero, base.transform.rotation, base.transform).GetComponent<CardItemDLC8>();
			component.transform.position = base.transform.position;
			base.DataItem = cardDlc8;
			component.InitData(cardDlc8, _positionType);
			_curCardItemDlc8 = component;
		}

		public void ShowTip()
		{
			if (!(_curCardItemDlc8 == null))
			{
				CardDLC8 cardDlc = _curCardItemDlc8.CardDlc8;
				if (cardDlc != null)
				{
					_cardTipDlc8 = DdosManagerDlc8.SpawnPool.Spawn("TipImageDLC8", base.transform).GetComponent<CardTipDLC8>();
					_cardTipDlc8.InitData(cardDlc);
					_cardTipDlc8.transform.position = base.transform.position;
				}
			}
		}

		public void HideTip()
		{
			if ((bool)_cardTipDlc8)
			{
				_cardTipDlc8.Hide();
				_cardTipDlc8 = null;
			}
		}

		private void Awake()
		{
			base.GroupKey = "Card";
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Invoke("ShowTip", 1f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			CancelInvoke("ShowTip");
			HideTip();
		}
	}
}
