using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class Subway : MonoBehaviour
{
	public GameObject bakAD;

	public GameObject bakADblack;

	public RectTransform shadow;

	public float shadowMoveSpeed;

	private int ADIndex = 1;

	private GameManager gameManager;

	public GameObject txt_zimu;

	public GameObject txt_zimu1;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang != LanguageCode.CN && I18N.instance.gameLang != LanguageCode.TC)
		{
			txt_zimu.transform.Find("Text").gameObject.SetActive(value: false);
			txt_zimu1.transform.Find("Text").gameObject.SetActive(value: false);
		}
		StartCoroutine(ShadowRun());
		StartCoroutine(ShowLabel());
		InvokeRepeating("ShowAD", 0f, 0.06f);
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
	}

	private void Update()
	{
	}

	private void ShowAD()
	{
		bakAD.GetComponent<RectTransform>().anchoredPosition = new Vector2(1970f, 34f);
		bakAD.GetComponent<RectTransform>().DOAnchorPosX(-1970f, 0.06f);
	}

	public void Change(string scene)
	{
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene(scene);
		}
	}

	private void ShadowMove()
	{
		shadow.GetComponent<RectTransform>().sizeDelta = new Vector2(1920f, 1080f);
		shadow.GetComponent<RectTransform>().DOSizeDelta(new Vector2(3840f, 1080f), shadowMoveSpeed).SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				shadow.GetComponent<RectTransform>().sizeDelta = new Vector2(1920f, 1080f);
			});
	}

	private IEnumerator ShadowRun()
	{
		while (true)
		{
			shadow.gameObject.SetActive(value: true);
			InvokeRepeating("ShadowMove", 0f, shadowMoveSpeed);
			yield return new WaitForSeconds(2f);
			CancelInvoke("ShadowMove");
			shadow.gameObject.SetActive(value: false);
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator ShowLabel()
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt_zimu);
		yield return new WaitForSeconds(3f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt_zimu);
		yield return new WaitForSeconds(2f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt_zimu1);
		yield return new WaitForSeconds(3f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt_zimu1);
	}
}
