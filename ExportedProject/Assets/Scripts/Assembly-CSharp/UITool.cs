using DG.Tweening;
using UnityEngine.UI;

public class UITool
{
	public Sequence LoadingText(Text text, string content, int dotCount)
	{
		Sequence sequence = DOTween.Sequence();
		for (int i = 0; i < dotCount; i++)
		{
			string text2 = "";
			for (int j = 0; j < i; j++)
			{
				text2 += ".";
			}
			sequence.Append(text.DOText(content + text2, 0f));
			sequence.AppendInterval(0.2f);
		}
		sequence.SetLoops(-1);
		sequence.Play();
		return sequence;
	}
}
