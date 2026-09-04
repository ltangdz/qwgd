using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskFailed : MonoBehaviour
{
	public Text title;

	public Button bakmain;

	public Button readgame;

	public Animator confirm;

	private GameManager gameManager;

	private int failedType;

	private void Start()
	{
	}

	public void Init(int type, GameManager gm)
	{
		gm.CanShowSetting(1);
		failedType = type;
		switch (type)
		{
		case 0:
			title.GetComponent<I18NText>().updateTranslation2("^mission_faild03_1");
			readgame.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^mission_faild04_1");
			break;
		case 1:
			title.GetComponent<I18NText>().updateTranslation2("^mission_faild03_2");
			readgame.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^mission_faild04_2");
			break;
		case 2:
			title.GetComponent<I18NText>().updateTranslation2("^mission_faild03_3");
			readgame.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^mission_faild04_3");
			break;
		case 4:
			title.GetComponent<I18NText>().updateTranslation2("^mission_faild06_4");
			readgame.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^mission_faild07_4");
			break;
		}
		GetComponent<CanvasGroup>().DOFade(1f, 1f);
		gameManager = gm;
		bakmain.onClick.AddListener(BakMain);
		readgame.onClick.AddListener(delegate
		{
			StartCoroutine(ReadGame());
		});
		StartCoroutine(ShowThanks());
	}

	private IEnumerator ReadGame()
	{
		gameManager.CanShowSetting(-1);
		Debug.Log(failedType);
		if (failedType == 0)
		{
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			if (gameManager.homeScene.weizhuangDialog != null)
			{
				gameManager.homeScene.weizhuangDialog.Hide();
			}
			yield return new WaitForSeconds(0.5f);
			gameManager.homeScene.computerButtonBox.FrontTool(0);
			Object.Destroy(base.gameObject);
		}
		else if (failedType == 1)
		{
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			if (gameManager.homeScene.phoneDialog != null)
			{
				gameManager.homeScene.phoneDialog.Hide();
			}
			yield return new WaitForSeconds(0.5f);
			gameManager.homeScene.computerButtonBox.FrontTool(15);
			Object.Destroy(base.gameObject);
		}
		else if (failedType == 2)
		{
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			gameManager.homeScene.computerButtonBox.FrontTool(12);
			Object.Destroy(base.gameObject);
		}
		else if (failedType == 3)
		{
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			gameManager.homeScene.computerButtonBox.FrontTool(12);
			Object.Destroy(base.gameObject);
		}
		else if (failedType == 4)
		{
			if (gameManager.homeScene.newbrowserDialog != null)
			{
				gameManager.homeScene.newbrowserDialog.Hide();
			}
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
			{
				gameManager.player.playerdata.compeletehopelist.Remove("10;1");
				gameManager.player.playerdata.compeletehopelist.Remove("10;0");
				gameManager.player.playerdata.livebroadinganswerrecords.Remove(I18N.instance.getValue("^livename40"));
				gameManager.homeScene.zhibojiannotebook.DeleteBossHopePanel();
				gameManager.player.playerdata.livebroadinglefttime = 600;
				gameManager.player.playerdata.livebroadingchatstep = 3;
				gameManager.player.playerdata.livebroadingstep = 3;
				gameManager.player.playerdata.livebroadingfailedcount = 0;
				gameManager.saveManager.SavePlayerData();
			}
			else
			{
				gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
				gameManager.player.playerdata.ResetLiveBroading(1);
			}
			if (gameManager.homeScene.middle.Find("liveBroadcastingDialog") == null)
			{
				Object.Instantiate(Resources.Load<GameObject>("Livebroadcasting/LiveBroadcastingDialog"), gameManager.homeScene.middle).name = "liveBroadcastingDialog";
			}
			gameManager.saveManager.SavePlayerData();
			Object.Destroy(base.gameObject);
		}
	}

	private void BakMain()
	{
		confirm.gameObject.SetActive(value: true);
		confirm.Play("Exit Panel In");
	}

	public void CancelExitGame()
	{
		confirm.Play("Exit Panel Out");
	}

	public void Quit()
	{
		gameManager.player.ClearEvent();
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("home");
	}

	private IEnumerator ShowThanks()
	{
		yield return new WaitForSeconds(2f);
		title.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2.5f);
		bakmain.gameObject.SetActive(value: true);
		readgame.gameObject.SetActive(value: true);
	}
}
