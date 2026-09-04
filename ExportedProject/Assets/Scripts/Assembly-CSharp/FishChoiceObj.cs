using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishChoiceObj : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public FishPhoneThought parObj;

	public GameObject imgSelBk;

	public bool isTrue;

	public GameObject vagueImg;

	private bool choiced;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
			vagueImg.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
			choiced = true;
			parObj.ObjFoces(base.gameObject);
		});
	}

	public void Blur()
	{
		base.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
		choiced = false;
		vagueImg.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!choiced)
		{
			vagueImg.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!choiced)
		{
			vagueImg.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		}
	}
}
