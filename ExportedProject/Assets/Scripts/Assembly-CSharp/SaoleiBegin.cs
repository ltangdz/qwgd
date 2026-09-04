using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SaoleiBegin : MonoBehaviour
{
	public GameManager gameManager;

	public CanvasGroup img_red0;

	public CanvasGroup img_greed0text;

	public Image img_icon;

	public Image img_slider0;

	public Image img_ok;

	public CanvasGroup img_loading;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(StartAni());
	}

	private IEnumerator StartAni()
	{
		yield return new WaitForSeconds(2f);
		img_loading.transform.DOScale(1f, 0.5f);
		img_loading.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		img_slider0.DOFillAmount(1f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		img_ok.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.2f);
		img_red0.transform.DOScale(1f, 0.5f);
		img_red0.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(1f);
		img_greed0text.transform.DOScale(1f, 0.5f);
		img_greed0text.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(2.5f);
		gameManager.homeScene.ShowVideoTip("3700057");
		Object.Destroy(base.gameObject);
	}
}
