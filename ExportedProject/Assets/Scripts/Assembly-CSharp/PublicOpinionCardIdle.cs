using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionCardIdle : MonoBehaviour
{
	public Image cityIcon;

	public Text cityName;

	public Text title;

	public Text info;

	private PublicOpinionInfo _newsInfo;

	public List<Sprite> cityIconList;

	private List<string> _stateNameList = new List<string> { "driord", "phax", "dreg", "tawilah", "slutiarm", "aridru", "gauti", "glalos", "uyagh" };

	public void Init(PublicOpinionInfo news)
	{
		news.positionType = PositionType.IDLE;
		_newsInfo = news;
		cityName.text = news.city;
		if (news.name != "")
		{
			title.GetComponent<I18NText>().updateTranslation2(news.name);
		}
		info.text = I18N.instance.GetValueNoSpacing(news.newsInfo);
		int num = _stateNameList.IndexOf(_newsInfo.city.ToLower());
		if (num < 0)
		{
			num = 0;
		}
		cityIcon.sprite = cityIconList[num];
	}
}
