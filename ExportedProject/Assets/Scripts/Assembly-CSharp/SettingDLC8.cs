using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingDLC8 : MonoBehaviour
{
	public Button sureBtn;

	public Button resolutionBtn;

	public Button bakBtn;

	public Button resLeftBtn;

	public Button resRightBtn;

	public Text resLabel;

	public Slider musicSlider;

	public Slider soundSlider;

	public Text musicVoice;

	public Text soundVoice;

	public Image musicIcon;

	public Image soundIcon;

	public Sprite haveVoice;

	public Sprite noVoice;

	public Animator exitWindow;

	public GameObject bakconfirm;

	public Toggle tog_window;

	public Toggle tog_fullscreen;

	private string[] language = new string[3] { "简体中文 Simplified Chinese", "English", "繁體中文  Traditional Chinese" };

	public string[] winresolution = new string[17]
	{
		"1920 × 1080", "1680 × 1050", "1600 × 1024", "1600 × 900", "1440 × 900", "1366 × 768", "1360 × 768", "1280 × 1024", "1280 × 960", "1280 × 800",
		"1280 × 768", "1280 × 720", "1176 × 664", "1152 × 864", "1024 × 768", "960 × 640", "800 × 600"
	};

	public string[] macresolution = new string[19]
	{
		"2880 × 1800", "2560 × 1600", "1920 × 1080", "1680 × 1050", "1600 × 1024", "1600 × 900", "1440 × 900", "1366 × 768", "1360 × 768", "1280 × 1024",
		"1280 × 960", "1280 × 800", "1280 × 768", "1280 × 720", "1176 × 664", "1152 × 864", "1024 × 768", "960 × 640", "800 × 600"
	};

	private string choiceResolution = "";

	private int resIndex;

	private bool musicNoVoice;

	private bool soundNoVoice;

	private GameManager gameManager;

	private void OnEnable()
	{
		sureBtn.interactable = true;
		resolutionBtn.interactable = true;
		string text = Screen.width + " × " + Screen.height;
		for (int i = 0; i < winresolution.Length; i++)
		{
			if (text.Equals(winresolution[i]))
			{
				choiceResolution = winresolution[i];
			}
		}
		Init();
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		string text = Screen.width + " × " + Screen.height;
		for (int i = 0; i < winresolution.Length; i++)
		{
			if (text.Equals(winresolution[i]))
			{
				choiceResolution = winresolution[i];
			}
		}
		Init();
		sureBtn.onClick.AddListener(SureBtn);
		resolutionBtn.onClick.AddListener(SureBtn);
		bakBtn.onClick.AddListener(BakBtn);
		resLeftBtn.onClick.AddListener(ResLeftBtn);
		resRightBtn.onClick.AddListener(ResRightBtn);
		sureBtn.interactable = true;
		resolutionBtn.interactable = true;
	}

	private void Init()
	{
		_ = SceneManager.GetActiveScene().name;
		if (choiceResolution == null || choiceResolution.Equals(""))
		{
			choiceResolution = "1920 × 1080";
		}
		resLabel.text = choiceResolution;
		resIndex = GetIndex(choiceResolution, winresolution);
		if (Screen.fullScreen)
		{
			tog_fullscreen.isOn = true;
			tog_window.isOn = false;
		}
		else
		{
			tog_fullscreen.isOn = false;
			tog_window.isOn = true;
		}
		RefreshResToggleMac();
		musicSlider.value = PlayerPrefs.GetFloat("musicvol", 1f);
		soundSlider.value = PlayerPrefs.GetFloat("soundvol", 1f);
	}

	private void RefreshResToggleMac()
	{
		_ = Screen.currentResolution;
		tog_fullscreen.gameObject.SetActive(value: true);
		tog_window.gameObject.SetActive(value: true);
	}

	private int GetIndex(string val, string[] array)
	{
		int result = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == val)
			{
				result = i;
			}
		}
		return result;
	}

	private void SureBtn()
	{
		gameManager.soundManager.PlaySound(16);
		Screen.SetResolution(int.Parse(choiceResolution.Split('×')[0].Trim()), int.Parse(choiceResolution.Split('×')[1].Trim()), tog_fullscreen.isOn ? true : false);
		BakBtn();
	}

	public void BakBtn()
	{
		gameManager.soundManager.PlaySound(16);
		GetComponent<Animator>().SetBool("closeSetting", value: true);
		Invoke("HideSetting", 2f);
	}

	private void BakMainBtn()
	{
		bakconfirm.gameObject.SetActive(value: true);
		bakconfirm.GetComponent<Animator>().Play("Exit Panel In");
	}

	private void OutGameBtn()
	{
		exitWindow.gameObject.SetActive(value: true);
		exitWindow.GetComponent<Animator>().Play("Exit Panel In");
	}

	public void CancelExitGame(Animator confirm)
	{
		confirm.Play("Exit Panel Out");
	}

	public void BakMain()
	{
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	private void ResLeftBtn()
	{
		Resolution currentResolution = Screen.currentResolution;
		resIndex = ((resIndex <= 0) ? (winresolution.Length - 1) : (resIndex - 1));
		resLabel.text = winresolution[resIndex];
		choiceResolution = winresolution[resIndex];
		if (tog_window.isOn)
		{
			string[] array = choiceResolution.Split('×');
			if (int.Parse(array[0].Trim()) > currentResolution.width || int.Parse(array[1].Trim()) > currentResolution.height)
			{
				ResLeftBtn();
				return;
			}
		}
		RefreshResToggleMac();
	}

	private void ResRightBtn()
	{
		Resolution currentResolution = Screen.currentResolution;
		resIndex = ((resIndex < winresolution.Length - 1) ? (resIndex + 1) : 0);
		resLabel.text = winresolution[resIndex];
		choiceResolution = winresolution[resIndex];
		if (tog_window.isOn)
		{
			string[] array = choiceResolution.Split('×');
			if (int.Parse(array[0].Trim()) > currentResolution.width || int.Parse(array[1].Trim()) > currentResolution.height)
			{
				ResRightBtn();
				return;
			}
		}
		RefreshResToggleMac();
	}

	private void HideSetting()
	{
		base.gameObject.SetActive(value: false);
	}

	public void Music()
	{
		float num = float.Parse(musicSlider.value.ToString("f2"));
		PlayerPrefs.SetFloat("musicvol", num);
		musicVoice.GetComponent<I18NText>().updateTranslation2((num * 100f).ToString());
		musicIcon.sprite = ((num * 100f == 0f) ? noVoice : haveVoice);
		gameManager.musicManager.GetComponent<AudioSource>().volume = num;
		musicNoVoice = false;
	}

	public void Sound()
	{
		float num = float.Parse(soundSlider.value.ToString("f2"));
		PlayerPrefs.SetFloat("soundvol", num);
		soundVoice.GetComponent<I18NText>().updateTranslation2((num * 100f).ToString());
		soundIcon.sprite = ((num * 100f == 0f) ? noVoice : haveVoice);
		gameManager.soundManager.GetComponent<AudioSource>().volume = num;
		soundNoVoice = false;
	}

	public void MusicNoVoice()
	{
		if (!musicNoVoice)
		{
			musicNoVoice = true;
			gameManager.musicManager.GetComponent<AudioSource>().mute = true;
			musicIcon.sprite = noVoice;
		}
		else
		{
			musicNoVoice = false;
			gameManager.musicManager.GetComponent<AudioSource>().mute = false;
			musicIcon.sprite = haveVoice;
		}
	}

	public void SoundNoVoice()
	{
		if (!soundNoVoice)
		{
			soundNoVoice = true;
			gameManager.soundManager.GetComponent<AudioSource>().mute = true;
			soundIcon.sprite = noVoice;
		}
		else
		{
			soundNoVoice = false;
			gameManager.soundManager.GetComponent<AudioSource>().mute = false;
			soundIcon.sprite = haveVoice;
		}
	}

	public void IsShow()
	{
	}
}
