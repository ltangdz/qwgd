using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanInvade : CustomDialog
{
	public InvadeCodeRun codeRunBox;

	public InputField editInput;

	public Text taskStepLabel;

	public Text taskTitleLabel;

	public List<Color> typeColor;

	public GameObject writeBox;

	public string userid;

	public List<string> allID;

	[HideInInspector]
	public bool run;

	[HideInInspector]
	public bool pojieSucc;

	public Image white;

	private Coroutine startColl;

	private List<string> taskStep = new List<string>();

	public Dictionary<string, int> fileObjList = new Dictionary<string, int>();

	public Text writeStep;

	public GameObject titleBox;

	public List<GameObject> resultObj;

	public GameObject zhadanBox;

	[SerializeField]
	private Button btn_enter;

	private List<string> urlList = new List<string>();

	public int failedTime;

	public List<string> TaskStep => taskStep;

	public void SetLastSib()
	{
		base.transform.SetAsLastSibling();
	}

	private void Start()
	{
		gameManager.homeScene.zhadanInvade = this;
		btn_close.onClick.AddListener(delegate
		{
			bk.GetComponent<ContentSizeFitter>().enabled = false;
		});
	}

	public void Init(string webID)
	{
		Debug.Log(webID);
		gameManager.CanShowSetting(1);
		StartCoroutine(InitShow());
		if (webID == "3300010" || webID == "3300008")
		{
			for (int i = 0; i < allID.Count; i++)
			{
				string url = gameManager.dataManager.dic33[allID[i]].url;
				urlList.Add(url);
			}
		}
		string url2 = gameManager.dataManager.dic33[webID].url;
		taskStep.Add(url2);
		codeRunBox.Init(userid, gameManager);
		editInput.ActivateInputField();
	}

	private IEnumerator InitShow()
	{
		codeRunBox.Run(15f, "success");
		gameManager.LightShow(titleBox);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(codeRunBox.serverName.gameObject);
		yield return new WaitForSeconds(0.5f);
		gameManager.LightShow(codeRunBox.ruqinTxt.gameObject);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(codeRunBox.logo.gameObject);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(writeBox);
	}

	private IEnumerator EndInput()
	{
		string text = editInput.text;
		bool trueInfo = false;
		bool flag = true;
		if (allID.Count != 0)
		{
			flag = IsCanJudge(text, out trueInfo);
		}
		if (!flag)
		{
			yield break;
		}
		editInput.text = "";
		editInput.ActivateInputField();
		if (pojieSucc)
		{
			yield break;
		}
		run = true;
		string text2 = ((taskStep[taskStep.Count - 1].IndexOf("^") > -1) ? I18N.instance.getValue(taskStep[taskStep.Count - 1].ToLower()) : taskStep[taskStep.Count - 1].ToLower());
		if (text.Trim().ToLower() == text2 || trueInfo)
		{
			GUIUtility.systemCopyBuffer = "";
			codeRunBox.Run(15f, "success");
			yield return new WaitForSeconds(4.2f);
			if (taskStep.Count - 1 == 0)
			{
				taskStepLabel.GetComponent<I18NText>().updateTranslation2("^invade01_label04");
				writeStep.GetComponent<I18NText>().updateTranslation2("^invade_label25");
			}
			else if (taskStep.Count - 1 == 1)
			{
				taskStepLabel.text = "";
				taskStep.Add("");
				editInput.readOnly = true;
				editInput.DeactivateInputField();
			}
		}
		else
		{
			codeRunBox.Run(5f, "failed");
			yield return new WaitForSeconds(0.9f);
			StartCoroutine(ShowWrongTips(taskStep.Count - 1));
		}
		run = false;
	}

	private bool IsCanJudge(string ipt, out bool trueInfo)
	{
		trueInfo = false;
		bool result = true;
		for (int i = 0; i < urlList.Count; i++)
		{
			if (!(ipt == I18N.instance.getValue(urlList[i])))
			{
				continue;
			}
			trueInfo = true;
			Debug.Log(allID[i]);
			if (allID[i] != userid)
			{
				if (userid == "3300010" || userid == "3300011")
				{
					result = false;
					gameManager.homeScene.ShowVideoTip("3700072");
				}
			}
			else if (userid == "3300010" || userid == "3300011")
			{
				result = false;
				gameManager.homeScene.ShowVideoTip("3700071");
			}
			userid = allID[i];
			taskStep.Clear();
			taskStep.Add(I18N.instance.getValue(urlList[i]));
		}
		return result;
	}

	private IEnumerator ShowWrongTips(int index)
	{
		resultObj[index].SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		resultObj[index].SetActive(value: false);
	}

	private void Update()
	{
	}

	public void PojieSuccess()
	{
		if (userid == "3300011" || userid == "3300009")
		{
			string[] boomList = gameManager.player.playerdata.boomList;
			for (int i = 0; i < boomList.Length; i++)
			{
				Debug.Log("userid:" + userid);
				if (boomList[i] == "3300008" && userid == "3300009")
				{
					boomList[i] = "0";
					break;
				}
				if (boomList[i] == "3300010" && userid == "3300011")
				{
					boomList[i] = "0";
					break;
				}
			}
		}
		if (userid == "3300010")
		{
			gameManager.player.playerdata.zhadanhide = true;
		}
		pojieSucc = true;
		writeBox.SetActive(value: false);
		editInput.DeactivateInputField();
		codeRunBox.Complete();
		if (userid != "3300010")
		{
			codeRunBox.HideServerBox();
		}
		else
		{
			codeRunBox.ShowWhileTrue("^zhadan_label26");
		}
		zhadanBox.SetActive(value: true);
		string file = gameManager.dataManager.dic33[userid].file;
		Object.Instantiate(Resources.Load<Transform>("zhadan/" + file), zhadanBox.transform).SetSiblingIndex(0);
		btn_close.gameObject.SetActive(value: false);
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		Init(userid);
		gameManager.homeScene.notebook.transform.SetAsLastSibling();
		bk.GetComponent<ContentSizeFitter>().enabled = true;
	}

	public void GameOver(bool isOver = true)
	{
		string[] boomList = gameManager.player.playerdata.boomList;
		for (int i = 0; i < boomList.Length; i++)
		{
			if ((bool)gameManager.homeScene.zhadanInvade)
			{
				if (boomList[i] == gameManager.homeScene.zhadanInvade.userid)
				{
					boomList[i] = "0";
					return;
				}
				continue;
			}
			List<string> openMail = gameManager.player.playerdata.OpenMail;
			if (openMail.Count != 0 && openMail[openMail.Count - 1] == "1500089")
			{
				gameManager.StopRecordTime();
				boomList[3] = "0";
				boomList[4] = "0";
			}
			return;
		}
		gameManager.player.playerdata.isTriggerBoom = !isOver;
	}
}
