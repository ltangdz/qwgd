using UnityEngine;

public class ReasoningLineResult : MonoBehaviour
{
	public int start = -1;

	public int end = -1;

	public string startavatarname;

	public string endavatarname;

	public Reasoning4003Step02 reasoning4003Step02;

	private void Start()
	{
	}

	public void ClearLine()
	{
		ClearLine0();
		if (!reasoning4003Step02.reasoningLineResults.Contains(base.gameObject))
		{
			reasoning4003Step02.reasoningLineResults.Add(base.gameObject);
		}
		if (reasoning4003Step02.drawreasoningLineResults.Contains(base.gameObject))
		{
			reasoning4003Step02.drawreasoningLineResults.Remove(base.gameObject);
		}
	}

	public void ClearLine0()
	{
		reasoning4003Step02.ClearDrawLine(start, end);
		start = -1;
		end = -1;
		startavatarname = "";
		endavatarname = "";
	}
}
