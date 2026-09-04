using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Sprite hover;

	private Sprite crtSp;

	public bool isScale;

	public float scaleValue = 1f;

	private void Start()
	{
		crtSp = base.transform.GetComponent<Image>().sprite;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isScale)
		{
			base.transform.DOScale(scaleValue, 0f);
		}
		base.transform.GetComponent<Image>().sprite = hover;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isScale)
		{
			base.transform.DOScale(1f, 0f);
		}
		Image component = base.transform.GetComponent<Image>();
		if (component != null)
		{
			component.sprite = crtSp;
		}
	}
}
