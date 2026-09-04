using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragPanel : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int chessid;

	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public float offy;

	public Vector3 oripostion0;

	public Vector3 oripostion;

	public GameManager gameManager;

	public RectTransform img_pic;

	public string[] itemid;

	public Transform oriparent;

	public int parentpos;

	public Transform newparent;

	public string content;

	private void Start()
	{
		m_rt = img_pic.GetComponent<RectTransform>();
		oripostion0 = m_rt.localPosition;
		oripostion = m_rt.localPosition;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		oriparent = base.transform.parent;
		parentpos = base.transform.GetSiblingIndex();
		newparent = gameManager.homeScene.goalDialog.transform.parent;
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
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent.GetComponent<RectTransform>());
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		img_pic.gameObject.SetActive(value: true);
		img_pic.transform.parent = newparent;
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

	public void OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		img_pic.gameObject.SetActive(value: false);
		base.transform.parent = oriparent;
		base.transform.SetSiblingIndex(parentpos);
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (gameObject.tag.Equals("itembox"))
			{
				gameManager.homeScene.notebook.AddNewItems(itemid);
				ResetPosition();
			}
			else
			{
				ResetPosition();
			}
		}
		else
		{
			ResetPosition();
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "itembox")
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
			m_rt.position = worldPoint + m_offset;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}
}
