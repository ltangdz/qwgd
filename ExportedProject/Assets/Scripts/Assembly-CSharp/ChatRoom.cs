using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using tnt_deploy;

public class ChatRoom : MonoBehaviour
{
	public ScrollRect chatRoom;

	public Text chatName;

	public Image onlineImg;

	public Sprite noClick;

	public Image screenFloat;

	public Image screenImg;

	public GameObject content;

	public Sprite underLine;

	public Sprite onLine;

	public GameObject selectCanvas;

	public GameObject up;

	public GameObject down;

	public Button img_click;

	public bool haveVal;

	public Button btnTop;

	private bool canScreen;

	private GameManager gameManager;

	private string screenID;

	private ChatList chatList;

	private string chatType;

	public string[] reply;

	private List<string> idList = new List<string>();

	private float chatTrustVal;

	private string chatLabelID = "";

	private int target;

	private bool openSel;

	private bool saying;

	private Coroutine run;

	private DATA3 _data3;

	private string _lastId;

	private bool canSel = true;

	public void Init(ChatList chatListObj, string type, GameManager gm, bool isrecord, DATA3 data3)
	{
		_data3 = data3;
		gameManager = gm;
		chatList = chatListObj;
		chatType = type;
		string[] chatListID = chatListObj.ChatListID;
		for (int i = 0; i < content.transform.childCount; i++)
		{
			if (!content.transform.GetChild(i).gameObject.name.Equals("img_click"))
			{
				Object.Destroy(content.transform.GetChild(i).gameObject);
			}
		}
		switch (type)
		{
		case "0":
		case "2":
			Show24Form(chatListID, isrecord);
			break;
		case "1":
			StartCoroutine(Show22Form(chatListID));
			break;
		}
		btnTop.onClick.AddListener(delegate
		{
			chatRoom.DOVerticalNormalizedPos(1f, 0.29f);
		});
	}

	private IEnumerator Show22Form(string[] chatlistID)
	{
		SetTitle(chatList.GetId);
		string lastTime = "";
		for (int i = 0; i < chatlistID.Length; i++)
		{
			DATA22 dATA = gameManager.dataManager.dic22[chatlistID[i]];
			string title = dATA.title;
			if (gameManager.dataManager.dic22[chatlistID[i]].type == 3)
			{
				lastTime = title;
				Object.Instantiate(Resources.Load<Transform>("Chat/frd_tishi"), content.transform).Find("Text").GetComponent<I18NText>()
					.updateTranslation5(gameManager.dataManager.dic22[chatlistID[i]].title);
			}
			else
			{
				if (gameManager.GameType == GameTypeEnum.DLC6 || gameManager.GameType == GameTypeEnum.DLC7)
				{
					if (lastTime == "")
					{
						lastTime = title;
						Object.Instantiate(Resources.Load<Transform>("Chat/frd_tishi"), content.transform).Find("Text").GetComponent<I18NText>()
							.updateTranslation5(title);
					}
					else if (title != lastTime)
					{
						lastTime = title;
						Object.Instantiate(Resources.Load<Transform>("Chat/frd_tishi"), content.transform).Find("Text").GetComponent<I18NText>()
							.updateTranslation5(title);
					}
				}
				int num = gameManager.dataManager.dic22[chatlistID[i]].chatType;
				GameObject gameObject = ((num != 0) ? Object.Instantiate(Resources.Load<GameObject>("Chat/chat_item"), content.transform) : Object.Instantiate(Resources.Load<GameObject>("Chat/chat_itemBak"), content.transform));
				gameObject.GetComponent<ChatLabelInfo>().Init(chatlistID[i], chatType, chatList.parObj, chatList, gameManager, num, isrecord: true, i == chatlistID.Length - 1);
			}
			if (dATA.is_blacklist == "#1")
			{
				Object.Instantiate(Resources.Load<Transform>("Chat/frd_blacklist"), content.transform);
			}
			chatList.parObj.LineToBottom(chatRoom);
			yield return new WaitForSeconds(0.01f);
		}
		string screen = gameManager.dataManager.dic23[chatList.GetId].screen;
		if (screen != "" && !gameManager.player.playerdata.itemlist.Contains(screen))
		{
			NeedScreen(screen.Substring(1), chatList.parObj);
		}
		if (!(chatList.GetId == "2310071") && chatList.GetId == "2310088")
		{
			gameManager.UnlockAchievements("chemicalreaction");
		}
	}

	private void Show24Form(string[] chatlistID, bool isrecord)
	{
		Debug.Log("Show24Form:" + chatlistID.ToString());
		SetTitle(chatList.GetId);
		for (int i = 0; i < chatlistID.Length; i++)
		{
			Show24Label(chatlistID[i], 0, isrecord);
		}
	}

	public void StartChat(ChatList parobj, string type, string chatID, GameManager gm, DATA3 data3)
	{
		if (_lastId == chatID)
		{
			return;
		}
		_lastId = chatID;
		_data3 = data3;
		btnTop.gameObject.SetActive(value: false);
		chatList = parobj;
		gameManager = gm;
		chatType = type;
		string frdreply = gameManager.dataManager.dic24[chatID].frdreply;
		if (gameManager.IsBasic() || _data3 == null)
		{
			SetTitle(parobj.GetId, 1);
		}
		else
		{
			SetTitle(_data3.reply.Substring(1), 1);
		}
		int num = ((!(frdreply.Trim() == "")) ? 1 : 0);
		target = num;
		chatLabelID = chatID;
		Debug.Log(type);
		StartCoroutine(ChatInfo(chatLabelID, target, openSel));
		parobj.parObj.hotarea.onClick.RemoveAllListeners();
		parobj.parObj.hotarea.onClick.AddListener(delegate
		{
			if (!saying)
			{
				if (run != null)
				{
					StopCoroutine(run);
				}
				chatList.parObj.mouse.SetActive(value: false);
				StartCoroutine(ChatInfo(chatLabelID, target, openSel));
			}
		});
		img_click.onClick.RemoveAllListeners();
		img_click.onClick.AddListener(delegate
		{
			if (!saying)
			{
				if (run != null)
				{
					StopCoroutine(run);
				}
				if (chatList.parObj.mouse != null)
				{
					chatList.parObj.mouse.SetActive(value: false);
				}
				StartCoroutine(ChatInfo(chatLabelID, target, openSel));
			}
		});
	}

	private IEnumerator ShowMouse()
	{
		yield return new WaitForSeconds(5f);
		if (chatList != null && chatList.parObj != null && chatList.parObj.mouse != null)
		{
			chatList.parObj.mouse.SetActive(value: true);
		}
	}

	private IEnumerator ChatInfo(string id, int chatType0, bool select)
	{
		saying = true;
		if (!idList.Contains(id))
		{
			idList.Add(id);
		}
		_ = gameManager.dataManager.dic24[id].judeg;
		if (!select)
		{
			yield return new WaitForSeconds(1f);
			switch (chatType0)
			{
			case 0:
				Show24Label(id, 0, isrecord: true);
				yield return new WaitForSeconds(0.08f);
				chatList.parObj.LineToBottom(chatRoom);
				yield return new WaitForSeconds(0.1f);
				break;
			case 1:
				Show24Label(id, 1, isrecord: false);
				yield return new WaitForSeconds(0.1f);
				chatList.parObj.LineToBottom(chatRoom);
				yield return new WaitForSeconds(0.1f);
				break;
			}
			switch (gameManager.dataManager.dic24[id].replyType)
			{
			case 0:
				if (run != null)
				{
					StopCoroutine(run);
				}
				run = StartCoroutine(ShowMouse());
				openSel = true;
				saying = false;
				break;
			case 1:
			{
				string replyInfo = (chatLabelID = gameManager.dataManager.dic24[id].replyBtn.Substring(1));
				target = 1;
				openSel = false;
				if (run != null)
				{
					StopCoroutine(run);
				}
				yield return new WaitForSeconds(1.5f);
				StartCoroutine(ChatInfo(replyInfo, 1, select: false));
				break;
			}
			case 2:
			{
				btnTop.gameObject.SetActive(value: true);
				gameManager.CanShowSetting(-1);
				int endType = gameManager.dataManager.dic24[id].EndType;
				chatList.ChatListID = idList.ToArray();
				bool flag = true;
				if (endType == 1)
				{
					yield return new WaitForSeconds(2f);
					flag = false;
					Object.Instantiate(Resources.Load<Transform>("Chat/frd_tishi"), content.transform).Find("Text").GetComponent<I18NText>()
						.updateTranslation2("^frd_underLine");
					SetOnlineType(onLineType: false);
					chatList.parObj.LineToBottom(chatRoom);
				}
				if (endType == 2)
				{
					yield return new WaitForSeconds(2f);
					flag = false;
					Object.Instantiate(Resources.Load<Transform>("Chat/frd_tishi"), content.transform).Find("Text").GetComponent<I18NText>()
						.updateTranslation2("^pull_black");
					SetOnlineType(onLineType: false);
					chatList.parObj.LineToBottom(chatRoom);
				}
				chatList.parObj.ChatOver();
				img_click.GetComponent<Button>().interactable = false;
				if (chatType == "0")
				{
					chatList.parObj.LineToBottom(chatRoom);
					if (SceneManager.GetActiveScene().name == "homecourse")
					{
						gameManager.homeScene.weizhuangDialog.img_dragarea.SetActive(value: true);
					}
				}
				if (!flag)
				{
					yield return new WaitForSeconds(1f);
					if (gameManager.IsAllDlc())
					{
						Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(0, gameManager);
						gameManager.musicManager.ResumeVol();
					}
					else
					{
						gameManager.homeScene.StartVideoDialog("videoDialogtaskfailed", "chat");
					}
					if (gameManager.player.playerdata.camChatInfo.ContainsKey(chatList.GetId))
					{
						gameManager.player.playerdata.camChatInfo.Remove(chatList.GetId);
					}
					else
					{
						Debug.LogError("记录中没有");
					}
				}
				else
				{
					gameManager.player.playerdata.AddCamChatInfo(chatList.GetId, idList);
					if (chatList.GetId == "31000")
					{
						DLCEventManager.Instance.NoticeAITalk("3910002");
					}
					else if (chatList.GetId == "31004")
					{
						DLCEventManager.Instance.NoticeAITalk("3910022");
					}
					Debug.Log("套话成功：" + chatList.GetId);
				}
				saying = false;
				break;
			}
			case 3:
			{
				if (run != null)
				{
					StopCoroutine(run);
				}
				run = StartCoroutine(ShowMouse());
				string text = gameManager.dataManager.dic24[id].replyBtn.Substring(1);
				chatLabelID = text;
				target = 0;
				openSel = false;
				saying = false;
				break;
			}
			}
		}
		else
		{
			string[] anwser = GetAnwser(id);
			selectCanvas.gameObject.SetActive(value: true);
			selectCanvas.GetComponent<SelectGroup>().SetSelect(anwser, ClickSelect);
		}
	}

	public void ClickSelect(int poss)
	{
		if (!canSel)
		{
			return;
		}
		canSel = false;
		run = StartCoroutine(ShowMouse());
		string text = reply[poss];
		if (text.Equals("2400060"))
		{
			OpenAchi(0);
		}
		else if (text.Equals("2400061"))
		{
			OpenAchi(1);
		}
		else if (text.Equals("2400062"))
		{
			OpenAchi(2);
		}
		chatLabelID = text;
		target = 0;
		openSel = false;
		StartCoroutine(ChatInfo(chatLabelID, target, openSel));
		selectCanvas.GetComponent<SelectGroup>().HideSelect();
		if (chatType == "0")
		{
			int trustType = gameManager.dataManager.dic24[text].TrustType;
			chatTrustVal += trustType;
			if (trustType == 0)
			{
				StartCoroutine(ValDown(text));
			}
			else
			{
				StartCoroutine(ValUp(text));
			}
		}
		Invoke("CanSel", 1f);
	}

	private void OpenAchi(int type)
	{
		if (!gameManager.issteam || !(gameManager.steamAchi != null))
		{
			return;
		}
		int stat = gameManager.steamAchi.GetStat("event_flower");
		string text = $"{stat:000}";
		string s = "000";
		if (text.Length == 3)
		{
			if (!text.Substring(0, 1).Equals("0") && !text.Substring(0, 1).Equals("1"))
			{
				text = "1" + text.Substring(1, 2);
			}
			if (!text.Substring(1, 1).Equals("0") && !text.Substring(1, 1).Equals("1"))
			{
				text = text.Substring(0, 1) + "1" + text.Substring(2, 1);
			}
			if (!text.Substring(2, 1).Equals("0") && !text.Substring(2, 1).Equals("1"))
			{
				text = text.Substring(0, 2) + "1";
			}
		}
		MonoBehaviour.print("桌上的花");
		switch (type)
		{
		case 0:
			s = text.Substring(0, 2) + "1";
			break;
		case 1:
			s = text.Substring(0, 1) + "1" + text.Substring(2, 1);
			break;
		case 2:
			s = "1" + text.Substring(1, 2);
			break;
		}
		gameManager.steamAchi.SetGlobalStat("event_flower", int.Parse(s), "deskflower");
	}

	private void CanSel()
	{
		canSel = true;
	}

	private IEnumerator ValUp(string id)
	{
		string judeg = gameManager.dataManager.dic24[id].judeg;
		up.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(judeg);
		up.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		up.SetActive(value: false);
	}

	private IEnumerator ValDown(string id)
	{
		string judeg = gameManager.dataManager.dic24[id].judeg;
		down.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(judeg);
		down.SetActive(value: true);
		down.GetComponent<Animator>().Play("ani_trastDown");
		yield return new WaitForSeconds(2f);
		down.SetActive(value: false);
	}

	private void Show24Label(string id, int ifChat, bool isrecord)
	{
		string frdreply = gameManager.dataManager.dic24[id].frdreply;
		GameObject gameObject = null;
		int chat = 0;
		if (frdreply.Trim() == "")
		{
			gameObject = Object.Instantiate(Resources.Load<GameObject>("Chat/chat_itemBak"), content.transform);
		}
		else
		{
			chat = 1;
			gameObject = Object.Instantiate(Resources.Load<GameObject>("Chat/chat_item"), content.transform);
		}
		gameObject.GetComponent<ChatLabelInfo>().Init(id, chatType, chatList.parObj, chatList, gameManager, chat, isrecord, islast: false, ifChat);
		chatList.parObj.LineToBottom(chatRoom);
	}

	public void SetTitle(string listID, int onLineType = -1)
	{
		string text = "";
		text = (gameManager.IsBasic() ? I18N.instance.getValue(gameManager.dataManager.dic23[listID].personnikename) : ((_data3 != null) ? I18N.instance.getValue(_data3.target) : I18N.instance.getValue(gameManager.dataManager.dic23[listID].personnikename)));
		chatName.GetComponent<I18NText>().updateTranslation2(text);
		int num;
		switch (onLineType)
		{
		default:
			num = 1;
			break;
		case 0:
			num = 0;
			break;
		case -1:
			return;
		}
		bool onlineType = (byte)num != 0;
		SetOnlineType(onlineType);
	}

	public void SetOnlineType(bool onLineType)
	{
		onlineImg.sprite = (onLineType ? onLine : underLine);
	}

	public void NeedScreen(string screenImgID, ChatBox chatbox)
	{
		canScreen = true;
		screenID = screenImgID;
	}

	private IEnumerator StartScreen()
	{
		screenFloat.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.2f);
		screenFloat.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.3f);
		Sprite sprite = ((screenImg.sprite == null) ? Resources.Load<Sprite>("Image/" + gameManager.dataManager.dic1[screenID].image) : screenImg.sprite);
		screenImg.sprite = sprite;
		screenImg.SetNativeSize();
		screenImg.gameObject.SetActive(value: true);
		screenImg.transform.DOScaleX(1f, 0.2f);
		screenImg.transform.DOScaleY(1f, 0.2f);
		yield return new WaitForSeconds(2f);
		screenImg.transform.DOScaleX(0f, 0.2f);
		screenImg.transform.DOScaleY(0f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		screenImg.gameObject.SetActive(value: false);
		gameManager.homeScene.notebook.AddNewItem(screenID);
	}

	private string[] GetAnwser(string crtChatID)
	{
		reply = gameManager.dataManager.dic24[crtChatID].replyBtn.Substring(1).Split(';');
		string[] array = new string[reply.Length];
		for (int i = 0; i < reply.Length; i++)
		{
			array[i] = gameManager.dataManager.dic24[reply[i]].title;
		}
		return array;
	}
}
