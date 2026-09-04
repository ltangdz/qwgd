using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class FishPhoneInvadeDialog : CustomDialog
{
	public InvadePojieBox pojieBox;

	public InvadeCodeRun codeRunBox;

	public InputField editInput;

	public GameObject writeBox;

	public List<GameObject> resultObj;

	[HideInInspector]
	public string userid;

	[HideInInspector]
	public bool run;

	[HideInInspector]
	public bool pojieSucc;

	public GameObject titleBox;

	public GameObject mail;

	[SerializeField]
	private Button btn_enter;

	public void SetLastSib()
	{
		base.transform.SetAsLastSibling();
	}

	public void Init(string webID, bool pojieResult)
	{
		userid = webID;
		gameManager.CanShowSetting(1);
		pojieSucc = pojieResult;
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
		gameManager.LightShow(pojieBox.resultBox.gameObject);
		yield return new WaitForSeconds(0.3f);
		if (gameManager.IsAllDlc())
		{
			StartCoroutine(Run());
			yield break;
		}
		gameManager.LightShow(writeBox);
		yield return new WaitForSeconds(0.2f);
		mail.SetActive(value: true);
	}

	private IEnumerator Run()
	{
		string value = I18N.instance.getValue(gameManager.dataManager.dic33[userid].url);
		string text = editInput.text;
		if (gameManager.Is_Dlc6())
		{
			text = value;
		}
		editInput.text = "";
		editInput.DeactivateInputField();
		editInput.readOnly = true;
		resultObj[0].SetActive(value: true);
		if (text.Trim().ToLower() == value.Trim().ToLower())
		{
			pojieBox.StepLoading(1, sce: true);
			codeRunBox.Run(10f, "success");
			yield return new WaitForSeconds(3f);
			pojieBox.Step(1, pojieSql: false);
			resultObj[0].SetActive(value: false);
			resultObj[1].SetActive(value: true);
			if (pojieSucc)
			{
				pojieBox.StepLoading(2, sce: true);
				codeRunBox.Run(10f, "success");
				yield return new WaitForSeconds(3f);
				pojieBox.Step(2, pojieSql: false);
				resultObj[1].SetActive(value: false);
				resultObj[2].SetActive(value: true);
				pojieBox.StepLoading(3, sce: true);
				codeRunBox.Run(10f, "success");
				yield return new WaitForSeconds(3f);
				pojieBox.Step(3, pojieSql: false);
				resultObj[2].SetActive(value: false);
				resultObj[3].SetActive(value: true);
				codeRunBox.Run(10f, "success");
				yield return new WaitForSeconds(2f);
				codeRunBox.Run(5f, "success");
				Debug.LogError("入侵成功");
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/invadephoneDialog" + userid), base.transform.parent);
				obj.GetComponent<InvadePhoneDialog>().Init();
				obj.GetComponent<InvadePhoneDialog>().Show();
				yield return new WaitForSeconds(1f);
				codeRunBox.serverName.transform.parent.gameObject.SetActive(value: false);
				gameManager.CanShowSetting(-1);
				Hide();
				gameManager.homeScene.notebook.transform.SetAsLastSibling();
			}
			else
			{
				pojieBox.StepLoading(2, sce: false);
				codeRunBox.Run(5f, "failed");
				yield return new WaitForSeconds(2f);
				resultObj[1].SetActive(value: false);
				resultObj[4].SetActive(value: true);
				pojieBox.CompleteStep(0);
				yield return new WaitForSeconds(2f);
				gameManager.homeScene.StartVideoDialog("videoDialogtaskfailed", "invadephone");
				gameManager.CanShowSetting(-1);
				Hide();
			}
		}
		else
		{
			pojieBox.StepLoading(1, sce: false);
			codeRunBox.Run(6f, "failed");
			yield return new WaitForSeconds(2f);
			resultObj[0].SetActive(value: false);
			resultObj[5].SetActive(value: true);
			yield return new WaitForSeconds(1.5f);
			resultObj[5].SetActive(value: false);
			editInput.ActivateInputField();
			run = false;
		}
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
		if (!gameManager.player.playerdata.videotiplist.Contains("3700049") && gameManager.player.GetEventId() == "110002")
		{
			gameManager.homeScene.ShowVideoTip("3700049");
		}
		StartCoroutine(InitShow());
		codeRunBox.Init(userid, gameManager);
		pojieBox.Init(0, gameManager);
		gameManager.homeScene.fishPhoneInvadeDialog = this;
		btn_enter.onClick.AddListener(PressEnter);
	}

	private void PressEnter()
	{
		float num = base.transform.GetSiblingIndex();
		float num2 = base.transform.parent.childCount;
		if (num == num2 - 1f && !run)
		{
			run = true;
			StartCoroutine(Run());
		}
	}
}
