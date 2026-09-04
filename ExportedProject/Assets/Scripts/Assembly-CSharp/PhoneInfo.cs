using System.Collections;
using System.Collections.Generic;
using Honeti;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class PhoneInfo : MonoBehaviour
{
	public Image avatar;

	public Text userName;

	public Text phoneNum;

	public GameObject content;

	public GameObject noClose;

	public GameObject noHangup;

	public Button hangup;

	public GameObject selectCanvas;

	public ScrollRect scrollBox;

	public Button noDestroy;

	private string id;

	private PhoneCallDialog parObj;

	private GameManager gameManager;

	private bool startTalk = true;

	private string[] reply;

	private string chatLabelID = "";

	private int target;

	private bool openSel;

	public bool saying;

	private IEnumerator run;

	private float waitTime;

	public List<MultiplyText> multiplytextlist = new List<MultiplyText>();

	private bool taskSuccess;

	public Dictionary<string, List<string>> phoneRecord = new Dictionary<string, List<string>>();

	public string getID => id;

	private void Update()
	{
	}

	public void Init(string userID, PhoneCallDialog par, GameManager gm, int callType)
	{
		id = userID;
		parObj = par;
		gameManager = gm;
		if (gameManager.Is_Dlc6() && gameManager.issteam)
		{
			SteamUserStats.GetAchievement("detialman", out var _);
		}
		if (callType == 0)
		{
			gameManager.musicManager.LowerVol();
		}
		taskSuccess = ((callType != 0) ? true : false);
		hangup.onClick.RemoveAllListeners();
		hangup.onClick.AddListener(delegate
		{
			gameManager.CanShowSetting(-1);
			gameManager.musicManager.ResumeVol();
			parObj.Hide();
			gameManager.istaohuashow = false;
			gameManager.soundManager.Stop();
			parObj.chatBak.SetActive(value: false);
			Debug.Log("是否出套话失败：" + !taskSuccess);
			if (!taskSuccess)
			{
				gameManager.homeScene.taskFail("phone");
			}
			else
			{
				gameManager.homeScene.ShowNextVideo();
				if (id == "3700008")
				{
					gameManager.homeScene.notebook.AddNewItem("10598");
				}
				else if (id == "3700009")
				{
					gameManager.homeScene.computerButtonBox.OpenTool(15);
				}
			}
		});
		string head = gameManager.dataManager.dic37[userID].head;
		string key = gameManager.dataManager.dic37[userID].name;
		string phone = gameManager.dataManager.dic37[userID].phone;
		avatar.sprite = Resources.Load<Sprite>("phone/" + head);
		userName.GetComponent<I18NText>().updateTranslation2(key);
		phoneNum.GetComponent<I18NText>().updateTranslation2(phone);
		string text = (chatLabelID = gameManager.dataManager.dic37[id].reply.Substring(1));
		target = ((!(gameManager.dataManager.dic38[chatLabelID].frdreply.Trim() == "")) ? 1 : 0);
		string secondcall = gameManager.dataManager.dic37[userID].secondcall;
		if (callType == 0)
		{
			if (secondcall != "" && secondcall != "#1")
			{
				ShowOrig(secondcall.Substring(1), isCalled: false);
			}
			gameManager.istaohuashow = true;
			parObj.hotarea.SetActive(value: true);
			StartCoroutine(StartTalking(text, target, openSel));
			parObj.hotarea.GetComponent<Button>().onClick.RemoveAllListeners();
			parObj.hotarea.GetComponent<Button>().onClick.AddListener(delegate
			{
				if (!saying)
				{
					saying = true;
					if (run != null)
					{
						StopCoroutine(run);
					}
					parObj.mouse.SetActive(value: false);
					StartCoroutine(StartTalking(chatLabelID, target, openSel));
				}
			});
			noDestroy.GetComponent<Button>().onClick.RemoveAllListeners();
			noDestroy.GetComponent<Button>().onClick.AddListener(delegate
			{
				if (!saying)
				{
					saying = true;
					if (run != null)
					{
						StopCoroutine(run);
					}
					parObj.mouse.SetActive(value: false);
					StartCoroutine(StartTalking(chatLabelID, target, openSel));
				}
			});
		}
		else if (secondcall != "")
		{
			ShowOrig(secondcall.Substring(1));
		}
		else
		{
			ShowOrig();
		}
	}

	private IEnumerator ShowMouse()
	{
		yield return new WaitForSeconds(waitTime + 3f);
		if (parObj != null && parObj.mouse != null)
		{
			parObj.mouse.SetActive(value: true);
		}
	}

	private IEnumerator StartTalking(string reply, int replyType, bool select)
	{
		Debug.Log(reply + ":" + replyType);
		if (!select)
		{
			bool flag = false;
			if (phoneRecord.ContainsKey(id) && phoneRecord[id].Contains(reply))
			{
				flag = true;
			}
			if (!flag)
			{
				saying = true;
				if (startTalk && replyType == 0)
				{
					gameManager.soundManager.Stop();
					string label = gameManager.dataManager.dic38[reply].content;
					Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPhoneItemBakName()), content.transform).GetComponent<PhoneItemBak>().Init(label, this, gameManager);
					LineToBottom();
					float num = gameManager.soundManager.PlayDLCEventSound(gameManager.player.GetEventId(), getID, reply);
					waitTime = num;
					yield return new WaitForSeconds(0.5f);
				}
				else if (startTalk && replyType == 1)
				{
					int num2 = -1;
					if (!gameManager.dataManager.dic38[reply].sound.Equals(""))
					{
						num2 = int.Parse(gameManager.dataManager.dic38[reply].sound.Split(':')[1]);
					}
					gameManager.soundManager.Stop();
					Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPhoneItemName()), content.transform).GetComponent<PhoneItemInfo>().Init(gameManager.dataManager.dic38[reply], this, gameManager, gameManager.dataManager.dic38[reply].collectID.Substring(1), 0, num2);
					if (num2 != -1)
					{
						waitTime = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), num2, playAudio: false);
						yield return new WaitForSeconds(1.5f);
					}
					if (gameManager.Is_Dlc7())
					{
						waitTime = gameManager.soundManager.PlayDLCEventSound(gameManager.player.GetEventId(), getID, reply, playAudio: false);
						yield return new WaitForSeconds(1.5f);
					}
				}
				if (phoneRecord.ContainsKey(id))
				{
					phoneRecord[id].Add(reply);
				}
				else
				{
					List<string> list = new List<string>();
					list.Add(reply);
					phoneRecord.Add(id, list);
				}
				switch (gameManager.dataManager.dic38[reply].replyType)
				{
				case 0:
					if (run != null)
					{
						StopCoroutine(run);
					}
					run = ShowMouse();
					StartCoroutine(run);
					Debug.Log("getrun");
					openSel = true;
					saying = false;
					break;
				case 1:
				{
					if (run != null)
					{
						StopCoroutine(run);
					}
					string text2 = gameManager.dataManager.dic38[reply].replyBtn.Substring(1);
					chatLabelID = text2;
					target = 1;
					openSel = false;
					saying = true;
					yield return new WaitForSeconds(waitTime);
					StartCoroutine(StartTalking(chatLabelID, target, openSel));
					break;
				}
				case 2:
				{
					if (id != "3700011")
					{
						Object.Instantiate(Resources.Load<GameObject>("phone_hangdown"), content.transform);
					}
					startTalk = false;
					parObj.hotarea.SetActive(value: false);
					noDestroy.GetComponent<Button>().onClick.RemoveAllListeners();
					gameManager.musicManager.LowerVol();
					gameManager.istaohuashow = false;
					for (int i = 0; i < multiplytextlist.Count; i++)
					{
						multiplytextlist[i].SetIscanaddtoItem(iscan: true);
					}
					LineToBottom();
					int endType = gameManager.dataManager.dic38[reply].EndType;
					taskSuccess = ((endType != 1) ? true : false);
					Debug.Log("任务是否成功：" + taskSuccess.ToString() + " " + endType);
					if (taskSuccess)
					{
						string secondcall = gameManager.dataManager.dic37[id].secondcall;
						if (endType == 0 || endType == 1)
						{
							if (!gameManager.player.playerdata.phoneRecord.ContainsKey(id))
							{
								if (secondcall != "")
								{
									secondcall = secondcall.Substring(1);
									List<string> value = gameManager.player.playerdata.phoneRecord[secondcall];
									gameManager.player.playerdata.phoneRecord.Add(id, value);
									for (int j = 0; j < phoneRecord[id].Count; j++)
									{
										gameManager.player.playerdata.phoneRecord[id].Add(phoneRecord[id][j]);
									}
								}
								else
								{
									gameManager.player.playerdata.phoneRecord.Add(id, phoneRecord[id]);
								}
								gameManager.player.playerdata.phoneCall.Add(id);
								if (id == "3700012")
								{
									gameManager.player.playerdata.canPlayHideGame = true;
								}
							}
							gameManager.player.playerdata.camFailedTime["phone"] = 0;
							gameManager.saveManager.SavePlayerData();
						}
						else if (endType == 2 && secondcall != "")
						{
							secondcall = secondcall.Substring(1);
							if (!gameManager.player.playerdata.phoneCall.Contains(id))
							{
								gameManager.player.playerdata.phoneCall.Add(id);
							}
							if (!gameManager.player.playerdata.phoneRecord.ContainsKey(secondcall))
							{
								gameManager.player.playerdata.phoneRecord.Add(secondcall, phoneRecord[id]);
							}
							else
							{
								for (int k = 0; k < phoneRecord[id].Count; k++)
								{
									gameManager.player.playerdata.phoneRecord[secondcall].Add(phoneRecord[id][k]);
								}
							}
							if (gameManager.player.playerdata.calledStep[secondcall].Count > 1)
							{
								gameManager.player.playerdata.calledStep[secondcall].Remove(id);
								Debug.Log("删除的id：" + id);
							}
							gameManager.saveManager.SavePlayerData();
						}
						else if (endType == 4)
						{
							saying = false;
							gameManager.istaohuashow = false;
							yield return new WaitForSeconds(6f);
							Debug.Log("显示2次:3700077");
							gameManager.homeScene.ShowVideoTip("3700077");
							parObj.Close();
							gameManager.saveManager.SavePlayerData();
						}
					}
					saying = false;
					noClose.SetActive(value: false);
					noHangup.SetActive(value: false);
					parObj.chatBak.GetComponent<ChatBak>().HideBlack();
					break;
				}
				case 3:
				{
					if (run != null)
					{
						StopCoroutine(run);
					}
					run = ShowMouse();
					StartCoroutine(run);
					string text = gameManager.dataManager.dic38[reply].replyBtn.Substring(1);
					chatLabelID = text;
					target = 0;
					openSel = false;
					saying = false;
					break;
				}
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(0.1f);
			string[] anwser = GetAnwser(reply);
			selectCanvas.gameObject.SetActive(value: true);
			selectCanvas.GetComponent<SelectGroup>().SetSelect(anwser, ClickSelect);
			saying = false;
		}
		if (!(reply == "3810040"))
		{
			yield break;
		}
		Debug.Log("套话成功");
		if (gameManager.issteam && gameManager.steamAchi != null && gameManager.Is_Dlc6())
		{
			Debug.Log("issteam");
			if (!gameManager.steamAchi.GetAchievement("detialman"))
			{
				Debug.Log("没有成就：detialman");
				gameManager.UnlockAchievements("detialman");
			}
		}
	}

	private void ShowOrig(string userid = "", bool isCalled = true)
	{
		userid = ((userid == "") ? id : userid);
		Dictionary<string, List<string>> dictionary = gameManager.player.playerdata.phoneRecord;
		if (!dictionary.ContainsKey(userid))
		{
			Debug.LogError("phoneRecord:" + userid);
			gameManager.player.playerdata.phoneCall.Remove(userid);
			gameManager.saveManager.SavePlayerData();
			return;
		}
		for (int i = 0; i < dictionary[userid].Count; i++)
		{
			Debug.Log(dictionary[userid][i]);
			if (gameManager.dataManager.dic38[dictionary[userid][i]].frdreply.Trim() == "")
			{
				string label = gameManager.dataManager.dic38[dictionary[userid][i]].content;
				Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPhoneItemBakName()), content.transform).GetComponent<PhoneItemBak>().Init(label, this, gameManager);
			}
			else
			{
				Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPhoneItemName()), content.transform).GetComponent<PhoneItemInfo>().Init(gameManager.dataManager.dic38[dictionary[userid][i]], this, gameManager, gameManager.dataManager.dic38[dictionary[userid][i]].collectID.Substring(1), -1);
			}
		}
		if (isCalled)
		{
			startTalk = false;
			noClose.SetActive(value: false);
			noHangup.SetActive(value: false);
			parObj.chatBak.SetActive(value: false);
		}
		LineToBottom();
	}

	public void ClickSelect(int poss)
	{
		if (!saying)
		{
			run = ShowMouse();
			StartCoroutine(run);
			string text = reply[poss];
			chatLabelID = text;
			target = 0;
			openSel = false;
			StartCoroutine(StartTalking(chatLabelID, target, openSel));
			selectCanvas.GetComponent<SelectGroup>().HideSelect();
		}
	}

	public void LineToBottom()
	{
		Invoke("ToBottom", 0.3f);
	}

	public void LineToBottom(ScrollRect scrollRect)
	{
		StartCoroutine(ToBottom(scrollRect));
	}

	private IEnumerator ToBottom(ScrollRect scrollRect)
	{
		yield return new WaitForSeconds(1f);
		scrollBox.normalizedPosition = Vector3.zero;
	}

	public void ToBottom()
	{
		scrollBox.normalizedPosition = Vector3.zero;
	}

	private string[] GetAnwser(string crtChatID)
	{
		reply = gameManager.dataManager.dic38[crtChatID].replyBtn.Substring(1).Split(';');
		string[] array = new string[reply.Length];
		for (int i = 0; i < reply.Length; i++)
		{
			array[i] = gameManager.dataManager.dic38[reply[i]].title;
		}
		return array;
	}
}
