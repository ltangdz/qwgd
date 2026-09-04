using UnityEngine;
using UnityEngine.EventSystems;

public class UGUIEventTriggerListener : EventTrigger
{
	public delegate void VoidDelegate(GameObject go);

	private float clickDownTime;

	private float clickUpTime;

	private float ClickWaitTime = 0.1f;

	public VoidDelegate onClick;

	public VoidDelegate onDown;

	public VoidDelegate onUp;

	public static UGUIEventTriggerListener Get(GameObject go)
	{
		UGUIEventTriggerListener uGUIEventTriggerListener = go.GetComponent<UGUIEventTriggerListener>();
		if (uGUIEventTriggerListener == null)
		{
			uGUIEventTriggerListener = go.AddComponent<UGUIEventTriggerListener>();
		}
		return uGUIEventTriggerListener;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		if (clickDownTime != 0f && clickUpTime != 0f && clickUpTime - clickDownTime < ClickWaitTime && eventData.clickCount == 1 && onClick != null)
		{
			onClick(base.gameObject);
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		clickDownTime = Time.time;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		clickUpTime = Time.time;
	}
}
