using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffectBKText : MonoBehaviour
{
	public Text txt;

	public string key;

	public string keyword;

	public string keyword2;

	public HorizontalLayoutGroup horizontalLayoutGroup;

	public Image img_red;

	public Image img_red2;

	public List<string> linestrs = new List<string>();

	public string strFragment;

	public string strFragment2;

	public float perlineheight = 22f;

	public float perlinespaceheight = 19.5f;

	public bool isred;

	public bool isred2;

	public string oldstr = "";

	public string oristr = "";

	public float offx;

	public float sizeoffx;

	private void Start()
	{
		if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
		{
			offx = 0f;
			sizeoffx = 0f;
		}
		if (isred)
		{
			Init();
			strFragment = I18N.instance.getValue(keyword);
			if (isred2)
			{
				strFragment2 = I18N.instance.getValue(keyword2);
			}
			Init2();
		}
	}

	public float Init()
	{
		float num = (float)I18N.instance.getValue(key).Length * 0.02f;
		txt.DOText(I18N.instance.getValue(key), num).SetEase(Ease.Linear).OnComplete(delegate
		{
			if (isred)
			{
				img_red.gameObject.SetActive(value: true);
				if (isred2)
				{
					img_red2.gameObject.SetActive(value: true);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(horizontalLayoutGroup.GetComponent<RectTransform>());
			}
		});
		horizontalLayoutGroup.padding.left = 10;
		horizontalLayoutGroup.padding.right = 10;
		return num;
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txt.font;
		font.RequestCharactersInTexture(message, txt.fontSize, txt.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	private void Init2()
	{
		CheckStr(I18N.instance.getValue(key));
		float inset = (GetLineNumber() - GetLineNumber(strFragment)) * (perlineheight + perlinespaceheight);
		RectTransform component = img_red.GetComponent<RectTransform>();
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, GetLinePreWidth(strFragment) + 10f + offx, CalculateLengthOfText(strFragment) + sizeoffx);
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, inset, perlineheight);
		if (isred2)
		{
			RectTransform component2 = img_red2.GetComponent<RectTransform>();
			component2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, GetLinePreWidth(strFragment2) + 10f + offx, CalculateLengthOfText(strFragment2) + sizeoffx);
			component2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, inset, perlineheight);
		}
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

	private void CheckStr(string message)
	{
		oldstr = txt.text;
		linestrs.Clear();
		int num = 0;
		Font font = txt.font;
		font.RequestCharactersInTexture(message, txt.fontSize, txt.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			font.GetCharacterInfo(array[i], out info, txt.fontSize);
			num += info.advance;
			if ((float)num > 1200f)
			{
				linestrs.Add(text);
				text = array[i].ToString();
				num = 0;
				continue;
			}
			if ((float)num == 1200f)
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
		txt.text = text2;
		if (txt.GetComponent<NonBreakingSpaceTextComponent>() != null)
		{
			txt.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
		}
		oldstr = text2;
	}
}
