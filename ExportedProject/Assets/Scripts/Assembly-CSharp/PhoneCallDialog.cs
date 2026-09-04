using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PhoneCallDialog : CustomDialog
{
	public GameObject numList;

	public GameObject phoneCall;

	public GameObject talkInfo;

	public GameObject chatBak;

	public GameObject hotarea;

	public GameObject mouse;

	public GameObject title;

	private string eventid;

	private string choicePersonID;

	private List<DATA37> phone = new List<DATA37>();

	public GameObject img_dragarea;

	public Canvas canvasself;

	public GraphicRaycaster graphicRaycaster;

	public void ShowNormal()
	{
		Object.Destroy(graphicRaycaster);
		Object.Destroy(canvasself);
	}

	private void Start()
	{
		gameManager.homeScene.phoneDialog = this;
		gameManager.CanShowSetting(1);
		btn_close.onClick.AddListener(delegate
		{
			gameManager.CanShowSetting(-1);
		});
	}

	private IEnumerator StartFun()
	{
		Debug.LogError("phone.Count:" + phone.Count);
		if (phone.Count != 0)
		{
			numList.GetComponent<PhoneNumList>().Init(this, gameManager);
			for (int i = 0; i < phone.Count; i++)
			{
				numList.GetComponent<PhoneNumList>().ShowList(phone[i].ID.ToString());
				yield return new WaitForSeconds(0.1f);
			}
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void ShowCourse()
	{
		chatBak.SetActive(value: true);
		chatBak.GetComponent<ChatBak>().ShowCourse();
		img_dragarea.SetActive(value: false);
	}

	public void PhoneCalling(string id, int callType)
	{
		choicePersonID = id;
		StartCoroutine(ChangeSceneToCalling(callType));
	}

	private IEnumerator ChangeSceneToCalling(int callType)
	{
		bool canCalling = true;
		numList.GetComponent<RectTransform>().DOSizeDelta(new Vector2(464f, 0f), 0.5f);
		yield return new WaitForSeconds(0.5f);
		Debug.Log(gameManager.player.playerdata.phoneRecord.ContainsKey(choicePersonID));
		if (!gameManager.player.playerdata.phoneRecord.ContainsKey(choicePersonID))
		{
			phoneCall.SetActive(value: true);
			phoneCall.GetComponent<PhoneCalling>().Init(choicePersonID, this, gameManager);
			yield return new WaitForSeconds(3f);
			string empty = gameManager.dataManager.dic37[choicePersonID].empty;
			string video = gameManager.dataManager.dic37[choicePersonID].video;
			if (empty.Trim() == "1")
			{
				phoneCall.GetComponent<PhoneCalling>().StopRip(empty: true, video: false);
				canCalling = false;
			}
			else if (video.Trim() != "")
			{
				bool flag = false;
				if (video.Trim().StartsWith("#4"))
				{
					if (gameManager.player.playerdata.reasoninglist.Contains(video.Trim().Substring(1)))
					{
						flag = true;
					}
				}
				else
				{
					string[] array = video.Substring(1).Split('*');
					for (int i = 0; i < array.Length; i++)
					{
						if (gameManager.player.playerdata.videotiplist.Contains(array[i]))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					phoneCall.GetComponent<PhoneCalling>().StopRip(empty: false, video: false);
					Debug.Log("添加的id:" + choicePersonID);
				}
				else
				{
					canCalling = false;
					phoneCall.GetComponent<PhoneCalling>().StopRip(empty: false, video: true);
				}
			}
			else
			{
				phoneCall.GetComponent<PhoneCalling>().StopRip(empty: false, video: false);
				Debug.Log("添加的id:" + choicePersonID);
			}
		}
		if (canCalling)
		{
			talkInfo.gameObject.SetActive(value: true);
			talkInfo.GetComponent<PhoneInfo>().Init(choicePersonID, this, gameManager, callType);
			btn_close.gameObject.SetActive(value: false);
		}
	}

	public override void BeforeShowSize()
	{
		eventid = gameManager.player.GetEventId();
		List<DATA37> all37Items = gameManager.dataManager.GetAll37Items(eventid);
		for (int i = 0; i < all37Items.Count; i++)
		{
			string condition = all37Items[i].condition;
			string failcondition = all37Items[i].failcondition;
			if (!(condition.Trim() != "") && !gameManager.isbug)
			{
				continue;
			}
			bool flag = true;
			if (!gameManager.isbug)
			{
				if (condition.StartsWith("#4"))
				{
					if (!gameManager.player.playerdata.reasoninglist.Contains(condition.Substring(1)) && !gameManager.isbug)
					{
						flag = false;
					}
				}
				else
				{
					string[] array = condition.Substring(1).Split(';');
					for (int j = 0; j < array.Length; j++)
					{
						if (!gameManager.player.playerdata.itemlist.Contains(array[j]) && !gameManager.isbug)
						{
							Debug.LogError("itemmeiyou:" + array[j]);
							flag = false;
						}
						if (!string.IsNullOrEmpty(failcondition) && !gameManager.player.playerdata.phoneCall.Contains(all37Items[i].ID.ToString()) && gameManager.player.playerdata.reasoninglist.Contains(failcondition.Substring(1)))
						{
							flag = false;
						}
					}
				}
				if (all37Items[i].ID.ToString() == "3700011")
				{
					string[] array2 = all37Items[i].video.Substring(1).Split('*');
					bool flag2 = false;
					for (int k = 0; k < array2.Length; k++)
					{
						if (gameManager.player.playerdata.videotiplist.Contains(array2[k]))
						{
							flag2 = true;
						}
					}
					flag = flag2;
				}
			}
			if (flag)
			{
				phone.Add(all37Items[i]);
			}
		}
		if (phone.Count == 0)
		{
			content.GetComponent<RectTransform>().sizeDelta = new Vector2(464f, 130f);
			numList.GetComponent<RectTransform>().localPosition = new Vector2(0f, 0f);
			numList.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			numList.GetComponent<RectTransform>().sizeDelta = new Vector2(464f, 130f);
			height = 130f;
			title.GetComponent<Text>().fontSize = 30;
			title.GetComponent<I18NText>().updateTranslation2("^no_phone");
			bk.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 375f, 0f);
			bk.SetActive(value: true);
		}
		else
		{
			title.SetActive(value: false);
		}
		gameManager.saveManager.SavePlayerData();
	}

	public override void AfterShowSize()
	{
		StartCoroutine(StartFun());
	}
}
