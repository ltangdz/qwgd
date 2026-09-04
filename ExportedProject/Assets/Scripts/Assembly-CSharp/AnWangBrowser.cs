using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AnWangBrowser : MonoBehaviour
{
	public GameObject listBox;

	public GameObject startUP;

	public Image title;

	public Image titleImage;

	public Text loading;

	public Text loadingText;

	private void Start()
	{
		StartCoroutine(Loading());
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
}
