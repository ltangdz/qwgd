using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
	public static DragManager instance;

	public ReasoningPage1ResultItem reasoningPage1ResultItem;

	public Canvas uiCanvas;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
public class DragManager<T>
{
	private static DragManager<T> _instance;

	public static DragManager<T> Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new DragManager<T>();
			}
			return _instance;
		}
	}

	public event Action<string, PointerEventData, T, string> onDraging;

	public event Action<string, PointerEventData, T, string> onDragEnd;

	public event Action<string, PointerEventData, T, string> onDragStart;

	public event Action<string, GameObject, T, string> onItemDragEnd;

	public event Action<string, GameObject, T, string> onItemDrag;

	public event Action<T, string, string> onCanInBag;

	public event Action<string, T, string> onDragOk;

	public event Action<string, string> onReplaceData;

	public void CanInBag(T d, string fromGuid, string targetGuid)
	{
		if (this.onCanInBag != null)
		{
			this.onCanInBag(d, fromGuid, targetGuid);
		}
	}

	public void ReplaceData(string groupKey, string itemKey)
	{
		if (this.onReplaceData != null)
		{
			this.onReplaceData(groupKey, itemKey);
		}
	}

	public void ItemDrag(string groupKey, GameObject obj, T d, string sourceId)
	{
		if (this.onItemDrag != null)
		{
			this.onItemDrag(groupKey, obj, d, sourceId);
		}
	}

	public void ItemDragEnd(string groupKey, GameObject obj, T d, string sourceId)
	{
		if (this.onItemDragEnd != null)
		{
			this.onItemDragEnd(groupKey, obj, d, sourceId);
		}
	}

	public void Draging(string groupKey, PointerEventData p, T d, string sourceId)
	{
		if (this.onDraging != null)
		{
			this.onDraging(groupKey, p, d, sourceId);
		}
	}

	public void DragStart(string groupKey, PointerEventData p, T d, string sourceId)
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(groupKey, p, d, sourceId);
		}
	}

	public void DragEnd(string groupKey, PointerEventData p, T d, string sourceId)
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(groupKey, p, d, sourceId);
		}
	}

	public void DragOk(string groupKey, T d, string sourceId)
	{
		if (this.onDragOk != null)
		{
			this.onDragOk(groupKey, d, sourceId);
		}
	}
}
