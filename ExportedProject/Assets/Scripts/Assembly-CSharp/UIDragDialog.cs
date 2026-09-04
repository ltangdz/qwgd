using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragDialog : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public float offy;

	public RectTransform m_parent;

	public Vector3 oripostion0;

	public Vector3 oripostion;

	public Vector2 limitminpos;

	public Vector2 limitmaxpos;

	public bool islimit = true;

	public GameManager gameManager;

	public bool ismain;

	public bool iscandrag = true;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		oripostion0 = m_rt.localPosition;
		oripostion = m_rt.localPosition;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void ResetPosition(bool isrealori = false)
	{
		if (isrealori)
		{
			m_rt.localPosition = oripostion0;
		}
		else
		{
			m_rt.localPosition = oripostion;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!ismain)
		{
			base.transform.parent.SetAsLastSibling();
		}
		else
		{
			base.transform.SetAsLastSibling();
		}
		if (iscandrag)
		{
			oripostion = m_rt.localPosition;
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
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			SetDraggedPosition(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			SetDraggedPosition(eventData);
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		if (!iscandrag)
		{
			return null;
		}
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
			Vector3 vector = worldPoint + m_offset;
			if (vector.x <= limitminpos.x)
			{
				vector.x = limitminpos.x;
			}
			else if (vector.x >= limitmaxpos.x)
			{
				vector.x = limitmaxpos.x;
			}
			if (vector.y <= limitminpos.y)
			{
				vector.y = limitminpos.y;
			}
			else if (vector.y >= limitmaxpos.y)
			{
				vector.y = limitmaxpos.y;
			}
			m_rt.position = (islimit ? vector : (worldPoint + m_offset));
		}
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (!ismain)
		{
			base.transform.parent.SetAsLastSibling();
		}
		else
		{
			base.transform.SetAsLastSibling();
		}
	}

	public void SetFront()
	{
		base.transform.SetAsLastSibling();
	}
}
