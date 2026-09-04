using System.Collections.Generic;
using UnityEngine;

public class PhoneNumList : MonoBehaviour
{
	public GameObject listContent;

	public GameObject noclick;

	[HideInInspector]
	public PhoneCallDialog parObj;

	private GameManager gameManager;

	public void Init(PhoneCallDialog par, GameManager gm)
	{
		parObj = par;
		gameManager = gm;
	}

	public void ShowList(string userID)
	{
		string text = gameManager.dataManager.dic37[userID].secondcall;
		Debug.Log(userID + "%%" + text);
		bool flag = false;
		if (text != "")
		{
			if ((!gameManager.player.playerdata.calledStep.ContainsKey(text.Substring(1)) || !gameManager.player.playerdata.calledStep[text.Substring(1)].Contains(userID)) && !gameManager.player.playerdata.phoneCall.Contains(userID))
			{
				text = text.Substring(1);
				if (!gameManager.player.playerdata.calledStep.ContainsKey(text))
				{
					List<string> list = new List<string>();
					list.Add(userID);
					gameManager.player.playerdata.calledStep.Add(text, list);
				}
				else if (!gameManager.player.playerdata.calledStep[text].Contains(userID))
				{
					gameManager.player.playerdata.calledStep[text].Add(userID);
				}
				gameManager.saveManager.SavePlayerData();
			}
			flag = CanShowList(userID, text);
		}
		if (string.IsNullOrEmpty(text) || flag)
		{
			Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPhoneListName()), listContent.transform).GetComponent<PhoneListItem>().Init(userID, this, gameManager);
			return;
		}
		Debug.LogError("无法显示：" + userID + ":" + text + "**");
	}

	private bool CanShowList(string id, string sameid)
	{
		sameid = ((sameid.IndexOf("#") > -1) ? sameid.Substring(1) : sameid);
		bool result = false;
		Debug.Log("sameid::" + sameid);
		if (gameManager.player.playerdata.calledStep.ContainsKey(sameid))
		{
			Debug.Log(id + " " + gameManager.player.playerdata.calledStep[sameid][0]);
			List<string> list = gameManager.player.playerdata.calledStep[sameid];
			if (id == list[0])
			{
				result = true;
			}
		}
		return result;
	}
}
