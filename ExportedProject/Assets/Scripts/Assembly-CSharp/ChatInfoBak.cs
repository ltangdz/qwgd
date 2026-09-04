using System.Collections.Generic;
using Honeti;
using LeonKim;
using UnityEngine;
using UnityEngine.UI;

public class ChatInfoBak : MonoBehaviour
{
	public GameObject friendListPar;

	private List<Sprite> headPor;

	private string[] chatName;

	private string[] chatInfo;

	private BaseLoopList bll;

	private void Start()
	{
		chatName = new string[2] { "Kay", "Jacob" };
		chatInfo = new string[2] { "最近过得还好吧", "那天跟你说的秘方你用了没？" };
		bll = GetComponent<BaseLoopList>();
		bll.Init(BakFun);
		bll.ShowList(chatName.Length);
		Transform transform = friendListPar.transform.Find("0");
		transform.Find("img_checkBorder").gameObject.SetActive(value: true);
		Object.FindObjectOfType<ChatDialog>().ChangeChat(transform);
		for (int i = 0; i < friendListPar.transform.childCount; i++)
		{
			Transform chatList = friendListPar.transform.GetChild(i);
			chatList.GetComponent<Button>().onClick.AddListener(delegate
			{
				for (int j = 0; j < friendListPar.transform.childCount; j++)
				{
					friendListPar.transform.GetChild(j).Find("img_checkBorder").gameObject.SetActive(value: false);
				}
				chatList.Find("img_checkBorder").gameObject.SetActive(value: true);
				Object.FindObjectOfType<ChatDialog>().ChangeChat(chatList);
			});
		}
	}

	public void BakFun(GameObject cell, int i)
	{
		cell.transform.Find("txt_chatName").GetComponent<I18NText>().updateTranslation2(chatName[i - 1]);
		GameManager.SetTextWithEllipsis(cell.transform.Find("txt_chatInfo").GetComponent<Text>(), chatInfo[i - 1]);
		cell.transform.Find("img_headPor").GetComponent<Image>().sprite = Resources.Load("chatPor" + i, typeof(Sprite)) as Sprite;
	}
}
