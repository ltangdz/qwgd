using System;
using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HB3Top : MonoBehaviour
{
	public Text crtDate;

	public Text crtTime;

	public Text crtWeek;

	public Button setting;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(GetTime());
		StartCoroutine(OpenSound());
		setting.onClick.AddListener(delegate
		{
			gameManager.saveManager.ShowPausePanel();
			gameManager.homeScene.ShowNewVideoCanvas();
		});
	}

	private IEnumerator OpenSound()
	{
		yield return new WaitForSeconds(1f);
		gameManager.soundManager.PlaySound(1);
	}

	private IEnumerator GetTime()
	{
		while (true)
		{
			long num = long.Parse(gameManager.dataManager.dic11[gameManager.player.GetEventId()].date);
			gameManager.player.playerdata.endTime += 60000L;
			long nowTime = gameManager.player.playerdata.endTime + num;
			DateTime dateTime = gameManager.StampToTime(nowTime, isSecounds: false);
			_ = DateTime.Now.Hour;
			_ = DateTime.Now.Minute;
			crtDate.GetComponent<I18NText>().updateTranslation2(dateTime.ToString("d"));
			crtTime.GetComponent<I18NText>().updateTranslation2(dateTime.ToString("t"));
			crtWeek.GetComponent<I18NText>().updateTranslation2(GetWeek(dateTime.DayOfWeek.ToString()));
			yield return new WaitForSeconds(60f);
		}
	}

	private string GetWeek(string weekName)
	{
		return I18N.instance.getValue("^week_" + weekName);
	}
}
