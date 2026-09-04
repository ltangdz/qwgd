using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.WaterPipe
{
	public class WaterPipeManager : MonoBehaviour
	{
		public GridLayoutGroup gridLayoutGroup;

		private int _level;

		public List<WaterPipeItem> prefabs;

		private List<WaterPipeItem> _itemList = new List<WaterPipeItem>();

		private List<WaterPipeItem> _finishedItemList;

		private bool _isSuccess;

		private WaterPipeItem _startItem;

		private bool _isInit;

		public bool isOver;

		private string[] _dataList = new string[40]
		{
			"1,3,2,3*4,3,1,2*3,2,4,2*1,2,3,0", "3,2,4,3*3,3,2,1*3,3,3,3*3,2,1,0", "1,2,2,3*3,2,3,2*2,0,3,4*3,3,1,3", "3,2,2,0*2,3,3,1*2,2,3,4*3,3,1,3", "1,3,2,3*4,4,1,2*2,0,2,3*3,2,2,1", "1,2,4,3*3,3,2,2*2,0,2,2*3,2,3,1", "1,3,3,0*2,2,1,2*4,3,1,4*3,2,2,3", "3,2,2,1*4,2,2,3*3,3,1,0*1,4,4,1", "3,2,2,3,1*3,3,3,3,2*3,3,4,2,3*4,3,1,0,3*1,3,2,2,3", "1,3,3,1,3*4,3,3,3,2*2,0,2,3,2*2,3,2,3,2*3,3,1,4,3",
			"1,3,2,4,3*3,3,3,3,2*3,0,2,1,4*3,3,3,3,2*1,4,2,3,1", "3,3,1,4,3*0,3,3,2,2*3,1,2,2,2*4,2,3,2,2*3,2,2,3,1", "1,4,2,3,1*3,3,1,4,4*4,2,3,1,2*2,1,2,3,3*3,3,3,2,0", "3,2,2,4,3*3,3,3,3,2*3,3,3,3,2*2,1,0,2,2*3,3,3,3,1", "1,3,2,2,3*2,2,3,2,3*4,3,3,2,0*2,3,3,3,3*3,3,3,3,1", "3,4,3,1,3*2,2,3,2,4*2,2,3,0,2*2,2,2,1,2*1,3,3,3,3", "3,2,4,3,0*3,3,2,1,2*1,2,3,2,3*4,3,1,2,3*3,2,2,2,3", "3,1,3,3,1*4,3,0,2,2*2,4,1,2,2*2,2,3,3,2*1,3,4,2,3", "3,1,3,2,3*2,3,3,3,4*4,3,1,2,2*3,3,2,2,0*1,4,3,3,1", "3,3,3,2,3*2,1,4,1,2*2,3,3,0,2*2,3,3,2,2*3,2,3,3,3",
			"1,4,3,3,4,1*3,4,1,2,3,0*2,1,3,3,1,3*4,3,3,3,3,4*2,4,1,2,2,2*1,3,2,4,3,1", "1,3,2,2,2,3*2,3,4,2,0,2*4,2,3,3,1,2*3,2,3,3,3,2*3,3,3,3,3,3*1,3,2,4,2,1", "1,4,3,1,2,3*3,3,3,4,2,3*2,3,2,3,0,3*2,1,3,3,3,3*4,3,2,2,2,1*1,3,3,3,4,3", "1,3,2,4,3,1*4,3,3,3,3,4*2,3,3,1,3,2*2,2,3,3,3,3*2,2,0,3,2,3*1,3,2,2,2,3", "3,3,3,2,2,3*2,2,2,1,3,2*2,2,2,3,4,2*2,3,4,3,1,2*3,1,3,2,3,2*0,2,3,1,4,3", "1,3,4,2,2,1*2,2,3,2,2,0*4,3,3,3,1,3*2,1,4,2,3,4*3,3,2,3,3,2*1,4,3,1,2,3", "3,2,3,3,0,1*2,1,3,3,1,4*2,4,2,2,3,2*2,2,3,1,2,2*2,2,4,3,3,4*3,3,1,3,2,3", "3,4,2,1,3,3*2,3,2,2,3,2*4,1,0,3,2,3*2,3,3,4,2,3*2,2,1,4,1,2*1,3,2,2,2,3", "3,1,3,3,3,3*4,2,3,3,3,2*2,3,0,3,2,3*2,3,2,3,1,3*3,2,3,3,3,2*1,2,4,3,3,3", "1,0,2,2,3,1*4,4,3,1,2,2*2,2,3,4,3,4*2,2,1,4,1,2*2,3,2,3,3,3*3,2,1,3,4,1",
			"3,2,2,2,4,1*2,3,3,1,3,0*3,3,3,4,3,3*3,2,3,3,3,2*2,3,3,3,3,2*1,3,2,3,3,3", "3,1,3,2,2,3*3,3,2,3,3,2*3,3,2,2,3,3*4,3,2,3,2,3*2,2,3,0,3,3*1,3,2,2,4,1", "3,2,2,0,3,3,1*2,1,4,4,3,2,2*3,3,1,2,3,3,2*1,3,3,2,3,3,2*3,3,3,3,1,2,2*3,3,3,4,3,3,4*3,2,3,3,2,2,3", "1,4,3,3,2,3,1*3,3,3,3,3,3,2*2,0,3,1,2,3,4*4,1,2,2,3,3,2*3,2,3,4,2,2,3*3,1,3,3,1,2,3*3,2,4,2,2,2,3", "3,2,2,4,2,2,1*2,1,2,3,3,2,3*4,2,2,2,3,1,0*3,2,3,3,2,4,3*3,3,2,1,3,2,4*2,1,3,3,3,3,2*3,2,2,4,2,3,1", "3,2,4,2,2,3,1*3,1,3,3,3,3,2*3,2,3,2,3,2,4*2,1,0,4,3,1,3*3,4,3,3,3,4,3*1,2,1,3,3,1,2*3,4,2,3,3,2,3", "1,3,2,4,3,1,3*4,3,1,2,2,3,4*3,3,2,2,3,3,2*1,4,3,3,3,3,3*3,0,3,2,3,3,3*2,3,3,3,1,3,3*3,3,1,4,2,4,1", "1,4,2,2,4,2,1*0,2,1,3,3,3,3*2,2,2,3,3,2,2*2,3,4,3,3,3,2*2,3,3,3,1,3,4*2,2,3,2,2,3,2*3,3,1,2,2,2,3", "3,2,2,3,3,2,3*3,3,0,2,1,3,4*1,3,3,3,3,2,1*3,4,2,3,2,3,3*3,3,1,3,3,2,4*4,2,2,2,3,3,3*3,2,2,1,3,4,1", "1,3,0,1,2,2,3*2,3,2,3,3,2,3*2,3,2,3,3,2,3*2,2,3,4,2,2,4*4,3,1,2,3,2,3*3,3,3,3,3,2,3*1,4,3,1,2,2,3"
		};

		private List<WaterPipeItem> _checkedItems = new List<WaterPipeItem>();

		private List<WaterPipeItem> _greenItems = new List<WaterPipeItem>();

		public void InitData(int level)
		{
			_isInit = false;
			_level = level;
			string[] array = _dataList[level].Split('*');
			float num = (float)array.Length * 118f;
			float endValue = 472f / num;
			float num2 = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(',');
				int num3 = array2.Length * 118;
				if (num2 < (float)num3)
				{
					num2 = num3;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					int num4 = Convert.ToInt32(array2[j]);
					WaterPipeItem waterPipeItem = UnityEngine.Object.Instantiate(prefabs[num4], base.transform);
					waterPipeItem.Manager = this;
					if (num4 == 0)
					{
						_startItem = waterPipeItem;
					}
					waterPipeItem.Show(UnityEngine.Random.Range(0, 4));
					_itemList.Add(waterPipeItem);
				}
			}
			base.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(num, num2);
			base.transform.DOScale(endValue, 0f);
			_isInit = true;
			Invoke("RefreshColor", 0.1f);
		}

		private void Awake()
		{
			_finishedItemList = new List<WaterPipeItem>();
		}

		public void FinishItem(WaterPipeItem waterPipeItem)
		{
			if (!_isSuccess && !isOver && !_finishedItemList.Contains(waterPipeItem))
			{
				_finishedItemList.Add(waterPipeItem);
			}
		}

		public void RemoveItem(WaterPipeItem waterPipeItem)
		{
			if (!_isSuccess && !isOver)
			{
				_finishedItemList.Remove(waterPipeItem);
			}
		}

		public void RefreshColor()
		{
			_checkedItems.Clear();
			if (_isInit && !isOver)
			{
				for (int i = 0; i < _itemList.Count; i++)
				{
					_itemList[i].SetColor(isGreen: false);
				}
				_greenItems.Clear();
				SetSuccessColor(null, _startItem);
			}
		}

		private void SetSuccessColor(WaterPipeItem from, WaterPipeItem item)
		{
			if (_checkedItems.Contains(item))
			{
				return;
			}
			List<WaterPipeCollider> colliders = item.colliders;
			int count = colliders.Count;
			_checkedItems.Add(item);
			for (int i = 0; i < count; i++)
			{
				List<WaterPipeItem> triggerStayItemList = colliders[i].triggerStayItemList;
				if (triggerStayItemList.Count > 0)
				{
					item.SetColor(isGreen: true);
					if (!_greenItems.Contains(item))
					{
						_greenItems.Add(item);
					}
				}
				for (int j = 0; j < triggerStayItemList.Count; j++)
				{
					WaterPipeItem waterPipeItem = triggerStayItemList[j];
					if (!(waterPipeItem == from))
					{
						SetSuccessColor(item, waterPipeItem);
					}
				}
			}
			if (!_isSuccess && !isOver && _greenItems.Count == _itemList.Count)
			{
				_isSuccess = true;
				for (int k = 0; k < _greenItems.Count; k++)
				{
					_greenItems[k].Success();
				}
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_GAMME, 0);
			}
		}
	}
}
