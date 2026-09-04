using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
	public List<GameObject> zimu;

	public GameObject bk;

	private GameManager gameManager;

	public bool iscanclick;

	private bool jumped;

	[SerializeField]
	private I18NText txt_lyrics;

	[SerializeField]
	private HoldEsc holdesc;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.homeScene != null)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_1.GetComponent<CameraFilterPack_TV_Distorted>().enabled = true;
		}
		gameManager.musicManager.PlayNormalMusic(9, 6);
		StartCoroutine(ShowZimu());
		if (I18N.instance.gameLang.Equals(LanguageCode.EN))
		{
			StartCoroutine(ShowLyrics2());
		}
		else
		{
			StartCoroutine(ShowLyrics());
		}
	}

	private IEnumerator ShowLyrics2()
	{
		yield return new WaitForSeconds(25f);
		ShowOneLyrics("^musiczimu01");
		yield return new WaitForSeconds(2.5f);
		ShowOneLyrics("^musiczimu02");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^musiczimu03");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu04");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^musiczimu05");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu06");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^musiczimu07");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu08");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu09");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu10");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu11");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu12");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^musiczimu13");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu14");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu15");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu16");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu17");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu18");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu19");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu20");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu21");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu22");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^musiczimu23");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu24");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu25");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^musiczimu26");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu27");
		yield return new WaitForSeconds(13f);
		ShowOneLyrics("^musiczimu28");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu29");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu30");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^musiczimu31");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu32");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu33");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu34");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu35");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu36");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu37");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu38");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu39");
		yield return new WaitForSeconds(2f);
		ShowOneLyrics("^musiczimu40");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^musiczimu41");
		yield return new WaitForSeconds(6f);
		ShowOneLyrics("^musiczimu42");
		yield return new WaitForSeconds(4f);
		txt_lyrics.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(1f);
		Object.Destroy(holdesc.gameObject);
		JumpTo();
	}

	private IEnumerator ShowLyrics()
	{
		yield return new WaitForSeconds(15f);
		ShowOneLyrics("^newep03");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep04");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^newep05");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep06");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^newep07");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep08");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep09");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep10");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep11");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep12");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep13");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep14");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep15");
		yield return new WaitForSeconds(13f);
		ShowOneLyrics("^newep16");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^newep17");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep18");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^newep19");
		yield return new WaitForSeconds(11f);
		ShowOneLyrics("^newep20");
		yield return new WaitForSeconds(3f);
		ShowOneLyrics("^newep21");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep22");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^newep23");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep24");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep25");
		yield return new WaitForSeconds(4f);
		ShowOneLyrics("^newep26");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep27");
		yield return new WaitForSeconds(5f);
		ShowOneLyrics("^newep28");
		yield return new WaitForSeconds(10f);
		ShowOneLyrics("^newep29");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep30");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep31");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep32");
		yield return new WaitForSeconds(29f);
		ShowOneLyrics("^newep33");
		yield return new WaitForSeconds(9f);
		ShowOneLyrics("^newep34");
		yield return new WaitForSeconds(7f);
		ShowOneLyrics("^newep35");
		yield return new WaitForSeconds(8f);
		ShowOneLyrics("^newep36");
		yield return new WaitForSeconds(10f);
		txt_lyrics.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(1f);
		Object.Destroy(holdesc.gameObject);
		JumpTo();
	}

	private void ShowOneLyrics(string key)
	{
		txt_lyrics.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(delegate
		{
			txt_lyrics.updateTranslation2(key);
			txt_lyrics.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		});
	}

	private IEnumerator ShowZimu()
	{
		yield return new WaitForSeconds(2f);
		bk.transform.DOLocalMoveY(-840f, 5 * (zimu.Count - 1) + 1).SetEase(Ease.Linear);
		for (int i = 0; i < zimu.Count; i++)
		{
			yield return new WaitForSeconds(1f);
			zimu[i].transform.Find("label").GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
			yield return new WaitForSeconds(4f);
			if (i != zimu.Count - 1)
			{
				zimu[i].transform.Find("label").GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
				continue;
			}
			holdesc.gameObject.SetActive(value: true);
			iscanclick = true;
		}
	}

	public void JumpTo()
	{
		StartCoroutine(Jump());
	}

	private IEnumerator Jump()
	{
		if (!jumped)
		{
			gameManager.ShowFloatBox();
			jumped = true;
			yield return new WaitForSeconds(2f);
			gameManager.musicManager.Stop();
			gameManager.txt_studio.SetActive(value: true);
			if (gameManager.homeScene != null)
			{
				gameManager.homeScene.cameraFilterPack_Noise_TV_1.GetComponent<CameraFilterPack_TV_Distorted>().enabled = false;
			}
			Object.Instantiate(Resources.Load("Dialog/missionresultDialog") as GameObject, gameManager.homeScene.middle);
			yield return new WaitForSeconds(2f);
			Object.Destroy(base.gameObject);
		}
	}
}
