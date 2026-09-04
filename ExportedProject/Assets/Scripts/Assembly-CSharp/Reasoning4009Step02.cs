using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4009Step02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private GameObject txt_tip;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private List<ClickCard> clickCards = new List<ClickCard>();

	public bool iscanclick;

	private bool iscankeyboard;

	private void Start()
	{
		iscanclick = true;
		btn_continue.onClick.AddListener(delegate
		{
			Check();
		});
	}

	private void Check()
	{
		bool flag = true;
		for (int i = 0; i < clickCards.Count; i++)
		{
			if (i == 0 || i == 2 || i == 3)
			{
				if (clickCards[i].isup)
				{
					flag = false;
					break;
				}
			}
			else if (!clickCards[i].isup)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			txt_tip.SetActive(value: false);
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili0923"), 3f).OnComplete(delegate
			{
				iscankeyboard = true;
			});
		}
		else
		{
			iscanclick = true;
			for (int num = 0; num < clickCards.Count; num++)
			{
				clickCards[num].StartRed();
			}
		}
	}

	private void Update()
	{
		if (iscankeyboard && Input.anyKey)
		{
			txt_summry.fontSize = 16;
			txt_summry.fontStyle = FontStyle.Normal;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(step02.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.Append(txt_summry.transform.DOLocalMoveY(230f, 1f));
			sequence.OnComplete(delegate
			{
				step03.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
