using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class NewZhadanDialog : MonoBehaviour
{
	public ZhadanInvoke zhadanInvoke;

	public ZhadanZimu txt_tishi;

	public InputField input;

	public Button btnSearch;

	public ZhadanCodeRun zhadanCode;

	public NewZhadanMap zhadanMap;

	public List<ZhadanInfo> zhadan;

	private GameManager gameManager;

	public List<ZhadanInfo> showList = new List<ZhadanInfo>();

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.zhibojiannotebook.DeleteHopePanel();
		gameManager.player.playerdata.isZhadanStart = true;
		gameManager.homeScene.computerButtonBox.appFun["13"].SetActive(value: true);
		if (!gameManager.player.playerdata.phoneCall.Contains("3700011"))
		{
			List<string> list = new List<string>();
			list.Add("3800335");
			gameManager.player.playerdata.phoneRecord.Add("3700011", list);
			gameManager.player.playerdata.phoneCall.Add("3700011");
		}
		gameManager.homeScene.newZhadanDialog = this;
		StartCoroutine(ShowObj());
		btnSearch.onClick.AddListener(RunGame);
		gameManager.musicManager.PlayMusicLoop(16);
	}

	public void StartGame()
	{
		FreshType();
		zhadanCode.Complete();
		btnSearch.interactable = true;
		if (showList.Count != 0)
		{
			for (int i = 0; i < showList.Count; i++)
			{
				showList[i].redPoint.GetComponent<ZhadanPos>().Hide();
			}
		}
	}

	private IEnumerator ShowObj()
	{
		yield return new WaitForSeconds(0.3f);
		GetComponent<RectTransform>().DOLocalMoveX(-423f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.SendMail("1500086");
	}

	private void RunGame()
	{
		string text = input.text;
		if (!(text.Replace(" ", "") != ""))
		{
			return;
		}
		input.text = "";
		List<string> openMail = gameManager.player.playerdata.OpenMail;
		GameObject gameObject = null;
		if (openMail[openMail.Count - 1] != "1500089")
		{
			gameObject = Object.Instantiate(Resources.Load<GameObject>("zhadan/newzhadanlogin"), gameManager.homeScene.middle);
		}
		if (text == I18N.instance.getValue("^zhadan_label05") && openMail[openMail.Count - 1] == "1500086")
		{
			gameObject.GetComponent<NewZhadanLogin>().Init(isSucce: true, "3300007");
			return;
		}
		if (text == I18N.instance.getValue("^zhadan_label14") && openMail[openMail.Count - 1] == "1500087")
		{
			gameObject.GetComponent<NewZhadanLogin>().Init(isSucce: true, "3300008");
			return;
		}
		if (text == I18N.instance.getValue("^zhadan_label15") && openMail[openMail.Count - 1] == "1500087")
		{
			gameObject.GetComponent<NewZhadanLogin>().Init(isSucce: true, "3300009");
			return;
		}
		if (text == I18N.instance.getValue("^zhadan_label17") && openMail[openMail.Count - 1] == "1500089" && gameManager.player.playerdata.canPlayHideGame)
		{
			gameManager.homeScene.ShowVideoTip("3700071");
			return;
		}
		if (text == I18N.instance.getValue("^zhadan_label17") && openMail[openMail.Count - 1] == "1500089" && !gameManager.player.playerdata.canPlayHideGame)
		{
			gameManager.homeScene.ShowVideoTip("3700072");
			return;
		}
		if (text == I18N.instance.getValue("^zhadan_label22") && openMail[openMail.Count - 1] == "1500089")
		{
			gameObject = Object.Instantiate(Resources.Load<GameObject>("zhadan/newzhadanlogin"), gameManager.homeScene.middle);
			gameObject.GetComponent<NewZhadanLogin>().Init(isSucce: true, "3300011");
			return;
		}
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(Resources.Load<GameObject>("zhadan/newzhadanlogin"), gameManager.homeScene.middle);
		}
		gameObject.GetComponent<NewZhadanLogin>().Init(isSucce: false);
	}

	public void ZhadanSuccess(string zhadanid, bool isEMP = false)
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		switch (zhadanid)
		{
		case "3300007":
			if (gameManager.homeScene.zhadanInvade != null)
			{
				gameManager.homeScene.zhadanInvade.GameOver();
			}
			if (!gameManager.player.playerdata.OpenedMail.Contains("1500086"))
			{
				gameManager.player.playerdata.OpenedMail.Add("1500086");
			}
			gameManager.homeScene.SendMail("1500087");
			btnSearch.interactable = false;
			gameManager.homeScene.zhadanInvoke.CrackSucc();
			break;
		case "3300008":
			if (gameManager.homeScene.zhadanInvade != null)
			{
				gameManager.homeScene.zhadanInvade.GameOver();
			}
			break;
		case "3300009":
			if (!isEMP)
			{
				if (gameManager.homeScene.zhadanInvade != null)
				{
					gameManager.homeScene.zhadanInvade.GameOver();
				}
				if (gameManager.homeScene.zhadanInvoke != null)
				{
					gameManager.homeScene.zhadanInvoke.ResetInterval();
				}
				if (!gameManager.player.playerdata.OpenedMail.Contains("1500087"))
				{
					gameManager.player.playerdata.OpenedMail.Add("1500087");
				}
				gameManager.player.playerdata.boomList[1] = "0";
				gameManager.player.playerdata.boomList[2] = "0";
				gameManager.homeScene.SendMail("1500088");
				break;
			}
			goto default;
		default:
			if (zhadanid == "3300009" && isEMP)
			{
				if (gameManager.homeScene.zhadanInvoke != null)
				{
					gameManager.homeScene.zhadanInvoke.Success();
				}
			}
			else if (zhadanid == "3300010")
			{
				gameManager.player.playerdata.zhadanhide = false;
				gameManager.player.playerdata.completeHideGame = true;
			}
			break;
		}
		for (int i = 0; i < showList.Count; i++)
		{
			showList[i].redPoint.GetComponent<ZhadanPos>().PojieSucc();
		}
		zhadanCode.TaskOver();
		gameManager.saveManager.SavePlayerData();
	}

	public void FreshType()
	{
		Debug.Log("清除内容");
		zhadanInvoke.FreshType();
		zhadanMap.FreshType();
		for (int i = 0; i < showList.Count; i++)
		{
			showList[i].redPoint.gameObject.SetActive(value: false);
		}
		showList.Clear();
		txt_tishi.FreshType();
	}

	private void Update()
	{
	}

	public void ShowZhadan(string id)
	{
		zhadanMap.HaveBoom();
		for (int i = 0; i < zhadan.Count; i++)
		{
			if (!(zhadan[i].id == id))
			{
				continue;
			}
			if (showList.Count != 0)
			{
				List<string> hideId = zhadan[i].hideId;
				List<ZhadanInfo> list = new List<ZhadanInfo>();
				for (int j = 0; j < showList.Count; j++)
				{
					Debug.Log("包含的ID：" + showList[j].id);
					if (hideId.Contains(showList[j].id))
					{
						showList[j].redPoint.GetComponent<ZhadanPos>().Hide();
						list.Add(showList[j]);
					}
				}
			}
			if (zhadan[i].redPoint != null)
			{
				zhadan[i].redPoint.gameObject.SetActive(value: true);
				zhadan[i].redPoint.GetComponent<ZhadanPos>().Init(zhadan[i].id);
				showList.Add(zhadan[i]);
				zhadanMap.map.DOLocalMove(zhadan[i].mapPos, 1f);
			}
			ZhadanInfo zhadanInfo = new ZhadanInfo();
			for (int k = 0; k < showList.Count; k++)
			{
				if (zhadanInfo == null)
				{
					zhadanInfo = showList[k];
				}
				else if (zhadanInfo.level < showList[k].level)
				{
					zhadanInfo = showList[k];
				}
				else if (zhadanInfo.num < showList[k].num)
				{
					zhadanInfo = showList[k];
				}
			}
			txt_tishi.Init(zhadanInfo.label);
		}
	}

	public void Hide()
	{
		GetComponent<RectTransform>().DOLocalMoveX(-1495f, 0.3f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}
}
