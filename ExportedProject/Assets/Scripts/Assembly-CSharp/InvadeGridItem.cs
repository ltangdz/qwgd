using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvadeGridItem : AlubaUIBase
{
	private int[] _curGrid;

	public Image _image;

	public Sprite[] _Sprites;

	private List<int> _roundIndexList = new List<int>();

	public int _rowCount;

	public int _columnCount;

	private bool _isOpen;

	private bool _canClick;

	public int[] CurGrid
	{
		get
		{
			return _curGrid;
		}
		set
		{
			_curGrid = value;
		}
	}

	public Image Image
	{
		get
		{
			return _image;
		}
		set
		{
			_image = value;
		}
	}

	public Sprite[] Sprites
	{
		get
		{
			return _Sprites;
		}
		set
		{
			_Sprites = value;
		}
	}

	public List<int> RoundIndexList
	{
		get
		{
			return _roundIndexList;
		}
		set
		{
			_roundIndexList = value;
		}
	}

	public int RowCount
	{
		get
		{
			return _rowCount;
		}
		set
		{
			_rowCount = value;
		}
	}

	public int ColumnCount
	{
		get
		{
			return _columnCount;
		}
		set
		{
			_columnCount = value;
		}
	}

	public bool IsOpen
	{
		get
		{
			return _isOpen;
		}
		set
		{
			_isOpen = value;
		}
	}

	public bool CanClick
	{
		get
		{
			return _canClick;
		}
		set
		{
			_canClick = value;
		}
	}

	public void InitData(int curIndex, string groupName, int rowCount, int columnCount, bool isOpen)
	{
		base.InitData(curIndex, groupName);
		_rowCount = rowCount;
		_columnCount = columnCount;
		int num = curIndex / _rowCount;
		int num2 = curIndex % columnCount;
		_curGrid = new int[2] { num, num2 };
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (num == 0)
		{
			num3 = -1;
		}
		else if (num == _rowCount - 1)
		{
			num4 = -1;
		}
		if (num2 == 0)
		{
			num5 = -1;
		}
		else if (num2 == _columnCount - 1)
		{
			num6 = -1;
		}
		if (num3 != -1)
		{
			_roundIndexList.Add(curIndex - _rowCount);
		}
		if (num4 != -1)
		{
			_roundIndexList.Add(curIndex + _rowCount);
		}
		if (num5 != -1)
		{
			_roundIndexList.Add(curIndex - 1);
		}
		if (num6 != -1)
		{
			_roundIndexList.Add(curIndex + 1);
		}
		_roundIndexList.Add(base.CurIndex);
		_isOpen = isOpen;
		_image.sprite = _Sprites[isOpen ? 1 : 0];
	}

	public void ResetData(bool isOpen)
	{
		_isOpen = isOpen;
		if (isOpen)
		{
			_image.sprite = _Sprites[1];
		}
		else
		{
			_image.sprite = _Sprites[0];
		}
	}

	private void SetOpen(bool isOpen)
	{
		_isOpen = isOpen;
		float speed = 0.15f;
		_image.transform.DOScaleX(0f, speed).OnComplete(delegate
		{
			if (isOpen)
			{
				_image.sprite = _Sprites[1];
			}
			else
			{
				_image.sprite = _Sprites[0];
			}
			_image.transform.DOScaleX(1f, speed).OnComplete(delegate
			{
				InvadeEvent.Instance.NoticeItemAnimationFinished(base.CurIndex);
			});
		});
	}

	private void NoticeItemChange(List<int> obj)
	{
		if (obj.Contains(base.CurIndex))
		{
			SetOpen(!_isOpen);
		}
	}

	private void NoticeItemCanClick()
	{
		_canClick = true;
	}

	private void OnEnable()
	{
		InvadeEvent.Instance.onNoticeItemChange += NoticeItemChange;
		InvadeEvent.Instance.onNoticeItemCanClick += NoticeItemCanClick;
	}

	private void OnDisable()
	{
		InvadeEvent.Instance.onNoticeItemChange -= NoticeItemChange;
		InvadeEvent.Instance.onNoticeItemCanClick -= NoticeItemCanClick;
	}

	protected override void PointerEnter(PointerEventData eventData)
	{
	}

	protected override void PointerExit(PointerEventData eventData)
	{
	}

	protected override void TouchDown(PointerEventData eventData)
	{
	}

	protected override void TouchUp(PointerEventData eventData)
	{
		if (_canClick)
		{
			InvadeEvent.Instance.NoticeItemChange(_roundIndexList);
		}
	}

	protected override void OnClick(PointerEventData eventData)
	{
	}
}
