using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAnswer2 : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
{
	[SerializeField]
	private bool iscanclick = true;

	public int id;

	private RectTransform m_rt;

	public Vector3 startpos;

	public Vector3 startscale;

	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	public QuestionPanel roleFourBlank;

	public int pos = -1;

	public int currentroleid;

	public Sprite[] sprites;

	public Image img_bk;

	public Text txt_content;

	public Color[] colors;

	private bool iscandrag;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.localPosition;
	}

	public bool IsRight()
	{
		if (roleFourBlank != null)
		{
			return currentroleid == roleFourBlank.id;
		}
		return false;
	}

	public void ResetPos()
	{
		m_rt.DOLocalMove(startpos, 0.2f);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!iscandrag)
		{
			return;
		}
		iscanclick = false;
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (gameObject.GetComponent<QuestionPanel>().isok)
			{
				ResetPos();
			}
			else if (gameObject.GetComponent<QuestionPanel>().id != id || !gameObject.GetComponent<QuestionPanel>().iscanclick)
			{
				gameObject.GetComponent<QuestionPanel>().Wrong();
				ResetPos();
			}
			else
			{
				gameObject.GetComponent<QuestionPanel>().Right();
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			ResetPos();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (iscanclick && !iscandrag)
		{
			iscandrag = true;
			iscanclick = false;
			base.transform.SetAsLastSibling();
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

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			Vector3 position = worldPoint + m_offset;
			m_rt.position = position;
		}
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOScale(new Vector3(startscale.x * 1.05f, startscale.y * 1.05f, startscale.z * 1.05f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOScale(startscale, 0.2f);
	}
}
