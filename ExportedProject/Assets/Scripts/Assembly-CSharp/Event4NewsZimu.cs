using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Event4NewsZimu : MonoBehaviour
{
	public Text crtTxt;

	public Text nextTxt;

	public List<string> zimu;

	public void Init(int i, float time)
	{
		StartCoroutine(ShowZimu(i, time));
	}

	private IEnumerator ShowZimu(int i, float time)
	{
		crtTxt.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
		crtTxt.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		crtTxt.GetComponent<CanvasGroup>().alpha = 1f;
		nextTxt.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		nextTxt.GetComponent<RectTransform>().localPosition = new Vector3(0f, -57.8f, 0f);
		nextTxt.GetComponent<CanvasGroup>().alpha = 0.2f;
		crtTxt.GetComponent<I18NText>().updateTranslation2(zimu[2 * i]);
		nextTxt.GetComponent<I18NText>().updateTranslation2(zimu[2 * i + 1]);
		yield return new WaitForSeconds((time - 0.3f) / 2f);
		crtTxt.GetComponent<RectTransform>().DOLocalMoveY(57.8f, 0.3f);
		crtTxt.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		crtTxt.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f);
		nextTxt.GetComponent<RectTransform>().DOLocalMoveY(0f, 0.3f);
		nextTxt.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		nextTxt.GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
		yield return new WaitForSeconds((time - 0.3f) / 2f);
		nextTxt.GetComponent<RectTransform>().DOLocalMoveY(57.8f, 0.3f);
		nextTxt.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		nextTxt.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f);
	}
}
