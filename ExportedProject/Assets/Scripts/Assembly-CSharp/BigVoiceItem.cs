using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BigVoiceItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
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

	public bool isred;

	public Image img_voice;

	public CanvasGroup img_bk;

	public GameObject grayitem;

	public Vector3 oriposition;

	public VoiceBlank voiceBlank;

	private bool iscandrag;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.localPosition;
		oriposition = m_rt.localPosition;
	}

	public bool IsRight()
	{
		return false;
	}

	public void ResetPos()
	{
		startpos = oriposition;
		startscale = Vector3.one;
		base.transform.DOScale(startscale, 0.2f);
		grayitem.SetActive(value: false);
		m_rt.localPosition = startpos;
		if (voiceBlank != null)
		{
			voiceBlank.bigVoiceItem = null;
			voiceBlank = null;
		}
	}

	private IEnumerator StartRed()
	{
		yield return new WaitForSeconds(1f);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (isred || !iscandrag)
		{
			return;
		}
		iscanclick = false;
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null && gameObject.GetComponent<VoiceBlank>().bigVoiceItem == null)
		{
			if (voiceBlank != null)
			{
				voiceBlank.transform.GetChild(0).gameObject.SetActive(value: false);
				if (voiceBlank.bigVoiceItem != null)
				{
					voiceBlank.bigVoiceItem.ResetPos();
				}
			}
			voiceBlank = gameObject.GetComponent<VoiceBlank>();
			voiceBlank.bigVoiceItem = this;
			startpos = gameObject.transform.localPosition;
			startscale = new Vector3(0.8f, 0.8f, 1f);
			base.transform.DOScale(startscale, 0.2f);
			grayitem.SetActive(value: true);
			m_rt.localPosition = startpos;
		}
		else if (gameObject != null && gameObject.GetComponent<VoiceBlank>().bigVoiceItem != null)
		{
			m_rt.localPosition = startpos;
		}
		else
		{
			ResetPos();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isred && !iscandrag)
		{
			Debug.LogError("置底");
			base.transform.SetAsLastSibling();
			iscandrag = true;
			iscanclick = false;
			startpos = m_rt.localPosition;
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
		if (!isred && iscandrag)
		{
			base.transform.SetAsLastSibling();
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
		if (!isred)
		{
			base.transform.DOScale(new Vector3(startscale.x * 1.05f, startscale.y * 1.05f, startscale.z * 1.05f), 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isred)
		{
			base.transform.DOScale(startscale, 0.2f);
		}
	}
}
