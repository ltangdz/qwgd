using System.Collections.Generic;
using UnityEngine;

public class ResultListPanel : MonoBehaviour
{
	public List<GameObject> items = new List<GameObject>();

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void ShowReport(string reportname)
	{
		Object.Instantiate(Resources.Load("Houtai/" + reportname) as GameObject, base.gameObject.transform).GetComponent<ReasonPic>().Show();
		gameManager.player.playerdata.isopenreport = true;
		gameManager.saveManager.SavePlayerData();
	}

	public void ShowItems(int type)
	{
		for (int i = 0; i < items.Count; i++)
		{
			items[i].SetActive(value: false);
		}
		switch (type)
		{
		case 0:
			items[0].SetActive(value: true);
			items[1].SetActive(value: true);
			items[2].SetActive(value: true);
			items[3].SetActive(value: true);
			break;
		case 1:
			items[4].SetActive(value: true);
			items[5].SetActive(value: true);
			items[6].SetActive(value: true);
			items[7].SetActive(value: true);
			break;
		case 2:
			items[8].SetActive(value: true);
			items[9].SetActive(value: true);
			items[12].SetActive(value: true);
			break;
		case 3:
			items[10].SetActive(value: true);
			items[11].SetActive(value: true);
			items[13].SetActive(value: true);
			break;
		}
	}
}
