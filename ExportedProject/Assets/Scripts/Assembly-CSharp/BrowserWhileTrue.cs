using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserWhileTrue : MonoBehaviour
{
	public Button btnJiaoyiBox;

	public Button btnXuyuanBox;

	public List<GameObject> tips;

	public GameObject pinglun;

	public Button btnXuyuan;

	public List<Button> tabBtnList;

	public List<GameObject> pinglunBoxList;

	public GameObject boxAlert;

	public Button btnCloseAlert;

	private void Start()
	{
		btnJiaoyiBox.onClick.AddListener(delegate
		{
			btnXuyuanBox.GetComponent<CanvasGroup>().alpha = 1f;
			btnJiaoyiBox.GetComponent<CanvasGroup>().alpha = 0.6f;
			pinglun.SetActive(value: false);
			for (int i = 0; i < tips.Count; i++)
			{
				tips[i].SetActive(value: true);
			}
		});
		btnXuyuanBox.onClick.AddListener(delegate
		{
			btnXuyuanBox.GetComponent<CanvasGroup>().alpha = 0.6f;
			btnJiaoyiBox.GetComponent<CanvasGroup>().alpha = 1f;
			pinglun.SetActive(value: true);
			for (int i = 0; i < tips.Count; i++)
			{
				tips[i].SetActive(value: false);
			}
		});
		for (int num = 0; num < tabBtnList.Count; num++)
		{
			int s = num;
			tabBtnList[s].onClick.AddListener(delegate
			{
				ShowPinglun(s);
			});
		}
		btnXuyuan.onClick.AddListener(delegate
		{
			boxAlert.SetActive(value: true);
		});
		btnCloseAlert.onClick.AddListener(delegate
		{
			boxAlert.SetActive(value: false);
		});
	}

	private void ShowPinglun(int i)
	{
		BtnClick(i);
		for (int j = 0; j < pinglunBoxList.Count; j++)
		{
			pinglunBoxList[j].SetActive(value: false);
		}
		pinglunBoxList[i].SetActive(value: true);
		LineTop();
	}

	private void BtnClick(int j)
	{
		for (int i = 0; i < tabBtnList.Count; i++)
		{
			tabBtnList[i].transform.Find("Text").GetComponent<Text>().color = new Color(0.74f, 0.74f, 0.73f);
			tabBtnList[i].transform.Find("Image").gameObject.SetActive(value: false);
		}
		Debug.Log(j);
		tabBtnList[j].transform.Find("Text").GetComponent<Text>().color = new Color(0.6f, 0.17f, 0.17f);
		tabBtnList[j].transform.Find("Image").gameObject.SetActive(value: true);
	}

	private void LineTop()
	{
		Canvas.ForceUpdateCanvases();
		GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
		Canvas.ForceUpdateCanvases();
	}
}
