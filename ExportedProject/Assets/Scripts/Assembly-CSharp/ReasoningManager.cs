using System.Collections.Generic;
using UnityEngine;
using tnt_deploy;

public class ReasoningManager : MonoBehaviour
{
	public int pos;

	public List<ReasoningPanel> list = new List<ReasoningPanel>();

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.reasoningManager = this;
		Init();
	}

	public void ShowReasonPreVideo(string id)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.name.Equals("ReasoningPanel" + id))
			{
				base.transform.Find("ReasoningPanel" + id).GetComponent<ReasoningPanel>().ShowPreVideo();
				break;
			}
		}
	}

	public void ShowReasoningPanel(string id, bool isover)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.name.Equals("ReasoningPanel" + id))
			{
				ReasoningPanel component = base.transform.Find("ReasoningPanel" + id).GetComponent<ReasoningPanel>();
				if (isover)
				{
					component.IsAllCompeleted();
				}
				else
				{
					component.Show();
				}
				break;
			}
		}
	}

	private void Init()
	{
		DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		if (!dATA.need_reason.Equals("#0"))
		{
			string[] array = dATA.need_reason.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("ReasoningPanel/ReasoningPanel" + array[i]), base.transform);
				gameObject.name = "ReasoningPanel" + array[i];
				list.Add(gameObject.GetComponent<ReasoningPanel>());
			}
		}
	}
}
