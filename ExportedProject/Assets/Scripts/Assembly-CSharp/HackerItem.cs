using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HackerItem : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	private RectTransform m_rt;

	[Header("类型-0 正方形  1长方形  2 圆形")]
	public int type;

	public bool iscandrag = true;

	[SerializeField]
	private Vector3 prelocalpos;

	[SerializeField]
	private HackerDialog hackerDialog;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			prelocalpos = base.transform.localPosition;
			SetFront();
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
		GameObject touchobject = IsPointerOverUIObject(eventData);
		if (touchobject != null)
		{
			Debug.Log("ok" + touchobject.name);
			m_rt.localPosition = touchobject.transform.localPosition;
			touchobject.GetComponent<HackerItem>().iscandrag = false;
			touchobject.transform.DOLocalMove(prelocalpos, 0.5f).OnComplete(delegate
			{
				touchobject.GetComponent<HackerItem>().iscandrag = true;
				touchobject.GetComponent<HackerItem>().prelocalpos = prelocalpos;
			});
			hackerDialog.ChangeItem(this, touchobject.GetComponent<HackerItem>());
		}
		else
		{
			iscandrag = false;
			base.transform.DOLocalMove(prelocalpos, 0.5f).OnComplete(delegate
			{
				iscandrag = true;
			});
		}
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			m_rt.position = worldPoint + m_offset;
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "itempanel" && !list[i].gameObject.name.Equals(base.gameObject.name))
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	public void SetFront()
	{
		base.transform.SetAsLastSibling();
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		SetFront();
	}
}
