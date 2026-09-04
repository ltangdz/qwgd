using UnityEngine;
using UnityEngine.EventSystems;

public class HoverShowTitle : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public GameObject dialog;

	public void OnPointerEnter(PointerEventData eventData)
	{
		dialog.SetActive(value: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		dialog.SetActive(value: false);
	}
}
