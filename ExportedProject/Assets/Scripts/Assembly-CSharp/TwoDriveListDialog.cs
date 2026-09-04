using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;

public class TwoDriveListDialog : CustomDialog
{
	public GameObject chatBak;

	public Transform chatContent;

	public TwoDrive twoDrive;

	public TwoDrive2 twoDrive2;

	[SerializeField]
	public List<int> typelist = new List<int>();

	[SerializeField]
	public List<string> contentlist = new List<string>();

	public List<string> contentID = new List<string>();

	public List<string> contentkey = new List<string>();

	public string leftname = "^message_event0114";

	public string rightname = "^message_event0137";

	public void Init(TwoDrive obj)
	{
		twoDrive = obj;
		chatBak.SetActive(value: true);
		chatBak.GetComponent<ChatBak>().ShowCourse();
		StartCoroutine(ShowList());
	}

	public void Init(TwoDrive2 obj = null)
	{
		if (obj != null)
		{
			twoDrive2 = obj;
			if (obj.typelist.Count != 0)
			{
				typelist = obj.typelist;
			}
			if (obj.contentlist.Count != 0)
			{
				contentlist = obj.contentlist;
			}
			if (obj.contentID.Count != 0)
			{
				contentID = obj.contentID;
			}
			if (obj.contentkey.Count != 0)
			{
				contentkey = obj.contentkey;
			}
			if (obj.leftname != "")
			{
				leftname = obj.leftname;
			}
			if (obj.rightname != "")
			{
				rightname = obj.rightname;
			}
		}
		chatBak.SetActive(value: true);
		chatBak.GetComponent<ChatBak>().ShowCourse();
		StartCoroutine(ShowList());
	}

	private void Start()
	{
		gameManager.istaohuashow = true;
		gameManager.CanShowSetting(1);
		gameManager.musicManager.LowerVol();
		btn_close.onClick.AddListener(delegate
		{
			gameManager.musicManager.ResumeVol();
			gameManager.CanShowSetting(-1);
			gameManager.istaohuashow = false;
			if (twoDrive != null)
			{
				twoDrive.CloseList();
			}
			if (twoDrive2 != null)
			{
				twoDrive2.CloseList();
			}
		});
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
				Transform obj3 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitem"), chatContent.transform);
				obj3.Find("txt_name").GetComponent<I18NText>().updateTranslation2(leftname);
				obj3.Find("box/tb_info").gameObject.SetActive(value: true);
				obj3.Find("box/Text").gameObject.SetActive(value: false);
				obj3.Find("box/tb_info").gameObject.GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentkey[i]));
			}
			else if (typelist[i] == 3)
			{
				Transform obj4 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitemBak"), chatContent.transform);
				obj4.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(rightname);
				obj4.Find("inbox/box/tb_info").gameObject.SetActive(value: true);
				obj4.Find("inbox/box/Text").gameObject.SetActive(value: false);
				obj4.Find("inbox/box/tb_info").gameObject.GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentkey[i]));
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
