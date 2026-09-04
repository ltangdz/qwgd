using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Weizhuang01 : CustomDialog
{
	public Transform choicePerson;

	public Sprite boxChiceImg;

	public Sprite boxNoChiceImg;

	public Sprite btnChoiceImg;

	public Sprite btnNoChoiceImg;

	private string eventID;

	private Dictionary<string, string> lying;

	private DataManager dataManager;

	private void Start()
	{
		dataManager = gameManager.dataManager;
		eventID = gameManager.player.GetEventId();
		string[] array = dataManager.dic11[eventID].camcondition.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (!gameManager.player.playerdata.itemlist.Contains(array[i]))
			{
				return;
			}
		}
		for (int j = 0; j < choicePerson.childCount; j++)
		{
			choicePerson.GetChild(j).Find("choice_btn/Text").GetComponent<I18NText>()
				.updateTranslation2("^txt_camouflagedialog04");
		}
		string[] array2 = dataManager.dic11[eventID].name.Substring(1).Split(';');
		string[] array3 = dataManager.dic11[eventID].fakename.Split(';');
		string[] array4 = dataManager.dic11[eventID].lyingchat.Substring(1).Split('#');
		string[] fakephoto = dataManager.dic11[eventID].fakephoto.Split(';');
		string[] photo = dataManager.dic11[eventID].photo.Substring(1).Split(';');
		lying = new Dictionary<string, string>();
		for (int k = 0; k < array2.Length; k++)
		{
			string key = ((array2[k] == "0") ? array3[k] : array2[k]);
			lying.Add(key, array4[k]);
		}
		string[] array5 = array4[gameManager.player.playerdata.weizhuangpos].Split(';');
		if (!gameManager.player.playerdata.weizhuang.ContainsKey(array5[0]))
		{
			gameManager.player.playerdata.weizhuang.Clear();
			for (int l = 0; l < array5.Length; l++)
			{
			}
		}
		ResetUserList(array3[gameManager.player.playerdata.weizhuangpos]);
		SetCamInfo(array2, array3, fakephoto, photo, gameManager.player.playerdata.weizhuangpos);
	}

	private void SetCamInfo(string[] name, string[] fakename, string[] fakephoto, string[] photo, int a)
	{
		Debug.Log(photo[a]);
		if (photo[a] == "0")
		{
			Debug.Log(fakephoto[a]);
		}
	}

	private void ChoicePerson(int i)
	{
		gameManager.player.playerdata.UseSocialMethod(2);
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Chat/chatLogin"), base.transform.parent);
		obj.GetComponent<ChatLogin>().Show();
		Close();
		obj.GetComponent<ChatLogin>().DirectLogin(dataManager.dic3[choicePerson.GetChild(i).Find("userID").GetComponent<Text>()
			.text].log.Substring(1), isweizhuang: true);
		gameManager.homeScene.goalDialog.CompleteItem(dataManager.dic3[choicePerson.GetChild(i).Find("userID").GetComponent<Text>()
			.text].missionID.Substring(1));
	}

	private void ResetUserList(string name)
	{
		string[] array = lying[name].Split(';');
		for (int i = 0; i < choicePerson.childCount; i++)
		{
			choicePerson.GetChild(i).Find("userID").GetComponent<I18NText>()
				.updateTranslation2(array[i]);
			choicePerson.GetChild(i).Find("head_photo/Image").GetComponent<Image>()
				.sprite = Resources.Load<Sprite>("touxiang/" + dataManager.dic3[array[i]].head);
			choicePerson.GetChild(i).Find("name").GetComponent<I18NText>()
				.updateTranslation2(dataManager.dic3[array[i]].name);
			choicePerson.GetChild(i).Find("info").GetComponent<I18NText>()
				.updateTranslation2(dataManager.dic3[array[i]].describe);
		}
	}

	private void ChangeBoxType(int s)
	{
		for (int i = 0; i < choicePerson.childCount; i++)
		{
			choicePerson.GetChild(i).GetComponent<Image>().sprite = boxNoChiceImg;
			choicePerson.GetChild(i).Find("out_headline").gameObject.SetActive(value: true);
			choicePerson.GetChild(i).Find("choice_headline").gameObject.SetActive(value: false);
			choicePerson.GetChild(i).Find("choice_btn").GetComponent<Image>()
				.sprite = btnNoChoiceImg;
			choicePerson.GetChild(i).Find("name").GetComponent<I18NText>()
				.updateTranslation2("<color=#66718f>" + choicePerson.GetChild(i).Find("name").GetComponent<Text>()
					.text + "</color>");
		}
		choicePerson.GetChild(s).Find("out_headline").gameObject.SetActive(value: false);
		choicePerson.GetChild(s).Find("choice_headline").gameObject.SetActive(value: true);
		choicePerson.GetChild(s).Find("choice_btn").GetComponent<Image>()
			.sprite = btnChoiceImg;
		choicePerson.GetChild(s).Find("name").GetComponent<I18NText>()
			.updateTranslation2("<color=#a7e0fe>" + choicePerson.GetChild(s).Find("name").GetComponent<Text>()
				.text + "</color>");
		choicePerson.GetChild(s).GetComponent<Image>().sprite = boxChiceImg;
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
