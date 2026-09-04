using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bowen : MonoBehaviour
{
	public List<Image> circleList;

	private void Start()
	{
		StartCoroutine(ShowLight());
	}

	private IEnumerator ShowLight()
	{
		int a = 0;
		while (true)
		{
			circleList[a].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			circleList[a].color = new Color(1f, 1f, 1f, 1f);
			circleList[a].GetComponent<RectTransform>().DOScale(new Vector3(2f, 2f, 2f), 3f);
			circleList[a].DOFade(0f, 3f);
			a = ((a + 1 < circleList.Count) ? (a + 1) : 0);
			yield return new WaitForSeconds(3 / circleList.Count);
		}
	}
}
