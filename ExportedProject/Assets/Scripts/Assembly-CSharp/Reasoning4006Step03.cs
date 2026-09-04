using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4006Step03 : MonoBehaviour
{
	[SerializeField]
	private GameObject step04;

	[SerializeField]
	private List<Card> cards = new List<Card>();

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	public bool iscandragcard;

	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
	}

	private void Check()
	{
		int num = 0;
		for (int i = 0; i < cards.Count; i++)
		{
			if (cards[i].correntpos != cards[i].pos)
			{
				cards[i].SetRed();
			}
			else
			{
				num++;
			}
		}
		if (num == cards.Count)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.DOText(I18N.instance.getValue("^tuili0348"), 2f).OnComplete(delegate
			{
				StartCoroutine(Over());
			});
		}
	}

	private IEnumerator Over()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
		txt_summry.transform.DOLocalMoveY(-281f, 1f);
		DOTween.To(() => txt_summry.fontSize, delegate(int x)
		{
			txt_summry.fontSize = x;
		}, 18, 1f);
		step04.SetActive(value: true);
		base.gameObject.SetActive(value: false);
		txt_summry.fontStyle = FontStyle.Normal;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
