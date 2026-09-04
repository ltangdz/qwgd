using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class TBHyperLinkText : MonoBehaviour
{
	public Image img_line;

	public Text txt_content;

	public Image img_click;

	public string link;

	public GameManager gameManager;

	public string oldstr = "";

	public string oristr = "";

	public string data2id;

	public List<string> linestrs = new List<string>();

	public string allText;

	public float perlineheight = 22f;

	public float perlinespaceheight = 19.5f;

	public float offx1;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void CheckStr(string message)
	{
		oldstr = txt_content.text;
		linestrs.Clear();
		int num = 0;
		Font font = txt_content.font;
		font.RequestCharactersInTexture(message, txt_content.fontSize, txt_content.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			font.GetCharacterInfo(array[i], out info, txt_content.fontSize);
			num += info.advance;
			if ((float)num > txt_content.GetComponent<RectTransform>().sizeDelta.x)
			{
				linestrs.Add(text);
				text = array[i].ToString();
				num = 0;
				continue;
			}
			if ((float)num == txt_content.GetComponent<RectTransform>().sizeDelta.x)
			{
				text += array[i];
				linestrs.Add(text);
				text = "";
				num = 0;
				continue;
			}
			text += array[i];
			if (i == array.Length - 1)
			{
				linestrs.Add(text);
				text = "";
				num = 0;
			}
		}
		string text2 = "";
		for (int j = 0; j < linestrs.Count; j++)
		{
			text2 = text2 + linestrs[j] + ((j == linestrs.Count - 1) ? "" : "\n");
		}
		txt_content.text = text2;
		if (txt_content.GetComponent<NonBreakingSpaceTextComponent>() != null)
		{
			txt_content.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
		}
		oldstr = text2;
	}

	public void ShowLink()
	{
		DATA2 data = gameManager.dataManager.dic2[data2id];
		gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
	}

	public void EnterLink()
	{
		ColorUtility.TryParseHtmlString("#8FD8FF", out var color);
		img_line.color = color;
		txt_content.text = I18N.instance.getValue(allText);
	}

	public void ExitLink()
	{
		ColorUtility.TryParseHtmlString("#3FB9F9", out var color);
		img_line.color = color;
		txt_content.text = I18N.instance.getValue(allText);
	}

	public void Init(string allText, string data2id)
	{
		this.allText = allText;
		this.data2id = data2id;
		CheckStr(I18N.instance.getValue(allText));
		RectTransform component = img_line.GetComponent<RectTransform>();
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, CalculateLengthOfText(I18N.instance.getValue(allText)));
		GetLineNumber();
		GetLineNumber(allText);
		_ = perlineheight;
		_ = perlinespaceheight;
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 2f);
		RectTransform component2 = img_click.GetComponent<RectTransform>();
		component2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, CalculateLengthOfText(I18N.instance.getValue(allText)));
		component2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, perlineheight);
		txt_content.text = I18N.instance.getValue(allText);
	}

	private float GetLineNumber()
	{
		return linestrs.Count;
	}

	private float GetLineNumber(string strFragment)
	{
		for (int i = 0; i < linestrs.Count; i++)
		{
			if (linestrs[i].Contains(strFragment))
			{
				return i + 1;
			}
		}
		return -1f;
	}

	private float GetLinePreWidth(string strFragment)
	{
		float result = 0f;
		int num = (int)GetLineNumber(strFragment) - 1;
		if (num < linestrs.Count && num >= 0)
		{
			int num2 = linestrs[num].IndexOf(strFragment);
			if (num2 > -1)
			{
				result = CalculateLengthOfText(linestrs[num].Substring(0, num2));
			}
		}
		return result;
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txt_content.font;
		font.RequestCharactersInTexture(message, txt_content.fontSize, txt_content.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt_content.fontSize);
			num += (float)info.advance;
		}
		return num;
	}
}
