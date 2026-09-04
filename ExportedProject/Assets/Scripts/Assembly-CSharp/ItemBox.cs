using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using tnt_deploy;

public class ItemBox : MonoBehaviour
{
	public bool iszhibojianitembox;

	public GameObject img_drag;

	public GameManager gameManager;

	public Transform[] panels;

	public bool isshow;

	public Button btn_close;

	public Button btn_submit;

	public ComputerButton btn_note;

	public Dictionary<string, NoteTab> tablist = new Dictionary<string, NoteTab>();

	public Transform tabgroup;

	public Transform panelgroup;

	public Text txt_upload;

	public Text txt_download;

	public Text txt_hhh;

	public bool iscanchangetab = true;

	public Dictionary<string, NoteItem> noteitemlist = new Dictionary<string, NoteItem>();

	public List<NoteTab> alltablist = new List<NoteTab>();

	public List<NoteItem> allinvadeitems = new List<NoteItem>();

	public List<NoteItem> allinvadeserveritems = new List<NoteItem>();

	public bool isshowfirstbrowsershow;

	public DATA1 currentdata;

	public Animator tijiaoAlert;

	public Button btn_yes;

	public Button btn_no;

	public Text txt_tijiaotip;

	protected Vector3 oldpos;

	protected bool isSubmit;

	protected CodeDialog codeDialog;

	private Dictionary<string, string> achievementDic = new Dictionary<string, string>();

	public void RefreshCount()
	{
	}

	private void UpdateDOWNLoadText()
	{
		float num = Random.Range(0f, 100f);
		txt_download.text = num.ToString("f2") + "MB/s";
	}

	private void UpdateHHHText()
	{
		float num = Random.Range(0f, 100f);
		txt_hhh.text = num.ToString("f2") + "MB/s";
	}

	private void AddHighLight()
	{
		if (gameManager.IsAllDlc())
		{
			if (base.gameObject.GetComponent<Canvas>() == null)
			{
				base.gameObject.AddComponent<Canvas>();
				base.gameObject.GetComponent<Canvas>().overrideSorting = true;
			}
		}
		else if (base.gameObject.AddComponent<Canvas>() == null)
		{
			base.gameObject.AddComponent<Canvas>().overrideSorting = true;
		}
		base.gameObject.GetComponent<Canvas>().sortingOrder = 9;
	}

	private void DeleteHighLight()
	{
		Object.Destroy(base.gameObject.GetComponent<GraphicRaycaster>());
		Object.Destroy(base.gameObject.GetComponent<Canvas>());
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		achievementDic.Add("11211", "pinkroom");
		achievementDic.Add("11210", "youlove");
		achievementDic.Add("11170", "eater");
		if (gameManager.Is_Dlc6() && gameManager.player.playerdata.isovertask && !gameManager.player.playerdata.videotiplist.Contains("3710003"))
		{
			btn_submit.interactable = true;
		}
		btn_close.onClick.AddListener(Hide);
		btn_submit.onClick.AddListener(delegate
		{
			if (SceneManager.GetActiveScene().name.Equals("homecourse"))
			{
				Submit();
			}
			else
			{
				ShowTishiAlert();
			}
		});
		InvokeRepeating("UpdateDOWNLoadText", 0.5f, 0.5f);
		InvokeRepeating("UpdateHHHText", 2f, 1f);
	}

	public void InitItems()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!iszhibojianitembox)
		{
			StartCoroutine(InitManyAdd2(gameManager.player.playerdata.itemlist.ToArray()));
		}
		else
		{
			StartCoroutine(InitManyAdd2(gameManager.player.playerdata.temporaryhopelist.ToArray()));
		}
	}

	private IEnumerator InitManyAdd2(string[] ids)
	{
		iscanchangetab = true;
		for (int i = 0; i < ids.Length; i++)
		{
			AddItem(ids[i], isadd: false);
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(1f);
		if (alltablist.Count > 0)
		{
			alltablist[alltablist.Count - 1].Click();
		}
		if (gameManager.player.GetEventId().Equals("110005") && !gameManager.player.playerdata.itemlist.Contains("10456"))
		{
			gameManager.homeScene.notebook.AddNewItem("10456");
		}
	}

	public void InitManyAdd(string[] ids)
	{
		iscanchangetab = true;
		for (int i = 0; i < ids.Length; i++)
		{
			AddItem(ids[i], isadd: false);
		}
		StartCoroutine(InitTabClick());
	}

	private IEnumerator InitTabClick()
	{
		yield return new WaitForSeconds(1f);
		if (alltablist.Count > 0)
		{
			alltablist[alltablist.Count - 1].Click();
		}
	}

	private void AddNoteItem(string id)
	{
		if (!gameManager.player.playerdata.itemlist.Contains(id) && !gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			if (achievementDic.ContainsKey(id))
			{
				gameManager.UnlockAchievements(achievementDic[id]);
			}
			NoticeAIDialog(id);
			AddItem(id, isadd: true);
		}
	}

	private void NoticeAIDialog(string id)
	{
		if (id == "11420")
		{
			DLCEventManager.Instance.NoticeAITalk("3910009");
		}
		if (id == "11412")
		{
			gameManager.UnlockAchievements("thingsremain");
		}
		else if (id == "11389")
		{
			gameManager.UnlockAchievements("medicalreport");
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{ "11317", "3910011" },
			{ "11314", "3910012" },
			{ "11352", "3910013" },
			{ "11415", "3910024" },
			{ "11414", "3910018" },
			{ "11408", "3910017" },
			{ "11412", "3910016" },
			{ "11416", "3910015" },
			{ "11422", "3910026" },
			{ "11431", "3910025" }
		};
		if (dictionary.ContainsKey(id))
		{
			DLCEventManager.Instance.NoticeAITalk(dictionary[id]);
		}
	}

	public void AddItem(string id, bool isadd)
	{
		if (!gameManager.dataManager.dic1.ContainsKey(id))
		{
			Debug.Log("没有此词条：" + id);
			return;
		}
		DATA1 dATA = gameManager.dataManager.dic1[id];
		if (gameManager.homeScene.newZhadanDialog != null && isadd)
		{
			gameManager.homeScene.newZhadanDialog.ShowZhadan(dATA.ID.ToString());
		}
		string[] array = dATA.role.Substring(1).Split(';');
		foreach (string text in array)
		{
			if (!tablist.ContainsKey(text) & !text.Equals("0"))
			{
				if (gameManager.dataManager.dic31.ContainsKey(text))
				{
					DATA31 data = gameManager.dataManager.dic31[text];
					GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNoteTabName()), tabgroup);
					gameObject.GetComponent<NoteTab>().Init(data, panelgroup, tabgroup, dATA.changeavatar == 1, this, (dATA.changename == 1) ? dATA.message : "");
					tablist.Add(text, gameObject.GetComponent<NoteTab>());
					tablist[text].Click();
					tablist[text].notePanel.AddItem(id, isadd, this);
				}
			}
			else if (tablist.ContainsKey(text))
			{
				tablist[text].Click();
				tablist[text].notePanel.AddItem(id, isadd, this);
				if (dATA.changename == 1)
				{
					tablist[text].UpdateName(dATA.message);
					tablist[text].UpdateAvatar();
				}
				if (dATA.dieavatar == 1)
				{
					tablist[text].UpdateWhiteAvatar();
				}
			}
		}
		if (isadd)
		{
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void Front()
	{
		base.transform.SetAsLastSibling();
	}

	private void ShowTishiAlert()
	{
		if (tijiaoAlert != null)
		{
			isSubmit = false;
			tijiaoAlert.gameObject.SetActive(value: true);
			DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
			string text = "";
			text = ((!gameManager.player.playerdata.itemlist.Contains("10453")) ? (gameManager.player.playerdata.itemlist.Count + " / " + dATA.number) : (gameManager.player.playerdata.itemlist.Count - 1 + " / " + dATA.number));
			txt_tijiaotip.text = string.Format(I18N.instance.getValue("^tijiaotip01"), text);
			tijiaoAlert.transform.SetAsLastSibling();
			tijiaoAlert.Play("Exit Panel In");
		}
		else
		{
			Submit();
		}
	}

	public void CancleTishi()
	{
		if (!isSubmit)
		{
			isSubmit = true;
			tijiaoAlert.Play("Exit Panel Out");
		}
	}

	public void Submit()
	{
		if (isSubmit)
		{
			return;
		}
		isSubmit = true;
		if (tijiaoAlert != null)
		{
			btn_yes.interactable = false;
			btn_no.interactable = false;
			tijiaoAlert.Play("Exit Panel Out");
		}
		Debug.Log("submit");
		if (gameManager.player.playerdata.isCourse11 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel11.HideCourse();
		}
		if (!gameManager.player.playerdata.isovertask && !gameManager.isbug)
		{
			return;
		}
		if (gameManager.player.GetEventId() == "110008")
		{
			string time = (gameManager.player.playerdata.endTime / 60000).ToString();
			DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
			if (gameManager.Is_Dlc7() && gameManager.player.playerdata.itemlist.Count >= dATA.number)
			{
				gameManager.UnlockAchievements("helloworld");
			}
			int count = gameManager.player.playerdata.itemlist.Count;
			gameManager.player.RefreshLevel(count.ToString(), time);
			gameManager.homeScene.ShowVideoTip("3910008");
			gameManager.saveManager.CreateNewSave(gameManager.player.playerdata.nickname);
			return;
		}
		Debug.Log("submit1");
		btn_submit.interactable = false;
		if (gameManager.player.GetEventId().Equals("110000"))
		{
			gameManager.homeScene.StartVideoDialog("videoDialog110000last");
		}
		else if (gameManager.player.GetEventId().Equals("110002"))
		{
			gameManager.homeScene.StartVideoDialog("videoDialog" + gameManager.player.GetEventId() + "last");
		}
		else if (gameManager.player.GetEventId().Equals("110003"))
		{
			Object.Instantiate(Resources.Load("Hacker/img_bk") as GameObject, gameManager.homeScene.middle);
			Object.Instantiate(Resources.Load("Dialog/Hacker/hackervideoDialog01") as GameObject, gameManager.homeScene.middle);
		}
		else if (gameManager.player.GetEventId().Equals("110005"))
		{
			btn_submit.interactable = true;
			btn_yes.interactable = true;
			btn_no.interactable = true;
			if (gameManager.player.playerdata.phoneCall.Contains("3700012"))
			{
				if (gameManager.player.playerdata.videotiplist.Contains("3700081"))
				{
					if (gameManager.homeScene.otherdialogpanel.Find("phoneDialog(Clone)") == null)
					{
						gameManager.homeScene.computerButtonBox.OpenTool(15);
					}
				}
				else
				{
					gameManager.homeScene.ShowVideoTip("3700081");
				}
			}
			else if (gameManager.player.playerdata.videotiplist.Contains("3700082"))
			{
				if (gameManager.homeScene.otherdialogpanel.Find("phoneDialog(Clone)") == null)
				{
					gameManager.homeScene.computerButtonBox.OpenTool(15);
				}
			}
			else
			{
				gameManager.homeScene.ShowVideoTip("3700082");
			}
		}
		else if (gameManager.player.GetEventId().Equals("110006"))
		{
			btn_submit.interactable = true;
			btn_yes.interactable = true;
			btn_no.interactable = true;
		}
		else
		{
			StartCloseAllDialog();
			if (gameManager.player.GetEventId().Equals("110004"))
			{
				btn_submit.interactable = true;
				btn_yes.interactable = true;
				btn_no.interactable = true;
			}
		}
	}

	public void StartCloseAllDialog0()
	{
		for (int i = 0; i < base.transform.parent.childCount; i++)
		{
			if (base.transform.parent.GetChild(i).name.Contains("Clone") || base.transform.parent.GetChild(i).name.Contains("pic"))
			{
				SqlDialog component2;
				if (base.transform.parent.GetChild(i).TryGetComponent<CustomDialog>(out var component))
				{
					component.Close();
				}
				else if (base.transform.parent.GetChild(i).TryGetComponent<SqlDialog>(out component2))
				{
					component2.Close();
				}
			}
		}
		gameManager.homeScene.goalDialog.CompleteItem(gameManager.dataManager.GetLastMissionItem(gameManager.player.GetEventId()));
		Object.Instantiate(Resources.Load("Dialog/missionresultDialog") as GameObject, base.transform.parent);
	}

	public void StartCloseAllDialog()
	{
		StartCoroutine(CloseAllDialog());
	}

	private IEnumerator CloseAllDialog()
	{
		for (int i = 0; i < base.transform.parent.childCount; i++)
		{
			if (base.transform.parent.GetChild(i).name.Contains("Clone") || base.transform.parent.GetChild(i).name.Contains("pic"))
			{
				SqlDialog component2;
				if (base.transform.parent.GetChild(i).TryGetComponent<CustomDialog>(out var component))
				{
					component.Close();
					yield return new WaitForSeconds(0.5f);
				}
				else if (base.transform.parent.GetChild(i).TryGetComponent<SqlDialog>(out component2))
				{
					component2.Close();
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
		gameManager.homeScene.goalDialog.CompleteItem(gameManager.dataManager.GetLastMissionItem(gameManager.player.GetEventId()));
		Object.Instantiate(Resources.Load("Dialog/missionresultDialog") as GameObject, base.transform.parent);
		Hide();
	}

	public void SceneLarge()
	{
		if (!gameManager.IsAllDlc())
		{
			gameManager.maincamera.NoteMoveTransform();
		}
	}

	public void SceneNormal()
	{
		if (!gameManager.IsAllDlc())
		{
			gameManager.maincamera.NoteEmpty();
		}
	}

	public void Show()
	{
		if (!isshow)
		{
			oldpos = new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f);
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.5f);
			isshow = true;
			base.transform.SetAsLastSibling();
		}
	}

	public void ShowSubmit()
	{
		gameManager.player.playerdata.isovertask = true;
		gameManager.saveManager.SavePlayerData();
		btn_submit.gameObject.SetActive(value: true);
		btn_submit.interactable = true;
		StartCoroutine(ShowSubmitVideo());
	}

	private IEnumerator ShowSubmitVideo()
	{
		yield return new WaitForSeconds(7f);
		if (gameManager.player.GetEventId().Equals("110001"))
		{
			gameManager.homeScene.ShowVideoTip("3700011");
		}
	}

	public void ShowSide()
	{
		if (isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.5f);
			base.transform.SetAsLastSibling();
		}
	}

	private void ShowCodeDialog(bool isneedhighlight = false)
	{
		if (codeDialog == null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Dialog/codeDialog"), base.transform.parent);
			codeDialog = gameObject.GetComponent<CodeDialog>();
			if (isneedhighlight)
			{
				gameObject.AddComponent<Canvas>().overrideSorting = true;
				gameObject.GetComponent<Canvas>().sortingOrder = 9;
			}
		}
	}

	public void HideCodeDialog(string itemid = "")
	{
		if (codeDialog != null)
		{
			if (itemid.Equals("10453"))
			{
				codeDialog.ShowRed();
			}
			else
			{
				Object.Destroy(codeDialog.gameObject);
			}
		}
	}

	public IEnumerator ShowNormalAdd(string id)
	{
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		yield return new WaitForSeconds(0.3f);
		ShowCodeDialog();
		AddNoteItem(id);
		yield return new WaitForSeconds(3.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f);
		DATA1 dATA = gameManager.dataManager.dic1[id];
		if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
			}
		}
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else
		{
			btn_note.HideNoteDialog();
		}
	}

	public IEnumerator ShowAdd(string id, bool isneedhighlight)
	{
		if (gameManager.player.playerdata.itemlist.Contains(id) || gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			yield break;
		}
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
			{
				img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
			}
			yield return new WaitForSeconds(0.5f);
		}
		if (gameManager.IsBasic())
		{
			if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
			{
				img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
			}
			SceneLarge();
			yield return new WaitForSeconds(0.3f);
			ShowCodeDialog(isneedhighlight);
		}
		AddNoteItem(id);
		if (gameManager.IsBasic())
		{
			yield return new WaitForSeconds(1f);
			SceneNormal();
			yield return new WaitForSeconds(1f);
		}
		gameManager.homeScene.eventsystem.SetActive(value: true);
		if (gameManager.IsBasic())
		{
			if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
			{
				img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f).OnComplete(delegate
				{
					if (isneedhighlight)
					{
						DeleteHighLight();
					}
				});
			}
		}
		else
		{
			yield return new WaitForSeconds(0.6f);
			img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.1f).OnComplete(delegate
			{
				if (isneedhighlight)
				{
					DeleteHighLight();
				}
			});
		}
		DATA1 data1 = gameManager.dataManager.dic1[id];
		yield return new WaitForSeconds(0.1f);
		gameManager.homeScene.ShowNextVideo();
		Debug.Log("shownext");
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else if (btn_note != null)
		{
			btn_note.HideNoteDialog();
		}
		if (gameManager.player.playerdata.isstartgetemailitem == 0)
		{
			gameManager.homeScene.StartTask2();
			gameManager.player.playerdata.isstartgetemailitem = 1;
		}
		if (!data1.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = data1.missionID.Substring(1).Split(';');
			string[] array2 = data1.aimspercent.Substring(1).Split(';');
			for (int num = 0; num < array.Length; num++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[num], float.Parse(array2[num]));
			}
		}
		if (gameManager.homeScene.invadePhoneDialog != null)
		{
			gameManager.homeScene.invadePhoneDialog.RefreshCount();
		}
	}

	public IEnumerator ShowFirstAdd(string id)
	{
		oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		ShowCodeDialog();
		AddNoteItem(id);
		float num = 1f;
		yield return new WaitForSeconds(num + 2f + 2f);
		gameManager.homeScene.courseManager.ShowTuli1();
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	private IEnumerator ShowFirstAdd2()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		gameManager.homeScene.courseManager.ShowCourse1();
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.5f);
		DATA1 dATA = gameManager.dataManager.dic1["10057"];
		if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
			}
		}
	}

	public void ShowFirst2()
	{
		StartCoroutine(ShowFirstAdd2());
	}

	public IEnumerator ShowManyAdd(string[] ids, bool isneedhighlight)
	{
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		if (gameManager.IsBasic())
		{
			base.transform.SetAsLastSibling();
			if (!isshow)
			{
				btn_note.ShowNoteDialog();
				img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
				yield return new WaitForSeconds(0.5f);
			}
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
			SceneLarge();
			yield return new WaitForSeconds(0.3f);
			ShowCodeDialog(isneedhighlight);
		}
		else
		{
			base.transform.SetAsLastSibling();
			if (!isshow)
			{
				btn_note.ShowNoteDialog();
				img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
				yield return new WaitForSeconds(0.5f);
			}
		}
		for (int i = 0; i < ids.Length; i++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(ids[i]))
			{
				continue;
			}
			AddNoteItem(ids[i]);
			if (gameManager.dataManager.dic1.ContainsKey(ids[i]))
			{
				DATA1 dATA = gameManager.dataManager.dic1[ids[i]];
				if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
				{
					string[] array = dATA.missionID.Substring(1).Split(';');
					string[] array2 = dATA.aimspercent.Substring(1).Split(';');
					for (int j = 0; j < array.Length; j++)
					{
						gameManager.homeScene.goalDialog.CompletePercentItem(array[j], float.Parse(array2[j]));
					}
				}
			}
			yield return new WaitForSeconds(1f);
		}
		SceneNormal();
		yield return new WaitForSeconds(0.5f);
		HideCodeDialog();
		gameManager.homeScene.eventsystem.SetActive(value: true);
		img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f).OnComplete(delegate
		{
			if (isneedhighlight)
			{
				DeleteHighLight();
			}
		});
		gameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.ShowNextVideo();
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else
		{
			btn_note.HideNoteDialog();
		}
	}

	public void Hide()
	{
		if (isshow)
		{
			img_drag.GetComponent<RectTransform>().localPosition = new Vector3(gameManager.IsAllDlc() ? 1351f : 1231f, GetNoteDialogShowY(), 0f);
			isshow = false;
			gameManager.soundManager.PlaySound(7);
			btn_note.HideNoteDialog();
		}
	}

	public void HideAll()
	{
		if (isshow)
		{
			img_drag.GetComponent<RectTransform>().localPosition = new Vector3(gameManager.IsAllDlc() ? 1351f : 1231f, GetNoteDialogShowY(), 0f);
			isshow = false;
		}
	}

	public void AddNewItem(string id, bool isneedhighlight = false)
	{
		if (gameManager.player.playerdata.itemlist.Contains(id) || gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			return;
		}
		switch (id)
		{
		case "11304":
			AddNewItems(new string[2] { "11399", "11304" });
			return;
		case "11312":
			AddNewItems(new string[2] { "11312", "11396" });
			return;
		case "11358":
			AddNewItems(new string[2] { "11413", "11358" });
			return;
		case "11373":
			AddNewItems(new string[2] { "11373", "11396" });
			return;
		case "11408":
			gameManager.UnlockAchievements("familypicture");
			break;
		}
		if (id == "11435")
		{
			gameManager.UnlockAchievements("clues");
		}
		if (id == "11394")
		{
			gameManager.UnlockAchievements("wheeloffortune");
		}
		gameManager.player.playerdata.NearlyItemIds.Clear();
		gameManager.player.playerdata.NearlyItemIds.Add(id);
		gameManager.soundManager.PlaySound(3);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		if (gameManager.player.playerdata.isCourse01 == 0)
		{
			StartCoroutine(ShowFirstAdd(id));
			return;
		}
		if (gameManager.player.playerdata.isCourse04 == 0)
		{
			StartCoroutine(ShowNormalAdd(id));
			return;
		}
		if (gameManager.player.playerdata.isCourse05 == 0)
		{
			StartCoroutine(ShowNormalAdd(id));
			return;
		}
		if (gameManager.player.playerdata.isCourse11 == 0 && id.Equals("10066"))
		{
			AddHighLight();
			StartCoroutine(ShowLastAdd(id));
			gameManager.player.playerdata.isCourseOver = 1;
			return;
		}
		if (isneedhighlight)
		{
			AddHighLight();
		}
		Debug.Log("播放新cio");
		StartCoroutine(ShowAdd(id, isneedhighlight));
	}

	public void AddNewItems(string[] ids, bool isneedhighlight = false)
	{
		if (ids.Contains("11408"))
		{
			gameManager.UnlockAchievements("familypicture");
		}
		gameManager.player.playerdata.NearlyItemIds.Clear();
		foreach (string text in ids)
		{
			if (!string.IsNullOrEmpty(text) && gameManager.player.playerdata.NearlyItemIds.Contains(text))
			{
				gameManager.player.playerdata.NearlyItemIds.Add(text);
			}
		}
		gameManager.soundManager.PlaySound(3);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		if (isneedhighlight)
		{
			AddHighLight();
		}
		StartCoroutine(ShowManyAdd(ids, isneedhighlight));
	}

	public IEnumerator ShowLastAdd(string id)
	{
		oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(GetNoteDialogShowX(), GetNoteDialogShowY(), 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		ShowCodeDialog(isneedhighlight: true);
		AddNoteItem(id);
		if (gameManager.dataManager.dic1.ContainsKey(id))
		{
			DATA1 dATA = gameManager.dataManager.dic1[id];
			if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
			{
				string[] array = dATA.missionID.Substring(1).Split(';');
				string[] array2 = dATA.aimspercent.Substring(1).Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
				}
			}
		}
		yield return new WaitForSeconds(5.3f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		DeleteHighLight();
	}

	public void DeleteInvadeItem()
	{
		for (int i = 0; i < allinvadeitems.Count; i++)
		{
			DATA1 dATA = gameManager.dataManager.dic1[allinvadeitems[i].itemid];
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int j = 0; j < array.Length; j++)
			{
				Debug.Log("需要删除的ID：" + array[j] + "  " + array2[j]);
				gameManager.homeScene.goalDialog.goalitemlist[array[j]].MinusPercent(int.Parse(array2[j]));
			}
			gameManager.player.playerdata.itemlist.Remove(allinvadeitems[i].itemid);
			Object.Destroy(allinvadeitems[i].gameObject);
		}
		allinvadeitems.Clear();
		RefreshCount();
	}

	public void DeleteInvadeServerItem()
	{
		for (int i = 0; i < allinvadeserveritems.Count; i++)
		{
			DATA1 dATA = gameManager.dataManager.dic1[allinvadeserveritems[i].itemid];
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			Debug.Log(array.Length + "*****" + array2.Length);
			for (int j = 0; j < array.Length; j++)
			{
				gameManager.homeScene.goalDialog.goalitemlist[array[i]].MinusPercent(int.Parse(array2[i]));
			}
			gameManager.player.playerdata.itemlist.Remove(allinvadeserveritems[i].itemid);
			Object.Destroy(allinvadeserveritems[i].gameObject);
		}
		allinvadeserveritems.Clear();
		RefreshCount();
	}

	public void DeleteSpecialItem(string itemid)
	{
		gameManager.player.playerdata.itemlist.Remove(itemid);
		if (noteitemlist.ContainsKey(itemid))
		{
			Object.Destroy(noteitemlist[itemid].gameObject);
			noteitemlist.Remove(itemid);
		}
		if (gameManager.player.playerdata.temporaryhopelist.Contains(itemid))
		{
			gameManager.player.playerdata.temporaryhopelist.Remove(itemid);
		}
		RefreshCount();
	}

	public void DeleteSpecialItem(string[] itemids)
	{
		Debug.LogError("删除DeleteSpecialItem");
		for (int i = 0; i < itemids.Length; i++)
		{
			if (noteitemlist.ContainsKey(itemids[i]) && noteitemlist[itemids[i]] != null)
			{
				Debug.LogError("删除DeleteSpecialItem" + itemids[i]);
				DATA1 dATA = gameManager.dataManager.dic1[itemids[i]];
				string[] array = dATA.missionID.Substring(1).Split(';');
				string[] array2 = dATA.aimspercent.Substring(1).Split(';');
				for (int j = 0; j < array.Length; j++)
				{
					Debug.LogError("data1.missionID:" + dATA.missionID);
					if (gameManager.homeScene.goalDialog.goalitemlist.ContainsKey(array[j]))
					{
						gameManager.homeScene.goalDialog.goalitemlist[array[j]].MinusPercent(int.Parse(array2[j]));
					}
				}
				Object.Destroy(noteitemlist[itemids[i]].gameObject);
				noteitemlist.Remove(itemids[i]);
			}
			gameManager.player.playerdata.itemlist.Remove(itemids[i]);
			Debug.Log("删除的ID：" + itemids[i]);
			if (gameManager.player.playerdata.temporaryhopelist.Contains(itemids[i]))
			{
				gameManager.player.playerdata.temporaryhopelist.Remove(itemids[i]);
			}
		}
		RefreshCount();
	}

	public void DeleteHopePanel()
	{
		foreach (KeyValuePair<string, NoteTab> item in tablist)
		{
			if (int.Parse(item.Key) >= 3100036 && int.Parse(item.Key) < 3100046)
			{
				item.Value.notePanel.DestroyAllHopeItem();
			}
		}
		foreach (KeyValuePair<string, NoteTab> item2 in tablist)
		{
			if (int.Parse(item2.Key) >= 3100036 && int.Parse(item2.Key) < 3100046)
			{
				alltablist.Remove(item2.Value);
				Object.Destroy(item2.Value.gameObject);
			}
		}
		tablist.Clear();
	}

	public void DeleteBossHopePanel()
	{
		foreach (KeyValuePair<string, NoteTab> item in tablist)
		{
			if (int.Parse(item.Key) == 3100046)
			{
				item.Value.notePanel.DestroyAllHopeItem();
			}
		}
		foreach (KeyValuePair<string, NoteTab> item2 in tablist)
		{
			if (int.Parse(item2.Key) == 3100046)
			{
				alltablist.Remove(item2.Value);
				Object.Destroy(item2.Value.gameObject);
			}
		}
		tablist.Remove("3100046");
		using (Dictionary<string, NoteTab>.Enumerator enumerator = tablist.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				enumerator.Current.Value.Click();
			}
		}
	}

	private float GetNoteDialogShowX()
	{
		if (gameManager.Is_Dlc6())
		{
			return 750f;
		}
		if (gameManager.Is_Dlc7())
		{
			return 726f;
		}
		return 741f;
	}

	private float GetNoteDialogShowY()
	{
		if (!gameManager.IsAllDlc())
		{
			return -104f;
		}
		return -140f;
	}
}
