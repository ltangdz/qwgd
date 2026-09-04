using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFrame : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public Vector3 startpos;

	public string key;

	public int id;

	public List<DateItem> dateitemlist = new List<DateItem>();

	public int currentpos;

	[SerializeField]
	private Image img_frame;

	[SerializeField]
	private Image img_arrow;

	[SerializeField]
	private Image img_riqi;

	[SerializeField]
	private Sprite[] sprites;

	public bool iscandrag = true;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.localPosition;
	}

	public void ShowWrong()
	{
		StartCoroutine(StartRed());
	}

	private IEnumerator StartRed()
	{
		iscandrag = false;
		img_frame.sprite = sprites[0];
		img_arrow.sprite = sprites[1];
		img_riqi.sprite = sprites[2];
		img_frame.DOFade(0.2f, 0.1f);
		img_arrow.DOFade(0.2f, 0.1f);
		img_riqi.DOFade(0.2f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_frame.DOFade(1f, 0.1f);
		img_arrow.DOFade(1f, 0.1f);
		img_riqi.DOFade(1f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_frame.DOFade(0.2f, 0.1f);
		img_arrow.DOFade(0.2f, 0.1f);
		img_riqi.DOFade(0.2f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_frame.DOFade(1f, 0.1f);
		img_arrow.DOFade(1f, 0.1f);
		img_riqi.DOFade(1f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_frame.sprite = sprites[6];
		img_arrow.sprite = sprites[7];
		img_riqi.sprite = sprites[8];
		yield return new WaitForSeconds(0.1f);
		iscandrag = true;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (iscandrag)
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
			img_frame.transform.localScale = Vector3.one;
			SetDraggedPosition(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!iscandrag)
		{
			return;
		}
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (!(gameObject != null) || !(gameObject.transform.parent.GetComponent<DateItem>() != null))
		{
			return;
		}
		startpos = gameObject.transform.parent.localPosition;
		currentpos = gameObject.transform.parent.GetComponent<DateItem>().pos;
		for (int i = 0; i < dateitemlist.Count; i++)
		{
			if (dateitemlist[i].pos != currentpos)
			{
				dateitemlist[i].SetGray();
			}
			else
			{
				dateitemlist[i].SetBlue();
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			SetDraggedPosition(eventData);
			m_rt.localPosition = new Vector3(startpos.x, m_rt.localPosition.y, m_rt.localPosition.z);
			img_frame.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f);
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
				Debug.Log(list[i].gameObject.name);
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
			m_rt.position = new Vector3(vector.x, m_rt.position.y, m_rt.position.z);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (iscandrag)
		{
			img_frame.sprite = sprites[3];
			img_arrow.sprite = sprites[4];
			img_riqi.sprite = sprites[5];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (iscandrag)
		{
			img_frame.sprite = sprites[6];
			img_arrow.sprite = sprites[7];
			img_riqi.sprite = sprites[8];
		}
	}
}
