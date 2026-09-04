using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBadBox : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private Image img_select;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Text txt_name;

	public bool isselect;

	public bool iscanclick = true;

	[SerializeField]
	private Sprite graysprite;

	[SerializeField]
	private Sprite redsprite;

	[SerializeField]
	private Sprite bluesprite;

	[SerializeField]
	private Color[] colors;

	public void SetRed()
	{
		if (isselect)
		{
			img_bk.sprite = redsprite;
			txt_name.color = Color.red;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(img_bk.DOFade(0.2f, 0.2f));
			sequence.Append(img_bk.DOFade(1f, 0.2f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				txt_name.color = colors[0];
				img_bk.sprite = graysprite;
				img_select.gameObject.SetActive(value: false);
				isselect = false;
			});
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (iscanclick)
		{
			txt_name.color = ((!isselect) ? colors[1] : colors[0]);
			img_bk.sprite = ((!isselect) ? bluesprite : graysprite);
			img_select.gameObject.SetActive(!isselect);
			isselect = !isselect;
		}
	}

	public void Cancel()
	{
		txt_name.color = colors[0];
		img_bk.sprite = graysprite;
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
