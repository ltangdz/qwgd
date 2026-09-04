using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class LiveBroadcastingDialog : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField]
	private Text txt_zimu;

	[SerializeField]
	private List<string> zimus = new List<string>();

	[SerializeField]
	private List<string> zimus2 = new List<string>();

	[SerializeField]
	private List<string> rightresultzimus = new List<string>();

	[SerializeField]
	private List<string> wrongresultzimus = new List<string>();

	[SerializeField]
	private List<string> bossresultzimus = new List<string>();

	[SerializeField]
	private List<string> bossrightzimus = new List<string>();

	[SerializeField]
	private List<string> rightdanmus = new List<string>();

	[SerializeField]
	private List<string> wrongdanmus = new List<string>();

	[SerializeField]
	private List<string> failedzimus = new List<string>();

	public bool iscanclick;

	public bool ismax = true;

	[SerializeField]
	private Transform img_top;

	[SerializeField]
	private Transform img_bottom;

	[SerializeField]
	private RectTransform content;

	[SerializeField]
	private Button btn_min;

	[SerializeField]
	private Button btn_min2;

	[SerializeField]
	private Button btn_send;

	[SerializeField]
	private Button btn_send2;

	[SerializeField]
	private Button btn_close;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private InputField inputField2;

	[SerializeField]
	private GameObject rightpanel;

	[SerializeField]
	private GameObject bottompanel;

	[SerializeField]
	private GameObject leftpanel;

	[SerializeField]
	private GameObject overpanel;

	[SerializeField]
	private GameObject img_voicepanel;

	[SerializeField]
	private Transform bottomcontent;

	[SerializeField]
	private Transform rightcontent;

	[SerializeField]
	private ScrollRect bottomscrollRect;

	[SerializeField]
	private ScrollRect rightscrollRect;

	private Image img_notclick;

	[SerializeField]
	private List<string> jieshaos = new List<string>();

	[SerializeField]
	private List<string> hopegiveitemids = new List<string>();

	[SerializeField]
	private List<string> hopeansweritemids = new List<string>();

	[SerializeField]
	private List<string> specialdanmus = new List<string>();

	[SerializeField]
	private List<Vector3> specialpos = new List<Vector3>();

	[SerializeField]
	private List<string> customdanmus = new List<string>();

	[SerializeField]
	private int countdown = 72000;

	[SerializeField]
	private Image img_sliderfilled;

	[SerializeField]
	private bool isstart;

	private float timer;

	[SerializeField]
	private List<SpecialDanmu> specialDanmus = new List<SpecialDanmu>();

	public string currentitemid = "";

	public int hopeid;

	private int tensecond;

	public bool iscanclickspecialdanmu;

	public Animator img_man;

	private bool iscanpeek;

	public int currentp = -1;

	private void PeekComputer()
	{
		if (iscanpeek)
		{
			StartCoroutine(PeekComputerAni());
		}
	}

	private IEnumerator PeekComputerAni()
	{
		PlayOrzAnimation(4);
		yield return new WaitForSeconds(1f);
		PlayOrzAnimation(0);
	}

	private void GameOver()
	{
		StartCoroutine(StartGameOver());
	}

	private IEnumerator StartGameOver()
	{
		btn_min.interactable = false;
		btn_min2.interactable = false;
		btn_send.interactable = false;
		btn_send2.interactable = false;
		inputField.interactable = false;
		inputField2.interactable = false;
		if (!ismax)
		{
			Min();
			yield return new WaitForSeconds(0.3f);
		}
		img_top.DOLocalMoveY(475f, 0.5f);
		img_bottom.DOLocalMoveY(-475f, 0.5f);
		img_notclick.raycastTarget = true;
		img_notclick.color = new Color(0f, 0f, 0f, 0.658f);
		img_notclick.gameObject.AddComponent<Canvas>().overrideSorting = true;
		img_notclick.GetComponent<Canvas>().sortingOrder = 3;
		img_notclick.gameObject.AddComponent<GraphicRaycaster>();
		yield return new WaitForSeconds(0.8f);
		for (int i = 0; i < failedzimus.Count; i++)
		{
			PlayOrzAnimation(2);
			txt_zimu.text = "";
			txt_zimu.DOText(I18N.instance.getValue(failedzimus[i]), 2f);
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitForSeconds(0.5f);
		PlayOrzAnimation(0);
		img_top.DOLocalMoveY(615f, 0.5f);
		img_bottom.DOLocalMoveY(-615f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(4, gameManager);
		Object.Destroy(base.gameObject);
	}

	public void StartTime(string specialitemid, int hopeid)
	{
		if (hopeid != 10 && currentp != -1)
		{
			Debug.LogError("删除：" + currentp + ":" + gameManager.player.playerdata.leftshowspecials[currentp]);
			gameManager.player.playerdata.leftshowspecials.RemoveRange(currentp, 2);
			currentp = -1;
		}
		gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
		iscanpeek = true;
		gameManager.player.playerdata.livebroadingcurrenthopeid = hopeid;
		this.hopeid = hopeid;
		currentitemid = specialitemid;
		CancelSpecialDanmu();
		HideSpecialDanmu();
		img_sliderfilled.DOKill();
		img_sliderfilled.fillAmount = (float)countdown / 72000f;
		img_sliderfilled.DOFillAmount(0f, (float)countdown / 60f);
		isstart = true;
		btn_min.interactable = true;
		btn_send.interactable = true;
		btn_send2.interactable = true;
		inputField.interactable = true;
		inputField2.interactable = true;
		if (hopeid != -1)
		{
			gameManager.homeScene.notebook.AddNewItem(hopegiveitemids[hopeid]);
		}
	}

	private void HideSpecialDanmu()
	{
		for (int i = 0; i < specialDanmus.Count; i++)
		{
			specialDanmus[i].Hide();
		}
	}

	private void SelectTwo()
	{
		btn_send.interactable = false;
		btn_send2.interactable = false;
		inputField.interactable = false;
		inputField2.interactable = false;
		for (int num = leftpanel.transform.childCount - 1; num >= 0; num--)
		{
			if (leftpanel.transform.GetChild(num).name.Contains("specialzimu"))
			{
				Object.Destroy(leftpanel.transform.GetChild(num).gameObject);
			}
		}
		specialDanmus.Clear();
		if (gameManager.player.playerdata.leftshowspecials.Count >= 2)
		{
			int num2 = Random.Range(0, gameManager.player.playerdata.leftshowspecials.Count - 1);
			int index = gameManager.player.playerdata.leftshowspecials[num2];
			int index2 = gameManager.player.playerdata.leftshowspecials[num2 + 1];
			string[] array = specialdanmus[index].Split(';');
			ShowSpecialDanmu(array[0], specialpos[0], int.Parse(array[1]));
			string[] array2 = specialdanmus[index2].Split(';');
			ShowSpecialDanmu(array2[0], specialpos[1], int.Parse(array2[1]));
			currentp = num2;
			gameManager.saveManager.SavePlayerData();
		}
		else
		{
			StartCoroutine(BossStart());
		}
	}

	private IEnumerator BossStart()
	{
		for (int i = 0; i < zimus2.Count; i++)
		{
			PlayOrzAnimation(1);
			txt_zimu.text = "";
			txt_zimu.DOText(I18N.instance.getValue(zimus2[i]), 2f);
			yield return new WaitForSeconds(3f);
		}
		PlayOrzAnimation(0);
		ShowSpecialDanmu("^live22", specialpos[1], 10);
	}

	private void ShowSpecialDanmu(string key, Vector3 pos, int hopeid, bool iscanclick = true)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/specialzimu"), leftpanel.transform);
		gameObject.GetComponent<SpecialDanmu>().Init((hopeid == -1) ? "" : hopeansweritemids[hopeid], I18N.instance.getValue(key), pos, hopeid);
		gameObject.GetComponent<SpecialDanmu>().liveBroadcastingDialog = this;
		gameObject.GetComponent<SpecialDanmu>().iscanclick = iscanclick;
		specialDanmus.Add(gameObject.GetComponent<SpecialDanmu>());
	}

	public void CancelSpecialDanmu()
	{
		for (int i = 0; i < specialDanmus.Count; i++)
		{
			if (specialDanmus[i].isclick)
			{
				specialDanmus[i].Cancel();
			}
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(15);
		countdown = gameManager.player.playerdata.livebroadinglefttime;
		iscanclick = false;
		img_notclick = GetComponent<Image>();
		btn_close.onClick.AddListener(delegate
		{
			if (!gameManager.player.playerdata.maillist["admin"][0].ContainsKey("1500107"))
			{
				gameManager.homeScene.SendMail("1500107");
			}
			gameManager.musicManager.PlayMusicLoop(3);
			GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			Object.Destroy(base.gameObject, 0.4f);
		});
		gameManager.homeScene.ShowLiveBroadSqlEnterBtn();
		if (gameManager.player.playerdata.hopelist.Contains(10))
		{
			overpanel.SetActive(value: true);
			leftpanel.SetActive(value: false);
			btn_close.gameObject.SetActive(value: true);
			btn_min.gameObject.SetActive(value: false);
			btn_close.interactable = true;
			btn_send.interactable = false;
			btn_send2.interactable = false;
			inputField.interactable = false;
			inputField2.interactable = false;
			rightpanel.SetActive(value: true);
			img_notclick.raycastTarget = false;
			img_notclick.color = new Color(1f, 1f, 1f, 0f);
			Object.Destroy(img_notclick.GetComponent<GraphicRaycaster>());
			Object.Destroy(img_notclick.GetComponent<Canvas>());
		}
		else
		{
			StartCoroutine(ShowZimu());
			btn_min.onClick.AddListener(Min);
			btn_min2.onClick.AddListener(Min);
			btn_send.onClick.AddListener(Reply1);
			btn_send2.onClick.AddListener(Reply2);
			InvokeRepeating("ShowDanmu", 1f, 2f);
			InvokeRepeating("PeekComputer", 1f, 15f);
		}
	}

	private void ShowDanmu()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), leftpanel.transform);
			int index = Random.Range(0, customdanmus.Count);
			obj.GetComponent<LiveBroadcastZimu>().Init(customdanmus[index], 0);
		}
	}

	private void ShowMaynDanmu(bool isgood)
	{
		for (int num = leftpanel.transform.childCount - 1; num >= 0; num--)
		{
			if (leftpanel.transform.GetChild(num).name.Contains("specialzimu"))
			{
				Object.Destroy(leftpanel.transform.GetChild(num).gameObject);
			}
		}
		for (int i = 0; i < 5; i++)
		{
			if (isgood)
			{
				GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), leftpanel.transform);
				int index = Random.Range(0, 10);
				obj.GetComponent<LiveBroadcastZimu>().Init(customdanmus[index], 0);
			}
			else
			{
				GameObject obj2 = (GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcastzimu"), leftpanel.transform);
				int index2 = Random.Range(10, 19);
				obj2.GetComponent<LiveBroadcastZimu>().Init(customdanmus[index2], 0);
			}
		}
	}

	private void PlayOrzAnimation(int type)
	{
		switch (type)
		{
		case 0:
			img_man.Play("ani_normalspeaking");
			break;
		case 1:
			img_man.Play("ani_happyspeaking");
			break;
		case 2:
			img_man.Play("ani_zhenfengxiangdui");
			break;
		case 3:
			img_man.Play("ani_huangluanliuhan");
			break;
		case 4:
			img_man.Play("ani_toumiaodiannao");
			break;
		}
	}

	private IEnumerator ShowZimu()
	{
		if (gameManager.player.playerdata.livebroadingstep == 0)
		{
			img_top.DOLocalMoveY(475f, 0.5f);
			img_bottom.DOLocalMoveY(-475f, 0.5f);
			yield return new WaitForSeconds(0.8f);
			PlayOrzAnimation(0);
			for (int i = 0; i < zimus.Count; i++)
			{
				txt_zimu.text = "";
				txt_zimu.DOText(I18N.instance.getValue(zimus[i]), 2f);
				if (i == 1)
				{
					yield return new WaitForSeconds(2f);
					img_voicepanel.SetActive(value: true);
					PlayOrzAnimation(2);
				}
				yield return new WaitForSeconds(3f);
			}
			yield return new WaitForSeconds(0.5f);
			img_top.DOLocalMoveY(615f, 0.5f);
			img_bottom.DOLocalMoveY(-615f, 0.5f);
			PlayOrzAnimation(0);
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			img_notclick.color = new Color(1f, 1f, 1f, 0f);
			for (int j = 0; j < jieshaos.Count; j++)
			{
				if (j == 1)
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright"), rightcontent)).transform.GetChild(0).GetComponent<Text>().text = I18N.instance.getValue(jieshaos[j]);
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().text = I18N.instance.getValue(jieshaos[j]);
				}
				else
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft"), rightcontent)).transform.GetChild(0).GetComponent<Text>().text = string.Format(I18N.instance.getValue(jieshaos[j]), gameManager.player.playerdata.nickname);
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().text = string.Format(I18N.instance.getValue(jieshaos[j]), gameManager.player.playerdata.nickname);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
				LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
				bottomscrollRect.normalizedPosition = Vector2.zero;
				rightscrollRect.normalizedPosition = Vector2.zero;
			}
		}
		if (gameManager.player.playerdata.livebroadingcurrenthopeid == -1)
		{
			SelectTwo();
		}
		else
		{
			StartTime(hopeansweritemids[gameManager.player.playerdata.livebroadingcurrenthopeid], gameManager.player.playerdata.livebroadingcurrenthopeid);
		}
		if (gameManager.player.playerdata.livebroadingstep == 0)
		{
			yield return new WaitForSeconds(0.5f);
			leftpanel.transform.DOLocalMoveX(-652f, 0.2f);
			yield return new WaitForSeconds(0.2f);
			rightpanel.SetActive(value: true);
			for (int i = 0; i < jieshaos.Count; i++)
			{
				if (i == 1)
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright"), rightcontent)).transform.GetChild(0).GetComponent<Text>().DOText(I18N.instance.getValue(jieshaos[i]), 3f);
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().DOText(I18N.instance.getValue(jieshaos[i]), 3f);
				}
				else
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft"), rightcontent)).transform.GetChild(0).GetComponent<Text>().DOText(string.Format(I18N.instance.getValue(jieshaos[i]), gameManager.player.playerdata.nickname), 3f);
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().DOText(string.Format(I18N.instance.getValue(jieshaos[i]), gameManager.player.playerdata.nickname), 3f);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
				LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
				bottomscrollRect.normalizedPosition = Vector2.zero;
				rightscrollRect.normalizedPosition = Vector2.zero;
				yield return new WaitForSeconds(3f);
			}
			gameManager.player.playerdata.livebroadingstep = 1;
			gameManager.saveManager.SavePlayerData();
		}
		else
		{
			leftpanel.transform.localPosition = new Vector3(-652f, 0f, 0f);
			rightpanel.SetActive(value: true);
		}
		yield return new WaitForSeconds(0.5f);
		iscanclickspecialdanmu = true;
		iscanclick = true;
		img_notclick.raycastTarget = false;
		img_notclick.color = new Color(1f, 1f, 1f, 0f);
		Object.Destroy(img_notclick.GetComponent<GraphicRaycaster>());
		Object.Destroy(img_notclick.GetComponent<Canvas>());
	}

	private void Min()
	{
		rightpanel.SetActive(!ismax);
		bottompanel.SetActive(ismax);
		content.DOKill();
		if (ismax)
		{
			content.DOLocalMove(new Vector2(-404f, 261f), 0.2f);
			content.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
		}
		else
		{
			content.DOLocalMove(Vector3.zero, 0.2f);
			content.DOScale(Vector3.one, 0.2f);
		}
		ismax = !ismax;
	}

	private void Reply1()
	{
		if (!inputField.text.Trim().Equals("") && inputField.text.Trim() != null)
		{
			iscanpeek = false;
			PlayOrzAnimation(1);
			DATA1 dATA = gameManager.dataManager.dic1[currentitemid.ToString()];
			string value = I18N.instance.getValue(dATA.message);
			((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright"), rightcontent)).transform.GetChild(0).GetComponent<Text>().text = inputField.text.Trim();
			((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().text = inputField2.text.Trim();
			if (inputField.text.Trim().ToLower().Equals(value.ToLower()))
			{
				Bingo();
			}
			else
			{
				Wrong();
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
			LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
			bottomscrollRect.normalizedPosition = Vector2.zero;
			rightscrollRect.normalizedPosition = Vector2.zero;
		}
	}

	private void Reply2()
	{
		if (!inputField2.text.Trim().Equals("") && inputField2.text.Trim() != null)
		{
			iscanpeek = false;
			PlayOrzAnimation(1);
			DATA1 dATA = gameManager.dataManager.dic1[currentitemid.ToString()];
			string value = I18N.instance.getValue(dATA.message);
			((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright"), rightcontent)).transform.GetChild(0).GetComponent<Text>().text = inputField.text.Trim();
			((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().text = inputField2.text.Trim();
			if (inputField2.text.Trim().ToLower().Equals(value.ToLower()))
			{
				Bingo();
			}
			else
			{
				Wrong();
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
			LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
			bottomscrollRect.normalizedPosition = Vector2.zero;
			rightscrollRect.normalizedPosition = Vector2.zero;
		}
	}

	private void Bingo()
	{
		ShowMaynDanmu(isgood: true);
		PlayOrzAnimation(1);
		gameManager.player.playerdata.livebroadingcurrenthopeid = -1;
		StartCoroutine(SelectOneSpecial(isright: true));
	}

	private void Wrong()
	{
		ShowMaynDanmu(isgood: false);
		PlayOrzAnimation(3);
		StartCoroutine(SelectOneSpecial(isright: false));
	}

	private IEnumerator SelectOneSpecial(bool isright)
	{
		btn_send.interactable = false;
		btn_send2.interactable = false;
		inputField.interactable = false;
		inputField2.interactable = false;
		txt_zimu.text = "";
		if (!inputField.text.Equals(""))
		{
			txt_zimu.DOText(string.Format(I18N.instance.getValue(isright ? rightresultzimus[hopeid] : wrongresultzimus[hopeid]), inputField.text), 2f);
		}
		else if (!inputField2.text.Equals(""))
		{
			txt_zimu.DOText(string.Format(I18N.instance.getValue(isright ? rightresultzimus[hopeid] : wrongresultzimus[hopeid]), inputField2.text), 2f);
		}
		yield return new WaitForSeconds(2f);
		specialDanmus.Clear();
		int index = Random.Range(0, rightdanmus.Count);
		ShowSpecialDanmu(isright ? rightdanmus[index] : wrongdanmus[index], specialpos[1], -1, iscanclick: false);
		yield return new WaitForSeconds(2f);
		((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft"), rightcontent)).transform.GetChild(0).GetComponent<Text>().DOText(string.Format(I18N.instance.getValue(isright ? "^live39" : "^live40"), inputField.text), 1f);
		((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft2"), bottomcontent)).transform.GetChild(0).GetComponent<Text>().DOText(string.Format(I18N.instance.getValue(isright ? "^live39" : "^live40"), inputField.text), 1f);
		LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
		LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
		bottomscrollRect.normalizedPosition = Vector2.zero;
		rightscrollRect.normalizedPosition = Vector2.zero;
		yield return new WaitForSeconds(1f);
		PlayOrzAnimation(0);
		if (isright && hopeid != 10)
		{
			SelectTwo();
			yield return new WaitForSeconds(0.5f);
		}
		else if (isright && hopeid == 10)
		{
			if (gameManager.player.playerdata.livebroadtotaltime <= 600)
			{
				gameManager.UnlockAchievements("livebroading");
			}
			btn_min2.interactable = false;
			btn_min.interactable = false;
			gameManager.homeScene.liveBroadingEnterBtn.gameObject.SetActive(value: false);
			if (!ismax)
			{
				Min();
				yield return new WaitForSeconds(0.3f);
			}
			img_notclick.raycastTarget = true;
			img_notclick.color = new Color(0f, 0f, 0f, 0.658f);
			img_notclick.gameObject.AddComponent<Canvas>().overrideSorting = true;
			img_notclick.GetComponent<Canvas>().sortingOrder = 3;
			img_notclick.gameObject.AddComponent<GraphicRaycaster>();
			img_top.DOLocalMoveY(475f, 0.5f);
			img_bottom.DOLocalMoveY(-475f, 0.5f);
			yield return new WaitForSeconds(0.8f);
			for (int i = 0; i < bossresultzimus.Count; i++)
			{
				PlayOrzAnimation(1);
				txt_zimu.text = "";
				txt_zimu.DOText(I18N.instance.getValue(bossresultzimus[i]), 2f);
				yield return new WaitForSeconds(3f);
			}
			yield return new WaitForSeconds(0.5f);
			img_top.DOLocalMoveY(615f, 0.5f);
			img_bottom.DOLocalMoveY(-615f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			for (int i = 0; i < bossrightzimus.Count; i++)
			{
				if (i == 4 || i == 6)
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextright"), rightcontent)).transform.GetChild(0).GetComponent<Text>().DOText(I18N.instance.getValue(bossrightzimus[i]), 1f);
				}
				else
				{
					((GameObject)Object.Instantiate(Resources.Load("Livebroadcasting/livebroadcasttextleft"), rightcontent)).transform.GetChild(0).GetComponent<Text>().DOText(I18N.instance.getValue(bossrightzimus[i]), 1f);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(rightcontent as RectTransform);
				LayoutRebuilder.ForceRebuildLayoutImmediate(bottomcontent as RectTransform);
				bottomscrollRect.normalizedPosition = Vector2.zero;
				rightscrollRect.normalizedPosition = Vector2.zero;
				yield return new WaitForSeconds(1f);
			}
			yield return new WaitForSeconds(0.5f);
			overpanel.SetActive(value: true);
			leftpanel.SetActive(value: false);
			btn_min.gameObject.SetActive(value: false);
			btn_close.interactable = true;
			btn_close.gameObject.SetActive(value: true);
			btn_send.interactable = false;
			btn_send2.interactable = false;
			inputField.interactable = false;
			inputField2.interactable = false;
		}
		if ((!isright && hopeid == 10) || hopeid != 10)
		{
			iscanclick = true;
			img_notclick.raycastTarget = false;
			img_notclick.color = new Color(1f, 1f, 1f, 0f);
			Object.Destroy(img_notclick.GetComponent<GraphicRaycaster>());
			Object.Destroy(img_notclick.GetComponent<Canvas>());
			if (!isright)
			{
				btn_send.interactable = true;
				btn_send2.interactable = true;
				inputField.interactable = true;
				inputField2.interactable = true;
			}
			inputField.text = "";
			inputField2.text = "";
		}
	}
}
