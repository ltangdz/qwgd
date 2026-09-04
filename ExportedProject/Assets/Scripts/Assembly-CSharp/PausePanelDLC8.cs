using System.Collections;
using Aluba;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _DLC8;

public class PausePanelDLC8 : MonoBehaviour
{
	public GameObject settingPannel;

	private GameManager _gameManager;

	[SerializeField]
	public Animator backtomainWindow;

	private bool alertCanClick = true;

	[SerializeField]
	public Animator exitWindow;

	[SerializeField]
	public Button btn_back;

	[SerializeField]
	public Button btn_setting;

	[SerializeField]
	public Button btn_mainscene;

	[SerializeField]
	public Button btn_quit;

	[SerializeField]
	private Text txt_version;

	public GameManager GameManager
	{
		get
		{
			if (_gameManager == null)
			{
				_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return _gameManager;
		}
	}

	private void OnEnable()
	{
		GameManager.soundManager.PlaySound(16);
	}

	private void Start()
	{
		txt_version.text = GameManager.str_version;
		btn_back.onClick.AddListener(delegate
		{
			GameManager.soundManager.PlaySound(16);
			base.gameObject.SetActive(value: false);
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
		});
		btn_setting.onClick.AddListener(delegate
		{
			if (!settingPannel.activeInHierarchy)
			{
				GameManager.soundManager.PlaySound(16);
				settingPannel.SetActive(value: true);
			}
		});
		btn_mainscene.onClick.AddListener(delegate
		{
			GameManager.soundManager.PlaySound(16);
			backtomainWindow.gameObject.SetActive(value: true);
			StartCoroutine(StartBacktoMain());
		});
		btn_quit.onClick.AddListener(delegate
		{
			GameManager.soundManager.PlaySound(16);
			exitWindow.gameObject.SetActive(value: true);
			StartCoroutine(ShowExit());
		});
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
			GameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StopBacktoMain());
		}
	}

	public void ExitCancle()
	{
		if (alertCanClick)
		{
			GameManager.soundManager.PlaySound(16);
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
		GameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		GameManager.soundManager.Stop();
		GameManager.musicManager.PlayMusicLoop(8);
		base.gameObject.SetActive(value: false);
	}
}
