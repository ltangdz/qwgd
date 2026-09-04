using System;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	public Button sureBtn;

	public Button sureBtn2;

	public Button bakBtn;

	public Button lanLeftBtn;

	public Button lanRightBtn;

	public Text lanLabel;

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

	private string choiceLanguage;

	private string choiceResolution = "";

	private int lanIndex;

	private int resIndex;

	private bool musicNoVoice;

	private bool soundNoVoice;

	public GameManager gameManager;

	private int effects = 1;

	public Toggle tog_ishaseffect;

	public Toggle tog_isnothaseffect;

	public int cameramove = 1;

	public Toggle tog_cameramove;

	public Toggle tog_cameranotmove;

	public int redline = 1;

	public Toggle tog_redline;

	public Toggle tog_notredline;

	private void OnEnable()
	{
		sureBtn.interactable = true;
		sureBtn2.interactable = true;
		gameManager.saveManager.saveCanvas.worldCamera = Camera.main;
		string text = Screen.width + " × " + Screen.height;
		for (int i = 0; i < winresolution.Length; i++)
		{
			if (text.Equals(winresolution[i]))
			{
				choiceResolution = winresolution[i];
			}
		}
		InitLanguage();
		Init();
		FreshResolution(Screen.width, Screen.height);
	}

	private void FreshResolution(float width, float height)
	{
		if ((SceneManager.GetActiveScene().name.Equals("homego") || SceneManager.GetActiveScene().name.Equals("homeDLC") || SceneManager.GetActiveScene().name.Equals("homeDLC7") || SceneManager.GetActiveScene().name.Equals("homecourse") || SceneManager.GetActiveScene().name.Equals("HomeDLC8")) && !(gameManager.homeScene == null))
		{
			float num = float.Parse((width / height).ToString("f2"));
			if (num == 1.77f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 935f);
			}
			else if (num == 1.33f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1248f);
			}
			else if (num == 1.6f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1038f);
			}
			else if (num == 1.5f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1108f);
			}
			else if (num == 1.56f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1065f);
			}
			else if (num == 1.25f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1332f);
			}
			else if (num == 1.66f)
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 998f);
			}
			else
			{
				gameManager.homeScene.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 935f);
			}
		}
	}

	private void InitLanguage()
	{
		_ = I18N.instance.gameLang;
		if (I18N.instance.gameLang == LanguageCode.CN)
		{
			choiceLanguage = language[0];
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			choiceLanguage = language[2];
		}
		else
		{
			choiceLanguage = language[1];
		}
	}

	private void Start()
	{
		InitLanguage();
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
		sureBtn2.onClick.AddListener(SureBtn);
		bakBtn.onClick.AddListener(BakBtn);
		lanLeftBtn.onClick.AddListener(LanLeftBtn);
		lanRightBtn.onClick.AddListener(LanRightBtn);
		resLeftBtn.onClick.AddListener(ResLeftBtn);
		resRightBtn.onClick.AddListener(ResRightBtn);
		FreshResolution(Screen.width, Screen.height);
		sureBtn.interactable = true;
		sureBtn2.interactable = true;
	}

	private void Init()
	{
		switch (SceneManager.GetActiveScene().name)
		{
		case "homego":
		case "homecourse":
		case "homeDLC":
			lanLabel.text = I18N.instance.getValue("^language_nochange");
			lanLeftBtn.gameObject.SetActive(value: false);
			lanRightBtn.gameObject.SetActive(value: false);
			lanLabel.transform.parent.Find("left").GetComponent<ButtonScale>().enabled = false;
			lanLabel.transform.parent.Find("right").GetComponent<ButtonScale>().enabled = false;
			break;
		default:
			lanLabel.text = choiceLanguage;
			lanLeftBtn.gameObject.SetActive(value: true);
			lanRightBtn.gameObject.SetActive(value: true);
			lanLabel.transform.parent.Find("left").GetComponent<ButtonScale>().enabled = true;
			lanLabel.transform.parent.Find("right").GetComponent<ButtonScale>().enabled = true;
			break;
		}
		if (choiceResolution == null || choiceResolution.Equals(""))
		{
			choiceResolution = "1920 × 1080";
		}
		resLabel.text = choiceResolution;
		lanIndex = GetIndex(choiceLanguage, language);
		resIndex = GetIndex(choiceResolution, winresolution);
		lanLabel.text = choiceLanguage;
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
		effects = PlayerPrefs.GetInt("gameeffects", 0);
		gameManager.isshoweffect = effects == 1;
		if (GameObject.Find("Main Camera").GetComponent<CameraFilterPack_Color_Noise>() != null)
		{
			GameObject.Find("Main Camera").GetComponent<CameraFilterPack_Color_Noise>().enabled = gameManager.isshoweffect;
		}
		tog_ishaseffect.isOn = effects == 1;
		tog_isnothaseffect.isOn = ((effects != 1) ? true : false);
		cameramove = PlayerPrefs.GetInt("cameramove", 0);
		tog_cameramove.isOn = cameramove == 1;
		tog_cameranotmove.isOn = ((cameramove != 1) ? true : false);
		redline = PlayerPrefs.GetInt("redline", 1);
		tog_redline.isOn = redline == 1;
		tog_notredline.isOn = ((redline != 1) ? true : false);
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

	public void Effects(int a)
	{
		effects = a;
	}

	public void CameraMove(int c)
	{
		cameramove = c;
	}

	public void Redline(int c)
	{
		redline = c;
	}

	private void SureBtn()
	{
		if (SceneManager.GetActiveScene().name == "home")
		{
			sureBtn.interactable = false;
			sureBtn2.interactable = false;
			try
			{
				if (choiceLanguage.Equals(language[0]))
				{
					I18N.instance.setLanguage(LanguageCode.CN);
					GameObject.Find("LoginCanvas").GetComponent<LoginCanvas>().loginPanel.qqgroup.SetActive(value: true);
					PlayerPrefs.SetInt("language", 0);
				}
				else if (choiceLanguage.Equals(language[1]))
				{
					I18N.instance.setLanguage(LanguageCode.EN);
					PlayerPrefs.SetInt("language", 1);
					GameObject.Find("LoginCanvas").GetComponent<LoginCanvas>().loginPanel.qqgroup.SetActive(value: false);
				}
				else if (choiceLanguage.Equals(language[2]))
				{
					I18N.instance.setLanguage(LanguageCode.TC);
					PlayerPrefs.SetInt("language", 2);
					GameObject.Find("LoginCanvas").GetComponent<LoginCanvas>().loginPanel.qqgroup.SetActive(value: true);
				}
				if (gameManager.loginPanel != null)
				{
					gameManager.loginPanel.ChangeDLCNotice();
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
			}
		}
		gameManager.soundManager.PlaySound(16);
		PlayerPrefs.SetInt("gameeffects", effects);
		gameManager.isshoweffect = effects == 1;
		if (GameObject.Find("Main Camera").GetComponent<CameraFilterPack_Color_Noise>() != null)
		{
			GameObject.Find("Main Camera").GetComponent<CameraFilterPack_Color_Noise>().enabled = gameManager.isshoweffect;
		}
		Screen.SetResolution(int.Parse(choiceResolution.Split('×')[0].Trim()), int.Parse(choiceResolution.Split('×')[1].Trim()), tog_fullscreen.isOn ? true : false);
		BakBtn();
		gameManager.isshowredline = redline == 1;
		PlayerPrefs.SetInt("cameramove", cameramove);
		PlayerPrefs.SetInt("redline", redline);
		if (gameManager.isshowredline)
		{
			gameManager.player.playerdata.isfixredline = true;
			gameManager.saveManager.SavePlayerData();
		}
		FreshResolution(float.Parse(choiceResolution.Split('×')[0].Trim()), float.Parse(choiceResolution.Split('×')[1].Trim()));
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
		gameManager.saveManager.SavePlayerData();
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
	}

	public void ExitGame()
	{
		gameManager.saveManager.SavePlayerData();
		Application.Quit();
	}

	private void LanLeftBtn()
	{
		lanIndex = ((lanIndex <= 0) ? (language.Length - 1) : (lanIndex - 1));
		lanLabel.text = language[lanIndex];
		choiceLanguage = language[lanIndex];
	}

	private void LanRightBtn()
	{
		lanIndex = ((lanIndex < language.Length - 1) ? (lanIndex + 1) : 0);
		lanLabel.text = language[lanIndex];
		choiceLanguage = language[lanIndex];
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
