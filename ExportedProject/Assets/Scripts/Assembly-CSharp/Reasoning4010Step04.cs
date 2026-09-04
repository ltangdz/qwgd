using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4010Step04 : MonoBehaviour
{
	[SerializeField]
	private GameObject step04;

	[SerializeField]
	private GameObject txt_maintitle;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	public bool iscanclick;

	public ReasoningMiddle4005 reasoningMiddle4010;

	public ReasoningPanel reasoningPanel;

	[SerializeField]
	private List<SelectBadBox> selectboxes = new List<SelectBadBox>();

	private bool iscankeyboard;

	private void Start()
	{
		iscanclick = true;
		btn_continue.interactable = true;
		btn_continue.gameObject.SetActive(value: true);
		btn_continue.onClick.AddListener(delegate
		{
			Check();
		});
	}

	private void Check()
	{
		bool flag = true;
		if ((selectboxes[1].isselect && selectboxes[2].isselect && selectboxes[3].isselect && selectboxes[5].isselect && !selectboxes[0].isselect && !selectboxes[4].isselect) ? true : false)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			reasoningMiddle4010.isallright = true;
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili1024"), 3f).OnComplete(delegate
			{
				iscankeyboard = true;
			});
			for (int num = 0; num < selectboxes.Count; num++)
			{
				selectboxes[num].iscanclick = false;
			}
		}
		else
		{
			iscanclick = true;
			for (int num2 = 0; num2 < selectboxes.Count; num2++)
			{
				selectboxes[num2].SetRed();
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
			sequence.Append(step04.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.OnComplete(delegate
			{
				txt_summry.gameObject.SetActive(value: false);
				txt_maintitle.SetActive(value: false);
				reasoningPanel.GetResult();
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
