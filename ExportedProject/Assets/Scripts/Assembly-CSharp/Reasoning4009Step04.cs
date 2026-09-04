using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4009Step04 : MonoBehaviour
{
	[SerializeField]
	private GameObject step04;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private List<Toggle> toggles = new List<Toggle>();

	public bool iscanclick;

	public ReasoningMiddle4005 reasoningMiddle4009;

	public ReasoningPanel reasoningPanel;

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
		if (!iscanclick)
		{
			return;
		}
		if (toggles[2].isOn)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			reasoningMiddle4009.isallright = true;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(step04.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.OnComplete(delegate
			{
				reasoningPanel.GetResult();
			});
			return;
		}
		for (int num = 0; num < toggles.Count; num++)
		{
			if (toggles[num].isOn)
			{
				StartCoroutine(StartRedAni(toggles[num]));
			}
		}
	}

	private IEnumerator StartRedAni(Toggle tog)
	{
		iscanclick = false;
		Sequence s = DOTween.Sequence();
		s.Append(tog.transform.Find("Label").GetComponent<Text>().DOColor(Color.red, 0.2f));
		s.Append(tog.transform.Find("Label").GetComponent<Text>().DOColor(new Color(0.28f, 0.29f, 0.32f, 1f), 0.2f));
		s.Append(tog.transform.Find("Label").GetComponent<Text>().DOColor(Color.red, 0.2f));
		s.Append(tog.transform.Find("Label").GetComponent<Text>().DOColor(new Color(0.28f, 0.29f, 0.32f, 1f), 0.2f));
		yield return new WaitForSeconds(1f);
		iscanclick = true;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
