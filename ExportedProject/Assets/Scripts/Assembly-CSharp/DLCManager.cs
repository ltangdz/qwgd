using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DLCManager : MonoBehaviour
{
	private delegate void BackupGroundDelegate(bool b);

	public I18N i18n;

	public SQLManager sqlManager;

	public DataManager dataManager;

	public MusicManager musicManager;

	public SoundManager soundManager;

	public SaveManager saveManager;

	public ReasoningManager reasoningManager;

	public Player player;

	public string hideColor;

	public FadeInOutControl fadeInOutControl;

	private BackupGroundDelegate dele;

	private bool isMainScene;

	private float gameOnlineTime;

	public DLCScene homeScene;

	public CameraMove maincamera;

	public bool holdEsc;

	public GameObject Esc;

	public GameObject floatBox;

	public GameObject whiteFloatBox;

	public GameObject setting;

	public bool iscancollect = true;

	public bool isshoweffect;

	public bool istaohuashow;

	public bool isshowexplainalert;

	public StartAniManager startAniManager;

	public Camera startMainCanvas;

	public bool isbug;

	public int canShowSetting;

	public bool issteam;

	public bool issave = true;

	public SteamAchi steamAchi;

	public string str_version;

	public GameObject txt_studio;

	public LoginPanel loginPanel;

	public bool isshowredline = true;

	public bool iscanhoutaiclose = true;

	public GameObject failedBlack;

	private IEnumerator hidefloat;

	private IEnumerator showfloat;

	private void Awake()
	{
		Screen.sleepTimeout = -1;
		hideColor = "black";
		isMainScene = false;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Debug.LogError("当前版本：" + str_version);
	}

	public void UnlockAchievements(string achiname)
	{
		Debug.Log("解锁成就" + achiname);
		if (steamAchi != null && issteam)
		{
			steamAchi.UnlockAchievements(achiname);
		}
	}

	public void CanShowSetting(int a)
	{
		canShowSetting += a;
		if (canShowSetting <= 0)
		{
			canShowSetting = 0;
		}
	}

	private void Start()
	{
		dele = fadeInOutControl.BackGroundControl;
		GUIUtility.systemCopyBuffer = "";
		if (!issteam)
		{
			switch (PlayerPrefs.GetInt("language", -1))
			{
			case -1:
			{
				string text = Application.systemLanguage.ToString();
				if (text.CompareTo("ChineseSimplified") == 0 || text.CompareTo("Chinese") == 0)
				{
					I18N.instance.setLanguage("CN");
				}
				else if (text.CompareTo("ChineseTraditional") == 0)
				{
					I18N.instance.setLanguage("TC");
				}
				else
				{
					I18N.instance.setLanguage("EN");
				}
				break;
			}
			case 0:
				I18N.instance.setLanguage("CN");
				PlayerPrefs.SetInt("language", 0);
				break;
			case 2:
				I18N.instance.setLanguage("TC");
				PlayerPrefs.SetInt("language", 2);
				break;
			default:
				I18N.instance.setLanguage("EN");
				PlayerPrefs.SetInt("language", 1);
				break;
			}
		}
		MonoBehaviour.print(Screen.currentResolution);
		MonoBehaviour.print(Display.displays[0].renderingWidth + "::" + Display.displays[0].renderingHeight + "::" + Display.displays[0].systemWidth + "::" + Display.displays[0].systemHeight);
		isshowredline = PlayerPrefs.GetInt("redline", 1) == 1;
		if (PlayerPrefs.GetInt("isfirstresolution3", 0) != 0)
		{
			return;
		}
		string[] winresolution = setting.GetComponent<Setting>().winresolution;
		Resolution currentResolution = Screen.currentResolution;
		if (currentResolution.height >= 1080 || currentResolution.width >= 1920)
		{
			PlayerPrefs.SetInt("isfirstresolution3", 1);
			Screen.SetResolution(1920, 1080, fullscreen: true);
			return;
		}
		for (int i = 0; i < winresolution.Length; i++)
		{
			string[] array = winresolution[i].Split('×');
			if (int.Parse(array[0].Trim()) == currentResolution.width && int.Parse(array[1].Trim()) == currentResolution.height)
			{
				PlayerPrefs.SetInt("isfirstresolution3", 1);
				Screen.SetResolution(int.Parse(array[0]), int.Parse(array[1]), fullscreen: true);
				break;
			}
			if (int.Parse(array[0].Trim()) < currentResolution.width && int.Parse(array[1].Trim()) < currentResolution.height)
			{
				PlayerPrefs.SetInt("isfirstresolution3", 1);
				Screen.SetResolution(int.Parse(array[0]), int.Parse(array[1]), fullscreen: false);
				break;
			}
		}
	}

	public void ChangeSoundVal(float sou, float val)
	{
		if (sou > val)
		{
			StartCoroutine(LowSound(sou, val));
		}
		else
		{
			StartCoroutine(LargeSound(sou, val));
		}
	}

	private IEnumerator LowSound(float orgVal, float val)
	{
		float vol = orgVal;
		soundManager.GetComponent<AudioSource>().volume = vol;
		while (vol > val)
		{
			vol -= 0.02f;
			yield return new WaitForSeconds(0.02f);
			soundManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	private IEnumerator LargeSound(float orgVal, float val)
	{
		float vol = orgVal;
		soundManager.GetComponent<AudioSource>().volume = vol;
		while (vol < val)
		{
			vol += 0.02f;
			yield return new WaitForSeconds(0.02f);
			soundManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	public void ChangeMusicVal(float sou, float val)
	{
		if (sou > val)
		{
			StartCoroutine(Low(sou, val));
		}
		else
		{
			StartCoroutine(Large(sou, val));
		}
	}

	private IEnumerator Low(float orgVal, float val)
	{
		float vol = orgVal;
		musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol > val)
		{
			vol -= 0.02f;
			yield return new WaitForSeconds(0.02f);
			musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	private IEnumerator Large(float orgVal, float val)
	{
		float vol = orgVal;
		musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol < val)
		{
			vol += 0.02f;
			yield return new WaitForSeconds(0.02f);
			musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	public void ShowFloatBox(string black = "black")
	{
		if (showfloat != null)
		{
			floatBox.GetComponent<CanvasGroup>().DOKill();
			StopCoroutine(showfloat);
		}
		showfloat = ShowFloat(black);
		StartCoroutine(showfloat);
	}

	private IEnumerator ShowFloat(string black)
	{
		if (hidefloat != null)
		{
			floatBox.GetComponent<CanvasGroup>().DOKill();
			StopCoroutine(hidefloat);
		}
		GameObject gameObject = ((!(black == "black")) ? whiteFloatBox : floatBox);
		gameObject.SetActive(value: true);
		gameObject.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		hidefloat = HideFloat(black);
		StartCoroutine(hidefloat);
	}

	private IEnumerator HideFloat(string black = "black")
	{
		GameObject box = ((!(black == "black")) ? whiteFloatBox : floatBox);
		yield return new WaitForSeconds(1f);
		box.GetComponent<CanvasGroup>().DOFade(0f, 3f);
		yield return new WaitForSeconds(3f);
		box.SetActive(value: false);
	}

	private void ChangeScene(string scene)
	{
		StartCoroutine(LoadScene(scene));
	}

	private IEnumerator LoadScene(string scene)
	{
		yield return new WaitForSeconds(3f);
		if (!scene.Equals(""))
		{
			SceneManager.LoadScene(scene);
		}
	}

	public static void SetTextWithEllipsis(Text textComponent, string value)
	{
		TextGenerator textGenerator = new TextGenerator();
		RectTransform component = textComponent.GetComponent<RectTransform>();
		TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(component.rect.size);
		textGenerator.Populate(value, generationSettings);
		int characterCountVisible = textGenerator.characterCountVisible;
		string key = value;
		if (value.Length > characterCountVisible && characterCountVisible - 1 > 0)
		{
			key = value.Substring(0, characterCountVisible - 1);
			key += "…";
		}
		textComponent.GetComponent<I18NText>().updateTranslation2(key);
	}

	public void CloseBtn(GameObject closeBox)
	{
		closeBox.SetActive(value: false);
	}

	public void CloseInfoBtn(GameObject infoBox)
	{
		infoBox.SetActive(value: false);
	}

	public void ShowLabel(GameObject txt)
	{
		StartCoroutine(ShowText(txt));
	}

	public void HideLabel(GameObject txt)
	{
		StartCoroutine(HideText(txt));
	}

	private IEnumerator ShowText(GameObject txt)
	{
		float a = 0f;
		while (a <= 0.999f)
		{
			yield return new WaitForSeconds(0.01f);
			a += 0.01f;
			if (txt != null)
			{
				txt.GetComponent<CanvasGroup>().alpha = a;
			}
		}
	}

	private IEnumerator HideText(GameObject txt)
	{
		float a = 1f;
		while (a >= 0.001f)
		{
			yield return new WaitForSeconds(0.01f);
			a -= 0.01f;
			if (txt != null)
			{
				txt.GetComponent<CanvasGroup>().alpha = a;
			}
		}
	}

	public void Dialog(string label, Transform login)
	{
		string text = "";
		text = ((!(label[0].ToString() == "^")) ? label : I18N.instance.getValue(label));
		Transform obj = Resources.Load("Dialog", typeof(Transform)) as Transform;
		obj.Find("Text").GetComponent<I18NText>().updateTranslation2(text);
		UnityEngine.Object.Instantiate(obj, login);
	}

	public long GetTimeStamp(string isSeconds)
	{
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
		long result = 0L;
		if (isSeconds == "s")
		{
			result = Convert.ToInt64(timeSpan.TotalSeconds);
		}
		else if (isSeconds == "ms")
		{
			result = Convert.ToInt64(timeSpan.TotalMilliseconds);
		}
		return result;
	}

	public DateTime StampToTime(long nowTime, bool isSecounds = true)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		string text = (isSecounds ? dateTime.AddSeconds(nowTime) : dateTime.AddSeconds(nowTime / 1000)).ToString("MM/dd/yyyy HH:mm:ss");
		try
		{
			return Convert.ToDateTime(text);
		}
		catch (Exception)
		{
			Debug.LogError("报错时间：" + text);
			return default(DateTime);
		}
	}

	public int ObjSizeType(GameObject obj)
	{
		float width = obj.GetComponent<RectTransform>().rect.width;
		float height = obj.GetComponent<RectTransform>().rect.height;
		if (!(width >= height))
		{
			return 1;
		}
		return 0;
	}

	public void LightShow(GameObject obj)
	{
		obj.GetComponent<CanvasGroup>().alpha = 0f;
		StartCoroutine(LightRun(obj.GetComponent<CanvasGroup>()));
	}

	public void LightHide(GameObject obj)
	{
		obj.GetComponent<CanvasGroup>().alpha = 1f;
		StartCoroutine(LightHideRun(obj.GetComponent<CanvasGroup>()));
	}

	private IEnumerator LightRun(CanvasGroup obj)
	{
		obj.DOFade(0.8f, 0.1f);
		yield return new WaitForSeconds(0.1f);
		obj.DOFade(0f, 0.1f);
		yield return new WaitForSeconds(0.1f);
		obj.DOFade(1f, 0.1f);
	}

	private IEnumerator LightHideRun(CanvasGroup obj)
	{
		obj.DOFade(0.2f, 0.1f);
		yield return new WaitForSeconds(0.1f);
		obj.DOFade(1f, 0.1f);
		yield return new WaitForSeconds(0.1f);
		obj.DOFade(0f, 0.1f);
	}

	public float CalculateLengthOfText(string message, Text txt)
	{
		float num = 0f;
		Font font = txt.font;
		font.RequestCharactersInTexture(message, txt.fontSize, txt.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt.fontSize);
			num += (float)info.advance;
		}
		return num + 15f;
	}

	public void CrackBoom(string id)
	{
		if (player.playerdata.OpenedMail.Contains(id))
		{
			return;
		}
		if (!player.playerdata.OpenMail.Contains(id))
		{
			player.playerdata.OpenMail.Add(id);
		}
		if (homeScene.zhadanInvoke1 != null)
		{
			return;
		}
		string[] boomList = player.playerdata.boomList;
		player.playerdata.isTriggerBoom = true;
		List<string> openMail = player.playerdata.OpenMail;
		ZhadanInvoke zhadanInvoke = homeScene.zhadanInvoke;
		for (int i = 0; i < boomList.Length; i++)
		{
			if (boomList[i] != "0")
			{
				homeScene.newZhadanDialog.StartGame();
				if (boomList[i] == "3300007")
				{
					zhadanInvoke.Init(600f, id);
					player.playerdata.zhadantime = 0f;
					saveManager.SavePlayerData();
					StartRecordTime();
					return;
				}
				if (boomList[i] == "3300008")
				{
					zhadanInvoke.Init(600f, id);
					return;
				}
				if (boomList[i] == "3300009")
				{
					zhadanInvoke.Init(600f, id);
					return;
				}
				if ((boomList[i] == "3300010" && openMail[openMail.Count - 1] == "1500087") || (boomList[i] == "3300010" && openMail[openMail.Count - 1] == "1500088"))
				{
					float boomLastTime = player.playerdata.boomLastTime;
					boomLastTime = ((boomLastTime < 30f) ? 100f : boomLastTime);
					zhadanInvoke.Init(boomLastTime, id, inter: false);
					return;
				}
				if (boomList[i] == "3300010")
				{
					zhadanInvoke.Init(600f, id);
					return;
				}
				if (boomList[i] == "3300011")
				{
					zhadanInvoke.Init(600f, id);
					return;
				}
			}
		}
		saveManager.SavePlayerData();
	}

	public void ShowFailedBlack()
	{
		failedBlack.SetActive(value: true);
		failedBlack.transform.Find("Image").GetComponent<RectTransform>().DOScale(Vector3.one, 2f)
			.OnComplete(delegate
			{
				UnityEngine.Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadangameover"), homeScene.middle);
				failedBlack.transform.Find("Image").GetComponent<Image>().DOFade(0f, 1f)
					.OnComplete(delegate
					{
						failedBlack.transform.Find("Image").localScale = Vector3.zero;
						failedBlack.transform.Find("Image").GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
						failedBlack.SetActive(value: false);
					});
			});
	}

	public void StartRecordTime()
	{
		StopRecordTime();
		InvokeRepeating("RecordTime", 0f, 1f);
	}

	public void StopRecordTime()
	{
		CancelInvoke("RecordTime");
	}

	private void RecordTime()
	{
		if (!player.playerdata.zhadanhide)
		{
			player.playerdata.zhadantime += 1f;
		}
	}
}
