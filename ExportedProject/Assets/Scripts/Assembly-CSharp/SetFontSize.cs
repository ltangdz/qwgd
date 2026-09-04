using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SetFontSize : MonoBehaviour
{
	public int cnSize;

	public int enSize;

	private void Start()
	{
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			GetComponent<Text>().fontSize = cnSize;
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			GetComponent<Text>().fontSize = enSize;
		}
	}
}
