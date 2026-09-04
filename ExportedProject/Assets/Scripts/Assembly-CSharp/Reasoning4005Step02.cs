using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4005Step02 : MonoBehaviour
{
	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private List<Card> cards = new List<Card>();

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private ReasoningMiddle4005 reasoningMiddle;

	[SerializeField]
	private ReasoningPanel reasoningPanel;

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
			txt_summry.DOText(I18N.instance.getValue("^tuili0332"), 2f).OnComplete(delegate
			{
				StartCoroutine(End());
			});
		}
	}

	private IEnumerator End()
	{
		yield return new WaitForSeconds(2f);
		reasoningMiddle.isallright = true;
		GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
		{
			reasoningPanel.GetResult();
			base.gameObject.SetActive(value: false);
		});
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
