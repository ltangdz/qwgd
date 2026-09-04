using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ChatTip : MonoBehaviour
{
	public Text txt_to;

	public Text txt_from;

	public Text txt_subject;

	public HomeScene homeScene;

	public bool ishasclick;

	public string userid = "";

	public GameManager gameManager;

	public Image img_avatar;

	public Text txt_count;

	public ComputerButton btn_chat;

	public Image img_black;

	public bool isforce = true;

	public string frdID;

	public string labelID;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void OpenChat()
	{
		if (!ishasclick)
		{
			StartCoroutine(ShowChat());
			ishasclick = true;
			GetComponent<Animator>().Play("ani_hidemailtip");
		}
	}

	private IEnumerator ShowChat()
	{
		gameManager.homeScene.computerButtonBox.btn_chat.transform.Find("img_red").gameObject.SetActive(value: false);
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Chat/chatLogin"), base.transform.parent);
		obj.transform.parent.gameObject.SetActive(value: true);
		obj.GetComponent<ChatLogin>().Show();
		obj.GetComponent<ChatLogin>().ChatBoxLogin(gameManager.player.playerdata.chatLoginID, "2");
		yield return new WaitForSeconds(4.2f);
	}

	public void HideChat()
	{
		ishasclick = true;
		img_black.raycastTarget = false;
		img_black.enabled = false;
		GetComponent<Animator>().Play("ani_hidemailtip");
	}

	public void ShowChatTip(string avatar, string from, string content, bool isforce)
	{
		if (isforce)
		{
			img_black.raycastTarget = true;
			img_black.enabled = true;
		}
		base.transform.SetAsLastSibling();
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(20);
		this.isforce = isforce;
		img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + avatar);
		txt_to.GetComponent<I18NText>().updateTranslation2(gameManager.player.playerdata.nickname);
		txt_from.GetComponent<I18NText>().updateTranslation2(from);
		txt_subject.GetComponent<I18NText>().updateTranslation2(content);
		txt_count.GetComponent<I18NText>().updateTranslation2("1");
		GetComponent<Animator>().Play("ani_mailtip");
		Invoke("HideImgBak", 8f);
	}

	private void HideImgBak()
	{
		img_black.raycastTarget = false;
		img_black.enabled = false;
	}

	public void Addcount()
	{
		txt_count.GetComponent<I18NText>().updateTranslation2((int.Parse(txt_count.text) + 1).ToString());
	}
}
