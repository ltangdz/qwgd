using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameManager gameManager;

	public PlayerData playerdata;

	private int savecount;

	private void Start()
	{
		if (Directory.Exists(Application.persistentDataPath + "/ManualSave/"))
		{
			FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + "/ManualSave/").GetFiles("*.es3", SearchOption.TopDirectoryOnly);
			playerdata = JsonConvert.DeserializeObject<PlayerData>(ES3.Load<string>("playerdata", Application.persistentDataPath + "/ManualSave/" + files[files.Length - 1].Name));
			gameManager.IsDlc = playerdata.isDLC;
			gameManager.GameType = playerdata.GameType;
		}
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		InvokeRepeating("TimeInterval", 0f, 60f);
	}

	private void OnDestroy()
	{
		CancelInvoke();
	}

	public string GetLevel(int eventid)
	{
		string result = "";
		if (playerdata.alllevelinfo.ContainsKey(eventid))
		{
			result = playerdata.alllevelinfo[eventid];
		}
		return result;
	}

	public string GetLevelTime(int eventid)
	{
		string result = "";
		if (playerdata.alllevelinfo.ContainsKey(1000 + eventid))
		{
			result = playerdata.alllevelinfo[1000 + eventid];
		}
		return result;
	}

	public void OpenLevel()
	{
		if (!playerdata.alllevelinfo.ContainsKey(playerdata.Eventid))
		{
			playerdata.alllevelinfo.Add(playerdata.Eventid, "0");
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void OpenSpecialLevel(int i)
	{
		if (!playerdata.alllevelinfo.ContainsKey(i))
		{
			playerdata.alllevelinfo.Add(i, "0");
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void RefreshLevel(string count, string time)
	{
		Debug.Log("playerdata.eventid:" + playerdata.Eventid + ":" + count + ":time:" + time);
		if (playerdata.alllevelinfo.ContainsKey(playerdata.Eventid))
		{
			playerdata.alllevelinfo[playerdata.Eventid] = count;
		}
		else
		{
			playerdata.alllevelinfo.Add(playerdata.Eventid, count);
		}
		if (playerdata.alllevelinfo.ContainsKey(1000 + playerdata.Eventid))
		{
			playerdata.alllevelinfo[1000 + playerdata.Eventid] = time;
		}
		else
		{
			playerdata.alllevelinfo.Add(1000 + playerdata.Eventid, time);
		}
		gameManager.saveManager.SavePlayerData();
	}

	public void AddEventID(bool isadd = false)
	{
		if (isadd)
		{
			playerdata.Eventid++;
		}
		ClearEvent();
		gameManager.saveManager.SavePlayerData();
	}

	public void ClearEvent()
	{
		playerdata.ClearEvent0();
	}

	public void CreateNewPlayerData()
	{
		playerdata.CreateNewPlayerData();
	}

	private void TimeInterval()
	{
		if (playerdata.getMask && (SceneManager.GetActiveScene().name.Equals("homego") || SceneManager.GetActiveScene().name.Equals("homeDLC") || SceneManager.GetActiveScene().name.Equals("homeDLC7") || SceneManager.GetActiveScene().name.Equals("homecourse")))
		{
			savecount++;
			playerdata.AddGameTime(60L);
			if (savecount == 5)
			{
				savecount = 0;
			}
		}
	}

	public void AddMail(string name, string key, int readType = 0, bool issave = true)
	{
		if (playerdata.maillist.ContainsKey(name))
		{
			if (!playerdata.maillist[name][0].ContainsKey(key))
			{
				playerdata.maillist[name][0].Add(key, readType);
			}
		}
		else
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary.Add(key, readType);
			List<Dictionary<string, int>> list = new List<Dictionary<string, int>>();
			list.Add(dictionary);
			playerdata.maillist.Add(name, list);
		}
	}

	public void SendMail(string name, string key, int readType = 0, bool issave = true)
	{
		AddMail(name, key, readType, issave);
		if (issave)
		{
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void RemoveMail(string name, string key)
	{
		if (playerdata.maillist.ContainsKey(name) && playerdata.maillist[name][0].ContainsKey(key))
		{
			playerdata.maillist[name][0].Remove(key);
		}
	}

	public string GetEventId()
	{
		return gameManager.dataManager.dic0[playerdata.Eventid.ToString()].eventid.ToString();
	}

	public string GetEventId(PlayerData playerData)
	{
		return gameManager.dataManager.dic0[playerData.Eventid.ToString()].eventid.ToString();
	}
}
