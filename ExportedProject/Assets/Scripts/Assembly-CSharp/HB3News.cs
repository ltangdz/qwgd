using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HB3News : MonoBehaviour
{
	public List<GameObject> newsBox;

	private DataManager dataManager;

	private int newsIndex;

	private string[][] newsModel;

	private void Start()
	{
		dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
	}

	public void SetNews(int newsModelNum)
	{
		newsIndex = newsModelNum;
		newsBox[newsIndex - 1].SetActive(value: true);
		newsModel = new string[3][];
		newsModel[0] = new string[1] { "1300001" };
		newsModel[1] = new string[2] { "1300002", "1300003" };
		newsModel[2] = new string[3] { "1300004", "1300005", "1300006" };
		for (int i = 0; i < newsModel[newsIndex - 1].Length; i++)
		{
			for (int j = 0; j < newsModel[i].Length; j++)
			{
				Transform transform = base.gameObject.transform.Find(newsBox[newsIndex - 1].name.ToString() + "/new_info" + (j + 1));
				if (transform.Find("img_news") != null)
				{
					transform.Find("img_news").GetComponent<Image>().sprite = Resources.Load("News/news", typeof(Sprite)) as Sprite;
				}
				if (transform.Find("txt_news") != null)
				{
					transform.Find("txt_news").GetComponent<I18NText>().updateTranslation2(dataManager.dic13[newsModel[i][j]].arrowid.Trim());
				}
				if (transform.Find("txt_newsTitle") != null)
				{
					transform.Find("txt_newsTitle").GetComponent<I18NText>().updateTranslation2(dataManager.dic13[newsModel[i][j]].title.Trim());
				}
				if (transform.Find("img_newsSubTitle") != null)
				{
					transform.Find("img_newsSubTitle/txt_newsSubTitle").GetComponent<I18NText>().updateTranslation2(dataManager.dic13[newsModel[i][j]].arrowid.Trim());
				}
			}
		}
	}

	private void Update()
	{
	}
}
