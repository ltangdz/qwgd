using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class YulunNewsDialog : CustomDialog
{
	public List<float> orderVal;

	public List<string> newsList;

	public string hotNewsLabel;

	public YulunDialog yulunDialog;

	public Transform newsListContent;

	private GameObject hotNews;

	private int newsIndex;

	private GameObject alubaNews;

	private int alubaNewsIndex;

	public float percent;

	private void Start()
	{
		InitNews();
	}

	public void InitNews()
	{
		for (int i = -1; i < newsList.Count; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate(Resources.Load<Transform>("Dialog/Yulun/yulun_newshotlist"), newsListContent);
			if (i == -1)
			{
				transform.Find("txt_no").GetComponent<I18NText>().updateTranslation2("<color=#FFFA64>NO." + (i + 2) + "</color>");
				transform.Find("txt_info").GetComponent<I18NText>().updateTranslation2("<color=#FFFA64>" + I18N.instance.getValue(hotNewsLabel) + "</color>");
				hotNews = transform.gameObject;
				newsIndex = 0;
				continue;
			}
			transform.Find("txt_no").GetComponent<I18NText>().updateTranslation2("NO." + (i + 2));
			if (newsList[i].IndexOf(";") > -1)
			{
				transform.Find("txt_info").GetComponent<I18NText>().updateTranslation2(newsList[i].Split(';')[0]);
				alubaNews = transform.gameObject;
				alubaNewsIndex = i + 1;
			}
			else
			{
				transform.Find("txt_info").GetComponent<I18NText>().updateTranslation2(newsList[i]);
			}
		}
	}

	public void ChangeHotNews()
	{
		float num = (percent = (float)yulunDialog.zAllPerson / (float)yulunDialog.allPerson * 100f);
		int newNewsPos = 0;
		for (int i = 0; i < orderVal.Count; i++)
		{
			if (num <= orderVal[i])
			{
				newNewsPos = i;
				break;
			}
			if (i == orderVal.Count - 1)
			{
				newNewsPos = orderVal.Count;
			}
		}
		StartCoroutine(ChangePos(newNewsPos));
	}

	public void ChangeAlubaNews(Dictionary<string, YulunNewsInfo> showNewsData)
	{
		if (alubaNewsIndex > 0)
		{
			foreach (KeyValuePair<string, YulunNewsInfo> showNewsDatum in showNewsData)
			{
				if (showNewsDatum.Value.newsType == "4.0")
				{
					Debug.Log(showNewsDatum.Value.round);
					if (showNewsDatum.Value.round == "1")
					{
						StartCoroutine(ChangeAlubaPos());
					}
				}
			}
			return;
		}
		gameManager.UnlockAchievements("alubastudio");
	}

	private IEnumerator ChangePos(int newNewsPos)
	{
		int changVal = Math.Abs(newNewsPos - newsIndex);
		int a = 1;
		if (newNewsPos < newsIndex)
		{
			a = -1;
		}
		while (newNewsPos != newsIndex)
		{
			yield return new WaitForSeconds((yulunDialog.changeTime - 0.6f) / (float)changVal);
			newsListContent.GetChild(newsIndex + a).GetComponent<RectTransform>().DOScaleY(0f, 0.3f);
			hotNews.GetComponent<RectTransform>().DOScaleY(0f, 0.3f);
			yield return new WaitForSeconds(0.2f);
			newsListContent.GetChild(newsIndex + a).Find("txt_no").GetComponent<I18NText>()
				.updateTranslation2("NO." + (newsIndex + 1));
			hotNews.transform.Find("txt_no").GetComponent<I18NText>().updateTranslation2("<color=#FFFA64>NO." + (newsIndex + a + 1) + "</color>");
			newsListContent.GetChild(newsIndex + a).GetComponent<RectTransform>().DOScaleY(1f, 0.3f);
			hotNews.GetComponent<RectTransform>().DOScaleY(1f, 0.3f);
			hotNews.transform.SetSiblingIndex(newsIndex + a);
			yield return new WaitForSeconds(0.2f);
			newsIndex += a;
		}
		if (newsIndex == 10)
		{
			yulunDialog.gameSuccess = true;
			yulunDialog.gameOver = true;
			yulunDialog.GameResult();
		}
	}

	private IEnumerator ChangeAlubaPos()
	{
		yield return new WaitForSeconds(yulunDialog.changeTime - 0.6f);
		newsListContent.GetChild(alubaNewsIndex - 1).GetComponent<RectTransform>().DOScaleY(0f, 0.3f);
		alubaNews.GetComponent<RectTransform>().DOScaleY(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		newsListContent.GetChild(alubaNewsIndex - 1).Find("txt_no").GetComponent<I18NText>()
			.updateTranslation2("NO." + (alubaNewsIndex + 1));
		alubaNews.transform.Find("txt_no").GetComponent<I18NText>().updateTranslation2("NO." + alubaNewsIndex);
		newsListContent.GetChild(alubaNewsIndex - 1).GetComponent<RectTransform>().DOScaleY(1f, 0.3f);
		alubaNews.GetComponent<RectTransform>().DOScaleY(1f, 0.3f);
		alubaNews.transform.SetSiblingIndex(alubaNewsIndex - 1);
		yield return new WaitForSeconds(0.3f);
		alubaNewsIndex--;
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
