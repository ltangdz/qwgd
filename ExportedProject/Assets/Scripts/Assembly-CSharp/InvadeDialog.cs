using System;
using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InvadeDialog : CustomDialog
{
	public InvadePojieBox pojieBox;

	public InvadeListBox listBox;

	public InvadeCodeRun codeRunBox;

	public InputField editInput;

	public Text taskStepLabel;

	public Text taskTitleLabel;

	public List<Color> typeColor;

	public DownloadDialog downloadDialog;

	public GameObject writeBox;

	[HideInInspector]
	public string userid;

	[HideInInspector]
	public bool run;

	[HideInInspector]
	public bool pojieSucc;

	[HideInInspector]
	public GameObject secFile;

	public GameObject countDown;

	private Coroutine startColl;

	private GameObject soundPassword;

	public List<InvadeFileBox> itemFileBox;

	private List<string> taskStep = new List<string>();

	public Dictionary<string, int> fileObjList = new Dictionary<string, int>();

	public Text writeStep;

	public GameObject titleBox;

	public List<GameObject> resultObj;

	[SerializeField]
	private Button btn_enter;

	public bool selectTaskOver;

	private bool yinpinSuc;

	private bool isLoading = true;

	public List<string> TaskStep => taskStep;

	public void SetLastSib()
	{
		base.transform.SetAsLastSibling();
	}

	public void Init(string webID)
	{
		Debug.Log(webID);
		string mission = gameManager.dataManager.dic33[userid].mission;
		string[] itemids = (string.IsNullOrEmpty(mission) ? new string[0] : mission.Substring(1).Split(';'));
		gameManager.homeScene.notebook.DeleteSpecialItem(itemids);
		gameManager.homeScene.invadeDialog = this;
		gameManager.CanShowSetting(1);
		StartCoroutine(InitShow());
		if ((bool)listBox)
		{
			listBox.Init(webID, gameManager, this);
		}
		string url = gameManager.dataManager.dic33[webID].url;
		taskStep.Add(url);
		codeRunBox.Init(userid, gameManager, this);
		pojieBox.Init(0, gameManager, this);
		editInput.ActivateInputField();
	}

	private IEnumerable DLC7ShowTaskLabel()
	{
		string content = I18N.instance.getValue("^110008_invade_3");
		string[] str = new string[3] { ".", "..", "..." };
		while (isLoading)
		{
			for (int i = 0; i < 3; i++)
			{
				taskStepLabel.text = $"{content}{str[i]}";
				yield return new WaitForSeconds(0.5f);
			}
		}
	}

	private void ShowTaskLabelDLC()
	{
		Debug.Log("ShowTaskLabelDLC");
		StartCoroutine("DLC7ShowTaskLabel");
	}

	private IEnumerator AutoShow()
	{
		GUIUtility.systemCopyBuffer = "";
		taskStep.Add("1");
		pojieBox.StepLoading(taskStep.Count, sce: true);
		codeRunBox.Run(15f, "success");
		taskStepLabel.GetComponent<I18NText>().updateTranslation2("^110008_invade_3");
		writeStep.GetComponent<I18NText>().updateTranslation2("^invade_label25");
		ShowTaskLabelDLC();
		yield return new WaitForSeconds(3f);
		pojieBox.Step(1);
		string truepassword = pojieBox.truepassword;
		taskStep.Add(truepassword);
		pojieBox.StepLoading(taskStep.Count, sce: true);
		yield return new WaitForSeconds(4f);
		taskStepLabel.text = "";
		pojieBox.Step(2);
		taskStep.Add("");
		pojieBox.SetInvadeDialog(this);
		pojieBox.StepLoading(taskStep.Count, sce: true);
	}

	private IEnumerator InitShow()
	{
		codeRunBox.Run(15f, "success");
		gameManager.LightShow(titleBox);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(codeRunBox.serverName.gameObject);
		pojieBox.InitShow();
		yield return new WaitForSeconds(0.5f);
		gameManager.LightShow(codeRunBox.ruqinTxt.gameObject);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(codeRunBox.logo.gameObject);
		gameManager.LightShow(pojieBox.resultBox.gameObject);
		yield return new WaitForSeconds(0.3f);
		gameManager.LightShow(writeBox);
		if (gameManager.Is_Dlc7())
		{
			yield return new WaitForSeconds(1f);
			pojieSucc = true;
			taskStep.Clear();
			StartCoroutine("AutoShow");
		}
	}

	private IEnumerator EndInput()
	{
		string text = editInput.text;
		editInput.text = "";
		editInput.ActivateInputField();
		if (pojieSucc)
		{
			if (text.Trim().ToLower().Equals("quit") && (downloadDialog.file.Count == downloadDialog.loadingFile.Count || downloadDialog.file.Count == 0))
			{
				RunListBox(taskOver: true);
			}
			yield break;
		}
		run = true;
		Debug.Log(text.Trim().ToLower() + " " + taskStep[taskStep.Count - 1]);
		if (text.Trim().ToLower() == taskStep[taskStep.Count - 1].ToLower() || text.Trim().ToLower() == "https://" + taskStep[taskStep.Count - 1].ToLower())
		{
			GUIUtility.systemCopyBuffer = "";
			pojieBox.StepLoading(taskStep.Count, sce: true);
			codeRunBox.Run(15f, "success");
			yield return new WaitForSeconds(4.2f);
			if (taskStep.Count - 1 == 0)
			{
				pojieBox.Step(1);
				string truepassword = pojieBox.truepassword;
				taskStep.Add(truepassword);
				taskStepLabel.GetComponent<I18NText>().updateTranslation2("^invade01_label04");
				writeStep.GetComponent<I18NText>().updateTranslation2("^invade_label25");
			}
			else if (taskStep.Count - 1 == 1)
			{
				taskStepLabel.text = "";
				pojieBox.Step(2);
				taskStep.Add("");
				editInput.readOnly = true;
				editInput.DeactivateInputField();
			}
		}
		else
		{
			pojieBox.StepLoading(taskStep.Count, sce: false);
			codeRunBox.Run(5f, "failed");
			yield return new WaitForSeconds(0.9f);
			StartCoroutine(ShowWrongTips(taskStep.Count - 1));
		}
		run = false;
	}

	private IEnumerator ShowWrongTips(int index)
	{
		resultObj[index].SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		resultObj[index].SetActive(value: false);
	}

	public void ComSucc()
	{
		if (gameManager.Is_Dlc7())
		{
			gameManager.musicManager.Stop();
			SceneManager.LoadSceneAsync("DDOS");
			HideDialog();
			return;
		}
		switch (gameManager.dataManager.dic33[userid].type)
		{
		case 0:
			PojieSuccess();
			break;
		case 1:
			gameManager.homeScene.ShowVideoTip("3700035");
			pojieBox.gameObject.SetActive(value: false);
			codeRunBox.serverName.transform.parent.gameObject.SetActive(value: false);
			soundPassword = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Dialog/invade_soundpassword"), content);
			writeBox.transform.SetAsLastSibling();
			codeRunBox.Complete(success: false);
			writeBox.transform.Find("invadein_input").gameObject.SetActive(value: false);
			writeBox.transform.Find("btn_sound").gameObject.SetActive(value: true);
			writeBox.transform.Find("btn_sound").GetComponent<Button>().onClick.AddListener(delegate
			{
				if (!yinpinSuc)
				{
					StartCoroutine(JudgeIfSuccess());
				}
			});
			break;
		}
	}

	private IEnumerator JudgeIfSuccess()
	{
		if (soundPassword != null)
		{
			bool suc = false;
			float value = soundPassword.GetComponent<InvadePojieBox>().slider_hor.value;
			float value2 = soundPassword.GetComponent<InvadePojieBox>().slider_ver.value;
			if ((double)value >= 0.78 && (double)value <= 0.81 && (double)value2 >= 0.75 && value2 <= 1f)
			{
				suc = true;
				writeBox.transform.Find("btn_sound").GetComponent<Button>().interactable = false;
			}
			soundPassword.GetComponent<InvadePojieBox>().loadBox.gameObject.SetActive(value: true);
			gameManager.homeScene.eventsystem.SetActive(value: false);
			soundPassword.GetComponent<InvadePojieBox>().loadBox.Loading(suc);
			yield return new WaitForSeconds(3.5f);
			if (suc)
			{
				UnityEngine.Object.Destroy(soundPassword);
				writeBox.transform.Find("btn_sound").gameObject.SetActive(value: false);
				PojieSuccess();
			}
		}
		else
		{
			Debug.LogError("soundPasword没有");
		}
	}

	private void PojieSuccess()
	{
		yinpinSuc = true;
		downloadDialog = UnityEngine.Object.Instantiate(Resources.Load<DownloadDialog>("Dialog/downloadDialog"), gameManager.homeScene.middle);
		string[] collectID = gameManager.dataManager.dic33[userid].collect.Substring(1).Split(';');
		downloadDialog.Init(collectID, gameManager, this);
		pojieSucc = true;
		writeBox.SetActive(value: false);
		pojieBox.gameObject.SetActive(value: false);
		listBox.gameObject.SetActive(value: true);
		listBox.Init(" ", gameManager, this);
		listBox.ShowRoot();
		editInput.DeactivateInputField();
		codeRunBox.Complete();
		RunListBox();
	}

	private void RunListBox(bool taskOver = false)
	{
		if (!taskOver)
		{
			GameObject obj = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("invade_alert"), content);
			obj.GetComponent<InvadeAlert>().Init("^invade_label29");
			countDown.GetComponent<CountDownDialog>().SetTime(60);
			countDown.GetComponent<CountDownDialog>().PauseTime();
			obj.GetComponent<InvadeAlert>().btnOk.onClick.AddListener(delegate
			{
				CountDownDialog component = countDown.GetComponent<CountDownDialog>();
				component.callBak = (CountDownDialog.CallBak)Delegate.Combine(component.callBak, new CountDownDialog.CallBak(RunCountDown));
				countDown.GetComponent<CountDownDialog>().RestartTime();
			});
		}
		if (taskOver)
		{
			gameManager.CanShowSetting(-1);
			if (gameManager.player.GetEventId().Equals("110002"))
			{
				PlayVideo();
				gameManager.player.playerdata.delSec.Add(userid, value: true);
			}
			gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[userid].name] = 1;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void RunCountDown()
	{
		Debug.Log("任务结束");
		StartCoroutine(CountDown());
	}

	private IEnumerator CountDown()
	{
		GameObject alert1 = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("invade_alert"), content);
		alert1.GetComponent<InvadeAlert>().Init("^invade_label41", base.gameObject);
		codeRunBox.TaskOver();
		Debug.Log("over");
		if (selectTaskOver)
		{
			Debug.Log("当前事件id" + gameManager.player.GetEventId());
			if (gameManager.player.GetEventId().Equals("110002"))
			{
				PlayVideo();
				gameManager.player.playerdata.delSec.Add(userid, value: true);
			}
			gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[userid].name] = 1;
		}
		else
		{
			if (downloadDialog.gameObject.activeInHierarchy)
			{
				downloadDialog.LoadFailed();
			}
			gameManager.player.playerdata.delSec.Add(userid, value: false);
			GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			GetComponent<CanvasGroup>().alpha = 0f;
			content.gameObject.SetActive(value: false);
			LoadFailed();
			gameManager.homeScene.notebook.DeleteInvadeServerItem();
		}
		yield return new WaitForSeconds(1f);
		gameManager.CanShowSetting(-1);
		UnityEngine.Object.Destroy(alert1);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void PlayVideo()
	{
		Debug.Log("增加新cio");
		gameManager.homeScene.ShowVideoTip("3700043");
	}

	private void LoadFailed()
	{
		gameManager.player.playerdata.delSec.Remove(userid);
		gameManager.player.playerdata.fishLink.Remove(gameManager.dataManager.dic33[userid].name);
		gameManager.homeScene.StartVideoDialog("videoDialogtaskfailed", "invade");
	}

	public void DelFile(string itemid)
	{
		for (int i = 0; i < itemFileBox.Count; i++)
		{
			Debug.Log(itemFileBox[i].listID + " : " + itemid);
			string del = gameManager.dataManager.dic42[itemFileBox[i].listID].del;
			if (del != "" && del.Substring(1) == itemid)
			{
				Debug.Log("删除：" + itemFileBox[i].listID);
				fileObjList[itemFileBox[i].listID] = 1;
				countDown.GetComponent<CountDownDialog>().PauseTime();
				Invoke("RestartTime", 4.5f);
				listBox.CompleteTask();
				UnityEngine.Object.Destroy(itemFileBox[i].gameObject);
				itemFileBox.RemoveAt(i);
			}
		}
		float num = listBox.fileInfoBox.transform.childCount;
		for (int j = 0; (float)j < num; j++)
		{
			UnityEngine.Object.Destroy(listBox.fileInfoBox.transform.GetChild(j).gameObject);
		}
	}

	private void RestartTime()
	{
		countDown.GetComponent<CountDownDialog>().RestartTime();
	}

	public void TaskOver()
	{
		Debug.Log("立即退出" + downloadDialog.file.Count);
		RunListBox(taskOver: true);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			PressEnter();
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		Debug.Log("初始化");
		Init(userid);
		gameManager.homeScene.notebook.transform.SetAsLastSibling();
		bk.GetComponent<ContentSizeFitter>().enabled = true;
		btn_enter.onClick.AddListener(PressEnter);
	}

	private void PressEnter()
	{
		float num = base.transform.GetSiblingIndex();
		float num2 = base.transform.parent.childCount;
		if ((num == num2 - 2f || num == num2 - 1f) && !run)
		{
			Debug.Log("回车");
			StartCoroutine(EndInput());
		}
	}
}
