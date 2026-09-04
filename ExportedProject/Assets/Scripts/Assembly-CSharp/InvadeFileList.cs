using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeFileList : MonoBehaviour
{
	public Text filename;

	public Text size;

	public Text time;

	private InvadeListBox parObj;

	private GameManager gameManager;

	private string listID;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(ChoiceList);
	}

	private void ChoiceList()
	{
		if (!parObj.parObj.secFile.activeInHierarchy)
		{
			parObj.parObj.secFile.SetActive(value: true);
		}
		if (gameManager.dataManager.dic42[listID].open == "#1")
		{
			Object.Instantiate(Resources.Load<GameObject>("invade_alert"), parObj.parObj.content).GetComponent<InvadeAlert>().Init("^invade_label39");
			return;
		}
		string type = gameManager.dataManager.dic42[listID].type;
		string files = gameManager.dataManager.dic42[listID].files;
		if (type == "#0")
		{
			parObj.ShowFileList(files.Substring(1).Split(';'), "open");
		}
		else if (type == "#1")
		{
			if (gameManager.dataManager.dic42[listID].searchfile != "")
			{
				string[] files2 = gameManager.dataManager.dic42[listID].searchfile.Split(';');
				parObj.ShowSearchPanel(files2);
			}
			else
			{
				parObj.ShowFileImg(files);
			}
		}
	}

	public void Init(string id, GameManager gm, InvadeListBox par)
	{
		listID = id;
		gameManager = gm;
		parObj = par;
		string key = gameManager.dataManager.dic42[id].name;
		string key2 = gameManager.dataManager.dic42[id].size;
		string key3 = gameManager.dataManager.dic42[id].time;
		filename.GetComponent<I18NText>().updateTranslation2(key);
		size.GetComponent<I18NText>().updateTranslation2(key2);
		time.GetComponent<I18NText>().updateTranslation2(key3);
	}
}
