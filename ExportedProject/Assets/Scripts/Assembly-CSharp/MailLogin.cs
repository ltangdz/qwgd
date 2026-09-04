using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MailLogin : CustomDialog
{
	public GameObject userNameInput;

	public GameObject passwordInput;

	public GameObject choiceAccount;

	public Button loginBtn;

	private BrowserMail bm;

	private DataManager dataManager;

	private bool isFocuse;

	private bool sub;

	private void Start()
	{
		gameManager.homeScene.mailLogin = this;
		dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
		userNameInput.GetComponent<InputField>().onEndEdit.AddListener(Input_End);
		passwordInput.GetComponent<InputField>().onEndEdit.AddListener(InputPassword);
		loginBtn.onClick.AddListener(delegate
		{
			SubMail();
		});
		if (gameManager.player.playerdata.isCourse08 == 0)
		{
			gameManager.homeScene.courseManager.ShowTuli6();
		}
		if (gameManager.player.playerdata.isCourse15 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel15.maillogindialog = base.gameObject;
		}
	}

	private void InputPassword(string ipt)
	{
		StartCoroutine(GetInputPassword());
	}

	private IEnumerator GetInputPassword()
	{
		yield return new WaitForSeconds(0f);
		string text = passwordInput.GetComponent<InputField>().text;
		passwordInput.transform.Find("password").GetComponent<I18NText>().updateTranslation2(text);
	}

	private void Input_End(string arg0)
	{
		StartCoroutine(GetInputEnd());
	}

	private IEnumerator GetInputEnd()
	{
		yield return new WaitForSeconds(0f);
		string text = userNameInput.GetComponent<InputField>().text;
		if (userNameInput.transform.Find("userName").GetComponent<Text>().text.Equals(""))
		{
			userNameInput.transform.Find("userName").GetComponent<I18NText>().updateTranslation2(text);
		}
		isFocuse = false;
		Invoke("Blur", 0.1f);
	}

	private void Focus()
	{
		for (int i = 0; i < choiceAccount.transform.childCount; i++)
		{
			Object.Destroy(choiceAccount.transform.GetChild(i).gameObject);
		}
		choiceAccount.SetActive(value: true);
		List<string> list = gameManager.player.playerdata.MailNamelist();
		for (int j = 0; j < list.Count; j++)
		{
			int index = j;
			string path = ((list[index] == "admin") ? "Dialog/default_account" : "Dialog/other_account");
			string text = ((list[index] == "admin") ? (gameManager.player.playerdata.nickname + "@GOmail.com") : list[j]);
			GameObject mailNameList = Object.Instantiate(Resources.Load<GameObject>(path), choiceAccount.transform);
			mailNameList.GetComponent<Account>().Reset(text, list[index]);
			mailNameList.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(text);
			mailNameList.GetComponent<Button>().onClick.RemoveAllListeners();
			mailNameList.GetComponent<Button>().onClick.AddListener(delegate
			{
				FullMailInfo(mailNameList);
			});
		}
		if (gameManager.player.GetEventId() != "110000")
		{
			choiceAccount.transform.DOScaleY(1f, 0.2f);
		}
	}

	private void FullMailInfo(GameObject mailNameList)
	{
		string mailName = mailNameList.GetComponent<Account>().MailName;
		string mailAddress = mailNameList.GetComponent<Account>().MailAddress;
		string key = ((mailName == "admin") ? "admin" : (gameManager.GetData14Prefix() + mailName));
		string password = dataManager.dic14_userid[key].password;
		StartCoroutine(SetMail(mailAddress, password, mailName));
	}

	private IEnumerator SetMail(string mailName, string pw, string address)
	{
		passwordInput.transform.Find("password").GetComponent<I18NText>().updateTranslation2(pw);
		userNameInput.transform.Find("userName").GetComponent<I18NText>().updateTranslation2(address);
		userNameInput.GetComponent<InputField>().text = mailName;
		passwordInput.GetComponent<InputField>().text = pw;
		yield return null;
	}

	private void Blur()
	{
		choiceAccount.transform.DOScaleY(0f, 0.2f);
	}

	private void Update()
	{
		if (userNameInput.GetComponent<InputField>().isFocused && !isFocuse)
		{
			Debug.Log("login update");
			isFocuse = true;
			Focus();
		}
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			float num = base.transform.GetSiblingIndex();
			float num2 = base.transform.parent.childCount;
			if (num == num2 - 1f && !sub)
			{
				SubMail();
				sub = true;
			}
		}
	}

	public void SubMail()
	{
		string text = userNameInput.GetComponent<InputField>().text;
		string text2 = passwordInput.GetComponent<InputField>().text;
		Debug.Log("邮箱:" + text + " 密码： " + text2);
		if (text == "" || text2 == "" || text2 == " " || text == " ")
		{
			passwordInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: true);
			Invoke("CloseWarding", 3f);
			return;
		}
		if (text2 == "admin")
		{
			if (text == gameManager.player.playerdata.nickname + "@GOmail.com")
			{
				text = "admin";
			}
			else
			{
				passwordInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: true);
				Invoke("CloseWarding", 3f);
			}
		}
		DATA14 dATA = ContainsKeyIgnoreCase(dataManager.dic14_userid, text);
		if ((dATA != null && dATA.eventid.ToString() == gameManager.player.GetEventId()) || text == "admin")
		{
			if (dATA.type == 2)
			{
				if (text2 == dATA.password.Replace(".0", ""))
				{
					GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/mailDialog"), gameManager.homeScene.computerButtonBox.dialogtool);
					if (!dATA.newsid.Substring(1).Equals("0"))
					{
						gameManager.homeScene.AddNews(dATA.newsid.Substring(1));
					}
					BrowserMail component = obj.GetComponent<BrowserMail>();
					component.Show();
					component.Login(dATA.user, text2);
					Hide();
				}
				else
				{
					passwordInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: true);
					Invoke("CloseWarding", 3f);
				}
			}
			else
			{
				userNameInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: true);
				Invoke("CloseWarding", 3f);
			}
		}
		else
		{
			userNameInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: true);
			Invoke("CloseWarding", 3f);
		}
	}

	private DATA14 ContainsKeyIgnoreCase(Dictionary<string, DATA14> list, string key)
	{
		string[] array = list.Keys.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Replace(gameManager.GetData14Prefix(), "").ToUpper().Equals(key.ToUpper()))
			{
				return list[array[i]];
			}
		}
		return null;
	}

	private void CloseWarding()
	{
		userNameInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: false);
		passwordInput.transform.parent.Find("wrong_warding").gameObject.SetActive(value: false);
	}

	public void ClearInputVal()
	{
		StopAllCoroutines();
		userNameInput.GetComponent<InputField>().text = "";
		passwordInput.GetComponent<InputField>().text = "";
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
