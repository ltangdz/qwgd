using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeFolderBox : MonoBehaviour
{
	public GameObject lastLine;

	public GameObject childLine;

	public Text fileName;

	public CanvasGroup imgBak;

	public GameObject list2Par;

	public string listID;

	private GameManager gameManager;

	[HideInInspector]
	public InvadeListBox parObj;

	private void Start()
	{
		list2Par.GetComponent<Button>().onClick.AddListener(ChoiceList);
	}

	private void ChoiceList()
	{
		if (!parObj.parObj.secFile.activeInHierarchy)
		{
			parObj.parObj.secFile.SetActive(value: true);
		}
		if (gameManager.dataManager.dic42[listID].open == "#0")
		{
			parObj.BlurAll();
			imgBak.alpha = 1f;
		}
		string type = gameManager.dataManager.dic42[listID].type;
		string files = gameManager.dataManager.dic42[listID].files;
		if (type == "#0")
		{
			parObj.ShowFileList(files.Substring(1).Split(';'), listID);
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

	public void Init(string id, GameManager gm, InvadeListBox par, bool last)
	{
		listID = id;
		gameManager = gm;
		parObj = par;
		if (last)
		{
			lastLine.SetActive(value: false);
		}
		if ((gameManager.dataManager.dic42[id].type == "#0" && gameManager.dataManager.dic42[id].files.Trim() == "") || gameManager.dataManager.dic42[id].type != "#0")
		{
			childLine.SetActive(value: false);
		}
		fileName.GetComponent<I18NText>().updateTranslation2(gameManager.dataManager.dic42[id].name);
		string[] array = gameManager.dataManager.dic42[id].files.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("invade_list3"), base.transform);
			bool last2 = ((i == array.Length - 1) ? true : false);
			obj.GetComponent<InvadeFileBox>().Init(array[i], gameManager, parObj, last2);
			parObj.parObj.fileObjList.Add(array[i], 0);
		}
	}
}
