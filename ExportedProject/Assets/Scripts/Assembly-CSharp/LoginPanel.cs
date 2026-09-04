using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
	public Button btn_setting;

	public Button btn_demo;

	public Button btn_quit;

	public GameObject demoPannel;

	public List<UserItem> userlists;

	public LoginCanvas loginCanvas;

	public Transform userGroup;

	public Transform plusitem;

	public GameManager gameManager;

	public string currentusername = "";

	public Button btn_continue;

	public Button btn_new;

	public Button btn_save;

	public Button btn_read;

	public Button btn_select;

	public Button btn_laborer;

	public Button btn_account;

	public Button btn_DLc;

	public GameObject qqgroup;

	[SerializeField]
	private Animator exitWindow;

	[SerializeField]
	private Text txt_version;

	[SerializeField]
	private Animator savehurtWindow;

	public Sprite[] _publishSprites;

	public Image _dlcTextImage;

	private GameObject noticeAlert_cn;

	private bool iscanclick = true;

	public bool alertCanClick = true;

	public void ExitSaveHurt()
	{
		if (alertCanClick)
		{
			gameManager.soundManager.PlaySound(16);
			StartCoroutine(StopSaveHurt());
		}
	}

	private IEnumerator StopSaveHurt()
	{
		savehurtWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
		iscanclick = true;
	}

	private IEnumerator ShowSaveHurt()
	{
		savehurtWindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
	}

	public void ChangeDLCNotice()
	{
		if (I18N.instance.gameLang == LanguageCode.CN)
		{
			_dlcTextImage.sprite = _publishSprites[0];
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			_dlcTextImage.sprite = _publishSprites[1];
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			_dlcTextImage.sprite = _publishSprites[2];
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.loginPanel = this;
		ChangeDLCNotice();
		txt_version.text = gameManager.str_version;
		btn_setting.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			gameManager.saveManager.pausePanel.settingPannel.SetActive(value: true);
		});
		btn_laborer.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			if (!gameManager.isBuyDLC(9))
			{
				gameManager.ValidDLC(9);
			}
			else
			{
				base.gameObject.SetActive(value: false);
				gameManager.CanShowSetting(-1);
				SceneManager.LoadSceneAsync("HomeDLC8");
			}
		});
		btn_quit.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			exitWindow.gameObject.SetActive(value: true);
			StartCoroutine(ShowExit());
		});
		btn_save.onClick.AddListener(delegate
		{
			gameManager.saveManager.ShowSavePanel(1);
		});
		btn_read.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			gameManager.saveManager.ShowSavePanel(0);
		});
		btn_new.onClick.AddListener(CreateUser);
		btn_select.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			gameManager.saveManager.ShowChoiceLevel();
		});
		if (gameManager.saveManager.IsHasAutoSave())
		{
			gameManager.saveManager.LoadAutoData();
			Dictionary<int, string> allLevelInfo = gameManager.saveManager.getAllLevelInfo();
			if (allLevelInfo.Count > 0)
			{
				gameManager.player.playerdata.alllevelinfo = allLevelInfo;
				gameManager.saveManager.SavePlayerData();
			}
			btn_continue.gameObject.SetActive(value: true);
			btn_save.interactable = true;
			btn_save.GetComponent<ButtonScale>().enabled = true;
			btn_save.GetComponent<ButtonScale>().SetWhite();
		}
		else
		{
			btn_continue.gameObject.SetActive(value: false);
			btn_save.interactable = false;
			btn_save.GetComponent<ButtonScale>().SetGray();
		}
		btn_account.onClick.AddListener(delegate
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/noticeAlert"), base.transform.parent);
			obj.SetActive(value: true);
			obj.GetComponent<Animator>().Play("ani_showalertrightnow");
		});
		if (btn_DLc != null)
		{
			btn_DLc.onClick.AddListener(delegate
			{
				if (noticeAlert_cn == null)
				{
					noticeAlert_cn = Object.Instantiate(Resources.Load<GameObject>("Dialog/dlc6_noticeAlert"), base.transform.parent);
					noticeAlert_cn.SetActive(value: true);
					noticeAlert_cn.GetComponent<NoticeAlertDLC>().InitInfo();
				}
			});
		}
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			qqgroup.SetActive(value: true);
		}
		else
		{
			qqgroup.SetActive(value: false);
		}
	}

	public void ExitCancle()
	{
		if (alertCanClick)
		{
			gameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StopExit());
		}
	}

	private IEnumerator StopExit()
	{
		exitWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
	}

	public void QuitGame()
	{
		StartCoroutine(StartExit());
	}

	private IEnumerator StartExit()
	{
		exitWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		Application.Quit();
	}

	private IEnumerator ShowExit()
	{
		exitWindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
	}

	public void Init()
	{
		string[] array = PlayerPrefs.GetString("players", "").Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null && !array[i].Equals(""))
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("useritem"), userGroup);
				gameObject.GetComponent<UserItem>().Init(array[i], this);
				if (i == array.Length - 1)
				{
					currentusername = array[i];
					gameObject.GetComponent<UserItem>().SetSelected(isselected: true);
				}
				else
				{
					gameObject.GetComponent<UserItem>().SetSelected(isselected: false);
				}
				userlists.Add(gameObject.GetComponent<UserItem>());
			}
		}
		plusitem.SetAsLastSibling();
	}

	public void SelectUser(string username)
	{
		currentusername = username;
		for (int i = 0; i < userlists.Count; i++)
		{
			if (!username.Equals(userlists[i].un))
			{
				userlists[i].SetSelected(isselected: false);
			}
		}
	}

	public void CreateUser()
	{
		gameManager.soundManager.PlaySound(16);
		loginCanvas.createUserPanel.gameObject.SetActive(value: true);
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator StartSignUp()
	{
		yield return new WaitForSeconds(0.2f);
		loginCanvas.loadingPanel.gameObject.SetActive(value: true);
		gameManager.saveManager.LoadAutoData();
		if (gameManager.player.GetEventId().Equals("110003") && gameManager.player.playerdata.islast4)
		{
			gameManager.player.AddEventID(isadd: true);
			gameManager.player.playerdata.islast4 = false;
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.GetEventId().Equals("110004") && gameManager.player.playerdata.islast4)
		{
			gameManager.player.AddEventID(isadd: true);
			gameManager.player.playerdata.islast4 = false;
			gameManager.saveManager.SavePlayerData();
		}
		if ((gameManager.player.GetEventId().Equals("110005") && gameManager.player.playerdata.islast4) || gameManager.isbug)
		{
			loginCanvas.loadingPanel.txt_username.text = gameManager.player.playerdata.nickname;
			loginCanvas.loadingPanel.txt_loading.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue("^loading_01"), gameManager.player.playerdata.nickname) + "......");
			yield return new WaitForSeconds(2f);
			gameManager.ShowFloatBox();
			yield return new WaitForSeconds(2f);
			gameManager.player.playerdata.GoToEventID(6, isclear: true);
			gameManager.txt_studio.SetActive(value: false);
			SceneManager.LoadScene("homego");
		}
		else
		{
			loginCanvas.loadingPanel.SetLoading(gameManager.player.playerdata.nickname, isnew: false);
			iscanclick = true;
		}
		base.gameObject.SetActive(value: false);
	}

	public void AutoLoginSystem()
	{
		if (!iscanclick)
		{
			return;
		}
		gameManager.soundManager.PlaySound(16);
		if (gameManager.saveManager.IsHasAutoSave())
		{
			iscanclick = false;
			PlayerData autoSaveItem = gameManager.saveManager.GetAutoSaveItem();
			if (autoSaveItem != null)
			{
				Debug.Log("自动存档文件存在");
				gameManager.IsDlc = autoSaveItem.Eventid == 7;
				if (autoSaveItem.Eventid > 6 && !gameManager.isBuyDLC(autoSaveItem.Eventid))
				{
					gameManager.ValidDLC(autoSaveItem.Eventid);
					iscanclick = true;
				}
				else
				{
					StartCoroutine(StartSignUp());
				}
			}
			else if (alertCanClick)
			{
				gameManager.soundManager.PlaySound(16);
				alertCanClick = false;
				savehurtWindow.gameObject.SetActive(value: true);
				StartCoroutine(ShowSaveHurt());
			}
		}
		else
		{
			Debug.Log("自动存档文件不存在");
		}
	}
}
