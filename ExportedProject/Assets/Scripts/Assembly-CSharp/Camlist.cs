using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Camlist : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Image userAvatar;

	public Text introLabel;

	public Text userName;

	public Text userIntro;

	public Image arrow;

	public Image camAvatar;

	public Text camName;

	public Text camIntro;

	public Text intro;

	public Image imgPoint;

	public GameObject camObj;

	public GameObject camInfo;

	public Sprite[] hoverBakUrl;

	public Sprite[] point;

	public Color[] mainColor;

	private GameManager gameManager;

	private Weizhuang parObj;

	private string camID;

	private Dictionary<string, string> userMessInfo;

	private bool selected;

	public string getID => camID;

	public void Init(Dictionary<string, string> userMess, GameManager gm, Weizhuang par)
	{
		gameManager = gm;
		parObj = par;
		camID = userMess["lyingChatId"];
		userMessInfo = userMess;
		Debug.Log("camID:" + camID);
		string key = gameManager.dataManager.dic3[camID].name;
		string key2 = gameManager.dataManager.dic3[camID].describe.Replace(".0", "");
		string text = gameManager.dataManager.dic3[camID].head.Replace(".0", "");
		userName.GetComponent<I18NText>().updateTranslation2(userMess["name"]);
		userIntro.GetComponent<I18NText>().updateTranslation2(userMess["des"].Replace(".0", ""));
		Debug.Log("userMess[avatar]::" + userMess["avatar"]);
		userAvatar.sprite = Resources.Load<Sprite>("touxiang/" + userMess["avatar"]);
		camAvatar.sprite = Resources.Load<Sprite>("touxiang/" + text);
		camName.GetComponent<I18NText>().updateTranslation2(key);
		camIntro.GetComponent<I18NText>().updateTranslation2(key2);
		GetComponent<Button>().onClick.AddListener(ChoiceUser);
	}

	private void ChoiceUser()
	{
		parObj.submitBtn.interactable = true;
		gameManager.player.playerdata.weizhuangpos = int.Parse(userMessInfo["index"]);
		if (!gameManager.player.playerdata.weizhuang.ContainsKey(userMessInfo["name"]))
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary.Add(userMessInfo["lyingChatId"], 0);
			gameManager.player.playerdata.weizhuang.Add(userMessInfo["name"], dictionary);
		}
		else if (gameManager.player.playerdata.weizhuang[userMessInfo["name"]][userMessInfo["lyingChatId"]] == 1)
		{
			parObj.submitBtn.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^search_chat");
		}
		else
		{
			parObj.submitBtn.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^cam_label03");
		}
		parObj.choiceCamID = userMessInfo["lyingChatId"];
		for (int i = 0; i < parObj.personParent.transform.childCount; i++)
		{
			if (i.ToString() != userMessInfo["index"])
			{
				parObj.personParent.transform.GetChild(i).GetComponent<Camlist>().CloseOtherCaminfo();
			}
		}
		selected = true;
		GetComponent<Image>().sprite = hoverBakUrl[1];
		Focus(isFocus: true);
	}

	public void CloseOtherCaminfo()
	{
		selected = false;
		GetComponent<Image>().sprite = hoverBakUrl[0];
		Focus(isFocus: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<Image>().sprite = hoverBakUrl[1];
		Focus(isFocus: true);
	}

	private void Focus(bool isFocus)
	{
		if (gameManager.GameType == GameTypeEnum.DLC7)
		{
			imgPoint.sprite = point[isFocus ? 1 : 0];
			intro.color = mainColor[isFocus ? 1 : 0];
			camName.color = mainColor[isFocus ? 1 : 0];
			camIntro.color = mainColor[isFocus ? 1 : 0];
		}
		else
		{
			imgPoint.sprite = point[isFocus ? 1 : 0];
			introLabel.color = mainColor[isFocus ? 1 : 0];
			userName.color = mainColor[isFocus ? 1 : 0];
			userIntro.color = mainColor[isFocus ? 1 : 0];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!selected)
		{
			GetComponent<Image>().sprite = hoverBakUrl[0];
			Focus(isFocus: false);
		}
	}
}
