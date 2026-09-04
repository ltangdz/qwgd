using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4008Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Button btn_continue2;

	[SerializeField]
	private Text txt_summry;

	public bool iscandragcard;

	[SerializeField]
	private List<BigCard> bigCards = new List<BigCard>();

	[SerializeField]
	private List<Selectbox> itemelist = new List<Selectbox>();

	[SerializeField]
	private GameObject img_title2;

	[SerializeField]
	private Text txt_01;

	[SerializeField]
	private Text txt_02;

	[SerializeField]
	private Text txt_03;

	private void Start()
	{
		btn_continue.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
		btn_continue2.onClick.AddListener(Check2);
	}

	private void Check()
	{
		bool flag = true;
		for (int i = 0; i < bigCards.Count; i++)
		{
			if (!bigCards[i].IsRight())
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			GetComponent<Animator>().enabled = true;
			GetComponent<Animator>().Play("reasoning4008step01_01");
		}
		else
		{
			for (int j = 0; j < bigCards.Count; j++)
			{
				bigCards[j].SetRed();
			}
		}
	}

	private void Check2()
	{
		if (itemelist[1].isselect && itemelist[3].isselect && !itemelist[4].isselect && !itemelist[0].isselect && !itemelist[2].isselect && !itemelist[5].isselect && !itemelist[6].isselect)
		{
			for (int i = 0; i < itemelist.Count; i++)
			{
				itemelist[i].iscanclick = false;
			}
			btn_continue2.interactable = false;
			btn_continue2.gameObject.SetActive(value: false);
			StartCoroutine(Over());
		}
		else
		{
			for (int j = 0; j < itemelist.Count; j++)
			{
				itemelist[j].SetRed();
			}
		}
	}

	private IEnumerator Over()
	{
		yield return new WaitForSeconds(1f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(bigCards[0].transform.DOLocalMoveY(bigCards[0].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(bigCards[0].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(bigCards[1].transform.DOLocalMoveY(bigCards[1].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(bigCards[1].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(bigCards[2].transform.DOLocalMoveY(bigCards[2].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(bigCards[2].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(txt_summry.transform.DOLocalMoveY(txt_summry.transform.localPosition.y + 50f, 0.5f));
		sequence.Join(txt_summry.DOFade(0f, 0.5f));
		sequence.Join(img_title2.transform.DOLocalMoveY(img_title2.transform.localPosition.y + 50f, 0.5f));
		sequence.Join(img_title2.GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(itemelist[0].transform.DOLocalMoveY(itemelist[0].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(itemelist[0].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(itemelist[2].transform.DOLocalMoveY(itemelist[2].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(itemelist[2].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(itemelist[4].transform.DOLocalMoveY(itemelist[4].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(itemelist[4].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(itemelist[5].transform.DOLocalMoveY(itemelist[5].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(itemelist[5].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(itemelist[6].transform.DOLocalMoveY(itemelist[6].transform.localPosition.y + 50f, 0.5f));
		sequence.Join(itemelist[6].GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
		sequence.Join(txt_01.transform.DOLocalMoveY(txt_01.transform.localPosition.y + 50f, 0.5f));
		sequence.Join(txt_01.DOFade(0f, 0.5f));
		sequence.Join(txt_02.transform.DOLocalMoveY(txt_02.transform.localPosition.y + 50f, 0.5f));
		sequence.Join(txt_02.DOFade(0f, 0.5f));
		sequence.Join(txt_03.transform.DOLocalMoveY(txt_03.transform.localPosition.y + 50f, 0.5f));
		sequence.Join(txt_03.DOFade(0f, 0.5f)).OnStart(delegate
		{
			itemelist[1].ResetSelect();
			itemelist[3].ResetSelect();
		});
		sequence.Append(itemelist[1].transform.DOLocalMoveY(300f, 1f));
		sequence.Join(itemelist[3].transform.DOLocalMoveY(300f, 1f));
		sequence.Append(itemelist[1].transform.DOLocalMove(new Vector3(-210f, 200f, 0f), 1f));
		sequence.Join(itemelist[3].transform.DOLocalMove(new Vector3(210f, 200f, 0f), 1f));
		sequence.Append(itemelist[1].transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 1f)).OnComplete(delegate
		{
			step02.SetActive(value: true);
			step01.SetActive(value: false);
		});
		sequence.Join(itemelist[3].transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 1f));
		sequence.Play();
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
		iscandragcard = true;
	}

	public void ShowSummry()
	{
		txt_summry.DOText(I18N.instance.getValue("^tuili400817"), 3f).OnComplete(delegate
		{
		});
	}
}
