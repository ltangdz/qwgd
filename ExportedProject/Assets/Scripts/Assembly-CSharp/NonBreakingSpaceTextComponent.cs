using Honeti;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NonBreakingSpaceTextComponent : MonoBehaviour
{
	public static readonly string no_breaking_space = "\u00a0";

	protected Text mytext;

	public bool isinit;

	public string key;

	public bool iszhanweifu;

	private GameManager gameManager;

	private void Awake()
	{
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			Refresh();
		}
	}

	public void Refresh()
	{
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			if (iszhanweifu)
			{
				gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				GetComponent<Text>().text = string.Format(GetComponent<Text>().text, gameManager.player.playerdata.nickname);
			}
			base.enabled = false;
			return;
		}
		mytext = GetComponent<Text>();
		if (isinit)
		{
			GetComponent<Text>().text = "";
			string text = I18N.instance.getValue(key).Replace(" ", no_breaking_space);
			if (iszhanweifu)
			{
				mytext.text = string.Format(text, gameManager.player.playerdata.nickname);
			}
			else
			{
				mytext.text = text;
			}
		}
		else
		{
			mytext.RegisterDirtyVerticesCallback(SetMyText);
		}
	}

	public void SetMyText()
	{
		if (mytext.text.Contains(" "))
		{
			mytext.text = mytext.text.Replace(" ", no_breaking_space);
		}
	}
}
