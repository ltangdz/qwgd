using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class fingercodeitem : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
{
	[SerializeField]
	private int id;

	[SerializeField]
	private bool isselect;

	[SerializeField]
	private Image img_green;

	[SerializeField]
	private Image img_white;

	[SerializeField]
	private LineRendererInfo lineRendererInfo;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private string tagname;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isselect)
		{
			lineRendererInfo.vecs.Clear();
			lineRendererInfo.AddDotToLine(GetComponent<RectTransform>().localPosition);
			ShowGreen();
			lineRendererInfo.count++;
			isselect = true;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (lineRendererInfo.count < lineRendererInfo.vecs.Count)
		{
			lineRendererInfo.vecs.RemoveAt(lineRendererInfo.vecs.Count - 1);
			lineRendererInfo.RefreshLine();
		}
		lineRendererInfo.CheckPw();
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (!gameObject.GetComponent<fingercodeitem>().isselect)
			{
				lineRendererInfo.RefreshLine(gameObject.GetComponent<RectTransform>().localPosition);
				gameObject.GetComponent<fingercodeitem>().isselect = true;
				gameObject.GetComponent<fingercodeitem>().ShowGreen();
				lineRendererInfo.count++;
			}
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
			if (lineRendererInfo.count == lineRendererInfo.vecs.Count)
			{
				lineRendererInfo.AddDotToLine(localPoint);
			}
			else
			{
				lineRendererInfo.RefreshLine(localPoint);
			}
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == tagname)
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	public void ShowGreen()
	{
		img_green.color = Color.white;
		img_green.gameObject.SetActive(value: true);
		lineRendererInfo.currentpw += id;
	}

	public void ShowRed()
	{
		img_green.sprite = sprites[1];
		img_green.DOFade(0.3f, 0.5f).SetLoops(3).OnComplete(delegate
		{
			img_green.sprite = sprites[0];
			isselect = false;
			img_green.gameObject.SetActive(value: false);
			lineRendererInfo.ResetLine();
		});
	}

	public void Lock()
	{
		isselect = true;
		GetComponent<Image>().raycastTarget = false;
	}
}
