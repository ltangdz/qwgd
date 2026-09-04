using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4010Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	public bool iscanclick;

	[SerializeField]
	private Selectbox correctbox;

	[SerializeField]
	private List<Selectbox> selectboxes = new List<Selectbox>();

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
		if (correctbox.isselect)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili1008"), 3f).OnComplete(delegate
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
			sequence.Append(step01.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.Append(txt_summry.transform.DOLocalMoveY(308.9f, 1f));
			sequence.OnComplete(delegate
			{
				step02.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
