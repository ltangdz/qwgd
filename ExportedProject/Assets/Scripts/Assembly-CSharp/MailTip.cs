using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MailTip : MonoBehaviour
{
	public Text txt_title;

	public Text txt_from;

	public Text txt_subject;

	public HomeScene homeScene;

	public bool ishasclick;

	public string userid = "";

	public GameManager gameManager;

	public ComputerButton btn_mail;

	public bool isshow;

	[SerializeField]
	private string username;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void OpenMail()
	{
		if (!ishasclick)
		{
			if (gameManager.homeScene.browserMail == null)
			{
				homeScene.computerButtonBox.OpenTool(11);
			}
			else if (gameManager.homeScene.browserMail.UserMail.Equals(username))
			{
				gameManager.homeScene.browserMail.Refresh();
			}
			else if (username.Equals("admin"))
			{
				StartCoroutine(OpenNewMailDialog());
			}
			ishasclick = true;
			GetComponent<Animator>().Play("ani_hidemailtip");
			isshow = false;
		}
	}

	private IEnumerator OpenNewMailDialog()
	{
		gameManager.homeScene.browserMail.transform.SetAsLastSibling();
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.browserMail.Close();
		yield return new WaitForSeconds(1f);
		BrowserMail component = ((GameObject)Object.Instantiate(Resources.Load("Dialog/mailDialog"), gameManager.homeScene.computerButtonBox.dialogtool)).GetComponent<BrowserMail>();
		component.Show();
		component.Login("admin", "admin");
	}

	public void HideMail()
	{
		if (isshow)
		{
			ishasclick = true;
			GetComponent<Animator>().Play("ani_hidemailtip");
			isshow = false;
		}
	}

	public void SetMail(string mailAddress, string mailid)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!homeScene.gameManager.dataManager.dic15.ContainsKey(mailid) || (gameManager.player.playerdata.maillist.ContainsKey(mailAddress) && gameManager.player.playerdata.maillist[mailAddress][0].ContainsKey(mailid)))
		{
			return;
		}
		username = mailAddress;
		ishasclick = false;
		gameManager.soundManager.PlaySound(5);
		DATA15 dATA = homeScene.gameManager.dataManager.dic15[mailid];
		txt_from.text = I18N.instance.getValue(dATA.sender).Split('(')[0];
		txt_subject.GetComponent<I18NText>().updateTranslation2(dATA.title);
		GetComponent<Animator>().Play("ani_mailtip");
		isshow = true;
		gameManager.player.SendMail(mailAddress, mailid);
		if (homeScene.browserMail != null)
		{
			homeScene.browserMail.Refresh(closepanel: false);
			if (homeScene.browserMail.transform.GetSiblingIndex() != homeScene.browserMail.transform.parent.childCount - 1)
			{
				btn_mail.ShowRed(isshow: true);
			}
		}
		else
		{
			btn_mail.ShowRed(isshow: true);
		}
		base.transform.SetAsLastSibling();
	}

	public void SetMail1(string mailAddress, string mailid)
	{
		ishasclick = false;
		username = mailAddress;
		DATA15 dATA = homeScene.gameManager.dataManager.dic15[mailid];
		txt_from.text = I18N.instance.getValue(dATA.sender).Split('(')[0];
		txt_subject.GetComponent<I18NText>().updateTranslation2(dATA.title);
		GetComponent<Animator>().Play("ani_mailtip");
		isshow = true;
		gameManager.player.SendMail(mailAddress, mailid);
		if (homeScene.browserMail != null)
		{
			homeScene.browserMail.Refresh();
		}
	}
}
