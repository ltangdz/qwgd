using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FaceItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int id;

	private RectTransform m_rt;

	public Vector3 startpos;

	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	[SerializeField]
	private Sprite bluesprite;

	[SerializeField]
	private Sprite graysprite;

	[SerializeField]
	private Sprite redsprite;

	[SerializeField]
	private Image img_bk;

	public LineItem currentlineitem;

	[SerializeField]
	private Reasoning4005Step01 reasoning4005Step01;

	public bool isred;

	private void SetBlue()
	{
		img_bk.sprite = bluesprite;
	}

	private void SetGray()
	{
		base.transform.DOMove(startpos, 0.5f);
		base.transform.DOScale(Vector3.one, 0.2f);
		img_bk.sprite = graysprite;
		img_bk.color = Color.white;
		currentlineitem = null;
	}

	private void SetGray0()
	{
		img_bk.sprite = graysprite;
		img_bk.color = Color.white;
	}

	public void SetRed()
	{
		isred = true;
		img_bk.sprite = redsprite;
		img_bk.color = Color.white;
		currentlineitem = null;
		reasoning4005Step01.iscandrag = false;
		img_bk.DOFade(0.2f, 0.5f).SetLoops(3).OnComplete(delegate
		{
			SetGray();
			isred = false;
			reasoning4005Step01.iscandrag = true;
		});
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isred && reasoning4005Step01.iscandrag)
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
			base.transform.SetAsLastSibling();
			SetDraggedPosition(eventData);
			reasoning4005Step01.ShowAllLingxing();
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isred && reasoning4005Step01.iscandrag)
		{
			base.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f);
			SetDraggedPosition(eventData);
			GameObject gameObject = IsPointerOverUIObject(eventData);
			if (gameObject != null)
			{
				SetBlue();
				gameObject.GetComponent<LineItem>().SetBlue();
			}
			else
			{
				reasoning4005Step01.ResetAllGray();
				SetGray0();
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (isred || !reasoning4005Step01.iscandrag)
		{
			return;
		}
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject == null)
		{
			if (currentlineitem != null)
			{
				currentlineitem.SetGray();
			}
			SetGray();
		}
		else
		{
			if (currentlineitem != null)
			{
				currentlineitem.SetGray();
			}
			if (gameObject.GetComponent<LineItem>().currentfaceitem != null)
			{
				gameObject.GetComponent<LineItem>().currentfaceitem.SetGray();
			}
			base.transform.DOMove(gameObject.GetComponent<LineItem>().img_lingxing.transform.position, 0.2f);
			SetBlue();
			Debug.LogError("jiaru");
			gameObject.GetComponent<LineItem>().currentfaceitem = this;
			currentlineitem = gameObject.GetComponent<LineItem>();
			gameObject.GetComponent<LineItem>().SetBlue();
		}
		reasoning4005Step01.HideAllLingxing();
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
		if (!isred)
		{
			base.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isred)
		{
			base.transform.DOScale(Vector3.one, 0.2f);
		}
	}

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.position;
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
}
