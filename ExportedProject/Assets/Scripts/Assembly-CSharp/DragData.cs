using UnityEngine.EventSystems;

public class DragData<T>
{
	private PointerEventData _pointerData;

	private T _data;

	public PointerEventData PointerData
	{
		get
		{
			return _pointerData;
		}
		set
		{
			_pointerData = value;
		}
	}

	public T Data
	{
		get
		{
			return _data;
		}
		set
		{
			_data = value;
		}
	}

	public DragData(PointerEventData pointerData, T data)
	{
		_pointerData = pointerData;
		_data = data;
	}
}
