using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class ZhadanZimu : MonoBehaviour
{
	public I18NText zimu;

	public List<string> cioLabel;

	private float boxLength = 600f;

	private string showLabel = "";

	private GameManager gameManager;

	private IEnumerator label;

	private float lengthVal;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		label = ShowLabel();
	}

	public void Init(string txt)
	{
		showLabel = txt;
		StopAllCoroutines();
		StartCoroutine(ShowAllLabel());
	}

	private void ShowLabel(string txt)
	{
		if (label != null)
		{
			StopCoroutine(label);
		}
		zimu.GetComponent<CanvasGroup>().alpha = 0f;
		zimu.GetComponent<RectTransform>().DOKill();
		zimu.updateTranslation2(txt);
		Invoke("CountVal", 0.05f);
	}

	private void CountVal()
	{
		lengthVal = zimu.GetComponent<RectTransform>().sizeDelta.x;
		if (boxLength <= lengthVal)
		{
			zimu.GetComponent<RectTransform>().localPosition = new Vector3(boxLength / 2f + lengthVal / 2f, 0f, 0f);
			StartCoroutine(label);
		}
		else
		{
			zimu.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		}
		zimu.GetComponent<CanvasGroup>().alpha = 1f;
	}

	private IEnumerator ShowLabel()
	{
		while (true)
		{
			zimu.GetComponent<RectTransform>().DOLocalMoveX(boxLength / 2f * -1f + lengthVal / 2f * -1f, 12f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(12f);
			zimu.GetComponent<RectTransform>().DOKill();
			zimu.GetComponent<RectTransform>().localPosition = new Vector3(boxLength / 2f + lengthVal / 2f, 0f, 0f);
		}
	}

	private IEnumerator ShowAllLabel()
	{
		while (true)
		{
			ShowLabel(showLabel);
			float num = Random.Range(3f, 11f);
			yield return new WaitForSeconds(num * 3f);
			float lasttime = gameManager.homeScene.zhadanInvoke.lasttime;
			float totalTime = gameManager.homeScene.zhadanInvoke.totalTime;
			float num2 = lasttime / totalTime;
			int num3 = 0;
			if (num2 <= 1f && num2 >= 0.8f)
			{
				num3 = Random.Range(0, 3);
			}
			else if (num2 < 0.8f && num2 >= 0.5f)
			{
				num3 = Random.Range(3, 6);
			}
			else if (num2 < 0.5f && num2 >= 0.3f)
			{
				num3 = Random.Range(6, 9);
			}
			else if (num2 < 0.3f && num2 >= 0.1f)
			{
				num3 = Random.Range(9, 12);
			}
			else if (num2 < 0.1f && num2 >= 0f)
			{
				num3 = Random.Range(12, 15);
			}
			num3 = (int)Mathf.Floor(num3);
			ShowLabel(cioLabel[num3]);
			yield return new WaitForSeconds(3f);
		}
	}

	public void FreshType()
	{
		StopAllCoroutines();
		zimu.updateTranslation2("");
	}
}
