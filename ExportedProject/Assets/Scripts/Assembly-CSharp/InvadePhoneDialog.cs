using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadePhoneDialog : CustomDialog
{
	[SerializeField]
	private Text txt_phonename;

	[SerializeField]
	private Text txt_warning;

	[SerializeField]
	private Text txt_status;

	[SerializeField]
	private Text txt_count;

	[SerializeField]
	private Animator ani_status;

	[SerializeField]
	private Image img_logo;

	[SerializeField]
	private Button btn_exit;

	[SerializeField]
	private GameObject lockpanel;

	[SerializeField]
	private ScrollRect resultscrollview;

	[SerializeField]
	private Button btn_unlock;

	[SerializeField]
	private Transform img_slider;

	[SerializeField]
	private Transform img_slidermask;

	public Coderunpanel coderunpanel;

	private int currentappid = -1;

	[SerializeField]
	private List<string> needcollectitem = new List<string>();

	[SerializeField]
	private string userID;

	[SerializeField]
	private Color redcolor;

	[SerializeField]
	private Color greencolor;

	public bool isWifi;

	public InvadeOpenLock suoping;

	public GameObject littlewindow;

	private AppButton btnObj;

	private void RefreshStatus()
	{
		if (gameManager.GameType == GameTypeEnum.DLC6 || gameManager.GameType == GameTypeEnum.DLC7)
		{
			txt_warning.DOText(I18N.instance.getValue("^499D8D01-3B7E-1A3B-2190-FD4EF10919FE"), 0.5f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(txt_warning.DOFade(0.5f, 0.5f));
			sequence.Append(txt_warning.DOFade(1f, 0.5f));
			sequence.SetLoops(-1);
			DOTween.Sequence().Append(img_slider.DOMoveX(img_slider.position.x - 22f, 0.5f));
			sequence.SetLoops(-1);
		}
		else
		{
			txt_warning.DOText(I18N.instance.getValue("^link_type01"), 0.5f);
			Sequence sequence2 = DOTween.Sequence();
			sequence2.AppendInterval(10f).Append(txt_warning.DOText(I18N.instance.getValue("^link_type02"), 0.5f)).AppendInterval(320f)
				.Append(txt_warning.DOText(I18N.instance.getValue("^link_type03"), 0.5f))
				.AppendInterval(140f)
				.Append(txt_warning.DOText(I18N.instance.getValue("^link_type04"), 0.5f));
			sequence2.Play();
		}
	}

	private void Start()
	{
		if (gameManager.IsAllDlc())
		{
			btn_exit.interactable = true;
		}
		string logo = gameManager.dataManager.dic33[userID].logo;
		string sqlname = gameManager.dataManager.dic33[userID].sqlname;
		txt_phonename.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(sqlname) + I18N.instance.getValue("^w_phone"));
		img_logo.sprite = Resources.Load<Sprite>("touxiang/" + logo);
		suoping.Init(Resources.Load<Sprite>("touxiang/" + logo), sqlname);
		Invoke("SetInMuma", 1f);
		RefreshCount();
	}

	private void SetInMuma()
	{
		Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.getInvadeMuma()), content);
	}

	public void Init()
	{
		string[] itemids = gameManager.dataManager.dic33[userID].mission.Substring(1).Split(';');
		if (gameManager.IsBasic())
		{
			gameManager.homeScene.notebook.DeleteSpecialItem(itemids);
		}
		gameManager.homeScene.invadePhoneDialog = this;
		gameManager.CanShowSetting(1);
		txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone05");
		Showboxloading(isgreen: true);
		Show();
		btn_unlock.onClick.AddListener(Unlock);
		if (gameManager.IsBasic())
		{
			img_slider.DOLocalMoveX(-200f, 4f).SetEase(Ease.Linear).SetLoops(-1);
			img_slidermask.DOLocalMoveX(-599f, 480f).OnComplete(delegate
			{
				StartCoroutine(TimeOver());
			});
		}
		btn_close.onClick.AddListener(delegate
		{
			gameManager.CanShowSetting(-1);
		});
		RefreshStatus();
	}

	private IEnumerator TimeOver()
	{
		txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone11");
		Showboxloading(isgreen: false);
		if (littlewindow != null)
		{
			Object.Destroy(littlewindow);
		}
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		gameManager.CanShowSetting(-1);
		Close();
		if (gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[userID].name] != 1)
		{
			gameManager.homeScene.notebook.DeleteInvadeItem();
			if (gameManager.IsAllDlc())
			{
				btn_exit.interactable = true;
				Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(2, gameManager);
				gameManager.musicManager.ResumeVol();
			}
			else
			{
				gameManager.homeScene.StartVideoDialog("videoDialogtaskfailed", "invadephone");
			}
		}
	}

	public void Unlock()
	{
		if (btnObj.type != 3)
		{
			switch (btnObj.type)
			{
			case 1:
				ShowPasswordDialog();
				break;
			case 2:
				ShowFingerCodeDialog();
				break;
			case 4:
				ShowNumDialog();
				break;
			case 5:
				ShowQueDialog();
				break;
			case 6:
				ShowChangeNumDialog();
				break;
			case 3:
				break;
			}
		}
	}

	private void ShowPasswordDialog()
	{
		Debug.Log(2);
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/phonepasswordDialog"), base.transform);
		obj.GetComponent<PhonePasswordDialog>().Init(btnObj);
		obj.GetComponent<PhonePasswordDialog>().invadePhoneDialog = this;
		obj.GetComponent<PhonePasswordDialog>().Show();
	}

	private void ShowFingerCodeDialog()
	{
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetFingerCodeDialog()), base.transform);
		obj.GetComponent<FingercodeDialog>().Init(btnObj.password);
		obj.GetComponent<FingercodeDialog>().invadePhoneDialog = this;
		obj.GetComponent<FingercodeDialog>().Show();
	}

	private void ShowNumDialog()
	{
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/phonenumDialog"), base.transform);
		obj.GetComponent<PhoneNumDialog>().Init(btnObj.password, btnObj.passwordTip, btnObj.titlekey);
		obj.GetComponent<PhoneNumDialog>().invadePhoneDialog = this;
		obj.GetComponent<PhoneNumDialog>().Show();
	}

	private void ShowChangeNumDialog()
	{
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("_DLC/Prefabs/HomeTools/ChangeNumberPassword"), base.transform);
		obj.GetComponent<ChangeNumberPassword>().InitData(btnObj.password, "^3F68AE70-E7A0-144C-5FDC-139ED5FA5D16", btnObj.titlekey);
		obj.GetComponent<ChangeNumberPassword>().Show();
	}

	private void ShowQueDialog()
	{
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/phonequeDialog" + userID), base.transform);
		obj.GetComponent<InvadeQADialog>().invadePhoneDialog = this;
		obj.GetComponent<InvadeQADialog>().Show();
	}

	public void AppClick(AppButton appObj)
	{
		btnObj = appObj;
		currentappid = appObj.btnType;
		txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone06");
		Showboxloading(isgreen: true);
		StopAllCoroutines();
		coderunpanel.Run(20f, "OK");
		if (!appObj.isUnlock)
		{
			InitNull(appObj);
		}
		else
		{
			InitScucess(appObj);
		}
	}

	public void ShowUnlock()
	{
		InitScucess(btnObj);
	}

	public void InitNull(AppButton type)
	{
		lockpanel.SetActive(value: false);
		resultscrollview.gameObject.SetActive(value: true);
		if (type.type != 3)
		{
			bool flag = ((type.type != 0) ? true : false);
			if (!type.isChecked)
			{
				StartCoroutine(InitNull0(type.type));
			}
			else if (flag)
			{
				txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone07");
				Showboxloading(isgreen: false);
				lockpanel.SetActive(value: true);
				resultscrollview.gameObject.SetActive(value: false);
			}
		}
		else if (type == null || type.type == 3)
		{
			StartCoroutine(InitNull0(3));
		}
	}

	private IEnumerator InitNull0(int type)
	{
		Debug.Log("按钮状态:" + type);
		coderunpanel.Run(20f, "Failed");
		for (int i = 0; i < resultscrollview.content.childCount; i++)
		{
			Object.Destroy(resultscrollview.content.GetChild(i).gameObject);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		yield return new WaitForSeconds(0.2f);
		for (int j = 0; j < 7; j++)
		{
			((GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitemName()), resultscrollview.content)).GetComponent<InvadePhoneItem>().StartAnimation("^invadephoneapp0" + (j + 1), (type == 0 || j != 6) ? 1 : 0);
			yield return new WaitForSeconds(0.5f);
			if (j < 6)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
		}
		yield return new WaitForSeconds(0.2f);
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		switch (type)
		{
		default:
			txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone07");
			Showboxloading(isgreen: false);
			yield return new WaitForSeconds(1f);
			coderunpanel.Run(20f, "OK");
			lockpanel.SetActive(value: true);
			resultscrollview.gameObject.SetActive(value: false);
			break;
		case 0:
			InitScucess(btnObj);
			break;
		case 3:
			txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone08");
			Showboxloading(isgreen: false);
			break;
		}
		btnObj.isChecked = true;
	}

	public void InitScucess(AppButton appObj)
	{
		lockpanel.SetActive(value: false);
		resultscrollview.gameObject.SetActive(value: true);
		coderunpanel.Run(20f, "Success");
		txt_status.GetComponent<I18NText>().updateTranslation2("^invadephone09");
		Showboxloading(isgreen: true);
		for (int i = 0; i < resultscrollview.content.childCount; i++)
		{
			Object.Destroy(resultscrollview.content.GetChild(i).gameObject);
		}
		coderunpanel.Run(20f, "OK");
		appObj.isUnlock = true;
		switch (appObj.btnType)
		{
		case 0:
			StartCoroutine(InitPic());
			break;
		case 1:
			StartCoroutine(InitDiary());
			break;
		case 8:
			StartCoroutine(InitBrowser());
			break;
		case 9:
			StartCoroutine(InitMessage());
			break;
		case 10:
			StartCoroutine(InitCalendar());
			break;
		case 11:
			StartCoroutine(InitSetting());
			break;
		case 12:
			InitGrooMusic();
			break;
		case 13:
			StartCoroutine(InitBrowser());
			break;
		default:
			StartCoroutine(InitBrowser());
			break;
		}
	}

	private IEnumerator InitPic()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
			gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private IEnumerator InitBrowser()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
			gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private IEnumerator InitDiary()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
			gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private IEnumerator InitCalendar()
	{
		if (btnObj.isNeedWifiRefresh)
		{
			if (isWifi)
			{
				if (!btnObj.refresh)
				{
					_ = (GameObject)Object.Instantiate(Resources.Load("InvadePhoneImage/invade_tishi03"), content);
					yield return new WaitForSeconds(2.5f);
					btnObj.refresh = true;
				}
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
				for (int i = 0; i < btnObj.names.Count; i++)
				{
					GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
					gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
					gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
					if (btnObj.readTypes[i] == 1)
					{
						gameObject.GetComponent<InvadePhoneItem>().SetGray();
					}
					if (i < btnObj.names.Count - 1)
					{
						_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
					}
					yield return new WaitForSeconds(0.2f);
				}
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
				yield break;
			}
			_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
			for (int i = 0; i < 2; i++)
			{
				GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
				if (i == 0)
				{
					gameObject2.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
				}
				else
				{
					Debug.Log("显示假数据");
					gameObject2.GetComponent<InvadePhoneItem>().InitItem(btnObj, "^invadecalendar_label01", "invade_tishi01", i, "^invadecalendar_label02", freshReadType: false);
				}
				gameObject2.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
				if (btnObj.readTypes[i] == 1)
				{
					gameObject2.GetComponent<InvadePhoneItem>().SetGray();
				}
				if (i < 1)
				{
					_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
				}
				yield return new WaitForSeconds(0.2f);
			}
			_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
			yield break;
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject3 = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
			gameObject3.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject3.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject3.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private IEnumerator InitSetting()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("invadephoneitem3"), resultscrollview.content);
			gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private IEnumerator InitMessage()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
		for (int i = 0; i < btnObj.names.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem2Name()), resultscrollview.content);
			gameObject.GetComponent<InvadePhoneItem>().InitItem(btnObj, btnObj.names[i], btnObj.prefabs[i], i);
			gameObject.GetComponent<InvadePhoneItem>().invadePhoneDialog = this;
			if (btnObj.readTypes[i] == 1)
			{
				gameObject.GetComponent<InvadePhoneItem>().SetGray();
			}
			if (i < btnObj.names.Count - 1)
			{
				_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem1Name()), resultscrollview.content);
			}
			yield return new WaitForSeconds(0.2f);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetInvadephoneitem0Name()), resultscrollview.content);
	}

	private void InitGrooMusic()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load("invade_musicplayer"), base.transform);
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}

	public void RefreshCount()
	{
		int num = 0;
		for (int i = 0; i < needcollectitem.Count; i++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(needcollectitem[i]))
			{
				num++;
			}
		}
		Debug.Log("总共的数量：" + needcollectitem.Count);
		txt_count.text = num + "/" + needcollectitem.Count;
		if (num == needcollectitem.Count)
		{
			gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[userID].name] = 1;
			btn_exit.interactable = true;
			gameManager.homeScene.notebook.allinvadeitems.Clear();
		}
	}

	public void Showboxloading(bool isgreen)
	{
		txt_status.color = (isgreen ? greencolor : redcolor);
		ani_status.SetBool("isgreen", isgreen);
	}

	private void OnEnable()
	{
		InvadeEvent.Instance.onNoticePasswordSuccess += NoticePasswordSuccess;
	}

	private void OnDisable()
	{
		InvadeEvent.Instance.onNoticePasswordSuccess -= NoticePasswordSuccess;
	}

	private void NoticePasswordSuccess()
	{
		ShowUnlock();
	}
}
