using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SaoleiDialog : CustomDialog
{
	public int timecount;

	public Transform img_red;

	public Text txt_leftcount;

	public Text txt_daojishi;

	public Animator ani_redline;

	public int correctcount = 22;

	public int leftcount = 22;

	public bool iscanclick = true;

	public Transform img_success;

	public bool fiveminutes = true;

	private void Start()
	{
		img_red.DOLocalMoveX(-316f, timecount).OnComplete(delegate
		{
		});
		txt_leftcount.text = I18N.instance.getValue("^saolei03") + " : " + leftcount;
		StartCoroutine(FiveMinntes());
	}

	private IEnumerator FiveMinntes()
	{
		yield return new WaitForSeconds(timecount);
		fiveminutes = false;
		txt_daojishi.text = I18N.instance.getValue("^saolei11");
		iscanclick = false;
		ShowFail();
		Debug.Log("时间到");
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}

	public void ShowFail()
	{
		iscanclick = false;
		ani_redline.enabled = false;
		img_red.DOKill();
		StartCoroutine(ShowGameOver());
		Debug.Log("失败");
	}

	private IEnumerator ShowGameOver()
	{
		yield return new WaitForSeconds(2f);
		Object.Instantiate(Resources.Load("Dialog/Saolei/saoleigameover") as GameObject, base.transform.parent);
		yield return new WaitForSeconds(5f);
		Hide();
	}

	public void MinusCount(bool isright)
	{
		if (isright)
		{
			correctcount--;
		}
		leftcount--;
		txt_leftcount.text = I18N.instance.getValue("^saolei03") + " : " + leftcount;
		if (correctcount == 0)
		{
			iscanclick = false;
			gameManager.player.playerdata.itemlist.Add("10438");
			gameManager.saveManager.SavePlayerData();
			StartCoroutine(ShowOver());
			if (fiveminutes)
			{
				gameManager.UnlockAchievements("clearvirus");
			}
			Debug.Log("成功");
		}
	}

	public void AddCount(bool isright)
	{
		if (isright)
		{
			correctcount++;
		}
		leftcount++;
		txt_leftcount.text = I18N.instance.getValue("^saolei03") + " : " + leftcount;
		if (correctcount == 0)
		{
			iscanclick = false;
			gameManager.player.playerdata.itemlist.Add("10438");
			gameManager.saveManager.SavePlayerData();
			StartCoroutine(ShowOver());
			if (fiveminutes)
			{
				gameManager.UnlockAchievements("clearvirus");
			}
			Debug.Log("成功");
		}
	}

	private IEnumerator ShowOver()
	{
		Object.Instantiate(Resources.Load("Dialog/Saolei/saoleisucess") as GameObject, base.transform.parent);
		yield return new WaitForSeconds(5f);
		gameManager.homeScene.ShowSpecialVideoTip("3700058");
		Hide();
	}
}
