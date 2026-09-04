using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeQADialog : CustomDialog
{
	public List<InvadeQAList> queList;

	public Button btnNext;

	public Button btnLast;

	public GameObject txtTip;

	public GameObject tipBox;

	public List<Image> queIndex;

	public List<Sprite> queIndexSprite;

	public InvadePhoneDialog invadePhoneDialog;

	private int crtQue;

	private void Start()
	{
		btnNext.onClick.AddListener(NextQue);
		btnLast.onClick.AddListener(LastQue);
		txtTip.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^invade_phone0450") + "(" + (crtQue + 1) + "/" + queList.Count + ")");
	}

	private void NextQue()
	{
		if (crtQue == queList.Count - 1)
		{
			Submit();
			return;
		}
		queList[crtQue].gameObject.SetActive(value: false);
		crtQue++;
		if (crtQue == queList.Count - 1)
		{
			btnNext.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^btn_submit");
		}
		if (!btnLast.interactable)
		{
			btnLast.interactable = true;
		}
		queList[crtQue].gameObject.SetActive(value: true);
		txtTip.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^invade_phone0450") + "(" + (crtQue + 1) + "/" + queList.Count + ")");
		if (queList[crtQue].choiced)
		{
			btnNext.interactable = true;
		}
		else
		{
			btnNext.interactable = false;
		}
	}

	private void LastQue()
	{
		btnNext.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^invade_phone0436");
		queList[crtQue].gameObject.SetActive(value: false);
		crtQue--;
		txtTip.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^invade_phone0450") + "(" + (crtQue + 1) + "/" + queList.Count + ")");
		queList[crtQue].gameObject.SetActive(value: true);
		if (crtQue == 0)
		{
			btnLast.interactable = false;
		}
		if (!btnNext.interactable)
		{
			btnNext.interactable = true;
		}
	}

	private void Submit()
	{
		bool flag = true;
		for (int i = 0; i < queList.Count; i++)
		{
			if (!queList[i].IsTrueChoiced())
			{
				flag = false;
			}
		}
		if (flag)
		{
			invadePhoneDialog.ShowUnlock();
			Object.Destroy(base.gameObject);
			return;
		}
		txtTip.GetComponent<I18NText>().updateTranslation2("<color=#F93537>" + I18N.instance.getValue("^invade_phone0451") + "</color>");
		for (int j = 0; j < queIndex.Count; j++)
		{
			queIndex[j].sprite = queIndexSprite[2];
		}
		btnNext.interactable = false;
		StartCoroutine(HideTip());
	}

	private IEnumerator HideTip()
	{
		for (int i = 0; i < 5; i++)
		{
			tipBox.GetComponent<RectTransform>().DOLocalMoveX(-15f, 0.02f);
			yield return new WaitForSeconds(0.02f);
			tipBox.GetComponent<RectTransform>().DOLocalMoveX(15f, 0.02f);
			yield return new WaitForSeconds(0.02f);
		}
		tipBox.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.02f);
		crtQue = 0;
		txtTip.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^invade_phone0450") + "(" + (crtQue + 1) + "/" + queList.Count + ")");
		for (int j = 0; j < queIndex.Count; j++)
		{
			queIndex[j].sprite = queIndexSprite[0];
		}
		for (int k = 0; k < queList.Count; k++)
		{
			InvadeQAList component = queList[k].GetComponent<InvadeQAList>();
			component.answerList[component.choicedIndex].isOn = false;
		}
		queList[0].gameObject.SetActive(value: true);
		queList[2].gameObject.SetActive(value: false);
		btnLast.interactable = false;
		btnNext.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^invade_phone0436");
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
