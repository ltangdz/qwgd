using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterpipeManager : MonoBehaviour
{
	public bool iscanclick = true;

	public WaterpipeItem startitem;

	public List<WaterpipeItem> waterpipeItems = new List<WaterpipeItem>();

	public GameObject overPanel;

	public bool isEnd;

	public ZhadanInvade1 zhadanInvade;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		startitem.Next(isgreen: true);
	}

	public void Check()
	{
		for (int i = 0; i < waterpipeItems.Count; i++)
		{
			waterpipeItems[i].SetGreen(isgreen: false);
		}
		startitem.Next(isgreen: true);
		bool flag = true;
		for (int j = 0; j < waterpipeItems.Count; j++)
		{
			if (!waterpipeItems[j].isgreen)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			overPanel.SetActive(value: true);
			overPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f);
			Invoke("HidePanel", 2f);
			iscanclick = false;
			gameManager.saveManager.SavePlayerData();
		}
	}

	private void HidePanel()
	{
		if (!isEnd)
		{
			Object.Instantiate(Resources.Load<Transform>("zhadan/zhadan03"), base.transform.parent).SetSiblingIndex(0);
			gameManager.homeScene.zhadanInvade.codeRunBox.ShowWhileTrue("^zhadan_label18");
			Object.Destroy(base.gameObject);
			return;
		}
		if (!gameManager.player.playerdata.OpenedMail.Contains("1500089"))
		{
			gameManager.player.playerdata.OpenedMail.Add("1500089");
			gameManager.homeScene.zhadanInvade.GameOver(isOver: false);
		}
		gameManager.musicManager.PlayMusicLoop(6);
		zhadanInvade.GameOver();
	}
}
