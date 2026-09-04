using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class GameDialog : CustomDialog
{
	public int count;

	public List<string> inputlist;

	public Transform img_mask;

	public Text[] texts;

	private float CountDownTime = 600f;

	private float GameTime;

	private float timer;

	public Text GameCountTimeText;

	public GameObject startPanel;

	public GameObject gamePanel;

	public Button btn_start;

	public Animator codeAni;

	public bool isstart;

	public Text txt_result;

	public GameObject resultPanel;

	public Button btn_sure;

	public Text resultLabel;

	private void Start()
	{
		GameTime = CountDownTime;
		inputlist = new List<string>();
		btn_start.onClick.AddListener(delegate
		{
			startPanel.SetActive(value: false);
			InvokeRepeating("AddItem", 0.1f, 0.5f);
			isstart = true;
			codeAni.Play("ani_code");
		});
	}

	public void Add(string s)
	{
		inputlist.Add(s);
	}

	public void AddItem()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("gametextitem"), img_mask);
		int num = Random.Range(-383, 390);
		gameObject.transform.localPosition = new Vector2(num, gameObject.transform.localPosition.y);
	}

	private void Update()
	{
		int num = (int)(GameTime / 60f);
		float num2 = GameTime % 60f;
		if (isstart)
		{
			timer += Time.deltaTime;
			if (timer >= 1f / 60f)
			{
				timer = 0f;
				GameTime -= 1f;
				GameCountTimeText.GetComponent<I18NText>().updateTranslation2($"{num:00}" + ":" + $"{num2:00}");
				if (GameTime <= 0f)
				{
					GameCountTimeText.GetComponent<I18NText>().updateTranslation2("00:00");
					isstart = false;
					resultPanel.SetActive(value: true);
					txt_result.GetComponent<I18NText>().updateTranslation2("<color=\"#fd382b\">" + I18N.instance.getValue("^game05") + "</color>");
					resultLabel.GetComponent<I18NText>().updateTranslation2("^result_failedLabel");
				}
			}
		}
		InitInput(KeyCode.A, "A");
		InitInput(KeyCode.B, "B");
		InitInput(KeyCode.C, "C");
		InitInput(KeyCode.D, "D");
		InitInput(KeyCode.E, "E");
		InitInput(KeyCode.F, "F");
		InitInput(KeyCode.G, "G");
		InitInput(KeyCode.H, "H");
		InitInput(KeyCode.I, "I");
		InitInput(KeyCode.J, "J");
		InitInput(KeyCode.K, "K");
		InitInput(KeyCode.L, "L");
		InitInput(KeyCode.M, "M");
		InitInput(KeyCode.N, "N");
		InitInput(KeyCode.O, "O");
		InitInput(KeyCode.P, "P");
		InitInput(KeyCode.Q, "Q");
		InitInput(KeyCode.R, "R");
		InitInput(KeyCode.S, "S");
		InitInput(KeyCode.T, "T");
		InitInput(KeyCode.U, "U");
		InitInput(KeyCode.V, "V");
		InitInput(KeyCode.W, "W");
		InitInput(KeyCode.X, "X");
		InitInput(KeyCode.Y, "Y");
		InitInput(KeyCode.Z, "Z");
	}

	private void InitInput(KeyCode keycode, string r)
	{
		if (!Input.GetKeyDown(keycode) || !isstart || inputlist.Count <= 0 || !inputlist.Contains(r))
		{
			return;
		}
		if (count < texts.Length)
		{
			texts[count].GetComponent<I18NText>().updateTranslation2(r);
			texts[count].transform.GetChild(0).gameObject.SetActive(value: false);
		}
		count++;
		RemoveS(r, isdestroy: true);
		if (count >= texts.Length)
		{
			isstart = false;
			resultPanel.SetActive(value: true);
			txt_result.GetComponent<I18NText>().updateTranslation2("<color=\"#78f86e\">" + I18N.instance.getValue("^game04") + "</color>");
			resultLabel.GetComponent<I18NText>().updateTranslation2("^result_succLabel");
			codeAni.Play("Empty");
			CancelInvoke();
			for (int i = 0; i < inputlist.Count; i++)
			{
				RemoveS(inputlist[i], isdestroy: true);
			}
		}
	}

	public void RemoveS(string r, bool isdestroy)
	{
		if (inputlist.Contains(r))
		{
			inputlist.Remove(r);
		}
		if (isdestroy)
		{
			GameObject gameObject = img_mask.Find(r).gameObject;
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
