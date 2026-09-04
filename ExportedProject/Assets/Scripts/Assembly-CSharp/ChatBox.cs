using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ChatBox : CustomDialog
{
	public Transform frdListContent;

	public Transform frdChatListContent;

	[HideInInspector]
	public bool chatOver = true;

	public List<MultiplyText> multiplytestlist;

	public Image userAvatar;

	public Text userName;

	public Button changeUser;

	public Button noclose;

	public GameObject chatBak;

	public Button hotarea;

	public GameObject mouse;

	private string CrtUserID;

	private string chatType;

	private string ChatFrdID;

	private string frdID;

	private string chatID;

	public GameObject img_dragarea;

	public ToastImage _toastImage;

	private Sequence _sequence;

	private Image _tip;

	private DATA3 _data3;

	private void Start()
	{
		gameManager.homeScene.computerButtonBox.chatDialog = base.gameObject;
		gameManager.saveManager.SavePlayerData();
		changeUser.onClick.AddListener(delegate
		{
			if (chatOver)
			{
				GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Chat/chatLogin"), base.transform.parent);
				obj.transform.parent.gameObject.SetActive(value: true);
				obj.GetComponent<ChatLogin>().Show();
				gameManager.CanShowSetting(-1);
				Hide();
			}
		});
	}

	public void Init(string crtID, string type, string frd = "", string chat = "", DATA3 data3 = null)
	{
		Debug.Log("ChatBoxInit_crtID:" + crtID + "--type:" + type + "--frd:" + frd + "--chat:" + chat);
		CrtUserID = crtID;
		if (CrtUserID == "1400089")
		{
			gameManager.UnlockAchievements("brotherhood");
		}
		chatType = type;
		frdID = frd;
		chatID = chat;
		if (chatType == "1")
		{
			hotarea.gameObject.SetActive(value: false);
		}
		_data3 = data3;
	}

	private void SetFrdList(string[] showID)
	{
		for (int i = 0; i < showID.Length; i++)
		{
			Object.Instantiate(Resources.Load<GameObject>("Chat/chatList"), frdListContent).GetComponent<ChatList>().Init(showID[i], this, gameManager, chatType);
		}
	}

	private void SetFrdListDLC(string[] showID)
	{
		Object.Instantiate(Resources.Load<GameObject>("Chat/chatList"), frdListContent).GetComponent<ChatList>().Init(_data3.ID.ToString(), this, gameManager, chatType, gameManager.player.playerdata.camChatInfo.ContainsKey(_data3.ID.ToString()) ? "" : _data3.ID.ToString(), _data3);
	}

	private void StartChat(string frdID, string chatID)
	{
		chatOver = false;
		for (int i = 0; i < frdListContent.childCount; i++)
		{
			if (frdID == frdListContent.GetChild(i).GetComponent<ChatList>().GetId)
			{
				frdListContent.GetChild(i).SetAsFirstSibling();
				frdListContent.GetChild(i).GetComponent<ChatList>().chatContent.GetComponent<ChatRoom>().StartChat(frdListContent.GetChild(i).GetComponent<ChatList>(), chatType, chatID, gameManager, _data3);
				return;
			}
		}
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Chat/chatList"), frdListContent);
		obj.transform.SetAsFirstSibling();
		obj.GetComponent<ChatList>().Init(frdID, this, gameManager, chatType, chatID);
	}

	public void SearchRecord(ChatList listObj, bool isrecord = false)
	{
		ChatFrdID = listObj.GetId;
		listObj.chatContent.transform.SetAsLastSibling();
		listObj.chatContent.GetComponent<ChatRoom>().Init(listObj, chatType, gameManager, isrecord, _data3);
	}

	public void ChatOver()
	{
		gameManager.istaohuashow = false;
		gameManager.homeScene.ShowNextVideo();
		hotarea.gameObject.SetActive(value: false);
		chatOver = true;
		noclose.gameObject.SetActive(value: false);
		Object.Destroy(chatBak.gameObject);
		img_dragarea.SetActive(value: true);
		changeUser.interactable = true;
		Object.Destroy(GetComponent<GraphicRaycaster>());
		Object.Destroy(GetComponent<GraphicRaycaster>());
		Object.Destroy(GetComponent<Canvas>());
		for (int i = 0; i < multiplytestlist.Count; i++)
		{
			multiplytestlist[i].SetIscanaddtoItem(chatOver);
		}
	}

	public void LineToBottom(ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	public void LineToTop(ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 1f;
		Canvas.ForceUpdateCanvases();
	}

	public override void BeforeShowSize()
	{
	}

	public void ClickRedBag()
	{
		gameManager.player.playerdata.clickRedBagCount++;
		if (gameManager.Is_Dlc6() && gameManager.player.playerdata.clickRedBagCount >= 3)
		{
			gameManager.UnlockAchievements("benefit");
		}
		if (_toastImage == null)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load("_DLC/Prefabs/ToastImage"), base.transform) as GameObject;
			_toastImage = gameObject.GetComponent<ToastImage>();
			_toastImage.InitData(null, null);
		}
		_toastImage.ShowText(I18N.instance.getValue("^7FAB9F3F-19C4-5A31-F04C-CE2255BBAD33"));
	}

	private void ShowTip()
	{
		CanvasGroup component = _tip.GetComponent<CanvasGroup>();
		if (_sequence != null)
		{
			_sequence.Kill();
		}
		_sequence = DOTween.Sequence();
		_sequence.Append(component.DOFade(1f, 0.3f));
		_sequence.AppendInterval(2f);
		_sequence.Append(component.DOFade(0f, 0.3f));
		_sequence.Play();
	}

	public override void AfterShowSize()
	{
		string text = gameManager.dataManager.dic14[CrtUserID].avatar.Replace(".0", "");
		string nickname = gameManager.dataManager.dic14[CrtUserID].nickname;
		Debug.Log("AfterShowSize111111");
		userAvatar.sprite = Resources.Load<Sprite>("touxiang/" + text);
		userName.GetComponent<I18NText>().updateTranslation2(nickname);
		switch (chatType)
		{
		case "0":
		{
			Debug.Log("AfterShowSize222");
			img_dragarea.SetActive(value: false);
			Dictionary<string, List<string>> camChatInfo = gameManager.player.playerdata.camChatInfo;
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<string, List<string>> item in camChatInfo)
			{
				list2.Add(item.Key);
			}
			string[] array = list2.ToArray();
			if (gameManager.IsBasic())
			{
				SetFrdList(array);
			}
			else
			{
				SetFrdListDLC(array);
			}
			Debug.Log("AfterShowSiz33333");
			if (frdID.Trim() != "" && chatID.Trim() != "")
			{
				if (!gameManager.player.playerdata.camChatInfo.ContainsKey(frdID))
				{
					gameManager.CanShowSetting(1);
					gameManager.istaohuashow = true;
					chatBak.SetActive(value: true);
					chatBak.GetComponent<ChatBak>().ShowCourse();
					StartChat(frdID, chatID);
					Debug.Log("AfterShowSiz44444");
				}
				else
				{
					ChatOver();
				}
			}
			else
			{
				ChatOver();
			}
			if (frdListContent.childCount > 0)
			{
				frdListContent.GetChild(0).GetComponent<ChatList>().Focus();
				SearchRecord(frdListContent.GetChild(0).GetComponent<ChatList>(), isrecord: true);
			}
			break;
		}
		case "1":
		{
			string[] frdList2 = gameManager.dataManager.dic14[CrtUserID].discussid.Substring(1).Split(';');
			SetFrdList(frdList2);
			frdListContent.GetChild(0).GetComponent<ChatList>().Focus();
			if (frdListContent.GetChild(0).GetComponent<ChatList>().ID.Equals("2300087"))
			{
				gameManager.player.playerdata.islookcio2300087 = true;
			}
			else if (frdListContent.GetChild(0).GetComponent<ChatList>().ID.Equals("2300088"))
			{
				gameManager.player.playerdata.islookcio2300088 = true;
			}
			else if (frdListContent.GetChild(0).GetComponent<ChatList>().ID.Equals("2300089"))
			{
				gameManager.player.playerdata.islookcio2300089 = true;
			}
			if (gameManager.player.playerdata.islookcio2300087 && gameManager.player.playerdata.islookcio2300088 && gameManager.player.playerdata.islookcio2300089)
			{
				gameManager.UnlockAchievements("ciosecrect");
			}
			SearchRecord(frdListContent.GetChild(0).GetComponent<ChatList>());
			ChatOver();
			break;
		}
		case "2":
		{
			Dictionary<string, List<string>> mainChatInfo = gameManager.player.playerdata.mainChatInfo;
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, List<string>> item2 in mainChatInfo)
			{
				list.Add(item2.Key);
			}
			string[] frdList = list.ToArray();
			SetFrdList(frdList);
			if (frdListContent.childCount > 0)
			{
				frdListContent.GetChild(0).GetComponent<ChatList>().Focus();
				SearchRecord(frdListContent.GetChild(0).GetComponent<ChatList>());
			}
			if (frdID.Trim() != "" && chatID.Trim() != "")
			{
				chatBak.SetActive(value: true);
				chatBak.GetComponent<ChatBak>().ShowCourse();
				StartChat(frdID, chatID);
			}
			else
			{
				ChatOver();
			}
			break;
		}
		}
	}
}
