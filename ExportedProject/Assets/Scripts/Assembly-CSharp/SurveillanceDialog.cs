using System.Collections;
using System.Collections.Generic;
using ExtraFoundation.Components;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using tnt_deploy;

public class SurveillanceDialog : CustomDialog
{
	public Text txt_time;

	public Transform mapGroup;

	public int period;

	public List<SurveillanceDot> surveillanceDotLists = new List<SurveillanceDot>();

	public GameObject content1;

	public GameObject content2;

	public GameObject enterDialog;

	public GameObject surloseDialog;

	public List<SurveillanceDot> lastdotlists = new List<SurveillanceDot>();

	public UILineRenderer2 lineRenderer2;

	public UILineRenderer lineRenderer;

	public Transform lineGroup;

	public Transform surveillanceitemGroup;

	public List<SurveillanItem> surveillanceitemlist = new List<SurveillanItem>();

	public Button btn_startsearch;

	public string currentid;

	public int[] piccounts = new int[3] { 5, 4, 3 };

	public int currentpiccount;

	public GameObject buttonGroup;

	public Button btn_add;

	public bool iscanclick = true;

	public bool iscancancle;

	public Image img_notclick;

	public DATA36 data36;

	public Image[] goalpics;

	private float CountDownTime = 3600f;

	public float GameTime;

	private float timer;

	public bool isstart;

	private List<Vector2> vecs = new List<Vector2>();

	public void ShowButtons()
	{
		if (gameManager.player.playerdata.itemlist.Contains(gameManager.dataManager.dic36[currentid].getitemids.Substring(1)))
		{
			InvokeRepeating("CloseButton", 0.1f, 0.01f);
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
		}
		if (iscanclick)
		{
			img_notclick.gameObject.SetActive(value: true);
			buttonGroup.SetActive(value: true);
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(buttonGroup.GetComponent<RectTransform>(), Input.mousePosition, gameManager.maincamera.GetComponent<Camera>(), out var worldPoint))
			{
				buttonGroup.transform.position = worldPoint;
			}
			InvokeRepeating("StartButton", 0.1f, 0.01f);
			iscanclick = false;
		}
	}

	public void AddPic()
	{
		InvokeRepeating("CloseButton", 0.1f, 0.01f);
		btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
		gameManager.homeScene.notebook.AddNewItems(gameManager.dataManager.dic36[currentid].getitemids.Substring(1).Split(';'));
	}

	private void StartButton()
	{
		Vector3 localScale = buttonGroup.GetComponent<RectTransform>().localScale;
		if (localScale.x >= 1f)
		{
			iscancancle = true;
			CancelInvoke();
		}
		else
		{
			buttonGroup.GetComponent<RectTransform>().localScale = new Vector3(localScale.x + 0.1f, localScale.y + 0.1f, 1f);
		}
	}

	public void CancelClick()
	{
		if (iscancancle)
		{
			img_notclick.gameObject.SetActive(value: false);
			InvokeRepeating("CloseButton", 0.1f, 0.01f);
			iscanclick = true;
		}
	}

	private void CloseButton()
	{
		Vector3 localScale = buttonGroup.GetComponent<RectTransform>().localScale;
		if (localScale.x <= 0f)
		{
			CancelInvoke();
			iscancancle = false;
			img_notclick.gameObject.SetActive(value: false);
			buttonGroup.SetActive(value: false);
		}
		else
		{
			buttonGroup.GetComponent<RectTransform>().localScale = new Vector3(localScale.x - 0.1f, localScale.y - 0.1f, 1f);
		}
	}

	private void Start()
	{
	}

	private void InitContent1()
	{
		List<DATA36> list36 = gameManager.dataManager.GetShowSurveillanceItems(gameManager.player.GetEventId());
		for (int i = 0; i < list36.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("surveillanceitem"), surveillanceitemGroup);
			gameObject.GetComponent<SurveillanItem>().Init(list36[i], this);
			surveillanceitemlist.Add(gameObject.GetComponent<SurveillanItem>());
			if (i == 0)
			{
				currentid = list36[i].ID.ToString();
				gameObject.GetComponent<SurveillanItem>().Click();
				if (gameObject.GetComponent<SurveillanItem>().piccount > 1)
				{
					currentpiccount = piccounts[gameObject.GetComponent<SurveillanItem>().piccount - 1];
				}
			}
		}
		btn_startsearch.onClick.AddListener(delegate
		{
			if (list36.Count > 0)
			{
				StartCoroutine(EnterSystem());
			}
		});
	}

	private IEnumerator EnterSystem()
	{
		enterDialog.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		content1.SetActive(value: false);
		enterDialog.gameObject.SetActive(value: false);
		content2.SetActive(value: true);
		if (gameManager.player.playerdata.surveillancelist.ContainsKey(currentid))
		{
			string[] array = gameManager.dataManager.dic36[currentid].searchcontent.Split(';');
			List<Vector2> list = gameManager.player.playerdata.surveillancelist[currentid];
			for (int i = 0; i < 5; i++)
			{
				AddNewDot2(i + 1, list[i], array[i]);
			}
			period = 5;
			iscanclick = true;
		}
		else
		{
			isstart = true;
			AddDot(currentpiccount);
		}
		string[] array2 = gameManager.dataManager.dic36[currentid].itemids.Substring(1).Split(';');
		for (int j = 0; j < array2.Length; j++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(array2[j]) || gameManager.isbug)
			{
				Sprite sprite = Resources.Load<Sprite>("Image/" + gameManager.dataManager.dic1[array2[j]].image);
				goalpics[j].sprite = sprite;
				if (sprite.rect.width > sprite.rect.height)
				{
					goalpics[j].GetComponent<RectTransform>().sizeDelta = new Vector2(68f / sprite.rect.height * sprite.rect.width, 67f);
				}
				else
				{
					goalpics[j].GetComponent<RectTransform>().sizeDelta = new Vector2(68f, 67f / sprite.rect.width * sprite.rect.height);
				}
			}
			else
			{
				goalpics[j].transform.parent.gameObject.SetActive(value: false);
			}
		}
		for (int k = 0; k < 3 - array2.Length; k++)
		{
			goalpics[k + array2.Length].gameObject.SetActive(value: false);
		}
	}

	public void OtherItemCancel(string currentid, int piccount)
	{
		this.currentid = currentid;
		if (piccount >= 0)
		{
			currentpiccount = piccounts[piccount - 1];
		}
		for (int i = 0; i < surveillanceitemlist.Count; i++)
		{
			if (!surveillanceitemlist[i].id.Equals(currentid))
			{
				surveillanceitemlist[i].CancelClick();
			}
		}
	}

	private void Update()
	{
		if (!isstart)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer >= 1f / 60f)
		{
			timer = 0f;
			GameTime -= 1f;
			txt_time.GetComponent<I18NText>().updateTranslation2((GameTime / 60f).ToString("#0.00"));
			if (GameTime <= 0f)
			{
				isstart = false;
				GameOver();
			}
		}
	}

	public void CloseDialog()
	{
		surloseDialog.SetActive(value: true);
		Close();
	}

	private void GameOver()
	{
		surloseDialog.SetActive(value: true);
	}

	public void AddDot(int count)
	{
		string[] array = gameManager.dataManager.dic36[currentid].searchcontent.Split(';');
		for (int i = 0; i < lineGroup.childCount; i++)
		{
			Object.Destroy(lineGroup.GetChild(i).gameObject);
		}
		for (int j = 0; j < surveillanceDotLists.Count; j++)
		{
			if (surveillanceDotLists[j].status != 3)
			{
				Object.Destroy(surveillanceDotLists[j].gameObject);
				surveillanceDotLists.RemoveAt(j);
			}
		}
		surveillanceDotLists.Clear();
		if (period >= 5)
		{
			iscanclick = true;
			isstart = false;
			gameManager.player.playerdata.surveillanceRecord.Add(currentid, vecs);
			return;
		}
		int num = Random.Range(0, count);
		string text = "01234";
		for (int k = 0; k < count; k++)
		{
			int startIndex = Random.Range(0, text.Length);
			AddNewDot(k + 1, int.Parse(text.Substring(startIndex, 1)), num == k, array[period]);
			text.Remove(startIndex, 1);
		}
		period++;
	}

	public void OtherDotCancleClick(int currentpos)
	{
		for (int i = 0; i < surveillanceDotLists.Count; i++)
		{
			if (surveillanceDotLists[i].pos != currentpos)
			{
				surveillanceDotLists[i].CancelClick();
			}
		}
	}

	private void AddNewDot(int pos, int offy, bool iscorrect, string key)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("surveillance_dot"), mapGroup);
		int num = 0;
		int num2 = 0;
		switch (period)
		{
		case 0:
			num = Random.Range(-431, -280);
			break;
		case 1:
			num = Random.Range(-223, -106);
			break;
		case 2:
			num = Random.Range(-43, 103);
			break;
		case 3:
			num = Random.Range(156, 267);
			break;
		case 4:
			num = Random.Range(314, 425);
			break;
		}
		num2 = Random.Range(-158 + offy * 60, -158 + offy * 60 + 60);
		gameObject.GetComponent<RectTransform>().localPosition = new Vector2(num, num2);
		gameObject.GetComponent<SurveillanceDot>().Init(pos, iscorrect, key, this);
		surveillanceDotLists.Add(gameObject.GetComponent<SurveillanceDot>());
		if (lastdotlists.Count > 0)
		{
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load("virlineRenerder"), lineGroup);
			List<Vector2> list = new List<Vector2>
			{
				lastdotlists[lastdotlists.Count - 1].transform.localPosition,
				new Vector2(num, num2)
			};
			obj.GetComponent<UILineRenderer>().Points = list.ToArray();
		}
	}

	private void AddNewDot2(int pos, Vector2 vector2, string key)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("surveillance_dot"), mapGroup);
		gameObject.GetComponent<RectTransform>().localPosition = vector2;
		gameObject.GetComponent<SurveillanceDot>().Init(pos, iscorrect: true, key, this, 3);
		surveillanceDotLists.Add(gameObject.GetComponent<SurveillanceDot>());
		lastdotlists.Add(gameObject.GetComponent<SurveillanceDot>());
		DrawLine();
	}

	public void RemoveDot(int pos, bool ishide)
	{
		surveillanceDotLists[pos - 1].RemoveDot(ishide);
		if (lineGroup.childCount >= pos)
		{
			lineGroup.GetChild(pos - 1).gameObject.SetActive(value: false);
		}
	}

	public void DrawLine()
	{
		vecs.Clear();
		for (int i = 0; i < lastdotlists.Count; i++)
		{
			vecs.Add(lastdotlists[i].transform.localPosition);
		}
		lineRenderer.Points = vecs.ToArray();
	}

	public void ChangeDot(int pos)
	{
		if (lastdotlists.Count > 0)
		{
			lastdotlists[lastdotlists.Count - 1].ChangeSureDot();
		}
		lastdotlists.Add(surveillanceDotLists[pos - 1]);
		surveillanceDotLists[pos - 1].ChangeDot();
		if (lastdotlists.Count >= 2)
		{
			DrawLine();
		}
		if (period >= 5)
		{
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < lastdotlists.Count; i++)
			{
				lastdotlists[i].ShowItemContent();
				list.Add(lastdotlists[i].transform.localPosition);
			}
			gameManager.player.playerdata.surveillancelist.Add(currentid, list);
		}
		AddDot(currentpiccount);
	}

	public void OtherDotRemoveDot(int currentpos)
	{
		for (int i = 0; i < surveillanceDotLists.Count; i++)
		{
			if (surveillanceDotLists[i].pos != currentpos)
			{
				surveillanceDotLists[i].RemoveDot(ishide: true);
			}
		}
	}

	public void ShowSurSearchingDialog(int pos, bool iscorrect)
	{
		((GameObject)Object.Instantiate(Resources.Load("sursearchingDialog"), content2.transform)).GetComponent<SurSearchingDialog>().StartSearch(pos, iscorrect, this);
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		InitContent1();
		GameTime = CountDownTime;
	}
}
