using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
	public GameObject settingPannel;

	public GameManager gameManager;

	[SerializeField]
	public Animator backtomainWindow;

	private bool alertCanClick = true;

	[SerializeField]
	public Animator exitWindow;

	[SerializeField]
	public Button btn_back;

	[SerializeField]
	public Button btn_save;

	[SerializeField]
	public Button btn_read;

	[SerializeField]
	public Button btn_setting;

	[SerializeField]
	public Button btn_mainscene;

	[SerializeField]
	public Button btn_quit;

	[SerializeField]
	private Text txt_version;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_version.text = gameManager.str_version;
		btn_back.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			base.gameObject.SetActive(value: false);
			gameManager.CanShowSetting(-1);
		});
		btn_setting.onClick.AddListener(delegate
		{
			if (!settingPannel.activeInHierarchy)
			{
				gameManager.soundManager.PlaySound(16);
				settingPannel.SetActive(value: true);
			}
		});
		btn_save.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			gameManager.saveManager.ShowSavePanel(1);
		});
		btn_read.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			gameManager.saveManager.ShowSavePanel(0);
		});
		btn_mainscene.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			backtomainWindow.gameObject.SetActive(value: true);
			StartCoroutine(StartBacktoMain());
		});
		btn_quit.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			exitWindow.gameObject.SetActive(value: true);
			StartCoroutine(ShowExit());
		});
	}

	private void OnEnable()
	{
		gameManager.saveManager.saveCanvas.worldCamera = Camera.main;
		btn_save.interactable = !gameManager.player.GetEventId().Equals("110000");
		if (gameManager.player.GetEventId().Equals("110000"))
		{
			btn_save.GetComponent<ButtonScale>().SetGray();
			return;
		}
		btn_save.GetComponent<ButtonScale>().enabled = true;
		btn_save.GetComponent<ButtonScale>().SetWhite();
	}

	private void OnDisable()
	{
		if (gameManager.homeScene != null)
		{
			gameManager.homeScene.HideNewVideoCanvas();
		}
	}

	private IEnumerator StartBacktoMain()
	{
		backtomainWindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
	}

	public void Cancle()
	{
		if (alertCanClick)
		{
			gameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StopBacktoMain());
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

	private IEnumerator StopBacktoMain()
	{
		backtomainWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
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
		base.gameObject.SetActive(value: false);
		Application.Quit();
	}

	private IEnumerator ShowExit()
	{
		exitWindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
	}

	public void BakMain()
	{
		backtomainWindow.Play("Exit Panel Out");
		gameManager.saveManager.SavePlayerData();
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
		base.gameObject.SetActive(value: false);
	}
}
