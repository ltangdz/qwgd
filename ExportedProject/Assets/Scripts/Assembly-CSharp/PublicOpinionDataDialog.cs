using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionDataDialog : MonoBehaviour
{
	public GameObject analysis;

	public Text mapName;

	public Text zVal;

	public Text fVal;

	public Text noneVal;

	public List<Image> redList;

	public List<Image> greenList;

	public Transform content;

	private PublicOpinionController _controller;

	private PublicOpinionMap _curMap;

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

	private Color _color = new Color(1f, 1f, 1f, 1f);

	private Color _alphaColor = new Color(1f, 1f, 1f, 0f);

	public void Show()
	{
		GetComponent<RectTransform>().DOLocalMoveY(-427.5f, 0.5f);
	}

	public void Init(PublicOpinionController controller)
	{
		_controller = controller;
	}

	public void CountAll()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		List<PublicOpinionMap> maps = _controller.maps;
		for (int i = 0; i < maps.Count; i++)
		{
			PublicOpinionMap publicOpinionMap = maps[i];
			num3 += (int)publicOpinionMap.personTotal;
			num2 += (int)publicOpinionMap.TempNegativePersons;
			num += (int)publicOpinionMap.TempPositivePersons;
		}
		float progress = (float)num2 * 1f / (float)num3;
		_controller.Progress(progress);
	}

	private void FixedUpdate()
	{
		if (!_controller || !_controller.isBalancing)
		{
			return;
		}
		CountAll();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (_curMap == null)
		{
			List<PublicOpinionMap> maps = _controller.maps;
			for (int i = 0; i < maps.Count; i++)
			{
				PublicOpinionMap publicOpinionMap = maps[i];
				num3 += (int)publicOpinionMap.personTotal;
				num2 += (int)publicOpinionMap.TempNegativePersons;
				num += (int)publicOpinionMap.TempPositivePersons;
			}
		}
		else
		{
			num = _curMap.TempPositivePersons;
			num2 = _curMap.TempNegativePersons;
			num3 = _curMap.personTotal;
		}
		zVal.text = num.ToString();
		fVal.text = num2.ToString();
		noneVal.text = $"{num3 - num2 - num}";
		SetRedPoint();
		SetGreenPoint();
	}

	public void SetData()
	{
		if (_curMap == null)
		{
			mapName.GetComponent<I18NText>().updateTranslation2("ALL");
		}
		else
		{
			mapName.GetComponent<I18NText>().updateTranslation2(_curMap.mapName);
		}
		for (int i = 0; i < content.childCount; i++)
		{
			Object.Destroy(content.GetChild(i).gameObject);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (_curMap == null)
		{
			List<PublicOpinionMap> maps = _controller.maps;
			for (int j = 0; j < maps.Count; j++)
			{
				PublicOpinionMap publicOpinionMap = maps[j];
				num3 += (int)publicOpinionMap.personTotal;
				num2 += (int)publicOpinionMap.TempNegativePersons;
				num += (int)publicOpinionMap.TempPositivePersons;
			}
		}
		else
		{
			num = _curMap.TempPositivePersons;
			num2 = _curMap.TempNegativePersons;
			num3 = _curMap.personTotal;
			for (int k = 0; k < _curMap.trollList.Count; k++)
			{
				GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/tiplist"), content);
				YulunTipList yulunTipList = _controller.trollDialog.tipList[_curMap.trollList[k]];
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
		}
		zvalue = num;
		fvalue = num2;
		nonevalue = num3 - zvalue - fvalue;
		zVal.text = zvalue.ToString();
		fVal.text = fvalue.ToString();
		noneVal.text = nonevalue.ToString();
		this.yulunTipList.Clear();
		CountAll();
		SetRedPoint();
		SetGreenPoint();
	}

	private void SetRedPoint()
	{
		float num = 0f;
		int num2 = 0;
		if (_curMap == null)
		{
			List<PublicOpinionMap> maps = _controller.maps;
			for (int i = 0; i < maps.Count; i++)
			{
				PublicOpinionMap publicOpinionMap = maps[i];
				num += (float)(int)publicOpinionMap.personTotal;
				num2 += (int)publicOpinionMap.TempNegativePersons;
			}
		}
		else
		{
			num = (float)(int)_curMap.personTotal * 1f;
			num2 = _curMap.TempNegativePersons;
		}
		float num3 = Mathf.Round((float)num2 / num * (float)redList.Count) - (float)redTipList.Count;
		for (int j = 0; (float)j < Mathf.Abs(num3); j++)
		{
			if (num3 > 0f)
			{
				if (redTipList.Count + 1 <= redList.Count)
				{
					redList[redList.Count - redTipList.Count - 1].color = _color;
					redTipList.Add(redList[redList.Count - redTipList.Count - 1]);
				}
			}
			else if (redTipList.Count <= redList.Count && redTipList.Count >= 1)
			{
				redList[redList.Count - redTipList.Count].color = _alphaColor;
				redTipList.Remove(redTipList[redTipList.Count - 1]);
			}
		}
		redRunning = false;
	}

	private void SetGreenPoint()
	{
		float num = 0f;
		int num2 = 0;
		if (_curMap == null)
		{
			List<PublicOpinionMap> maps = _controller.maps;
			for (int i = 0; i < maps.Count; i++)
			{
				PublicOpinionMap publicOpinionMap = maps[i];
				num += (float)(int)publicOpinionMap.personTotal;
				num2 += (int)publicOpinionMap.TempPositivePersons;
			}
		}
		else
		{
			num = (float)(int)_curMap.personTotal * 1f;
			num2 = _curMap.TempPositivePersons;
		}
		float num3 = Mathf.Round((float)num2 / num * (float)greenList.Count) - (float)greenTipList.Count;
		for (int j = 0; (float)j < Mathf.Abs(num3); j++)
		{
			if (num3 > 0f)
			{
				if (greenTipList.Count < greenList.Count)
				{
					greenList[greenTipList.Count].color = new Color(1f, 1f, 1f, 1f);
					greenTipList.Add(greenList[greenTipList.Count]);
				}
			}
			else if (greenTipList.Count <= greenList.Count && greenTipList.Count >= 1)
			{
				greenList[greenTipList.Count - 1].color = new Color(1f, 1f, 1f, 0f);
				greenTipList.Remove(greenTipList[greenTipList.Count - 1]);
			}
		}
		greenRunning = false;
	}

	public void ShowData(PublicOpinionMap curMap)
	{
		_curMap = curMap;
		SetData();
	}
}
