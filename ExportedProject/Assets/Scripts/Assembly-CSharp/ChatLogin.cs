using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ChatLogin : CustomDialog
{
	public Transform userName;

	public Transform password;

	public GameObject wrongLabel;

	public Button loginBtn;

	public GameObject logo;

	public GameObject img_logocircle;

	public Sprite fakeLogo;

	public GameObject loginBox;

	public GameObject changePasswordBox;

	public GameObject changeSuccess;

	public Button forgetBtn;

	public InputField changeUser;

	public GameObject changeWrongWarning;

	public Button succBtn;

	public Button bakBtn;

	private string eventID;

	private bool login;

	private string speech;

	private bool LogAble;

	private string logUserID;

	public Sprite[] sprites;

	private bool isFocuse;

	public string Speech
	{
		get
		{
			return speech;
		}
		set
		{
			speech = value;
		}
	}

	private void Start()
	{
		gameManager.istaohuashow = true;
		loginBtn.onClick.AddListener(Submit);
		eventID = gameManager.player.GetEventId();
		userName.GetComponent<InputField>().onEndEdit.AddListener(Input_End);
		changeUser.onValueChanged.AddListener(ChangeUser);
		btn_close.onClick.AddListener(delegate
		{
			gameManager.istaohuashow = false;
		});
	}

	private void ChangeUser(string ipt)
	{
		if (ipt.Length > 0)
		{
			succBtn.GetComponent<Image>().sprite = sprites[1];
		}
		else
		{
			succBtn.GetComponent<Image>().sprite = sprites[0];
		}
	}

	private void Input_End(string str)
	{
		Invoke("Blur", 0.5f);
	}

	private void Blur()
	{
		isFocuse = false;
	}

	public void SetYellow()
	{
		if (!userName.GetComponent<InputField>().text.Equals("") && !password.GetComponent<InputField>().text.Equals(""))
		{
			loginBtn.image.sprite = sprites[1];
		}
		else
		{
			loginBtn.image.sprite = sprites[0];
		}
	}

	private void Submit()
	{
		if (login)
		{
			return;
		}
		login = true;
		StopAllCoroutines();
		string text = userName.GetComponent<InputField>().text.Trim();
		string text2 = password.GetComponent<InputField>().text.Trim();
		if (text != "" && text != " " && text2 != "" && text2 != " ")
		{
			wrongLabel.SetActive(value: false);
			List<DATA14> data14ByEventid = gameManager.dataManager.GetData14ByEventid(eventID);
			for (int i = 0; i < data14ByEventid.Count; i++)
			{
				if (data14ByEventid[i].type == 3)
				{
					if (text2 == "chatAdmin" && text == "hack_" + gameManager.player.playerdata.nickname)
					{
						text = "chatAdmin";
					}
					Debug.Log(data14ByEventid[i].user.Split('.')[0] + " " + data14ByEventid[i].password);
					Debug.Log("@@" + text + "::" + text2);
					if (text == data14ByEventid[i].user.Split('.')[0] && text2 == data14ByEventid[i].password.Split('.')[0])
					{
						if (text == "chatAdmin")
						{
							speech = "2";
						}
						else
						{
							speech = "1";
						}
						StartCoroutine(StartLoad(data14ByEventid[i].ID.ToString(), isweizhuang: false));
						gameManager.player.playerdata.chatLoginID = data14ByEventid[i].ID.ToString();
						wrongLabel.SetActive(value: false);
						break;
					}
					StartCoroutine(LoginWrong());
					login = false;
				}
				else
				{
					StartCoroutine(LoginWrong());
					login = false;
				}
			}
		}
		else
		{
			StartCoroutine(LoginWrong());
			login = false;
		}
	}

	public void ChatBoxLogin(string userID, string speechType, string frdID = "", string chatID = "")
	{
		speech = speechType;
		StartCoroutine(StartLoad(userID, isweizhuang: false, frdID, chatID));
	}

	public void DirectLogin(string userID, bool isweizhuang)
	{
		speech = "0";
		StartCoroutine(StartLoad(userID, isweizhuang));
	}

	public void ShowBox(GameObject obj)
	{
		loginBox.SetActive(value: false);
		changePasswordBox.SetActive(value: false);
		changeSuccess.SetActive(value: false);
		obj.SetActive(value: true);
		changeUser.text = "";
	}

	public void ChagePassword()
	{
		string text = changeUser.text;
		List<DATA14> data14ByEventid = gameManager.dataManager.GetData14ByEventid(gameManager.player.GetEventId());
		for (int i = 0; i < data14ByEventid.Count; i++)
		{
			if (data14ByEventid[i].user.Split('.')[0] == text.Trim() && data14ByEventid[i].type == 3)
			{
				ShowBox(changeSuccess);
				return;
			}
		}
		changeWrongWarning.SetActive(value: true);
		StartCoroutine(TimeHideObj(changeWrongWarning));
	}

	private IEnumerator TimeHideObj(GameObject obj)
	{
		yield return new WaitForSeconds(2f);
		obj.SetActive(value: false);
	}

	public void DirectLoginDLC(string userID, DATA3 data3)
	{
		speech = "0";
		StartCoroutine(StartLoad(userID, isweizhuang: true, data3.ID.ToString(), data3.reply.Substring(1), data3));
	}

	private IEnumerator StartLoad(string userID, bool isweizhuang, string frdID = "", string chatID = "", DATA3 data3 = null)
	{
		if (isweizhuang)
		{
			logo.GetComponent<Image>().sprite = fakeLogo;
		}
		login = true;
		userName.transform.parent.gameObject.SetActive(value: false);
		password.transform.parent.gameObject.SetActive(value: false);
		loginBtn.gameObject.SetActive(value: false);
		forgetBtn.gameObject.SetActive(value: false);
		float time = 0.2f;
		yield return new WaitForSeconds(time);
		logo.transform.DOLocalMoveY(0f, time);
		logo.transform.DOScaleX(1.3f, time);
		logo.transform.DOScaleY(1.3f, time);
		yield return new WaitForSeconds(time);
		img_logocircle.transform.DOLocalRotate(new Vector3(0f, 0f, -1440f), 0.5f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Chat/chatDialog"), base.transform.parent);
			gameObject.GetComponent<ChatBox>().Show();
			gameObject.GetComponent<ChatBox>().toolid = ((!isweizhuang) ? 1 : 0);
			if (speech == "0")
			{
				if (gameManager.IsBasic())
				{
					string frd = gameManager.dataManager.dic14[userID].discussid.Substring(1);
					string chat = gameManager.dataManager.dic3[gameManager.dataManager.dic14[userID].user.Substring(1)].reply.Substring(1);
					gameObject.GetComponent<ChatBox>().Init(userID, speech, frd, chat, data3);
				}
				else
				{
					gameObject.GetComponent<ChatBox>().Init(userID, speech, frdID, chatID, data3);
				}
				gameManager.homeScene.weizhuangDialog = gameObject.GetComponent<ChatBox>();
			}
			else
			{
				gameObject.GetComponent<ChatBox>().Init(userID, speech, frdID, chatID, data3);
				gameManager.homeScene.chatDialog = gameObject.GetComponent<ChatBox>();
			}
			Hide();
			login = false;
		});
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			float num = base.transform.GetSiblingIndex();
			float num2 = base.transform.parent.childCount;
			if (num == num2 - 1f && loginBox.activeInHierarchy)
			{
				Submit();
			}
			if (num == num2 - 1f && changePasswordBox.activeInHierarchy)
			{
				ChagePassword();
			}
		}
		if (userName.transform.GetComponent<InputField>().isFocused && !isFocuse)
		{
			isFocuse = true;
			Focus();
		}
	}

	private void Focus()
	{
	}

	private void FullMailInfo()
	{
		userName.GetComponent<InputField>().text = "hack_" + gameManager.player.playerdata.nickname;
		password.GetComponent<InputField>().text = "chatAdmin";
	}

	private IEnumerator LoginWrong()
	{
		wrongLabel.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		wrongLabel.SetActive(value: false);
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
