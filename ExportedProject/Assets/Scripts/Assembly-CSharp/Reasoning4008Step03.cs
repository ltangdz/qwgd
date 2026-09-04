using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4008Step03 : MonoBehaviour
{
	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private GameObject txt_tip;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private List<DragLetterItem> dragLetterItems = new List<DragLetterItem>();

	[SerializeField]
	private RoleTwoBlank img_left;

	[SerializeField]
	private RoleTwoBlank img_right;

	public bool iscandrag = true;

	public int isover;

	public ReasoningMiddle4008 reasoningMiddle4008;

	private void Start()
	{
		btn_continue.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
	}

	private void Check()
	{
		bool flag = true;
		for (int i = 0; i < dragLetterItems.Count; i++)
		{
			if (!dragLetterItems[i].IsRight())
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			iscandrag = false;
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_tip.SetActive(value: false);
			txt_summry.DOText(I18N.instance.getValue("^tuili0494"), 3f).OnComplete(delegate
			{
				isover = 1;
			});
		}
		else
		{
			for (int num = 0; num < dragLetterItems.Count; num++)
			{
				dragLetterItems[num].ResetPos();
			}
			img_left.ResetPos();
			img_right.ResetPos();
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}

	private void Update()
	{
		if (isover == 1 && Input.anyKey)
		{
			isover = 2;
			step03.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			txt_summry.gameObject.SetActive(value: false);
			reasoningMiddle4008.isallright = true;
			reasoningMiddle4008.reasoningPanel.GetResult();
			step03.SetActive(value: false);
		}
	}
}
