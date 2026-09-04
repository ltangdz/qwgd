using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragItem<T> : MonoBehaviour
{
	private RectTransform m_rt;

	private T data1;

	private Vector3 screenPos;

	public string _groupKey;

	public abstract void InitUI(T t);

	private void Start()
	{
		screenPos = Camera.main.WorldToScreenPoint(base.transform.position);
		m_rt = base.gameObject.GetComponent<RectTransform>();
		DragManager<T>.Instance.onDragStart += OnDragStart;
		DragManager<T>.Instance.onDraging += OnDraging;
		DragManager<T>.Instance.onDragEnd += OnDragEnd;
	}

	public void SetContent(T data1)
	{
		SetFront();
		InitUI(data1);
	}

	private void OnDisable()
	{
		DragManager<T>.Instance.onDragStart -= OnDragStart;
		DragManager<T>.Instance.onDraging -= OnDraging;
		DragManager<T>.Instance.onDragEnd -= OnDragEnd;
	}

	private void OnDragStart(string groupKey, PointerEventData eventData, T data, string sourceId)
	{
		Debug.Log("dragItem:" + groupKey);
		if (!(groupKey != _groupKey))
		{
			SetContent(data);
			base.transform.SetAsLastSibling();
		}
	}

	private void OnDraging(string groupKey, PointerEventData eventData, T data, string sourceId)
	{
		if (!(groupKey != _groupKey))
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = screenPos.z;
			Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
			base.transform.position = position;
			DragManager<T>.Instance.ItemDrag(_groupKey, base.gameObject, data, sourceId);
		}
	}

	private void OnDragEnd(string groupKey, PointerEventData eventData, T data, string sourceId)
	{
		if (!(groupKey != _groupKey))
		{
			DragManager<T>.Instance.ItemDragEnd(_groupKey, base.gameObject, data, sourceId);
			HideDialog();
		}
	}

	private void SetFront()
	{
		base.transform.SetAsLastSibling();
	}

	public void HideDialog()
	{
		base.transform.position = new Vector3(1196f, -140f, 0f);
	}
}
public class DragItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public Vector3 startpos;

	public string key;

	public I18NText text;

	public int id;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.position;
		text.updateTranslation5("#" + I18N.instance.getValue(key));
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (m_isPrecision)
		{
			RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint);
			m_offset = base.transform.position - worldPoint;
		}
		else
		{
			m_offset = Vector3.zero;
		}
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			gameObject.GetComponent<SelectItem>().Refresh(id, I18N.instance.getValue(key));
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			gameObject.GetComponent<SelectItem>().Refresh(id, I18N.instance.getValue(key));
		}
		m_rt.position = startpos;
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "itempanel")
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			Vector3 position = worldPoint + m_offset;
			m_rt.position = position;
		}
	}
}
