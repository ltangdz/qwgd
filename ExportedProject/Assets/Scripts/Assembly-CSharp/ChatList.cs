using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ChatList : MonoBehaviour
{
	public GameObject chatContent;

	public Image avatar;

	public Text userName;

	public Text chatLastLabel;

	public Text chatTime;

	public Image border;

	[HideInInspector]
	public ChatBox parObj;

	[SerializeField]
	private Image img_sex;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	private int sex;

	[SerializeField]
	private Color deepgray;

	[SerializeField]
	private Color lightgray;

	public string ID;

	private string[] chatListid = new string[0];

	private GameManager gameManager;

	private string chatType;

	private DATA3 _data3;

	public string[] ChatListID
	{
		get
		{
			return chatListid;
		}
		set
		{
			chatListid = value;
		}
	}

	public string GetId => ID;

	public GameManager GameManager
	{
		get
		{
			if (gameManager == null)
			{
				gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return gameManager;
		}
	}

	public void Init(string userID, ChatBox par, GameManager gm, string type, string chatID = "", DATA3 data3 = null)
	{
		Debug.Log("ChatListInit--userID:" + userID + "----type：" + type + "----chatID:" + chatID);
		parObj = par;
		gameManager = gm;
		chatType = type;
		_data3 = data3;
		chatContent = Object.Instantiate(Resources.Load<GameObject>("Chat/chat_room"), par.frdChatListContent);
		ID = userID;
		string text = "";
		string text2 = "";
		if (gameManager.IsBasic() || type == "1")
		{
			text = gameManager.dataManager.dic23[userID].personavatar;
			text = text.Replace(".0", "");
			text2 = gameManager.dataManager.dic23[userID].personnikename;
			sex = gameManager.dataManager.dic23[userID].sexuality;
		}
		else
		{
			text = data3.targetAvatar;
			text = text.Replace(".0", "");
			text2 = data3.target;
			if (ID == "31004")
			{
				sex = 1;
			}
		}
		avatar.sprite = Resources.Load<Sprite>("touxiang/" + text);
		userName.GetComponent<I18NText>().updateTranslation2(text2);
		if (sex != 2)
		{
			img_sex.gameObject.SetActive(value: true);
			img_sex.sprite = sprites[sex];
		}
		if (chatID.Trim() == "")
		{
			string text3 = "";
			switch (type)
			{
			case "0":
				chatListid = gameManager.player.playerdata.camChatInfo[userID].ToArray();
				text3 = gameManager.dataManager.dic24[chatListid[chatListid.Length - 1]].infoType;
				break;
			case "1":
				chatListid = gameManager.dataManager.dic23[userID].person.Substring(1).Split(';');
				text3 = gameManager.dataManager.dic22[chatListid[chatListid.Length - 1]].type.ToString();
				break;
			case "2":
				chatListid = gameManager.player.playerdata.mainChatInfo[userID].ToArray();
				text3 = gameManager.dataManager.dic24[chatListid[chatListid.Length - 1]].infoType;
				break;
			}
			switch (text3)
			{
			case "0":
			case "0.0":
			{
				string text4 = "";
				if (chatType == "1")
				{
					text4 = gameManager.dataManager.dic22[chatListid[chatListid.Length - 1]].content;
				}
				else
				{
					text4 = gameManager.dataManager.dic24[chatListid[chatListid.Length - 1]].frdreply;
					text4 = ((text4.Trim() == "") ? gameManager.dataManager.dic24[chatListid[chatListid.Length - 1]].content : text4);
				}
				chatLastLabel.GetComponent<I18NText>().updateTranslation2(text4);
				break;
			}
			case "1":
			case "1.0":
				chatLastLabel.GetComponent<I18NText>().updateTranslation2("[" + I18N.instance.getValue("^chat_img") + "]");
				break;
			case "2":
			case "2.0":
				chatLastLabel.GetComponent<I18NText>().updateTranslation2("[" + I18N.instance.getValue("^chat_biaoqing") + "]");
				break;
			case "-1":
			case "-1.0":
				chatLastLabel.GetComponent<Text>().text = "";
				break;
			}
			if (chatType == "1")
			{
				chatTime.GetComponent<I18NText>().updateTranslation2(gameManager.dataManager.dic22[chatListid[chatListid.Length - 1]].title);
			}
		}
		ChatList self = this;
		GetComponent<Button>().onClick.AddListener(delegate
		{
			ClickChatListExtra();
			if (parObj.chatOver)
			{
				float num = par.frdChatListContent.childCount;
				for (int i = 0; (float)i < num; i++)
				{
					par.frdListContent.GetChild(i).GetComponent<ChatList>().Blur();
				}
				Focus();
				if (!chatContent.GetComponent<ChatRoom>().haveVal)
				{
					par.SearchRecord(self);
					chatContent.GetComponent<ChatRoom>().haveVal = true;
				}
				else
				{
					chatContent.transform.SetAsLastSibling();
				}
				if (ID.Equals("2300087"))
				{
					gameManager.player.playerdata.islookcio2300087 = true;
				}
				else if (ID.Equals("2300088"))
				{
					gameManager.player.playerdata.islookcio2300088 = true;
				}
				else if (ID.Equals("2300089"))
				{
					gameManager.player.playerdata.islookcio2300089 = true;
				}
				if (gameManager.player.playerdata.islookcio2300087 && gameManager.player.playerdata.islookcio2300088 && gameManager.player.playerdata.islookcio2300089)
				{
					gameManager.UnlockAchievements("ciosecrect");
				}
			}
		});
		if (chatID.Trim() != "")
		{
			Debug.Log("套话ID：" + chatID);
			if (!gameManager.IsBasic() && data3 != null)
			{
				chatID = data3.reply.Substring(1);
			}
			chatContent.GetComponent<ChatRoom>().StartChat(this, type, chatID, gameManager, _data3);
		}
	}

	private void ClickChatListExtra()
	{
	}

	public void Focus()
	{
		border.GetComponent<CanvasGroup>().alpha = 1f;
		if (sex != 2)
		{
			img_sex.sprite = sprites[sex + 2];
		}
		userName.color = Color.white;
		chatLastLabel.color = Color.white;
		chatTime.color = Color.white;
	}

	public void Blur()
	{
		border.GetComponent<CanvasGroup>().alpha = 0f;
		if (sex != 2)
		{
			img_sex.sprite = sprites[sex];
		}
		userName.color = deepgray;
		chatLastLabel.color = lightgray;
		chatTime.color = lightgray;
	}
}
