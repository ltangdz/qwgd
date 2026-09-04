using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using tnt_deploy;

public class HomeScene : MonoBehaviour
{
	public MailTip mailTip;

	public VideoTip videoTip;

	public ItemBox notebook;

	public ItemBox zhibojiannotebook;

	public Transform middle;

	public Transform otherdialogpanel;

	public SelectGroup selectGroup;

	public GameManager gameManager;

	public LastGoalDialog goalDialog;

	public ComputerButtonBox computerButtonBox;

	public ComputerButton computerButton;

	public GameObject eventsystem;

	public HB3Top hB3Top;

	public PhoneCallDialog phoneDialog;

	public NewBrowserDialog newbrowserDialog;

	public SqlDialog sqlDialog;

	public BrowserBox browserBox;

	public BrowserMail browserMail;

	public MailLogin mailLogin;

	public GameObject socialBrowser;

	public PictureDialog pictureDialog;

	public GameObject largeDialog;

	public ChatBox chatDialog;

	public ChatBox weizhuangDialog;

	public Weizhuang weizhuang;

	public PhishingDialog1 phishing;

	public NewsPanel newsPanel;

	public LogPanel logPanel;

	public CourseManager courseManager;

	public InvadeDialog invadeDialog;

	public InvadePhoneDialog invadePhoneDialog;

	public FishPhoneInvadeDialog fishPhoneInvadeDialog;

	public PasswordDialog1 passworddialog1;

	public PasswordDialog2 passworddialog2;

	[Header("黑客入侵背景")]
	public HackerBk hackerBk;

	public CameraFilterPack_Atmosphere_Rain_Pro cameraFilterPack_Atmosphere_Rain_Pro;

	public CameraFilterPack_TV_Noise cameraFilterPack_TV_Noise;

	[Header("黑客花屏效果")]
	public CameraFilterPack_Noise_TV_2 cameraFilterPack_Noise_TV_2;

	[Header("黑客花屏效果细纹")]
	public CameraFilterPack_Noise_TV cameraFilterPack_Noise_TV_1;

	[Header("马赛克效果")]
	public CameraFilterPack_Pixel_Pixelisation cameraFilterPack_Pixel_Pixelisation;

	[Header("屏幕干扰效果")]
	public CameraFilterPack_FX_Glitch1 cameraFilterPack_fx_Glitch1;

	[Header("结尾动画切换效果")]
	public CameraFilterPack_TV_Distorted distorted;

	[Header("结尾动画切换效果")]
	public CameraFilterPack_FX_Glitch3 glitch3;

	[Header("删除动画效果")]
	public CameraFilterPack_NewGlitch4 glitch4;

	[Header("结尾动画切换效果")]
	public CameraFilterPack_Blur_DitherOffset ditheroffset;

	public GameObject noClick;

	public ZhadanInvade zhadanInvade;

	public bool isshowvideo;

	public List<string> needshowvideolist = new List<string>();

	public YulunDialog yulunDialog;

	public YulunEnterBtn yulunEnterBtn;

	public LiveBroadingEnterBtn liveBroadingEnterBtn;

	public LiveBroadingChatEnterBtn liveBroadingchatEnterBtn;

	public HoutaiPanel houtaiPanel;

	public ZhadanInvoke zhadanInvoke;

	public ZhadanInvoke zhadanInvoke1;

	public NewZhadanDialog newZhadanDialog;

	public ZhadanInvade1 zhadanInvade1;

	public ZhadanCodeRun zhadanCodeRun;

	[SerializeField]
	private GameObject NewVideoCanvas;

	public TijiaoAlertCody tijiaoAlertCody;

	public LiveBroadcastingDialog2 liveBroadcastingDialog;

	public LiveBroadingChatBox liveBroadingChatBox;

	public bool iszhibojian;

	public bool isloginzhibochat;

	public DuikangDialog duikangDialog;

	private bool isShowCatchButton;

	public CatchEnterButton _catchEnterButton;

	public LiveBroadingEnterBtn invadeTitanDLC7Button;

	public bool isgoaldialogalpha;

	public bool isopenachi = true;

	public void ShowLiveBroadSqlEnterBtn()
	{
		if (gameManager.player.playerdata.temporaryhopelist.Contains("10559"))
		{
			liveBroadingEnterBtn.gameObject.SetActive(value: true);
		}
	}

	private void Start()
	{
		Debug.LogError("停止音乐");
		gameManager.musicManager.Stop();
		if (SceneManager.GetActiveScene().name.Equals("homecourse") || gameManager.player.GetEventId() == "110001" || gameManager.player.GetEventId() == "110002")
		{
			gameManager.musicManager.PlayMusicLoop(3);
			if (SceneManager.GetActiveScene().name.Equals("homecourse"))
			{
				gameManager.player.playerdata.getMask = true;
				gameManager.saveManager.SavePlayerData();
			}
		}
		else if (gameManager.player.GetEventId().Equals("110003") || gameManager.player.GetEventId().Equals("110004") || gameManager.player.GetEventId().Equals("110005") || gameManager.player.GetEventId().Equals("110006") || gameManager.player.GetEventId().Equals("110008"))
		{
			gameManager.musicManager.PlayMusicLoop(3);
		}
		Debug.LogError("停止音乐");
		FreshResolution();
		Invoke("DLC7TitanFinished", 5f);
	}

	public void HideAll()
	{
		iszhibojian = true;
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			newsPanel.GetComponent<RectTransform>().DOLocalMoveX(-1255.5f, 0.3f);
		}
		logPanel.GetComponent<RectTransform>().DOLocalMoveX(-1855.5f, 0.3f);
		goalDialog.GetComponent<RectTransform>().DOLocalMoveX(1167f, 0.3f).OnComplete(delegate
		{
			if (gameManager.IsBasic())
			{
				computerButtonBox.btn_note.HideNoteDialog();
			}
		});
		notebook.HideAll();
	}

	public void ResumeAll()
	{
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			newsPanel.GetComponent<RectTransform>().DOLocalMoveX(-704.5f, 0.3f);
		}
		logPanel.GetComponent<RectTransform>().DOLocalMoveX(-958f, 0.3f);
		goalDialog.GetComponent<RectTransform>().DOLocalMoveX(734f, 0.3f);
		zhibojiannotebook.HideAll();
		computerButtonBox.btn_note.HideNoteDialog();
		iszhibojian = false;
	}

	private void FreshResolution()
	{
		if (SceneManager.GetActiveScene().name.Equals("homego") || SceneManager.GetActiveScene().name.Equals("homeDLC") || SceneManager.GetActiveScene().name.Equals("homeDLC7") || SceneManager.GetActiveScene().name.Equals("homecourse"))
		{
			float num = float.Parse(((float)Screen.width / (float)Screen.height).ToString("f2"));
			Debug.Log("bili:" + num);
			if (num == 1.77f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 935f);
			}
			else if (num == 1.33f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1248f);
			}
			else if (num == 1.6f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1038f);
			}
			else if (num == 1.5f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1108f);
			}
			else if (num == 1.56f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1065f);
			}
			else if (num == 1.25f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1332f);
			}
			else if (num == 1.66f)
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 998f);
			}
			else
			{
				GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 935f);
			}
		}
	}

	public bool Iscanopentool()
	{
		if (!(fishPhoneInvadeDialog != null) && !(invadePhoneDialog != null))
		{
			return invadeDialog != null;
		}
		return true;
	}

	public void HideNewVideoCanvas()
	{
		if (NewVideoCanvas != null)
		{
			NewVideoCanvas.SetActive(value: false);
		}
	}

	public void ShowNewVideoCanvas()
	{
		NewVideoCanvas.SetActive(value: true);
	}

	public void AddNeedShowVideoList(string id)
	{
		if (!needshowvideolist.Contains(id))
		{
			needshowvideolist.Add(id);
		}
	}

	public void ShowNextVideo()
	{
		isshowvideo = false;
		if (needshowvideolist.Count <= 0)
		{
			return;
		}
		if (needshowvideolist[0].StartsWith("#"))
		{
			ShowVideoTip(needshowvideolist[0].Substring(1));
		}
		else if (!needshowvideolist[0].StartsWith("37"))
		{
			if (Resources.Load("Dialog/" + needshowvideolist[0]) != null)
			{
				Object.Instantiate(Resources.Load("Dialog/" + needshowvideolist[0]) as GameObject, middle);
			}
			else
			{
				Debug.Log("不存在这个ciodialog:" + needshowvideolist[0]);
			}
		}
		else
		{
			string[] array = needshowvideolist[0].Split(':');
			ShowVideoTip(array[0], array[1], array[2] == "1");
		}
		needshowvideolist.RemoveAt(0);
	}

	private void Awake()
	{
		InvadeEvent.Instance.onNoticeInvadeDecryptSuccess += NoticeInvadeDecryptSuccess;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.issave = true;
		if (SceneManager.GetActiveScene().name.Equals("homecourse"))
		{
			gameManager.player.playerdata.ClearCourse();
		}
		else
		{
			gameManager.player.playerdata.SetCourse(1);
		}
		if (gameManager != null)
		{
			gameManager.homeScene = this;
		}
		if (gameManager.saveManager.pausePanel != null)
		{
			gameManager.saveManager.HidePausePanel();
		}
		List<string> itemlist = gameManager.player.playerdata.itemlist;
		List<DATA1> allItems = gameManager.dataManager.GetAllItems(gameManager.player.GetEventId());
		for (int i = 0; i < allItems.Count; i++)
		{
			DATA1 dATA = allItems[i];
			bool flag = false;
			for (int j = 0; j < itemlist.Count; j++)
			{
				if (itemlist[j] == dATA.ID.ToString())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Debug.Log("id:" + dATA.ID + "---message:+" + I18N.instance.getValue(dATA.message) + "---title:" + I18N.instance.getValue(dATA.title));
			}
		}
		Debug.Log("gameManager.player.playerdata");
		Debug.Log(gameManager.player.playerdata);
		if (!gameManager.player.playerdata.isstarttask)
		{
			Invoke("StartVideo", 5f);
		}
		else
		{
			ContinueGame();
		}
	}

	public void AddNews(string id, bool isloadoldnew = false)
	{
		if (isloadoldnew)
		{
			newsPanel.SetNewContent(id);
		}
		else if (!gameManager.player.playerdata.newsidlist.Contains(id))
		{
			newsPanel.SetNewContent(id);
			gameManager.player.playerdata.newsidlist.Add(id);
		}
	}

	public void ShowVideoTip(string id, string compeletemissionid = "", bool isshowreasoning = false)
	{
		if (isshowvideo || gameManager.istaohuashow)
		{
			Debug.LogError("未显示cio" + id + ";isshowvideo:" + isshowvideo + ";istaohuashow:" + gameManager.istaohuashow);
			AddNeedShowVideoList(id + ":" + compeletemissionid + ":" + (isshowreasoning ? "1" : "0"));
		}
		else if (!gameManager.player.playerdata.videotiplist.Contains(id) && gameManager.dataManager.dic39.ContainsKey(id))
		{
			if (gameManager.Is_Dlc7())
			{
				gameManager.homeScene.isshowvideo = true;
				Object.Instantiate(Resources.Load("Dialog/VideoDialogCustomDLC7") as GameObject, middle).GetComponent<VideoDialogCustom>().Init(id, showName: true, isStartTask: false, "");
			}
			else if (gameManager.dataManager.dic39[id].type == 0)
			{
				Debug.LogError("普通cio：" + id);
				gameManager.homeScene.isshowvideo = true;
				Object.Instantiate(Resources.Load("Dialog/customvideoDialog") as GameObject, middle).GetComponent<CustomVideoDialog>().Init(id, isshowreasoning, compeletemissionid);
			}
			else
			{
				Debug.LogError("特殊cio：" + id);
				ShowSpecialVideoTip(id);
			}
		}
		else
		{
			Debug.LogError("已有该cio：" + id);
		}
	}

	public void ShowSpecialVideoTip(string id)
	{
		if (!gameManager.player.playerdata.videotiplist.Contains(id))
		{
			gameManager.homeScene.isshowvideo = true;
			if (id != "3700077")
			{
				Object.Instantiate(Resources.Load("Dialog/videoDialog" + id) as GameObject, middle);
			}
			else
			{
				Object.Instantiate(Resources.Load("Dialog/videoDialog" + id) as GameObject, base.transform);
			}
		}
	}

	public void StartVideoDialog(string videoname, string funName = "")
	{
		if (isshowvideo)
		{
			AddNeedShowVideoList(videoname);
			return;
		}
		gameManager.homeScene.isshowvideo = true;
		Debug.Log("应该出现的cio说话：" + videoname);
		GameObject gameObject = Object.Instantiate(Resources.Load("Dialog/" + videoname) as GameObject, middle);
		if (videoname == "videoDialogtaskfailed" && funName != "")
		{
			gameObject.GetComponent<VideoDialogTaskFailed>().Init(funName);
		}
	}

	public void GetTask(string mailid, string othermailid = "")
	{
		StartTask1(mailid, othermailid);
	}

	private void DLC7TitanFinished()
	{
		if (!gameManager.player.playerdata.videotiplist.Contains("3910004") && gameManager.player.playerdata.dlc7Invades[0] == 2)
		{
			ShowVideoTip("3910004");
		}
		if (!gameManager.player.playerdata.aiSpeakHistoryIds.Contains("3910023") && gameManager.player.playerdata.dlc7Invades[1] == 2)
		{
			DLCEventManager.Instance.NoticeAITalk("3910023");
		}
	}

	public void ContinueGame()
	{
		Debug.Log("继续游戏");
		string taskmail = gameManager.dataManager.dic11[gameManager.player.GetEventId()].taskmail;
		if (taskmail.Trim() != "")
		{
			taskmail = taskmail.Substring(1);
			if (!gameManager.player.playerdata.maillist.ContainsKey("admin"))
			{
				mailTip.gameObject.SetActive(value: true);
				mailTip.SetMail("admin", taskmail);
				Debug.Log("不包含admin");
			}
			else if (!gameManager.player.playerdata.maillist["admin"][0].ContainsKey(taskmail))
			{
				mailTip.gameObject.SetActive(value: true);
				mailTip.SetMail("admin", taskmail);
				Debug.Log("没有给admin发过任务邮件");
			}
			else if (gameManager.player.playerdata.maillist["admin"][0][taskmail] == 0)
			{
				mailTip.gameObject.SetActive(value: true);
				mailTip.SetMail("admin", taskmail);
				Debug.Log("没有读过任务邮件");
			}
		}
		goalDialog.GetComponent<Animator>().Play("ani_showtaskdialog");
		DATA11 data11 = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		if (!gameManager.player.GetEventId().Equals("110000") && gameManager.GameType != GameTypeEnum.DLC7)
		{
			newsPanel.transform.DOLocalMoveX(-704.5f, 0.5f).OnComplete(delegate
			{
				for (int i = 0; i < gameManager.player.playerdata.newsidlist.Count; i++)
				{
					AddNews(gameManager.player.playerdata.newsidlist[i], isloadoldnew: true);
				}
				if (gameManager.player.playerdata.newsidlist.Count == 0)
				{
					string[] array = data11.newsid2.Substring(1).Split(';');
					for (int j = 0; j < array.Length; j++)
					{
						AddNews(array[j]);
					}
				}
				StartCoroutine(ShowNews());
			});
		}
		if (gameManager.player.GetEventId().Equals("110005"))
		{
			if (gameManager.player.playerdata.isZhadanStart)
			{
				Invoke("StartZhadan", 1f);
			}
			else
			{
				gameManager.StopRecordTime();
			}
			if (gameManager.player.playerdata.isDelVan)
			{
				ShowVideoTip("3700078");
			}
		}
		if (gameManager.player.playerdata.reasoninglist.Contains("4012") && !gameManager.player.playerdata.isDecryptInvade)
		{
			ShowCatchButton(1);
		}
		if (gameManager.player.playerdata.isCanCatch)
		{
			ShowCatchButton();
		}
		if (gameManager.Is_Dlc6())
		{
			List<string> itemlist = gameManager.player.playerdata.itemlist;
			Dictionary<string, List<Dictionary<string, int>>> maillist = gameManager.player.playerdata.maillist;
			bool flag = false;
			if (maillist.ContainsKey("admin"))
			{
				List<Dictionary<string, int>> list = maillist["admin"];
				for (int num = 0; num < list.Count; num++)
				{
					if (list[num].ContainsKey("1510021"))
					{
						flag = true;
						break;
					}
				}
				if (itemlist.Contains("11131") && itemlist.Contains("11172") && itemlist.Contains("11171") && itemlist.Contains("11173") && itemlist.Contains("11206") && itemlist.Contains("11183") && !flag)
				{
					SendMail("1510021");
				}
			}
		}
		if (gameManager.Is_Dlc7())
		{
			Invoke("ShowNoteDlc7", 1.5f);
		}
		else
		{
			computerButtonBox.btn_note.transform.DOLocalMoveX(GetOpenNoteX(), 0.5f);
		}
		computerButtonBox.iscanclick = true;
		logPanel.Open();
		noClick.SetActive(value: false);
		ExtraDlc7();
	}

	private void ExtraDlc7()
	{
		ShowDLC7ToolBox();
		BtnNoteDLC7();
		Invoke("Dlc7Finished", 5f);
	}

	private void Dlc7Finished()
	{
		if (gameManager.player.playerdata.titanStep == 4 && !gameManager.player.playerdata.itemlist.Contains("11410"))
		{
			notebook.AddNewItem("11410");
		}
	}

	public void ShowNoteDlc7()
	{
		if (gameManager.player.playerdata.showTitanButton)
		{
			invadeTitanDLC7Button.gameObject.SetActive(value: true);
		}
		if (computerButton != null)
		{
			computerButton.buttonbox.gameObject.SetActive(gameManager.player.playerdata.isShowNote);
			if (gameManager.player.playerdata.isShowNote)
			{
				BtnNoteDLC7();
			}
		}
	}

	private void StartZhadan()
	{
		HideAll();
		Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadandialog"), gameManager.homeScene.middle);
		string key = "";
		List<string> openedMail = gameManager.player.playerdata.OpenedMail;
		if (openedMail.Count == 0)
		{
			key = "1500086";
		}
		else
		{
			switch (openedMail[openedMail.Count - 1])
			{
			case "1500086":
				key = "1500087";
				break;
			case "1500087":
				key = "1500088";
				break;
			case "1500088":
				key = "1500089";
				break;
			}
		}
		if (gameManager.player.playerdata.maillist["admin"][0].ContainsKey(key))
		{
			gameManager.player.playerdata.maillist["admin"][0][key] = 0;
		}
		BrowserMail component = ((GameObject)Object.Instantiate(Resources.Load("Dialog/mailDialog"), gameManager.homeScene.computerButtonBox.dialogtool)).GetComponent<BrowserMail>();
		component.Show();
		component.Login("admin", "admin");
		gameManager.player.playerdata.zhadanhide = false;
		gameManager.StartRecordTime();
		if (gameManager.player.playerdata.temporaryhopelist.Contains("10607") && !gameManager.player.playerdata.OpenMail.Contains("1500089"))
		{
			ShowVideoTip("3700064");
		}
	}

	private void StartTask1(string mailid, string othermailid = "")
	{
		mailTip.gameObject.SetActive(value: true);
		if (!othermailid.Equals(""))
		{
			mailTip.SetMail("admin", othermailid);
		}
		if (!mailid.Equals(""))
		{
			mailTip.SetMail("admin", mailid);
		}
		computerButtonBox.iscanclick = true;
		goalDialog.GetComponent<Animator>().Play("ani_showtaskdialog");
		DATA11 data11 = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			newsPanel.transform.DOLocalMoveX(-704.5f, 0.5f).OnComplete(delegate
			{
				string[] array = data11.newsid2.Substring(1).Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					AddNews(array[i]);
				}
				StartCoroutine(ShowNews());
			});
		}
		logPanel.Open();
		computerButtonBox.btn_note.transform.DOLocalMoveX(GetOpenNoteX(), 0.5f);
		ShowDLC7ToolBox();
	}

	public void StartTasks(string mailid, string[] othermailids = null)
	{
		mailTip.gameObject.SetActive(value: true);
		if (othermailids != null)
		{
			for (int i = 0; i < othermailids.Length; i++)
			{
				mailTip.SetMail("admin", othermailids[i]);
			}
		}
		if (!mailid.Equals(""))
		{
			mailTip.SetMail("admin", mailid);
		}
		computerButtonBox.iscanclick = true;
		goalDialog.GetComponent<Animator>().Play("ani_showtaskdialog");
		DATA11 data11 = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			newsPanel.transform.DOLocalMoveX(-704.5f, 0.5f).OnComplete(delegate
			{
				string[] array = data11.newsid2.Substring(1).Split(';');
				for (int j = 0; j < array.Length; j++)
				{
					AddNews(array[j]);
				}
				StartCoroutine(ShowNews());
			});
		}
		logPanel.Open();
		BtnNoteDLC7();
		ShowDLC7ToolBox();
	}

	private IEnumerator ShowNews()
	{
		yield return new WaitForSeconds(1f);
		newsPanel.OpenNews();
	}

	public void StartTask2()
	{
		goalDialog.GetComponent<Animator>().Play("ani_showtaskdialog");
		BtnNoteDLC7();
		ShowDLC7ToolBox();
	}

	private void OnDestroy()
	{
		InvadeEvent.Instance.onNoticeInvadeDecryptSuccess -= NoticeInvadeDecryptSuccess;
	}

	private void BtnNoteDLC7()
	{
		if (iszhibojian)
		{
			return;
		}
		if (gameManager.Is_Dlc7())
		{
			if (gameManager.player.playerdata.videotiplist.Contains("3910004"))
			{
				computerButtonBox.btn_note.transform.DOLocalMoveX(GetOpenNoteX(), 0.5f);
			}
			else
			{
				computerButtonBox.btn_note.transform.DOLocalMoveX(GetOpenNoteX() + 1000f, 0f);
			}
		}
		else
		{
			computerButtonBox.btn_note.transform.DOLocalMoveX(GetOpenNoteX(), 0.5f);
		}
	}

	private void StartVideo()
	{
		StartVideoDialog(0);
	}

	private void ShowDLC7ToolBox()
	{
		if (gameManager.GameType == GameTypeEnum.DLC7)
		{
			hB3Top.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f);
			computerButtonBox.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		}
	}

	private void HideDLC7ToolBox()
	{
		if (gameManager.GameType == GameTypeEnum.DLC7)
		{
			computerButtonBox.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		}
	}

	public void taskFail(string funName)
	{
		int type = 0;
		switch (funName)
		{
		case "chat":
			type = 0;
			break;
		case "phone":
			type = 1;
			break;
		case "invade":
			type = 2;
			break;
		case "invadephone":
			type = 2;
			break;
		}
		Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(type, gameManager);
		gameManager.musicManager.ResumeVol();
	}

	public void StartVideoDialog(int type)
	{
		if (videoTip != null)
		{
			videoTip.gameObject.SetActive(value: true);
			videoTip.GetComponent<Animator>().Play("ani_videotip");
			if (gameManager.Is_Dlc6())
			{
				videoTip.SetTip("Herbert Lee", "_dlc6_Herberttb", type);
			}
			else if (gameManager.Is_Dlc7())
			{
				videoTip.SetTip(gameManager.player.playerdata.aiNameDlc7, "__dlc7_ai", type);
			}
			else
			{
				videoTip.SetTip("Ashley Clayson", "Sue", type);
			}
			if (SceneManager.GetActiveScene().name.Equals("homecourse") && gameManager.issteam && gameManager.steamAchi != null && !gameManager.steamAchi.GetAchievement("nowork"))
			{
				Invoke("OpenAchi", 1800f);
			}
		}
	}

	private void OpenAchi()
	{
		if (isopenachi)
		{
			Debug.Log("打开成就nowork");
			gameManager.UnlockAchievements("nowork");
		}
		else
		{
			Debug.Log("gaunbi成就nowork");
		}
	}

	public void AfterTimeSendEmail(float time, string emailid)
	{
		StartCoroutine(AfterTimeEmail(time, emailid));
	}

	private IEnumerator AfterTimeEmail(float time, string emailid)
	{
		yield return new WaitForSeconds(time);
		mailTip.SetMail("admin", emailid);
	}

	public void AddMail(string emailid)
	{
		gameManager.player.SendMail("admin", emailid);
	}

	public void SendMail(string emailid)
	{
		mailTip.SetMail("admin", emailid);
	}

	public void SendMail1(string emailid)
	{
		mailTip.SetMail1("admin", emailid);
	}

	public void ShowSaolei()
	{
		Object.Instantiate(Resources.Load("Dialog/Saolei/saoleibegin") as GameObject, middle);
	}

	public float GetOpenNoteX()
	{
		GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.IsAllDlc())
		{
			return 899.3f;
		}
		return 904.2f;
	}

	private void NoticeInvadeDecryptSuccess()
	{
		gameManager.player.playerdata.isDecryptInvade = true;
		gameManager.saveManager.SavePlayerData();
		ResumeAll();
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetFishPhoneInvadeDialogName()), middle);
		obj.GetComponent<FishPhoneInvadeDialog>().Init("3310000", pojieResult: true);
		obj.GetComponent<FishPhoneInvadeDialog>().Show();
	}

	public void ShowCatchButton(int type = 0)
	{
		if (_catchEnterButton == null)
		{
			isShowCatchButton = true;
			GameObject gameObject = Object.Instantiate(Resources.Load("_DLC/Prefabs/HomeTools/CatchEnterButton") as GameObject, middle);
			_catchEnterButton = gameObject.GetComponent<CatchEnterButton>();
			_catchEnterButton.InitData(type);
		}
	}

	public void StartInvadeDecrypt()
	{
		if (_catchEnterButton != null)
		{
			Object.Destroy(_catchEnterButton.gameObject);
		}
		HideAll();
		computerButtonBox.btn_note.transform.DOScale(Vector3.zero, 0f);
		Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/HomeTools/InvadeDecryptPanel"), gameManager.homeScene.middle);
	}
}
