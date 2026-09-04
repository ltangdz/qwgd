using System;

public class AlubaUIEvent
{
	private static AlubaUIEvent _instance;

	public static AlubaUIEvent Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new AlubaUIEvent();
			}
			return _instance;
		}
	}

	public event Action<string, int, string> onClickText;

	public void ClickText(string groupName, int index, string content)
	{
		if (this.onClickText != null)
		{
			this.onClickText(groupName, index, content);
		}
	}
}
