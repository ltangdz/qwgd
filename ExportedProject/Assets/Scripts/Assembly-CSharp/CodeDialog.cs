using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CodeDialog : MonoBehaviour
{
	public Image img_tip;

	public Text txt_code;

	public ScrollRect scrollRect;

	public string oricontent = "";

	public Text txt_download;

	public Image img_red;

	public List<Text> textlist = new List<Text>();

	private string[] strs;

	public Transform textparent;

	public GameObject img_code;

	private bool isanykey;

	private GameManager gameManager;

	private void ReadFile()
	{
		strs = File.ReadAllLines(Application.streamingAssetsPath + "/linux.txt");
		StartCoroutine(AddText());
	}

	private IEnumerator AddText()
	{
		yield return new WaitForSeconds(1.5f);
		img_code.SetActive(value: true);
		for (int i = 0; i < strs.Length; i++)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("txt_codefake"), textparent);
			gameObject.GetComponent<Text>().DOText(strs[i], 0.05f);
			if (textlist.Count >= 42)
			{
				Object.Destroy(textlist[0].gameObject);
				textlist.RemoveAt(0);
			}
			textlist.Add(gameObject.GetComponent<Text>());
			yield return new WaitForSeconds(0.05f);
		}
		Object.Destroy(textlist[0].gameObject);
		textlist.RemoveAt(0);
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("txt_codefake"), textparent);
		string value = I18N.instance.getValue("^missionresult13");
		obj.GetComponent<Text>().fontSize = 26;
		obj.GetComponent<Text>().DOText(value, 0.1f).OnComplete(delegate
		{
			Canvas.ForceUpdateCanvases();
			textparent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
			Canvas.ForceUpdateCanvases();
		});
		isanykey = true;
	}

	public void ShowRed()
	{
		img_red.gameObject.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(img_red.DOFade(0.4f, 0.2f));
		s.Append(img_red.DOFade(0.15f, 0.2f));
		s.Append(img_red.DOFade(0.4f, 0.2f));
		s.Append(img_red.DOFade(0.15f, 0.2f));
		s.Append(img_red.DOFade(0.4f, 0.2f));
		s.Append(img_red.DOFade(0.15f, 0.2f)).OnComplete(delegate
		{
			ReadFile();
		});
	}

	private void UpdateDOWNLoadText()
	{
		float num = Random.Range(0f, 255f);
		txt_download.text = num.ToString("f2") + "KB/s";
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		oricontent = txt_code.text;
		ShowTip();
		InvokeRepeating("StartCode", 0.1f, 10f);
		Invoke("HideTip", 1f);
		InvokeRepeating("UpdateDOWNLoadText", 0.5f, 0.5f);
	}

	private void ShowTip()
	{
		img_tip.GetComponent<RectTransform>().DOSizeDelta(new Vector2(282f, 34f), 0.5f);
		img_tip.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}

	private void HideTip()
	{
		img_tip.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 34f), 0.5f);
		img_tip.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
	}

	private void StartCode()
	{
		txt_code.text = "";
		txt_code.DOText(oricontent, 10f).OnUpdate(delegate
		{
			DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
			{
				scrollRect.normalizedPosition = x;
			}, Vector2.zero, 1f);
		});
	}

	private void Update()
	{
		if (Input.anyKey && isanykey)
		{
			isanykey = false;
			Object.Destroy(base.gameObject);
			ShowLink();
		}
	}

	public void ShowLink()
	{
		Object.Instantiate(Resources.Load("Houtai/HoutaiPanel") as GameObject, gameManager.homeScene.middle);
	}
}
