using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4009Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject txt_mailtitle;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private List<ClickItem> clickItems = new List<ClickItem>();

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
		for (int i = 0; i < clickItems.Count; i++)
		{
			if (i == 0 || i == 3 || i == 4 || i == 7 || i == 8 || i == 9)
			{
				if (!clickItems[i].isselect)
				{
					flag = false;
					break;
				}
			}
			else if (clickItems[i].isselect)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili0912"), 3f).OnComplete(delegate
			{
				iscankeyboard = true;
			});
		}
		else
		{
			iscanclick = true;
			for (int num = 0; num < clickItems.Count; num++)
			{
				clickItems[num].StartRed();
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
			sequence.Append(step01.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.Append(txt_summry.transform.DOLocalMoveY(308.9f, 1f));
			sequence.OnComplete(delegate
			{
				step02.SetActive(value: true);
				txt_mailtitle.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
