using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunFFile : MonoBehaviour
{
	public Image cityIcon;

	public Text cityName;

	public Text title;

	public Text info;

	public Text shuijun;

	public YulunNewsInfo newsInfo;

	public List<Sprite> cityIconList;

	public List<Sprite> whiteIconList;

	public int iconIndex;

	public void Init(YulunNewsInfo news)
	{
		newsInfo = news;
		iconIndex = int.Parse(news.newsName.ToLower().Replace("driord", "0").Replace("phax", "1")
			.Replace("dreg", "2")
			.Replace("tawilah", "3")
			.Replace("slutiarm", "4")
			.Replace("aridru", "5")
			.Replace("gauti", "6")
			.Replace("glalos", "7")
			.Replace("uyagh", "8"));
		cityIcon.sprite = cityIconList[iconIndex];
		cityName.GetComponent<I18NText>().updateTranslation2(news.newsName);
		title.GetComponent<I18NText>().updateTranslation2(news.title);
		info.GetComponent<I18NText>().updateTranslation2(news.info);
		shuijun.GetComponent<I18NText>().updateTranslation2(news.shuijunVal);
	}
}
