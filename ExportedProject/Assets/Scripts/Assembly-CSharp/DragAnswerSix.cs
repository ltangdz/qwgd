using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAnswerSix : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
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

	public RoleSixBlank roleSixBlank;

	public int pos = -1;

	public int currentroleid;

	public Sprite[] sprites;

	public Image img_bk;

	public Text txt_content;

	public Color[] colors;

	public Reasoning4007Step00 reasoning4007Step00;

	private bool iscandrag;

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.localPosition;
	}

	public bool IsRight()
	{
		if (roleSixBlank != null)
		{
			return currentroleid == roleSixBlank.id;
		}
		return false;
	}

	public void ResetPos()
	{
		if (roleSixBlank != null)
		{
			isred = true;
			StartCoroutine(StartRed());
		}
	}

	private IEnumerator StartRed()
	{
		reasoning4007Step00.iscandrag = false;
		img_bk.sprite = sprites[1];
		txt_content.color = colors[1];
		yield return new WaitForSeconds(0.2f);
		Sequence s = DOTween.Sequence();
		s.Append(img_bk.DOFade(0.2f, 0.2f));
		s.Append(img_bk.DOFade(1f, 0.2f));
		s.Append(img_bk.DOFade(0.2f, 0.2f));
		s.Append(img_bk.DOFade(1f, 0.2f));
		yield return new WaitForSeconds(0.8f);
		img_bk.sprite = sprites[0];
		txt_content.color = colors[0];
		isred = false;
		m_rt.localPosition = startpos;
		roleSixBlank = null;
		reasoning4007Step00.iscandrag = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (isred || !iscandrag || !reasoning4007Step00.iscandrag)
		{
			return;
		}
		iscanclick = false;
		SetDraggedPosition(eventData);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (roleSixBlank != null)
			{
				roleSixBlank.count--;
				switch (pos)
				{
				case 0:
					roleSixBlank.blank_answer1 = null;
					break;
				case 1:
					roleSixBlank.blank_answer2 = null;
					break;
				case 2:
					roleSixBlank.blank_answer3 = null;
					break;
				case 3:
					roleSixBlank.blank_answer4 = null;
					break;
				case 4:
					roleSixBlank.blank_answer5 = null;
					break;
				case 5:
					roleSixBlank.blank_answer6 = null;
					break;
				}
			}
			if (!gameObject.GetComponent<RoleSixBlank>().SetBlank(this))
			{
				m_rt.localPosition = startpos;
			}
			return;
		}
		m_rt.localPosition = startpos;
		if (roleSixBlank != null)
		{
			roleSixBlank.count--;
			switch (pos)
			{
			case 0:
				roleSixBlank.blank_answer1 = null;
				break;
			case 1:
				roleSixBlank.blank_answer2 = null;
				break;
			case 2:
				roleSixBlank.blank_answer3 = null;
				break;
			case 3:
				roleSixBlank.blank_answer4 = null;
				break;
			case 4:
				roleSixBlank.blank_answer5 = null;
				break;
			case 5:
				roleSixBlank.blank_answer6 = null;
				break;
			}
		}
		roleSixBlank = null;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isred && iscanclick && !iscandrag && reasoning4007Step00.iscandrag)
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
		if (!isred && iscandrag && reasoning4007Step00.iscandrag)
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
			if (list[i].gameObject.tag == "itempanel" && !list[i].gameObject.transform.parent.name.Equals(base.gameObject.name))
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
