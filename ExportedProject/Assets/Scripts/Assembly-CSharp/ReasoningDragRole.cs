using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasoningDragRole : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	public Vector3 startpos;

	public string key;

	public I18NText txt_name;

	public int id;

	public string imgpath;

	public Image img_gray;

	public bool isselect;

	public Image img_role;

	public bool showHover;

	public Vector3 hover = new Vector3(1f, 1f, 1f);

	public float hoverDuration = 0.2f;

	private Vector3 _mScale;

	private bool _mStarted;

	public Transform tweenTarget;

	public List<ReasoningDragBlank> reasoningDragBlanks = new List<ReasoningDragBlank>();

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.localPosition;
		txt_name.updateTranslation5(I18N.instance.getValue(key));
		if (!_mStarted)
		{
			_mStarted = true;
			if (tweenTarget == null)
			{
				tweenTarget = base.transform;
			}
			_mScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
	}

	private bool Iscandrag()
	{
		bool result = true;
		for (int i = 0; i < reasoningDragBlanks.Count; i++)
		{
			if (reasoningDragBlanks[i].isred)
			{
				return false;
			}
		}
		return result;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isselect && Iscandrag())
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
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isselect && Iscandrag())
		{
			SetDraggedPosition(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!isselect && Iscandrag())
		{
			GameObject gameObject = IsPointerOverUIObject(eventData);
			if (gameObject != null)
			{
				isselect = true;
				img_gray.gameObject.SetActive(value: true);
				GetComponent<Image>().color = new Color32(1, 1, 1, 0);
				gameObject.GetComponent<ReasoningDragBlank>().SetRole(this);
			}
			m_rt.localPosition = startpos;
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

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			Vector3 position = worldPoint + m_offset;
			m_rt.position = position;
		}
	}

	private void OnHover(bool isOver)
	{
		if (base.enabled && showHover)
		{
			if (!_mStarted)
			{
				Start();
			}
			Vector3 b = new Vector3((tweenTarget.localScale.x > 0f) ? hover.x : (0f - hover.x), hover.y, hover.z);
			Vector3 vector = new Vector3((tweenTarget.localScale.x > 0f) ? _mScale.x : (0f - _mScale.x), _mScale.y, _mScale.z);
			Vector3 endValue = (isOver ? Vector3.Scale(_mScale, b) : vector);
			tweenTarget.DOScale(endValue, hoverDuration).SetEase(Ease.InOutQuad);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnHover(isOver: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnHover(isOver: false);
	}

	public void ResetRole()
	{
		isselect = false;
		img_gray.gameObject.SetActive(value: false);
		GetComponent<Image>().color = Color.white;
	}
}
