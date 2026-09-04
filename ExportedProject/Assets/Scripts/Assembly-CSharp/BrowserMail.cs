using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class BrowserMail : CustomDialog
{
	public Transform btnGroup;

	public Transform btnGroup2;

	public Transform contentList;

	public Transform mailInfoBox;

	public Transform mailBox;

	public Transform mailListName;

	public Transform contentBox;

	public Transform writeBox;

	public Button accountBtn;

	public Transform accountTitle;

	public Transform passwordBox;

	public Transform userNameBox;

	[HideInInspector]
	public int mailReadType;

	private List<DATA15> getBox = new List<DATA15>();

	private List<DATA15> sendBox = new List<DATA15>();

	private List<DATA15> delBox = new List<DATA15>();

	private DataManager dataManager;

	private List<string> mailId;

	private string userMail = "";

	public Transform focus;

	private bool sub;

	public bool islogin;

	public GameObject imgDragArea;

	public string UserMail
	{
		get
		{
			return userMail;
		}
		set
		{
			userMail = value;
		}
	}

	private void Start()
	{
		gameManager.homeScene.browserMail = this;
		gameManager.homeScene.mailTip.HideMail();
		gameManager.homeScene.computerButtonBox.mailDialog = base.gameObject;
		string key = ((userMail == "admin") ? "admin" : (gameManager.GetData14Prefix() + userMail));
		string[] array = gameManager.dataManager.dic14_userid[key].discussid.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].Equals("0"))
			{
				bool flag = gameManager.player.playerdata.unReadMailIds.Contains(array[i]);
				gameManager.player.SendMail(userMail, array[i], (!flag) ? 2 : 0, issave: false);
			}
		}
		accountTitle.Find("mail_username").GetComponent<Text>().GetComponent<I18NText>()
			.updateTranslation2(userMail.Equals("admin") ? gameManager.player.playerdata.nickname : I18N.instance.getValue(gameManager.dataManager.dic14_userid[key].nickname));
		mailId = gameManager.player.playerdata.MailKeylist(userMail);
		for (int j = 0; j < mailId.Count; j++)
		{
			if (!(mailId[j] == "1510055"))
			{
				getBox.Add(gameManager.dataManager.dic15[mailId[j]]);
			}
		}
		for (int k = 0; k < mailId.Count; k++)
		{
			if (mailId[k] == "1510055")
			{
				delBox.Add(gameManager.dataManager.dic15[mailId[k]]);
			}
		}
		DATA14 dATA = gameManager.dataManager.dic14_userid[key];
		if (dATA.inbox.Trim() != "")
		{
			string[] array2 = dATA.inbox.Substring(1).Split(';');
			for (int l = 0; l < array2.Length; l++)
			{
				if (!(array2[l] != "0"))
				{
					continue;
				}
				DATA15 dATA2 = gameManager.dataManager.dic15[array2[l]];
				if (UserMail == "leeX@uu.com")
				{
					List<string> itemlist = gameManager.player.playerdata.itemlist;
					bool flag2 = itemlist.Contains("11131") && itemlist.Contains("11172") && itemlist.Contains("11171") && itemlist.Contains("11173") && itemlist.Contains("11206") && itemlist.Contains("11183");
					if ((dATA2.ID == 1510021 && !flag2) || (!gameManager.player.playerdata.reasoninglist.Contains("4011") && (dATA2.ID == 1510007 || dATA2.ID == 1510021)) || (!gameManager.player.playerdata.reasoninglist.Contains("4012") && dATA2.ID == 1510021))
					{
						continue;
					}
				}
				sendBox.Add(dATA2);
			}
		}
		for (int m = 0; m < btnGroup.childCount; m++)
		{
			List<DATA15> list = new List<DATA15>();
			string key2 = "";
			switch (m)
			{
			case 1:
				list = getBox;
				key2 = "^tab_inBox";
				break;
			case 2:
				list = sendBox;
				key2 = "^tab_sended";
				break;
			case 3:
				list = delBox;
				key2 = "^tab_dustbin";
				break;
			}
			if (m != 0)
			{
				btnGroup.GetChild(m).GetComponent<MailBtn>().AddMailData(list);
				if (list.Count > 0)
				{
					btnGroup.GetChild(m).Find("txt_inbox").GetComponent<I18NText>()
						.updateTranslation6(I18N.instance.getValue(key2) + "(" + list.Count + ")");
				}
			}
		}
		int num = gameManager.player.playerdata.NoReadMail(userMail);
		if (num > 0)
		{
			btnGroup.GetChild(1).Find("have_newMail").gameObject.SetActive(value: true);
			btnGroup.GetChild(1).Find("have_newMail/new_mailNum").GetComponent<I18NText>()
				.updateTranslation2(num.ToString());
		}
		else
		{
			btnGroup.GetChild(1).Find("have_newMail").gameObject.SetActive(value: false);
			btnGroup.GetChild(1).Find("have_newMail/new_mailNum").GetComponent<I18NText>()
				.updateTranslation2("0");
			gameManager.homeScene.computerButtonBox.btn_mail.ShowRed(isshow: false);
		}
		for (int n = 0; n < btnGroup.childCount; n++)
		{
			int s = n;
			Transform btn = btnGroup.GetChild(n);
			btn.GetComponent<Button>().onClick.AddListener(delegate
			{
				ChoiceMailGroup(btn, s);
			});
		}
		ChoiceMailGroup(btnGroup.GetChild(1), 1);
		accountBtn.onClick.AddListener(ChangeAccount);
		if (islogin)
		{
			ChangeAccount();
		}
		btn_close.onClick.AddListener(delegate
		{
			gameManager.homeScene.mailTip.userid = "admin";
		});
		if (gameManager.player.playerdata.isCourse14 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel14.maildialog = base.gameObject;
			gameManager.homeScene.courseManager.ShowCourse14();
		}
	}

	public void Login(string userName, string password)
	{
		btnGroup2.gameObject.SetActive(value: true);
		mailBox.gameObject.SetActive(value: true);
		UserMail = userName;
		string key = ((userName == "admin") ? "admin" : (gameManager.GetData14Prefix() + userName));
		gameManager.player.playerdata.AddHaveLogedMail(userName, password);
		accountTitle.Find("mail_username").GetComponent<Text>().GetComponent<I18NText>()
			.updateTranslation2(userMail.Equals("admin") ? gameManager.player.playerdata.nickname : I18N.instance.getValue(gameManager.dataManager.dic14_userid[key].nickname));
		accountTitle.gameObject.SetActive(value: true);
		if (!gameManager.dataManager.dic14_userid[key].missionID.Equals("") && gameManager.dataManager.dic14_userid[key].missionID != null)
		{
			gameManager.homeScene.goalDialog.CompleteItem(gameManager.dataManager.dic14_userid[key].missionID.Substring(1));
		}
		gameManager.homeScene.mailTip.userid = userName;
	}

	public void Refresh(bool closepanel = true)
	{
		base.transform.SetAsLastSibling();
		mailId = gameManager.player.playerdata.MailKeylist(userMail);
		getBox.Clear();
		sendBox.Clear();
		delBox.Clear();
		for (int i = 0; i < mailId.Count; i++)
		{
			if (!(mailId[i] == "1510055"))
			{
				getBox.Add(gameManager.dataManager.dic15[mailId[i]]);
			}
		}
		for (int j = 0; j < mailId.Count; j++)
		{
			if (mailId[j] == "1510055")
			{
				delBox.Add(gameManager.dataManager.dic15[mailId[j]]);
			}
		}
		string key = ((userMail == "admin") ? "admin" : (gameManager.GetData14Prefix() + userMail));
		string[] array = gameManager.dataManager.dic14_userid[key].inbox.Substring(1).Split(';');
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k] != "0")
			{
				sendBox.Add(gameManager.dataManager.dic15[array[k]]);
			}
		}
		for (int l = 0; l < btnGroup.childCount; l++)
		{
			List<DATA15> list = new List<DATA15>();
			string key2 = "";
			switch (l)
			{
			case 1:
				list = getBox;
				key2 = "^tab_inBox";
				break;
			case 2:
				list = sendBox;
				key2 = "^tab_sended";
				break;
			case 3:
				list = delBox;
				key2 = "^tab_dustbin";
				break;
			}
			if (l != 0)
			{
				btnGroup.GetChild(l).GetComponent<MailBtn>().AddMailData(list);
				if (list.Count > 0)
				{
					btnGroup.GetChild(l).Find("txt_inbox").GetComponent<I18NText>()
						.updateTranslation2(I18N.instance.getValue(key2) + "(" + list.Count + ")");
				}
			}
		}
		int num = gameManager.player.playerdata.NoReadMail(userMail);
		if (num > 0)
		{
			btnGroup.GetChild(1).Find("have_newMail").gameObject.SetActive(value: true);
			btnGroup.GetChild(1).Find("have_newMail/new_mailNum").GetComponent<Text>()
				.GetComponent<I18NText>()
				.updateTranslation2(num.ToString());
		}
		else
		{
			btnGroup.GetChild(1).Find("have_newMail").gameObject.SetActive(value: false);
			btnGroup.GetChild(1).Find("have_newMail/new_mailNum").GetComponent<Text>()
				.GetComponent<I18NText>()
				.updateTranslation2("0");
			gameManager.homeScene.computerButtonBox.btn_mail.ShowRed(isshow: false);
		}
		ChoiceMailGroup(btnGroup.GetChild(1), 1, closepanel);
	}

	public void ChoiceMailGroup(Transform btn, int type, bool autoclosepanel = true)
	{
		mailReadType = type;
		base.transform.SetAsLastSibling();
		if (autoclosepanel)
		{
			mailInfoBox.gameObject.SetActive(value: false);
		}
		for (int i = 0; i < btnGroup.childCount; i++)
		{
			btnGroup.GetChild(i).GetComponent<MailBtn>().ResetList(i);
		}
		for (int j = 0; j < contentList.childCount; j++)
		{
			Object.Destroy(contentList.GetChild(j).gameObject);
		}
		btn.GetComponent<MailBtn>().Focus();
		if (type != 0)
		{
			contentBox.gameObject.SetActive(value: true);
			writeBox.gameObject.SetActive(value: false);
			List<DATA15> mailType = btn.GetComponent<MailBtn>().GetMailList();
			for (int k = 0; k < mailType.Count; k++)
			{
				int s = k;
				Transform list = Object.Instantiate(Resources.Load("mail_list", typeof(Transform)) as Transform, contentList);
				if (type == 1 || type == 3)
				{
					string listId = mailType[k].ID.ToString();
					string value = I18N.instance.getValue(mailType[k].sender);
					string value2 = I18N.instance.getValue(mailType[k].title);
					string sendTime = mailType[k].sendTime;
					int num = gameManager.player.playerdata.maillist[userMail][0][mailType[k].ID.ToString()];
					string text = mailType[k].info.Split(';')[0].Replace("N", "").Replace("L", "");
					string listMailInfo = ((text.IndexOf("^") == -1) ? "^mail_fujian" : I18N.instance.getValue(text));
					list.GetComponent<MailList>().ResetList(listId, value, value2, sendTime, num, listMailInfo, userMail, mailReadType);
					mailListName.GetComponent<Text>().GetComponent<I18NText>().updateTranslation2(mailType[k].geter);
					if (num == 0 || num == 1)
					{
						list.transform.SetAsFirstSibling();
					}
				}
				else
				{
					string listId2 = mailType[k].ID.ToString();
					string value3 = I18N.instance.getValue(mailType[k].geter);
					string value4 = I18N.instance.getValue(mailType[k].title);
					string sendTime2 = mailType[k].sendTime;
					int haveRead = 1;
					string text2 = mailType[k].info.Split(';')[0].Replace("N", "").Replace("L", "");
					string listMailInfo2 = ((text2.IndexOf("^") == -1) ? "^mail_fujian" : I18N.instance.getValue(text2));
					list.GetComponent<MailList>().ResetList(listId2, value3, value4, sendTime2, haveRead, listMailInfo2, userMail, mailReadType);
					mailListName.GetComponent<Text>().GetComponent<I18NText>().updateTranslation2(mailType[k].sender);
				}
				list.GetComponent<Button>().onClick.RemoveAllListeners();
				list.GetComponent<Button>().onClick.AddListener(delegate
				{
					if (mailType[s].ID.ToString() == "1500086" || mailType[s].ID.ToString() == "1500087" || mailType[s].ID.ToString() == "1500088" || mailType[s].ID.ToString() == "1500089")
					{
						gameManager.CrackBoom(mailType[s].ID.ToString());
					}
					ShowMailInfo(list, mailType[s], type);
				});
			}
		}
		else
		{
			mailInfoBox.gameObject.SetActive(value: false);
			contentBox.gameObject.SetActive(value: false);
			writeBox.gameObject.SetActive(value: true);
		}
	}

	private void ShowMailInfo(Transform btn, DATA15 item, int type)
	{
		bool flag = false;
		for (int i = 0; i < contentList.childCount; i++)
		{
			contentList.GetChild(i).GetComponent<MailList>().Blur();
		}
		btn.GetComponent<MailList>().Focus();
		base.transform.SetAsLastSibling();
		if (mailReadType == 1 && gameManager.player.playerdata.maillist[userMail][0][item.ID.ToString()] == 0)
		{
			btn.GetComponent<MailList>().Read();
			if (item.open == 0)
			{
				flag = true;
				gameManager.player.playerdata.maillist[userMail][0][item.ID.ToString()] = 1;
				int num = gameManager.player.playerdata.NoReadMail(userMail);
				btnGroup.Find("btn_inbox/have_newMail/new_mailNum").GetComponent<Text>().GetComponent<I18NText>()
					.updateTranslation2(num.ToString());
				if (num.ToString() == "0")
				{
					btnGroup.Find("btn_inbox/have_newMail").gameObject.SetActive(value: false);
					gameManager.homeScene.computerButtonBox.btn_mail.ShowRed(isshow: false);
				}
			}
		}
		mailInfoBox.GetComponent<MailInfo>().Reset(btn, item, type);
		if (flag)
		{
			Debug.LogError("保存");
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void OpenCodeMail(DATA15 data15)
	{
		gameManager.player.playerdata.maillist[userMail][0][data15.ID.ToString()] = 1;
		int num = gameManager.player.playerdata.NoReadMail(userMail);
		btnGroup.Find("btn_inbox/have_newMail/new_mailNum").GetComponent<Text>().GetComponent<I18NText>()
			.updateTranslation2(num.ToString());
		if (num.ToString() == "0")
		{
			btnGroup.Find("btn_inbox/have_newMail").gameObject.SetActive(value: false);
			gameManager.homeScene.computerButtonBox.btn_mail.ShowRed(isshow: false);
		}
	}

	private void SendMailBtn(int type)
	{
	}

	public void SetItem(GameObject tile, DATA1 data)
	{
		if (tile.name == "password")
		{
			tile.transform.Find("Text").GetComponent<TypewriterEffect>().StartEffect("······");
		}
		else
		{
			tile.transform.Find("Text").GetComponent<TypewriterEffect>().StartEffect(I18N.instance.getValue(data.message));
		}
		tile.transform.Find("placeholder").GetComponent<Text>().GetComponent<I18NText>()
			.updateTranslation2(data.message);
		tile.transform.Find("placeholder").gameObject.SetActive(value: false);
	}

	public void ChangeAccount()
	{
		base.transform.SetAsLastSibling();
		sub = false;
		((GameObject)Object.Instantiate(Resources.Load("Dialog/mailLogin"), gameManager.homeScene.computerButtonBox.dialogtool)).GetComponent<MailLogin>().Show();
		Hide();
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
