using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DownloadDialog : MonoBehaviour
{
	public Text title;

	public Color failedColor;

	[HideInInspector]
	public Dictionary<string, GameObject> file = new Dictionary<string, GameObject>();

	private GameManager gameManager;

	private InvadeDialog parObj;

	[HideInInspector]
	public List<string> loadedID = new List<string>();

	[HideInInspector]
	public List<string> loadingFile = new List<string>();

	private bool taskType;

	public void Init(string[] collectID, GameManager gm, InvadeDialog obj = null)
	{
		gameManager = gm;
		parObj = obj;
		for (int i = 0; i < collectID.Length; i++)
		{
			Debug.Log("可以显示的" + collectID[i]);
			if (collectID[i] != "0")
			{
				GameObject value = Object.Instantiate(Resources.Load<GameObject>("box_downloadfile"), base.transform);
				file.Add(collectID[i], value);
			}
		}
		string key = I18N.instance.getValue("^file_load01") + "(" + 0 + "/" + file.Count + ")";
		title.GetComponent<I18NText>().updateTranslation2(key);
	}

	public void StartLoad(string itemID)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
		}
		if (file.ContainsKey(itemID))
		{
			parObj.listBox.CompleteTask();
			loadingFile.Add(itemID);
			string key = I18N.instance.getValue("^file_load01") + "(" + loadingFile.Count + "/" + file.Count + ")";
			title.GetComponent<I18NText>().updateTranslation2(key);
			float loadTime = parObj.countDown.GetComponent<CountDownDialog>().count_down;
			file[itemID].GetComponent<DownloadFileBox>().StartLoad(loadTime, itemID, this);
		}
		else
		{
			Debug.LogError("收集的id和表的id不符！！！！！！！！");
		}
	}

	public void LoadComplete(string id)
	{
		loadedID.Add(id);
		if (loadedID.Count == file.Count)
		{
			string key = I18N.instance.getValue("^file_load02") + "(" + title.text.Split('(')[1];
			title.GetComponent<I18NText>().updateTranslation2(key);
			gameManager.homeScene.notebook.AddNewItems(loadedID.ToArray());
		}
		Invoke("Hide", 1f);
	}

	public void LoadFailed()
	{
		StartCoroutine(SetFailed());
	}

	private IEnumerator SetFailed()
	{
		yield return new WaitForSeconds(2f);
		taskType = false;
		string key = I18N.instance.getValue("^file_load03") + "(" + title.text.Split('(')[1];
		title.GetComponent<I18NText>().updateTranslation2(key);
		title.color = failedColor;
		foreach (KeyValuePair<string, GameObject> item in file)
		{
			item.Value.GetComponent<DownloadFileBox>().LoadFailed();
		}
		Invoke("Hide", 1f);
	}

	public void LoadSuccess()
	{
		taskType = true;
		foreach (KeyValuePair<string, GameObject> item in file)
		{
			item.Value.GetComponent<DownloadFileBox>().LoadSce();
		}
	}

	private void Hide()
	{
		Object.Destroy(base.gameObject);
	}
}
