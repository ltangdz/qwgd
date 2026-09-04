using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selectbox : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private Image img_select;

	public bool isselect;

	public bool iscanclick = true;

	[SerializeField]
	private Sprite redsprite;

	[SerializeField]
	private Sprite bluesprite;

	public bool issingle;

	[SerializeField]
	private List<Selectbox> otherboxs = new List<Selectbox>();

	public void SetRed()
	{
		if (isselect)
		{
			img_select.sprite = redsprite;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(img_select.DOFade(0.2f, 0.2f));
			sequence.Append(img_select.DOFade(1f, 0.2f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				img_select.sprite = bluesprite;
				img_select.gameObject.SetActive(value: false);
				isselect = false;
			});
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!iscanclick)
		{
			return;
		}
		if (issingle)
		{
			for (int i = 0; i < otherboxs.Count; i++)
			{
				otherboxs[i].Cancel();
			}
		}
		img_select.gameObject.SetActive(!isselect);
		isselect = !isselect;
	}

	public void Cancel()
	{
		img_select.gameObject.SetActive(value: false);
		isselect = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (iscanclick)
		{
			base.transform.DOKill();
			base.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (iscanclick)
		{
			base.transform.DOKill();
			base.transform.DOScale(Vector3.one, 0.2f);
		}
	}

	public void ResetSelect()
	{
		img_select.gameObject.SetActive(value: false);
	}
}
