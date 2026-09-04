using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
	public GameManager gameManager;

	public Animator ani_saving;

	public GameObject img_saving;

	public Text studio;

	public GameObject noEsc;

	public Setting settingPanel;

	[SerializeField]
	public SavePanel savePanel;

	public PausePanel pausePanel;

	[SerializeField]
	private LoadingPanel loadingPanel;

	private Coroutine showEsc;

	public GameObject backToMainWindow;

	public GameObject exitWindow;

	[SerializeField]
	private ChoiceLevel choiceLevel;

	private FileComparer fileComparer = new FileComparer();

	public Transform playerdatapanel;

	public Canvas saveCanvas;

	private long lastTime;

	public GameObject videocio;

	public string dotstring = "......";

	public void ShowPausePanel()
	{
		pausePanel.gameObject.SetActive(value: true);
	}

	public void HidePausePanel()
	{
		pausePanel.gameObject.SetActive(value: false);
	}

	private void Start()
	{
	}

	private void OnApplicationQuit()
	{
	}

	public void ShowChoiceLevel()
	{
		choiceLevel.gameObject.SetActive(value: true);
	}

	public void ShowSavePanel(int type)
	{
		savePanel.gameObject.SetActive(value: true);
		savePanel.Init(type);
	}

	public void SetSavePanelItem(SaveItem im)
	{
		savePanel.isnewitem = false;
		savePanel.currentsaveitem = im;
	}

	public int GetSavePanelType()
	{
		return savePanel.type;
	}

	public void LoginSystem()
	{
		HidePausePanel();
		savePanel.BakBtn();
		loadingPanel.gameObject.SetActive(value: true);
		loadingPanel.txt_username.text = gameManager.player.playerdata.nickname;
		loadingPanel.txt_loading.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue("^loading_01"), gameManager.player.playerdata.nickname) + dotstring);
		if ((gameManager.player.GetEventId().Equals("110005") && gameManager.player.playerdata.islast4) || gameManager.isbug)
		{
			StartCoroutine(AddEnd());
			return;
		}
		if (gameManager.player.GetEventId().Equals("110004") && gameManager.player.playerdata.islast4)
		{
			gameManager.player.playerdata.GoToEventID(6, isclear: true);
		}
		loadingPanel.SetLoading(gameManager.player.playerdata.nickname, isnew: false);
	}

	private IEnumerator AddEnd()
	{
		yield return new WaitForSeconds(2f);
		gameManager.ShowFloatBox();
		SceneManager.LoadScene(gameManager.GetHomeSceneName());
		Cursor.visible = true;
		gameManager.musicManager.Stop();
		gameManager.musicManager.GetComponent<AudioSource>().Stop();
		gameManager.soundManager.PlaySound(16);
		if (IsHasAutoSave())
		{
			Debug.Log("自动存档文件存在");
			gameManager.player.playerdata.GoToEventID(6, isclear: true);
			gameManager.txt_studio.SetActive(value: false);
			SceneManager.LoadScene(gameManager.GetHomeSceneName());
		}
		else
		{
			Debug.Log("自动存档文件不存在");
		}
		gameManager.saveManager.SavePlayerData(isshowlogo: false);
		yield return new WaitForSeconds(2f);
		loadingPanel.gameObject.SetActive(value: false);
	}

	public void SavePlayerData(bool isshowlogo = true, bool isForce = false)
	{
		long num = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		isshowlogo = true;
		if (!gameManager.issave || (!isForce && lastTime > 0 && num - lastTime < 300))
		{
			return;
		}
		lastTime = num;
		if (isshowlogo)
		{
			img_saving.SetActive(value: true);
			ani_saving.Play("ani_saving");
			studio.GetComponent<I18NText>().updateTranslation2("SAVING...");
		}
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
		if (!(gameManager.player != null))
		{
			return;
		}
		if (gameManager.IsDlc)
		{
			gameManager.player.playerdata.SetDlcNickname();
		}
		gameManager.player.playerdata.savetime = Convert.ToInt64(timeSpan.TotalSeconds);
		Debug.Log("保存昵称：" + gameManager.player.playerdata.nickname);
		if (gameManager.player.playerdata.nickname.Equals("ALUBA"))
		{
			Debug.Log("有问题    存档保存昵称：" + gameManager.player.playerdata.nickname);
		}
		string value = JsonConvert.SerializeObject(gameManager.player.playerdata);
		try
		{
			if (gameManager.saveManager.GetAutoSaveItem() != null)
			{
				ES3.Save("playerdata", value, "AutoSaveData.es3");
			}
			else
			{
				if (IsHasAutoSave())
				{
					File.Delete(Application.persistentDataPath + "/AutoSaveData.es3");
				}
				ES3.Save("playerdata", value, "AutoSaveData.es3");
			}
			lastTime = num;
		}
		catch (Exception ex)
		{
			Debug.Log("存储失败：" + ex.ToString());
			if (IsHasAutoSave())
			{
				File.Delete(Application.persistentDataPath + "/AutoSaveData.es3");
			}
			ES3.Save("playerdata", value, "AutoSaveData.es3");
		}
	}

	public void SaveManualPlayerData(string path)
	{
		if (!gameManager.player.GetEventId().Equals("110000"))
		{
			img_saving.SetActive(value: true);
			ani_saving.Play("ani_saving");
			studio.GetComponent<I18NText>().updateTranslation2("SAVING...");
			Debug.Log(path + ":保存游戏" + gameManager.player.playerdata.nickname);
			TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
			gameManager.player.playerdata.savetime = Convert.ToInt64(timeSpan.TotalSeconds);
			string text = JsonConvert.SerializeObject(gameManager.player.playerdata);
			Debug.Log("js1:" + text);
			ES3.Save("playerdata", text, "ManualSave/" + gameManager.player.playerdata.nickname + "_" + Convert.ToInt64(timeSpan.TotalSeconds) + ".es3");
			File.Delete(Application.persistentDataPath + "/ManualSave/" + path);
		}
	}

	public void HideSaving()
	{
		img_saving.SetActive(value: false);
		studio.GetComponent<I18NText>().updateTranslation2("ALUBA STUDIO");
	}

	public void CreateNewPlayer(string playername)
	{
		if (gameManager.player != null)
		{
			gameManager.player.CreateNewPlayerData();
			gameManager.player.playerdata.nickname = playername;
			gameManager.player.playerdata.isDLC = false;
			string value = JsonConvert.SerializeObject(gameManager.player.playerdata);
			Debug.Log("保存昵称：" + gameManager.player.playerdata.nickname);
			if (gameManager.player.playerdata.nickname.Equals("ALUBA"))
			{
				Debug.Log("有问题    存档保存昵称：" + gameManager.player.playerdata.nickname);
			}
			ES3.Save("playerdata", value, "AutoSaveData.es3");
		}
	}

	public void CreateDLCNewPlayer(GameTypeEnum gameTypeEnum)
	{
		if (gameManager.player != null)
		{
			gameManager.player.CreateNewPlayerData();
			string nickname = "Aogesi Will";
			bool isDLC = true;
			int eventid = 7;
			if (gameTypeEnum == GameTypeEnum.DLC7)
			{
				nickname = "Benjamin Engel";
				isDLC = false;
				eventid = 8;
			}
			gameManager.player.playerdata.nickname = nickname;
			gameManager.player.playerdata.isDLC = isDLC;
			gameManager.player.playerdata.Eventid = eventid;
			gameManager.GameType = gameTypeEnum;
			gameManager.player.playerdata.GameType = gameTypeEnum;
			string value = JsonConvert.SerializeObject(gameManager.player.playerdata);
			Debug.Log("保存昵称：" + gameManager.player.playerdata.nickname);
			if (gameManager.player.playerdata.nickname.Equals("ALUBA"))
			{
				Debug.Log("有问题    存档保存昵称：" + gameManager.player.playerdata.nickname);
			}
			ES3.Save("playerdata", value, "AutoSaveData.es3");
		}
	}

	public void CreateDLC6NewPlayer()
	{
		if (gameManager.player != null)
		{
			gameManager.player.CreateNewPlayerData();
			gameManager.player.playerdata.nickname = "Aogesi Will";
			gameManager.player.playerdata.isDLC = true;
			string value = JsonConvert.SerializeObject(gameManager.player.playerdata);
			Debug.Log("保存昵称：" + gameManager.player.playerdata.nickname);
			if (gameManager.player.playerdata.nickname.Equals("ALUBA"))
			{
				Debug.Log("有问题    存档保存昵称：" + gameManager.player.playerdata.nickname);
			}
			ES3.Save("playerdata", value, "AutoSaveData.es3");
		}
	}

	public string CreateNewSave(string playername)
	{
		string text = "";
		gameManager.player.playerdata.nickname = playername;
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
		gameManager.player.playerdata.savetime = Convert.ToInt64(timeSpan.TotalSeconds);
		text = playername + "_" + Convert.ToInt64(timeSpan.TotalSeconds) + ".es3";
		string value = JsonConvert.SerializeObject(gameManager.player.playerdata);
		ES3.Save("playerdata", value, "ManualSave/" + text);
		return text;
	}

	public bool IsHasAutoSave()
	{
		return File.Exists(Application.persistentDataPath + "/AutoSaveData.es3");
	}

	private void Update()
	{
		if (!Input.GetKeyUp(KeyCode.Escape) || SceneManager.GetActiveScene().name.Equals("HomeDLC8"))
		{
			return;
		}
		if (SceneManager.GetActiveScene().name.Equals("homego") || SceneManager.GetActiveScene().name.Equals("homeDLC") || SceneManager.GetActiveScene().name.Equals("homeDLC7") || SceneManager.GetActiveScene().name.Equals("homecourse"))
		{
			if (!savePanel.isOver)
			{
				Debug.Log(gameManager.canShowSetting);
				if (gameManager.canShowSetting == 0)
				{
					noEsc.SetActive(value: false);
					if (!pausePanel.gameObject.activeInHierarchy)
					{
						gameManager.saveManager.ShowPausePanel();
						gameManager.homeScene.ShowNewVideoCanvas();
						return;
					}
					gameManager.saveManager.HidePausePanel();
					settingPanel.gameObject.SetActive(value: false);
					savePanel.gameObject.SetActive(value: false);
					backToMainWindow.SetActive(value: false);
					exitWindow.SetActive(value: false);
					gameManager.homeScene.HideNewVideoCanvas();
					return;
				}
				if (pausePanel.gameObject.activeInHierarchy && !savePanel.gameObject.activeInHierarchy)
				{
					Debug.Log("执行");
					gameManager.saveManager.HidePausePanel();
					settingPanel.gameObject.SetActive(value: false);
					backToMainWindow.SetActive(value: false);
					exitWindow.SetActive(value: false);
					gameManager.homeScene.HideNewVideoCanvas();
				}
				if (showEsc != null)
				{
					StopCoroutine(showEsc);
				}
				showEsc = StartCoroutine(ShowEsc());
			}
			else
			{
				savePanel.BakBtn();
			}
		}
		else
		{
			if (showEsc != null)
			{
				StopCoroutine(showEsc);
			}
			showEsc = StartCoroutine(ShowEsc());
		}
	}

	private IEnumerator ShowEsc()
	{
		noEsc.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		noEsc.SetActive(value: false);
	}

	public List<PlayerData> GetAllSaveList()
	{
		Debug.Log("GetAllSaveList");
		if (gameManager.player.playerdata.basicNameList == null)
		{
			gameManager.player.playerdata.basicNameList = new List<string>();
		}
		List<PlayerData> list = new List<PlayerData>();
		if (Directory.Exists(Application.persistentDataPath + "/ManualSave/"))
		{
			FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + "/ManualSave/").GetFiles("*.es3", SearchOption.TopDirectoryOnly);
			Array.Sort(files, fileComparer);
			for (int num = files.Length - 1; num >= 0; num--)
			{
				PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(ES3.Load<string>("playerdata", Application.persistentDataPath + "/ManualSave/" + files[num].Name));
				if (playerData.Eventid < 7 && !gameManager.player.playerdata.basicNameList.Contains(playerData.nickname))
				{
					gameManager.player.playerdata.basicNameList.Add(playerData.nickname);
				}
				list.Add(playerData);
			}
		}
		return list;
	}

	public string GetNormalPlayerName()
	{
		if (gameManager.player.playerdata.basicNameList.Count == 0)
		{
			gameManager.saveManager.GetAllSaveList();
		}
		if (gameManager.player.playerdata.basicNameList.Count > 0)
		{
			string text = "";
			string text2 = "";
			for (int i = 0; i < gameManager.player.playerdata.basicNameList.Count; i++)
			{
				if (!(text != ""))
				{
					string text3 = gameManager.player.playerdata.basicNameList[i];
					if (text3 != "Aogesi Will" || text3 != "Benjamin Engle")
					{
						text = text3;
						break;
					}
					if (text2 != "" && (text3 == "Aogesi Will" || text3 == "Benjamin Engle"))
					{
						text2 = text3;
					}
				}
			}
			if (text == "" && text2 != "")
			{
				text = text2;
			}
			if (text == "" && text2 == "")
			{
				text = "Administrator";
			}
			return text;
		}
		return "Administrator";
	}

	public Dictionary<int, string> getAllLevelInfo()
	{
		Debug.Log("getAllLevelInfo");
		List<PlayerData> allSaveList = GetAllSaveList();
		if (IsHasAutoSave())
		{
			allSaveList.Add(JsonConvert.DeserializeObject<PlayerData>(ES3.Load<string>("playerdata", Application.persistentDataPath + "/AutoSaveData.es3")));
		}
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		for (int i = 0; i < allSaveList.Count; i++)
		{
			Dictionary<int, string> alllevelinfo = allSaveList[i].alllevelinfo;
			foreach (int key in alllevelinfo.Keys)
			{
				string text = alllevelinfo[key];
				if (!dictionary.ContainsKey(key))
				{
					dictionary[key] = text;
				}
				else if (key < 100)
				{
					if (int.Parse(text) > int.Parse(dictionary[key]))
					{
						dictionary[key] = text;
					}
				}
				else if (int.Parse(text) < int.Parse(dictionary[key]))
				{
					dictionary[key] = text;
				}
			}
		}
		Debug.Log(dictionary.ToString());
		return dictionary;
	}

	public List<string> GetAllSavePathList()
	{
		List<string> list = new List<string>();
		if (Directory.Exists(Application.persistentDataPath + "/ManualSave/"))
		{
			FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + "/ManualSave/").GetFiles("*.es3", SearchOption.TopDirectoryOnly);
			Array.Sort(files, fileComparer);
			for (int num = files.Length - 1; num >= 0; num--)
			{
				list.Add(files[num].Name);
			}
		}
		return list;
	}

	public PlayerData GetAutoSaveItem()
	{
		if (File.Exists(Application.persistentDataPath + "/AutoSaveData.es3"))
		{
			try
			{
				PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(ES3.Load<string>("playerdata", Application.persistentDataPath + "/AutoSaveData.es3"));
				string text = JsonConvert.SerializeObject(playerData);
				Debug.LogError("自动存档存在：" + text);
				return playerData;
			}
			catch (Exception ex)
			{
				Debug.Log("自动存档损坏" + ex.ToString());
				return null;
			}
		}
		return null;
	}

	public PlayerData LoadData(string path)
	{
		string text = "";
		Debug.Log("dizhi:" + path);
		text = ((!path.Equals("AutoSaveData.es3")) ? ("/ManualSave/" + path) : "/AutoSaveData.es3");
		if (File.Exists(Application.persistentDataPath + text))
		{
			if (ES3.KeyExists("playerdata", Application.persistentDataPath + text))
			{
				PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(ES3.Load<string>("playerdata", Application.persistentDataPath + text));
				if (playerData.isDLC && !gameManager.IsBuySweetDLC())
				{
					gameManager.ValidDLC6();
					return null;
				}
				return playerData;
			}
			Debug.LogError("无playerdata内容");
		}
		else
		{
			Debug.LogError("无法加载playerdata");
		}
		return null;
	}

	public void DeleteSave(string path)
	{
		if (File.Exists(Application.persistentDataPath + "/ManualSave/" + path))
		{
			File.Delete(Application.persistentDataPath + "/ManualSave/" + path);
		}
	}

	public void LoadAutoData()
	{
		if (File.Exists(Application.persistentDataPath + "/AutoSaveData.es3"))
		{
			PlayerData autoSaveItem = gameManager.saveManager.GetAutoSaveItem();
			if (autoSaveItem != null)
			{
				gameManager.player.playerdata = autoSaveItem;
			}
			else
			{
				Debug.LogError("无法加载playerdata+丢失ALUBA");
			}
		}
		else
		{
			Debug.LogError("无法加载playerdata");
		}
	}

	public void ShowSaveLogo()
	{
		img_saving.SetActive(value: true);
		ani_saving.Play("ani_saving");
	}
}
