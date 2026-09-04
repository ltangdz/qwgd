using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AlubaUIBase : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	private delegate void Click();

	[SerializeField]
	private int _curIndex;

	[SerializeField]
	private string _groupName;

	public int CurIndex
	{
		get
		{
			return _curIndex;
		}
		set
		{
			_curIndex = value;
		}
	}

	public string GroupName
	{
		get
		{
			return _groupName;
		}
		set
		{
			_groupName = value;
		}
	}

	protected abstract void PointerEnter(PointerEventData eventData);

	protected abstract void PointerExit(PointerEventData eventData);

	protected abstract void TouchDown(PointerEventData eventData);

	protected abstract void TouchUp(PointerEventData eventData);

	protected abstract void OnClick(PointerEventData eventData);

	public virtual void InitData(int curIndex, string groupName)
	{
		_curIndex = curIndex;
		_groupName = groupName;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PointerEnter(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PointerExit(eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		TouchDown(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		TouchUp(eventData);
		OnClick(eventData);
	}
}
