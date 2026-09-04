using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvadeQAList : MonoBehaviour
{
	public int picType = -1;

	public int trueAnwser = -1;

	public int choicedIndex = -1;

	public InvadeQADialog invadeQADialog;

	public List<Toggle> answerList;

	public bool choiced;

	public void Init()
	{
		choicedIndex = -1;
		choiced = false;
	}

	public void IsChoiced(int i)
	{
		if (!choiced)
		{
			choicedIndex = i;
			invadeQADialog.queIndex[picType].sprite = invadeQADialog.queIndexSprite[1];
			invadeQADialog.btnNext.interactable = true;
		}
		else
		{
			choicedIndex = -1;
			invadeQADialog.queIndex[picType].sprite = invadeQADialog.queIndexSprite[0];
			invadeQADialog.btnNext.interactable = false;
		}
		choiced = !choiced;
	}

	public bool IsTrueChoiced()
	{
		if (trueAnwser != choicedIndex)
		{
			return false;
		}
		return true;
	}
}
