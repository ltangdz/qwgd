using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using tnt_deploy;

public class MissionResult : CustomDialog
{
	public Text eventTitle;

	public Text taskTime;

	public Text taskComplete;

	public Text taskPercent;

	public GameObject listContent;

	public Button leftBtn;

	public Button rightBtn;

	public Button sureBtn;

	public Button playAgainBtn;

	public Animator playAgainWindow;

	private bool play = true;

	private string eventID;

	private List<DATA20> allEvent = new List<DATA20>();

	[HideInInspector]
	public int completeEvent;

	[HideInInspector]
	public int completeItemVal;

	[HideInInspector]
	public string[] target = new string[4] { "^game_label03", "^game_label04", "^game_label05", "^game_label06" };

	private float page;

	[SerializeField]
	private Animator saveWindow;

	[SerializeField]
	private Button btn_save_no;

	[SerializeField]
	private Button btn_save_yes;

	private bool clickTaskOver;

	public bool isfake;

	[SerializeField]
	private GameObject dialog_van;

	[SerializeField]
	private string mailid;

	[SerializeField]
	private Button btn_competionfake;

	[SerializeField]
	private Button btn_skip;

	[SerializeField]
	private Button btn_check;

	public bool alertCanClick;

	public bool Play => play;

	private void Start()
	{
		if (gameManager.player.GetEventId().Equals("110004"))
		{
			isfake = !gameManager.player.playerdata.maillist["admin"][0].ContainsKey("1500083");
			if (gameManager.player.playerdata.isstartselectnored && !gameManager.player.playerdata.isfixredline)
			{
				gameManager.UnlockAchievements("disappearredline4");
			}
		}
		string time = (gameManager.player.playerdata.endTime / 60000).ToString();
		int num = 0;
		num = ((!gameManager.player.playerdata.itemlist.Contains("10453")) ? gameManager.player.playerdata.itemlist.Count : (gameManager.player.playerdata.itemlist.Count - 1));
		gameManager.player.RefreshLevel(num.ToString(), time);
		gameManager.CanShowSetting(1);
		eventID = gameManager.player.GetEventId();
		Debug.Log(eventID);
		SetAllList();
		Init();
		sureBtn.onClick.AddListener(delegate
		{
			if (!clickTaskOver)
			{
				clickTaskOver = true;
				TaskOver();
			}
		});
		playAgainBtn.onClick.AddListener(delegate
		{
			StartCoroutine(IfPlayAgain());
		});
		GetComponent<Animator>().enabled = true;
		gameManager.player.playerdata.isCourseOver = 1;
		btn_check.onClick.AddListener(delegate
		{
			if (!mailid.Equals(""))
			{
				gameManager.homeScene.SendMail(mailid);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		});
		btn_skip.onClick.AddListener(delegate
		{
			if (!clickTaskOver)
			{
				clickTaskOver = true;
				TaskOver();
			}
		});
		btn_competionfake.onClick.AddListener(delegate
		{
			dialog_van.SetActive(value: true);
			dialog_van.transform.DOScale(Vector3.one, 0.3f);
			dialog_van.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		});
	}

	private void SetAllList()
	{
		allEvent = gameManager.dataManager.GetAll20Items(eventID);
		for (int i = 0; i < allEvent.Count; i++)
		{
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("list_jiesuan"), listContent.transform).GetComponent<ListJieSuan>().Init(allEvent[i], this, i, gameManager);
		}
		if (allEvent.Count > 3)
		{
			leftBtn.gameObject.SetActive(value: true);
			rightBtn.gameObject.SetActive(value: true);
			leftBtn.GetComponent<CanvasGroup>().alpha = 0.2f;
			rightBtn.GetComponent<CanvasGroup>().alpha = 1f;
			listContent.transform.parent.GetComponent<ContentSizeFitter>().enabled = false;
			listContent.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1653f, 659f);
			listContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(15f, 315.7f);
			leftBtn.onClick.AddListener(MoveLeft);
			rightBtn.onClick.AddListener(MoveRight);
		}
	}

	public void Cancle()
	{
		if (alertCanClick)
		{
			btn_save_no.interactable = false;
			btn_save_yes.interactable = false;
			saveWindow.Play("Exit Panel Out");
			gameManager.CanShowSetting(-1);
			gameManager.musicManager.Stop();
			gameManager.saveManager.SavePlayerData(isshowlogo: false);
			Debug.Log("事件ID：" + gameManager.player.GetEventId());
			if (gameManager.player.GetEventId().Equals("110005"))
			{
				gameManager.player.ClearEvent();
				gameManager.player.playerdata.islast4 = true;
				gameManager.saveManager.SavePlayerData(isshowlogo: false);
				gameManager.saveManager.ShowSavePanel(1);
				gameManager.saveManager.savePanel.isOver = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				gameManager.player.AddEventID(isadd: true);
				gameManager.istaohuashow = false;
				gameManager.iscancollect = true;
				StartCoroutine(ChangeScene(gameManager.GetHomeSceneName()));
			}
		}
	}

	public void TaskOver()
	{
		btn_save_no.interactable = false;
		btn_save_yes.interactable = false;
		saveWindow.Play("Exit Panel Out");
		gameManager.musicManager.Stop();
		gameManager.saveManager.SavePlayerData();
		Debug.Log("事件ID：" + gameManager.player.GetEventId());
		if (gameManager.player.GetEventId().Equals("110005"))
		{
			gameManager.player.ClearEvent();
			gameManager.player.playerdata.islast4 = true;
			gameManager.saveManager.SavePlayerData(isshowlogo: false);
			gameManager.saveManager.ShowSavePanel(3);
			gameManager.saveManager.savePanel.isOver = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (gameManager.player.GetEventId().Equals("110003"))
		{
			gameManager.player.AddEventID(isadd: true);
			gameManager.saveManager.SavePlayerData(isshowlogo: false);
			gameManager.saveManager.ShowSavePanel(1);
			gameManager.saveManager.savePanel.isOver3 = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			gameManager.player.AddEventID(isadd: true);
			gameManager.saveManager.SavePlayerData(isshowlogo: false);
			gameManager.saveManager.ShowSavePanel(2);
			gameManager.saveManager.savePanel.isOver = true;
		}
	}

	private IEnumerator IfPlayAgain()
	{
		if (!playAgainWindow.isActiveAndEnabled)
		{
			playAgainWindow.gameObject.SetActive(value: true);
			playAgainWindow.Play("Exit Panel In");
			yield return new WaitForSeconds(0.5f);
			alertCanClick = true;
		}
	}

	public void DontPlayAgain()
	{
		if (alertCanClick)
		{
			alertCanClick = false;
			StartCoroutine(CloseWindow());
		}
	}

	private IEnumerator CloseWindow()
	{
		playAgainWindow.gameObject.SetActive(value: true);
		playAgainWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(2f);
		playAgainWindow.gameObject.SetActive(value: false);
	}

	public void PlayAgain()
	{
		if (alertCanClick)
		{
			alertCanClick = false;
			gameManager.player.AddEventID();
			if (gameManager.player.GetEventId() == "110000")
			{
				gameManager.player.playerdata.ClearCourse();
			}
			else
			{
				gameManager.player.ClearEvent();
			}
			gameManager.CanShowSetting(-1);
			gameManager.ShowFloatBox();
			StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
		}
	}

	private void MoveLeft()
	{
		if (page > 0f)
		{
			float x = listContent.GetComponent<RectTransform>().localPosition.x;
			if (page >= 3f)
			{
				listContent.transform.DOLocalMoveX(x + 1641f, 0.3f);
				page -= 3f;
			}
			else
			{
				listContent.transform.DOLocalMoveX(x + 547f * page, 0.3f);
				page -= page;
			}
			rightBtn.GetComponent<CanvasGroup>().alpha = 1f;
			if (page == 0f)
			{
				leftBtn.GetComponent<CanvasGroup>().alpha = 0.2f;
			}
		}
	}

	private void MoveRight()
	{
		if (page < (float)(allEvent.Count - 3))
		{
			float x = listContent.GetComponent<RectTransform>().localPosition.x;
			if (page <= (float)(allEvent.Count - 6))
			{
				listContent.transform.DOLocalMoveX(x - 1641f, 0.3f);
				page += 3f;
			}
			else
			{
				listContent.transform.DOLocalMoveX(x - 547f * ((float)allEvent.Count - (page + 3f)), 0.3f);
				page += (float)allEvent.Count - (page + 3f);
			}
			leftBtn.GetComponent<CanvasGroup>().alpha = 1f;
			if (page == (float)(allEvent.Count - 3))
			{
				rightBtn.GetComponent<CanvasGroup>().alpha = 0.2f;
			}
		}
	}

	private void Init()
	{
		string event_title = gameManager.dataManager.dic11[eventID].event_title;
		eventTitle.GetComponent<I18NText>().updateTranslation2(event_title);
		string text = (gameManager.player.playerdata.endTime / 60000).ToString();
		taskTime.GetComponent<I18NText>().updateTranslation2(text + " min");
		Debug.Log(completeEvent + ":" + allEvent.Count);
		float num = 0f;
		List<Transform> task = gameManager.homeScene.goalDialog.task;
		for (int i = 0; i < task.Count; i++)
		{
			num += task[i].GetComponent<GoalItem>().percent;
		}
		taskComplete.GetComponent<I18NText>().updateTranslation2(num / (float)(task.Count * 100) * 100f + "%");
		int number = gameManager.dataManager.dic11[eventID].number;
		if (gameManager.player.playerdata.itemlist.Contains("10453"))
		{
			completeItemVal = gameManager.player.playerdata.itemlist.Count - 1;
		}
		else
		{
			completeItemVal = gameManager.player.playerdata.itemlist.Count;
		}
		taskPercent.GetComponent<I18NText>().updateTranslation2(completeItemVal + "/" + number.ToString());
		if (completeItemVal >= number)
		{
			if (gameManager.player.GetEventId().Equals("110001"))
			{
				gameManager.UnlockAchievements("event01");
			}
			else if (gameManager.player.GetEventId().Equals("110002"))
			{
				gameManager.UnlockAchievements("event02");
			}
			else if (gameManager.player.GetEventId().Equals("110003"))
			{
				gameManager.UnlockAchievements("event03");
			}
			else if (gameManager.player.GetEventId().Equals("110004"))
			{
				gameManager.UnlockAchievements("event04");
			}
			else if (gameManager.player.GetEventId().Equals("110005"))
			{
				gameManager.UnlockAchievements("event05");
			}
		}
		btn_competionfake.transform.parent.gameObject.SetActive(isfake);
		sureBtn.gameObject.SetActive(!isfake);
	}

	private IEnumerator ChangeScene(string sceneName)
	{
		yield return new WaitForSeconds(2f);
		gameManager.CanShowSetting(-1);
		SceneManager.LoadScene(sceneName);
	}

	public void StopToResult(bool isend)
	{
	}

	public string DateTimeToStamp(DateTime now)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		return ((long)(now - dateTime).TotalMilliseconds).ToString();
	}

	public DateTime StampToDateTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long ticks = long.Parse(timeStamp + "0000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
