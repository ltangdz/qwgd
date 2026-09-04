using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;
using _DLC8.Game.PublicOpinion.Card;

public class PublicOpinionCardDrag : DragBagItem<PublicOpinionInfo>
{
	public Image cityIcon;

	public Text cityName;

	public Text title;

	public Text info;

	public Text shuijun;

	public PublicOpinionCounter counter;

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
		new Color(18f / 85f, 0.88235295f, 0.6313726f, 1f),
		new Color(0.95686275f, 0.32156864f, 0.30980393f, 1f)
	};

	private int _iconIndex;

	private _DLC8.Game.PublicOpinion.PositionType _positionType;

	private List<string> _stateNameList = new List<string> { "driord", "phax", "dreg", "tawilah", "slutiarm", "aridru", "gauti", "glalos", "uyagh" };

	private RectTransform _rt;

	private void Awake()
	{
		_rt = GetComponent<RectTransform>();
	}

	public void Init(PublicOpinionInfo news)
	{
		_newsInfo = news;
		if (_cityIconList != null)
		{
			_cityIconList.Clear();
		}
		Debug.LogError(news.up);
		cityName.text = news.city;
		if (news.name != "")
		{
			title.GetComponent<I18NText>().updateTranslation2(news.name);
		}
		info.text = I18N.instance.GetValueNoSpacing(news.newsInfo);
		shuijun.GetComponent<I18NText>().updateTranslation2(news.roleNum.ToString());
		base.transform.DOScale(0.9f, 0f);
		ChangeStatus();
	}

	private void ChangeStatus()
	{
		int positionType = (int)_positionType;
		int index = 0;
		switch (_positionType)
		{
		case _DLC8.Game.PublicOpinion.PositionType.UP:
			index = 18;
			break;
		case _DLC8.Game.PublicOpinion.PositionType.DOWN:
			index = 9;
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

	public override void InitUI(PublicOpinionInfo t)
	{
		Init(t);
	}

	public override void DragEnd(DragBagGrid<PublicOpinionInfo> bagGrid, List<Collider2D> touchList)
	{
		PublicOpinionBag publicOpinionBag = (PublicOpinionBag)bagGrid;
		if (touchList.Count == 0)
		{
			publicOpinionBag.Cancel();
			return;
		}
		float num = 0f;
		int index = 0;
		for (int i = 0; i < touchList.Count; i++)
		{
			Vector2 center = touchList[i].GetComponent<RectTransform>().rect.center;
			float num2 = Mathf.Abs(Vector2.Distance(_rt.rect.center, center));
			if (num == 0f || num > num2)
			{
				num = num2;
				index = i;
			}
		}
		PublicOpinionBag component = touchList[index].GetComponent<PublicOpinionBag>();
		if (publicOpinionBag == component)
		{
			publicOpinionBag.Cancel();
			return;
		}
		component.PutIntoBag(publicOpinionBag.DataItem);
		publicOpinionBag.DragOK();
		counter.InitUI();
	}
}
