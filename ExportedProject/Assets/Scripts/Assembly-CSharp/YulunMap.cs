using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YulunMap : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public string mapName;

	public float zPerson;

	public float fPerson;

	public long allPerson;

	public List<int> penziList;

	public YulunDialog yulunDialog;

	public Image shadow;

	public Image tu;

	public List<GameObject> pointList;

	public Sprite redPoint;

	public Sprite greenPoint;

	public float waitTime;

	public List<YulunTipList> yulunPenziList;

	private GameManager gameManager;

	[SerializeField]
	private bool choiced;

	private int zPercent;

	private int fPercent;

	private List<GameObject> zPercentList = new List<GameObject>();

	private List<GameObject> fPercentList = new List<GameObject>();

	private int onePointToPerson;

	private Coroutine redPointRun;

	private Coroutine greenPointRun;

	private bool calling;

	private List<YulunNewsInfo> calList = new List<YulunNewsInfo>();

	private void Start()
	{
		Invoke("Init", 4f);
	}

	private void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		yulunDialog.allPerson += allPerson;
		yulunDialog.zAllPerson += (long)zPerson;
		yulunDialog.yulunMapList.Add(mapName.ToLower(), this);
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
		GetComponent<Button>().onClick.AddListener(delegate
		{
			CheckMap();
		});
		onePointToPerson = (int)(allPerson / pointList.Count);
		CalPoint();
		redPointRun = StartCoroutine(ShowRedPoint());
		greenPointRun = StartCoroutine(ShowGreenPoint());
		if (choiced)
		{
			CheckMap(init: true);
		}
		for (int num = 0; num < penziList.Count; num++)
		{
			yulunPenziList.Add(yulunDialog.yulunPenziDialog.tipList[penziList[num]]);
		}
	}

	public void RefreshVal(YulunNews obj, YulunNewsInfo newsData)
	{
		if (!calling)
		{
			calling = true;
			CalPersonVal(obj, newsData);
			ChangePointVal("z");
			ChangePointVal("f");
			calling = false;
			if (calList.Count > 0)
			{
				CalPersonVal(obj, calList[0]);
				ChangePointVal("z");
				ChangePointVal("f");
				calList.Remove(calList[0]);
			}
			else
			{
				StopCoroutine(redPointRun);
				StopCoroutine(greenPointRun);
				StartCoroutine(ShowRedPoint(isUpdate: true));
				StartCoroutine(ShowGreenPoint(isUpdate: true));
			}
		}
		else
		{
			calList.Add(newsData);
		}
	}

	public void ChangePointVal(string type)
	{
		if (type == "z")
		{
			if (zPerson <= (float)allPerson)
			{
				if (zPerson + fPerson > (float)allPerson)
				{
					fPerson = (float)allPerson - zPerson;
				}
				zPerson = ((zPerson > 0f) ? zPerson : 0f);
			}
		}
		else if (fPerson <= (float)allPerson)
		{
			if (zPerson + fPerson > (float)allPerson)
			{
				zPerson = (float)allPerson - fPerson;
			}
			fPerson = ((fPerson > 0f) ? fPerson : 0f);
		}
		CalPoint();
	}

	private void CalPersonVal(YulunNews news, YulunNewsInfo newsData)
	{
		float num = 0f;
		float num2 = 0f;
		if (news.newsType == "0")
		{
			foreach (KeyValuePair<string, string> item in news.usedAutoNews)
			{
				if (gameManager.dataManager.dic43[item.Key].city.ToLower() == mapName.ToLower())
				{
					num = GetFVal(newsData, news.usedAutoNews[item.Key.ToString()]);
					num2 = GetZVal(newsData, news.usedAutoNews[item.Key.ToString()]);
				}
			}
		}
		else
		{
			num = GetFVal(newsData, news.round);
			num2 = GetZVal(newsData, news.round);
		}
		float num3 = 0f;
		if (num + num2 <= (float)allPerson)
		{
			num3 = num2 - zPerson;
			fPerson = num;
			zPerson = num2;
		}
		else if (num2 > zPerson && num <= fPerson)
		{
			num2 = ((num2 <= (float)allPerson) ? num2 : ((float)allPerson));
			num3 = num2 - zPerson;
			zPerson = num2;
			fPerson = (float)allPerson - zPerson;
		}
		else if (num2 <= zPerson && num > fPerson)
		{
			num = ((num <= (float)allPerson) ? num : ((float)allPerson));
			num3 = (float)allPerson - fPerson - zPerson;
			fPerson = num;
			zPerson = (float)allPerson - fPerson;
		}
		else
		{
			num2 = ((num2 <= (float)allPerson) ? num2 : ((float)allPerson));
			num3 = num2 - zPerson;
			zPerson = num2;
			fPerson = (float)allPerson - zPerson;
			Debug.LogError("out of the allperson");
		}
		yulunDialog.zAllPerson += (long)num3;
	}

	private float GetFVal(YulunNewsInfo newsData, string type)
	{
		float num = 0f;
		float num2 = (float)allPerson - zPerson - fPerson;
		float num3 = int.Parse(newsData.shuijunVal);
		if (type == "0")
		{
			float num4 = num2 * -0.06f * (1f - num3 / 20f);
			num = fPerson - num4;
			num = ((num <= 0f) ? 0f : num);
			return (num <= (float)allPerson) ? num : ((float)allPerson);
		}
		float num5 = num2 * 0.4f * (1f + num3 / 20f);
		num = Mathf.Ceil(fPerson * 0.6f - num5 / 3f);
		num = ((num <= 0f) ? 0f : num);
		return (num <= (float)allPerson) ? num : ((float)allPerson);
	}

	private float GetZVal(YulunNewsInfo newsData, string type)
	{
		float num = 0f;
		float num2 = (float)allPerson - zPerson - fPerson;
		float num3 = int.Parse(newsData.shuijunVal);
		float num4 = 0f;
		for (int i = 0; i < yulunPenziList.Count; i++)
		{
			num4 += yulunPenziList[i].val;
		}
		num4 /= 30f;
		if (type == "0")
		{
			float num5 = num2 * -0.06f * (1f - num3 / 20f);
			num = Mathf.Ceil(zPerson * (0.94f + num4) + num5);
			num = ((num <= 0f) ? 0f : num);
			return (num <= (float)allPerson) ? num : ((float)allPerson);
		}
		float num6 = num2 * 0.4f * (1f + num3 / 20f);
		float num7 = Mathf.Floor(Random.Range(1, 11));
		num = Mathf.Ceil(num2 * (num4 * num7 * 0.1f * 2f)) + zPerson + num6;
		num = ((num <= (float)allPerson) ? num : ((float)allPerson));
		return (num <= 0f) ? 0f : num;
	}

	private void CheckMap(bool init = false)
	{
		if (!yulunDialog.gameRunning && (!choiced || init))
		{
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(36);
			choiced = true;
			tu.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			shadow.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			yulunDialog.Blur(mapName);
			yulunDialog.yulunDataDialog.Init(this, gameManager);
		}
	}

	private void CalPoint()
	{
		int num = (int)Mathf.Floor(zPerson / (float)onePointToPerson);
		int num2 = (int)Mathf.Floor(fPerson / (float)onePointToPerson);
		zPercent = ((zPerson != 0f) ? ((num < 1) ? 1 : num) : 0);
		fPercent = ((fPerson != 0f) ? ((num2 < 1) ? 1 : num2) : 0);
	}

	private IEnumerator ShowRedPoint(bool isUpdate = false)
	{
		yield return new WaitForSeconds(waitTime);
		while (fPerson != 0f)
		{
			int num = Random.Range(0, pointList.Count);
			if (fPercentList.Count < fPercent && num % 2 == 0 && !fPercentList.Contains(pointList[num]))
			{
				fPercentList.Add(pointList[num]);
				pointList[num].GetComponent<Image>().sprite = redPoint;
				float num2 = (float)Random.Range(5, 16) * 0.1f;
				fPercentList[fPercentList.Count - 1].GetComponent<RectTransform>().DOScale(new Vector3(num2, num2, num2), 0.5f);
				if (isUpdate || num % 5 == 0)
				{
					StartCoroutine(HidePoint(fPercentList[fPercentList.Count - 1], fPercentList, fPercent));
				}
				float seconds = Random.Range(8f, 35f) * 0.1f;
				yield return new WaitForSeconds(seconds);
			}
			else
			{
				yield return new WaitForSeconds(0.001f);
			}
		}
		isUpdate = false;
	}

	private IEnumerator ShowGreenPoint(bool isUpdate = false)
	{
		yield return new WaitForSeconds(waitTime);
		while (zPerson != 0f)
		{
			int num = Random.Range(0, pointList.Count);
			if (zPercentList.Count < zPercent && num % 2 != 0 && !zPercentList.Contains(pointList[num]))
			{
				zPercentList.Add(pointList[num]);
				pointList[num].GetComponent<Image>().sprite = greenPoint;
				float num2 = (float)Random.Range(5, 16) * 0.1f;
				zPercentList[zPercentList.Count - 1].GetComponent<RectTransform>().DOScale(new Vector3(num2, num2, num2), 0.5f);
				if (isUpdate || num % 5 == 0)
				{
					StartCoroutine(HidePoint(zPercentList[zPercentList.Count - 1], zPercentList, zPercent));
				}
				float seconds = Random.Range(8f, 25f) * 0.1f;
				yield return new WaitForSeconds(seconds);
			}
			else
			{
				yield return new WaitForSeconds(0.001f);
			}
		}
		isUpdate = false;
	}

	private IEnumerator HidePoint(GameObject pointObj, List<GameObject> listObj, int val)
	{
		float num = Random.Range(Mathf.Ceil((val <= 1) ? 1 : (val - 1)), val + 1);
		yield return new WaitForSeconds(num * 1.4f);
		pointObj.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.5f);
		listObj.Remove(pointObj);
	}

	public void Blur()
	{
		choiced = false;
		shadow.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		tu.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!choiced && !yulunDialog.gameRunning)
		{
			tu.GetComponent<CanvasGroup>().DOFade(0.4f, 0.3f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!choiced)
		{
			tu.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		}
	}
}
