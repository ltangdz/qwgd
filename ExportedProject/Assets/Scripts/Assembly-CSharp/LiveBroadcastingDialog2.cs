using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class LiveBroadcastingDialog2 : MonoBehaviour
{
	[Serializable]
	private class FansDuihuaItem
	{
		public List<string> startduihua12 = new List<string>();

		public List<string> startduihua3 = new List<string>();

		public string startduihua4;

		public List<string> cuicuduihua = new List<string>();

		public string timeupduihua;

		public string rightduihua;

		public string wrongduihua;

		public int totaltime;

		public int fans5time;

		public int fans6time;
	}

	public List<string> avatarlist = new List<string>();

	private GameManager gameManager;

	[SerializeField]
	private Text txt_zimu;

	[SerializeField]
	private GameObject overpanel;

	[SerializeField]
	private GameObject leftpanel;

	[SerializeField]
	private GameObject vanpanel;

	[SerializeField]
	private GameObject fanspanel;

	[SerializeField]
	private GameObject callpanel;

	[SerializeField]
	private GameObject danmupanel;

	[SerializeField]
	private RectTransform content;

	[SerializeField]
	private RectTransform img_top;

	[SerializeField]
	private RectTransform img_bottom;

	[SerializeField]
	private RectTransform fanscontent;

	public bool ismax = true;

	[SerializeField]
	private Animator img_man;

	[SerializeField]
	private List<string> vanduihua = new List<string>();

	[SerializeField]
	private ScrollRect fansScrollRect;

	[SerializeField]
	private List<FansDuihuaItem> fansiduihua1 = new List<FansDuihuaItem>();

	private Image img_notclick;

	[SerializeField]
	private List<string> orzreadquestionzimus = new List<string>();

	[SerializeField]
	private List<string> hopegiveitemids = new List<string>();

	[SerializeField]
	private List<string> hopeansweritemids = new List<string>();

	[SerializeField]
	private List<string> bosstartzimu = new List<string>();

	[SerializeField]
	private List<string> bossresultzimus = new List<string>();

	[SerializeField]
	private List<string> hopefailedvanzimus = new List<string>();

	[SerializeField]
	public LiveBroadTimePanel liveBroadTimePanel;

	[SerializeField]
	private List<string> danmu01 = new List<string>();

	[SerializeField]
	private List<string> danmu02 = new List<string>();

	[SerializeField]
	private List<string> danmu03 = new List<string>();

	[SerializeField]
	private List<string> danmu04 = new List<string>();

	[SerializeField]
	private List<string> danmu05 = new List<string>();

	[SerializeField]
	private List<string> customsucessdanmu = new List<string>();

	[SerializeField]
	private List<string> customfailedsdanmu = new List<string>();

	[SerializeField]
	private List<string> danmu06 = new List<string>();

	[SerializeField]
	private List<string> danmu07 = new List<string>();

	[SerializeField]
	private List<string> bosssucessdanmu = new List<string>();

	[SerializeField]
	private List<string> bossfailedsdanmu = new List<string>();

	[SerializeField]
	private List<string> danmubosswait = new List<string>();

	[SerializeField]
	private List<string> danmubossfailed = new List<string>();

	private bool iswaitdanmu;

	[Header("当前弹幕List")]
	public List<string> currentdanmulist = new List<string>();

	private int p;

	private int[] pgroup = new int[18]
	{
		0, 7, 16, 12, 17, 15, 6, 1, 8, 11,
		10, 9, 3, 13, 14, 5, 2, 4
	};

	private bool iscanpeek;

	public int rangep;

	public void ShowWaitDanmus()
	{
		iswaitdanmu = true;
		StartCoroutine(ShowWaitDanmusAni());
	}

	private IEnumerator ShowWaitDanmusAni()
	{
		while (iswaitdanmu)
		{
			AddDanmu();
			yield return new WaitForSeconds(0.5f);
		}
	}

	private void AddDanmu()
	{
		int num = UnityEngine.Random.Range(0, currentdanmulist.Count);
		if (num >= currentdanmulist.Count - 1)
		{
			return;
		}
		if (!currentdanmulist[num].Contains(";"))
		{
			((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), danmupanel.transform)).GetComponent<LiveBroadcastZimu>().Init(currentdanmulist[num], pgroup[p]);
			SetP();
			return;
		}
		string[] array = currentdanmulist[num].Split(';');
		if (array[0].Equals("0"))
		{
			((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), danmupanel.transform)).GetComponent<LiveBroadcastZimu>().Init(array[1], pgroup[p]);
			SetP();
		}
		else
		{
			StartCoroutine(ManyDanmus(array[1]));
		}
	}

	private void SetP()
	{
		p++;
		if (p >= 18)
		{
			p = 0;
		}
	}

	private IEnumerator ManyDanmus(string content)
	{
		for (int j = 0; j < 12; j++)
		{
			((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), danmupanel.transform)).GetComponent<LiveBroadcastZimu>().Init(content, pgroup[p], ismanping: true);
			SetP();
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(1f);
		for (int j = 0; j < 6; j++)
		{
			((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), danmupanel.transform)).GetComponent<LiveBroadcastZimu>().Init(content, pgroup[p], ismanping: true);
			SetP();
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void TimeUp()
	{
		int num = gameManager.player.playerdata.livebroadingstep;
		if (num > 3)
		{
			num = 3;
		}
		FansDuihuaItem fansDuihuaItem = fansiduihua1[num];
		if (gameManager.player.playerdata.livebroadinglefttime == fansDuihuaItem.fans5time)
		{
			Debug.Log("催促第五句：" + fansDuihuaItem.cuicuduihua[0]);
			AddFansItem(fansDuihuaItem.cuicuduihua[0]);
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.playerdata.livebroadinglefttime == fansDuihuaItem.fans6time)
		{
			Debug.Log("催促第六句：" + fansDuihuaItem.cuicuduihua[1]);
			AddFansItem(fansDuihuaItem.cuicuduihua[1]);
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.playerdata.livebroadinglefttime <= 0)
		{
			gameManager.saveManager.SavePlayerData();
			Debug.Log("倒计时结束");
			if (!ismax)
			{
				Min();
			}
			if (gameManager.homeScene.liveBroadingChatBox != null)
			{
				gameManager.homeScene.liveBroadingChatBox.Hide();
			}
			Wrong();
		}
	}

	private void ShowHiTalk()
	{
		if (!gameManager.homeScene.isloginzhibochat)
		{
			GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatLogin") as GameObject, gameManager.homeScene.middle);
			obj.GetComponent<LiveBroadingChatLogin>().Show();
			obj.name = "livebroadingchatLogin";
			gameManager.homeScene.isloginzhibochat = true;
		}
		else
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatDialog") as GameObject, gameManager.homeScene.middle).GetComponent<LiveBroadingChatBox>().Show();
		}
	}

	private void Start()
	{
		img_notclick = GetComponent<Image>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.liveBroadcastingDialog = this;
		if (gameManager.player.playerdata.livebroadingstep == 0 || gameManager.player.playerdata.hopestep == -1)
		{
			gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
			gameManager.player.playerdata.ResetLiveBroading(0);
			gameManager.saveManager.SavePlayerData();
		}
		else if (gameManager.player.playerdata.livebroadinglefttime == 0)
		{
			if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
			{
				gameManager.player.playerdata.compeletehopelist.Remove("10;0");
				gameManager.player.playerdata.compeletehopelist.Remove("10;1");
				gameManager.player.playerdata.livebroadinganswerrecords.Remove(I18N.instance.getValue("^livename40"));
				gameManager.homeScene.zhibojiannotebook.DeleteBossHopePanel();
				gameManager.player.playerdata.livebroadinglefttime = 600;
				gameManager.player.playerdata.livebroadingchatstep = 3;
				gameManager.player.playerdata.livebroadingstep = 3;
				gameManager.player.playerdata.livebroadingfailedcount = 0;
				gameManager.saveManager.SavePlayerData();
			}
			else
			{
				gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
				gameManager.player.playerdata.ResetLiveBroading(0);
				gameManager.saveManager.SavePlayerData();
			}
		}
		if (gameManager.player.playerdata.iszhiboover)
		{
			gameManager.homeScene.liveBroadingchatEnterBtn.gameObject.SetActive(value: false);
			gameManager.homeScene.SendMail("1500107");
			Invoke("Over", 2f);
		}
		else
		{
			gameManager.musicManager.PlayMusicLoop(15);
			gameManager.homeScene.HideAll();
			StartCoroutine(StartAni());
			ShowWaitDanmus();
			InvokeRepeating("PeekComputer", 1f, 15f);
		}
	}

	public void Over()
	{
		overpanel.SetActive(value: true);
		leftpanel.SetActive(value: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void AddBlack(bool isshowzimu)
	{
		txt_zimu.text = "";
		img_notclick.raycastTarget = true;
		img_notclick.color = new Color(0f, 0f, 0f, 0.658f);
		if (img_notclick.gameObject.GetComponent<Canvas>() == null)
		{
			img_notclick.gameObject.AddComponent<Canvas>().overrideSorting = true;
		}
		img_notclick.GetComponent<Canvas>().sortingOrder = 3;
		if (img_notclick.gameObject.GetComponent<GraphicRaycaster>() == null)
		{
			img_notclick.gameObject.AddComponent<GraphicRaycaster>();
		}
		if (isshowzimu)
		{
			gameManager.CanShowSetting(1);
			img_top.DOLocalMoveY(475f, 0.5f);
			img_bottom.DOLocalMoveY(-475f, 0.5f);
		}
	}

	public void OrzReadQuestion(string answer, int soundid)
	{
		liveBroadTimePanel.StopTime();
		StartCoroutine(OrzReadQuestionAni(answer, soundid));
	}

	private void PeekComputer()
	{
		if (iscanpeek)
		{
			StartCoroutine(PeekComputerAni());
		}
	}

	private IEnumerator PeekComputerAni()
	{
		PlayOrzAnimation(4);
		yield return new WaitForSeconds(1f);
		PlayOrzAnimation(5);
	}

	private void PlayOrzAnimation(int type)
	{
		switch (type)
		{
		case 0:
			iscanpeek = false;
			img_man.Play("ani_normalspeaking");
			break;
		case 1:
			iscanpeek = false;
			img_man.Play("ani_happyspeaking");
			break;
		case 2:
			iscanpeek = false;
			img_man.Play("ani_zhenfengxiangdui");
			break;
		case 3:
			iscanpeek = false;
			img_man.Play("ani_huangluanliuhan");
			break;
		case 4:
			img_man.Play("ani_toumiaodiannao");
			break;
		case 5:
			iscanpeek = true;
			img_man.Play("ani_normalnospeaking");
			break;
		}
	}

	private IEnumerator OrzReadQuestionAni(string answer, int soundid)
	{
		Min();
		AddBlack(isshowzimu: true);
		yield return new WaitForSeconds(1f);
		PlayOrzAnimation(0);
		if (soundid >= 0)
		{
			gameManager.soundManager.PlayLiveOrzQuestion(soundid);
		}
		string endValue = string.Format(I18N.instance.getValue(orzreadquestionzimus[gameManager.player.playerdata.livebroadingcurrenthopeid]), answer);
		txt_zimu.DOText(endValue, 2f).SetEase(Ease.Linear).OnComplete(delegate
		{
			PlayOrzAnimation(5);
		});
		yield return new WaitForSeconds(1f);
		if (gameManager.player.playerdata.hopestep <= 2)
		{
			currentdanmulist = customsucessdanmu;
		}
		else
		{
			currentdanmulist = bosssucessdanmu;
		}
		yield return new WaitForSeconds(1f);
		string text = hopeansweritemids[gameManager.player.playerdata.livebroadingcurrenthopeid];
		DATA1 dATA = gameManager.dataManager.dic1[text.ToString()];
		string value = I18N.instance.getValue(dATA.message);
		if (answer.Trim().ToLower().Equals(value.ToLower()))
		{
			Bingo();
		}
		else
		{
			Wrong();
		}
	}

	private void Bingo()
	{
		if (gameManager.issteam)
		{
			gameManager.steamAchi.SetGlobalStat("stat_hope" + gameManager.player.playerdata.livebroadingcurrenthopeid, 1, "allknow");
			gameManager.steamAchi.CheckHopeAchi();
		}
		if (gameManager.player.playerdata.livebroadtotaltime <= 600 && gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
		{
			gameManager.UnlockAchievements("livebroading");
		}
		PlayOrzAnimation(1);
		gameManager.player.playerdata.compeletehopelist.Add(gameManager.player.playerdata.livebroadingcurrenthopeid + ";1");
		gameManager.saveManager.SavePlayerData();
		liveBroadTimePanel.RefreshLevel();
		liveBroadTimePanel.CompeleteHope();
		FansDuihuaItem fansDuihuaItem = fansiduihua1[gameManager.player.playerdata.hopestep];
		AddFansItem(fansDuihuaItem.rightduihua);
		DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
		{
			fansScrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
		StartCoroutine(MoveAllFansItem());
		if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
		{
			liveBroadTimePanel.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator MoveAllFansItem()
	{
		yield return new WaitForSeconds(2f);
		fanspanel.SetActive(value: true);
		fanspanel.transform.DOLocalMoveX(200f, 0.1f);
		yield return new WaitForSeconds(4f);
		for (int i = fanscontent.transform.childCount - 1; i >= 0; i--)
		{
			if (fanscontent.GetChild(i).GetComponent<FansItem>() != null)
			{
				fanscontent.GetChild(i).GetComponent<FansItem>().MoveLeft();
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield return new WaitForSeconds(2f);
		currentdanmulist = danmu05;
		PlayOrzAnimation(5);
		if (gameManager.player.playerdata.livebroadingfailedcount >= 1)
		{
			fanspanel.transform.DOLocalMoveX(-600f, 0.2f);
			fanspanel.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			yield return new WaitForSeconds(0.5f);
			vanpanel.SetActive(value: true);
			vanpanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			vanpanel.GetComponent<RectTransform>().DOLocalMoveX(200f, 0.2f);
			yield return new WaitForSeconds(0.2f);
			AddBlack(isshowzimu: true);
			yield return new WaitForSeconds(0.5f);
			for (int i = 0; i < hopefailedvanzimus.Count; i++)
			{
				if (i == 0)
				{
					currentdanmulist = danmubossfailed;
				}
				float num = gameManager.soundManager.PlayLiveVan(23 + i);
				gameManager.soundManager.PlayLiveVan(23 + i);
				txt_zimu.text = "";
				if (I18N.instance.getValue(hopefailedvanzimus[i]).StartsWith("ORZ：") || I18N.instance.getValue(hopefailedvanzimus[i]).StartsWith("ORZ:"))
				{
					PlayOrzAnimation(0);
				}
				txt_zimu.DOText(I18N.instance.getValue(hopefailedvanzimus[i]), num).SetEase(Ease.Linear).OnComplete(delegate
				{
					PlayOrzAnimation(5);
				});
				yield return new WaitForSeconds(num + 1f);
			}
			PlayOrzAnimation(5);
			gameManager.CanShowSetting(-1);
			img_top.DOLocalMoveY(615f, 0.5f);
			img_bottom.DOLocalMoveY(-615f, 0.5f);
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(4, gameManager);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
		{
			fanspanel.transform.DOLocalMoveX(-600f, 0.2f);
			fanspanel.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			yield return new WaitForSeconds(0.5f);
			vanpanel.SetActive(value: true);
			vanpanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			vanpanel.GetComponent<RectTransform>().DOLocalMoveX(200f, 0.2f);
			yield return new WaitForSeconds(0.2f);
			AddBlack(isshowzimu: true);
			yield return new WaitForSeconds(0.5f);
			for (int i = 0; i < bossresultzimus.Count; i++)
			{
				if (i == 0)
				{
					currentdanmulist = danmu06;
				}
				float num2 = gameManager.soundManager.PlayLiveVan(16 + i);
				gameManager.soundManager.PlayLiveVan(16 + i);
				txt_zimu.text = "";
				if (I18N.instance.getValue(bossresultzimus[i]).StartsWith("ORZ：") || I18N.instance.getValue(bossresultzimus[i]).StartsWith("ORZ:"))
				{
					PlayOrzAnimation(0);
				}
				txt_zimu.DOText(I18N.instance.getValue(bossresultzimus[i]), num2).OnComplete(delegate
				{
					PlayOrzAnimation(5);
				});
				yield return new WaitForSeconds(num2 + 1f);
				if (i == 3)
				{
					gameManager.soundManager.PlaySound(41);
					vanpanel.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.2f);
					vanpanel.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
					yield return new WaitForSeconds(3f);
				}
			}
			PlayOrzAnimation(5);
			gameManager.CanShowSetting(-1);
			img_top.DOLocalMoveY(615f, 0.5f);
			img_bottom.DOLocalMoveY(-615f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(SmallOverAni());
			yield return new WaitForSeconds(0.5f);
			overpanel.SetActive(value: true);
			leftpanel.SetActive(value: false);
			gameManager.player.playerdata.livebroadingstep = 3;
			gameManager.saveManager.SavePlayerData();
			yield return new WaitForSeconds(0.5f);
			gameManager.homeScene.liveBroadingEnterBtn.gameObject.SetActive(value: false);
			Min();
			yield return new WaitForSeconds(0.2f);
			ShowHiTalk();
		}
		else
		{
			yield return new WaitForSeconds(2f);
			Next(isnext: true);
		}
	}

	private void Wrong()
	{
		PlayOrzAnimation(3);
		if (!gameManager.player.playerdata.compeletehopelist.Contains(gameManager.player.playerdata.livebroadingcurrenthopeid + ";0"))
		{
			gameManager.player.playerdata.compeletehopelist.Add(gameManager.player.playerdata.livebroadingcurrenthopeid + ";0");
		}
		liveBroadTimePanel.RefreshLevel();
		Debug.LogError("答错");
		FansDuihuaItem fansDuihuaItem = fansiduihua1[gameManager.player.playerdata.hopestep];
		AddFansItem(fansDuihuaItem.wrongduihua);
		DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
		{
			fansScrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
		gameManager.player.playerdata.livebroadingfailedcount++;
		if (gameManager.player.playerdata.livebroadingcurrenthopeid == 10)
		{
			liveBroadTimePanel.gameObject.SetActive(value: false);
			gameManager.player.playerdata.livebroadingfailedcount = 2;
		}
		gameManager.saveManager.SavePlayerData();
		StartCoroutine(MoveAllFansItem());
		if (gameManager.player.playerdata.hopestep <= 2)
		{
			currentdanmulist = customfailedsdanmu;
		}
		else
		{
			currentdanmulist = bossfailedsdanmu;
		}
	}

	public void Hide()
	{
		StartCoroutine(SmallOverAni());
	}

	private IEnumerator SmallOverAni()
	{
		yield return new WaitForSeconds(1f);
		img_notclick.raycastTarget = false;
		img_notclick.color = new Color(1f, 1f, 1f, 0f);
		UnityEngine.Object.Destroy(img_notclick.GetComponent<GraphicRaycaster>());
		UnityEngine.Object.Destroy(img_notclick.GetComponent<Canvas>());
	}

	private IEnumerator StartAni()
	{
		if (gameManager.player.playerdata.livebroadingstep > 0)
		{
			overpanel.SetActive(value: false);
			leftpanel.SetActive(value: true);
			fanspanel.SetActive(value: true);
			fanspanel.transform.DOLocalMoveX(200f, 0.3f);
			fanspanel.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			currentdanmulist = danmu05;
			Next(isnext: false);
		}
		else if (gameManager.player.playerdata.livebroadingstep == 0)
		{
			gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
			gameManager.player.playerdata.ResetLiveBroading(0);
			gameManager.saveManager.SavePlayerData();
		}
		yield return new WaitForSeconds(2f);
		Min();
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatLogin") as GameObject, gameManager.homeScene.middle);
		obj.GetComponent<LiveBroadingChatLogin>().Show();
		obj.name = "livebroadingchatLogin";
		gameManager.homeScene.isloginzhibochat = true;
		if (gameManager.player.playerdata.livebroadingchatstep >= 3)
		{
			StartTime(isinit: false, isstarttime: false);
			Hide();
		}
		gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
		if (gameManager.player.playerdata.hopestep == 4)
		{
			StartCoroutine(InitBossFansItem());
		}
		gameManager.homeScene.liveBroadingchatEnterBtn.gameObject.SetActive(value: true);
	}

	private IEnumerator InitBossFansItem()
	{
		FansDuihuaItem fanitem = fansiduihua1[5];
		for (int i = 0; i < fanitem.startduihua12.Count; i++)
		{
			AddFansItem(fanitem.startduihua12[i]);
			yield return new WaitForSeconds(0f);
			DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
			{
				fansScrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds(0f);
		}
		for (int num = 0; num < fanitem.startduihua3.Count; num++)
		{
			string[] array = fanitem.startduihua3[num].Split(';');
			if (int.Parse(array[0]) == gameManager.player.playerdata.livebroadingcurrenthopeid)
			{
				AddFansItem(array[1]);
			}
		}
		yield return new WaitForSeconds(0f);
		AddFansItem(fanitem.startduihua4);
		yield return new WaitForSeconds(0f);
		DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
		{
			fansScrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
	}

	public void BossStart()
	{
		StartCoroutine(BossStartAni());
	}

	private IEnumerator BossStartAni()
	{
		yield return new WaitForSeconds(6f);
		base.transform.SetAsLastSibling();
		Min();
		fanspanel.SetActive(value: true);
		fanspanel.transform.localPosition = new Vector2(-600f, -25f);
		fanspanel.transform.DOLocalMoveX(200f, 0.3f);
		fanspanel.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(1f);
		Next(isnext: true);
	}

	public void Min()
	{
		content.DOKill();
		if (ismax)
		{
			content.DOLocalMove(new Vector2(-675f, 261f), 0.2f);
			content.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
		}
		else
		{
			content.DOLocalMove(Vector3.zero, 0.2f);
			content.DOScale(Vector3.one, 0.2f);
		}
		ismax = !ismax;
	}

	public void StartVan()
	{
		StartCoroutine(StartVanAni());
	}

	private IEnumerator StartVanAni()
	{
		Min();
		yield return new WaitForSeconds(1f);
		overpanel.SetActive(value: false);
		leftpanel.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		gameManager.CanShowSetting(1);
		img_top.DOLocalMoveY(475f, 0.5f);
		img_bottom.DOLocalMoveY(-475f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < vanduihua.Count; i++)
		{
			switch (i)
			{
			case 0:
				currentdanmulist = danmu01;
				break;
			case 2:
				currentdanmulist = danmu02;
				break;
			case 4:
				currentdanmulist = danmu03;
				break;
			case 6:
				currentdanmulist = danmu04;
				break;
			}
			txt_zimu.text = "";
			float num = gameManager.soundManager.PlayLiveVan(i);
			gameManager.soundManager.PlayLiveVan(i);
			if (I18N.instance.getValue(vanduihua[i]).StartsWith("ORZ：") || I18N.instance.getValue(vanduihua[i]).StartsWith("ORZ:"))
			{
				PlayOrzAnimation(0);
			}
			txt_zimu.DOText(I18N.instance.getValue(vanduihua[i]), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayOrzAnimation(5);
			});
			yield return new WaitForSeconds(num + 1f);
			switch (i)
			{
			case 1:
				callpanel.SetActive(value: true);
				img_man.GetComponent<RectTransform>().DOLocalMoveX(610f, 0.2f);
				gameManager.soundManager.PlaySound(39);
				yield return new WaitForSeconds(5.5f);
				callpanel.SetActive(value: false);
				vanpanel.SetActive(value: true);
				vanpanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
				yield return new WaitForSeconds(1f);
				break;
			case 7:
				yield return new WaitForSeconds(1f);
				currentdanmulist = danmu05;
				break;
			}
		}
		PlayOrzAnimation(5);
		vanpanel.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.2f);
		vanpanel.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
		gameManager.CanShowSetting(-1);
		img_top.DOLocalMoveY(615f, 0.5f);
		img_bottom.DOLocalMoveY(-615f, 0.5f);
		yield return new WaitForSeconds(1f);
		fanspanel.SetActive(value: true);
		fanspanel.transform.DOLocalMoveX(200f, 0.3f);
		fanspanel.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(2f);
		gameManager.player.playerdata.livebroadingstep = 1;
		gameManager.saveManager.SavePlayerData();
		Next(isnext: true);
	}

	public void Next(bool isnext)
	{
		StartCoroutine(NextAni(isnext));
	}

	private IEnumerator NextAni(bool isnext)
	{
		gameManager.CanShowSetting(-1);
		img_top.DOLocalMoveY(615f, 0.5f);
		img_bottom.DOLocalMoveY(-615f, 0.5f);
		if (isnext)
		{
			SelectHope();
		}
		if ((gameManager.player.playerdata.hopestep < 3 && gameManager.player.playerdata.hopestep >= 0) || (gameManager.player.playerdata.hopestep == 3 && gameManager.player.playerdata.livebroadingstep == 4))
		{
			FansDuihuaItem fanitem = fansiduihua1[gameManager.player.playerdata.hopestep];
			for (int i = 0; i < fanitem.startduihua12.Count; i++)
			{
				AddFansItem(fanitem.startduihua12[i]);
				yield return new WaitForSeconds(isnext ? 1f : 0f);
				DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
				{
					fansScrollRect.normalizedPosition = x;
				}, Vector2.zero, 1f);
				yield return new WaitForSeconds(isnext ? 1f : 0f);
			}
			for (int num = 0; num < fanitem.startduihua3.Count; num++)
			{
				string[] array = fanitem.startduihua3[num].Split(';');
				Debug.Log("&&&" + array[1]);
				if (int.Parse(array[0]) == gameManager.player.playerdata.livebroadingcurrenthopeid)
				{
					Debug.Log("###" + array[1]);
					AddFansItem(array[1]);
				}
			}
			yield return new WaitForSeconds(isnext ? 1f : 0f);
			AddFansItem(fanitem.startduihua4);
			yield return new WaitForSeconds(isnext ? 1f : 0f);
			if (!isnext)
			{
				if (gameManager.player.playerdata.livebroadinglefttime <= fanitem.fans5time)
				{
					Debug.Log("催促第五句：" + fanitem.cuicuduihua[0]);
					AddFansItem(fanitem.cuicuduihua[0]);
					yield return new WaitForSeconds(isnext ? 1f : 0f);
				}
				if (gameManager.player.playerdata.livebroadinglefttime <= fanitem.fans6time)
				{
					Debug.Log("催促第六句：" + fanitem.cuicuduihua[1]);
					AddFansItem(fanitem.cuicuduihua[1]);
					yield return new WaitForSeconds(isnext ? 1f : 0f);
				}
			}
			DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
			{
				fansScrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			if (!gameManager.player.playerdata.temporaryhopelist.Contains("10597"))
			{
				AddBlack(isshowzimu: false);
			}
			if (isnext)
			{
				yield return new WaitForSeconds(3f);
				Min();
				yield return new WaitForSeconds(1f);
				ShowHiTalk();
			}
		}
		else
		{
			if (!(gameManager.player.playerdata.hopestep == 3 && isnext))
			{
				yield break;
			}
			if (gameManager.player.playerdata.livebroadingstep == 2)
			{
				StartCoroutine(StartBossAni());
			}
			if (gameManager.player.playerdata.livebroadingstep != 3)
			{
				yield break;
			}
			FansDuihuaItem fanitem = fansiduihua1[3];
			for (int i = 0; i < fanitem.startduihua12.Count; i++)
			{
				AddFansItem(fanitem.startduihua12[i]);
				yield return new WaitForSeconds(1f);
				DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
				{
					fansScrollRect.normalizedPosition = x;
				}, Vector2.zero, 1f);
				yield return new WaitForSeconds(1f);
			}
			for (int num2 = 0; num2 < fanitem.startduihua3.Count; num2++)
			{
				string[] array2 = fanitem.startduihua3[num2].Split(';');
				if (int.Parse(array2[0]) == gameManager.player.playerdata.livebroadingcurrenthopeid)
				{
					AddFansItem(array2[1]);
				}
			}
			yield return new WaitForSeconds(1f);
			AddFansItem(fanitem.startduihua4);
			yield return new WaitForSeconds(1f);
			DOTween.To(() => fansScrollRect.normalizedPosition, delegate(Vector2 x)
			{
				fansScrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds(1f);
			Min();
			yield return new WaitForSeconds(1f);
			gameManager.player.playerdata.livebroadingstep = 4;
			gameManager.saveManager.SavePlayerData();
			ShowHiTalk();
		}
	}

	public void StartGame(string itemid)
	{
		if (hopegiveitemids.Contains(itemid) && gameManager.player.playerdata.livebroadingcurrenthopeid != 10 && gameManager.player.playerdata.livebroadingcurrenthopeid == hopegiveitemids.IndexOf(itemid))
		{
			StartTime(isinit: true, isstarttime: true);
		}
	}

	public void StartTime(bool isinit, bool isstarttime)
	{
		int num = gameManager.player.playerdata.hopestep;
		if (num > 3)
		{
			num = 3;
		}
		FansDuihuaItem fansDuihuaItem = fansiduihua1[num];
		if (isinit)
		{
			gameManager.player.playerdata.livebroadinglefttime = fansDuihuaItem.totaltime;
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.playerdata.hopestep < 4)
		{
			currentdanmulist = danmu04;
		}
		else
		{
			currentdanmulist = danmubosswait;
		}
		liveBroadTimePanel.gameObject.SetActive(value: true);
		if (isstarttime)
		{
			liveBroadTimePanel.StartTime(isstarttime: true);
		}
		else
		{
			liveBroadTimePanel.StartTime(gameManager.player.playerdata.temporaryhopelist.Contains(hopegiveitemids[gameManager.player.playerdata.livebroadingcurrenthopeid]));
		}
	}

	private IEnumerator StartBossAni()
	{
		AddBlack(isshowzimu: true);
		yield return new WaitForSeconds(0.5f);
		fanspanel.GetComponent<RectTransform>().DOLocalMoveY(-900f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		vanpanel.SetActive(value: true);
		vanpanel.GetComponent<RectTransform>().DOLocalMoveX(200f, 0.2f);
		vanpanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < bosstartzimu.Count; i++)
		{
			if (i == 0)
			{
				currentdanmulist = danmu07;
			}
			float num = gameManager.soundManager.PlayLiveVan(8 + i);
			gameManager.soundManager.PlayLiveVan(8 + i);
			txt_zimu.text = "";
			if (I18N.instance.getValue(bosstartzimu[i]).StartsWith("ORZ：") || I18N.instance.getValue(bosstartzimu[i]).StartsWith("ORZ:"))
			{
				PlayOrzAnimation(0);
			}
			txt_zimu.DOText(I18N.instance.getValue(bosstartzimu[i]), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayOrzAnimation(5);
			});
			yield return new WaitForSeconds(num + 1f);
		}
		PlayOrzAnimation(5);
		vanpanel.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.2f);
		vanpanel.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
		gameManager.CanShowSetting(-1);
		img_top.DOLocalMoveY(615f, 0.5f);
		img_bottom.DOLocalMoveY(-615f, 0.5f);
		Min();
		yield return new WaitForSeconds(0.5f);
		gameManager.player.playerdata.livebroadingstep = 3;
		gameManager.saveManager.SavePlayerData();
		ShowHiTalk();
	}

	private void AddFansItem(string key)
	{
		UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/fansitem") as GameObject, fanscontent).GetComponent<FansItem>().Init(key, avatarlist[gameManager.player.playerdata.livebroadingcurrenthopeid]);
		LayoutRebuilder.ForceRebuildLayoutImmediate(fanscontent);
	}

	private void SelectHope()
	{
		if (gameManager.player.playerdata.hopestep == -1)
		{
			int index = UnityEngine.Random.Range(0, gameManager.player.playerdata.leftshowspecials0102.Count);
			gameManager.player.playerdata.livebroadingcurrenthopeid = gameManager.player.playerdata.leftshowspecials0102[index];
			gameManager.player.playerdata.leftshowspecials0102.RemoveAt(index);
			gameManager.player.playerdata.hopestep++;
		}
		else if (gameManager.player.playerdata.hopestep == 0)
		{
			int index2 = UnityEngine.Random.Range(0, gameManager.player.playerdata.leftshowspecials0304.Count);
			gameManager.player.playerdata.livebroadingcurrenthopeid = gameManager.player.playerdata.leftshowspecials0304[index2];
			gameManager.player.playerdata.leftshowspecials0304.RemoveAt(index2);
			gameManager.player.playerdata.hopestep++;
		}
		else if (gameManager.player.playerdata.hopestep == 1)
		{
			int index3 = UnityEngine.Random.Range(0, gameManager.player.playerdata.leftshowspecials05.Count);
			gameManager.player.playerdata.livebroadingcurrenthopeid = gameManager.player.playerdata.leftshowspecials05[index3];
			gameManager.player.playerdata.leftshowspecials05.RemoveAt(index3);
			gameManager.player.playerdata.hopestep++;
		}
		else if (gameManager.player.playerdata.hopestep == 2)
		{
			gameManager.player.playerdata.livebroadingcurrenthopeid = 10;
			gameManager.player.playerdata.livebroadingstep = 2;
			gameManager.player.playerdata.hopestep++;
		}
		gameManager.saveManager.SavePlayerData();
	}
}
