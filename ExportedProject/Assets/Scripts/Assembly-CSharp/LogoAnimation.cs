using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{
	public List<Sprite> logo_cn;

	public List<Sprite> logo_en;

	public List<Sprite> logo_tc;

	private void Start()
	{
		List<Sprite> logoList = null;
		if (I18N.instance.gameLang == LanguageCode.CN)
		{
			logoList = logo_cn;
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			logoList = logo_en;
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			logoList = logo_tc;
		}
		StartCoroutine(LogoAni(logoList));
	}

	private IEnumerator LogoAni(List<Sprite> logoList)
	{
		for (int i = 0; i < logoList.Count; i++)
		{
			GetComponent<Image>().sprite = logoList[i];
			yield return new WaitForSeconds(0.02f);
		}
	}
}
