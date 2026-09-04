using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4006Step04 : MonoBehaviour
{
	[SerializeField]
	private Toggle correcttoggle;

	[SerializeField]
	private Toggle toggle01;

	[SerializeField]
	private Toggle toggle03;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private ReasoningMiddle4006 reasoningMiddle;

	[SerializeField]
	private ReasoningPanel reasoningPanel;

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
		if (correcttoggle.isOn)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			reasoningMiddle.isallright = true;
			reasoningPanel.GetResult();
		}
		else if (toggle01.isOn)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(toggle01.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence.Append(toggle01.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence.Play().SetLoops(3);
		}
		else if (toggle03.isOn)
		{
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(toggle03.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence2.Append(toggle03.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence2.Play().SetLoops(3);
		}
	}

	private IEnumerator Over()
	{
		yield return new WaitForSeconds(2f);
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
