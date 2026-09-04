using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskOver : CustomDialog
{
	public GameObject cont;

	public GameObject videoBox;

	public List<Sprite> body;

	public List<Sprite> eye;

	public List<Sprite> mouth;

	public Sprite greenBak;

	public Transform playBtn;

	public GameObject rightBox;

	public Exp exp;

	public GameObject loadingLine;

	public bool showSettle;

	private long maskTime;

	private float maxHotVal;

	private bool play = true;

	private Transform newsImg;

	private DataManager dataManager;

	public Button btn_backtohome;

	public Button btn_replay;

	public Button btn_setting;

	public Button btn_save;

	public Button btn_out;

	public GameObject buttonPanel;

	public Animator ani_videobox;

	public bool Play => play;

	public void ShowButton()
	{
		buttonPanel.SetActive(value: true);
	}

	public void HideButton()
	{
		buttonPanel.SetActive(value: false);
	}

	private void Start()
	{
		ResetMission();
		if (btn_backtohome != null)
		{
			btn_backtohome.onClick.AddListener(delegate
			{
				gameManager.txt_studio.SetActive(value: true);
				SceneManager.LoadScene("mainScene");
			});
		}
		if (btn_replay != null)
		{
			btn_replay.onClick.AddListener(delegate
			{
				gameManager.txt_studio.SetActive(value: true);
				SceneManager.LoadScene("home");
				gameManager.player.ClearEvent();
			});
		}
	}

	public void ResetMission()
	{
		dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
		string videoID = "2500001";
		NewsComment(videoID);
		PersonAni();
	}

	private void NewsComment(string videoID)
	{
	}

	private IEnumerator MoveLabel(Transform newsInfo)
	{
		if (play)
		{
			yield return new WaitForSeconds(1f);
			float num = newsInfo.Find("news_info").GetComponent<RectTransform>().rect.width;
			Debug.Log(num);
			newsInfo.Find("news_info").DOLocalMoveX(0f - (num + 500f), (num + 500f) / 90f).SetEase(Ease.Linear);
			loadingLine.transform.DOScaleX(1f, (num + 500f) / 90f).SetEase(Ease.Linear);
			yield return new WaitForSeconds((num + 500f) / 90f);
			NewsEnd();
		}
	}

	private void NewsEnd()
	{
		playBtn.gameObject.SetActive(value: true);
		playBtn.Find("play_again").GetComponent<Button>().onClick.RemoveAllListeners();
		playBtn.Find("play_again").GetComponent<Button>().onClick.AddListener(delegate
		{
			Replay();
		});
		StopVideo();
		if (showSettle && !rightBox.activeInHierarchy)
		{
			rightBox.SetActive(value: true);
			rightBox.transform.Find("bk").GetComponent<Animator>().Play("ani_newsRight");
			if (gameManager.player.playerdata.isovertask)
			{
				rightBox.GetComponent<NewsResultInfo>().ComepleteDialog();
			}
			else
			{
				rightBox.GetComponent<NewsResultInfo>().NotCompleteDialog();
			}
		}
	}

	private void StopVideo()
	{
		Transform obj = videoBox.transform.Find("img_person");
		play = false;
		StopAllCoroutines();
		videoBox.transform.Find("img_newsInfoBox/news_info").GetComponent<RectTransform>().localPosition = new Vector3(500f, -24.8f, 0f);
		obj.Find("img_mouth").gameObject.SetActive(value: false);
		obj.Find("img_biyan").gameObject.SetActive(value: false);
		loadingLine.GetComponent<RectTransform>().localScale = new Vector3(0f, 1f, 1f);
		newsImg.GetComponent<Animator>().speed = 0f;
		ani_videobox.Play("ani_taskover1");
		Debug.Log("视频结束");
	}

	private void Replay()
	{
		ani_videobox.Play("ani_taskover2");
		playBtn.gameObject.SetActive(value: false);
		play = true;
		ResetMission();
		if (exp != null)
		{
			exp.Replay();
		}
	}

	private void PersonAni()
	{
		StartCoroutine(Wink());
		StartCoroutine(Say());
	}

	private void LoadVideo(string videoId)
	{
	}

	private IEnumerator ChangeWidth(Transform obj, float val)
	{
		float objWidth = obj.GetComponent<RectTransform>().rect.width;
		while (objWidth < val)
		{
			objWidth += 10f;
			obj.GetComponent<RectTransform>().sizeDelta = new Vector2(objWidth, 15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator Wink()
	{
		Transform eye = videoBox.transform.Find("img_person/img_biyan");
		while (play)
		{
			yield return new WaitForSeconds(3f);
			eye.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.1f);
			eye.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator Say()
	{
		Transform mouth = videoBox.transform.Find("img_person/img_mouth");
		while (play)
		{
			yield return new WaitForSeconds(0.2f);
			mouth.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.2f);
			mouth.gameObject.SetActive(value: false);
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
