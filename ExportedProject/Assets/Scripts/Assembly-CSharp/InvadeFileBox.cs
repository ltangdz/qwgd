using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeFileBox : MonoBehaviour
{
	public GameObject lastLine;

	public GameObject fileName;

	public CanvasGroup imgBak;

	public string listID;

	private GameManager gameManager;

	public InvadeListBox parObj;

	public bool del;

	private void Start()
	{
		parObj.parObj.itemFileBox.Add(this);
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
		parObj.BlurAll();
		imgBak.alpha = 1f;
		string type = gameManager.dataManager.dic42[listID].type;
		string files = gameManager.dataManager.dic42[listID].files;
		if (type == "#0")
		{
			parObj.ShowFileList(files.Substring(1).Split(';'));
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
		if (gameManager.dataManager.dic42[id].del != "")
		{
			del = true;
		}
		if (last)
		{
			lastLine.SetActive(value: false);
		}
		if (gameManager.dataManager.dic42[id].secure != "")
		{
			parObj.parObj.secFile = base.gameObject;
		}
		fileName.GetComponent<I18NText>().updateTranslation2(gameManager.dataManager.dic42[id].name);
	}
}
