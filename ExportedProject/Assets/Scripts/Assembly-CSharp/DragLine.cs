using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class DragLine : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	public Image img_dot;

	public string tolinktag;

	private List<Vector2> vecs = new List<Vector2>();

	public UILineRenderer uILineRenderer;

	public Sprite[] sprites;

	public int number;

	public bool isselect;

	public Reasoning4003Step02 reasoning4003Step02;

	public string avatarname;

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (isselect && uILineRenderer != null)
		{
			vecs.Clear();
			uILineRenderer.Points = vecs.ToArray();
			uILineRenderer.GetComponent<ReasoningLineResult>().ClearLine();
			if (reasoning4003Step02.reasoningLineResults.Count > 0)
			{
				vecs.Add(img_dot.GetComponent<RectTransform>().localPosition);
				uILineRenderer = reasoning4003Step02.reasoningLineResults[0].GetComponent<UILineRenderer>();
				uILineRenderer.Points = vecs.ToArray();
				uILineRenderer.GetComponent<ReasoningLineResult>().ClearLine();
				ShowDot(isshow: true);
				uILineRenderer.GetComponent<ReasoningLineResult>().start = number;
				uILineRenderer.GetComponent<ReasoningLineResult>().startavatarname = avatarname;
			}
		}
		else if (reasoning4003Step02.reasoningLineResults.Count > 0)
		{
			vecs.Clear();
			vecs.Add(img_dot.GetComponent<RectTransform>().localPosition);
			uILineRenderer = reasoning4003Step02.reasoningLineResults[0].GetComponent<UILineRenderer>();
			uILineRenderer.Points = vecs.ToArray();
			uILineRenderer.GetComponent<ReasoningLineResult>().ClearLine();
			ShowDot(isshow: true);
			uILineRenderer.GetComponent<ReasoningLineResult>().start = number;
			uILineRenderer.GetComponent<ReasoningLineResult>().startavatarname = avatarname;
		}
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (gameObject.GetComponent<DragLine>().isselect)
			{
				vecs.Clear();
				uILineRenderer.Points = vecs.ToArray();
				uILineRenderer.GetComponent<ReasoningLineResult>().ClearLine();
				return;
			}
			gameObject.GetComponent<DragLine>().ShowDot(isshow: true);
			vecs[1] = gameObject.GetComponent<DragLine>().img_dot.GetComponent<RectTransform>().localPosition;
			uILineRenderer.Points = vecs.ToArray();
			uILineRenderer.GetComponent<ReasoningLineResult>().end = gameObject.GetComponent<DragLine>().number;
			uILineRenderer.GetComponent<ReasoningLineResult>().endavatarname = gameObject.GetComponent<DragLine>().avatarname;
			gameObject.GetComponent<DragLine>().uILineRenderer = uILineRenderer;
			if (!reasoning4003Step02.drawreasoningLineResults.Contains(uILineRenderer.gameObject))
			{
				reasoning4003Step02.drawreasoningLineResults.Add(uILineRenderer.gameObject);
			}
			if (reasoning4003Step02.reasoningLineResults.Contains(uILineRenderer.gameObject))
			{
				reasoning4003Step02.reasoningLineResults.Remove(uILineRenderer.gameObject);
			}
			reasoning4003Step02.IsAllRight();
		}
		else
		{
			vecs.Clear();
			uILineRenderer.Points = vecs.ToArray();
			uILineRenderer.GetComponent<ReasoningLineResult>().ClearLine();
			uILineRenderer = null;
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == tolinktag)
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
		if (vecs.Count == 1)
		{
			vecs.Add(localPoint);
		}
		else if (vecs.Count > 1)
		{
			vecs[1] = localPoint;
		}
		uILineRenderer.Points = vecs.ToArray();
	}

	public void ShowDot(bool isshow)
	{
		img_dot.gameObject.SetActive(isshow);
		if (!isshow)
		{
			img_dot.fillAmount = 0f;
		}
		else
		{
			img_dot.DOFillAmount(1f, 0.5f);
		}
		isselect = isshow;
		if (!isshow)
		{
			ShowNormal();
		}
	}

	public void ShowWrong()
	{
		img_dot.sprite = sprites[1];
		uILineRenderer = null;
	}

	public void ShowNormal()
	{
		img_dot.sprite = sprites[0];
	}
}
