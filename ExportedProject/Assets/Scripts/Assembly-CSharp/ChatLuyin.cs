using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;

public class ChatLuyin : CustomDialog
{
	public GameObject chatBak;

	public Transform chatContent;

	public int clipNum;

	[SerializeField]
	public List<int> typelist = new List<int>();

	[SerializeField]
	public List<string> contentlist = new List<string>();

	public List<string> contentID = new List<string>();

	public List<string> contentkey = new List<string>();

	public string leftname = "^message_event0114";

	public string rightname = "^message_event0137";

	private void Start()
	{
		gameManager.istaohuashow = true;
		gameManager.CanShowSetting(1);
		gameManager.musicManager.LowerVol();
		btn_close.onClick.AddListener(delegate
		{
			gameManager.musicManager.ResumeVol();
			gameManager.istaohuashow = false;
			gameManager.CanShowSetting(-1);
		});
		Show();
		gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), clipNum);
	}

	private IEnumerator ShowList()
	{
		for (int i = 0; i < typelist.Count; i++)
		{
			if (typelist[i] == 0)
			{
				Transform obj = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitem"), chatContent.transform);
				obj.Find("txt_name").GetComponent<I18NText>().updateTranslation2(leftname);
				obj.Find("box/Text").gameObject.SetActive(value: true);
				obj.Find("box/tb_info").gameObject.SetActive(value: false);
				obj.Find("box/Text").GetComponent<I18NText>().updateTranslation2(contentlist[i]);
			}
			else if (typelist[i] == 1)
			{
				Transform obj2 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitemBak"), chatContent.transform);
				obj2.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(rightname);
				obj2.Find("inbox/box/Text").GetComponent<I18NText>().updateTranslation2(contentlist[i]);
			}
			else if (typelist[i] == 2)
			{
				Transform transform = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitem"), chatContent.transform);
				transform.Find("txt_name").GetComponent<I18NText>().updateTranslation2(leftname);
				transform.Find("box/tb_info").gameObject.SetActive(value: true);
				transform.Find("box/Text").gameObject.SetActive(value: false);
				if (contentID[i].Contains(";"))
				{
					string[] array = contentID[i].Split(';');
					transform.Find("box/tb_info").gameObject.GetComponent<MultiplyText>().otheritem = array;
					transform.Find("box/tb_info").gameObject.GetComponent<MultiplyText>().SetContent2(contentlist[i], array[0], I18N.instance.getValue(contentkey[i]));
				}
				else
				{
					transform.Find("box/tb_info").gameObject.GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentkey[i]));
				}
			}
			else if (typelist[i] == 3)
			{
				Transform obj3 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitemBak"), chatContent.transform);
				obj3.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(rightname);
				obj3.Find("inbox/box/tb_info").gameObject.SetActive(value: true);
				obj3.Find("inbox/box/Text").gameObject.SetActive(value: false);
				obj3.Find("inbox/box/tb_info").gameObject.GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentkey[i]));
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public override void AfterShowSize()
	{
		chatBak.SetActive(value: true);
		chatBak.GetComponent<ChatBak>().ShowCourse();
		StartCoroutine(ShowList());
	}

	public override void BeforeShowSize()
	{
	}
}
