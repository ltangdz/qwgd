using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerDialog : MonoBehaviour
{
	[SerializeField]
	private List<HackerTriangle> hackerTriangles = new List<HackerTriangle>();

	[SerializeField]
	private List<HackerItem> hackerItems = new List<HackerItem>();

	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private Animator logo;

	[SerializeField]
	private GameObject red;

	[SerializeField]
	private Text txt_titleleft01;

	[SerializeField]
	private Text txt_titleleft02;

	[SerializeField]
	private Text txt_titleleft03;

	[SerializeField]
	private Text txt_titleright;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private CanvasGroup img_ip;

	[SerializeField]
	private CanvasGroup img_example;

	[SerializeField]
	private GameObject img_wending;

	[SerializeField]
	private List<string> loadsystem = new List<string>();

	[SerializeField]
	private List<string> ciolist = new List<string>();

	[SerializeField]
	private List<string> ciolist2 = new List<string>();

	[SerializeField]
	private List<string> oversystem = new List<string>();

	[SerializeField]
	private List<string> lastsystem = new List<string>();

	private GameManager gameManager;

	private string[] load01 = new string[5] { "THREATS FOUND", "REMOVING THREATS", "KANE_1.TROJAN", "BROTHERHOOD.WORK", "COMPLETE" };

	private string[] load02 = new string[1] { "COMPLETE" };

	private string[] load03 = new string[1] { "COMPLETE" };

	private string[] load04 = new string[21]
	{
		"LOGOUT.AUD", "REBOOT.AUD", "1577623982.PRI", "1577623983.PRI", "1577623984.PRI", "1577623985.PRI", "1577623986.PRI", "1577623987.PRI", "1577623988.PRI", "1577623989.PRI",
		"1577623990.PRI", "1577623991.PRI", "1577623992.PRI", "1577623993.PRI", "1577623994.PRI", "1577623995.PRI", "1577623996.PRI", "1577623997.PRI", "1577623998.PRI", "1577623999.PRI",
		"COMPLETE"
	};

	private string[] load05 = new string[8] { "FHCDL.DYY", "GMLF32.DYY", "HBAAPITY.DYY", "ICFUPGD.DYY", "KBDBGPH1.DYY", "REASON.DYY", "INTELLECT.DYY", "COMPLETE" };

	private bool isclick;

	public bool isopenachi = true;

	private bool isover;

	private IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds(2f);
		red.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		red.SetActive(value: false);
		logo.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		txt_titleleft01.DOText(I18N.instance.getValue("^hacker11"), 0.5f);
		yield return new WaitForSeconds(0.5f);
		txt_titleleft02.text = I18N.instance.getValue("^hacker13");
		yield return new WaitForSeconds(0.3f);
		txt_titleleft03.DOText(I18N.instance.getValue("^hacker12"), 0.5f);
		yield return new WaitForSeconds(0.6f);
		txt_titleright.text = I18N.instance.getValue("^hacker14");
		txt_titleright.DOFade(1f, 0.3f);
		StartCoroutine(Init());
		InvokeRepeating("RefreshTextRight", 0.3f, 0.2f);
	}

	private void RefreshTextRight()
	{
		string text = Convert.ToString(UnityEngine.Random.Range(1000000, 20000000), 16);
		txt_titleright.text = I18N.instance.getValue("^hacker14") + "00" + text.ToUpper();
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(4);
		gameManager.soundManager.StopLoop();
		StartCoroutine(StartAnimation());
	}

	private IEnumerator Init()
	{
		gameManager.homeScene.hackerBk.Crash();
		for (int i = 0; i < ciolist.Count; i++)
		{
			float seconds = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_typercode") as GameObject, scrollRect.content).GetComponent<TyperCode>().Init("^tuili400410", ciolist[i]);
			yield return new WaitForSeconds(seconds);
		}
		GameObject txt_code = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content);
		int percent = 0;
		DOTween.To(() => percent, delegate(int x)
		{
			percent = x;
		}, 6, 1f).OnUpdate(delegate
		{
			txt_code.GetComponent<Text>().text = ((percent % 2 == 0) ? "/" : "\\");
		});
		scrollRect.normalizedPosition = Vector3.zero;
		yield return new WaitForSeconds(0.5f);
		UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content).GetComponent<Text>().text = I18N.instance.getValue("^hacker23");
		yield return new WaitForSeconds(0.5f);
		isclick = true;
	}

	private void Update()
	{
		if (isclick && Input.anyKeyDown)
		{
			isclick = false;
			step02.SetActive(value: true);
			gameManager.soundManager.PlayHackerSound(8);
			img_ip.DOFade(1f, 0.3f);
			img_ip.transform.DOScale(Vector3.one, 0.3f);
			img_example.DOFade(1f, 0.3f);
			img_example.transform.DOScale(Vector3.one, 0.3f);
			if (gameManager.issteam && gameManager.steamAchi != null && !gameManager.steamAchi.GetAchievement("vanhack"))
			{
				Invoke("OpenAchi", 60f);
				isopenachi = true;
			}
		}
	}

	private void OpenAchi()
	{
		isopenachi = false;
	}

	public void ChangeItem(HackerItem aitem, HackerItem bitem)
	{
		for (int i = 0; i < hackerTriangles.Count; i++)
		{
			for (int j = 0; j < hackerTriangles[i].hackerItems.Count; j++)
			{
				if (hackerTriangles[i].hackerItems[j].gameObject.name.Equals(aitem.gameObject.name))
				{
					hackerTriangles[i].hackerItems[j] = bitem;
					hackerTriangles[i].check();
				}
				else if (hackerTriangles[i].hackerItems[j].gameObject.name.Equals(bitem.gameObject.name))
				{
					hackerTriangles[i].hackerItems[j] = aitem;
					hackerTriangles[i].check();
				}
			}
		}
		bool flag = true;
		for (int k = 0; k < hackerTriangles.Count; k++)
		{
			if (!hackerTriangles[k].iswending)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		if (gameManager.issteam && gameManager.steamAchi != null && !gameManager.steamAchi.GetAchievement("vanhack"))
		{
			if (isopenachi)
			{
				Debug.Log("打开成就vanhack");
				gameManager.UnlockAchievements("vanhack");
			}
			else
			{
				Debug.Log("gaunbi成就vanhack");
			}
		}
		for (int l = 0; l < hackerItems.Count; l++)
		{
			hackerItems[l].iscandrag = false;
		}
		if (gameManager.homeScene.hackerBk.hackerCountDown != null)
		{
			gameManager.homeScene.hackerBk.hackerCountDown.StopTime();
		}
		img_wending.SetActive(value: true);
		img_wending.GetComponent<Image>().DOFade(1f, 0.2f);
		img_wending.transform.DOScale(Vector3.one, 0.2f).OnComplete(delegate
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(img_wending.transform.GetChild(0).GetComponent<Text>().DOFade(0.2f, 0.5f));
			sequence.Append(img_wending.transform.GetChild(0).GetComponent<Text>().DOFade(1f, 0.5f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				img_ip.DOFade(0f, 0.3f);
				img_ip.transform.DOScale(Vector3.zero, 0.3f);
				img_example.DOFade(0f, 0.3f);
				img_example.transform.DOScale(Vector3.zero, 0.3f);
				img_wending.GetComponent<Image>().DOFade(0f, 0.3f);
				img_wending.transform.DOScale(Vector3.zero, 0.3f);
				gameManager.musicManager.PlayMusicLoop(3);
				if (!isover)
				{
					StartCoroutine(Over());
				}
			});
		});
	}

	private IEnumerator Over()
	{
		isover = true;
		for (int i = 0; i < hackerItems.Count; i++)
		{
			hackerItems[i].iscandrag = false;
		}
		yield return new WaitForSeconds(0.5f);
		for (int j = 0; j < 1; j++)
		{
			GameObject txt_code = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content);
			int percent = 0;
			DOTween.To(() => percent, delegate(int x)
			{
				percent = x;
			}, 20, 3f).OnUpdate(delegate
			{
				txt_code.GetComponent<Text>().text = ((percent % 2 == 0) ? "/" : "\\");
			});
			scrollRect.normalizedPosition = Vector3.zero;
			yield return new WaitForSeconds(3f);
		}
		for (int j = 0; j < ciolist2.Count; j++)
		{
			float seconds = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_typercode") as GameObject, scrollRect.content).GetComponent<TyperCode>().Init("^tuili400410", ciolist2[j]);
			scrollRect.normalizedPosition = Vector3.zero;
			yield return new WaitForSeconds(seconds);
		}
		for (int j = 0; j < 1; j++)
		{
			GameObject txt_code2 = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content);
			int percent2 = 0;
			DOTween.To(() => percent2, delegate(int x)
			{
				percent2 = x;
			}, 20, 3f).OnUpdate(delegate
			{
				txt_code2.GetComponent<Text>().text = ((percent2 % 2 == 0) ? "/" : "\\");
			});
			scrollRect.normalizedPosition = Vector3.zero;
			yield return new WaitForSeconds(3f);
		}
		StartLoadSelectGroup();
	}

	public void StartLoadSelectGroup()
	{
		StartCoroutine(LoadSelectGroup());
	}

	public void StartLoadReLoadSystem()
	{
		StartCoroutine(ReLoadSystem());
	}

	private IEnumerator LoadSelectGroup()
	{
		for (int i = 0; i < oversystem.Count; i++)
		{
			float seconds = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_typercode") as GameObject, scrollRect.content).GetComponent<TyperCode>().Init("", oversystem[i]);
			scrollRect.normalizedPosition = Vector3.zero;
			yield return new WaitForSeconds(seconds);
		}
		UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content).GetComponent<Text>().text = I18N.instance.getValue("^hacker54");
		scrollRect.normalizedPosition = Vector3.zero;
		yield return new WaitForSeconds(0.2f);
		UnityEngine.Object.Instantiate(Resources.Load("Hacker/hackbutton") as GameObject, scrollRect.content).GetComponent<HackerButton>().hackerDialog = this;
		yield return new WaitForSeconds(0.2f);
		scrollRect.normalizedPosition = Vector3.zero;
	}

	private IEnumerator ReLoadSystem()
	{
		float seconds = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_typercode") as GameObject, scrollRect.content).GetComponent<TyperCode>().Init("", "^hacker50");
		scrollRect.normalizedPosition = Vector3.zero;
		yield return new WaitForSeconds(seconds);
		for (int i = 0; i < loadsystem.Count; i++)
		{
			if (i <= 4)
			{
				float seconds2 = 0f;
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content);
				switch (i)
				{
				case 0:
					seconds2 = gameObject.GetComponent<TyperCode>().Init2(loadsystem[i], load01);
					break;
				case 1:
					seconds2 = gameObject.GetComponent<TyperCode>().Init2(loadsystem[i], load02);
					break;
				case 2:
					seconds2 = gameObject.GetComponent<TyperCode>().Init2(loadsystem[i], load03);
					break;
				case 3:
					seconds2 = gameObject.GetComponent<TyperCode>().Init2(loadsystem[i], load04);
					break;
				case 4:
					seconds2 = gameObject.GetComponent<TyperCode>().Init2(loadsystem[i], load05);
					break;
				}
				scrollRect.normalizedPosition = Vector3.zero;
				yield return new WaitForSeconds(seconds2);
			}
		}
		for (int j = 0; j < lastsystem.Count; j++)
		{
			GameObject txt_code = UnityEngine.Object.Instantiate(Resources.Load("Hacker/txt_code") as GameObject, scrollRect.content);
			int percent = 0;
			DOTween.To(() => percent, delegate(int x)
			{
				percent = x;
			}, 100, 5f).OnUpdate(delegate
			{
				txt_code.GetComponent<Text>().text = I18N.instance.getValue(lastsystem[j]) + percent + "%";
			});
			scrollRect.normalizedPosition = Vector3.zero;
			yield return new WaitForSeconds(5f);
		}
		gameManager.homeScene.hackerBk.DestroyAllLast();
		UnityEngine.Object.Instantiate(Resources.Load("Dialog/Hacker/loadingPanel") as GameObject, base.transform.parent).GetComponent<LoadingPanel>().SetReload();
		gameManager.homeScene.cameraFilterPack_Noise_TV_1.enabled = false;
		gameManager.homeScene.cameraFilterPack_Noise_TV_2.enabled = false;
		UnityEngine.Object.Destroy(base.gameObject);
		gameManager.musicManager.PlayMusicLoop(3);
	}
}
