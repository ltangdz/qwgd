using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiddlePanel : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public NoteDragItem _noteDragItem;

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null && gameObject.tag.Equals("browserdialog"))
		{
			gameObject.transform.SetAsLastSibling();
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[0].gameObject.tag == "browserdialog")
			{
				return list[0].gameObject;
			}
		}
		return null;
	}

	private void Start()
	{
	}
}
