using System;
using System.Collections.Generic;

public class QuestionOptionEvent
{
	private static QuestionOptionEvent _instance;

	public static QuestionOptionEvent Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new QuestionOptionEvent();
			}
			return _instance;
		}
	}

	public event Action<string, string> onClick;

	public event Action<string, List<string>> onSelectedList;

	public event Action<string> onValid;

	public event Action<string, List<string>, List<string>, bool> onNoticeValid;

	public void Click(string groupName, string curName)
	{
		if (this.onClick != null)
		{
			this.onClick(groupName, curName);
		}
	}

	public void Selected(string groupName, List<string> selectedList)
	{
		if (this.onSelectedList != null)
		{
			this.onSelectedList(groupName, selectedList);
		}
	}

	public void Valid(string groupName)
	{
		if (this.onValid != null)
		{
			this.onValid(groupName);
		}
	}

	public void NoticeValidResult(string groupName, List<string> successList, List<string> failList, bool success)
	{
		if (this.onNoticeValid != null)
		{
			this.onNoticeValid(groupName, successList, failList, success);
		}
	}
}
