using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReasonPic : MonoBehaviour
{
	public Button btn_close;

	public Transform content;

	public bool isdestory = true;

	public float opentime = 0.3f;

	public bool isreport;

	public int reportid;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_close.onClick.AddListener(delegate
		{
			if (isdestory && !isreport)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				Hide();
			}
			if (isreport)
			{
				gameManager.player.playerdata.OpenReport(reportid);
				gameManager.saveManager.SavePlayerData();
				if (gameManager.player.playerdata.Isallreportopen())
				{
					gameManager.UnlockAchievements("whoami");
					StartCoroutine(ShowVideo());
				}
				else
				{
					Object.Destroy(base.gameObject);
				}
			}
		});
	}

	private IEnumerator ShowVideo()
	{
		gameManager.iscanhoutaiclose = false;
		if (!gameManager.player.playerdata.videotiplist.Contains("3700062"))
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			gameManager.soundManager.PlaySound(38);
			yield return new WaitForSeconds(2.6f);
			gameManager.homeScene.ShowVideoTip("3700062");
			yield return new WaitForSeconds(1f);
			gameManager.homeScene.eventsystem.SetActive(value: true);
		}
		else
		{
			Debug.Log("已有3700062");
		}
		gameManager.iscanhoutaiclose = true;
		Object.Destroy(base.gameObject);
	}

	public void Show()
	{
		content.GetComponent<CanvasGroup>().DOFade(1f, opentime);
		content.DOScale(Vector3.one, 0.3f);
	}

	public void Hide()
	{
		content.GetComponent<CanvasGroup>().DOFade(0f, opentime);
		content.DOScale(Vector3.zero, 0.3f);
	}
}
