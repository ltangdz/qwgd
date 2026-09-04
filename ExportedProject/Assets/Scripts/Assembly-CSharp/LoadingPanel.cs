using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
	public Image img_loading;

	public Text txt_loading;

	public Text txt_username;

	public string dotstring = "......";

	public LoginCanvas loginCanvas;

	public GameManager gameManager;

	public GameObject beginningPanel;

	public CanvasGroup canvasGroup;

	public GameObject videocanvas;

	public Animator ani_saveloading;

	public string username;

	private int pos = 5;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void SetReload()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		username = gameManager.player.playerdata.nickname;
		txt_username.GetComponent<I18NText>().updateTranslation2(username);
		InvokeRepeating("StartLoadingTextAni", 0.1f, 0.5f);
		StartCoroutine(ReloadHacker());
	}

	private IEnumerator ReloadHacker()
	{
		yield return new WaitForSeconds(2f);
		canvasGroup.DOFade(0f, 2f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
		Object.Instantiate(Resources.Load("Dialog/Hacker/startpc") as GameObject, gameManager.homeScene.middle).GetComponent<Animator>().Play("ani_startpc3");
	}

	public void SetLoading(string um, bool isnew)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		username = um;
		txt_username.GetComponent<I18NText>().updateTranslation2(username);
		gameManager.player.playerdata.nickname = um;
		InvokeRepeating("StartLoadingTextAni", 0.1f, 0.5f);
		StartCoroutine(StartLogin());
	}

	private IEnumerator StartLogin()
	{
		yield return new WaitForSeconds(3f);
		gameManager.musicManager.Stop();
		if (gameManager.player.playerdata.isCourseOver == 0 || gameManager.player.playerdata.Eventid == 1)
		{
			gameManager.ShowFloatBox();
			yield return new WaitForSeconds(2f);
			if (!gameManager.player.playerdata.lookupnews)
			{
				videocanvas.SetActive(value: false);
				beginningPanel.SetActive(value: true);
			}
			else
			{
				gameManager.musicManager.PlayMusicLoop(3);
				gameManager.txt_studio.SetActive(value: false);
				gameManager.player.playerdata.SetCourse(0);
				SceneManager.LoadScene("homecourse");
			}
			base.gameObject.SetActive(value: false);
			gameManager.player.playerdata.startTime = long.Parse(gameManager.dataManager.dic11[gameManager.player.GetEventId()].date);
		}
		else
		{
			gameManager.ShowFloatBox();
			yield return new WaitForSeconds(2f);
			base.gameObject.SetActive(value: false);
			string eventId = gameManager.player.GetEventId();
			string date = gameManager.dataManager.dic11[eventId].date;
			gameManager.player.playerdata.startTime = long.Parse(date);
			gameManager.txt_studio.SetActive(value: false);
			gameManager.istaohuashow = false;
			gameManager.iscancollect = true;
			SceneManager.LoadScene(gameManager.GetHomeSceneName());
		}
	}

	private void StartLoadingAni()
	{
		img_loading.transform.DOLocalRotate(new Vector3(0f, 359f, 0f), 2f);
	}

	private void StartLoadingTextAni()
	{
		txt_loading.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue("^loading_01"), username) + dotstring.Substring(pos));
		pos--;
		if (pos == 0)
		{
			pos = 5;
		}
	}
}
