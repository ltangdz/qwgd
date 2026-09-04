using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class NoteItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image img_bk;

	public Image img_fx;

	public Image img_pic;

	public Text txt_content;

	public Image img_notclick;

	public bool iscanclick;

	public Button btn_copy;

	public GameManager gameManager;

	public Button btn_open;

	public bool islink;

	public Sprite[] sprites;

	public Image img_upload;

	public Image img_uploadmask;

	public Color bluecolor;

	public bool isstart;

	public int count_down;

	private DATA1 data1;

	public Transform iconGroup;

	private bool ishighlight;

	public NotePanel parObj;

	public string itemid;

	private string str_dots = "...";

	private int strdotpos = 1;

	private int strdotcount;

	private int firstbrowsercount;

	public void SetHighLightItem(bool ihl)
	{
		ishighlight = ihl;
		img_bk.sprite = sprites[ihl ? 3 : 0];
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void InitContent(DATA1 data1)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.data1 = data1;
		itemid = data1.ID.ToString();
		if (!gameManager.homeScene.notebook.noteitemlist.ContainsKey(itemid))
		{
			gameManager.homeScene.notebook.noteitemlist.Add(itemid, this);
		}
		islink = data1.link == 1;
		if (islink)
		{
			btn_open.onClick.AddListener(delegate
			{
				if (!gameManager.homeScene.Iscanopentool())
				{
					if (gameManager.homeScene.newbrowserDialog != null)
					{
						gameManager.homeScene.newbrowserDialog.ResumeMinimize();
					}
					JumpLine(data1.image.Substring(1), 4, I18N.instance.getValue(data1.message));
					CancelClick();
				}
			});
		}
		else if (data1.form == 3 || data1.form == 4)
		{
			img_pic.gameObject.SetActive(value: true);
			txt_content.rectTransform.sizeDelta = new Vector2(232f, txt_content.rectTransform.sizeDelta.y);
			btn_open.onClick.AddListener(delegate
			{
				ShowPicture();
				CancelClick();
			});
		}
		else if (data1.form == 5)
		{
			img_pic.gameObject.SetActive(value: true);
			txt_content.rectTransform.sizeDelta = new Vector2(232f, txt_content.rectTransform.sizeDelta.y);
			btn_open.onClick.AddListener(delegate
			{
				ShowMap();
				CancelClick();
			});
		}
		else if (data1.form == 8)
		{
			btn_open.onClick.AddListener(delegate
			{
				ShowLink();
				CancelClick();
			});
		}
		else if (data1.form == 9)
		{
			btn_open.onClick.AddListener(delegate
			{
				ShowLiveBroading();
				CancelClick();
			});
		}
		else
		{
			btn_copy.onClick.AddListener(delegate
			{
				if (gameManager.player.playerdata.isCourse01 == 0)
				{
					gameManager.homeScene.courseManager.coursepanel01.isclickcopy = true;
					gameManager.homeScene.courseManager.coursepanel01.Next();
				}
				GUIUtility.systemCopyBuffer = I18N.instance.getValue(data1.message);
				CancelClick();
			});
		}
		iscanclick = true;
		img_uploadmask.gameObject.SetActive(value: false);
		txt_content.color = bluecolor;
		txt_content.text = "";
		txt_content.DOText(I18N.instance.getValue(data1.title) + ":" + I18N.instance.getValue(data1.message), 0.3f);
		gameManager.homeScene.notebook.HideCodeDialog(itemid);
		if (!data1.label.Equals(""))
		{
			string[] array = data1.label.Substring(1).Split(';');
			for (int num = 0; num < array.Length; num++)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNoteItemToolName()), iconGroup);
				if (data1.form == 4)
				{
					gameObject.GetComponent<NoteItemTool>().imgstr = data1.image + "scan";
					gameObject.GetComponent<NoteItemTool>().imgName = data1.image;
				}
				gameObject.GetComponent<NoteItemTool>().Init(gameManager, int.Parse(array[num]));
			}
		}
		if (gameManager.Is_Dlc7())
		{
			img_fx.gameObject.SetActive(value: false);
		}
		else
		{
			img_fx.gameObject.SetActive(data1.fx == 1);
		}
	}

	private void FixedUpdate()
	{
		if (gameManager != null && gameManager.Is_Dlc7())
		{
			img_fx.gameObject.SetActive(gameManager.player.playerdata.NearlyItemIds.Contains(data1.ID.ToString()));
		}
	}

	public void ShowLink()
	{
		Object.Instantiate(Resources.Load("Houtai/HoutaiPanel") as GameObject, gameManager.homeScene.middle);
	}

	public void ShowLiveBroading()
	{
		if (gameManager.homeScene.middle.Find("liveBroadcastingDialog") == null)
		{
			Object.Instantiate(Resources.Load<GameObject>("Livebroadcasting/LiveBroadcastingDialog"), gameManager.homeScene.middle).name = "liveBroadcastingDialog";
		}
	}

	private void JumpLine(string jump, int type, string url = "")
	{
		Debug.Log("type：" + type);
		switch (type)
		{
		case 1:
		case 2:
			if (gameManager.homeScene.newbrowserDialog == null)
			{
				gameManager.homeScene.computerButtonBox.btn_search.SelectTool(2);
			}
			else
			{
				gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
			}
			StartCoroutine(OpenWeb(jump, url));
			break;
		case 4:
		{
			if (gameManager.homeScene.newbrowserDialog == null)
			{
				gameManager.homeScene.computerButtonBox.btn_search.SelectTool(2);
			}
			else
			{
				gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
			}
			DATA2 data = gameManager.dataManager.dic2[jump];
			gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
			break;
		}
		}
	}

	private IEnumerator OpenWeb(string jump, string url = "")
	{
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
		DATA2 data = gameManager.dataManager.dic2[jump];
		gameManager.homeScene.newbrowserDialog.AddNewPanel(data, isadmin: true);
		gameManager.homeScene.newbrowserDialog.ResumeMinimize();
	}

	public void SetContent(DATA1 data1, ItemBox ownitembox, bool isadd = true)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.data1 = data1;
		itemid = data1.ID.ToString();
		if (!ownitembox.noteitemlist.ContainsKey(itemid))
		{
			ownitembox.noteitemlist.Add(itemid, this);
		}
		islink = data1.link == 1;
		if (islink)
		{
			btn_open.onClick.AddListener(delegate
			{
				if (!gameManager.homeScene.Iscanopentool())
				{
					if (gameManager.homeScene.newbrowserDialog != null)
					{
						gameManager.homeScene.newbrowserDialog.ResumeMinimize();
					}
					JumpLine(data1.image.Substring(1), 4, I18N.instance.getValue(data1.message));
					CancelClick();
				}
			});
		}
		else if (data1.form == 3 || data1.form == 4)
		{
			img_pic.gameObject.SetActive(value: true);
			txt_content.rectTransform.sizeDelta = new Vector2(232f, txt_content.rectTransform.sizeDelta.y);
			btn_open.onClick.AddListener(delegate
			{
				ShowPicture();
				CancelClick();
			});
		}
		else if (data1.form == 5)
		{
			img_pic.gameObject.SetActive(value: true);
			txt_content.rectTransform.sizeDelta = new Vector2(232f, txt_content.rectTransform.sizeDelta.y);
			btn_open.onClick.AddListener(delegate
			{
				ShowMap();
				CancelClick();
			});
		}
		else if (data1.form == 8)
		{
			btn_open.onClick.AddListener(delegate
			{
				ShowLink();
				CancelClick();
			});
		}
		else if (data1.form == 9)
		{
			btn_open.onClick.AddListener(delegate
			{
				ShowLiveBroading();
				CancelClick();
			});
		}
		else
		{
			btn_copy.onClick.AddListener(delegate
			{
				if (gameManager.player.playerdata.isCourse01 == 0)
				{
					gameManager.homeScene.courseManager.coursepanel01.isclickcopy = true;
					gameManager.homeScene.courseManager.coursepanel01.Next();
				}
				GUIUtility.systemCopyBuffer = I18N.instance.getValue(data1.message);
				CancelClick();
			});
		}
		iscanclick = true;
		if (isadd)
		{
			StartAnimation();
		}
		else
		{
			NoAnimation();
		}
		AddLog(data1);
	}

	private void AddLog(DATA1 data1)
	{
		string text = "[" + gameManager.homeScene.hB3Top.crtTime.text + "]>>>>>>>>>>>>";
		string text2 = "";
		if (!gameManager.player.playerdata.logRealName.ContainsKey(data1.role.Substring(1)))
		{
			if (data1.changename == 1)
			{
				text2 = data1.message;
				gameManager.player.playerdata.logRealName.Add(data1.role.Substring(1), data1.message);
			}
			else
			{
				text2 = gameManager.dataManager.dic31[data1.role.Substring(1)].name;
				gameManager.player.playerdata.logRealName.Add(data1.role.Substring(1), "0");
			}
		}
		else
		{
			if (data1.changename == 1 && gameManager.player.playerdata.logRealName[data1.role.Substring(1)] == "0")
			{
				gameManager.player.playerdata.logRealName[data1.role.Substring(1)] = data1.message;
			}
			text2 = ((gameManager.player.playerdata.logRealName[data1.role.Substring(1)] != "0") ? gameManager.player.playerdata.logRealName[data1.role.Substring(1)] : gameManager.dataManager.dic31[data1.role.Substring(1)].name);
		}
		text += string.Format(I18N.instance.getValue("^logtip01"), I18N.instance.getValue(data1.sources), I18N.instance.getValue(text2), I18N.instance.getValue(data1.title));
		gameManager.homeScene.logPanel.AddLog(text);
	}

	private void NoAnimation()
	{
		CancelInvoke();
		img_upload.fillAmount = 1f;
		img_upload.sprite = sprites[2];
		img_uploadmask.gameObject.SetActive(value: false);
		txt_content.color = bluecolor;
		txt_content.text = "";
		txt_content.DOText(I18N.instance.getValue(data1.title) + ":" + I18N.instance.getValue(data1.message), 0.5f);
		if (gameManager.dataManager.dic1[itemid].sign == 7)
		{
			Invoke("SetCenter", 0.5f);
		}
		gameManager.homeScene.notebook.HideCodeDialog(itemid);
		if (!data1.label.Equals("") && data1.label != null)
		{
			string[] array = data1.label.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNoteItemToolName()), iconGroup);
				if (data1.form == 4)
				{
					gameObject.GetComponent<NoteItemTool>().imgstr = data1.image + "scan";
					gameObject.GetComponent<NoteItemTool>().imgName = data1.image;
				}
				gameObject.GetComponent<NoteItemTool>().Init(gameManager, int.Parse(array[i]));
			}
		}
		img_fx.gameObject.SetActive(data1.fx == 1);
	}

	private void StartAnimation()
	{
		if (gameManager.IsAllDlc())
		{
			InvokeRepeating("Time_count", 0.1f, 0.3f);
			return;
		}
		InvokeRepeating("Time_count0", 0.1f, 0.01f);
		img_upload.DOFillAmount(1f, 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			CancelInvoke();
			img_upload.sprite = sprites[2];
			InvokeRepeating("Time_count", 0.1f, 0.3f);
		});
	}

	private void SetCenter()
	{
		Debug.Log("重新刷一遍");
		if (parObj != null)
		{
			parObj.CenterOnItem(GetComponent<RectTransform>());
		}
	}

	private void Time_count0()
	{
		if (count_down <= 99)
		{
			count_down++;
			txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^notedialog01") + "..." + count_down + "%");
		}
		else
		{
			CancelInvoke();
		}
	}

	private void Time_count()
	{
		if (gameManager.IsAllDlc())
		{
			strdotcount = 4;
		}
		else
		{
			if (strdotpos < str_dots.Length)
			{
				txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^notedialog02") + str_dots.Substring(0, strdotpos));
			}
			strdotpos++;
			if (strdotpos == 3)
			{
				strdotpos = 1;
			}
			strdotcount++;
		}
		if (strdotcount < 4)
		{
			return;
		}
		img_uploadmask.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f).SetEase(Ease.InOutElastic);
		img_uploadmask.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.InExpo)
			.OnComplete(delegate
			{
				img_uploadmask.gameObject.SetActive(value: false);
				txt_content.color = bluecolor;
				txt_content.text = "";
				txt_content.DOText(I18N.instance.getValue(data1.title) + ":" + I18N.instance.getValue(data1.message), 0.5f);
				if (gameManager.dataManager.dic1[itemid].sign == 7)
				{
					Invoke("SetCenter", 0.5f);
				}
				gameManager.homeScene.notebook.HideCodeDialog(itemid);
				if (gameManager.homeScene.zhibojiannotebook != null)
				{
					gameManager.homeScene.zhibojiannotebook.HideCodeDialog(itemid);
				}
				if (!data1.label.Equals("") && data1.label != null)
				{
					string[] array = data1.label.Substring(1).Split(';');
					for (int i = 0; i < array.Length; i++)
					{
						GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNoteItemToolName()), iconGroup);
						if (data1.form == 4)
						{
							gameObject.GetComponent<NoteItemTool>().imgstr = data1.image + "scan";
							gameObject.GetComponent<NoteItemTool>().imgName = data1.image;
						}
						gameObject.GetComponent<NoteItemTool>().Init(gameManager, int.Parse(array[i]));
					}
				}
			});
		img_fx.gameObject.SetActive(data1.fx == 1);
		CancelInvoke();
	}

	public void CancelClick()
	{
		if (!iscanclick)
		{
			img_notclick.gameObject.SetActive(value: false);
			iscanclick = true;
			if (islink || data1.form == 3 || data1.form == 4 || data1.form == 5 || data1.form == 8 || data1.form == 9)
			{
				btn_open.GetComponent<CanvasGroup>().alpha = 0f;
				btn_open.gameObject.SetActive(value: false);
			}
			else
			{
				btn_copy.GetComponent<CanvasGroup>().alpha = 0f;
				btn_copy.gameObject.SetActive(value: false);
			}
		}
	}

	public void Click(bool isshowbutton = true)
	{
		if (!iscanclick)
		{
			return;
		}
		gameManager.homeScene.notebook.transform.SetAsLastSibling();
		iscanclick = false;
		img_notclick.gameObject.SetActive(value: true);
		if (isshowbutton)
		{
			if (islink || data1.form == 3 || data1.form == 4 || data1.form == 5 || data1.form == 8 || data1.form == 9)
			{
				btn_open.gameObject.SetActive(value: true);
				btn_open.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			}
			else
			{
				btn_copy.gameObject.SetActive(value: true);
				btn_copy.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			}
		}
		else
		{
			iscanclick = true;
		}
	}

	public void ShowMap()
	{
		Debug.Log(gameManager.homeScene.middle.Find(data1.image.ToString()) == null);
		if (gameManager.homeScene.middle.Find(data1.image.ToString()) == null)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Image/" + data1.image.ToString()), gameManager.homeScene.middle);
			gameObject.name = data1.image.ToString();
			string track = gameManager.dataManager.dic1[itemid].track;
			if (track.Trim().Equals(""))
			{
				Debug.LogError("there is no track");
			}
			else
			{
				gameObject.GetComponent<SurveilanceMap>().Init(track.Substring(1), gameManager);
			}
		}
	}

	public void ShowPicture()
	{
		if (gameManager.homeScene.middle.Find(data1.image.ToString()) == null)
		{
			Debug.Log(data1.image.ToString());
			string text = data1.image.ToString() + ((data1.form == 4) ? "scan" : "open");
			if (I18N.instance.gameLang.Equals(LanguageCode.EN) && (bool)Resources.Load<GameObject>("Image/" + text + "_en"))
			{
				text += "_en";
			}
			Debug.Log(text);
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Image/" + text), gameManager.homeScene.middle);
			gameObject.name = data1.image.ToString();
			gameObject.transform.DOLocalMove(Vector3.zero, 0.3f);
			if (gameObject.GetComponent<ReasonPic>() != null)
			{
				gameObject.GetComponent<ReasonPic>().Show();
			}
		}
		else
		{
			gameManager.homeScene.middle.Find(data1.image.ToString()).transform.DOLocalMove(Vector3.zero, 0.3f);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!ishighlight)
		{
			img_bk.sprite = sprites[1];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!ishighlight)
		{
			img_bk.sprite = sprites[0];
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}

	private void Update()
	{
		if (!iscanclick && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
		{
			if (gameManager.player.playerdata.isCourse01 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel01.isclickcopy = true;
				gameManager.homeScene.courseManager.coursepanel01.Next();
			}
			GUIUtility.systemCopyBuffer = I18N.instance.getValue(data1.message);
			CancelClick();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		NoteDragManager.Instance.DragStart(eventData, data1);
	}

	public void OnDrag(PointerEventData eventData)
	{
		NoteDragManager.Instance.Draging(eventData, data1);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		NoteDragManager.Instance.DragEnd(eventData, data1);
	}
}
