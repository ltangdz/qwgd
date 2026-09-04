using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunRstNews : MonoBehaviour
{
	public YulunDialog yulunDialog;

	public List<Transform> newsList;

	public List<Sprite> arrowList;

	public List<string> labelList;

	public void Init(Dictionary<string, YulunNewsInfo> newsRst)
	{
		StartCoroutine(PlayNews(newsRst));
	}

	private IEnumerator PlayNews(Dictionary<string, YulunNewsInfo> newsRst)
	{
		foreach (KeyValuePair<string, YulunNewsInfo> item in newsRst)
		{
			int newsIndex = Random.Range(0, newsList.Count);
			newsList[newsIndex].Find("info").GetComponent<I18NText>().updateTranslation2(item.Value.info);
			if (item.Value.round == "0")
			{
				newsList[newsIndex].Find("img_type").GetComponent<Image>().sprite = arrowList[1];
				newsList[newsIndex].Find("txt_type").GetComponent<I18NText>().updateTranslation2(labelList[1]);
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(item.Value.downRst);
			}
			else
			{
				newsList[newsIndex].Find("img_type").GetComponent<Image>().sprite = arrowList[0];
				newsList[newsIndex].Find("txt_type").GetComponent<I18NText>().updateTranslation2(labelList[0]);
				newsList[newsIndex].Find("result").GetComponent<I18NText>().updateTranslation2(item.Value.upRst);
			}
			newsList[newsIndex].DOScale(new Vector3(1f, 1f, 1f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			yield return new WaitForSeconds(yulunDialog.changeTime / 5f - 0.2f);
			newsList[newsIndex].DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f);
			newsList[newsIndex].GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
		base.gameObject.SetActive(value: false);
	}
}
