using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class FishItem : MonoBehaviour
{
	public Image img_avatar;

	public Text txt_name;

	public Text txt_link;

	public List<Sprite> icon;

	public Image btnIcon;

	public Text btnTxt;

	public Button sendBtn;

	private string type;

	private string avatar;

	private string username;

	private string link;

	private DATA33 data;

	private bool startFish;

	private GameManager gameManager;

	private PhishingDialog1 parObj;

	private string ID;

	public string GetID
	{
		get
		{
			return ID;
		}
		set
		{
			ID = value;
		}
	}

	public void Init(DATA33 itemData, GameManager gm, PhishingDialog1 par)
	{
		data = itemData;
		ID = itemData.ID.ToString();
		gameManager = gm;
		avatar = itemData.avatar;
		username = itemData.name;
		type = itemData.method.Substring(1);
		link = itemData.url;
		parObj = par;
		ResetInfo();
	}

	private void ResetInfo()
	{
		sendBtn.onClick.AddListener(PojieStart);
		Sprite sprite = Resources.Load<Sprite>("touxiang/" + avatar);
		img_avatar.sprite = sprite;
		txt_name.GetComponent<I18NText>().updateTranslation2(username);
		txt_link.GetComponent<I18NText>().updateTranslation2(link);
		if (type == "0")
		{
			btnIcon.sprite = icon[0];
			btnTxt.GetComponent<I18NText>().updateTranslation2("^fish_btnlabel01");
		}
		else if (type == "1")
		{
			btnIcon.sprite = icon[1];
			btnTxt.GetComponent<I18NText>().updateTranslation2("^fish_btnlabel02");
		}
		Debug.Log("data.name:" + data.name);
		if (gameManager.player.playerdata.fishLink[data.name] == 1 && gameManager.player.playerdata.Eventid != 7)
		{
			sendBtn.interactable = false;
		}
		if ((ID == "3310001" && gameManager.player.playerdata.dlc7Invades[0] == 2) || (ID == "3310002" && gameManager.player.playerdata.dlc7Invades[1] == 2))
		{
			sendBtn.interactable = false;
		}
	}

	public void PojieStart()
	{
		if (startFish)
		{
			return;
		}
		startFish = true;
		if (type == "0")
		{
			if (gameManager.Is_Dlc6())
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/invadephoneDialog3310000"), parObj.transform.parent);
				obj.GetComponent<InvadePhoneDialog>().Init();
				obj.GetComponent<InvadePhoneDialog>().Show();
				gameManager.CanShowSetting(-1);
				gameManager.homeScene.notebook.transform.SetAsLastSibling();
				parObj.Hide();
			}
			else
			{
				Debug.Log("手机破解");
				Object.Instantiate(Resources.Load<GameObject>("Dialog/fishphonethought"), parObj.transform.parent).GetComponent<FishPhoneThought>().Init(ID, gameManager);
				parObj.Hide();
			}
		}
		else
		{
			Debug.Log("服务器破解");
			gameManager.saveManager.SavePlayerData();
			GameObject obj2 = Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetInvadeDialogName()), gameManager.homeScene.middle.transform);
			obj2.GetComponent<InvadeDialog>().Show();
			obj2.GetComponent<InvadeDialog>().userid = ID;
			parObj.Hide();
		}
	}
}
