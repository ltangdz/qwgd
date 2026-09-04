using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningMiddle4003 : ReasoningMiddle
{
	public int step = 1;

	public List<ReasoningDragBlank> reasoningDragBlanklist = new List<ReasoningDragBlank>();

	public List<ReasoningDragRole> reasoningDragRolelist = new List<ReasoningDragRole>();

	public I18NText txt_tip;

	public Reasoning4003Step01 reasoning4003Step01;

	public Reasoning4003Step02 reasonint4003Step02;

	public bool isallright;

	[SerializeField]
	private Image img_role1;

	[SerializeField]
	private Image img_role2;

	[SerializeField]
	private Image img_jon;

	[SerializeField]
	private Image img_elsie;

	public int correctcount;

	public string step01answer = "1;3;5";

	public ReasoningDragBlank step02dragblank;

	public int step02answer;

	public List<ReasoningDragRole> reasoningDragRoleliststep02 = new List<ReasoningDragRole>();

	private void Start()
	{
		img_role1.gameObject.SetActive(value: true);
		img_role2.gameObject.SetActive(value: true);
		img_role1.DOFade(1f, 1f);
		img_role2.DOFade(1f, 1f);
	}

	private void Update()
	{
	}

	public override bool IsAllRight()
	{
		if (isallright)
		{
			img_jon.gameObject.SetActive(value: true);
			img_elsie.gameObject.SetActive(value: true);
			img_jon.DOFade(1f, 1f);
			img_elsie.DOFade(1f, 1f);
			img_role1.DOFade(0f, 1f);
			img_role2.DOFade(0f, 1f);
		}
		return isallright;
	}

	public void CheckStep01()
	{
		Debug.Log("CheckStep01");
		int num = 3;
		int num2 = 0;
		for (int i = 0; i < reasoningDragBlanklist.Count; i++)
		{
			Debug.Log("answer:" + reasoningDragBlanklist[i].gameObject.name + "::" + reasoningDragBlanklist[i].answer);
			if (reasoningDragBlanklist[i].answer != 0)
			{
				num2++;
			}
			if (!step01answer.Contains(reasoningDragBlanklist[i].answer.ToString()))
			{
				num--;
			}
		}
		Debug.Log("answercount:" + num2 + ":::isallright:" + num);
		correctcount = num2;
		if (num == 3)
		{
			reasoning4003Step01.Gotonext();
		}
		else if (num2 == 3)
		{
			for (int j = 0; j < reasoningDragBlanklist.Count; j++)
			{
				reasoningDragBlanklist[j].SetWrong();
			}
			Invoke("ResetAllDragBlankStep01", 1f);
		}
	}

	public void CheckStep02()
	{
		if (step02answer == step02dragblank.answer)
		{
			reasonint4003Step02.LastStep();
			return;
		}
		step02dragblank.SetWrong();
		Invoke("ResetDragBlankStep02", 1f);
	}

	private void ResetAllDragBlankStep01()
	{
		for (int i = 0; i < reasoningDragBlanklist.Count; i++)
		{
			reasoningDragBlanklist[i].ResetBlank();
		}
		for (int j = 0; j < reasoningDragRolelist.Count; j++)
		{
			reasoningDragRolelist[j].ResetRole();
		}
	}

	private void ResetDragBlankStep02()
	{
		step02dragblank.ResetBlank();
		for (int i = 0; i < reasoningDragRoleliststep02.Count; i++)
		{
			reasoningDragRoleliststep02[i].ResetRole();
		}
	}
}
