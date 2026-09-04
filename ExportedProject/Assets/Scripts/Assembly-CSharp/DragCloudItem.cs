using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCloudItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public Vector3 startpos;

	public string key;

	public int id;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Sprite[] sprites;

	public string dialogname;

	public Reasoning4007Step03 reasoning4007Step03;

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
			img_bk.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
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
		if (!iscandrag)
		{
			return;
		}
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (gameObject.name.Equals(dialogname))
			{
				gameObject.transform.GetChild(1).GetComponent<Image>().DOFade(0f, 0.5f);
				gameObject.transform.GetChild(2).GetComponent<Image>().DOFade(0f, 0.5f);
				base.gameObject.SetActive(value: false);
				reasoning4007Step03.Check1();
			}
			else
			{
				gameObject.GetComponent<DialogItem>().Red();
				img_bk.transform.localScale = Vector3.one;
				base.transform.localPosition = startpos;
			}
		}
		else
		{
			img_bk.transform.localScale = Vector3.one;
			base.transform.localPosition = startpos;
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
			Vector3 position = worldPoint + m_offset;
			m_rt.position = position;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (iscandrag)
		{
			img_bk.sprite = sprites[1];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (iscandrag)
		{
			img_bk.sprite = sprites[0];
		}
	}
}
