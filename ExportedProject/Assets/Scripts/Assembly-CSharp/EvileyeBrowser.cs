using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class EvileyeBrowser : MonoBehaviour
{
	public GameObject listBox;

	public GameObject startUP;

	public Image title;

	public Image titleImage;

	public Text loading;

	public Text loadingText;

	public InputField inputsearch;

	public Button btnsearch;

	public GameObject wrongWarning;

	public GameObject listbox;

	public GameObject listboxsearch01;

	public List<string> searchResult;

	private void Start()
	{
		StartCoroutine(Loading());
		btnsearch.onClick.AddListener(Submit);
	}

	private IEnumerator Loading()
	{
		startUP.GetComponent<CanvasGroup>().DOFade(1f, 1.2f);
		yield return new WaitForSeconds(0.8f);
		title.GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1f), 1.5f);
		yield return new WaitForSeconds(1.8f);
		titleImage.GetComponent<CanvasGroup>().DOFade(1f, 1.5f);
		yield return new WaitForSeconds(1.8f);
		loading.DOText(I18N.instance.getValue("^anwang_label01"), 0.8f);
		yield return new WaitForSeconds(0.8f);
		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForSeconds(0.1f);
			loadingText.DOText("...", 0.5f);
			yield return new WaitForSeconds(0.5f);
			loadingText.text = "";
		}
		listBox.SetActive(value: true);
		startUP.GetComponent<RectTransform>().DOLocalMoveY(1000f, 1f);
		yield return new WaitForSeconds(1f);
		Object.Destroy(startUP);
	}

	private void OnDisable()
	{
		Object.Destroy(startUP);
		listBox.SetActive(value: true);
	}

	private void Submit()
	{
		string text = inputsearch.text.ToLower().Replace(" ", "");
		Debug.Log(text + " " + I18N.instance.getValue(searchResult[0]));
		if (text == I18N.instance.getValue(searchResult[0]).ToLower().Replace(" ", ""))
		{
			listbox.SetActive(value: false);
			listboxsearch01.SetActive(value: true);
		}
		else if (text == "")
		{
			listbox.SetActive(value: true);
			listboxsearch01.SetActive(value: false);
		}
		else
		{
			CancelInvoke("HideWarning");
			wrongWarning.SetActive(value: true);
			Invoke("HideWarning", 2f);
		}
	}

	private void HideWarning()
	{
		wrongWarning.SetActive(value: false);
	}
}
