using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhishingLink : CustomDialog
{
	public Sprite[] loadImg;

	public Sprite[] loadType;

	public Color[] color1;

	public Color[] color2;

	public Image imgLoadType;

	public Text txtGeting;

	public Text txtEng;

	public Transform loadBox;

	public Button againBtn;

	public Text title;

	private float loadStep;

	private bool runing;

	private float virType;

	private string nameID;

	private string[] searchFile;

	public void Init(bool ifScs, float type, string id, string[] file)
	{
		nameID = id;
		virType = type;
		searchFile = file;
		runing = true;
		StartCoroutine(InitLabel(type));
		StartCoroutine(Pishing(ifScs));
	}

	private IEnumerator Pishing(bool ifScs)
	{
		float all = loadBox.childCount;
		float len = Mathf.Round(Random.Range(3, 5));
		for (int i = 1; (float)i <= len; i++)
		{
			float runVal = (((float)i == len) ? all : Mathf.Round(Random.Range(all / len * (float)(i - 1), all / len * (float)i)));
			if (!ifScs && (float)i >= len * 0.6f)
			{
				PishFailed();
				StopAllCoroutines();
				break;
			}
			StartCoroutine(RunTo(runVal));
			float seconds = Mathf.Round(Random.Range(1f, 3f));
			yield return new WaitForSeconds(seconds);
		}
	}

	private IEnumerator InitLabel(float type)
	{
		string key = ((type == 0f) ? "^txt_fishdialog8-4" : "^txt_fishdialog8-3");
		string getLabel = ((type == 0f) ? "^txt_fishdialog8-1" : "^txt_fishdialog8-2");
		title.GetComponent<I18NText>().updateTranslation2(key);
		string aa = "";
		while (runing)
		{
			txtGeting.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(getLabel) + aa);
			aa = ((aa == "...") ? "" : (aa + "."));
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator RunTo(float runVal)
	{
		for (int i = (int)loadStep; (float)i < runVal; i++)
		{
			loadBox.GetChild(i).gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.01f);
		}
		loadStep = runVal;
		if (runVal == (float)loadBox.childCount)
		{
			runing = false;
			txtGeting.GetComponent<I18NText>().updateTranslation2("^txt_fishdialog9");
			imgLoadType.sprite = loadType[2];
			yield return new WaitForSeconds(1f);
			if (virType == 0f)
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/pish_cmp"), gameManager.homeScene.transform);
				obj.GetComponent<PishCmp>().Init(nameID, searchFile);
				obj.GetComponent<PishCmp>().Show();
			}
			else
			{
				GameObject obj2 = Object.Instantiate(Resources.Load<GameObject>("Dialog/pish_phone"), gameManager.homeScene.transform);
				obj2.GetComponent<PhishPhone>().Init(nameID, searchFile);
				obj2.GetComponent<PhishPhone>().Show();
			}
			Hide();
		}
	}

	private void PishFailed()
	{
		runing = false;
		txtGeting.GetComponent<I18NText>().updateTranslation2("^txt_fishdialog10");
		imgLoadType.sprite = loadType[1];
		againBtn.gameObject.SetActive(value: true);
		againBtn.onClick.AddListener(delegate
		{
			gameManager.homeScene.phishing.gameObject.SetActive(value: true);
			Hide();
		});
		txtGeting.color = color1[1];
		txtEng.color = color2[1];
		for (int num = 0; (float)num < loadStep; num++)
		{
			loadBox.GetChild(num).GetComponent<Image>().sprite = loadImg[1];
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
