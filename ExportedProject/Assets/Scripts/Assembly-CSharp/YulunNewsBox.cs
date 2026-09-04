using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunNewsBox : MonoBehaviour
{
	public Image cityIcon;

	public Text cityName;

	public Text newsTitle;

	public Text newsInfoLabel;

	public List<Sprite> cityIconList;

	public YulunNewsInfo newsInfo;

	public void Init(YulunNewsInfo info)
	{
		newsInfo = info;
		int num = 0;
		Debug.LogError("水军:" + info.newsName);
		if (info.newsName.ToLower().Equals("driord"))
		{
			num = 0;
		}
		else if (info.newsName.ToLower().Equals("phax"))
		{
			num = 1;
		}
		else if (info.newsName.ToLower().Equals("dreg"))
		{
			num = 2;
		}
		else if (info.newsName.ToLower().Equals("tawilah"))
		{
			num = 3;
		}
		else if (info.newsName.ToLower().Equals("slutiarm"))
		{
			num = 4;
		}
		else if (info.newsName.ToLower().Equals("aridru"))
		{
			num = 5;
		}
		else if (info.newsName.ToLower().Equals("gauti"))
		{
			num = 6;
		}
		else if (info.newsName.ToLower().Equals("glalos"))
		{
			num = 7;
		}
		else if (info.newsName.ToLower().Equals("uyagh"))
		{
			num = 8;
		}
		Debug.LogError("##水军:" + info.newsName + ":" + num);
		cityIcon.sprite = cityIconList[num];
		cityName.GetComponent<I18NText>().updateTranslation2(info.newsName);
		if (info.title != "")
		{
			newsTitle.GetComponent<I18NText>().updateTranslation2(info.title);
		}
		newsInfoLabel.GetComponent<I18NText>().updateTranslation2(info.info);
	}
}
