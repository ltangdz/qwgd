using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLC7.DDOS
{
	public class BagGrid : DragBagGrid<Card>, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		[SerializeField]
		private PositionType _positionType;

		[SerializeField]
		private List<BagGrid> _roundGrids;

		private Rect _areaRect;

		private RectTransform _rt;

		private bool _isLock;

		private CardItem _curCardItem;

		private DDOSManager _ddosManager;

		private CardTip _cardTip;

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

		public List<BagGrid> RoundGrids
		{
			get
			{
				if (_roundGrids == null)
				{
					_roundGrids = new List<BagGrid>();
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

		public CardItem CurCardItem => _curCardItem;

		private void Start()
		{
			base.GroupKey = "Card";
		}

		protected override void InitUI()
		{
		}

		protected override void StartDrag()
		{
			if (_curCardItem != null)
			{
				_curCardItem.StartDrag();
			}
		}

		protected override void EndDrag()
		{
			if (_curCardItem != null)
			{
				_curCardItem.DragEnd();
			}
		}

		protected override bool CanDrag()
		{
			return base.DataItem != null;
		}

		public void Cancel()
		{
		}

		public void TrySave(BagGrid sourceGrid)
		{
			if (base.DataItem == null)
			{
				if (PositionType == PositionType.BAG)
				{
					if (_isLock)
					{
						sourceGrid.Cancel();
					}
					else
					{
						Save(sourceGrid);
					}
				}
				else if (PositionType == PositionType.ATTACKER)
				{
					if (sourceGrid.DataItem.Type == CardType.QUEEN || sourceGrid.DataItem.Type == CardType.ATTAKER)
					{
						Save(sourceGrid);
					}
					else
					{
						sourceGrid.Cancel();
					}
				}
				return;
			}
			Card dataItem = sourceGrid.DataItem;
			if (PositionType == PositionType.BAG)
			{
				if ((dataItem.IsEffectCard() && base.DataItem.IsEffectCard()) || (dataItem.IsEffectCard() && !base.DataItem.IsEffectCard()) || (!dataItem.IsEffectCard() && base.DataItem.IsEffectCard()))
				{
					sourceGrid.Cancel();
				}
				else
				{
					NormalControl(sourceGrid);
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
							sourceGrid.Cancel();
							return;
						}
						if (dataItem.Type == CardType.CARD_OVERCLOCK_QUEEN || dataItem.Type == CardType.CARD_TRANSFER_QUEEN)
						{
							_curCardItem.PlayEffectAnimation(dataItem.Type);
						}
						sourceGrid.RemoveCard();
					}
					else if (base.DataItem.Type == CardType.ATTAKER)
					{
						if (dataItem.IsQueenEffectCard())
						{
							sourceGrid.Cancel();
							return;
						}
						_curCardItem.PlayEffectAnimation(dataItem.Type);
						sourceGrid.RemoveCard();
					}
				}
				else
				{
					NormalControl(sourceGrid);
				}
			}
		}

		private void NormalControl(BagGrid sourceGrid)
		{
			Card dataItem = sourceGrid.DataItem;
			if (dataItem.Type != base.DataItem.Type)
			{
				ExchangeCard(sourceGrid);
			}
			else if (dataItem.Type == base.DataItem.Type && !dataItem.IsEffectCard())
			{
				Fusion(sourceGrid);
			}
		}

		private void Save(BagGrid sourceGrid)
		{
			AddCard(sourceGrid.DataItem);
			sourceGrid.RemoveCard();
		}

		private void Fusion(BagGrid sourceGrid)
		{
			Card dataItem = sourceGrid.DataItem;
			if (dataItem.IsEffectCard() || dataItem.Lv != base.DataItem.Lv || dataItem.Type != base.DataItem.Type)
			{
				sourceGrid.Cancel();
				return;
			}
			IntensifyType intensify = dataItem.Intensify;
			if (base.DataItem.Intensify == IntensifyType.NONE || intensify != IntensifyType.NONE)
			{
				base.DataItem.Intensify = intensify;
			}
			_curCardItem.Upgrade(base.DataItem.Intensify);
			sourceGrid.RemoveCard();
		}

		private void ExchangeCard(BagGrid sourceGrid)
		{
			Card dataItem = base.DataItem;
			Card dataItem2 = sourceGrid.DataItem;
			sourceGrid.AddCard(dataItem);
			AddCard(dataItem2);
		}

		public void RemoveCard()
		{
			Debug.Log("RemoveCard");
			base.DataItem = null;
			if (_curCardItem != null)
			{
				DdosManager.SpawnPool.Despawn(_curCardItem.transform);
				_curCardItem = null;
			}
		}

		public void LockBag()
		{
			_isLock = true;
		}

		public void AddCard(Card card)
		{
			if (base.DataItem != null)
			{
				RemoveCard();
			}
			_isLock = false;
			CardItem component = DdosManager.SpawnPool.Spawn("CardItem", Vector2.zero, base.transform.rotation, base.transform).GetComponent<CardItem>();
			component.transform.position = base.transform.position;
			base.DataItem = card;
			component.InitData(card, _positionType);
			_curCardItem = component;
		}

		public void ShowTip()
		{
			Debug.Log("ShowTip");
			if (!(_curCardItem == null))
			{
				Card card = _curCardItem.Card;
				if (card != null)
				{
					_cardTip = DdosManager.SpawnPool.Spawn("TipImage", base.transform).GetComponent<CardTip>();
					_cardTip.InitData(card);
					_cardTip.transform.position = base.transform.position;
				}
			}
		}

		public void HideTip()
		{
			if ((bool)_cardTip)
			{
				_cardTip.Hide();
				_cardTip = null;
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
