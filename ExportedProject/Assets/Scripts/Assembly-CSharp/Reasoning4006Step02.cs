using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4006Step02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private List<Selectbox> itemelist = new List<Selectbox>();

	[SerializeField]
	private int correct;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

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
		if (itemelist[1].isselect && itemelist[3].isselect && itemelist[4].isselect && !itemelist[0].isselect && !itemelist[2].isselect)
		{
			for (int i = 0; i < itemelist.Count; i++)
			{
				itemelist[i].iscanclick = false;
			}
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.DOText(I18N.instance.getValue("^tuili0340"), 3f).OnComplete(delegate
			{
				StartCoroutine(Over());
			});
		}
		else
		{
			for (int num = 0; num < itemelist.Count; num++)
			{
				itemelist[num].SetRed();
			}
		}
	}

	private IEnumerator Over()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
		txt_summry.transform.DOLocalMoveY(-193f, 1f);
		DOTween.To(() => txt_summry.fontSize, delegate(int x)
		{
			txt_summry.fontSize = x;
		}, 18, 1f);
		step03.SetActive(value: true);
		base.gameObject.SetActive(value: false);
		txt_summry.fontStyle = FontStyle.Normal;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
