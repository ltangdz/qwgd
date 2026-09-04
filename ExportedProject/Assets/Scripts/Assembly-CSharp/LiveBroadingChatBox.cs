using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LiveBroadingChatBox : CustomDialog
{
	[Serializable]
	private class HopeItem
	{
		public string hopestartduihua;

		public string itemmessage;

		public string itemid;
	}

	public Button hotarea;

	public Button btn_top;

	[SerializeField]
	private Text txt_loginname;

	[SerializeField]
	private GameObject mouse;

	[SerializeField]
	private GameObject img_dragarea;

	[SerializeField]
	private List<string> startzimus = new List<string>();

	[SerializeField]
	private List<string> startzimus2 = new List<string>();

	[SerializeField]
	private List<HopeItem> starthopezimus = new List<HopeItem>();

	[SerializeField]
	private Transform content0;

	[SerializeField]
	private SelectGroup selectGroup;

	[SerializeField]
	private ScrollRect scrollRect;

	public ChatBak chatbak;

	public Text txt_zimu;

	public LivebroadingChatLabelInfo highlightlabelinfo;

	[SerializeField]
	[Header("输入框")]
	private InputField inputField;

	[SerializeField]
	private Button btn_send;

	[SerializeField]
	private List<string> hopeansweritemids = new List<string>();

	[SerializeField]
	private string lastonewrong;

	[SerializeField]
	private List<string> lastonewright = new List<string>();

	[SerializeField]
	private GameObject img_noclose;

	[SerializeField]
	private List<string> special5before = new List<string>();

	[SerializeField]
	private List<string> bossover = new List<string>();

	[SerializeField]
	private List<string> everyhopeanswers = new List<string>();

	[SerializeField]
	private List<string> everyhopeanswervoices = new List<string>();

	private string currentkey;

	private void Update()
	{
		if (btn_send.interactable && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
		{
			AddQuestion();
		}
	}

	public void AddQuestion()
	{
		if (gameManager.player.playerdata.livebroadinglefttime <= 0 || string.IsNullOrEmpty(inputField.text))
		{
			return;
		}
		bool flag = false;
		int soundid = -1;
		string[] array = everyhopeanswers[gameManager.player.playerdata.livebroadingcurrenthopeid].Trim().Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Debug.LogError(I18N.instance.getValue(array[i].Trim()).Trim() + "^^^" + inputField.text.Trim().ToLower());
			if (I18N.instance.getValue(array[i].Trim()).Trim().ToLower()
				.Equals(inputField.text.Trim().ToLower()))
			{
				flag = true;
				soundid = int.Parse(everyhopeanswervoices[gameManager.player.playerdata.livebroadingcurrenthopeid].Split(';')[i]);
				break;
			}
		}
		if (flag)
		{
			img_noclose.SetActive(value: true);
			inputField.interactable = false;
			btn_send.interactable = false;
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, inputField.text);
			gameManager.homeScene.liveBroadcastingDialog.OrzReadQuestion(inputField.text, soundid);
			gameManager.player.playerdata.livebroadinganswerrecords.Add(inputField.text);
			gameManager.saveManager.SavePlayerData();
			Hide();
		}
		else
		{
			StartCoroutine(ShowWrongAnswer());
		}
	}

	private IEnumerator ShowWrongAnswer()
	{
		string text = inputField.text;
		inputField.text = "";
		UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, text);
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: true, "^wrong_answer0501");
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: true, "^wrong_answer0502");
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, Vector2.zero, 1f);
	}

	public void ShowCourse1()
	{
		chatbak.gameObject.SetActive(value: true);
		chatbak.ShowCourse();
		txt_zimu.DOText(I18N.instance.getValue("^zhibo_name32"), 2f);
	}

	public void HideCourse1()
	{
		Debug.LogError("HideCourse1");
		chatbak.HideBlack();
		chatbak.gameObject.SetActive(value: false);
		txt_zimu.text = "";
		if (highlightlabelinfo != null)
		{
			highlightlabelinfo.DeleteHighLight();
		}
		Hide();
	}

	private void Start()
	{
		gameManager.homeScene.liveBroadingChatBox = this;
		btn_send.onClick.AddListener(AddQuestion);
		btn_close.onClick.AddListener(delegate
		{
			if (gameManager.homeScene.liveBroadcastingDialog != null)
			{
				gameManager.homeScene.liveBroadcastingDialog.Hide();
			}
		});
		btn_top.onClick.AddListener(delegate
		{
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.one, 1f);
		});
		txt_loginname.text = gameManager.player.playerdata.nickname;
	}

	private void Init()
	{
		if (gameManager.player.playerdata.livebroadingchatstep <= 1)
		{
			StartCoroutine(InitTor());
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 2)
		{
			for (int i = 0; i < startzimus.Count; i++)
			{
				string[] array = startzimus[i].Split(';');
				if (array[0].Equals("0"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array[1]);
				}
				else if (array[0].Equals("1"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array[1]);
				}
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			StartCoroutine(InitTor3());
		}
		else
		{
			if (gameManager.player.playerdata.livebroadingchatstep < 3)
			{
				return;
			}
			for (int num = 0; num < startzimus.Count; num++)
			{
				string[] array2 = startzimus[num].Split(';');
				if (array2[0].Equals("0"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array2[1]);
				}
				else if (array2[0].Equals("1"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array2[1]);
				}
			}
			for (int num2 = 0; num2 < startzimus2.Count; num2++)
			{
				string[] array3 = startzimus2[num2].Split(';');
				if (array3[0].Equals("0"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array3[1]);
				}
				else if (array3[0].Equals("1"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array3[1]);
				}
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			StartNewHope();
		}
	}

	private IEnumerator InitTor()
	{
		gameManager.player.playerdata.livebroadingchatstep = 0;
		gameManager.saveManager.SavePlayerData();
		for (int i = 0; i < 2; i++)
		{
			string[] array = startzimus[i].Split(';');
			if (array[0].Equals("0"))
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: true, array[1]);
			}
			else if (array[0].Equals("1"))
			{
				currentkey = array[1];
				string[] selects = new string[1] { I18N.instance.getValue(array[1]) };
				selectGroup.gameObject.SetActive(value: true);
				selectGroup.SetSelect(selects, ClickSelect);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator InitTor2()
	{
		yield return new WaitForSeconds(2f);
		for (int i = 2; i < startzimus.Count; i++)
		{
			string[] array = startzimus[i].Split(';');
			if (array[0].Equals("0"))
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: true, array[1]);
			}
			else if (array[0].Equals("1"))
			{
				currentkey = array[1];
				string[] selects = new string[1] { I18N.instance.getValue(array[1]) };
				selectGroup.gameObject.SetActive(value: true);
				selectGroup.SetSelect(selects, ClickSelect);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator InitTor3()
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < startzimus2.Count; i++)
		{
			string[] array = startzimus2[i].Split(';');
			if (array[0].Equals("0"))
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: true, array[1]);
			}
			else if (array[0].Equals("1"))
			{
				currentkey = array[1];
				string[] selects = new string[1] { I18N.instance.getValue(array[1]) };
				selectGroup.gameObject.SetActive(value: true);
				selectGroup.SetSelect(selects, ClickSelect);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds(2f);
		}
		StartNewHope();
		if (gameManager.player.playerdata.livebroadingchatstep < 3)
		{
			gameManager.player.playerdata.livebroadingchatstep = 3;
			gameManager.saveManager.SavePlayerData();
		}
	}

	private void StartNewHope()
	{
		StartCoroutine(StartNewHopeAni());
	}

	private IEnumerator StartNewHopeAni()
	{
		bool iscannext = true;
		for (int i = 0; i < gameManager.player.playerdata.compeletehopelist.Count; i++)
		{
			string[] array = gameManager.player.playerdata.compeletehopelist[i].Split(';');
			AddWenti(int.Parse(array[0]), isani: false);
			if (gameManager.player.playerdata.livebroadinganswerrecords.Count > i)
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, gameManager.player.playerdata.livebroadinganswerrecords[i]);
			}
			if (i < 2)
			{
				if (array[1].Equals("1"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(i == gameManager.player.playerdata.compeletehopelist.Count - 1, lastonewright[i]);
				}
				else if (array[1].Equals("0"))
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(i == gameManager.player.playerdata.compeletehopelist.Count - 1, lastonewrong);
				}
			}
			else if (i == 2)
			{
				for (int j = 0; j < special5before.Count; j++)
				{
					if (j != 2)
					{
						UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(gameManager.player.playerdata.livebroadingchatstep == 3, special5before[j]);
					}
					else
					{
						UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init2(gameManager.player.playerdata.livebroadingchatstep == 3, special5before[j], "^liveroom04", "10597");
					}
				}
				if (gameManager.player.playerdata.livebroadingchatstep == 3)
				{
					gameManager.player.playerdata.livebroadingchatstep = 4;
					gameManager.saveManager.SavePlayerData();
					iscannext = false;
				}
				else if (gameManager.player.playerdata.livebroadingchatstep >= 4)
				{
					img_noclose.SetActive(value: false);
				}
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds((i == gameManager.player.playerdata.compeletehopelist.Count - 1) ? 2f : 0f);
		}
		if (iscannext && gameManager.player.playerdata.livebroadingchatstep < 4)
		{
			AddWenti(gameManager.player.playerdata.livebroadingcurrenthopeid, isani: true);
			inputField.interactable = true;
			btn_send.interactable = true;
			img_noclose.SetActive(value: false);
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
		}
		if (iscannext && gameManager.player.playerdata.livebroadingchatstep >= 4 && gameManager.player.playerdata.temporaryhopelist.Contains("10559") && !gameManager.player.playerdata.compeletehopelist.Contains("10;1"))
		{
			AddWenti(gameManager.player.playerdata.livebroadingcurrenthopeid, gameManager.player.playerdata.livebroadingchatstep == 4);
			inputField.interactable = true;
			btn_send.interactable = true;
			if (gameManager.player.playerdata.livebroadingchatstep != 4)
			{
				img_noclose.SetActive(value: false);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
		}
		if (gameManager.player.playerdata.livebroadingchatstep == 5 && gameManager.player.playerdata.compeletehopelist.Contains("10;1"))
		{
			StartCoroutine(bossover1(0, 5, 5, isitembak: false));
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 6 && gameManager.player.playerdata.compeletehopelist.Contains("10;1"))
		{
			StartCoroutine(bossover1(0, 7, 6, isitembak: true));
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 7 && gameManager.player.playerdata.compeletehopelist.Contains("10;1"))
		{
			StartCoroutine(bossover1(0, 9, 7, isitembak: true));
		}
	}

	private IEnumerator bossover1(int start, int length, int setstep, bool isitembak)
	{
		img_noclose.SetActive(gameManager.player.playerdata.livebroadingchatstep <= setstep);
		for (int i = start; i < start + length; i++)
		{
			if (bossover[i].Contains("*"))
			{
				if (isitembak)
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, bossover[i].Substring(0, bossover[i].Length - 1));
				}
				else if (gameManager.player.playerdata.livebroadingchatstep == setstep)
				{
					gameManager.homeScene.liveBroadcastingDialog.AddBlack(isshowzimu: false);
					string[] selects = new string[1] { I18N.instance.getValue(bossover[i].Substring(0, bossover[i].Length - 1)) };
					img_noclose.SetActive(value: true);
					selectGroup.gameObject.SetActive(value: true);
					selectGroup.SetSelect(selects, ClickSelect);
					gameManager.player.playerdata.livebroadingchatstep++;
				}
				else
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, bossover[i].Substring(0, bossover[i].Length - 1));
				}
			}
			else
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(gameManager.player.playerdata.livebroadingchatstep <= setstep, bossover[i]);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
			yield return new WaitForSeconds((gameManager.player.playerdata.livebroadingchatstep == setstep) ? 2f : 0f);
		}
	}

	private void AddWenti(int hopeid, bool isani)
	{
		HopeItem hopeItem = starthopezimus[hopeid];
		string[] array = hopeItem.hopestartduihua.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("*"))
			{
				if (gameManager.player.playerdata.livebroadingchatstep < 5)
				{
					gameManager.homeScene.liveBroadcastingDialog.AddBlack(isshowzimu: false);
					string[] selects = new string[1] { I18N.instance.getValue(array[i].Substring(0, array[i].Length - 1)) };
					img_noclose.SetActive(value: true);
					selectGroup.gameObject.SetActive(value: true);
					selectGroup.SetSelect(selects, ClickSelect);
				}
				else
				{
					UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, array[i].Substring(0, array[i].Length - 1));
					if (hopeid == 10 && array[i].Substring(0, array[i].Length - 1).Equals("^live2716") && !gameManager.homeScene.liveBroadcastingDialog.liveBroadTimePanel.isstart && !gameManager.player.playerdata.compeletehopelist.Contains("10;1"))
					{
						gameManager.homeScene.liveBroadcastingDialog.StartTime(isinit: true, isstarttime: true);
						gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
					}
				}
			}
			else if (array.Length == 1 || i == 2)
			{
				if (hopeItem.itemid != "")
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0);
					gameObject.GetComponent<LivebroadingChatLabelInfo>().Init2(!gameManager.player.playerdata.temporaryhopelist.Contains(hopeItem.itemid), array[i], hopeItem.itemmessage, hopeItem.itemid);
					highlightlabelinfo = gameObject.GetComponent<LivebroadingChatLabelInfo>();
				}
			}
			else
			{
				UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_item") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani, array[i]);
			}
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
		}
	}

	public void ClickSelect(int poss)
	{
		if (!selectGroup.iscanclick)
		{
			return;
		}
		if (gameManager.player.playerdata.hopestep == 3 && gameManager.player.playerdata.livebroadingchatstep < 6)
		{
			if (gameManager.player.playerdata.livebroadingchatstep == 4)
			{
				gameManager.player.playerdata.livebroadingchatstep = 5;
				gameManager.saveManager.SavePlayerData();
				gameManager.homeScene.liveBroadcastingDialog.StartTime(isinit: true, isstarttime: true);
			}
			gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
			gameManager.homeScene.liveBroadcastingDialog.Hide();
			Hide();
			img_noclose.SetActive(value: false);
			return;
		}
		Debug.LogError("gameManager.player.playerdata.livebroadingchatstep" + gameManager.player.playerdata.livebroadingchatstep);
		if (gameManager.player.playerdata.livebroadingchatstep == 6)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, bossover[4].Replace("*", ""));
			StartCoroutine(bossover1(5, 2, 6, isitembak: false));
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 7)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, bossover[6].Replace("*", ""));
			StartCoroutine(bossover1(7, 2, 7, isitembak: false));
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 8)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, bossover[8].Replace("*", ""));
			if (gameManager.homeScene.liveBroadcastingDialog != null)
			{
				gameManager.homeScene.liveBroadcastingDialog.Over();
			}
			gameManager.homeScene.zhibojiannotebook.DeleteBossHopePanel();
			gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
			gameManager.player.playerdata.ResetLiveBroading(0);
			gameManager.homeScene.ResumeAll();
			gameManager.homeScene.SendMail("1500107");
			gameManager.homeScene.goalDialog.CompletePercentItem("2000023", 100f);
			gameManager.player.playerdata.iszhiboover = true;
			gameManager.saveManager.SavePlayerData();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.homeScene.liveBroadingEnterBtn.gameObject.SetActive(value: false);
			gameManager.homeScene.liveBroadingchatEnterBtn.gameObject.SetActive(value: false);
			img_noclose.SetActive(value: false);
			Hide();
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 0)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, currentkey);
			gameManager.player.playerdata.livebroadingchatstep++;
			StartCoroutine(InitTor2());
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 1)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, currentkey);
			gameManager.homeScene.liveBroadcastingDialog.StartVan();
			gameManager.homeScene.liveBroadingchatEnterBtn.gameObject.SetActive(value: true);
			gameManager.player.playerdata.livebroadingchatstep++;
			Hide();
		}
		else if (gameManager.player.playerdata.livebroadingchatstep == 3)
		{
			img_noclose.SetActive(value: false);
			UnityEngine.Object.Instantiate(Resources.Load("Livebroadcasting/chat_itemBak") as GameObject, content0).GetComponent<LivebroadingChatLabelInfo>().Init(isani: false, currentkey);
			gameManager.saveManager.SavePlayerData();
			gameManager.homeScene.liveBroadcastingDialog.StartVan();
			gameManager.homeScene.liveBroadingchatEnterBtn.gameObject.SetActive(value: true);
			gameManager.player.playerdata.livebroadingchatstep++;
			Hide();
		}
		gameManager.saveManager.SavePlayerData();
		gameManager.soundManager.Stop();
		selectGroup.HideSelect();
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		Init();
	}
}
