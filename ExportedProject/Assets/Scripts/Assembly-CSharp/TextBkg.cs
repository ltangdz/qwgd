using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TextBkg : BaseMeshEffect
{
	public I18NText i18ntext;

	public MultiplyText multiplyText;

	private Rect rect;

	public string keyword;

	public Transform imgGroup;

	public float perlineheight = 22f;

	public float perlinespaceheight = 19.5f;

	private GameManager gameManager;

	public float leftoffx = -3f;

	public float rightoffx = 4f;

	public bool issetnewwidth;

	public Text text;

	public string oldstr = "";

	public string oristr = "";

	public int itemcount = 2;

	public List<GameObject> MultiplyImageList = new List<GameObject>();

	public float limitlengh;

	public List<string> linestrs = new List<string>();

	public void SetContent(string content)
	{
		i18ntext.updateTranslation2(content);
	}

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!IsActive())
		{
			return;
		}
		float num = 1000000f;
		float num2 = 1000000f;
		float num3 = -1000000f;
		float num4 = -1000000f;
		List<UIVertex> list = new List<UIVertex>();
		vh.GetUIVertexStream(list);
		foreach (UIVertex item in list)
		{
			Vector3 position = item.position;
			if (num > position.x)
			{
				num = position.x;
			}
			if (num2 > position.y)
			{
				num2 = position.y;
			}
			if (num3 < position.x)
			{
				num3 = position.x;
			}
			if (num4 < position.y)
			{
				num4 = position.y;
			}
		}
		rect = new Rect(num, num2, num3 - num, num4 - num2);
		RectTransform component = GetComponent<RectTransform>();
		rect.x += component.pivot.x * component.rect.size.x;
		rect.y += component.pivot.y * component.rect.size.y;
		Vector2 size = base.transform.parent.gameObject.GetComponent<RectTransform>().rect.size;
		rect.x += component.offsetMin.x + component.anchorMin.x * size.x;
		rect.y += component.offsetMin.y + component.anchorMin.y * size.y;
	}

	private float GetLineNumber()
	{
		return linestrs.Count;
	}

	private float GetLineHeight()
	{
		return perlineheight;
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
				result = CalculateLengthOfText3(linestrs[num].Substring(0, num2));
			}
		}
		return result;
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

	public void SetContent(string content, string itemid, string strFragment, bool istypeeffect = false)
	{
		if (!strFragment.Equals(""))
		{
			if (!I18N.instance.gameLang.Equals(LanguageCode.EN))
			{
				leftoffx = 0f;
				rightoffx = 0f;
			}
			if (linestrs.Count == 0)
			{
				oldstr = content;
				oristr = content;
				CheckStr(oldstr, istypeeffect);
			}
			StartSetContent(itemid, strFragment);
		}
	}

	private void StartSetContent(string itemid, string strFragment)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (oristr.Contains(strFragment))
		{
			if (!IsEnjambment(strFragment))
			{
				Debug.Log("不换行" + strFragment);
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("MultiplyImage"), imgGroup);
				RectTransform component = gameObject.GetComponent<RectTransform>();
				gameObject.GetComponent<MultiplyTextRedImage>().SetMultiplyText(multiplyText);
				gameObject.GetComponent<MultiplyTextRedImage>().Init(itemid, strFragment);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, GetLinePreWidth(strFragment) + leftoffx, CalculateLengthOfText(strFragment) + rightoffx);
				float inset = (GetLineNumber() - GetLineNumber(strFragment)) * (GetLineHeight() + perlinespaceheight);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, inset, GetLineHeight());
				MultiplyImageList.Add(gameObject);
				multiplyText.RefreshContent();
			}
			else
			{
				Debug.Log("换行:" + strFragment);
				DivideStr(itemid, strFragment);
			}
			if (gameManager.player.playerdata.itemlist.Contains(itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(itemid))
			{
				multiplyText.ishad = true;
			}
		}
		else if (!strFragment.Equals("*"))
		{
			Debug.Log("没有：" + oristr + "\n$$$" + strFragment);
		}
	}

	private float CalculateLengthOfText(string message)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(Vector2.zero);
		generationSettings.scaleFactor = 1f;
		return text.cachedTextGeneratorForLayout.GetPreferredWidth(message, generationSettings);
	}

	private float CalculateLengthOfText3(string message)
	{
		float num = 0f;
		Font font = text.font;
		font.RequestCharactersInTexture(message, text.fontSize, text.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, text.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	private void CheckStr(string message, bool istypeeffect = false)
	{
		float num = ((limitlengh == 0f) ? base.transform.GetComponent<RectTransform>().sizeDelta.x : limitlengh);
		oldstr = this.text.text;
		linestrs.Clear();
		int num2 = 0;
		Font font = this.text.font;
		font.RequestCharactersInTexture(message, this.text.fontSize, this.text.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		string text = "";
		string text2 = "";
		string text3 = "";
		int num3 = -1;
		for (int i = 0; i < array.Length; i++)
		{
			font.GetCharacterInfo(array[i], out info, this.text.fontSize);
			num2 += info.advance;
			if (I18N.instance.gameLang.Equals(LanguageCode.EN) && !message.Contains("https://www.Twodrive.com/George/65H3js4") && !issetnewwidth)
			{
				if ((float)num2 >= num && !array[i].ToString().Equals(" "))
				{
					linestrs.Add(text2);
					text = "";
					num2 = 0;
					i = num3;
					text2 = "";
					text3 = "";
					continue;
				}
				text += array[i];
				text3 += array[i];
				if (array[i].ToString() == " ")
				{
					num3 = i;
					text2 += text3;
					text3 = "";
				}
				if (i == array.Length - 1)
				{
					linestrs.Add(text);
					text = "";
					num2 = 0;
				}
			}
			else if (I18N.instance.gameLang.Equals(LanguageCode.TC) && message.Contains("Mason_Toney"))
			{
				if (linestrs.Count == 0)
				{
					linestrs.Add(message);
				}
				else
				{
					linestrs[0] = message;
				}
			}
			else if ((float)num2 > num)
			{
				linestrs.Add(text);
				text = array[i].ToString();
				num2 = 0;
			}
			else if ((float)num2 == num)
			{
				text += array[i];
				linestrs.Add(text);
				text = "";
				num2 = 0;
			}
			else
			{
				text += array[i];
				if (i == array.Length - 1)
				{
					linestrs.Add(text);
					text = "";
					num2 = 0;
				}
			}
		}
		string text4 = "";
		for (int j = 0; j < linestrs.Count; j++)
		{
			text4 = text4 + linestrs[j] + ((j == linestrs.Count - 1) ? "" : "\n");
		}
		if (!istypeeffect)
		{
			this.text.text = text4;
		}
		else
		{
			this.text.DOText(text4, 0.5f);
		}
		if (i18ntext.GetComponent<NonBreakingSpaceTextComponent>() != null)
		{
			i18ntext.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
		}
		oldstr = text4;
	}

	private bool IsEnjambment(string strFragment)
	{
		bool result = true;
		if (oristr.Contains(strFragment))
		{
			for (int i = 0; i < linestrs.Count; i++)
			{
				if (linestrs[i].Contains(strFragment))
				{
					return false;
				}
			}
		}
		return result;
	}

	private void DivideStr(string itemid, string strFragment)
	{
		if (!oristr.Contains(strFragment))
		{
			return;
		}
		int num = oristr.IndexOf(strFragment);
		_ = strFragment.Length;
		int num2 = 0;
		for (int i = 0; i < linestrs.Count; i++)
		{
			num2 += linestrs[i].Length;
			if (num < num2)
			{
				int num3 = 0;
				for (int j = 0; j <= i - 1; j++)
				{
					num3 += linestrs[j].Length;
				}
				MultiplyTextRedImage multiplyTextRedImage = AddImage(linestrs[i].Substring(num - num3), i + 1, itemid, 0);
				MultiplyTextRedImage multiplyTextRedImage2 = AddImage(linestrs[i + 1].Substring(0, strFragment.Length - linestrs[i].Substring(num - num3).Length), i + 2, itemid, 1);
				multiplyTextRedImage.SetLinkImage(multiplyTextRedImage2);
				multiplyTextRedImage2.SetLinkImage(multiplyTextRedImage);
				if (gameManager.player.playerdata.itemlist.Contains(itemid))
				{
					multiplyTextRedImage2.Selected();
				}
				return;
			}
		}
		multiplyText.RefreshContent();
	}

	private MultiplyTextRedImage AddImage(string strFragment, int line, string itemid, int pos)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("MultiplyImage"), imgGroup);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		gameObject.GetComponent<MultiplyTextRedImage>().SetMultiplyText(multiplyText);
		gameObject.GetComponent<MultiplyTextRedImage>().Init(itemid, strFragment, pos);
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, GetLinePreWidth(strFragment) + leftoffx, CalculateLengthOfText(strFragment) + rightoffx);
		float inset = (GetLineNumber() - GetLineNumber(strFragment)) * (GetLineHeight() + perlinespaceheight);
		component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, inset, GetLineHeight());
		MultiplyImageList.Add(gameObject);
		multiplyText.RefreshContent();
		return gameObject.GetComponent<MultiplyTextRedImage>();
	}
}
