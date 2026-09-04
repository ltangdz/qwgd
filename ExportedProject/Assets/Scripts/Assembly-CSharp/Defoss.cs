using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Defoss : MonoBehaviour
{
	public GameObject listBox;

	public Button leftBtn;

	public Image leftBtnImg;

	public Button rightBtn;

	public Image rightBtnImg;

	public Text page;

	public List<Sprite> leftArrow;

	public List<Sprite> rightArrow;

	private GameManager gameManager;

	private bool run;

	private float allPage;

	private float crtPage;

	private float listBoxWidth;

	public MultiplyText txt_mail;

	public MultiplyText txt_name;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_mail.SetContent2("^defoss_label141", "10027", I18N.instance.getValue("^message_event0126"));
		txt_name.SetContent2("^defoss_label11", "10045", I18N.instance.getValue("^defoss_label11"));
		allPage = listBox.transform.childCount;
		crtPage = 1f;
		page.GetComponent<I18NText>().updateTranslation2(crtPage + "/" + allPage);
		leftBtn.onClick.AddListener(delegate
		{
			if (crtPage != 1f && !run)
			{
				StartCoroutine(MoveLeft());
			}
		});
		rightBtn.onClick.AddListener(delegate
		{
			if (crtPage != allPage && !run)
			{
				StartCoroutine(MoveRight());
			}
		});
	}

	private IEnumerator MoveLeft()
	{
		if (listBoxWidth == 0f)
		{
			listBoxWidth = listBox.GetComponent<RectTransform>().rect.width;
		}
		run = true;
		float x = listBox.GetComponent<RectTransform>().localPosition.x;
		Debug.Log(x + " " + listBoxWidth + " " + allPage);
		listBox.transform.DOLocalMoveX(x + listBoxWidth / allPage, 0.2f);
		crtPage -= 1f;
		rightBtnImg.sprite = rightArrow[1];
		page.GetComponent<I18NText>().updateTranslation2(crtPage + "/" + allPage);
		if (crtPage == 1f)
		{
			leftBtnImg.sprite = leftArrow[0];
		}
		else
		{
			leftBtnImg.sprite = leftArrow[1];
		}
		yield return new WaitForSeconds(0.2f);
		run = false;
	}

	private IEnumerator MoveRight()
	{
		if (listBoxWidth == 0f)
		{
			listBoxWidth = listBox.GetComponent<RectTransform>().rect.width;
		}
		run = true;
		float x = listBox.GetComponent<RectTransform>().localPosition.x;
		listBox.transform.DOLocalMoveX(x - listBoxWidth / allPage, 0.2f);
		crtPage += 1f;
		leftBtnImg.sprite = leftArrow[1];
		page.GetComponent<I18NText>().updateTranslation2(crtPage + "/" + allPage);
		if (crtPage == allPage)
		{
			rightBtnImg.sprite = rightArrow[0];
		}
		else
		{
			rightBtnImg.sprite = rightArrow[1];
		}
		yield return new WaitForSeconds(0.2f);
		run = false;
	}

	private void Update()
	{
	}
}
