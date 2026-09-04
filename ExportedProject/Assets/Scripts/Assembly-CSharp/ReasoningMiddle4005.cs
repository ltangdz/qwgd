using UnityEngine;

public class ReasoningMiddle4005 : ReasoningMiddle
{
	public bool isallright;

	public ReasoningPanel reasoningPanel;

	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	private void Start()
	{
	}

	public override bool IsAllRight()
	{
		return isallright;
	}
}
