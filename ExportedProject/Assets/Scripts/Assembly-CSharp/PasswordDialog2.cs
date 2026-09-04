using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PasswordDialog2 : CustomDialog
{
	public Text[] passwords;

	public string pw = "23jkKOdd";

	public int hascount;

	public string trueCode;

	public CustomSlider slider;

	public Color lightcolor;

	public GameObject buttons;

	public GameObject img_notclick;

	public GameObject passworddialog1;

	public Button btn_add;

	public Button btn_sign;

	public Image[] img_dots;

	public Sprite[] sprites;

	public GameObject passwordFailed;

	public GameObject imgBk;

	private int dotpos;

	public bool passEnd;

	public Image passworkbk;

	public GameObject imgDragArea;

	public Color bluecolor;

	private bool ishasitem;

	private bool ishasclick;

	public bool iscanclick;

	private bool iscancancle;

	private string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

	public Transform ani1;

	public Transform ani2;

	public Transform ani3;

	private int runPasswordIndex;

	private void StartDots()
	{
		img_dots[dotpos].sprite = sprites[0];
		dotpos = ((dotpos != img_dots.Length - 1) ? (dotpos + 1) : 0);
		img_dots[dotpos].sprite = sprites[1];
	}

	private void Start()
	{
		gameManager.homeScene.passworddialog2 = this;
		new DATA11();
		btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^highlighttip01");
		hascount = 0;
		slider.SetPercent(100);
		string passwords = gameManager.dataManager.dic11[gameManager.player.GetEventId()].passwords1;
		passwords = ((passwords == "" || passwords == " ") ? "" : passwords.Substring(1));
		string[] array = passwords.Split(';');
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			Debug.Log(pw + "  " + array[i]);
			if (pw == array[i])
			{
				flag = true;
			}
		}
		if (flag)
		{
			InvokeRepeating("SetAnimation1", 0.1f, 0.03f);
			InvokeRepeating("SetAnimation2", 0.5f, 0.03f);
			InvokeRepeating("SetAnimation3", 0.3f, 0.03f);
			InvokeRepeating("StartDots", 0.1f, 0.5f);
			SetPassword();
		}
		else
		{
			StartCoroutine(Failed());
		}
		btn_add.onClick.AddListener(delegate
		{
			img_notclick.gameObject.SetActive(value: false);
			gameManager.homeScene.notebook.gameObject.SetActive(value: true);
			passworddialog1.GetComponent<PasswordDialog1>().Hide();
			Hide();
			int num = int.Parse(gameManager.dataManager.dic1[trueCode].role.Substring(1));
			if (num >= 3100036 && num <= 3100047)
			{
				gameManager.homeScene.zhibojiannotebook.gameObject.SetActive(value: true);
				gameManager.homeScene.zhibojiannotebook.AddNewItem(trueCode);
			}
			else if (num == 3110003)
			{
				gameManager.homeScene.notebook.gameObject.SetActive(value: true);
				gameManager.homeScene.notebook.AddNewItem(trueCode);
				gameManager.homeScene.notebook.AddNewItems(new string[2] { "11123", trueCode });
			}
			else
			{
				gameManager.homeScene.notebook.gameObject.SetActive(value: true);
				gameManager.homeScene.notebook.AddNewItem(trueCode);
			}
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			passworkbk.sprite = sprites[3];
			CloseButton();
			if (gameManager.player.playerdata.isCourse13 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel13.HideCourse();
			}
		});
		if (btn_sign != null)
		{
			btn_sign.onClick.AddListener(delegate
			{
				img_notclick.gameObject.SetActive(value: false);
				CloseButton();
			});
		}
		btn_close.onClick.AddListener(delegate
		{
			if (passworddialog1 != null)
			{
				passworddialog1.GetComponent<PasswordDialog1>().passworddialog2 = null;
			}
		});
		if (gameManager.player.playerdata.isCourse13 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel13.pojieresult = imgBk;
		}
		SetFront();
	}

	private IEnumerator Failed()
	{
		InvokeRepeating("SetAnimation1", 0.1f, 0.03f);
		InvokeRepeating("SetAnimation2", 0.5f, 0.03f);
		InvokeRepeating("SetAnimation3", 0.3f, 0.03f);
		InvokeRepeating("StartDots", 0.1f, 0.5f);
		yield return new WaitForSeconds(2f);
		CancelInvoke("SetAnimation1");
		CancelInvoke("SetAnimation2");
		CancelInvoke("SetAnimation3");
		CancelInvoke("StartDots");
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(26);
		imgBk.SetActive(value: false);
		passwordFailed.SetActive(value: true);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(30);
		yield return new WaitForSeconds(0.2f);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(30);
		yield return new WaitForSeconds(0.2f);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(30);
		passEnd = true;
	}

	private void SetPassword()
	{
		StartCoroutine(StartPassword());
	}

	private IEnumerator StartPassword()
	{
		int[] pass = new int[8];
		for (int i = 0; i < passwords.Length; i++)
		{
			bool findPw = false;
			yield return new WaitForSeconds(0.5f);
			while (!findPw)
			{
				int num = Random.Range(0, 8);
				if (pass[num] == 0)
				{
					findPw = true;
					pass[num]++;
					if (pw != null && num < pw.Length)
					{
						passwords[num].GetComponent<I18NText>().updateTranslation2(pw.Substring(num, 1));
						gameManager.soundManager.Stop();
						gameManager.soundManager.PlaySound(30);
					}
					passwords[num].color = lightcolor;
					AddCount();
				}
			}
		}
		CancelInvoke("SetAnimation1");
		CancelInvoke("SetAnimation2");
		CancelInvoke("SetAnimation3");
		CancelInvoke("StartDots");
		EndAnimation(ani1, 3);
		EndAnimation(ani2, 2);
		EndAnimation(ani3, 3);
		if (gameManager.player.playerdata.isCourse13 == 0)
		{
			gameManager.homeScene.courseManager.ShowCourse13();
		}
		yield return new WaitForSeconds(0.5f);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(30);
		yield return new WaitForSeconds(0.2f);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(30);
		passEnd = true;
		if (gameManager.player.playerdata.itemlist.Contains(trueCode))
		{
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			passworkbk.sprite = sprites[3];
			for (int j = 0; j < passwords.Length; j++)
			{
				passwords[j].color = Color.white;
			}
			ishasitem = true;
		}
	}

	public void GetPassword()
	{
		if (!iscanclick)
		{
			return;
		}
		ishasclick = true;
		img_notclick.gameObject.SetActive(value: true);
		buttons.SetActive(value: true);
		passworkbk.sprite = sprites[3];
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(buttons.GetComponent<RectTransform>(), Input.mousePosition, gameManager.maincamera.GetComponent<Camera>(), out var worldPoint))
		{
			buttons.transform.position = worldPoint;
		}
		if (gameManager.player.playerdata.itemlist.Contains(trueCode))
		{
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			passworkbk.sprite = sprites[3];
			for (int i = 0; i < passwords.Length; i++)
			{
				passwords[i].color = Color.white;
			}
			ishasitem = true;
		}
		StartButton();
	}

	public void Enter()
	{
		if (iscanclick && !ishasitem && !ishasclick)
		{
			for (int i = 0; i < passwords.Length; i++)
			{
				passwords[i].color = Color.white;
			}
			passworkbk.sprite = sprites[3];
		}
	}

	public void Exit()
	{
		if (iscanclick && !ishasitem && !ishasclick)
		{
			for (int i = 0; i < passwords.Length; i++)
			{
				passwords[i].color = lightcolor;
			}
			passworkbk.sprite = sprites[2];
		}
	}

	public void CancelClick()
	{
		ishasclick = false;
		img_notclick.gameObject.SetActive(value: false);
		iscanclick = true;
		if (!ishasitem)
		{
			passworkbk.sprite = sprites[2];
		}
		CloseButton();
	}

	private void StartButton()
	{
		buttons.GetComponent<RectTransform>().DOKill();
		buttons.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).OnComplete(delegate
		{
			iscancancle = true;
		});
		if (buttons.GetComponent<CanvasGroup>() == null)
		{
			buttons.AddComponent<CanvasGroup>().DOFade(1f, 0.3f);
			return;
		}
		buttons.GetComponent<CanvasGroup>().DOKill();
		buttons.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	private void CloseButton()
	{
		buttons.GetComponent<RectTransform>().DOKill();
		buttons.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f).OnComplete(delegate
		{
			iscancancle = false;
			buttons.SetActive(value: false);
		});
		if (buttons.GetComponent<CanvasGroup>() != null)
		{
			buttons.GetComponent<CanvasGroup>().DOKill();
			buttons.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		}
	}

	public void AddCount()
	{
		hascount++;
		if (hascount >= pw.Length)
		{
			CancelInvoke("AddCount");
			if (trueCode == "0" || trueCode == "")
			{
				iscanclick = true;
			}
			else
			{
				iscanclick = true;
			}
		}
	}

	private IEnumerator AutoHide(float s)
	{
		_ = (GameObject)Object.Instantiate(Resources.Load("Dialog/errorDialog"), base.transform.parent);
		yield return new WaitForSeconds(s);
		Hide();
	}

	private IEnumerator Succe(float s)
	{
		gameManager.homeScene.notebook.AddNewItem(trueCode);
		yield return new WaitForSeconds(s);
		Hide();
		passworddialog1.SetActive(value: false);
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}

	public void SetAnimation1()
	{
		SetAnimation(ani1);
	}

	public void SetAnimation2()
	{
		SetAnimation(ani2);
	}

	public void SetAnimation3()
	{
		SetAnimation(ani3);
	}

	public void SetAnimation(Transform ani)
	{
		int index = Random.Range(0, ani.childCount);
		Text component = ani.GetChild(index).GetComponent<Text>();
		int startIndex = Random.Range(0, chars.Length);
		component.GetComponent<I18NText>().updateTranslation2(chars.Substring(startIndex, 1));
		if (Random.Range(0, 10) > 8)
		{
			component.GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + component.text + "</color>");
		}
		else
		{
			component.GetComponent<I18NText>().updateTranslation2("<color=#717E84>" + component.text + "</color>");
		}
	}

	public void EndAnimation(Transform ani, int times)
	{
		for (int i = 0; i < ani.childCount; i++)
		{
			string text = ani.GetChild(i).GetComponent<Text>().text.Replace("<color=#ffffff>", "").Replace("</color>", "").Replace("<color=#717E84>", "");
			ani.GetChild(i).GetComponent<I18NText>().updateTranslation2("<color=#717E84>" + text + "</color>");
		}
		List<int> list = new List<int>();
		for (int j = 0; j < times; j++)
		{
			int num = Random.Range(0, ani.childCount);
			while (list.Contains(num))
			{
				num = Random.Range(0, ani.childCount);
			}
			list.Add(num);
			ani.GetChild(num).GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + pw[runPasswordIndex] + "</color>");
			runPasswordIndex++;
		}
	}
}
