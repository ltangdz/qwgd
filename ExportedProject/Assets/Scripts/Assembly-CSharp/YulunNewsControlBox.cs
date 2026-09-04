using System.Collections.Generic;
using Honeti;
using UnityEngine;
using tnt_deploy;

public class YulunNewsControlBox : MonoBehaviour
{
	public YulunDialog yulunDialog;

	public YulunNewsControl boxMiddle;

	public YulunNewsControl yulunNewsControl;

	public YulunZNews yulunZNews;

	public YulunFNews yulunFNews;

	private GameManager gameManager;

	private YulunNews news4 = new YulunNews();

	private YulunNews news6 = new YulunNews();

	private YulunNews news8 = new YulunNews();

	private YulunNews autoNews = new YulunNews();

	private List<YulunNews> allNews = new List<YulunNews>();

	private bool instant;

	public void Init()
	{
		if (!instant)
		{
			InitNews();
		}
		List<YulunNewsInfo> newsList = GetNewsList();
		yulunNewsControl.boxShuiJun.allShuijun = 10f;
		yulunNewsControl.boxChoiceBorder.bottomText.GetComponent<I18NText>().updateTranslation2("^yulun_bottomlabel02");
		boxMiddle.Init(newsList);
		yulunZNews.ClearFiles();
		yulunFNews.ClearFiles();
	}

	private List<YulunNewsInfo> GetNewsList()
	{
		autoNews.ResetAutoNews();
		yulunDialog.showNewsList.Clear();
		yulunDialog.showNewsData.Clear();
		List<YulunNewsInfo> list = new List<YulunNewsInfo>();
		int num = 0;
		int num2 = 0;
		float num3 = yulunDialog.zAllPerson;
		float num4 = yulunDialog.allPerson;
		while (list.Count < 5 && num2 <= 100)
		{
			YulunNewsInfo yulunNewsInfo = new YulunNewsInfo();
			DATA43 data = allNews[num].GetData(num3 / num4 * 100f);
			if (data != null)
			{
				Debug.Log(allNews[num].newsType + "***" + data.ID + "***" + num2);
			}
			if (data != null && !yulunDialog.showNewsList.ContainsKey(data.ID.ToString()))
			{
				yulunNewsInfo.newsid = data.ID.ToString();
				yulunNewsInfo.newsType = data.type;
				yulunNewsInfo.newsName = data.city;
				yulunNewsInfo.title = data.name;
				yulunNewsInfo.info = data.info;
				yulunNewsInfo.penzi = data.penzitype;
				yulunNewsInfo.penziChufa = data.chufa;
				yulunNewsInfo.shuijunVal = "0";
				yulunNewsInfo.upRst = data.uprst;
				yulunNewsInfo.downRst = data.downrst;
				yulunNewsInfo.slide = "0";
				yulunNewsInfo.round = "-1";
				yulunNewsInfo.danmu = data.danmu;
				list.Add(yulunNewsInfo);
				yulunDialog.showNewsList.Add(data.ID.ToString(), allNews[num]);
				yulunDialog.showNewsData.Add(data.ID.ToString(), yulunNewsInfo);
			}
			num = ((num >= 3) ? 3 : (num + 1));
			num2++;
		}
		return list;
	}

	private void InitNews()
	{
		instant = true;
		news4.Init("4");
		news6.Init("6");
		news8.Init("8");
		autoNews.Init("0");
		allNews.Add(news4);
		allNews.Add(news6);
		allNews.Add(news8);
		allNews.Add(autoNews);
	}

	public void Slide(string arrow, string newsID)
	{
		if (yulunDialog.showNewsList.ContainsKey(newsID))
		{
			if (yulunDialog.showNewsList[newsID].newsType != "0")
			{
				yulunDialog.showNewsList[newsID].Slide(arrow);
			}
			else
			{
				yulunDialog.showNewsList[newsID].Slide(arrow, newsID);
			}
		}
	}

	public void BakSlide(string newsID)
	{
		yulunDialog.showNewsList[newsID].ResumeSlide();
	}
}
