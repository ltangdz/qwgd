using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReaditBrowser : MonoBehaviour
{
	public List<string> pinglun;

	public List<string> reply;

	public List<string> collectID;

	public List<string> replyImg;

	public Transform pinglunListBox;

	public Transform numberBox;

	public int onePageReplyNum;

	public int crtPage;

	public float pageNum;

	public List<Sprite> pageBk;

	public List<Sprite> btnLeftImg;

	public List<Sprite> btnRightImg;

	public Button btnRight;

	public Button btnLeft;

	public Button btnLogin;

	public Button btnRegister;

	public Button btnCloseAlert;

	public GameObject alertObj;

	private GameManager gameManager;

	private void Start()
	{
		SetPage();
		InitList();
		btnRight.onClick.AddListener(delegate
		{
			if ((float)crtPage < pageNum - 1f)
			{
				crtPage++;
				InitList();
				RefreshBtn();
			}
		});
		btnLeft.onClick.AddListener(delegate
		{
			if (crtPage > 0)
			{
				crtPage--;
				InitList();
				RefreshBtn();
			}
		});
		btnLogin.onClick.AddListener(delegate
		{
			alertObj.SetActive(value: true);
		});
		btnRegister.onClick.AddListener(delegate
		{
			alertObj.SetActive(value: true);
		});
		btnCloseAlert.onClick.AddListener(delegate
		{
			alertObj.SetActive(value: false);
		});
	}

	private void InitList()
	{
		for (int i = 0; i < pinglunListBox.childCount; i++)
		{
			Object.Destroy(pinglunListBox.GetChild(i).gameObject);
		}
		int num = (((crtPage + 1) * onePageReplyNum > pinglun.Count) ? pinglun.Count : ((crtPage + 1) * onePageReplyNum));
		for (int j = crtPage * onePageReplyNum; j < num; j++)
		{
			Object.Instantiate(Resources.Load<GameObject>("Browser/readPinglunList"), pinglunListBox).GetComponent<ReadPinglunList>().Init(j, this, gameManager);
		}
		DOTween.To(() => GetComponent<ScrollRect>().normalizedPosition, delegate(Vector2 x)
		{
			GetComponent<ScrollRect>().normalizedPosition = x;
		}, Vector2.one, 0.2f);
	}

	private void SetPage()
	{
		pageNum = Mathf.Ceil((float)pinglun.Count / (float)onePageReplyNum);
		for (int i = 0; (float)i < pageNum; i++)
		{
			int s = i;
			Transform transform = Object.Instantiate(Resources.Load<Transform>("Browser/readpage"), numberBox);
			transform.GetComponent<Button>().onClick.AddListener(delegate
			{
				crtPage = s;
				InitList();
				RefreshBtn();
			});
			transform.Find("Text").GetComponent<I18NText>().updateTranslation2((i + 1).ToString());
			if (i == 0)
			{
				transform.GetComponent<Image>().sprite = pageBk[1];
				string text = transform.Find("Text").GetComponent<Text>().text;
				transform.Find("Text").GetComponent<I18NText>().updateTranslation2(text);
				continue;
			}
			transform.GetComponent<Image>().sprite = pageBk[0];
			if (btnRight.GetComponent<Image>().sprite != btnRightImg[1])
			{
				btnRight.GetComponent<Image>().sprite = btnRightImg[1];
			}
		}
	}

	private void RefreshBtn()
	{
		if (crtPage == 0 && pageNum > 1f)
		{
			btnLeft.GetComponent<Image>().sprite = btnLeftImg[0];
			btnRight.GetComponent<Image>().sprite = btnRightImg[1];
		}
		else if ((float)crtPage == pageNum - 1f && pageNum > 1f)
		{
			btnLeft.GetComponent<Image>().sprite = btnLeftImg[1];
			btnRight.GetComponent<Image>().sprite = btnRightImg[0];
		}
		else if (pageNum == 1f)
		{
			btnLeft.GetComponent<Image>().sprite = btnLeftImg[0];
			btnRight.GetComponent<Image>().sprite = btnRightImg[0];
		}
		else
		{
			btnLeft.GetComponent<Image>().sprite = btnLeftImg[1];
			btnRight.GetComponent<Image>().sprite = btnRightImg[1];
		}
		for (int i = 0; (float)i < pageNum; i++)
		{
			numberBox.GetChild(i).GetComponent<Image>().sprite = pageBk[0];
			string text = numberBox.GetChild(i).Find("Text").GetComponent<Text>()
				.text;
			numberBox.GetChild(i).Find("Text").GetComponent<I18NText>()
				.updateTranslation2(text);
		}
		numberBox.GetChild(crtPage).GetComponent<Image>().sprite = pageBk[1];
		string text2 = numberBox.GetChild(crtPage).Find("Text").GetComponent<Text>()
			.text;
		numberBox.GetChild(crtPage).Find("Text").GetComponent<I18NText>()
			.updateTranslation2(text2);
	}
}
