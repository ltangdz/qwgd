using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PasswordDialog1 : CustomDialog
{
	public Image[] img_lightitems;

	public TypewriterEffect txt_title;

	public TypewriterEffect txt_title2;

	public PasswordDialog2 passworddialog2;

	public Button btn_go;

	public TypewriterEffect[] txt_dragtexts;

	public Image progressBar;

	public Text progressPersent;

	public float barWidth;

	public bool inputType;

	public GameObject imgDragArea;

	protected List<int> btnIfOpen = new List<int>();

	protected Dictionary<string, Dictionary<string, float>> totalVal = new Dictionary<string, Dictionary<string, float>>();

	protected string breakCodeVal;

	protected string breakCodeNum;

	public PasswordItem[] _pwdItems;

	private List<DATA1> _items = new List<DATA1>();

	private void Start()
	{
		_items.Clear();
		StartCoroutine(StartAnimation());
		btn_go.onClick.AddListener(delegate
		{
			StartCrack();
		});
		btn_close.onClick.AddListener(delegate
		{
			if (passworddialog2 != null)
			{
				UnityEngine.Object.Destroy(passworddialog2.gameObject);
			}
		});
		gameManager.homeScene.passworddialog1 = this;
	}

	public void InputEnd(int inputType)
	{
		ValidInput();
		Debug.Log("inputType");
	}

	private void ValidInput()
	{
		Dictionary<string, List<DATA1>> eventPasswordItem = gameManager.dataManager.getEventPasswordItem(gameManager.player.GetEventId());
		string key = "";
		List<DATA1> list = new List<DATA1>();
		int num = 0;
		for (int i = 0; i < eventPasswordItem.Keys.Count; i++)
		{
			string text = eventPasswordItem.Keys.ElementAt(i);
			Debug.Log("key:" + text);
			List<DATA1> list2 = eventPasswordItem[text];
			List<DATA1> list3 = new List<DATA1>();
			for (int j = 0; j < _pwdItems.Length; j++)
			{
				string text2 = _pwdItems[j].inputField.text.Trim().ToLower();
				if (text2 == "")
				{
					continue;
				}
				for (int k = 0; k < list2.Count; k++)
				{
					DATA1 dATA = list2[k];
					int passwordnumber = dATA.passwordnumber;
					if (j == passwordnumber - 1)
					{
						DATA1 dATA2 = dATA;
						if (I18N.instance.getValue(dATA2.message).ToLower().Trim() == text2)
						{
							Debug.Log(text2 + "相同");
							list3.Add(dATA);
						}
					}
				}
			}
			if (list3.Count > list.Count)
			{
				key = list2[0].name.ToLower().Trim();
				list = list3;
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			num += int.Parse(list[l].percent.Substring(1));
		}
		totalVal = new Dictionary<string, Dictionary<string, float>> { 
		{
			key,
			new Dictionary<string, float> { { "percent", num } }
		} };
		if (list.Count > 0)
		{
			totalVal[key]["id"] = list[0].ID;
		}
		RefreshProBar();
	}

	public void SetItem(DATA1 data)
	{
		AddVal(data);
		if (data.ID.ToString() == "10462")
		{
			AddVal(gameManager.dataManager.dic1["10645"]);
		}
		else if (data.ID.ToString() == "10645")
		{
			AddVal(gameManager.dataManager.dic1["10462"]);
		}
		gameManager.soundManager.PlaySound(21);
		RefreshProBar();
	}

	private void AddVal(DATA1 data)
	{
		_items.Add(data);
		string text = data.name.Trim().ToLower().Replace(" ", "");
		if (!totalVal.ContainsKey(text))
		{
			Dictionary<string, float> dictionary = new Dictionary<string, float>();
			dictionary.Add("id", 0f);
			dictionary.Add("percent", 0f);
			totalVal.Add(text, dictionary);
		}
		float num = float.Parse(data.percent.Substring(1));
		Debug.Log(data.ID + " " + num + " " + text);
		totalVal[text]["percent"] += num;
		totalVal[text]["id"] = data.ID;
	}

	public void MinusItem(DATA1 data)
	{
		_items.Remove(data);
		string key = data.name.Trim().ToLower().Replace(" ", "");
		float num = float.Parse(data.percent.Substring(1));
		totalVal[key]["percent"] -= num;
		RefreshProBar();
	}

	private void RefreshProBar()
	{
		float num = 0f;
		float num2 = 0f;
		foreach (KeyValuePair<string, Dictionary<string, float>> item in totalVal)
		{
			num2 = ((item.Value["percent"] > num) ? item.Value["id"] : num2);
			num = ((item.Value["percent"] > num) ? item.Value["percent"] : num);
		}
		num = Math.Min(num, 100f);
		float x = barWidth * (num / 100f);
		float y = progressBar.GetComponentInParent<RectTransform>().sizeDelta.y;
		if (gameManager.Is_Dlc6())
		{
			y = 30f;
		}
		if (gameManager.Is_Dlc7())
		{
			y = 20f;
		}
		progressBar.GetComponent<RectTransform>().DOSizeDelta(new Vector2(x, y), 0.1f).OnComplete(delegate
		{
			inputType = false;
		});
		progressPersent.GetComponent<I18NText>().updateTranslation2(num + "%");
		if (num == 100f)
		{
			string text = gameManager.dataManager.dic1[num2.ToString()].passwordID.Substring(1);
			breakCodeVal = I18N.instance.getValue(gameManager.dataManager.dic1[text].message);
			breakCodeNum = text.ToString();
		}
		else
		{
			breakCodeVal = "";
			breakCodeNum = "";
		}
	}

	public void OpenBtnFun(int itemType, float val)
	{
		if (val == -1f && btnIfOpen.Contains(itemType))
		{
			btnIfOpen.Remove(itemType);
		}
		else if (val == 1f && !btnIfOpen.Contains(itemType))
		{
			btnIfOpen.Add(itemType);
		}
		if (btnIfOpen.Count == 0)
		{
			btn_go.interactable = false;
		}
		else
		{
			btn_go.interactable = true;
		}
	}

	private void StartCrack()
	{
		Debug.Log("可不可以点击" + !inputType);
		if (!inputType && (!(passworddialog2 != null) || passworddialog2.passEnd))
		{
			if (passworddialog2 != null)
			{
				UnityEngine.Object.Destroy(passworddialog2.gameObject);
			}
			gameManager.player.playerdata.UseSocialMethod(3);
			ComputResult();
		}
	}

	private void ComputResult()
	{
		passworddialog2 = UnityEngine.Object.Instantiate(Resources.Load<PasswordDialog2>(DLCNameUtil.Instance.GetPasswordDialog2Name()), base.transform.parent);
		passworddialog2.GetComponent<PasswordDialog2>().pw = breakCodeVal;
		passworddialog2.GetComponent<PasswordDialog2>().trueCode = breakCodeNum;
		passworddialog2.GetComponent<PasswordDialog2>().gameManager = gameManager;
		passworddialog2.GetComponent<PasswordDialog2>().passworddialog1 = base.gameObject;
		passworddialog2.GetComponent<PasswordDialog2>().Show();
	}

	public void SetText(int pos)
	{
		txt_dragtexts[pos].StartEffect(I18N.instance.getValue("^password_tip01"));
	}

	public void BeginGame()
	{
		((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Dialog/gameDialog"), base.transform.parent)).GetComponent<GameDialog>().Show();
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		StartCoroutine(StartAnimation());
	}

	public IEnumerator StartAnimation()
	{
		GetComponent<Animator>().Play("ani_passdialog1");
		yield return new WaitForSeconds(0.1f);
		txt_title.StartEffect("PASSWORD");
		yield return new WaitForSeconds(0.1f);
		txt_title2.StartEffect(I18N.instance.getValue("^txt_pwtitle"));
	}

	private void ChangePlayer(string player_id)
	{
	}

	private void OnEnable()
	{
		if (gameManager.Is_Dlc6())
		{
			NoteDragManager.Instance.onChangePlayer += ChangePlayer;
		}
	}

	private void OnDisable()
	{
		if (gameManager.Is_Dlc6())
		{
			NoteDragManager.Instance.onChangePlayer -= ChangePlayer;
		}
	}
}
