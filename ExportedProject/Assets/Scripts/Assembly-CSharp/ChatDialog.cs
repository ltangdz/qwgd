using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ChatDialog : CustomDialog
{
	public Transform replyParent;

	public GameObject chatRoom;

	public ScrollRect scrollRect;

	private GameObject chatBox;

	private bool haveChat;

	private bool haveReply = true;

	private string sendLabel;

	private Transform chatTarget;

	public void ChangeChat(Transform chatList)
	{
		if (!(chatList != chatTarget))
		{
			return;
		}
		haveChat = false;
		haveReply = true;
		chatTarget = chatList;
		StopCoroutine("SetChatInfo");
		StopCoroutine("Loading");
		if (chatRoom.transform.childCount > 0)
		{
			for (int i = 0; i < chatRoom.transform.childCount; i++)
			{
				Object.Destroy(chatRoom.transform.GetChild(i).gameObject);
			}
		}
		string text = "我曾经说话的内容";
		if (text.Length > 0)
		{
			RefreshHisChat(1, text);
		}
		GetSayLabel();
	}

	private void GetSayLabel()
	{
		if (replyParent.childCount > 0)
		{
			for (int i = 0; i < replyParent.childCount; i++)
			{
				Object.Destroy(replyParent.GetChild(i).gameObject);
			}
		}
		string[] array = new string[3] { "您好", "您发的求职邮件我们收到了", "您把电话和邮箱给我，我做个记录" };
		for (int j = 0; j < array.Length; j++)
		{
			GameObject gameObject = Resources.Load("Chat/chatLabel", typeof(GameObject)) as GameObject;
			gameObject.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(array[j].Trim());
			GameObject objChat = Object.Instantiate(gameObject, replyParent);
			objChat.GetComponent<Button>().onClick.AddListener(delegate
			{
				SendChat(objChat.transform);
			});
			objChat.SetActive(value: true);
		}
	}

	private void SendChat(Transform replyLabel)
	{
		if (!haveChat && haveReply)
		{
			sendLabel = replyLabel.Find("Text").GetComponent<Text>().text;
			CreatChat(sendLabel);
			haveReply = false;
		}
	}

	private void WaitReply()
	{
		if (!haveReply)
		{
			string label = "我发的求职邮件你们收到了";
			CreatChat(label);
		}
	}

	private void CreatChat(string label)
	{
		if (!haveChat)
		{
			chatBox = Resources.Load("Dialog/chat_itemBak", typeof(GameObject)) as GameObject;
		}
		else
		{
			chatBox = Resources.Load("Dialog/chat_item", typeof(GameObject)) as GameObject;
			chatBox.transform.Find("img_friendHeadPor").GetComponent<Image>().sprite = chatTarget.Find("img_headPor").GetComponent<Image>().sprite;
		}
		GameObject addChat = Object.Instantiate(chatBox, chatRoom.transform);
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
		StartCoroutine(SetChatInfo(addChat, label));
	}

	private IEnumerator SetChatInfo(GameObject addChat, string label)
	{
		float time = (float)label.Length * 0.1f;
		StartCoroutine(Loading(addChat));
		yield return new WaitForSeconds(time);
		StopCoroutine(Loading(addChat));
		if (addChat != null)
		{
			addChat.transform.Find("loading").gameObject.SetActive(value: false);
			addChat.transform.Find("img_chatInfo").gameObject.SetActive(value: true);
			addChat.transform.Find("img_chatInfo/txt_chatInfo").GetComponent<I18NText>().updateTranslation2(label);
			haveChat = !haveChat;
			if (haveChat)
			{
				Invoke("WaitReply", time * 2f);
				yield break;
			}
			haveReply = !haveReply;
			GetSayLabel();
		}
	}

	private IEnumerator Loading(GameObject chat)
	{
		yield return new WaitForSeconds(0.1f);
		int a = 0;
		string loadLabel = I18N.instance.getValue("^write_ing");
		while (true)
		{
			if (chat != null)
			{
				chat.transform.Find("loading").GetComponent<I18NText>().updateTranslation2(loadLabel);
			}
			yield return new WaitForSeconds(0.2f);
			loadLabel += ".";
			if (chat != null)
			{
				chat.transform.Find("loading").GetComponent<I18NText>().updateTranslation2(loadLabel);
			}
			a++;
			if (a >= 3)
			{
				yield return new WaitForSeconds(0.2f);
				if (chat != null)
				{
					loadLabel = loadLabel.Substring(0, loadLabel.Length - 3);
					chat.transform.Find("loading").GetComponent<I18NText>().updateTranslation2(loadLabel);
					a = 0;
				}
			}
		}
	}

	private void RefreshHisChat(int person, string chatInfo)
	{
		switch (person)
		{
		case 0:
			chatBox = Resources.Load("chat_itemBak", typeof(GameObject)) as GameObject;
			break;
		case 1:
			chatBox = Resources.Load("chat_item", typeof(GameObject)) as GameObject;
			chatBox.transform.Find("img_friendHeadPor").GetComponent<Image>().sprite = chatTarget.Find("img_headPor").GetComponent<Image>().sprite;
			break;
		}
		GameObject obj = Object.Instantiate(chatBox, chatRoom.transform);
		obj.transform.Find("loading").gameObject.SetActive(value: false);
		obj.transform.Find("img_chatInfo").gameObject.SetActive(value: true);
		obj.transform.Find("img_chatInfo/txt_chatInfo").GetComponent<I18NText>().updateTranslation2(chatInfo);
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
