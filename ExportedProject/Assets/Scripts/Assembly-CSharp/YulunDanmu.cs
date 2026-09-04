using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunDanmu : MonoBehaviour
{
	public YulunDialog yulunDialog;

	public List<Transform> danmuList;

	private List<Transform> usedDanmu = new List<Transform>();

	private Dictionary<string, YulunNewsInfo> news;

	public void Init(Dictionary<string, YulunNewsInfo> cNews)
	{
		if (usedDanmu.Count != 0)
		{
			danmuList.Clear();
			for (int i = 0; i < usedDanmu.Count; i++)
			{
				danmuList.Add(usedDanmu[i]);
				danmuList[i].GetComponent<RectTransform>().localPosition = new Vector3(1000f, danmuList[i].GetComponent<RectTransform>().localPosition.y, 0f);
			}
			usedDanmu.Clear();
		}
		news = cNews;
		StartCoroutine(RunDanmu());
	}

	private IEnumerator RunDanmu()
	{
		foreach (KeyValuePair<string, YulunNewsInfo> item in news)
		{
			float seconds = Random.Range(1f, (yulunDialog.changeTime - 8f) / 5f);
			yield return new WaitForSeconds(seconds);
			string[] array = item.Value.danmu.Split(';');
			string danmuInfo = array[Random.Range(0, array.Length)];
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
		int index = Random.Range(0, danmuList.Count);
		danmuList[index].Find("avatar/Image").GetComponent<Image>().sprite = sprite;
		danmuList[index].Find("Text").GetComponent<I18NText>().updateTranslation2(showDanmu);
		danmuList[index].DOLocalMoveX(-2500f, 10f).SetEase(Ease.Linear);
		usedDanmu.Add(danmuList[index]);
		danmuList.Remove(danmuList[index]);
	}
}
