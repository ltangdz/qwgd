using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weizhuang : CustomDialog
{
	public GameObject personParent;

	public Button submitBtn;

	[HideInInspector]
	public string choiceCamID;

	private string eventID;

	private Dictionary<string, Dictionary<string, string>> lying;

	private DataManager dataManager;

	private string[] username;

	private string[] fakeName;

	private string[] lyingChat;

	private string[] fakePhoto;

	private string[] photo;

	private string[] task;

	private string[] camdes;

	public Sprite notingSprite;

	public GameObject camTip;

	public GameObject titleObj;

	public GameObject noobject;

	public GameObject imgDragArea;

	private void Init()
	{
		gameManager.homeScene.weizhuang = this;
		dataManager = gameManager.dataManager;
		eventID = gameManager.player.GetEventId();
		username = dataManager.dic11[eventID].name.Split(';');
		lyingChat = dataManager.dic11[eventID].lyingchat.Substring(1).Split(';');
		photo = dataManager.dic11[eventID].photo.Split(';');
		if (gameManager.Is_Dlc7())
		{
			task = new string[2] { "31000", "31004" };
		}
		else
		{
			task = dataManager.dic11[eventID].camcondition.Substring(1).Split(';');
		}
		camdes = dataManager.dic11[eventID].camdes.Split(';');
		lying = new Dictionary<string, Dictionary<string, string>>();
		for (int i = 0; i < username.Length; i++)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("des", camdes[i]);
			dictionary.Add("avatar", photo[i]);
			dictionary.Add("lyingChatId", lyingChat[i]);
			dictionary.Add("condition", task[i]);
			dictionary.Add("index", i.ToString());
			dictionary.Add("name", username[i]);
			lying.Add(username[i], dictionary);
		}
		ResetUserList();
		submitBtn.onClick.AddListener(delegate
		{
			if (choiceCamID.Trim() != "")
			{
				ChoicePerson();
			}
		});
		if (gameManager.player.playerdata.isCourse10 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel10.weizhuang = bk;
		}
	}

	public void ChoicePerson()
	{
		gameManager.player.playerdata.UseSocialMethod(2);
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Chat/chatLogin"), base.transform.parent);
		gameObject.GetComponent<ChatLogin>().Show();
		Close();
		if (gameManager.IsBasic())
		{
			gameObject.GetComponent<ChatLogin>().DirectLogin(dataManager.dic3[choiceCamID].log.Substring(1), isweizhuang: true);
		}
		else
		{
			gameObject.GetComponent<ChatLogin>().DirectLoginDLC(dataManager.dic3[choiceCamID].log.Substring(1), dataManager.dic3[choiceCamID]);
		}
		gameManager.homeScene.goalDialog.CompleteItem(dataManager.dic3[choiceCamID].missionID.Substring(1));
	}

	private void ResetUserList()
	{
		for (int i = 0; i < personParent.transform.childCount; i++)
		{
			Object.Destroy(personParent.transform.GetChild(i).gameObject);
		}
		bool flag = true;
		string camListName = DLCNameUtil.Instance.GetCamListName();
		foreach (KeyValuePair<string, Dictionary<string, string>> item in lying)
		{
			if (item.Value["lyingChatId"] == "31004" && gameManager.player.playerdata.itemlist.Contains("11401") && gameManager.player.playerdata.itemlist.Contains("11402"))
			{
				noobject.SetActive(value: false);
				Object.Instantiate(Resources.Load<Transform>(camListName), personParent.transform).GetComponent<Camlist>().Init(item.Value, gameManager, this);
				flag = false;
			}
			else if (gameManager.player.playerdata.canweizhuangcondition.Contains(item.Value["condition"]) || item.Value["condition"].Equals("0") || gameManager.isbug)
			{
				noobject.SetActive(value: false);
				Object.Instantiate(Resources.Load<Transform>(camListName), personParent.transform).GetComponent<Camlist>().Init(item.Value, gameManager, this);
				flag = false;
			}
			else if (gameManager.player.playerdata.itemlist.Contains(item.Value["condition"]) || item.Value["condition"].Equals("0") || gameManager.isbug)
			{
				noobject.SetActive(value: false);
				Object.Instantiate(Resources.Load<Transform>(camListName), personParent.transform).GetComponent<Camlist>().Init(item.Value, gameManager, this);
				flag = false;
			}
			else if (gameManager.player.playerdata.temporaryhopelist.Contains(item.Value["condition"]) || item.Value["condition"].Equals("0") || gameManager.isbug)
			{
				noobject.SetActive(value: false);
				Object.Instantiate(Resources.Load<Transform>(camListName), personParent.transform).GetComponent<Camlist>().Init(item.Value, gameManager, this);
				flag = false;
			}
		}
		if (flag)
		{
			height = 130f;
			bk.GetComponent<Image>().sprite = notingSprite;
			titleObj.SetActive(value: false);
			content.GetComponent<RectTransform>().sizeDelta = new Vector2(745f, 130f);
			noobject.SetActive(value: true);
			content.Find("Scroll View").gameObject.SetActive(value: false);
			bk.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 166f, 0f);
			submitBtn.gameObject.SetActive(value: false);
			camTip.GetComponent<Text>().fontSize = 30;
			noobject.GetComponent<Text>().fontSize = 30;
		}
	}

	private void ShowRecord()
	{
		string text = "";
		string[] array = gameManager.dataManager.dic11[gameManager.player.GetEventId()].lyingchat.Substring(1).Split('#')[gameManager.player.playerdata.weizhuangpos].Split(';');
		if (gameManager.player.playerdata.weizhuangpos != -1)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Debug.Log("lyingPerson" + array[i]);
				if (gameManager.player.playerdata.camChatInfo.ContainsKey(array[i]))
				{
					text = gameManager.dataManager.dic3[array[i]].log.Substring(1);
				}
			}
		}
		else
		{
			text = gameManager.dataManager.dic3[array[array.Length - 1]].log.Substring(1);
		}
		if (text.Trim() != "")
		{
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Chat/chatLogin"), gameManager.homeScene.transform);
			obj.transform.parent.gameObject.SetActive(value: true);
			obj.GetComponent<ChatLogin>().Show();
			obj.GetComponent<ChatLogin>().DirectLogin(text, isweizhuang: true);
			obj.GetComponent<ChatLogin>().toolid = 0;
			Hide();
		}
	}

	public override void BeforeShowSize()
	{
		Init();
	}

	public override void AfterShowSize()
	{
	}
}
