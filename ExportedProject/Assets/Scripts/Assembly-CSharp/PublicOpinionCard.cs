using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionCard : MonoBehaviour
{
	public Image cityIcon;

	public Text cityName;

	public Text title;

	public Text info;

	public Text shuijun;

	private PublicOpinionInfo _newsInfo;

	private List<Sprite> _cityIconList = new List<Sprite>();

	public Image titleBg;

	public Image personBg;

	public List<Sprite> titleBgSpriteList;

	public List<Sprite> personBgSpriteList;

	public List<Sprite> iconList;

	private Color[] _colors = new Color[3]
	{
		new Color(1f, 1f, 1f, 1f),
		new Color(0.95686275f, 0.32156864f, 0.30980393f, 1f),
		new Color(18f / 85f, 0.88235295f, 0.6313726f, 1f)
	};

	private int _iconIndex;

	private PositionType _positionType;

	private List<string> _stateNameList = new List<string> { "driord", "phax", "dreg", "tawilah", "slutiarm", "aridru", "gauti", "glalos", "uyagh" };

	public void Init(PublicOpinionInfo news, PositionType positionType)
	{
		_positionType = positionType;
		_newsInfo = news;
		_cityIconList.Clear();
		cityName.text = news.city;
		if (news.name != "")
		{
			title.GetComponent<I18NText>().updateTranslation2(news.name);
		}
		info.text = I18N.instance.GetValueNoSpacing(news.newsInfo);
		shuijun.GetComponent<I18NText>().updateTranslation2(news.roleNum.ToString());
		ChangeStatus();
	}

	private void ChangeStatus()
	{
		int positionType = (int)_positionType;
		int index = 0;
		switch (_positionType)
		{
		case PositionType.UP:
			index = 9;
			break;
		case PositionType.DOWN:
			index = 18;
			break;
		}
		_cityIconList.AddRange(iconList.GetRange(index, 9));
		titleBg.sprite = titleBgSpriteList[positionType];
		personBg.sprite = personBgSpriteList[positionType];
		cityName.color = _colors[positionType];
		_iconIndex = _stateNameList.IndexOf(_newsInfo.city.ToLower());
		if (_iconIndex < 0)
		{
			_iconIndex = 0;
		}
		cityIcon.sprite = _cityIconList[_iconIndex];
	}
}
