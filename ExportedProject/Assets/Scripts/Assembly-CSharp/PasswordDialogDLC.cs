using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PasswordDialogDLC : PasswordDialog1
{
	public Text _nameText;

	public Text _socialnumText;

	public Text _nicknameText;

	public Text _birthText;

	public Text _phoneText;

	public Text _homeText;

	private void Start()
	{
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

	public new void SetItem(DATA1 data)
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

	public new void MinusItem(DATA1 data)
	{
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
		progressBar.GetComponent<RectTransform>().DOSizeDelta(new Vector2(x, 34f), 0.1f).OnComplete(delegate
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

	public new void OpenBtnFun(int itemType, float val)
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

	public new void SetText(int pos)
	{
		txt_dragtexts[pos].StartEffect(I18N.instance.getValue("^password_tip01"));
	}

	public new void BeginGame()
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

	public new IEnumerator StartAnimation()
	{
		GetComponent<Animator>().Play("ani_passdialog1");
		yield return new WaitForSeconds(0.1f);
		txt_title.StartEffect("PASSWORD");
		yield return new WaitForSeconds(0.1f);
		txt_title2.StartEffect(I18N.instance.getValue("^txt_pwtitle"));
	}
}
