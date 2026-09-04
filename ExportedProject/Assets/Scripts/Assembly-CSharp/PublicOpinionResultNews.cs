using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionResultNews : MonoBehaviour
{
	public List<Transform> newsList;

	public List<Sprite> arrowList;

	public List<string> labelList;

	public void Init(List<PublicOpinionInfo> newsRst)
	{
		base.gameObject.SetActive(value: true);
		StartCoroutine(PlayNews(newsRst));
	}

	private IEnumerator PlayNews(List<PublicOpinionInfo> newsRst)
	{
		for (int i = 0; i < newsRst.Count; i++)
		{
			PublicOpinionInfo publicOpinionInfo = newsRst[i];
			int newsIndex = Random.Range(0, newsList.Count);
			newsList[newsIndex].Find("Mask2d/info").GetComponent<I18NText>().updateTranslation2(publicOpinionInfo.newsInfo);
			if (publicOpinionInfo.up == 0)
			{
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(publicOpinionInfo.downFeedback);
			}
			else
			{
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(publicOpinionInfo.upFeedback);
			}
			newsList[newsIndex].Find("img_type").GetComponent<Image>().sprite = arrowList[(!publicOpinionInfo.IsCorrect()) ? 1 : 0];
			newsList[newsIndex].Find("txt_type").GetComponent<I18NText>().updateTranslation2(labelList[(!publicOpinionInfo.IsCorrect()) ? 1 : 0]);
			newsList[newsIndex].DOScale(new Vector3(1f, 1f, 1f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			yield return new WaitForSeconds(1.8f);
			newsList[newsIndex].DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
		foreach (PublicOpinionInfo item in newsRst)
		{
			_ = item;
		}
		base.gameObject.SetActive(value: false);
	}
}
