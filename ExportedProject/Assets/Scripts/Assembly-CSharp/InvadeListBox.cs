using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeListBox : MonoBehaviour
{
	public GameObject listBox;

	public GameObject fileInfoBox;

	public string serverID;

	private GameManager gameManager;

	[HideInInspector]
	public InvadeDialog parObj;

	public Text taskList;

	public Button quitBtn;

	private int cptTask;

	private int taskVal;

	public void Init(string id, GameManager gm, InvadeDialog par = null)
	{
		if (!(id.Trim() == ""))
		{
			serverID = id;
			gameManager = gm;
			parObj = par;
			string file = gameManager.dataManager.dic33[id].file;
			string[] file2 = (string.IsNullOrEmpty(file) ? new string[0] : file.Substring(1).Split(';'));
			SetFile(file2);
			string mission = gameManager.dataManager.dic33[id].mission;
			taskVal = ((!string.IsNullOrEmpty(mission)) ? mission.Substring(1).Split(';').Length : 0);
			taskList.GetComponent<I18NText>().updateTranslation2(cptTask + "/" + taskVal);
			quitBtn.onClick.AddListener(delegate
			{
				parObj.TaskOver();
			});
		}
	}

	private void SetFile(string[] fileList)
	{
		for (int i = 0; i < fileList.Length; i++)
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("invade_list2"), listBox.transform);
			bool last = ((i == fileList.Length - 1) ? true : false);
			obj.GetComponent<InvadeFolderBox>().Init(fileList[i], gameManager, this, last);
		}
	}

	public void CompleteTask()
	{
		cptTask++;
		if (cptTask >= taskVal)
		{
			parObj.selectTaskOver = true;
			cptTask = taskVal;
			gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[parObj.userid].name] = 1;
			quitBtn.interactable = true;
			gameManager.homeScene.notebook.allinvadeserveritems.Clear();
		}
		taskList.GetComponent<I18NText>().updateTranslation2(cptTask + "/" + taskVal);
	}

	public void BlurAll()
	{
		float num = listBox.transform.childCount;
		for (int i = 0; (float)i < num; i++)
		{
			if (!(listBox.transform.GetChild(i).name != "list1"))
			{
				continue;
			}
			listBox.transform.GetChild(i).GetComponent<InvadeFolderBox>().imgBak.alpha = 0f;
			for (int j = 0; j < listBox.transform.GetChild(i).childCount; j++)
			{
				if (listBox.transform.GetChild(i).GetChild(j).name.IndexOf("invade") > -1)
				{
					listBox.transform.GetChild(i).GetChild(j).GetComponent<InvadeFileBox>()
						.imgBak.alpha = 0f;
				}
			}
		}
	}

	public void ShowRoot()
	{
		string[] array = gameManager.dataManager.dic33[serverID].file.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Object.Instantiate(Resources.Load<GameObject>("invade_filelist"), fileInfoBox.transform).GetComponent<InvadeFileList>().Init(array[i], gameManager, this);
		}
	}

	public void ShowFileList(string[] file, string foldID = "")
	{
		ClearOrgFile();
		for (int i = 0; i < file.Length; i++)
		{
			if (foldID != "" && parObj.fileObjList[file[i]] == 0)
			{
				Object.Instantiate(Resources.Load<GameObject>("invade_filelist"), fileInfoBox.transform).GetComponent<InvadeFileList>().Init(file[i], gameManager, this);
			}
		}
	}

	public void ShowFileImg(string file, string[] fileList = null)
	{
		ClearOrgFile();
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Image/" + file), fileInfoBox.transform);
		if (fileList != null)
		{
			gameObject.GetComponent<InvadeSearchResult>().Init(fileList);
		}
	}

	public void ShowSearchPanel(string[] files)
	{
		ClearOrgFile();
		Object.Instantiate(Resources.Load<GameObject>("Dialog/invadeSearchPanel"), fileInfoBox.transform).GetComponent<InvadeSearchPanel>().Init(files, this);
	}

	private void ClearOrgFile()
	{
		for (int i = 0; i < fileInfoBox.transform.childCount; i++)
		{
			Object.Destroy(fileInfoBox.transform.GetChild(i).gameObject);
		}
	}
}
