using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionSubtitles : MonoBehaviour
{
	public PublicOpinionController controller;

	public List<Transform> subtitleList;

	private List<Transform> usedSubtitleList = new List<Transform>();

	private List<PublicOpinionInfo> infos;

	public void Init(List<PublicOpinionInfo> cNews)
	{
		base.gameObject.SetActive(value: true);
		if (usedSubtitleList.Count != 0)
		{
			subtitleList.Clear();
			for (int i = 0; i < usedSubtitleList.Count; i++)
			{
				subtitleList.Add(usedSubtitleList[i]);
				subtitleList[i].GetComponent<RectTransform>().localPosition = new Vector3(1000f, subtitleList[i].GetComponent<RectTransform>().localPosition.y, 0f);
			}
			usedSubtitleList.Clear();
		}
		infos = cNews;
		StartCoroutine(RunDanmu());
	}

	private IEnumerator RunDanmu()
	{
		foreach (PublicOpinionInfo item in infos)
		{
			float seconds = Random.Range(0.4f, 0.9f);
			yield return new WaitForSeconds(seconds);
			string[] barrageList = item.barrageList;
			string danmuInfo = barrageList[Random.Range(0, barrageList.Length)];
			SetDanmuInfo(danmuInfo);
		}
		yield return new WaitForSeconds(8f);
		base.gameObject.SetActive(value: false);
	}

	private void SetDanmuInfo(string showDanmu)
	{
		int num = Random.Range(10, 500);
		num = ((num.ToString().Length >= 2) ? num : int.Parse("0" + num));
		Sprite sprite = Resources.Load<Sprite>("touxiang/" + num);
		int index = Random.Range(0, subtitleList.Count);
		subtitleList[index].Find("avatar/Image").GetComponent<Image>().sprite = sprite;
		subtitleList[index].Find("Text").GetComponent<I18NText>().updateTranslation2(showDanmu);
		subtitleList[index].DOLocalMoveX(-2500f, 10f).SetEase(Ease.Linear);
		usedSubtitleList.Add(subtitleList[index]);
		subtitleList.Remove(subtitleList[index]);
	}
}
