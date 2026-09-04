using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningMiddle4001 : ReasoningMiddle
{
	public ReasoningPanel reasoningPanel;

	public List<ProcessItem> processItems;

	public bool isallright;

	public Button btn_sure;

	private GameManager gameManager;

	private bool canClick = true;

	public override bool IsAllRight()
	{
		int num = 0;
		for (int i = 0; i < processItems.Count; i++)
		{
			if (!processItems[i].IsRight())
			{
				processItems[i].SetWrong();
				continue;
			}
			processItems[i].SetRight();
			num++;
		}
		isallright = num == processItems.Count;
		return isallright;
	}

	public override void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(InitMiddle());
	}

	private IEnumerator InitMiddle()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < processItems.Count; i++)
		{
			processItems[i].Init();
			yield return new WaitForSeconds(0.3f);
		}
		btn_sure.gameObject.SetActive(value: true);
		btn_sure.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		btn_sure.onClick.AddListener(delegate
		{
			if (canClick)
			{
				canClick = false;
				if (reasoningPanel.GetResult())
				{
					btn_sure.gameObject.SetActive(value: false);
					StartCoroutine(Sure());
				}
				_ = gameManager.soundManager.event01[reasoningPanel.sounds[1]].length;
				Invoke("BtnCanClick", 0.5f);
			}
		});
	}

	private void BtnCanClick()
	{
		canClick = true;
	}

	private IEnumerator Sure()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < processItems.Count; i++)
		{
			processItems[i].ShowResult();
			yield return new WaitForSeconds(0.5f);
		}
	}
}
