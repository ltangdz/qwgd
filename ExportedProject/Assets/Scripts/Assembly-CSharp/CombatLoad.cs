using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CombatLoad : MonoBehaviour
{
	public List<GameObject> loadList;

	public I18NText percent;

	private float showIndex;

	private float pctVal;

	public void SetPercent(float per, float time)
	{
		float endValue = Mathf.Round((float)(loadList.Count - 1) * (per / 100f));
		DOTween.To(() => showIndex, delegate(float x)
		{
			showIndex = x;
		}, endValue, time).OnUpdate(delegate
		{
			if (showIndex <= (float)loadList.Count)
			{
				loadList[(int)showIndex].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
			}
		}).SetEase(Ease.Linear);
		DOTween.To(() => pctVal, delegate(float x)
		{
			pctVal = x;
		}, per, time).OnUpdate(delegate
		{
			string text = Mathf.Floor(pctVal).ToString();
			percent.GetComponent<Text>().text = text + "%";
		}).SetEase(Ease.Linear);
	}
}
