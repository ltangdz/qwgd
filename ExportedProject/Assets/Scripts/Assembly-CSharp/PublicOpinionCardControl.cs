using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;
using _DLC8.Game.PublicOpinion.Card;

public class PublicOpinionCardControl : MonoBehaviour
{
	public PublicOpinionController controller;

	public List<PublicOpinionInfo> cardInfos;

	public PublicOpinionCounter counter;

	public List<PublicOpinionBag> bags;

	public Button btnRun;

	public PublicOpinionCard cardPrefab;

	public PublicOpinionCardIdle cardIdlePrefab;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnRun.onClick.AddListener(CalData);
	}

	private void CalData()
	{
		List<PublicOpinionInfo> list = new List<PublicOpinionInfo>();
		for (int i = 0; i < bags.Count; i++)
		{
			PublicOpinionBag publicOpinionBag = bags[i];
			if (publicOpinionBag.infos == null)
			{
				continue;
			}
			for (int j = 0; j < publicOpinionBag.infos.Count; j++)
			{
				PublicOpinionInfo publicOpinionInfo = publicOpinionBag.infos[j];
				if (publicOpinionInfo.positionType == PositionType.IDLE)
				{
					return;
				}
				list.Add(publicOpinionInfo);
			}
		}
		controller.StartBalance(list);
		base.gameObject.SetActive(value: false);
	}

	public void Show(List<PublicOpinionInfo> newsInfo)
	{
		cardInfos = newsInfo;
		for (int i = 0; i < cardInfos.Count; i++)
		{
			cardInfos[i].roleNum = 0;
		}
		for (int j = 0; j < bags.Count; j++)
		{
			bags[j].InitData(this);
		}
		Debug.Log("初始化newscontrol：1");
		Debug.Log("初始化newscontrol：2");
		counter.Init(this);
		Debug.Log("初始化newscontrol：3");
		base.gameObject.SetActive(value: true);
	}

	public PublicOpinionInfo GetCurCardInfo()
	{
		List<PublicOpinionInfo> infos = bags[2].infos;
		int count = infos.Count;
		if (count > 0)
		{
			return infos[count - 1];
		}
		return null;
	}

	public int UsedPersonCount()
	{
		int num = 0;
		for (int i = 0; i < bags.Count; i++)
		{
			PublicOpinionBag publicOpinionBag = bags[i];
			if (publicOpinionBag.infos != null)
			{
				for (int j = 0; j < publicOpinionBag.infos.Count; j++)
				{
					num += (int)publicOpinionBag.infos[j].roleNum;
				}
			}
		}
		return num;
	}
}
