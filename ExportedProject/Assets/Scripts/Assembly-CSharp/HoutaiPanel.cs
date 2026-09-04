using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HoutaiPanel : MonoBehaviour
{
	[SerializeField]
	private GameObject logo;

	[SerializeField]
	private GameObject img_start;

	private GameManager gameManager;

	public Text txt_zimu;

	private IEnumerator currentienumerator;

	public bool showPasswordPanel;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.houtaiPanel = this;
	}

	private void Awake()
	{
		img_start.SetActive(value: true);
	}

	public void Showlogo()
	{
		logo.SetActive(value: true);
		StartCoroutine(ShowStart());
	}

	private IEnumerator ShowStart()
	{
		yield return new WaitForSeconds(1.5f);
		img_start.SetActive(value: false);
		GetComponent<Animator>().Play("ani_houtai");
	}

	public void ShowZimu(List<string> zimus, List<int> yuyins, float waittime)
	{
		Stop();
		txt_zimu.DOKill();
		txt_zimu.text = "";
		currentienumerator = ShowZimuAni(zimus, yuyins, waittime);
		StartCoroutine(currentienumerator);
	}

	public void Stop()
	{
		if (currentienumerator != null)
		{
			StopCoroutine(currentienumerator);
		}
	}

	private IEnumerator ShowZimuAni(List<string> zimus, List<int> yuyins, float waittime)
	{
		yield return new WaitForSeconds(waittime);
		gameManager.musicManager.LowerVol();
		gameManager.soundManager.Stop();
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < zimus.Count; i++)
		{
			float num = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), yuyins[i]);
			txt_zimu.DOText(I18N.instance.getValue(zimus[i]), num).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num + 1f);
			txt_zimu.text = "";
		}
		txt_zimu.text = "";
		gameManager.musicManager.ResumeVol();
	}
}
