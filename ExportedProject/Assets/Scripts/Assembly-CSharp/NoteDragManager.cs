using System;
using UnityEngine.EventSystems;
using tnt_deploy;

public class NoteDragManager
{
	private static NoteDragManager _instance;

	public static NoteDragManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new NoteDragManager();
			}
			return _instance;
		}
	}

	public event Action<PointerEventData, DATA1> onDraging;

	public event Action<PointerEventData, DATA1> onDragEnd;

	public event Action<PointerEventData, DATA1> onDragStart;

	public event Action<string> onChangePlayer;

	public void ChangePlayer(string playerId)
	{
		if (this.onChangePlayer != null)
		{
			this.onChangePlayer(playerId);
		}
	}

	public void Draging(PointerEventData p, DATA1 d)
	{
		if (this.onDraging != null)
		{
			this.onDraging(p, d);
		}
	}

	public void DragStart(PointerEventData p, DATA1 d)
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(p, d);
		}
	}

	public void DragEnd(PointerEventData p, DATA1 d)
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(p, d);
		}
	}
}
