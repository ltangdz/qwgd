using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class UIDragPicItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
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

	public Transform oriparent;

	public int parentpos;

	public Transform newparent;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		oripostion0 = m_rt.localPosition;
		oripostion = m_rt.localPosition;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		oriparent = base.transform.parent;
		parentpos = base.transform.GetSiblingIndex();
		newparent = gameManager.homeScene.middle;
	}

	public void ResetPosition(bool isrealori = false)
	{
		m_rt.localPosition = oripostion0;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent.GetComponent<RectTransform>());
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		base.transform.parent = newparent;
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
		base.transform.parent = oriparent;
		base.transform.SetSiblingIndex(parentpos);
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (gameObject.tag.Equals("search"))
			{
				_ = gameManager.dataManager.dic1[GetComponent<ItemNormal>().itemid];
				ResetPosition();
			}
			else if (gameObject.tag.Equals("scandialog"))
			{
				DATA1 dATA = gameManager.dataManager.dic1[base.transform.parent.GetComponent<ItemPicture>().itemid];
				gameObject.transform.parent.parent.parent.parent.parent.GetComponent<ScanDialog>().StartScan(dATA.ID.ToString());
				ResetPosition();
			}
			else if (gameObject.tag.Equals("password_cont"))
			{
				_ = gameManager.dataManager.dic1[GetComponent<ItemNormal>().itemid].percent != "0";
				ResetPosition();
			}
			else if (gameObject.tag.Equals("loginTxt"))
			{
				DATA1 data = gameManager.dataManager.dic1[GetComponent<ItemNormal>().itemid];
				gameObject.transform.parent.parent.GetComponent<BrowserMail>().SetItem(gameObject, data);
				ResetPosition();
			}
			else if (gameObject.tag.Equals("toothbooklogin"))
			{
				DATA1 data2 = gameManager.dataManager.dic1[GetComponent<ItemNormal>().itemid];
				gameObject.transform.parent.parent.parent.GetComponent<Login>().SetItem(gameObject, data2);
				ResetPosition();
			}
			else if (gameObject.tag.Equals("toothbooklogin2"))
			{
				DATA1 data3 = gameManager.dataManager.dic1[GetComponent<ItemNormal>().itemid];
				gameObject.transform.parent.parent.GetComponent<Login>().SetItem(gameObject, data3);
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
			if (list[i].gameObject.tag == "search" || list[i].gameObject.tag == "scandialog" || list[i].gameObject.tag == "password_cont" || list[i].gameObject.tag == "loginTxt" || list[i].gameObject.tag == "toothbooklogin" || list[i].gameObject.tag == "toothbooklogin2")
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
}
