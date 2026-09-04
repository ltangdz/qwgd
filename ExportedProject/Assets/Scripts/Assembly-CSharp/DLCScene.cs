using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using tnt_deploy;

public class DLCScene : MonoBehaviour
{
	public MailTip mailTip;

	public VideoTip videoTip;

	public ItemBox notebook;

	public ItemBox zhibojiannotebook;

	public Transform middle;

	public Transform otherdialogpanel;

	public SelectGroup selectGroup;

	public DLCManager gameManager;

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
		else if (gameManager.player.GetEventId().Equals("110003") || gameManager.player.GetEventId().Equals("110004") || gameManager.player.GetEventId().Equals("110005"))
		{
			gameManager.musicManager.PlayMusicLoop(3);
		}
		Debug.LogError("停止音乐");
		FreshResolution();
	}

	public void HideAll()
	{
		iszhibojian = true;
		newsPanel.GetComponent<RectTransform>().DOLocalMoveX(-1255.5f, 0.3f);
		logPanel.GetComponent<RectTransform>().DOLocalMoveX(-1855.5f, 0.3f);
		goalDialog.GetComponent<RectTransform>().DOLocalMoveX(1167f, 0.3f).OnComplete(delegate
		{
			computerButtonBox.btn_note.HideNoteDialog();
		});
		notebook.HideAll();
	}

	public void ResumeAll()
	{
		newsPanel.GetComponent<RectTransform>().DOLocalMoveX(-704.5f, 0.3f);
		logPanel.GetComponent<RectTransform>().DOLocalMoveX(-958f, 0.3f);
		goalDialog.GetComponent<RectTransform>().DOLocalMoveX(734f, 0.3f);
		zhibojiannotebook.HideAll();
		computerButtonBox.btn_note.HideNoteDialog();
		iszhibojian = false;
	}

	private void FreshResolution()
	{
		if (SceneManager.GetActiveScene().name.Equals("homego") || SceneManager.GetActiveScene().name.Equals("homeDLC") || SceneManager.GetActiveScene().name.Equals("homecourse"))
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
		gameManager = GameObject.Find("DLCManager").GetComponent<DLCManager>();
		gameManager.issave = true;
		if (SceneManager.GetActiveScene().name.Equals("homecourse"))
		{
			gameManager.player.playerdata.ClearCourse();
		}
		else
		{
			gameManager.player.playerdata.SetCourse(1);
		}
		gameManager.saveManager.SavePlayerData();
		if (gameManager != null)
		{
			gameManager.homeScene = this;
		}
		if (gameManager.saveManager.pausePanel != null)
		{
			gameManager.saveManager.HidePausePanel();
		}
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
			if (gameManager.dataManager.dic39[id].type == 0)
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
		if (!gameManager.player.GetEventId().Equals("110000"))
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
		computerButtonBox.btn_note.transform.DOLocalMoveX(904.2f, 0.5f);
		computerButtonBox.iscanclick = true;
		logPanel.Open();
		noClick.SetActive(value: false);
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
		newsPanel.transform.DOLocalMoveX(-704.5f, 0.5f).OnComplete(delegate
		{
			string[] array = data11.newsid2.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				AddNews(array[i]);
			}
			StartCoroutine(ShowNews());
		});
		logPanel.Open();
		computerButtonBox.btn_note.transform.DOLocalMoveX(904.2f, 0.5f);
	}

	private IEnumerator ShowNews()
	{
		yield return new WaitForSeconds(1f);
		newsPanel.OpenNews();
	}

	public void StartTask2()
	{
		goalDialog.GetComponent<Animator>().Play("ani_showtaskdialog");
		computerButtonBox.btn_note.transform.DOLocalMoveX(904.2f, 0.5f);
	}

	private void StartVideo()
	{
		StartVideoDialog(0);
	}

	public void StartVideoDialog(int type)
	{
		Debug.Log("StartVideo:" + type);
		if (videoTip != null)
		{
			videoTip.gameObject.SetActive(value: true);
			videoTip.GetComponent<Animator>().Play("ani_videotip");
			videoTip.SetTip("Ashley Clayson", "Sue", type);
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
}
