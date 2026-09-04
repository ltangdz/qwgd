using Honeti;
using UnityEngine;

public class MailListDialog : CustomDialog
{
	public GameObject chatBak;

	public Transform chatContent;

	private TapEmail parObj;

	public void Init(TapEmail obj)
	{
		parObj = obj;
	}

	private void Start()
	{
		btn_close.onClick.AddListener(delegate
		{
			gameManager.musicManager.ResumeVol();
			parObj.CloseList();
		});
	}

	private void ShowList()
	{
		for (int i = 0; i < parObj.typelist.Count; i++)
		{
			if (parObj.typelist[i] == 0)
			{
				Transform obj = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitem"), chatContent.transform);
				obj.Find("txt_name").GetComponent<I18NText>().updateTranslation2(parObj.leftname);
				obj.Find("box/Text").gameObject.SetActive(value: true);
				obj.Find("box/tb_info").gameObject.SetActive(value: false);
				obj.Find("box/Text").GetComponent<I18NText>().updateTranslation2(parObj.contentlist[i]);
			}
			else if (parObj.typelist[i] == 1)
			{
				Transform obj2 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitemBak"), chatContent.transform);
				obj2.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(parObj.rightname);
				obj2.Find("inbox/box/Text").GetComponent<I18NText>().updateTranslation2(parObj.contentlist[i]);
			}
			else if (parObj.typelist[i] == 2)
			{
				Transform obj3 = Object.Instantiate(Resources.Load<Transform>("twodrive_phoneitem"), chatContent.transform);
				obj3.Find("txt_name").GetComponent<I18NText>().updateTranslation2(parObj.leftname);
				obj3.Find("box/tb_info").gameObject.SetActive(value: true);
				obj3.Find("box/Text").gameObject.SetActive(value: false);
				obj3.Find("box/tb_info").GetComponent<MultiplyText>().SetNewWidth(I18N.instance.getValue(parObj.contentlist[i]));
				obj3.Find("box/tb_info").GetComponent<MultiplyText>().SetContent2(parObj.contentlist[i], parObj.contentID[i], I18N.instance.getValue(parObj.contentlist[i]));
			}
		}
	}

	public override void AfterShowSize()
	{
		chatBak.SetActive(value: true);
		chatBak.GetComponent<ChatBak>().ShowCourse();
		ShowList();
		gameManager.musicManager.LowerVol();
		btn_close.onClick.AddListener(delegate
		{
			gameManager.soundManager.Stop();
			gameManager.musicManager.ResumeVol();
			gameManager.CanShowSetting(-1);
		});
	}

	public override void BeforeShowSize()
	{
	}
}
