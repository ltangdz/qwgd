using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunDataDialog : MonoBehaviour
{
	public GameObject analysis;

	public Text mapName;

	public Text zVal;

	public Text fVal;

	public Text noneVal;

	public List<Image> redList;

	public List<Image> greenList;

	public Transform content;

	public YulunDialog yulunDialog;

	private GameManager gameManager;

	private YulunMap parObj;

	private List<YulunTipList> yulunTipList = new List<YulunTipList>();

	[SerializeField]
	private List<Image> greenTipList = new List<Image>();

	[SerializeField]
	private List<Image> redTipList = new List<Image>();

	private bool redRunning;

	private bool greenRunning;

	[SerializeField]
	private int zvalue;

	[SerializeField]
	private int fvalue;

	[SerializeField]
	private int nonevalue;

	public void Show()
	{
		GetComponent<RectTransform>().DOLocalMoveY(-427.5f, 0.5f);
	}

	public void Init(YulunMap par, GameManager gm)
	{
		parObj = par;
		gameManager = gm;
		SetData();
	}

	public void SetData()
	{
		mapName.GetComponent<I18NText>().updateTranslation2(parObj.mapName);
		zvalue = (int)parObj.zPerson;
		fvalue = (int)parObj.fPerson;
		nonevalue = (int)((float)parObj.allPerson - parObj.zPerson - parObj.fPerson);
		zVal.text = zvalue.ToString();
		fVal.text = fvalue.ToString();
		noneVal.text = nonevalue.ToString();
		for (int i = 0; i < content.childCount; i++)
		{
			Object.Destroy(content.GetChild(i).gameObject);
		}
		this.yulunTipList.Clear();
		for (int j = 0; j < parObj.penziList.Count; j++)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/tiplist"), content);
			YulunTipList yulunTipList = yulunDialog.yulunPenziDialog.tipList[parObj.penziList[j]];
			this.yulunTipList.Add(yulunTipList);
			gameObject.transform.Find("Image").GetComponent<Image>().sprite = yulunTipList.icon.sprite;
			if (yulunTipList.tipName.text.Length >= 12)
			{
				gameObject.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(yulunTipList.tipName.text.Substring(0, 12) + "...");
			}
			else
			{
				gameObject.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(yulunTipList.tipName.text);
			}
		}
		StartCoroutine(SetRedPoint(0f));
		StartCoroutine(SetGreenPoint(0f));
	}

	private void InitData()
	{
		DOTween.To(() => zvalue, delegate(int x)
		{
			zvalue = x;
		}, (int)parObj.zPerson, yulunDialog.changeTime).OnUpdate(delegate
		{
			zVal.text = zvalue.ToString();
		}).OnComplete(delegate
		{
			zVal.text = ((int)parObj.zPerson).ToString();
		});
		DOTween.To(() => fvalue, delegate(int x)
		{
			fvalue = x;
		}, (int)parObj.fPerson, yulunDialog.changeTime).OnUpdate(delegate
		{
			fVal.text = fvalue.ToString();
		}).OnComplete(delegate
		{
			fVal.text = ((int)parObj.fPerson).ToString();
		});
		DOTween.To(() => nonevalue, delegate(int x)
		{
			nonevalue = x;
		}, (int)((float)parObj.allPerson - parObj.zPerson - parObj.fPerson), yulunDialog.changeTime).OnUpdate(delegate
		{
			noneVal.text = nonevalue.ToString();
		}).OnComplete(delegate
		{
			noneVal.text = ((int)((float)parObj.allPerson - parObj.zPerson - parObj.fPerson)).ToString();
		});
	}

	public void ChangeVal()
	{
		FreshVal("z");
		FreshVal("f");
	}

	private void FreshVal(string type)
	{
		if (!redRunning && type == "f")
		{
			redRunning = true;
			InitData();
			StartCoroutine(SetRedPoint(yulunDialog.changeTime));
		}
		if (!greenRunning && type == "z")
		{
			greenRunning = true;
			InitData();
			StartCoroutine(SetGreenPoint(yulunDialog.changeTime));
		}
	}

	private IEnumerator SetRedPoint(float time)
	{
		float num = Mathf.Round(parObj.fPerson / (float)parObj.allPerson * (float)redList.Count);
		float times = num - (float)redTipList.Count;
		for (int i = 0; (float)i < Mathf.Abs(times); i++)
		{
			yield return new WaitForSeconds(time / Mathf.Abs(times));
			if (times > 0f)
			{
				if (redTipList.Count + 1 <= redList.Count)
				{
					redList[redList.Count - redTipList.Count - 1].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
					redTipList.Add(redList[redList.Count - redTipList.Count - 1]);
				}
			}
			else if (redTipList.Count <= redList.Count && redTipList.Count >= 1)
			{
				redList[redList.Count - redTipList.Count].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
				redTipList.Remove(redTipList[redTipList.Count - 1]);
			}
		}
		redRunning = false;
	}

	private IEnumerator SetGreenPoint(float time)
	{
		float num = Mathf.Round(parObj.zPerson / (float)parObj.allPerson * (float)greenList.Count);
		float times = num - (float)greenTipList.Count;
		for (int i = 0; (float)i < Mathf.Abs(times); i++)
		{
			yield return new WaitForSeconds(time / Mathf.Abs(times));
			if (times > 0f)
			{
				if (greenTipList.Count < greenList.Count)
				{
					greenList[greenTipList.Count].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
					greenTipList.Add(greenList[greenTipList.Count]);
				}
			}
			else if (greenTipList.Count <= greenList.Count && greenTipList.Count >= 1)
			{
				greenList[greenTipList.Count - 1].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
				greenTipList.Remove(greenTipList[greenTipList.Count - 1]);
			}
		}
		greenRunning = false;
	}
}
