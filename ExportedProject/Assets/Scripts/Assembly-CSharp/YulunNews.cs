using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using tnt_deploy;

public class YulunNews
{
	public string newsType;

	public Dictionary<string, DATA43> newsList = new Dictionary<string, DATA43>();

	public string nextNewsID = "";

	public string oriNewsID = "";

	public Dictionary<string, string> usedAutoNews = new Dictionary<string, string>();

	public string round = "0";

	public Dictionary<string, DATA43> usedNewsList = new Dictionary<string, DATA43>();

	private GameManager gameManager;

	public void Init(string type)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		newsType = type;
		newsList = gameManager.dataManager.GetYulunNewsInfo("110004", type);
		usedAutoNews.Clear();
		if (nextNewsID == "")
		{
			nextNewsID = GetFirstNews();
		}
	}

	private string GetFirstNews()
	{
		string result = "";
		foreach (KeyValuePair<string, DATA43> news in newsList)
		{
			if (news.Value.newslv == 1f)
			{
				result = news.Key;
				break;
			}
		}
		return result;
	}

	public DATA43 GetData(float percent)
	{
		DATA43 dATA = null;
		float num = ((percent >= 50f) ? 6f : ((percent >= 40f && percent < 50f) ? 5f : ((percent >= 30f && percent < 40f) ? 4f : ((percent >= 20f && percent < 30f) ? 3f : ((percent >= 10f && percent < 20f) ? 2f : ((percent >= 0f && percent < 10f) ? 1f : 0f))))));
		int num2 = ((percent >= 50f) ? 100 : ((percent >= 40f && percent < 50f) ? 100 : ((percent >= 30f && percent < 40f) ? 80 : ((percent >= 20f && percent < 30f) ? 80 : ((percent >= 10f && percent < 20f) ? 50 : ((percent >= 0f && percent < 10f) ? 50 : 0))))));
		if (newsType != "0" && nextNewsID != "" && nextNewsID != "0")
		{
			Debug.LogError(nextNewsID + "GetData:" + newsList[nextNewsID].newslv + ":::" + num);
		}
		else
		{
			Debug.LogError(nextNewsID + "GetDDDDDData:" + newsType);
		}
		if (newsType != "0" && nextNewsID != "" && nextNewsID != "0" && newsList[nextNewsID].newslv <= num)
		{
			Debug.Log("newslv < choicenewstype");
			if (nextNewsID != "0" && Random.Range(0, 100) <= num2)
			{
				dATA = newsList[nextNewsID];
				usedNewsList.Add(nextNewsID, dATA);
			}
		}
		if (newsType == "0")
		{
			bool flag = false;
			while (!flag)
			{
				if (newsList.Count <= 0)
				{
					flag = true;
					Debug.LogError("the newsList.count is zero");
				}
				foreach (KeyValuePair<string, DATA43> news in newsList)
				{
					if ((float)Random.Range(0, 10) < 5f)
					{
						dATA = news.Value;
						usedAutoNews.Add(news.Key, "0");
						usedNewsList.Add(news.Key, news.Value);
						flag = true;
						newsList.Remove(news.Key);
						break;
					}
				}
			}
		}
		return dATA;
	}

	public void Slide(string arrow, string newsID = "")
	{
		if (newsType != "0")
		{
			string text = "";
			if (arrow == "-1")
			{
				round = newsList[nextNewsID].down.Split(';')[1];
				text = newsList[nextNewsID].down.Split(';')[0];
				if (usedNewsList.ContainsKey(text) || text == "0")
				{
					text = newsList[nextNewsID].up.Split(';')[0];
				}
			}
			else
			{
				round = newsList[nextNewsID].up.Split(';')[1];
				text = newsList[nextNewsID].up.Split(';')[0];
				Debug.Log(text + "****" + usedNewsList.ContainsKey(text));
				if (usedNewsList.ContainsKey(text) || text == "0")
				{
					text = newsList[nextNewsID].down.Split(';')[0];
				}
				Debug.Log("最终：" + text);
			}
			oriNewsID = nextNewsID;
			nextNewsID = text;
		}
		else
		{
			usedAutoNews[newsID] = ((arrow == "-1") ? usedNewsList[newsID].down.Split(';')[1] : usedNewsList[newsID].up.Split(';')[1]);
		}
	}

	public void ResumeSlide()
	{
		nextNewsID = oriNewsID;
	}

	public void ResetAutoNews()
	{
		usedAutoNews.Clear();
		if (newsList.Count <= 5)
		{
			KeyValuePair<string, DATA43>[] array = usedNewsList.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				KeyValuePair<string, DATA43> keyValuePair = array[i];
				newsList.Add(keyValuePair.Key, keyValuePair.Value);
			}
			usedNewsList.Clear();
		}
	}
}
