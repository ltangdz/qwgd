using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4010Step02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	public bool iscanclick;

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
		if ((selectboxes[0].isselect && selectboxes[2].isselect && selectboxes[3].isselect && !selectboxes[1].isselect) ? true : false)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili1014"), 3f).OnComplete(delegate
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
			sequence.Append(step02.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.Append(txt_summry.transform.DOLocalMoveY(239f, 1f));
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
