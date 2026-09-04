using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class NotePanel : MonoBehaviour
{
	public Transform[] panels;

	public GameManager gameManager;

	public ScrollRect scrollRect;

	public RectTransform contentTransform;

	public RectTransform viewPointTransform;

	public List<NoteItemTitle> noteItemTitles;

	public Text[] titlepanels;

	public Color blackcolor;

	public ItemBox ownitembox;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		noteItemTitles = new List<NoteItemTitle>();
	}

	public void ChangeTitle()
	{
		for (int i = 0; i < titlepanels.Length; i++)
		{
			switch (i)
			{
			case 0:
				titlepanels[i].text = I18N.instance.getValue("^vantitle");
				break;
			case 1:
				titlepanels[i].text = I18N.instance.getValue("^boom_new20");
				break;
			case 2:
				titlepanels[i].text = I18N.instance.getValue("^boom_new27");
				break;
			}
			titlepanels[i].transform.parent.gameObject.SetActive(value: false);
		}
		SetGrayTitle("0");
		SetGrayTitle("1");
	}

	public void SetGrayTitle(string hasgroup)
	{
		string[] array = hasgroup.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			titlepanels[int.Parse(array[i])].fontStyle = FontStyle.Bold;
			titlepanels[int.Parse(array[i])].color = blackcolor;
			titlepanels[int.Parse(array[i])].transform.GetChild(0).gameObject.SetActive(value: false);
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		GetComponent<RectTransform>().localPosition = new Vector2(438f, 0f);
		GetComponent<RectTransform>().DOLocalMoveX(-5f, 0.5f).OnComplete(delegate
		{
			ownitembox.iscanchangetab = true;
		});
		GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	public void Hide()
	{
		GetComponent<RectTransform>().localPosition = new Vector2(-5f, 0f);
		GetComponent<RectTransform>().DOLocalMoveX(-443f, 0.3f);
		GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
		{
			GetComponent<RectTransform>().localPosition = new Vector2(438f, 0f);
			base.gameObject.SetActive(value: false);
		});
	}

	public void CenterOnItem(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		Vector3 worldPointInWidget = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPointPlusHeight(target));
		Vector3 vector = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(viewPointTransform)) - worldPointInWidget;
		vector.z = 0f;
		Vector2 vector2 = new Vector2(vector.x / (contentTransform.rect.width - viewPointTransform.rect.width), vector.y / (contentTransform.rect.height - viewPointTransform.rect.height));
		vector2 = scrollRect.normalizedPosition - vector2;
		vector2.x = Mathf.Clamp01(vector2.x);
		vector2.y = Mathf.Clamp01(vector2.y);
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, vector2, 1f);
		Canvas.ForceUpdateCanvases();
	}

	private Vector3 GetWidgetWorldPoint(RectTransform target)
	{
		Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
		Vector3 position = target.localPosition + vector;
		return target.parent.TransformPoint(position);
	}

	private Vector3 GetWidgetWorldPointPlusHeight(RectTransform target)
	{
		Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
		Vector3 position = target.localPosition + vector + new Vector3(0f, target.sizeDelta.y, 0f);
		return target.parent.TransformPoint(position);
	}

	private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
	{
		return target.InverseTransformPoint(worldPoint);
	}

	public void AddItem(string id, bool isadd, ItemBox ownitembox)
	{
		DATA1 dATA = gameManager.dataManager.dic1[id];
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNoteItemName()), panels[dATA.sign]);
		gameObject.GetComponent<NoteItem>().parObj = this;
		gameObject.GetComponent<NoteItem>().SetContent(dATA, ownitembox, isadd);
		if (dATA.changename == 1)
		{
			gameObject.transform.SetAsFirstSibling();
		}
		if (panels[dATA.sign].transform.childCount > 0)
		{
			CenterOnItem(panels[dATA.sign].transform.GetChild(panels[dATA.sign].transform.childCount - 1).GetComponent<RectTransform>());
		}
		else
		{
			CenterOnItem(panels[dATA.sign].GetComponent<RectTransform>());
		}
		titlepanels[dATA.sign].transform.parent.gameObject.SetActive(value: true);
		if (id.Equals("10068") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel01.noteitem = gameObject;
		}
		if (id.Equals("10057") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel05.noteitemname = gameObject;
		}
		if (id.Equals("10059") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel05.noteitembirth = gameObject;
			gameManager.homeScene.courseManager.ShowCourse5();
		}
		if (id.Equals("10067") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel06.noteitememail = gameObject;
			gameManager.homeScene.courseManager.coursepanel07.noteitememail = gameObject;
			gameManager.homeScene.courseManager.ShowCourse6();
		}
		if (id.Equals("10063") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel07.noteitememai2 = gameObject;
			gameManager.homeScene.courseManager.ShowCourse7();
		}
		if (id.Equals("10066") && isadd)
		{
			gameManager.homeScene.notebook.ShowSubmit();
			gameManager.homeScene.courseManager.ShowCourse11();
		}
		if (id.Equals("10065") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel09.notepanelscrollRect = scrollRect;
			gameManager.homeScene.courseManager.coursepanel09.noteitemleadername = gameObject;
		}
		if (id.Equals("10064") && isadd)
		{
			gameManager.homeScene.courseManager.coursepanel09.notepanelscrollRect = scrollRect;
			gameManager.homeScene.courseManager.coursepanel09.noteitemhitalkid = gameObject;
		}
		if (id.Equals("10065") && gameManager.player.playerdata.itemlist.Contains("10064") && isadd)
		{
			gameManager.homeScene.courseManager.ShowCourse9();
		}
		if (id.Equals("10064") && gameManager.player.playerdata.itemlist.Contains("10065") && isadd)
		{
			gameManager.homeScene.courseManager.ShowCourse9();
		}
		if (!isadd && !dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
			}
			gameManager.homeScene.notebook.RefreshCount();
		}
		gameManager.homeScene.notebook.currentdata = dATA;
		if (!isadd)
		{
			return;
		}
		int num = int.Parse(dATA.role.Substring(1));
		if (num >= 3100036 && num <= 3100047)
		{
			if (!gameManager.player.playerdata.temporaryhopelist.Contains(id))
			{
				gameManager.player.playerdata.temporaryhopelist.Add(id);
				if (!dATA.videoid.Equals("#0"))
				{
					gameManager.homeScene.AddNeedShowVideoList(dATA.videoid);
				}
			}
		}
		else if (!gameManager.player.playerdata.itemlist.Contains(id))
		{
			bool flag = true;
			if (!dATA.needotherid.Equals("#0"))
			{
				string[] array3 = dATA.needotherid.Substring(1).Split(';');
				for (int j = 0; j < array3.Length; j++)
				{
					if (!gameManager.player.playerdata.itemlist.Contains(array3[j]))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag && !dATA.videoid.Equals("#0"))
			{
				gameManager.homeScene.AddNeedShowVideoList(dATA.videoid);
			}
			if (!dATA.newsid.Equals("") && dATA.newsid != null && !dATA.newsid.Equals("#0"))
			{
				gameManager.homeScene.AddNews(dATA.newsid.Substring(1));
			}
			gameManager.player.playerdata.itemlist.Add(id);
			gameManager.homeScene.notebook.RefreshCount();
			if (gameManager.homeScene.invadePhoneDialog != null)
			{
				gameManager.homeScene.invadePhoneDialog.RefreshCount();
				gameManager.homeScene.notebook.allinvadeitems.Add(gameObject.GetComponent<NoteItem>());
			}
			if (gameManager.homeScene.invadeDialog != null)
			{
				gameManager.homeScene.notebook.allinvadeserveritems.Add(gameObject.GetComponent<NoteItem>());
			}
			if (id.Equals("10070"))
			{
				gameManager.UnlockAchievements("swindlinggang");
			}
			else if (id.Equals("10243"))
			{
				gameManager.UnlockAchievements("strangedeath");
			}
			else if (id.Equals("10349"))
			{
				gameManager.UnlockAchievements("manyyearrecord");
			}
			if (!dATA.newemail.Equals(""))
			{
				string[] array4 = dATA.newemail.Substring(1).Split(';');
				for (int k = 0; k < array4.Length; k++)
				{
					StartCoroutine(SendMail(array4[k]));
				}
			}
			string text = dATA.ID.ToString();
			List<string> itemlist = gameManager.player.playerdata.itemlist;
			if ((text.Equals("11131") || text.Equals("11172") || text.Equals("11171") || text.Equals("11173") || text.Equals("11206") || text.Equals("11183")) && itemlist.Contains("11131") && itemlist.Contains("11172") && itemlist.Contains("11171") && itemlist.Contains("11173") && itemlist.Contains("11206") && itemlist.Contains("11183"))
			{
				StartCoroutine(SendMail("1510021"));
			}
		}
		if (dATA.sign == 7)
		{
			StartCoroutine(SetBottom());
		}
	}

	private IEnumerator SetBottom()
	{
		yield return new WaitForSeconds(0.5f);
		scrollRect.normalizedPosition = Vector2.zero;
	}

	private IEnumerator SendMail(string mailid)
	{
		yield return new WaitForSeconds(5f);
		gameManager.homeScene.SendMail(mailid);
	}

	public void DestroyAllHopeItem()
	{
		for (int i = 0; i < panels.Length; i++)
		{
			for (int num = panels[i].childCount - 1; num >= 0; num--)
			{
				gameManager.player.playerdata.temporaryhopelist.Remove(panels[i].GetChild(num).GetComponent<NoteItem>().itemid);
				Object.Destroy(panels[i].GetChild(num).gameObject);
			}
			panels[i].gameObject.SetActive(value: false);
		}
		gameManager.saveManager.SavePlayerData();
		Object.Destroy(base.gameObject);
	}
}
