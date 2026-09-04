using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4007Step00 : MonoBehaviour
{
	[SerializeField]
	private GameObject step00;

	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_maintitle;

	[SerializeField]
	private Text txt_summry01;

	[SerializeField]
	private List<DragAnswerSix> dragAnswerSixes = new List<DragAnswerSix>();

	[SerializeField]
	private RoleSixBlank RoleSixBlank;

	public ReasoningMiddle4007 reasoningMiddle4007;

	public bool iscandrag;

	public int isover;

	private void Start()
	{
		txt_maintitle.DOFade(1f, 0.2f);
		btn_continue.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check1);
	}

	private void Check1()
	{
		bool flag = true;
		for (int i = 0; i < dragAnswerSixes.Count; i++)
		{
			if (i == 0 || i == 1 || i == 4 || i == 5 || i == 8 || i == 9)
			{
				if (dragAnswerSixes[i].roleSixBlank == null)
				{
					flag = false;
					break;
				}
			}
			else if (dragAnswerSixes[i].roleSixBlank != null)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry01.DOText(I18N.instance.getValue("^tuili400726"), 1.5f).OnComplete(delegate
			{
				isover = 1;
			});
		}
		else
		{
			for (int num = 0; num < dragAnswerSixes.Count; num++)
			{
				dragAnswerSixes[num].ResetPos();
			}
			RoleSixBlank.ResetPos();
		}
	}

	private void Update()
	{
		if (isover == 1 && Input.anyKey)
		{
			isover = 2;
			GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
			{
				txt_summry01.gameObject.SetActive(value: false);
				step01.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
