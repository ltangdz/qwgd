using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4006Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private int correct;

	[SerializeField]
	private TimePanel timepanel;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private Text txt_maintitle;

	private void Start()
	{
		txt_maintitle.DOFade(1f, 0.2f);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
	}

	private void Check()
	{
		if (timepanel.current == correct)
		{
			timepanel.iscanclick = false;
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.DOText(I18N.instance.getValue("^tuili0337"), 3f).OnComplete(delegate
			{
				StartCoroutine(Over());
			});
		}
		else
		{
			timepanel.SetRed();
		}
	}

	private IEnumerator Over()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
		txt_summry.transform.DOLocalMoveY(-123f, 1f);
		DOTween.To(() => txt_summry.fontSize, delegate(int x)
		{
			txt_summry.fontSize = x;
		}, 18, 1f);
		step02.SetActive(value: true);
		base.gameObject.SetActive(value: false);
		txt_summry.fontStyle = FontStyle.Normal;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
