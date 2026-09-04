using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4007Step04 : MonoBehaviour
{
	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry4;

	[SerializeField]
	private List<Toggle> toggles = new List<Toggle>();

	public ReasoningMiddle4007 reasoningMiddle4007;

	public bool iscancheck = true;

	private void Start()
	{
		btn_continue.gameObject.SetActive(value: true);
		btn_continue.onClick.AddListener(Check);
	}

	private void Check()
	{
		if (!iscancheck)
		{
			return;
		}
		if (toggles[0].isOn && toggles[2].isOn && toggles[3].isOn && toggles[6].isOn && !toggles[1].isOn && !toggles[4].isOn && !toggles[5].isOn)
		{
			reasoningMiddle4007.isallright = true;
			step03.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			reasoningMiddle4007.IsAllRight();
			reasoningMiddle4007.reasoningPanel.GetResult();
			return;
		}
		int a = 0;
		for (int i = 0; i < toggles.Count; i++)
		{
			if (!toggles[i].isOn)
			{
				continue;
			}
			iscancheck = false;
			a++;
			toggles[i].isOn = false;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(toggles[i].transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence.Append(toggles[i].transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				a--;
				if (a == 0)
				{
					iscancheck = true;
				}
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
