using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Tbitem1600165 : MonoBehaviour
{
	public int count;

	public Text txt_read;

	private void Start()
	{
		if (count > 100000)
		{
			if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
			{
				txt_read.GetComponent<I18NText>().updateTranslation2(count / 10000 + "W+");
			}
			else
			{
				txt_read.GetComponent<I18NText>().updateTranslation2(count / 1000 + "K+");
			}
		}
		else
		{
			txt_read.GetComponent<I18NText>().updateTranslation2(count.ToString());
		}
	}

	private void Update()
	{
	}
}
