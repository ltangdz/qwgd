using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class Login : MonoBehaviour
{
	public Transform userName;

	public Transform password;

	public Transform loginBtn;

	public Transform login;

	public Transform forgetPassword;

	public Transform mailChange;

	public Sprite inputWrongBox;

	public Sprite inputBox;

	public Transform inputWarding;

	public Transform inputWardingBox;

	public Transform emailWarding;

	public Transform emailWardingBox;

	public Text txt_emailwarding;

	public Button submitBtn;

	public Button cancelBtn;

	public Transform passwordInput;

	public GameObject stopSecondSend;

	public BrowserDialog bd;

	private string userNameInfo;

	private string passwordInfo;

	private string changePw;

	private Sprite anginBtn;

	private Transform changeBtn;

	private DataManager dataManager;

	private GameManager gameManager;

	private string eventID;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		dataManager = gameManager.dataManager;
		eventID = gameManager.player.GetEventId();
		userName.GetComponent<InputField>().onEndEdit.AddListener(UserEndEdit);
		password.GetComponent<InputField>().onEndEdit.AddListener(PasswordEndEdit);
		loginBtn.GetComponent<Button>().onClick.AddListener(Submit);
		login.Find("forget_password").GetComponent<Button>().onClick.AddListener(ForgetPassword);
	}

	private void OnEnable()
	{
		login.gameObject.SetActive(value: true);
		forgetPassword.gameObject.SetActive(value: false);
		mailChange.gameObject.SetActive(value: false);
		stopSecondSend.gameObject.SetActive(value: false);
	}

	private void UserEndEdit(string inp)
	{
		userNameInfo = inp;
	}

	private void PasswordEndEdit(string inp)
	{
		passwordInfo = inp;
	}

	private void Submit()
	{
		userNameInfo = userName.GetComponent<InputField>().text.ToLower();
		passwordInfo = password.GetComponent<InputField>().text;
		if (dataManager.dic11[eventID].tbnum.Equals(""))
		{
			if (!inputWarding.gameObject.activeInHierarchy)
			{
				inputWarding.gameObject.SetActive(value: true);
				inputWardingBox.gameObject.SetActive(value: true);
				password.GetComponent<Image>().sprite = inputWrongBox;
				Invoke("ValChange", 3f);
			}
			return;
		}
		string[] array = dataManager.dic11[eventID].tbnum.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (userNameInfo == dataManager.dic14[array[i]].user.ToLower() && passwordInfo == dataManager.dic14[array[i]].password)
			{
				gameManager.homeScene.newbrowserDialog.AddNewPanel(dataManager.dic2[dataManager.dic14[array[i]].data2ID.ToString()], isadmin: true);
				Debug.Log("登录成功" + userNameInfo);
				if (gameManager.player.GetEventId().Equals("110004") && userNameInfo.ToLower().Equals("tb3994002"))
				{
					gameManager.UnlockAchievements("event4helen");
				}
			}
			else if (!inputWarding.gameObject.activeInHierarchy)
			{
				inputWarding.gameObject.SetActive(value: true);
				inputWardingBox.gameObject.SetActive(value: true);
				password.GetComponent<Image>().sprite = inputWrongBox;
				Invoke("ValChange", 3f);
			}
		}
	}

	private void CancelPassword()
	{
		login.gameObject.SetActive(value: true);
		forgetPassword.gameObject.SetActive(value: false);
	}

	public void SetItem(GameObject tile, DATA1 data)
	{
		if (tile.name == "password")
		{
			tile.transform.Find("Text").GetComponent<TypewriterEffect>().StartEffect("······");
		}
		else
		{
			tile.transform.Find("Text").GetComponent<TypewriterEffect>().StartEffect(I18N.instance.getValue(data.message));
		}
		tile.transform.Find("placeholder").GetComponent<I18NText>().updateTranslation2(data.message);
		tile.transform.Find("placeholder").gameObject.SetActive(value: false);
		changePw = passwordInput.Find("Text").GetComponent<Text>().text;
	}

	private void SubmitNewPassword()
	{
		changePw = passwordInput.Find("Text").GetComponent<Text>().text;
		if (!(changePw != ""))
		{
			return;
		}
		string text = gameManager.GetData14Prefix() + changePw;
		Debug.Log("data14Prefix:" + text);
		DATA14 dATA = (gameManager.dataManager.dic14_userid.ContainsKey(text) ? gameManager.dataManager.dic14_userid[text] : null);
		if (gameManager.dataManager.dic14_userid.ContainsKey(text) && dATA != null && dATA.type == 1)
		{
			if (!gameManager.player.playerdata.sendMess.ContainsKey(changePw))
			{
				if (dATA.email != "")
				{
					if (gameManager.homeScene.mailTip.userid.Equals(I18N.instance.getValue(dATA.email)))
					{
						gameManager.homeScene.mailTip.SetMail1(I18N.instance.getValue(dATA.email), dATA.findpassword.Substring(1));
						gameManager.player.playerdata.sendMess.Add(changePw, 1);
						if (!gameManager.dataManager.dic15[dATA.findpassword.Substring(1)].missionID.Equals(""))
						{
							gameManager.homeScene.goalDialog.CompleteItem(gameManager.dataManager.dic15[dATA.findpassword.Substring(1)].missionID.Substring(1));
						}
						if (gameManager.player.GetEventId().Equals("110001"))
						{
							gameManager.homeScene.ShowVideoTip("3700009");
						}
					}
					else
					{
						gameManager.player.SendMail(I18N.instance.getValue(dATA.email), dATA.findpassword.Substring(1));
						if (!gameManager.dataManager.dic15[dATA.findpassword.Substring(1)].missionID.Equals(""))
						{
							gameManager.homeScene.goalDialog.CompleteItem(gameManager.dataManager.dic15[dATA.findpassword.Substring(1)].missionID.Substring(1));
						}
						gameManager.player.playerdata.sendMess.Add(changePw, 1);
					}
					SetURLUnderLine(dATA.email);
					mailChange.gameObject.SetActive(value: true);
					Debug.Log("发送成功");
					forgetPassword.gameObject.SetActive(value: false);
				}
				else if (!emailWarding.gameObject.activeInHierarchy)
				{
					emailWarding.gameObject.SetActive(value: true);
					emailWardingBox.gameObject.SetActive(value: true);
					txt_emailwarding.text = I18N.instance.getValue("^emailpasswordfail");
					Invoke("PasswordChange", 3f);
				}
			}
			else
			{
				stopSecondSend.SetActive(value: true);
				forgetPassword.gameObject.SetActive(value: false);
			}
		}
		else if (!emailWarding.gameObject.activeInHierarchy)
		{
			emailWarding.gameObject.SetActive(value: true);
			emailWardingBox.gameObject.SetActive(value: true);
			txt_emailwarding.text = I18N.instance.getValue("^email_wrong");
			Invoke("PasswordChange", 3f);
		}
	}

	private void LineToMail()
	{
		gameManager.homeScene.computerButtonBox.btn_mail.SelectTool(10);
		StartCoroutine(ShowLog());
	}

	private IEnumerator ShowLog()
	{
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.browserMail.ChangeAccount();
	}

	private void ForgetPassword()
	{
		login.gameObject.SetActive(value: false);
		forgetPassword.gameObject.SetActive(value: true);
		passwordInput.GetComponent<InputField>().text = "";
		submitBtn.GetComponent<Button>().onClick.RemoveListener(SubmitNewPassword);
		cancelBtn.GetComponent<Button>().onClick.RemoveListener(CancelPassword);
		submitBtn.GetComponent<Button>().onClick.AddListener(SubmitNewPassword);
		cancelBtn.GetComponent<Button>().onClick.AddListener(CancelPassword);
	}

	private void SetURLUnderLine(string emailadd)
	{
		emailadd = ((emailadd.IndexOf("^") > -1) ? I18N.instance.getValue(emailadd) : emailadd);
		int num = emailadd.IndexOf("@");
		string oldValue = emailadd.Substring(num - 4, 4);
		emailadd = emailadd.Replace(oldValue, "****");
		mailChange.Find("mail_info").GetComponent<I18NText>().updateTranslation2(emailadd);
		float preferredWidth = mailChange.Find("mail_info").GetComponent<Text>().preferredWidth;
		Vector2 sizeDelta = mailChange.Find("mail_info/img_line").GetComponent<RectTransform>().sizeDelta;
		mailChange.Find("mail_info/img_line").GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, sizeDelta.y);
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.KeypadEnter) && !Input.GetKeyDown(KeyCode.Return))
		{
			return;
		}
		float num = gameManager.homeScene.newbrowserDialog.transform.GetSiblingIndex();
		float num2 = gameManager.homeScene.newbrowserDialog.transform.parent.childCount;
		Debug.Log(num + " " + num2);
		if (num == num2 - 1f)
		{
			if (login.gameObject.activeInHierarchy)
			{
				Submit();
			}
			else if (forgetPassword.gameObject.activeInHierarchy)
			{
				SubmitNewPassword();
			}
		}
	}

	private void ValChange()
	{
		if (inputWarding.gameObject.activeInHierarchy)
		{
			inputWarding.gameObject.SetActive(value: false);
			inputWardingBox.gameObject.SetActive(value: false);
			password.GetComponent<Image>().sprite = inputBox;
		}
	}

	private void PasswordChange()
	{
		emailWarding.gameObject.SetActive(value: false);
		emailWardingBox.gameObject.SetActive(value: false);
	}

	public void SetFront()
	{
		bd.SetFront();
	}
}
