using System;
using System.Collections.Generic;

public class InvadeEvent
{
	private static InvadeEvent _instance;

	public static InvadeEvent Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new InvadeEvent();
			}
			return _instance;
		}
	}

	public event Action<int, bool> onNoticeStepFinished;

	public event Action<int> onNoticeItemAnimationFinished;

	public event Action<List<int>> onNoticeItemChange;

	public event Action onNoticeItemCanClick;

	public event Action onNoticeInvadeDecryptSuccess;

	public event Action onNoticePasswordSuccess;

	public void NoticePasswordSuccess()
	{
		if (this.onNoticePasswordSuccess != null)
		{
			this.onNoticePasswordSuccess();
		}
	}

	public void NoticeInvadeDecryptSuccess()
	{
		if (this.onNoticeInvadeDecryptSuccess != null)
		{
			this.onNoticeInvadeDecryptSuccess();
		}
	}

	public void NoticeItemCanClick()
	{
		if (this.onNoticeItemCanClick != null)
		{
			this.onNoticeItemCanClick();
		}
	}

	public void NoticeStepFinished(int step, bool isSuccess)
	{
		if (this.onNoticeStepFinished != null)
		{
			this.onNoticeStepFinished(step, isSuccess);
		}
	}

	public void NoticeItemAnimationFinished(int itemIndex)
	{
		if (this.onNoticeItemAnimationFinished != null)
		{
			this.onNoticeItemAnimationFinished(itemIndex);
		}
	}

	public void NoticeItemChange(List<int> itemIndexList)
	{
		if (this.onNoticeItemChange != null)
		{
			this.onNoticeItemChange(itemIndexList);
		}
	}
}
