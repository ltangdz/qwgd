using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LiveBroadTimePanel : MonoBehaviour
{
	[SerializeField]
	private Text txt_time;

	[SerializeField]
	private Text txt_question;

	[SerializeField]
	private Text txt_count;

	[SerializeField]
	public bool isstart;

	private float timer;

	private GameManager gameManager;

	[SerializeField]
	private List<GameObject> img_whites = new List<GameObject>();

	[SerializeField]
	private List<GameObject> img_grays = new List<GameObject>();

	[SerializeField]
	private List<GameObject> img_oks = new List<GameObject>();

	[SerializeField]
	private List<string> str_questions = new List<string>();

	[SerializeField]
	private CanvasGroup img_red;

	[SerializeField]
	private Animator ani_time;

	public float waring = 0.6f;

	private bool isstartwarning = true;

	private Sequence sq;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void StartTime(bool isstarttime)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		RefreshLevel();
		ani_time.enabled = true;
		isstart = isstarttime;
	}

	private void StartWarning()
	{
		if (isstartwarning)
		{
			isstartwarning = false;
			sq = DOTween.Sequence();
			sq.Append(img_red.DOFade(0.2f, waring));
			sq.Append(img_red.DOFade(1f, waring));
			sq.Play().SetLoops(-1);
		}
	}

	private void StartWarning2()
	{
		sq.Kill();
		img_red.alpha = 1f;
		if (isstartwarning)
		{
			isstartwarning = false;
			sq = DOTween.Sequence();
			sq.Append(img_red.DOFade(0.2f, waring));
			sq.Append(img_red.DOFade(1f, waring));
			sq.Play().SetLoops(-1);
		}
	}

	private void Update()
	{
		if (!isstart)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer >= 1f)
		{
			timer = 0f;
			gameManager.player.playerdata.livebroadinglefttime--;
			gameManager.player.playerdata.livebroadtotaltime++;
			txt_time.text = gameManager.player.playerdata.livebroadinglefttime + "<size=56>s</size>";
			gameManager.homeScene.liveBroadcastingDialog.TimeUp();
			if (gameManager.player.playerdata.livebroadinglefttime == 20)
			{
				StartWarning();
			}
			else if (gameManager.player.playerdata.livebroadinglefttime == 10)
			{
				waring = 0.2f;
				isstartwarning = true;
				StartWarning2();
			}
			if (gameManager.player.playerdata.livebroadinglefttime <= 0)
			{
				sq.Kill();
				ani_time.enabled = false;
				img_red.alpha = 1f;
				txt_time.DOText(I18N.instance.getValue("^saolei11"), 0.1f);
				isstart = false;
			}
		}
	}

	public void StopTime()
	{
		isstart = false;
		sq.Kill();
		if (ani_time != null)
		{
			ani_time.enabled = false;
		}
		img_red.alpha = 1f;
		ResetTime();
		RefreshLevel();
	}

	public void RefreshLevel()
	{
		Debug.LogError("RefreshLevel:" + gameManager.player.playerdata.compeletehopelist.Count);
		CompeleteHope();
		int num = gameManager.player.playerdata.hopestep;
		if (num > 3)
		{
			num = 3;
		}
		img_grays[num].SetActive(value: false);
		img_oks[num].SetActive(value: false);
		img_whites[num].SetActive(value: true);
		txt_question.text = "Q" + (num + 1) + ":" + I18N.instance.getValue(str_questions[gameManager.player.playerdata.livebroadingcurrenthopeid]) + "?";
		txt_count.text = I18N.instance.getValue("^livequestion") + "(" + (num + 1) + "/ 4)";
		if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
		{
			gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
		}
	}

	private void ResetTime()
	{
		txt_time.text = "0<size=56>s</size>";
	}

	public void CompeleteHope()
	{
		for (int i = 0; i < gameManager.player.playerdata.compeletehopelist.Count; i++)
		{
			if (i < 4)
			{
				img_oks[i].SetActive(value: true);
				img_grays[i].SetActive(value: false);
				img_whites[i].SetActive(value: false);
			}
		}
		for (int j = gameManager.player.playerdata.compeletehopelist.Count; j < 4; j++)
		{
			if (j < 4)
			{
				img_oks[j].SetActive(value: false);
				img_grays[j].SetActive(value: true);
				img_whites[j].SetActive(value: false);
			}
		}
	}
}
