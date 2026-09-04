using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragItemSource<T> : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
{
	[SerializeField]
	public DragMode _dragMode;

	[SerializeField]
	private bool _isDraged;

	private string _sourceId;

	private T _dataItem;

	private string _groupKey;

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

	public DragMode DragMode
	{
		get
		{
			return _dragMode;
		}
		set
		{
			_dragMode = value;
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

	public bool IsDraged
	{
		get
		{
			return _isDraged;
		}
		set
		{
			_isDraged = value;
		}
	}

	protected abstract void InitUI();

	protected abstract void ResetUI();

	protected abstract void DragOk(T data);

	protected abstract void StartDrag();

	public void Init(T key, string groupKey, string sourceId)
	{
		_sourceId = sourceId;
		_groupKey = groupKey;
		Debug.Log("source:" + _groupKey);
		_dataItem = key;
		InitUI();
	}

	public void ResetData()
	{
		_isDraged = false;
		ResetUI();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (_dragMode != DragMode.ONCE || !_isDraged)
		{
			DragManager<T>.Instance.Draging(_groupKey, eventData, _dataItem, _sourceId);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (_dragMode != DragMode.ONCE || !_isDraged)
		{
			StartDrag();
			DragManager<T>.Instance.DragStart(_groupKey, eventData, _dataItem, _sourceId);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (_dragMode != DragMode.ONCE || !_isDraged)
		{
			ResetUI();
			DragManager<T>.Instance.DragEnd(_groupKey, eventData, _dataItem, _sourceId);
		}
	}

	private void DragOk(string groupKey, T data, string sourceId)
	{
		if (_groupKey == groupKey && _sourceId == sourceId)
		{
			if (_dragMode == DragMode.ONCE)
			{
				_isDraged = true;
			}
			DragOk(data);
		}
	}

	private void OnEnable()
	{
		DragManager<T>.Instance.onDragOk += DragOk;
		DragManager<T>.Instance.onReplaceData += ReplaceData;
	}

	private void ReplaceData(string groupKey, string sourceKey)
	{
		if (groupKey == _groupKey && sourceKey == _sourceId)
		{
			_isDraged = false;
			ResetUI();
		}
	}

	private void OnDisable()
	{
		DragManager<T>.Instance.onDragOk -= DragOk;
		DragManager<T>.Instance.onReplaceData -= ReplaceData;
	}
}
