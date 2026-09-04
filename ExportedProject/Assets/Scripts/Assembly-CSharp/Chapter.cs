using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chapter : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public RectTransform imgBk;

	public GameObject buyType;

	public GameObject dataBox;

	public Image progressBar;

	public GameObject complete;

	public GameObject noComplete;

	public GameObject locked;

	public Text comVal;

	public GameObject buttonbox;

	public Button btnBuy;

	public Button btnDownload;

	public Image choiceBox;

	public Image zhezhao;

	public string eventID;

	public ChoiceLevel parObj;

	public bool isDLC;

	private bool choiced;

	[SerializeField]
	private bool isBuy;

	[SerializeField]
	private bool isDownload;

	[SerializeField]
	private bool isLocked;

	[SerializeField]
	private float completeVal;

	private float totalVal;

	private GameManager gameManager;

	public int eventid;

	[SerializeField]
	private Text txt_time;

	public bool IsBuy
	{
		get
		{
			return isBuy;
		}
		set
		{
			isBuy = value;
		}
	}

	public bool IsDownload
	{
		get
		{
			return isDownload;
		}
		set
		{
			isDownload = value;
		}
	}

	public bool IsLocked
	{
		get
		{
			return isLocked;
		}
		set
		{
			isLocked = value;
		}
	}

	private void OnEnable()
	{
		Refresh();
	}

	public void Refresh()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		buyType.SetActive(value: true);
		choiceBox.gameObject.SetActive(value: false);
		dataBox.SetActive(value: false);
		locked.SetActive(value: false);
		noComplete.SetActive(value: false);
		complete.SetActive(value: false);
		buttonbox.SetActive(value: false);
		btnBuy.gameObject.SetActive(value: false);
		btnDownload.gameObject.SetActive(value: false);
		zhezhao.gameObject.SetActive(value: true);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.alllevelinfo.ContainsKey(4) && !gameManager.player.playerdata.alllevelinfo.ContainsKey(4).Equals("0"))
		{
			gameManager.player.OpenSpecialLevel(5);
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.playerdata.alllevelinfo.ContainsKey(5) && !gameManager.player.playerdata.alllevelinfo.ContainsKey(5).Equals("0"))
		{
			gameManager.player.OpenSpecialLevel(6);
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player != null)
		{
			string level = gameManager.player.GetLevel(eventid);
			if (level.Equals(""))
			{
				totalVal = gameManager.dataManager.dic11[eventID].number;
				if (eventID == "110000")
				{
					ChapterType(3);
				}
				else
				{
					ChapterType(2);
				}
				CompleteValue(0f);
			}
			else
			{
				ChapterType(3);
				CompleteValue(int.Parse(level));
			}
			string levelTime = gameManager.player.GetLevelTime(eventid);
			if (!levelTime.Equals(""))
			{
				txt_time.gameObject.SetActive(value: true);
				txt_time.text = I18N.instance.getValue("^leveltime") + levelTime + " min";
			}
			else
			{
				txt_time.gameObject.SetActive(value: false);
			}
		}
		else
		{
			totalVal = gameManager.dataManager.dic11[eventID].number;
			ChapterType(2);
		}
		if (eventid == 7)
		{
			gameManager.IsBuySweetDLC();
			if (gameManager.isBuySweetDlc)
			{
				ChapterType(3);
			}
			else
			{
				ChapterType(0);
			}
		}
		if (eventid == 8)
		{
			gameManager.IsBuyDLC(DLCEnum.HELLO_WORLD);
			if (gameManager.isBuyHelloWorldDlc)
			{
				ChapterType(3);
			}
			else
			{
				ChapterType(0);
			}
		}
		SetInit();
	}

	private void Start()
	{
		Refresh();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			if (isBuy && isDownload && !isLocked)
			{
				choiced = true;
				Focus();
				parObj.eventid = eventid;
				if (eventid == 7)
				{
					parObj.isDLC = true;
					gameManager.player.playerdata.isDLC = true;
					gameManager.IsDlc = true;
					gameManager.GameType = GameTypeEnum.DLC6;
					gameManager.player.playerdata.GameType = GameTypeEnum.DLC6;
				}
				else if (eventid == 8)
				{
					parObj.isDLC = false;
					gameManager.player.playerdata.isDLC = false;
					gameManager.IsDlc = false;
					gameManager.GameType = GameTypeEnum.DLC7;
					gameManager.player.playerdata.GameType = GameTypeEnum.DLC7;
				}
				else
				{
					gameManager.player.playerdata.GameType = GameTypeEnum.BASIC;
					parObj.isDLC = false;
					gameManager.player.playerdata.isDLC = false;
					gameManager.IsDlc = false;
				}
				parObj.startConfirm.SetActive(value: true);
				parObj.startConfirm.GetComponent<Animator>().Play("Exit Panel In");
				Dictionary<int, string> allLevelInfo = gameManager.saveManager.getAllLevelInfo();
				if (allLevelInfo.ContainsKey(eventid))
				{
					string text = allLevelInfo[eventid];
					if (string.IsNullOrEmpty(text) || text == "0")
					{
						parObj.titleText.text = I18N.instance.getValue("^110008_common_90");
					}
					else
					{
						parObj.titleText.text = I18N.instance.getValue("^Select_Chapter09");
					}
				}
				else
				{
					parObj.titleText.text = I18N.instance.getValue("^110008_common_90");
				}
				for (int i = 0; i < base.transform.parent.childCount - 1; i++)
				{
					if (base.transform.parent.GetChild(i).name != base.name)
					{
						base.transform.parent.GetChild(i).GetComponent<Chapter>().choiced = false;
						base.transform.parent.GetChild(i).GetComponent<Chapter>().Blur();
					}
				}
			}
			else
			{
				if (eventid == 7)
				{
					gameManager.ValidDLC6();
				}
				if (eventid == 8)
				{
					gameManager.ValidDLC(8);
				}
			}
		});
	}

	public void ChapterType(int i)
	{
		switch (i)
		{
		case 0:
			IsBuy = false;
			break;
		case 1:
			IsBuy = true;
			IsDownload = false;
			IsBuy = true;
			break;
		case 2:
			IsBuy = true;
			IsDownload = true;
			IsLocked = true;
			break;
		default:
			IsBuy = true;
			IsDownload = true;
			IsLocked = false;
			break;
		}
	}

	public void CompleteValue(float val)
	{
		completeVal = val;
		totalVal = gameManager.dataManager.dic11[eventID].number;
	}

	private void SetInit()
	{
		if (isBuy)
		{
			buyType.SetActive(value: false);
			if (isDownload)
			{
				choiceBox.gameObject.SetActive(value: true);
				dataBox.SetActive(value: true);
				if (isLocked)
				{
					locked.SetActive(value: true);
				}
				else if (completeVal < totalVal && completeVal == 0f)
				{
					noComplete.SetActive(value: true);
				}
				else
				{
					complete.SetActive(value: true);
				}
				progressBar.GetComponent<RectTransform>().DOLocalMoveX(completeVal / totalVal * 326f - 163f, 0.05f);
				comVal.GetComponent<I18NText>().updateTranslation2(completeVal + " / <color=#5c6880>" + totalVal + "</color>");
			}
			else
			{
				buttonbox.SetActive(value: true);
				btnDownload.gameObject.SetActive(value: true);
			}
		}
		else
		{
			buyType.SetActive(value: true);
			buttonbox.SetActive(value: true);
			if (eventid < 7)
			{
				btnBuy.gameObject.SetActive(value: true);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isBuy && isDownload)
		{
			Focus(ischoiced: false);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Blur(ischoiced: false);
	}

	public void Focus(bool ischoiced = true)
	{
		if (ischoiced)
		{
			choiceBox.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		}
		zhezhao.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
		imgBk.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.3f);
	}

	public void Blur(bool ischoiced = true)
	{
		if (!choiced)
		{
			if (ischoiced)
			{
				choiceBox.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			}
			zhezhao.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			imgBk.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		}
	}
}
