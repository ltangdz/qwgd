using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class GoalItem : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
{
	public Image img_bk;

	public Image imgSuccBk;

	public Image jindutiao;

	public int state;

	public Text txt_content;

	public Image img_line;

	public Image img_ok;

	public Sprite[] sprites;

	public Color graycolor;

	public string goalid;

	public string periodpos;

	public DATA20 data20;

	public HomeScene homeScene;

	public Button btn_tuili;

	public GameObject img_circlebk;

	public Image img_circle;

	public Text txt_percent;

	public float percent;

	public bool ishastuili;

	public string tuiliid = "";

	public Color pinkcolor;

	public Text txt_tuili;

	public Image img_tuili;

	private GameManager gameManager;

	[SerializeField]
	private GameObject img_black;

	[SerializeField]
	private GameObject img_bottom;

	[SerializeField]
	private Text txt_zimu;

	public bool isred;

	public void MinusPercent(int percent1)
	{
		percent -= percent1;
		UpdatePercent(isminus: true);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		homeScene = gameManager.homeScene;
		btn_tuili.onClick.AddListener(delegate
		{
			if (!gameManager.player.playerdata.reasoninglist.Contains(data20.renwu.Substring(1)))
			{
				if (!isred)
				{
					homeScene.gameManager.reasoningManager.ShowReasoningPanel(data20.renwu.Substring(1), isover: false);
				}
				else if (!data20.ID.ToString().Equals("2000001") || gameManager.player.playerdata.videotiplist.Contains("3700003"))
				{
					gameManager.canShowSetting = 0;
					homeScene.gameManager.reasoningManager.ShowReasoningPanel(data20.renwu.Substring(1), isover: true);
					DeleteHighLight();
				}
			}
		});
		GetComponent<Button>().onClick.AddListener(delegate
		{
			if (!gameManager.player.playerdata.reasoninglist.Contains(data20.renwu.Substring(1)))
			{
				if (!isred)
				{
					homeScene.gameManager.reasoningManager.ShowReasoningPanel(data20.renwu.Substring(1), isover: false);
				}
				else if (!data20.ID.ToString().Equals("2000001") || gameManager.player.playerdata.videotiplist.Contains("3700003"))
				{
					gameManager.canShowSetting = 0;
					homeScene.gameManager.reasoningManager.ShowReasoningPanel(data20.renwu.Substring(1), isover: true);
					DeleteHighLight();
				}
			}
		});
	}

	public void AddHighLight()
	{
		gameManager.canShowSetting = 1;
		img_black.SetActive(value: true);
		base.transform.parent.gameObject.AddComponent<Canvas>().overrideSorting = true;
		base.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 20;
		base.transform.parent.gameObject.AddComponent<GraphicRaycaster>();
		img_bottom.SetActive(value: true);
		img_bottom.transform.DOLocalMoveY(-903f, 1f).OnComplete(delegate
		{
			txt_zimu.DOText(I18N.instance.getValue("^interface04"), 1.5f);
		});
	}

	public void DeleteHighLight()
	{
		if (base.transform.parent.GetComponent<GraphicRaycaster>() != null)
		{
			Object.Destroy(base.transform.parent.GetComponent<GraphicRaycaster>());
		}
		if (base.transform.parent.GetComponent<Canvas>() != null)
		{
			Object.Destroy(base.transform.parent.GetComponent<Canvas>());
		}
		img_black.SetActive(value: false);
		img_bottom.SetActive(value: false);
	}

	public void SetState(int s, DATA20 data20)
	{
		homeScene = GameObject.Find("GameManager").GetComponent<GameManager>().homeScene;
		this.data20 = data20;
		goalid = data20.ID.ToString();
		periodpos = data20.pos.ToString();
		state = s;
		tuiliid = data20.renwu.Substring(1);
		ishastuili = !data20.renwu.Substring(1).Equals("0");
		if (ishastuili)
		{
			btn_tuili.gameObject.SetActive(value: true);
		}
		if (homeScene.gameManager.Is_Dlc6() && tuiliid != "4013")
		{
			txt_tuili.text = "";
		}
		txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(data20.title));
		switch (state)
		{
		case 0:
			img_bk.sprite = sprites[1];
			txt_content.color = graycolor;
			img_line.gameObject.SetActive(value: false);
			img_circlebk.gameObject.SetActive(value: true);
			percent = 0f;
			UpdatePercent();
			CancelInvoke();
			break;
		case 1:
			homeScene.goalDialog.CompleteItem(goalid);
			break;
		}
		img_line.SetNativeSize();
		if (goalid.Equals("2000001"))
		{
			homeScene.courseManager.coursepanel16.goalitem = base.gameObject;
		}
		if (goalid.Equals("2000020") && homeScene.gameManager.player.playerdata.isYulunGameOver)
		{
			AddPercent(100f);
		}
		if (goalid.Equals("2000023") && homeScene.gameManager.player.playerdata.iszhiboover)
		{
			AddPercent(100f);
		}
	}

	public void UpdatePercent(bool isminus = false)
	{
		if (!imgSuccBk.gameObject.activeInHierarchy && percent != 0f)
		{
			imgSuccBk.gameObject.SetActive(value: true);
			img_bk.sprite = sprites[0];
		}
		if (percent >= 100f)
		{
			percent = 100f;
		}
		float num = percent / 100f;
		img_circle.DOFillAmount(num, 0.3f);
		if (num != 0f)
		{
			jindutiao.transform.DOLocalMoveX(416f * num - 208f, 0.3f);
		}
		txt_percent.GetComponent<I18NText>().updateTranslation5(percent + "%");
		if (percent >= 100f)
		{
			if (ishastuili)
			{
				if (!gameManager.player.playerdata.reasoninglist.Contains(data20.renwu.Substring(1)))
				{
					if (!isred)
					{
						jindutiao.GetComponent<CanvasGroup>().alpha = 1f;
						txt_tuili.color = new Color(1f, 1f, 1f, 1f);
						img_tuili.sprite = sprites[4];
						isred = true;
						btn_tuili.interactable = false;
						if (!tuiliid.Equals("0") && !tuiliid.Equals(""))
						{
							homeScene.gameManager.reasoningManager.ShowReasonPreVideo(tuiliid);
						}
						btn_tuili.interactable = true;
					}
				}
				else
				{
					jindutiao.GetComponent<CanvasGroup>().alpha = 1f;
					CompeleteMission();
				}
			}
			else
			{
				jindutiao.GetComponent<CanvasGroup>().alpha = 1f;
				CompeleteMission();
			}
		}
		else
		{
			if (!isminus)
			{
				return;
			}
			if (ishastuili)
			{
				if (!gameManager.player.playerdata.reasoninglist.Contains(data20.renwu.Substring(1)))
				{
					jindutiao.GetComponent<CanvasGroup>().alpha = 0.5f;
					txt_tuili.color = new Color(0.54f, 0.623f, 0.69f, 1f);
					img_tuili.sprite = sprites[3];
					isred = false;
					btn_tuili.interactable = true;
					if (!tuiliid.Equals("0") && !tuiliid.Equals("") && homeScene.needshowvideolist.Contains(tuiliid))
					{
						homeScene.needshowvideolist.Remove(tuiliid);
					}
				}
				else
				{
					jindutiao.GetComponent<CanvasGroup>().alpha = 0.5f;
				}
			}
			else
			{
				jindutiao.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
		}
	}

	public void AddPercent(float addpercent)
	{
		percent += addpercent;
		UpdatePercent();
	}

	public void SetNewState(int s)
	{
		state = s;
		switch (state)
		{
		case 0:
			img_bk.sprite = sprites[0];
			txt_content.color = graycolor;
			CancelInvoke();
			break;
		case 1:
			img_bk.sprite = sprites[1];
			txt_content.color = Color.white;
			img_line.gameObject.SetActive(value: false);
			StartOkAnimation();
			break;
		case 2:
			img_bk.sprite = sprites[1];
			txt_content.color = Color.white;
			img_line.gameObject.SetActive(value: false);
			StartOkAnimation();
			break;
		}
		img_line.SetNativeSize();
	}

	private void StartLineAnimation()
	{
		img_line.GetComponent<CanvasGroup>().alpha += 0.1f;
		if (img_line.GetComponent<CanvasGroup>().alpha >= 1f)
		{
			img_line.GetComponent<CanvasGroup>().alpha = 0f;
		}
	}

	public void CompeleteMission()
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		state = 2;
		img_bk.sprite = sprites[1];
		txt_content.color = Color.white;
		btn_tuili.gameObject.SetActive(value: false);
		img_circlebk.gameObject.SetActive(value: false);
		img_ok.gameObject.SetActive(value: true);
		StartOkAnimation();
		if (data20.last == 1 && percent >= 100f)
		{
			gameManager.player.playerdata.isovertask = true;
			homeScene.notebook.ShowSubmit();
		}
		homeScene.goalDialog.CompeleteGoal(data20.ID.ToString());
		AddLog();
	}

	private void AddLog()
	{
		string text = "[" + homeScene.hB3Top.crtTime.text + "]>>>>>>>>>>>>";
		text += string.Format(I18N.instance.getValue("^logtip03"), I18N.instance.getValue(data20.title));
		homeScene.logPanel.AddLog(text);
	}

	private void StartOkAnimation()
	{
		img_ok.gameObject.SetActive(value: true);
		img_ok.DOFillAmount(1f, 1f);
	}

	public void RemoveItem()
	{
		StartCoroutine(StartMove());
	}

	private IEnumerator StartMove()
	{
		base.transform.parent.GetComponent<Animator>().Play("ani_goalitemhide");
		yield return new WaitForSeconds(0.3f);
		Object.Destroy(base.transform.parent.gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (percent < 100f)
		{
			txt_tuili.color = new Color(0.5254902f, 0.6156863f, 35f / 51f);
			img_tuili.sprite = sprites[3];
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		txt_tuili.color = new Color(1f, 1f, 1f, 1f);
		img_tuili.sprite = sprites[4];
	}
}
