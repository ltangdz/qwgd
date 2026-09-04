using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunDragFile : MonoBehaviour
{
	public Image imgIcon;

	public Text cityName;

	public Text title;

	public Text info;

	public Text shuijun;

	public void Init(YulunNewsInfo newsInfo, Sprite sprite)
	{
		imgIcon.sprite = sprite;
		cityName.GetComponent<I18NText>().updateTranslation2(newsInfo.newsName);
		title.GetComponent<I18NText>().updateTranslation2(newsInfo.title);
		info.GetComponent<I18NText>().updateTranslation2(newsInfo.info);
		shuijun.GetComponent<I18NText>().updateTranslation2(newsInfo.shuijunVal);
	}
}
