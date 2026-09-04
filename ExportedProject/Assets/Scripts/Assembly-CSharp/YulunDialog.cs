using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class YulunDialog : MonoBehaviour
{
	public YulunTimeDialog yulunTimeDialog;

	public YulunPenziDialog yulunPenziDialog;

	public YulunBtnControlDialog yulunBtnCtrlDialog;

	public YulunNewsDialog yulunNewsDialog;

	public YulunDataDialog yulunDataDialog;

	public YulunNewsControlBox yulunNewsControlBox;

	public YulunRstNews yulunRstNews;

	public YulunDanmu yulunDanmu;

	public Button btnBk;

	public Dictionary<string, YulunMap> yulunMapList = new Dictionary<string, YulunMap>();

	public float changeTime = 15f;

	public bool gameOver;

	public bool gameSuccess;

	public bool gameRunning;

	public YulunCourseManager yulunCourseManager;

	public long zAllPerson;

	public long allPerson;

	public Dictionary<string, YulunNews> showNewsList = new Dictionary<string, YulunNews>();

	public Dictionary<string, YulunNewsInfo> showNewsData = new Dictionary<string, YulunNewsInfo>();

	public List<int> addPenziList = new List<int>();

	public Button btnReplay;

	public GameObject replayAlert;

	private GameManager gameManager;

	private bool isHaveRst;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.yulunDialog = this;
		GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(delegate
		{
			StartCoroutine(ShowDialog());
			InitData();
		});
		if (gameManager.player.playerdata.isYulunCourse01 != 1)
		{
			gameManager.homeScene.eventsystem.gameObject.SetActive(value: false);
			Invoke("ShowCourse", 5f);
		}
		btnReplay.onClick.AddListener(delegate
		{
			replayAlert.SetActive(value: true);
			replayAlert.GetComponent<Animator>().Play("Exit Panel In");
		});
	}

	private void ShowCourse()
	{
		gameManager.homeScene.eventsystem.gameObject.SetActive(value: true);
		yulunCourseManager.gameObject.SetActive(value: true);
		yulunCourseManager.ShowCourse(0, gameManager);
	}

	private IEnumerator ShowDialog()
	{
		yulunTimeDialog.Show();
		yield return new WaitForSeconds(0.2f);
		yulunBtnCtrlDialog.Show();
		yield return new WaitForSeconds(0.2f);
		yulunPenziDialog.Show();
		yield return new WaitForSeconds(0.2f);
		yulunNewsDialog.Show();
		yield return new WaitForSeconds(0.2f);
		yulunDataDialog.Show();
	}

	private void InitData()
	{
	}

	public void Restart()
	{
		gameManager.ShowFloatBox();
		Invoke("ResetScene", 2f);
	}

	private void ResetScene()
	{
		Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulunDialog"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}

	public void NoRestart()
	{
		replayAlert.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("HideWindow", 1.25f);
	}

	private void HideWindow()
	{
		replayAlert.SetActive(value: false);
	}

	public void RefreshVal()
	{
		if (gameRunning || gameOver)
		{
			return;
		}
		gameRunning = true;
		yulunPenziDialog.AddVal(addPenziList);
		foreach (KeyValuePair<string, YulunNews> showNews in showNewsList)
		{
			if (showNews.Value.newsType == "0")
			{
				yulunMapList[showNews.Value.usedNewsList[showNews.Key].city.ToLower()].RefreshVal(showNews.Value, showNewsData[showNews.Key]);
			}
			else
			{
				yulunMapList[showNews.Value.newsList[showNews.Key].city.ToLower()].RefreshVal(showNews.Value, showNewsData[showNews.Key]);
			}
		}
		yulunDataDialog.ChangeVal();
		yulunTimeDialog.StartCountDown();
		yulunNewsDialog.ChangeHotNews();
		yulunNewsDialog.ChangeAlubaNews(showNewsData);
		yulunRstNews.gameObject.SetActive(value: true);
		yulunRstNews.Init(showNewsData);
		yulunDanmu.gameObject.SetActive(value: true);
		yulunDanmu.Init(showNewsData);
	}

	public void GameResult(bool result = false)
	{
		if (isHaveRst)
		{
			return;
		}
		isHaveRst = true;
		if ((gameOver && gameSuccess) || result)
		{
			Debug.Log("游戏成功");
			if (gameManager.homeScene.yulunEnterBtn != null)
			{
				Object.Destroy(gameManager.homeScene.yulunEnterBtn.gameObject);
			}
			gameManager.homeScene.goalDialog.CompletePercentItem("2000020", 100f);
			gameManager.player.playerdata.isYulunGameOver = true;
			gameManager.saveManager.SavePlayerData();
			Invoke("ShowDaniel", 2f);
		}
		else
		{
			Debug.Log("游戏失败");
			Object.Instantiate(Resources.Load<GameObject>("Dialog/faileddanielvideoDialog"), gameManager.homeScene.middle).GetComponent<FailedDanielVideoDialog>().endLoadPrefab = "Dialog/Yulun/yulungameover";
			Object.Destroy(base.gameObject);
		}
	}

	private void ShowDaniel()
	{
		Object.Instantiate(Resources.Load<GameObject>("Dialog/yulunSuccessDanielVideoDialog"), gameManager.homeScene.middle);
	}

	public void EndGame()
	{
		gameManager.homeScene.ShowSpecialVideoTip("3700060");
		Object.Destroy(base.gameObject);
	}

	public void Blur(string mapName)
	{
		foreach (KeyValuePair<string, YulunMap> yulunMap in yulunMapList)
		{
			if (yulunMap.Value.mapName != mapName)
			{
				yulunMap.Value.Blur();
			}
		}
	}
}
