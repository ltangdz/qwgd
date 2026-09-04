using System.Collections.Generic;
using UnityEngine;

public class ReasoningMiddleCustom : ReasoningMiddle
{
	public ReasoningPanel reasoningPanel;

	public List<ProcessItem> processItems;

	public bool isallright;

	private GameManager gameManager;

	public override bool IsAllRight()
	{
		return isallright;
	}

	public override void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
}
