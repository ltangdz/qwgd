using System;
using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MailInfo : MonoBehaviour
{
	public DATA15 data15;

	public Text getName;

	public Text mailTime;

	public Transform mailInfoBox;

	public GameManager gameManager;

	private string mailID;

	public DownloadPanel downloadPanel;

	public Transform mailInfoScrollcontent;

	public string getID => mailID;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Reset(Transform btn, DATA15 item, int type)
	{
		data15 = item;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		mailID = btn.GetComponent<MailList>().getID;
		for (int i = 0; i < mailInfoScrollcontent.childCount; i++)
		{
			if (mailInfoScrollcontent.GetChild(i).name.IndexOf("mail_code") > -1 || mailInfoScrollcontent.GetChild(i).name.IndexOf("mailInfo") > -1 || mailInfoScrollcontent.GetChild(i).name.IndexOf("link") > -1)
			{
				UnityEngine.Object.Destroy(mailInfoScrollcontent.GetChild(i).gameObject);
			}
		}
		mailInfoScrollcontent.transform.parent.parent.GetComponent<ScrollRect>();
		base.transform.gameObject.SetActive(value: true);
		mailInfoBox.Find("txt_title").GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^mail_titleKey") + I18N.instance.getValue(data15.title));
		mailTime.GetComponent<I18NText>().updateTranslation2(data15.sendTime);
		if (type == 1 || type == 3)
		{
			getName.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^mail_senderKey") + I18N.instance.getValue(data15.sender));
			if (type == 3)
			{
				Transform transform = base.transform.Find("mail_title/txt_mailBtn");
				if (transform != null)
				{
					transform.gameObject.SetActive(value: false);
				}
			}
		}
		else
		{
			string key = I18N.instance.getValue("^mail_geterKey") + I18N.instance.getValue(data15.geter);
			if (gameManager.IsAllDlc() && string.IsNullOrEmpty(data15.geter))
			{
				string nickname = gameManager.player.playerdata.nickname;
				key = nickname + "(" + nickname + "@Gomail.com)";
			}
			getName.GetComponent<I18NText>().updateTranslation2(key);
		}
		getName.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
		try
		{
			OpenCodeMail();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

	public void OpenCodeMail()
	{
		if (data15.open == 1 && !gameManager.player.playerdata.MailReadType(gameManager.homeScene.browserMail.UserMail, data15.ID.ToString()))
		{
			GameObject obj = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_code"), mailInfoScrollcontent);
			obj.GetComponent<MailCode>().Init(data15);
			obj.name = "mail_code";
			return;
		}
		for (int i = 0; i < mailInfoScrollcontent.childCount; i++)
		{
			if (mailInfoScrollcontent.GetChild(i).name.IndexOf("mail_code") > -1)
			{
				UnityEngine.Object.Destroy(mailInfoScrollcontent.GetChild(i).gameObject);
			}
		}
		if (data15.type != 3)
		{
			string[] array = data15.info.Split(';');
			string[] array2 = data15.highlight.Substring(1).Split(';');
			string[] array3 = data15.clue.Split(';');
			for (int j = 0; j < array.Length; j++)
			{
				GameObject mailTxt;
				if (array2[j].Equals("0"))
				{
					string text = array[j][0].ToString();
					if (text.Equals("L"))
					{
						mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_link"), mailInfoScrollcontent);
						mailTxt.GetComponent<I18NText>().updateTranslation2(array[j].Substring(1));
					}
					else if (text.Equals("P"))
					{
						Sprite sprite = Resources.Load<Sprite>("Email/" + array[j].Substring(1));
						if (sprite.rect.width > sprite.rect.height)
						{
							mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_image"), mailInfoScrollcontent);
						}
						else
						{
							mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_imageH"), mailInfoScrollcontent);
						}
						mailTxt.GetComponent<Image>().sprite = sprite;
						mailTxt.GetComponent<HighLightPic>().iscancollect = false;
					}
					else
					{
						mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_text0"), mailInfoScrollcontent);
						mailTxt.GetComponent<I18NText>().updateTranslation2(array[j].Substring(1));
					}
				}
				else
				{
					string text2 = array[j][0].ToString();
					if (text2.Equals("L"))
					{
						mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_link"), mailInfoScrollcontent);
					}
					else if (text2.Equals("P"))
					{
						Sprite sprite2 = Resources.Load<Sprite>("Email/" + array[j].Substring(1));
						if (sprite2.rect.width > sprite2.rect.height)
						{
							mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_image"), mailInfoScrollcontent);
						}
						else
						{
							mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_imageH"), mailInfoScrollcontent);
						}
						mailTxt.GetComponent<Image>().sprite = sprite2;
						if (!array2[j].Equals("0"))
						{
							mailTxt.GetComponent<HighLightPic>().SetContent(array2[j]);
						}
					}
					else
					{
						mailTxt = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Browser/mail_text"), mailInfoScrollcontent);
						if (array2[j].Contains("*"))
						{
							string[] array4 = array2[j].Split('*');
							if (gameManager.dataManager.dic1.ContainsKey(array4[0]))
							{
								_ = gameManager.dataManager.dic1[array4[0]];
								mailTxt.GetComponent<MultiplyText>().SetContentPanel(mailInfoScrollcontent);
								mailTxt.GetComponent<MultiplyText>().otheritem = array4;
								mailTxt.GetComponent<MultiplyText>().SetContent2(array[j].Substring(1), array4[0], I18N.instance.getValue(array3[j]));
							}
						}
						else if (gameManager.dataManager.dic1.ContainsKey(array2[j]))
						{
							_ = gameManager.dataManager.dic1[array2[j]];
							mailTxt.GetComponent<MultiplyText>().SetContentPanel(mailInfoScrollcontent);
							mailTxt.GetComponent<MultiplyText>().SetContent2(array[j].Substring(1), array2[j], I18N.instance.getValue(array3[j]));
						}
					}
				}
				mailTxt.name = "mailInfo" + j;
				if (mailTxt.GetComponent<Button>() != null)
				{
					mailTxt.name = "link";
					mailTxt.GetComponent<Button>().onClick.RemoveAllListeners();
					mailTxt.GetComponent<Button>().onClick.AddListener(delegate
					{
						Debug.Log("需要收的信息：" + data15.ID);
						JumpLine(data15.Jump.Substring(1), data15.type, mailTxt.GetComponent<Text>().text);
					});
				}
			}
		}
		else
		{
			GameObject obj2 = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Email/" + data15.info), mailInfoScrollcontent);
			obj2.SetActive(value: true);
			obj2.name = "mailInfo1";
			LayoutRebuilder.ForceRebuildLayoutImmediate(obj2.GetComponent<RectTransform>());
		}
		mailInfoScrollcontent.GetComponent<RectTransform>().localPosition = Vector2.zero;
	}

	private void JumpLine(string jump, int type, string url = "")
	{
		Debug.Log("type：" + type);
		switch (type)
		{
		case 1:
		case 2:
			if (gameManager.homeScene.newbrowserDialog == null)
			{
				gameManager.homeScene.computerButtonBox.btn_search.SelectTool(2);
			}
			else
			{
				gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
			}
			StartCoroutine(OpenWeb(jump, url));
			break;
		case 4:
		{
			if (gameManager.homeScene.newbrowserDialog == null)
			{
				gameManager.homeScene.computerButtonBox.btn_search.SelectTool(2);
			}
			else
			{
				gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
			}
			DATA2 data = gameManager.dataManager.dic2[jump];
			gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
			break;
		}
		}
	}

	private IEnumerator OpenWeb(string jump, string url = "")
	{
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
		DATA2 data = gameManager.dataManager.dic2[jump];
		gameManager.homeScene.newbrowserDialog.AddNewPanel(data, isadmin: true);
		gameManager.homeScene.newbrowserDialog.ResumeMinimize();
	}
}
