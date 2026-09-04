using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weizhuang02 : CustomDialog
{
	public GameObject personParent;

	public Dropdown choiceTarget;

	public Button recordBtn;

	private string eventID;

	private Dictionary<string, string> lying;

	private DataManager dataManager;

	private string[] username;

	private string[] fakeName;

	private string[] lyingChat;

	private string[] fakePhoto;

	private string[] photo;

	private void Start()
	{
		dataManager = gameManager.dataManager;
		eventID = gameManager.player.GetEventId();
		username = dataManager.dic11[eventID].name.Substring(1).Split(';');
		fakeName = dataManager.dic11[eventID].fakename.Split(';');
		lyingChat = dataManager.dic11[eventID].lyingchat.Substring(1).Split('#');
		fakePhoto = dataManager.dic11[eventID].fakephoto.Split(';');
		photo = dataManager.dic11[eventID].photo.Substring(1).Split(';');
		lying = new Dictionary<string, string>();
		for (int i = 0; i < username.Length; i++)
		{
			string key = ((username[i] == "0") ? fakeName[i] : username[i]);
			lying.Add(key, lyingChat[i]);
		}
		ChoiceGuy(0);
	}

	private void ChoiceGuy(int guyIndex)
	{
		string[] array = lyingChat[guyIndex].Split(';');
		if (gameManager.player.playerdata.weizhuang.Count <= guyIndex && !gameManager.player.playerdata.weizhuang.ContainsKey(username[guyIndex]))
		{
			string key = ((username[guyIndex] == "0") ? fakeName[guyIndex] : username[guyIndex]);
			gameManager.player.playerdata.weizhuang.Add(key, new Dictionary<string, int>());
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.player.playerdata.weizhuang[key].Add(array[i], 0);
			}
		}
		ResetSelect(username, fakeName, fakePhoto, photo);
		ResetUserList(fakeName[guyIndex], guyIndex);
		recordBtn.onClick.RemoveAllListeners();
		recordBtn.onClick.AddListener(ShowRecord);
	}

	public void ChangeTargetVal()
	{
		gameManager.player.playerdata.weizhuangpos = choiceTarget.value;
		ChoiceGuy(choiceTarget.value);
	}

	private void ResetSelect(string[] name, string[] fakeName, string[] fakePhoto, string[] photo)
	{
		choiceTarget.options.Clear();
		for (int i = 0; i < name.Length; i++)
		{
			string text = ((name[i] == "0") ? fakeName[i] : name[i]);
			string text2 = ((photo[i] == "0") ? fakePhoto[i] : photo[i]);
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			optionData.text = text;
			optionData.image = Resources.Load<Sprite>("touxiang/" + text2);
			choiceTarget.options.Add(optionData);
		}
	}

	public void ChoicePerson(int i)
	{
		gameManager.player.playerdata.UseSocialMethod(2);
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Chat/chatLogin"), base.transform.parent);
		obj.GetComponent<ChatLogin>().Show();
		Close();
		obj.GetComponent<ChatLogin>().DirectLogin(dataManager.dic3[personParent.transform.GetChild(i).GetComponent<WeizhuangPerson>().ID].log.Substring(1), isweizhuang: true);
		gameManager.homeScene.goalDialog.CompleteItem(dataManager.dic3[personParent.transform.GetChild(i).GetComponent<WeizhuangPerson>().ID].missionID.Substring(1));
	}

	private void ResetUserList(string name, int index)
	{
		for (int i = 0; i < personParent.transform.childCount; i++)
		{
			Object.Destroy(personParent.transform.GetChild(i).gameObject);
		}
		string[] array = lying[name].Split(';');
		for (int j = 0; j < array.Length; j++)
		{
			string item = gameManager.dataManager.dic3[array[j]].condition.Substring(1);
			gameManager.player.playerdata.itemlist.Contains(item);
			Object.Instantiate(Resources.Load<Transform>(DLCNameUtil.Instance.GetWeizhuangChoiceName()), personParent.transform);
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
	}

	public override void AfterShowSize()
	{
	}
}
