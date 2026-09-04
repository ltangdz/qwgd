using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragItemTarget<T> : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private bool _isEnter;

	private DragInType _dragInType;

	[SerializeField]
	private string _groupKey = "";

	private T _dataItem;

	private string _sourcekey;

	[SerializeField]
	private int _index;

	private bool _isDrag;

	public Transform _overlapTransform;

	public bool IsEnter
	{
		get
		{
			return _isEnter;
		}
		set
		{
			_isEnter = value;
		}
	}

	public DragInType DragInType
	{
		get
		{
			return _dragInType;
		}
		set
		{
			_dragInType = value;
		}
	}

	public string GroupKey
	{
		get
		{
			return _groupKey;
		}
		set
		{
			_groupKey = value;
		}
	}

	public T DataItem
	{
		get
		{
			return _dataItem;
		}
		set
		{
			_dataItem = value;
		}
	}

	public int Index
	{
		get
		{
			return _index;
		}
		set
		{
			_index = value;
		}
	}

	public string Sourcekey
	{
		get
		{
			return _sourcekey;
		}
		set
		{
			_sourcekey = value;
		}
	}

	public bool IsDrag
	{
		get
		{
			return _isDrag;
		}
		set
		{
			_isDrag = value;
		}
	}

	public Transform OverlapTransform
	{
		get
		{
			return _overlapTransform;
		}
		set
		{
			_overlapTransform = value;
		}
	}

	protected abstract void InitUI();

	protected abstract void ClearUI();

	protected abstract void ResetUI();

	protected abstract void IsEnterUI();

	protected abstract void DragOk();

	protected abstract void OnDragEnd();

	public abstract bool ValidResult();

	public void Init(string groupKey, int index, DragInType dragInType)
	{
		_dragInType = dragInType;
		_index = index;
		_groupKey = groupKey;
		InitUI();
	}

	public void ClearData()
	{
		_dataItem = default(T);
		_sourcekey = "";
		_isDrag = false;
		_isEnter = false;
		ClearUI();
	}

	public void ResetData()
	{
		ResetUI();
	}

	public bool ValidData()
	{
		return ValidResult();
	}

	private void OnItemDragEnd(string groupKey, GameObject arg2, T arg3, string sourceId)
	{
		if (groupKey != _groupKey)
		{
			return;
		}
		if (_isEnter)
		{
			if (sourceId != _sourcekey)
			{
				DragManager<T>.Instance.ReplaceData(_groupKey, _sourcekey);
				_sourcekey = sourceId;
			}
			_dataItem = arg3;
			DragOk();
			DragManager<T>.Instance.DragOk(groupKey, arg3, sourceId);
		}
		OnDragEnd();
	}

	private void OnItemDrag(string groupKey, GameObject arg2, T arg3, string sourceId)
	{
		if (groupKey != _groupKey)
		{
			return;
		}
		bool flag = AlubaTools.IsRectTransformOverlap(arg2.GetComponent<RectTransform>(), _overlapTransform.GetComponent<RectTransform>());
		if (_dragInType == DragInType.GAMEOBJECT)
		{
			if (flag)
			{
				_isEnter = true;
			}
			else
			{
				_isEnter = false;
			}
		}
		IsEnterUI();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (_isDrag && _dragInType == DragInType.MOUSE)
		{
			_isEnter = false;
			IsEnterUI();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (_isDrag && _dragInType == DragInType.MOUSE)
		{
			_isEnter = true;
			IsEnterUI();
		}
	}

	private void OnEnable()
	{
		DragManager<T>.Instance.onItemDrag += OnItemDrag;
		DragManager<T>.Instance.onDragStart += DragStart;
		DragManager<T>.Instance.onDragEnd += DragEnd;
		DragManager<T>.Instance.onItemDragEnd += OnItemDragEnd;
	}

	private void OnDisable()
	{
		DragManager<T>.Instance.onDragStart -= DragStart;
		DragManager<T>.Instance.onDragEnd -= DragEnd;
		DragManager<T>.Instance.onItemDrag -= OnItemDrag;
		DragManager<T>.Instance.onItemDragEnd -= OnItemDragEnd;
	}

	private void DragEnd(string arg1, PointerEventData arg2, T arg3, string sourceId)
	{
		_isDrag = true;
	}

	private void DragStart(string arg1, PointerEventData arg2, T arg3, string sourceId)
	{
		_isDrag = false;
	}
}
