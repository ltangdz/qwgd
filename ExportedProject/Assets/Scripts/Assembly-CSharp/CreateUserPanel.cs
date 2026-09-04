using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserPanel : MonoBehaviour
{
	public InputField inputField;

	public Button btn_create;

	public Button btn_setting;

	public Button btn_back;

	public LoginCanvas loginCanvas;

	public Animator sureWindow;

	public GameManager gameManager;

	public GameObject settingPannel;

	public GameObject beginPanel;

	private bool alertCanClick = true;

	private bool waitCreat;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_create.onClick.AddListener(delegate
		{
			string text = inputField.text.Trim();
			string[] array = new string[7] { "\\", "/", "*", "\"", "<", ">", "|" };
			for (int i = 0; i < array.Length; i++)
			{
				if (text.Contains(array[i]))
				{
					return;
				}
			}
			if (!inputField.text.Trim().Equals("") && !waitCreat)
			{
				gameManager.soundManager.PlaySound(16);
				sureWindow.gameObject.SetActive(value: true);
				sureWindow.Play("Exit Panel In");
				waitCreat = true;
			}
		});
		btn_setting.onClick.AddListener(delegate
		{
			if (!settingPannel.activeInHierarchy)
			{
				settingPannel.SetActive(value: true);
			}
		});
		btn_back.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			loginCanvas.loginPanel.gameObject.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		});
	}

	public void Cancle()
	{
		if (alertCanClick)
		{
			waitCreat = false;
			gameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StopSignUp());
		}
	}

	public void SignUp()
	{
		if (alertCanClick)
		{
			gameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StartSignUp());
			string playername = inputField.text.Trim();
			gameManager.IsDlc = false;
			gameManager.GameType = GameTypeEnum.BASIC;
			gameManager.saveManager.CreateNewPlayer(playername);
		}
	}

	private IEnumerator StopSignUp()
	{
		sureWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
		sureWindow.gameObject.SetActive(value: false);
	}

	private IEnumerator StartSignUp()
	{
		sureWindow.gameObject.SetActive(value: true);
		sureWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
		loginCanvas.loadingPanel.gameObject.SetActive(value: true);
		loginCanvas.loadingPanel.SetLoading(inputField.text.Trim(), isnew: true);
		base.gameObject.SetActive(value: false);
		gameManager.player.playerdata.nickname = inputField.text.Trim();
		gameManager.IsDlc = false;
		gameManager.GameType = GameTypeEnum.BASIC;
		gameManager.saveManager.SavePlayerData();
		if (gameManager.player.playerdata.isCourseOver == 0)
		{
			yield return new WaitForSeconds(2f);
			beginPanel.SetActive(value: true);
			gameManager.player.playerdata.startTime = long.Parse(gameManager.dataManager.dic11[gameManager.player.GetEventId()].date);
		}
	}

	private void Update()
	{
		if (!Input.GetKeyUp(KeyCode.Return) && !Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			return;
		}
		if (sureWindow.gameObject.activeInHierarchy)
		{
			SignUp();
			return;
		}
		base.transform.GetSiblingIndex();
		_ = base.transform.parent.childCount;
		if (!inputField.text.Trim().Equals(""))
		{
			sureWindow.gameObject.SetActive(value: true);
			sureWindow.Play("Exit Panel In");
		}
	}
}
