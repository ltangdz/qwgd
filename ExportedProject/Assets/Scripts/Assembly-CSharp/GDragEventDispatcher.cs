using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GDragEventDispatcher : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IScrollHandler
{
	private ScrollRect anotherScrollRect;

	private Image thisRaycast;

	private bool isscroll;

	private void Awake()
	{
	}

	private void Start()
	{
		thisRaycast = base.gameObject.GetComponent<Image>();
	}

	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") == 0f && (bool)thisRaycast)
		{
			thisRaycast.raycastTarget = true;
		}
		if ((Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f) && (bool)thisRaycast)
		{
			thisRaycast.raycastTarget = false;
		}
	}

	private void FindScrollRect(GameObject obj)
	{
		GameObject gameObject = obj.transform.parent.gameObject;
		anotherScrollRect = gameObject.GetComponent<ScrollRect>();
		if (!anotherScrollRect)
		{
			FindScrollRect(gameObject);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if ((bool)anotherScrollRect)
		{
			anotherScrollRect.OnBeginDrag(eventData);
		}
		if ((bool)thisRaycast)
		{
			thisRaycast.raycastTarget = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if ((bool)anotherScrollRect)
		{
			anotherScrollRect.OnDrag(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if ((bool)anotherScrollRect)
		{
			anotherScrollRect.OnEndDrag(eventData);
		}
		if ((bool)thisRaycast)
		{
			thisRaycast.raycastTarget = true;
		}
	}

	public void OnScroll(PointerEventData eventData)
	{
		if ((bool)anotherScrollRect)
		{
			anotherScrollRect.OnBeginDrag(eventData);
		}
		isscroll = true;
		if ((bool)thisRaycast)
		{
			thisRaycast.raycastTarget = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if ((bool)anotherScrollRect)
		{
			anotherScrollRect.OnBeginDrag(eventData);
		}
		if ((bool)thisRaycast)
		{
			thisRaycast.raycastTarget = true;
		}
	}

	public void OnPointerExit2()
	{
		if ((bool)thisRaycast)
		{
			thisRaycast.raycastTarget = true;
		}
	}
}
