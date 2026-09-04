using System.Collections;
using DG.Tweening;
using DLC7;
using Honeti;
using UnityEngine;

public class PreAnimationManager : MonoBehaviour
{
	public GameObject mainCamera;

	public GameObject event4alert;

	[SerializeField]
	private GameObject EventCanvas;

	[SerializeField]
	private Transform middle;

	private GameManager gameManager;

	private bool gameStart;

	public Camera dlc7Camera;

	private void Awake()
	{
		Cursor.visible = true;
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Init();
	}

	private void Init()
	{
		gameManager.player.OpenLevel();
		if (gameManager.player.GetEventId().Equals("110003") && !gameManager.player.playerdata.getMask)
		{
			StartCoroutine(StartAnimation("event3video", 17f));
		}
		else if (gameManager.player.GetEventId().Equals("110002") && !gameManager.player.playerdata.getMask)
		{
			StartCoroutine(StartAnimation("event2video", 28f));
		}
		else if (gameManager.player.GetEventId().Equals("110001") && !gameManager.player.playerdata.getMask)
		{
			StartCoroutine(StartAnimation("event1video", 22f));
		}
		else if (gameManager.player.GetEventId().Equals("110004") && !gameManager.player.playerdata.getMask && (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC))
		{
			event4alert.SetActive(value: true);
			event4alert.GetComponent<Animator>().Play("Exit Panel In");
		}
		else if (gameManager.player.GetEventId().Equals("110004") && !gameManager.player.playerdata.getMask && I18N.instance.gameLang == LanguageCode.EN)
		{
			event4alert.SetActive(value: true);
			event4alert.GetComponent<Animator>().Play("Exit Panel In");
		}
		else if (gameManager.player.GetEventId().Equals("110005") && !gameManager.player.playerdata.getMask)
		{
			StartCoroutine(StartAnimation("event5video", 20f, isbig: false));
		}
		else if (gameManager.player.GetEventId().Equals("110006") && !gameManager.player.playerdata.getMask)
		{
			StartCoroutine(StartAnimation("event6video", 53f, isbig: false));
		}
		else if (gameManager.player.GetEventId().Equals("110008") && !gameManager.player.playerdata.getMask)
		{
			if (dlc7Camera != null)
			{
				dlc7Camera.gameObject.SetActive(value: true);
			}
			StartCoroutine(StartAnimation("event7video", 35f, isbig: false));
		}
		else
		{
			gameManager.musicManager.Stop();
			EventCanvas.SetActive(value: true);
		}
	}

	public void NoRed()
	{
		if (!gameStart)
		{
			gameManager.soundManager.PlaySound(16);
			gameStart = true;
			event4alert.GetComponent<Animator>().Play("Exit Panel Out");
			gameManager.isshowredline = false;
			PlayerPrefs.SetInt("redline", 0);
			gameManager.player.playerdata.isstartselectnored = true;
			gameManager.saveManager.SavePlayerData();
			Invoke("Show4Ani", 1.2f);
		}
	}

	public void HaveRed()
	{
		if (!gameStart)
		{
			gameManager.soundManager.PlaySound(16);
			gameStart = true;
			event4alert.GetComponent<Animator>().Play("Exit Panel Out");
			gameManager.isshowredline = true;
			PlayerPrefs.SetInt("redline", 1);
			gameManager.player.playerdata.isstartselectnored = false;
			gameManager.saveManager.SavePlayerData();
			Invoke("Show4Ani", 1.2f);
		}
	}

	public void Show4Ani()
	{
		event4alert.SetActive(value: false);
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			StartCoroutine(StartAnimation("event4video", 60f));
		}
		else
		{
			StartCoroutine(StartAnimation("event4video", 68f));
		}
	}

	private IEnumerator StartAnimation(string name, float time, bool isbig = true)
	{
		GameObject event3video = Object.Instantiate(Resources.Load("Animation/" + name) as GameObject, base.transform);
		if (name == "event2video")
		{
			event3video.transform.Find("event2video").GetComponent<Event2Video>().mainCamera = mainCamera;
			event3video.transform.Find("event2video").GetComponent<Event2Video>().Init();
		}
		else if (name == "event7video")
		{
			OpeningStoryBoard component = event3video.GetComponent<OpeningStoryBoard>();
			component.SetCamera(dlc7Camera);
			component.SetFinishCallback(delegate
			{
				if (dlc7Camera != null)
				{
					gameManager.soundManager.Stop();
					gameManager.CanShowSetting(-1);
					EventCanvas.SetActive(value: true);
					dlc7Camera.gameObject.SetActive(value: false);
				}
			});
			yield break;
		}
		yield return new WaitForSeconds(time);
		if (isbig)
		{
			event3video.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f);
		}
		event3video.transform.Find("black").GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		gameManager.soundManager.Stop();
		Object.Destroy(event3video);
		gameManager.CanShowSetting(-1);
		EventCanvas.SetActive(value: true);
	}
}
