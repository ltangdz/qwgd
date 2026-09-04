using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PhishingDialog1 : CustomDialog
{
	public GameObject list;

	public GameObject itemcontent;

	[HideInInspector]
	public string toUserID;

	[HideInInspector]
	public int toUserIndex;

	[HideInInspector]
	public string choiceLinkID;

	public Text titleText;

	[SerializeField]
	private GameObject txt_title2;

	private void Init()
	{
		gameManager.homeScene.phishing = this;
		ResetList();
	}

	public void ResetList()
	{
		toUserID = "";
		toUserIndex = 0;
		choiceLinkID = "";
		for (int i = 0; i < itemcontent.transform.childCount; i++)
		{
			Object.Destroy(itemcontent.transform.GetChild(i).gameObject);
		}
		string eventId = gameManager.player.GetEventId();
		List<DATA33> all33Items = gameManager.dataManager.GetAll33Items(eventId);
		for (int j = 0; j < all33Items.Count; j++)
		{
			DATA33 dATA = all33Items[j];
			bool flag = false;
			if (gameManager.Is_Dlc7())
			{
				flag = gameManager.player.playerdata.dlc7Invades[j] >= 1;
			}
			else if (dATA.ID == 3310000)
			{
				flag = gameManager.player.playerdata.isDecryptInvade;
			}
			else if (all33Items[j].condition.Trim().Equals("") || all33Items[j].condition.Trim().Equals("#0"))
			{
				flag = true;
			}
			else
			{
				int num = 0;
				string[] array = all33Items[j].condition.Substring(1).Split(';');
				for (int k = 0; k < array.Length; k++)
				{
					if (gameManager.player.playerdata.itemlist.Contains(array[k]) || gameManager.isbug)
					{
						num++;
					}
				}
				flag = ((num == array.Length) ? true : false);
			}
			if (flag && all33Items[j].method != "")
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetFishItemName()), itemcontent.transform);
				obj.name = "fishitem" + j;
				if (!gameManager.player.playerdata.fishLink.ContainsKey(all33Items[j].name))
				{
					int value = 0;
					gameManager.player.playerdata.fishLink.Add(all33Items[j].name, value);
				}
				obj.GetComponent<FishItem>().Init(all33Items[j], gameManager, this);
			}
		}
	}

	public override void BeforeShowSize()
	{
		toUserID = "";
		toUserIndex = 0;
		choiceLinkID = "";
		for (int i = 0; i < itemcontent.transform.childCount; i++)
		{
			Object.Destroy(itemcontent.transform.GetChild(i).gameObject);
		}
		string eventId = gameManager.player.GetEventId();
		List<DATA33> all33Items = gameManager.dataManager.GetAll33Items(eventId);
		bool flag = false;
		for (int j = 0; j < all33Items.Count; j++)
		{
			DATA33 dATA = all33Items[j];
			if (gameManager.Is_Dlc7() && !flag)
			{
				flag = gameManager.player.playerdata.dlc7Invades[j] >= 1;
				continue;
			}
			if (dATA.ID == 3310000)
			{
				flag = gameManager.player.playerdata.isDecryptInvade;
				continue;
			}
			if (all33Items[j].condition.Trim().Equals("") || all33Items[j].condition.Trim().Equals("#0"))
			{
				flag = true;
				continue;
			}
			int num = 0;
			string[] array = all33Items[j].condition.Substring(1).Split(';');
			for (int k = 0; k < array.Length; k++)
			{
				if (gameManager.player.playerdata.itemlist.Contains(array[k]) || gameManager.isbug)
				{
					num++;
				}
			}
			flag = ((num == array.Length) ? true : false);
		}
		if (!flag)
		{
			txt_title2.SetActive(value: true);
			bk.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 222f, 0f);
			content.GetComponent<RectTransform>().sizeDelta = new Vector2(745f, 130f);
			content.Find("content1").GetComponent<RectTransform>().sizeDelta = new Vector2(745f, 130f);
			content.Find("content1/Scroll View").gameObject.SetActive(value: false);
			if (gameManager.Is_Dlc7())
			{
				titleText.GetComponent<RectTransform>().anchoredPosition = new Vector3(73f, -33f, 0f);
				btn_close.GetComponent<RectTransform>().anchoredPosition = new Vector3(419f, -2.6f, 0f);
			}
			else
			{
				titleText.GetComponent<RectTransform>().anchoredPosition = new Vector3(50f, -41f, 0f);
				btn_close.GetComponent<RectTransform>().anchoredPosition = new Vector3(665f, -39f, 0f);
			}
			height = 130f;
		}
		else if (gameManager.Is_Dlc6() || gameManager.IsBasic())
		{
			titleText.GetComponent<RectTransform>().anchoredPosition = new Vector3(53f, gameManager.IsAllDlc() ? (-66) : (-40), 0f);
			btn_close.GetComponent<RectTransform>().anchoredPosition = new Vector3(655f, gameManager.IsAllDlc() ? (-66) : (-40), 0f);
		}
		Init();
	}

	public override void AfterShowSize()
	{
	}
}
