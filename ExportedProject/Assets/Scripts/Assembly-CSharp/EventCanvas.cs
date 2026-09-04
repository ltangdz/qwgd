using System.Collections;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class EventCanvas : MonoBehaviour
{
	public Text txt_date;

	public Text txt_title;

	public GameManager gameManager;

	public Image img_mouse;

	public bool iscanclick;

	public HomeScene homeScene;

	private bool startgo = true;

	private void Awake()
	{
	}

	public void GoToLogin()
	{
		if (iscanclick)
		{
			if (gameManager.maincamera != null)
			{
				gameManager.maincamera.GetComponent<CameraFilterPack_TV_Artefact>().enabled = false;
			}
			img_mouse.gameObject.SetActive(value: false);
			gameManager.ShowFloatBox();
			Invoke("ShowHomeScene", 2f);
			gameManager.soundManager.Stop();
			if (!gameManager.player.playerdata.getMask)
			{
				gameManager.player.playerdata.getMask = true;
				gameManager.player.playerdata.startTime = long.Parse(gameManager.dataManager.dic11[gameManager.player.GetEventId()].date);
			}
		}
	}

	private void ShowHomeScene()
	{
		gameManager.CanShowSetting(-1);
		homeScene.gameObject.SetActive(value: true);
		if (gameManager.player.GetEventId() == "110004" && gameManager.player.playerdata.isCanPlayYulun && !gameManager.player.playerdata.isYulunGameOver)
		{
			Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulunEnterBtn"), homeScene.middle);
		}
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator SetContent(string str_date, string str_title)
	{
		gameManager.CanShowSetting(1);
		yield return new WaitForSeconds(2f);
		txt_date.GetComponent<TypewriterEffect>().StartSlowEffect(I18N.instance.getValue(str_date), 0.4f, issound: true);
		yield return new WaitForSeconds((float)I18N.instance.getValue(str_date).Length * 0.4f + 0.2f);
		txt_title.GetComponent<TypewriterEffect>().StartSlowEffect(I18N.instance.getValue(str_title), 0.4f, issound: true);
		yield return new WaitForSeconds((float)I18N.instance.getValue(str_title).Length * 0.4f + 0.2f);
		iscanclick = true;
		img_mouse.gameObject.SetActive(value: true);
		DLCEventManager.Instance.NoticeShowAITalk(isShow: false);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.playerdata.completeHideGame = false;
		gameManager.saveManager.SavePlayerData();
		if (gameManager.player.playerdata.isstarttask)
		{
			startgo = true;
			iscanclick = true;
			Go();
		}
		else
		{
			Init();
		}
	}

	public void Init()
	{
		gameManager.musicManager.Stop();
		if (gameManager.maincamera != null)
		{
			gameManager.maincamera.GetComponent<CameraFilterPack_TV_Artefact>().enabled = true;
		}
		DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		StartCoroutine(SetContent(dATA.event_date, dATA.event_title));
	}

	private void Update()
	{
		if (Input.anyKey && iscanclick)
		{
			Go();
		}
	}

	public void Go()
	{
		if (startgo)
		{
			startgo = false;
			GoToLogin();
		}
	}
}
