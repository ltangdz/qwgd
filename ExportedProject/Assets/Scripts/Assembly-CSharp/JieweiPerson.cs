using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class JieweiPerson : MonoBehaviour
{
	public string label;

	public Text nameEN;

	public Text nameCN1;

	public Text nameCN2;

	private void Start()
	{
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			string value = I18N.instance.getValue(label);
			string key = value.Substring(0, 1);
			string key2 = value.Substring(1);
			nameCN1.GetComponent<I18NText>().updateTranslation2(key);
			nameCN2.GetComponent<I18NText>().updateTranslation2(key2);
		}
		else
		{
			nameEN.gameObject.SetActive(value: true);
		}
	}
}
