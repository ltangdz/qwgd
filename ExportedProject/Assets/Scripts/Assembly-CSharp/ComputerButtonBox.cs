using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;

public class ComputerButtonBox : MonoBehaviour
{
	public ComputerButton btn_search;

	public ComputerButton btn_chat;

	public ComputerButton btn_weizhuang;

	public ComputerButton btn_dingwei;

	public ComputerButton btn_pic;

	public ComputerButton btn_pojie;

	public ComputerButton btn_cctv;

	public ComputerButton btn_note;

	public ComputerButton btn_mail;

	public ComputerButton btn_file;

	public ComputerButton btn_socialWorker;

	public Transform dialogtool;

	public GameManager gameManager;

	public HomeScene homeScene;

	public bool iscanclick;

	public GameObject browserDialog;

	public GameObject chatDialog;

	public GameObject mailDialog;

	public GameObject noteDialog;

	public GameObject sqlDialog;

	public GameObject scanDialog;

	public GameObject passwordDialog;

	public GameObject weizhaung;

	public GameObject phishing;

	public GameObject surveillanceDialog;

	public GameObject anoPhoneDialog;

	public Dictionary<string, GameObject> appFun = new Dictionary<string, GameObject>();

	private string chatID;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.computerButtonBox = this;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).GetComponent<ComputerButton>() != null)
			{
				appFun.Add(base.transform.GetChild(i).GetComponent<ComputerButton>().tool.ToString(), base.transform.GetChild(i).gameObject);
			}
		}
		if (!gameManager.player.GetEventId().Equals("110000"))
		{
			Debug.Log(gameManager.player.GetEventId());
			if (gameManager.player.GetEventId() == "110008")
			{
				RefreshToolDLC7();
			}
			else
			{
				string app = gameManager.dataManager.dic11[gameManager.player.GetEventId()].app;
				if (app != "")
				{
					string[] array = app.Substring(1).Split(';');
					for (int j = 0; j < array.Length; j++)
					{
						appFun[array[j]].SetActive(value: true);
					}
				}
			}
		}
		if (gameManager.player.playerdata.isZhadanStart && gameManager.player.GetEventId().Equals("110005"))
		{
			appFun["13"].SetActive(value: true);
		}
	}

	public void RefreshToolDLC7()
	{
		if (gameManager.player.GetEventId() == "110008")
		{
			string[] toolDLC = gameManager.player.playerdata.toolDLC7;
			for (int i = 0; i < toolDLC.Length; i++)
			{
				appFun[toolDLC[i]].SetActive(value: true);
			}
		}
	}

	public void FrontTool(int toolid)
	{
		if (gameManager.player.playerdata.isCourseOver == 0 && ((gameManager.player.playerdata.isCourse08 == 0 && toolid == 0) || (gameManager.player.playerdata.isCourse06 != 1 && toolid == 5) || (gameManager.player.playerdata.isCourse06 == 0 && toolid == 8) || (gameManager.player.playerdata.isCourse04 == 0 && toolid == 9)))
		{
			return;
		}
		switch (toolid)
		{
		case 0:
			if (gameManager.player.playerdata.isCourse09 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel09.HideCourse();
			}
			if (gameManager.player.playerdata.isTuli07 == 0)
			{
				gameManager.homeScene.courseManager.ShowTuli7();
			}
			if (weizhaung != null)
			{
				weizhaung.transform.SetAsLastSibling();
				SetZero(weizhaung);
			}
			else if (chatDialog != null)
			{
				chatDialog.transform.SetAsLastSibling();
				SetZero(chatDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 1:
			if (chatDialog != null)
			{
				chatDialog.transform.SetAsLastSibling();
				SetZero(chatDialog);
			}
			else if (weizhaung != null)
			{
				weizhaung.transform.SetAsLastSibling();
				SetZero(weizhaung);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 2:
			if (gameManager.player.playerdata.isCourse01 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel01.ReshowBlack();
			}
			if (browserDialog != null)
			{
				browserDialog.transform.SetAsLastSibling();
				SetZero(browserDialog);
				if (browserDialog.GetComponent<NewBrowserDialog>().isminimize)
				{
					browserDialog.GetComponent<NewBrowserDialog>().ResumeMinimize();
				}
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 4:
			if (scanDialog != null)
			{
				scanDialog.transform.SetAsLastSibling();
				SetZero(scanDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 5:
			if (gameManager.player.playerdata.isCourse06 == 1 && gameManager.player.playerdata.isCourseOver == 0)
			{
				if (gameManager.homeScene.courseManager.coursepanel06.gameObject.activeSelf)
				{
					gameManager.homeScene.courseManager.coursepanel06.HideCourse();
				}
				gameManager.homeScene.courseManager.ShowTuli5();
			}
			if (passwordDialog != null)
			{
				passwordDialog.transform.SetAsLastSibling();
				SetZero(passwordDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 6:
			if (gameManager.homeScene.iszhibojian)
			{
				homeScene.zhibojiannotebook.gameObject.SetActive(value: true);
				homeScene.zhibojiannotebook.Show();
				gameManager.soundManager.PlaySound(11);
				noteDialog = homeScene.zhibojiannotebook.gameObject;
				if (noteDialog != null)
				{
					noteDialog.transform.SetAsLastSibling();
				}
				else
				{
					OpenTool(toolid);
				}
			}
			else
			{
				homeScene.notebook.gameObject.SetActive(value: true);
				homeScene.notebook.Show();
				gameManager.soundManager.PlaySound(11);
				noteDialog = homeScene.notebook.gameObject;
				if (noteDialog != null)
				{
					noteDialog.transform.SetAsLastSibling();
				}
				else
				{
					OpenTool(toolid);
				}
			}
			break;
		case 9:
			if (gameManager.player.playerdata.isCourse05 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel05.HideCourse();
			}
			if (sqlDialog != null)
			{
				sqlDialog.transform.SetAsLastSibling();
				SetZero(sqlDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 10:
			if (gameManager.player.playerdata.isCourse07 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel07.HideCourse();
			}
			if (mailDialog != null)
			{
				mailDialog.transform.SetAsLastSibling();
				if (mailDialog.name.Contains("Login"))
				{
					SetZero(mailDialog);
				}
				else
				{
					SetMailDialogZero(mailDialog);
				}
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 12:
			if (phishing != null)
			{
				phishing.gameObject.SetActive(value: true);
				phishing.transform.SetAsLastSibling();
				phishing.transform.DOLocalMove(Vector3.zero, 0.3f);
				phishing.gameObject.GetComponent<PhishingDialog1>().ResetList();
				SetZero(phishing);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 13:
			if (surveillanceDialog != null)
			{
				surveillanceDialog.transform.SetAsLastSibling();
				SetZero(surveillanceDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 15:
			if (anoPhoneDialog != null)
			{
				anoPhoneDialog.transform.SetAsLastSibling();
				SetZero(anoPhoneDialog);
			}
			else
			{
				OpenTool(toolid);
			}
			break;
		case 3:
		case 7:
		case 8:
		case 11:
		case 14:
			break;
		}
	}

	public void SetZero(GameObject obj)
	{
		obj.transform.DOLocalMove(Vector3.zero, 0.3f);
	}

	public void SetMailDialogZero(GameObject obj)
	{
		obj.transform.DOLocalMove(new Vector3(-330f, 0f, 0f), 0.3f);
	}

	public void OpenTool(int toolid)
	{
		if (!iscanclick && toolid != 11 && toolid != 10)
		{
			return;
		}
		switch (toolid)
		{
		case 0:
		{
			ShowSelectPage();
			GameObject gameObject7 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetWeizhuangName()), dialogtool);
			gameObject7.transform.parent.gameObject.SetActive(value: true);
			gameObject7.GetComponent<Weizhuang>().Show();
			weizhaung = gameObject7;
			break;
		}
		case 1:
		{
			_ = gameManager.player.playerdata.chatLoginID;
			GameObject gameObject11 = (GameObject)Object.Instantiate(Resources.Load("Chat/chatLogin"), dialogtool);
			gameObject11.transform.parent.gameObject.SetActive(value: true);
			gameObject11.GetComponent<ChatLogin>().Show();
			chatDialog = gameObject11;
			break;
		}
		case 2:
		{
			GameObject gameObject4 = (GameObject)Object.Instantiate(Resources.Load("Dialog/browserDialog"), dialogtool);
			gameObject4.transform.parent.gameObject.SetActive(value: true);
			gameObject4.GetComponent<NewBrowserDialog>().Show();
			browserDialog = gameObject4;
			homeScene.newbrowserDialog = gameObject4.GetComponent<NewBrowserDialog>();
			break;
		}
		case 4:
		{
			GameObject gameObject6 = (GameObject)Object.Instantiate(Resources.Load("Dialog/scanDialog"), dialogtool);
			gameObject6.transform.parent.gameObject.SetActive(value: true);
			gameObject6.GetComponent<ScanDialog>().Show();
			scanDialog = gameObject6;
			break;
		}
		case 5:
		{
			GameObject gameObject10 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetPasswordDialogName()), dialogtool);
			gameObject10.transform.parent.gameObject.SetActive(value: true);
			gameObject10.GetComponent<PasswordDialog1>().Show();
			passwordDialog = gameObject10;
			break;
		}
		case 6:
			if (gameManager.homeScene.iszhibojian)
			{
				homeScene.zhibojiannotebook.gameObject.SetActive(value: true);
				homeScene.zhibojiannotebook.Show();
				gameManager.soundManager.PlaySound(11);
				noteDialog = homeScene.zhibojiannotebook.gameObject;
			}
			else
			{
				homeScene.notebook.gameObject.SetActive(value: true);
				homeScene.notebook.Show();
				gameManager.soundManager.PlaySound(11);
				noteDialog = homeScene.notebook.gameObject;
			}
			break;
		case 8:
			((GameObject)Object.Instantiate(Resources.Load("Browser/browser_mail"), dialogtool)).GetComponent<BrowserMail>().Show();
			break;
		case 9:
		{
			GameObject gameObject5 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetSqlDialogName()), dialogtool);
			sqlDialog = gameObject5;
			break;
		}
		case 10:
		case 11:
		{
			if (gameManager.homeScene.browserMail != null && gameManager.homeScene.browserMail.gameObject.activeInHierarchy)
			{
				return;
			}
			if (gameManager.homeScene.mailLogin != null && gameManager.homeScene.mailLogin.gameObject.activeInHierarchy)
			{
				gameManager.homeScene.mailLogin.Hide();
			}
			GameObject gameObject9 = ((toolid == 10) ? ((GameObject)Object.Instantiate(Resources.Load("Dialog/mailLogin"), dialogtool)) : ((GameObject)Object.Instantiate(Resources.Load("Dialog/mailDialog"), dialogtool)));
			string eventId = gameManager.player.GetEventId();
			if (toolid == 11)
			{
				BrowserMail component = gameObject9.GetComponent<BrowserMail>();
				component.UserMail = "admin";
				if (eventId != "110000")
				{
					gameManager.player.playerdata.AddHaveLogedMail("admin", "admin");
				}
				component.Show();
			}
			else
			{
				MailLogin component2 = gameObject9.GetComponent<MailLogin>();
				if (eventId != "110000")
				{
					gameManager.player.playerdata.AddHaveLogedMail("admin", "admin");
				}
				component2.Show();
			}
			mailDialog = gameObject9;
			break;
		}
		case 12:
		{
			GameObject gameObject8 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetFishDialogName()), dialogtool);
			gameObject8.transform.parent.gameObject.SetActive(value: true);
			gameObject8.GetComponent<PhishingDialog1>().Show();
			phishing = gameObject8;
			break;
		}
		case 13:
			if (gameManager.dataManager.GetShowSurveillanceItems(gameManager.player.GetEventId()).Count > 0)
			{
				GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load(gameManager.player.GetEventId().Equals("110005") ? "Dialog/zhuizongDialog2" : "Dialog/zhuizongDialog"), dialogtool);
				gameObject2.transform.parent.gameObject.SetActive(value: true);
				gameObject2.GetComponent<ZhuizongDialog>().Show();
				surveillanceDialog = gameObject2;
			}
			else
			{
				GameObject gameObject3 = (GameObject)Object.Instantiate(Resources.Load("Dialog/noitemDialog"), dialogtool);
				gameObject3.GetComponent<NoItemDialog>().Init("^surveillance01", "^surveillance18");
				gameObject3.GetComponent<NoItemDialog>().Show();
				surveillanceDialog = gameObject3;
			}
			break;
		case 15:
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetPhoneDialogName()), dialogtool);
			Debug.Log("phoneDialogOpen");
			gameObject.transform.parent.gameObject.SetActive(value: true);
			gameObject.GetComponent<PhoneCallDialog>().Show();
			anoPhoneDialog = gameObject;
			break;
		}
		}
		dialogtool.SetAsLastSibling();
	}

	private bool ShowSelectPage()
	{
		bool result = true;
		string[] array = gameManager.dataManager.dic11[gameManager.player.GetEventId()].lyingchat.Substring(1).Split(';');
		if (array[0].Equals("0"))
		{
			return false;
		}
		string key = array[gameManager.player.playerdata.weizhuangpos];
		if (gameManager.player.playerdata.weizhuangpos != -1)
		{
			if (gameManager.player.playerdata.camChatInfo.ContainsKey(key))
			{
				result = false;
				chatID = gameManager.dataManager.dic3[key].log.Substring(1);
				return result;
			}
		}
		else
		{
			result = false;
			chatID = gameManager.dataManager.dic3[key].log.Substring(1);
		}
		return result;
	}

	private void Awake()
	{
		DLCEventManager.Instance.onNoticeRefreshTool += RefreshToolDLC7;
	}

	private void OnDestroy()
	{
		DLCEventManager.Instance.onNoticeRefreshTool -= RefreshToolDLC7;
	}
}
