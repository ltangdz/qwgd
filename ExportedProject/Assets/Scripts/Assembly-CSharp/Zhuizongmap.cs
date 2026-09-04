using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Zhuizongmap : MonoBehaviour
{
	[SerializeField]
	private ZhuizongDialog zhuizongDialog;

	[SerializeField]
	private List<GameObject> pointlist = new List<GameObject>();

	[SerializeField]
	private List<GameObject> keypointlist = new List<GameObject>();

	[SerializeField]
	private Transform icon0;

	[SerializeField]
	private Transform masaike;

	[SerializeField]
	private List<Image> linelist = new List<Image>();

	public int keypos;

	private int pos;

	[SerializeField]
	private GameObject warningPanel;

	[SerializeField]
	private GameObject overPanel;

	private GameManager gameManager;

	[SerializeField]
	private string date36id;

	public int count = -1;

	public List<HighLightPic> highLightPics = new List<HighLightPic>();

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Invoke("InitMap", 3f);
	}

	private void InitMap()
	{
		icon0.gameObject.SetActive(value: true);
		if (!gameManager.player.playerdata.surveillanceRecord.ContainsKey(date36id))
		{
			Go();
			return;
		}
		ShowAllDot();
		keypos = keypointlist.Count - 1;
		zhuizongDialog.UpdateAddress();
		for (int i = 0; i < highLightPics.Count; i++)
		{
			highLightPics[i].gameObject.SetActive(value: true);
		}
	}

	private void ShowAllDot()
	{
		icon0.gameObject.SetActive(value: false);
		for (int i = 0; i < keypointlist.Count; i++)
		{
			keypointlist[i].SetActive(value: true);
		}
		for (int j = 0; j < linelist.Count; j++)
		{
			linelist[j].fillAmount = 1f;
		}
		zhuizongDialog.GetComponent<Image>().DOFade(0f, 0.2f);
		zhuizongDialog.GetComponent<Image>().raycastTarget = false;
		if (zhuizongDialog.GetComponent<GraphicRaycaster>() != null)
		{
			Object.Destroy(zhuizongDialog.GetComponent<GraphicRaycaster>());
		}
		if (zhuizongDialog.GetComponent<Canvas>() != null)
		{
			Object.Destroy(zhuizongDialog.GetComponent<Canvas>());
		}
	}

	private void Go()
	{
		warningPanel.SetActive(value: false);
		if (pos + 1 >= pointlist.Count)
		{
			zhuizongDialog.btn_close.gameObject.SetActive(value: true);
			zhuizongDialog.GetComponent<Image>().DOFade(0f, 0.2f);
			zhuizongDialog.GetComponent<Image>().raycastTarget = false;
			if (zhuizongDialog.GetComponent<GraphicRaycaster>() != null)
			{
				Object.Destroy(zhuizongDialog.GetComponent<GraphicRaycaster>());
			}
			if (zhuizongDialog.GetComponent<Canvas>() != null)
			{
				Object.Destroy(zhuizongDialog.GetComponent<Canvas>());
			}
			if (!gameManager.player.playerdata.surveillanceRecord.ContainsKey(date36id))
			{
				gameManager.player.playerdata.surveillanceRecord.Add(date36id, new List<Vector2>());
			}
			overPanel.SetActive(value: true);
			for (int i = 0; i < highLightPics.Count; i++)
			{
				highLightPics[i].gameObject.SetActive(value: true);
			}
			return;
		}
		if (pos == 0)
		{
			keypointlist[keypos].SetActive(value: true);
			zhuizongDialog.UpdateAddress();
			keypos++;
		}
		float duration = Vector3.Distance(pointlist[pos + 1].transform.localPosition, pointlist[pos].transform.localPosition) / 60f;
		icon0.DOLocalMove(pointlist[pos + 1].transform.localPosition, duration).SetEase(Ease.Linear).OnComplete(delegate
		{
			if (pointlist[pos + 1].name.Contains("keypoint"))
			{
				icon0.transform.GetChild(0).gameObject.SetActive(value: false);
				icon0.transform.GetChild(1).gameObject.SetActive(value: true);
				icon0.GetComponent<Image>().enabled = false;
				warningPanel.SetActive(value: true);
				Sequence sequence = DOTween.Sequence();
				sequence.Append(warningPanel.GetComponent<CanvasGroup>().DOFade(0.5f, 0.5f));
				sequence.Append(warningPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f));
				sequence.Play().SetLoops(-1);
			}
			else if (pointlist[pos + 1].name.Contains("masaike"))
			{
				icon0.transform.GetChild(0).gameObject.SetActive(value: true);
				pos++;
				Go();
			}
			else
			{
				pos++;
				Go();
			}
		});
		linelist[pos].DOFillAmount(1f, duration).SetEase(Ease.Linear);
	}

	public void ClickQuestion()
	{
		if (gameManager.homeScene.transform.Find("shenzhuizongPanel") == null)
		{
			count++;
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/shenzhuizongPanel"), gameManager.homeScene.transform);
			obj.name = "shenzhuizongPanel";
			obj.GetComponent<ShenzhuizongPanel>().data36id = zhuizongDialog.currentdata36.ID.ToString();
			obj.GetComponent<ShenzhuizongPanel>().zhuizongmap = this;
		}
	}

	public void GoOn()
	{
		keypointlist[keypos].SetActive(value: true);
		zhuizongDialog.UpdateAddress();
		keypos++;
		pos++;
		icon0.transform.GetChild(0).gameObject.SetActive(value: false);
		icon0.transform.GetChild(1).gameObject.SetActive(value: false);
		icon0.GetComponent<Image>().enabled = true;
		Go();
	}
}
