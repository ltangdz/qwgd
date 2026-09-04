using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Address : MonoBehaviour
{
	public InputField searchInfo;

	public Button searchBtn;

	public BrowserDialog browserDialog;

	private GameManager gameManager;

	private string eventID;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		eventID = gameManager.player.GetEventId();
		searchBtn.onClick.AddListener(StartSearch);
	}

	private void StartSearch()
	{
		gameManager.player.playerdata.UseSocialMethod(0);
		string text = searchInfo.text;
		if (!text.Contains("toothbook.com"))
		{
			return;
		}
		string tbnum = gameManager.dataManager.dic11[eventID].tbnum;
		if (tbnum == "" && tbnum == " ")
		{
			return;
		}
		Debug.Log(tbnum);
		string[] array = tbnum.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Debug.Log(array[i] + ":::" + text + ":::" + I18N.instance.getValue(gameManager.dataManager.dic14[array[i]].nickname));
			if (text.Contains(I18N.instance.getValue(gameManager.dataManager.dic14[array[i]].nickname).ToLower()))
			{
				Debug.Log(array[i] + "###");
				browserDialog.AddSocialPanel(array[i], isadmin: false, text);
			}
		}
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) && searchInfo.isFocused)
		{
			StartSearch();
		}
	}
}
