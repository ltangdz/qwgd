using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ChatLabelInfo : MonoBehaviour
{
	public GameObject largeDialog;

	public Transform contentGroup;

	public MultiplyText txt_newcontent;

	public Text txtcontent0;

	public GameObject imgChatInfo;

	public GameObject biaoqing;

	public GameObject img;

	public GameObject prefab;

	public Image headAvatar;

	public Text textName;

	public Text _isGetText;

	public Text _moneyText;

	public GameObject _redbag;

	public GameObject _chatInfo;

	private ChatBox chatBox;

	private GameManager gameManager;

	[SerializeField]
	private string chatID;

	private string chatType;

	private int chatPsn;

	private ChatList chatList;

	public float limitwidth;

	private DATA22 _data22;

	private DATA24 _data24;

	public bool isrecord;

	public void Init(string id, string type, ChatBox par, ChatList parList, GameManager gm, int chat, bool isrecord, bool islast, int isChat = 0)
	{
		this.isrecord = isrecord;
		chatPsn = chat;
		chatBox = par;
		gameManager = gm;
		chatType = type;
		chatID = id;
		chatList = parList;
		if (type == "1")
		{
			_data22 = gameManager.dataManager.dic22[id];
		}
		else
		{
			_data24 = gameManager.dataManager.dic24[id];
			_isGetText.gameObject.SetActive(value: false);
			_redbag.gameObject.SetActive(value: false);
		}
		_redbag.GetComponent<Button>().onClick.AddListener(delegate
		{
			chatBox.ClickRedBag();
		});
		string chatHeadName = ((type == "1") ? gameManager.dataManager.dic22[id].personavatar : gameManager.dataManager.dic24[id].personavatar);
		string userName = ((type == "1") ? gameManager.dataManager.dic22[id].person : gameManager.dataManager.dic24[id].personnikename);
		if (id == "2410022" || gameManager.Is_Dlc7())
		{
			isChat = 0;
		}
		ResetChatInfo(chatHeadName, userName);
		if (isChat == 0)
		{
			switch (type)
			{
			case "0":
			case "2":
				Show24Info(id, chat, isrecord);
				break;
			case "1":
				Show22Info(id, islast);
				break;
			}
		}
	}

	public void ShowReply()
	{
		if (chatType != "1")
		{
			Show24Info(chatID, chatPsn, isrecord);
		}
	}

	private void Show22Info(string id, bool islast)
	{
		base.transform.Find("loading").gameObject.SetActive(value: false);
		imgChatInfo.gameObject.SetActive(value: true);
		string personavatar = gameManager.dataManager.dic22[id].personavatar;
		string person = gameManager.dataManager.dic22[id].person;
		ResetChatInfo(personavatar, person);
		int type = gameManager.dataManager.dic22[id].type;
		string content = gameManager.dataManager.dic22[id].content;
		switch (type)
		{
		case 0:
			SetLabel(content, isrecord: true, islast);
			break;
		case 1:
			SetImg(content);
			break;
		case 2:
			SetBiaoqing(content);
			break;
		case 4:
			SetRedbag(content);
			break;
		}
	}

	private void Show24Info(string id, int chat, bool isrecord)
	{
		base.transform.Find("loading").gameObject.SetActive(value: false);
		imgChatInfo.gameObject.SetActive(value: true);
		string infoType = gameManager.dataManager.dic24[id].infoType;
		string text = ((chat == 0) ? gameManager.dataManager.dic24[id].content : gameManager.dataManager.dic24[id].frdreply);
		switch (infoType)
		{
		case "0.0":
			SetLabel(text, isrecord);
			break;
		case "1.0":
			SetImg(text);
			break;
		case "2.0":
			SetBiaoqing(text);
			break;
		case "3.0":
			if (!gameManager.IsBasic())
			{
				text = gameManager.dataManager.dic24[id].yuzhijianname;
			}
			SetPrefab(id, text);
			break;
		}
	}

	private void SetLabel(string content, bool isrecord, bool islast = false)
	{
		ShowBox("txt_chatInfo0");
		string text = "";
		text = ((!(chatType == "1")) ? gameManager.dataManager.dic24[chatID].collectID : gameManager.dataManager.dic22[chatID].chatTask);
		if (!text.Equals("#0"))
		{
			imgChatInfo.gameObject.SetActive(value: true);
			contentGroup.gameObject.SetActive(value: true);
			txt_newcontent.gameObject.SetActive(value: true);
			txtcontent0.gameObject.SetActive(value: false);
			_ = gameManager.dataManager.dic1[text.Substring(1)];
			if (gameManager.player.GetEventId().Equals("110000"))
			{
				txt_newcontent.SetIscanaddtoItem(isrecord);
			}
			txt_newcontent.AudoWidth(limitwidth, I18N.instance.getValue(content));
			if (chatType != "1")
			{
				txt_newcontent.SetContent2(content, text.Substring(1), I18N.instance.getValue(gameManager.dataManager.dic24[chatID].highlight));
			}
			else
			{
				txt_newcontent.SetContent2(content, text.Substring(1), I18N.instance.getValue(gameManager.dataManager.dic22[chatID].highlight));
			}
			if (!chatBox.multiplytestlist.Contains(txt_newcontent))
			{
				chatBox.multiplytestlist.Add(txt_newcontent);
			}
		}
		else
		{
			if (contentGroup != null)
			{
				contentGroup.gameObject.SetActive(value: false);
			}
			float num = CalculateLengthOfText(I18N.instance.getValue(content));
			if (num > limitwidth)
			{
				txtcontent0.GetComponent<LayoutElement>().enabled = true;
				txtcontent0.rectTransform.sizeDelta = new Vector2(num, txtcontent0.rectTransform.sizeDelta.y);
			}
			txtcontent0.GetComponent<I18NText>().updateTranslation2(content);
		}
		chatList.parObj.LineToBottom(chatList.chatContent.GetComponent<ChatRoom>().chatRoom);
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txtcontent0.font;
		font.RequestCharactersInTexture(message, txtcontent0.fontSize, txtcontent0.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txtcontent0.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	public void SetImg(string content)
	{
		if (content == null || content.Equals(""))
		{
			Debug.Log("tb图片为空");
			return;
		}
		GetComponent<HorizontalLayoutGroup>().padding.left = (base.gameObject.name.Contains("Bak") ? 50 : 5);
		ShowBox("yuzhijian");
		for (int i = 0; i < img.transform.childCount; i++)
		{
			Object.Destroy(img.transform.GetChild(i).gameObject);
		}
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Image/" + content), img.transform);
		gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
		if (gameManager.Is_Dlc6() && content == "_dlc6_010")
		{
			gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("Image/_dlc6_010open"), gameManager.homeScene.middle);
				gameManager.UnlockAchievements("dejavu");
				gameObject2.transform.DOLocalMove(Vector3.zero, 0.3f);
				if (gameObject2.GetComponent<ReasonPic>() != null)
				{
					gameObject2.GetComponent<ReasonPic>().Show();
				}
				Debug.Log("点击照片");
			});
		}
		if (chatType == "1")
		{
			_ = gameManager.dataManager.dic22[chatID].chatTask;
		}
		else
		{
			_ = gameManager.dataManager.dic24[chatID].collectID;
		}
		chatList.parObj.LineToBottom(chatList.chatContent.GetComponent<ChatRoom>().chatRoom);
	}

	public void SetBiaoqing(string imgUrl)
	{
		ShowBox("biaoqing");
		biaoqing.GetComponent<Image>().sprite = Resources.Load<Sprite>("Biaoqing/" + imgUrl);
		biaoqing.GetComponent<Image>().SetNativeSize();
		chatList.parObj.LineToBottom(chatList.chatContent.GetComponent<ChatRoom>().chatRoom);
	}

	public void SetRedbag(string imgUrl)
	{
		ShowBox("redbag");
	}

	public void SetPrefab(string id, string prefaburl)
	{
		ShowBox("prefab");
		for (int i = 0; i < prefab.transform.childCount; i++)
		{
			Object.Destroy(prefab.transform.GetChild(i).gameObject);
		}
		prefab.GetComponent<Button>().onClick.RemoveAllListeners();
		if (gameManager.Is_Dlc7())
		{
			Object.Instantiate(Resources.Load<GameObject>("Chat/" + prefaburl), prefab.transform);
		}
		else
		{
			Object.Instantiate(Resources.Load<GameObject>("Image/" + prefaburl), prefab.transform);
			prefab.GetComponent<Button>().onClick.AddListener(delegate
			{
				string yuzhijianname = gameManager.dataManager.dic24[id].yuzhijianname;
				Object.Instantiate(Resources.Load<GameObject>("Chat/" + yuzhijianname), gameManager.homeScene.middle);
			});
		}
		chatList.parObj.LineToBottom(chatList.chatContent.GetComponent<ChatRoom>().chatRoom);
	}

	private void ResetChatInfo(string chatHeadName, string userName)
	{
		chatHeadName = chatHeadName.Replace(".0", "");
		headAvatar.sprite = Resources.Load<Sprite>("touxiang/" + chatHeadName);
		textName.GetComponent<I18NText>().updateTranslation2(userName);
	}

	private void Enlarge(string name, string taskID)
	{
		Transform transform = Object.Instantiate(Resources.Load<GameObject>("Dialog/pic"), gameManager.homeScene.transform.Find("Panel/middle")).GetComponent<EnlargeImg>().group.transform;
		Object.Instantiate(Resources.Load<GameObject>("Chat/" + name), transform);
		transform.GetComponent<HighLightPic>().itemid = taskID;
	}

	public void ShowBox(string name)
	{
		if (_data22 != null)
		{
			if (_data22.type == 4)
			{
				_redbag.gameObject.SetActive(value: true);
				_moneyText.text = _data22.money.Substring(1);
				imgChatInfo.SetActive(value: false);
				_chatInfo.SetActive(value: false);
			}
			else
			{
				_isGetText.gameObject.SetActive(value: false);
				_redbag.gameObject.SetActive(value: false);
				imgChatInfo.SetActive(value: true);
				_chatInfo.SetActive(value: true);
			}
			if (_data22.is_get.Substring(1) == "0")
			{
				_isGetText.gameObject.SetActive(value: false);
			}
			else
			{
				_isGetText.gameObject.SetActive(value: true);
			}
		}
		else
		{
			_isGetText.gameObject.SetActive(value: false);
			_redbag.gameObject.SetActive(value: false);
			imgChatInfo.SetActive(value: true);
			_chatInfo.SetActive(value: true);
		}
		imgChatInfo.SetActive(value: true);
		float num = imgChatInfo.transform.childCount;
		for (int i = 0; (float)i < num; i++)
		{
			if (imgChatInfo.transform.GetChild(i).name != name)
			{
				imgChatInfo.transform.GetChild(i).gameObject.SetActive(value: false);
			}
			else
			{
				imgChatInfo.transform.GetChild(i).gameObject.SetActive(value: true);
			}
		}
	}
}
