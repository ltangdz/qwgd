using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AD01 : MonoBehaviour
{
	public List<Sprite> cnImg;

	public List<Sprite> enImg;

	public List<Sprite> tcImg;

	private void Start()
	{
		int index = Random.Range(0, cnImg.Count);
		GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
		if ((bool)component && component.GameType == GameTypeEnum.DLC6 && (cnImg[index].name == "broswer_dlc_05_en" || cnImg[index].name == "broswer_dlc_05_tw" || cnImg[index].name == "broswer_dlc_05_cn"))
		{
			index = Random.Range(0, cnImg.Count);
		}
		if ((bool)component && component.GameType == GameTypeEnum.DLC7 && (cnImg[index].name == "broswer_dlc_05_en" || cnImg[index].name == "broswer_dlc_05_tw" || cnImg[index].name == "broswer_dlc_05_cn" || cnImg[index].name == "Browser_link_13Cn" || cnImg[index].name == "Browser_link_13En" || cnImg[index].name == "news_dlc4cn" || cnImg[index].name == "news_dlc4en" || cnImg[index].name == "news_dlc4tw" || cnImg[index].name == "broswer_dlc_01_cn" || cnImg[index].name == "broswer_dlc_01_en" || cnImg[index].name == "broswer_dlc_01_tw" || cnImg[index].name == "news_dlc1cn" || cnImg[index].name == "news_dlc1en" || cnImg[index].name == "news_dlc1tw" || cnImg[index].name == "News_link_12CN" || cnImg[index].name == "News_link_12EN" || cnImg[index].name == "News_link_12TW" || cnImg[index].name == "News_link_13CN" || cnImg[index].name == "News_link_13EN"))
		{
			index = 0;
		}
		if (I18N.instance.gameLang == LanguageCode.CN)
		{
			GetComponent<Image>().sprite = cnImg[index];
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			GetComponent<Image>().sprite = tcImg[index];
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			GetComponent<Image>().sprite = enImg[index];
		}
	}
}
