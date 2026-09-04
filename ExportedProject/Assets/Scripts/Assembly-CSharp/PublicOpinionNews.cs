using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionNews : MonoBehaviour
{
	public PublicOpinionController controller;

	public List<Transform> newsList;

	public List<Sprite> arrowList;

	public List<string> labelList;

	public void Init(Dictionary<string, PublicOpinionInfo> newsRst)
	{
		StartCoroutine(PlayNews(newsRst));
	}

	private IEnumerator PlayNews(Dictionary<string, PublicOpinionInfo> newsRst)
	{
		foreach (KeyValuePair<string, PublicOpinionInfo> item in newsRst)
		{
			int newsIndex = Random.Range(0, newsList.Count);
			newsList[newsIndex].Find("info").GetComponent<I18NText>().updateTranslation2(item.Value.newsInfo);
			if (item.Value.up == 0)
			{
				newsList[newsIndex].Find("img_type").GetComponent<Image>().sprite = arrowList[1];
				newsList[newsIndex].Find("txt_type").GetComponent<I18NText>().updateTranslation2(labelList[1]);
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(item.Value.downFeedback);
			}
			else
			{
				newsList[newsIndex].Find("img_type").GetComponent<Image>().sprite = arrowList[0];
				newsList[newsIndex].Find("txt_type").GetComponent<I18NText>().updateTranslation2(labelList[0]);
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(item.Value.upFeedback);
			}
			newsList[newsIndex].DOScale(new Vector3(1f, 1f, 1f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			yield return new WaitForSeconds(controller.changeTime / 5f - 0.2f);
			newsList[newsIndex].DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
		base.gameObject.SetActive(value: false);
	}
}
