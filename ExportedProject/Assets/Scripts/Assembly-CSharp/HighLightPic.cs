using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLightPic : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject buttons;

	public Button btn_add;

	public Button btn_sign;

	public Image img_notclick;

	private GameManager gameManager;

	public string itemid;

	public string otheritemid;

	public bool iscanclick = true;

	public Image img_light;

	public Image img_sign;

	public Sprite[] sprites;

	public bool iscancancle;

	public bool isscan;

	public bool iscancollect;

	public bool isneedautoinit;

	public bool dontCollect;

	public string collectLabel;

	public bool delFile;

	public bool isneedhighlight;

	public bool isInvade;

	[SerializeField]
	private Transform topparent;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (isneedautoinit)
		{
			SetContent(itemid);
		}
	}

	public void Enter()
	{
		if (iscancollect && !img_sign.gameObject.activeSelf && gameManager.isshowredline)
		{
			img_light.gameObject.SetActive(value: true);
		}
	}

	public void Exit()
	{
		if (iscancollect && !img_sign.gameObject.activeSelf && gameManager.isshowredline)
		{
			img_light.gameObject.SetActive(value: false);
		}
	}

	public void Selected()
	{
		if (iscancollect && img_sign != null)
		{
			img_sign.gameObject.SetActive(value: true);
		}
	}

	public void ShowButtons()
	{
		if (iscanclick && !(itemid == "") && !(itemid == " ") && !(itemid == "0"))
		{
			if (topparent != null)
			{
				topparent.SetAsLastSibling();
			}
			SetContent(itemid);
			img_notclick.gameObject.SetActive(value: true);
			buttons.SetActive(value: true);
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(buttons.GetComponent<RectTransform>(), Input.mousePosition, gameManager.maincamera.GetComponent<Camera>(), out var worldPoint))
			{
				buttons.transform.position = worldPoint;
			}
			if (img_light != null)
			{
				img_light.gameObject.SetActive(value: true);
			}
			StartButton();
			if (gameManager.player.playerdata.isCourse04 == 0 && itemid.Equals("10058"))
			{
				gameManager.homeScene.courseManager.coursepanel04.tbpic = base.gameObject;
				gameManager.homeScene.courseManager.ShowCourse4();
			}
			iscanclick = false;
		}
	}

	public void SetContent(string id)
	{
		itemid = id;
		if (gameManager.GameType == GameTypeEnum.DLC6 && itemid.Contains("11136*"))
		{
			itemid = "11136";
			otheritemid = id.Replace("11136*", "");
		}
		iscancollect = true;
		if (!dontCollect)
		{
			if (gameManager.player.playerdata.itemlist.Contains(itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(itemid))
			{
				btn_add.interactable = false;
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			}
			else if (delFile)
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^file_load05");
			}
			else
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^highlighttip01");
			}
		}
		else if (gameManager.player.playerdata.itemlist.Contains(itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(itemid) || (gameManager.homeScene.invadeDialog != null && gameManager.homeScene.invadeDialog.downloadDialog.loadingFile.Contains(itemid)))
		{
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^file_load01");
		}
		if (gameManager.player.playerdata.itemlist.Contains(itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(itemid))
		{
			Selected();
		}
	}

	private void Start()
	{
		btn_add.onClick.AddListener(delegate
		{
			if (!delFile)
			{
				gameManager.soundManager.PlaySound(24);
				img_notclick.gameObject.SetActive(value: false);
				CloseButton();
				if (dontCollect)
				{
					if (gameManager.homeScene.invadeDialog.gameObject.activeInHierarchy)
					{
						gameManager.homeScene.invadeDialog.downloadDialog.StartLoad(itemid);
						iscanclick = true;
						btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(collectLabel);
						btn_add.interactable = false;
					}
				}
				else
				{
					gameManager.homeScene.notebook.gameObject.SetActive(value: true);
					if (itemid == "10227")
					{
						gameManager.homeScene.notebook.AddNewItem(itemid, isneedhighlight: true);
					}
					else if (!otheritemid.Equals(""))
					{
						if (otheritemid.Contains("*"))
						{
							string[] array = otheritemid.Split('*');
							string[] array2 = new string[1 + array.Length];
							if (gameManager.IsAllDlc())
							{
								for (int i = 0; i < array.Length; i++)
								{
									array2[i] = array[i];
								}
								array2[array.Length] = itemid;
							}
							else
							{
								array2[0] = itemid;
								for (int j = 0; j < array.Length; j++)
								{
									array2[j + 1] = array[j];
								}
							}
							gameManager.homeScene.notebook.AddNewItems(array2, isneedhighlight);
						}
						else
						{
							string[] ids = new string[2] { itemid, otheritemid };
							gameManager.homeScene.notebook.AddNewItems(ids, isneedhighlight);
						}
					}
					else if (gameManager.IsAllDlc())
					{
						if (itemid.Contains("*"))
						{
							string[] ids2 = itemid.Split('*');
							gameManager.homeScene.notebook.AddNewItems(ids2, isneedhighlight);
						}
						else
						{
							string[] ids3 = new string[1] { itemid };
							gameManager.homeScene.notebook.AddNewItems(ids3, isneedhighlight);
						}
					}
					else
					{
						int num = int.Parse(gameManager.dataManager.dic1[itemid].role.Substring(1));
						if (num >= 3100036 && num <= 3100047)
						{
							gameManager.homeScene.zhibojiannotebook.AddNewItem(itemid, isneedhighlight);
						}
						else
						{
							gameManager.homeScene.notebook.AddNewItem(itemid, isneedhighlight);
						}
					}
					iscanclick = true;
					btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
					if (isInvade)
					{
						gameManager.homeScene.invadeDialog.listBox.CompleteTask();
					}
					if (gameManager.player.playerdata.isCourse04 == 0)
					{
						gameManager.homeScene.courseManager.coursepanel04.HideCourse();
					}
					Selected();
				}
			}
			else
			{
				gameManager.homeScene.invadeDialog.DelFile(itemid);
				gameManager.homeScene.notebook.AddNewItem(itemid, isneedhighlight);
			}
		});
		if (img_sign != null)
		{
			if (gameManager.player.playerdata.itemlist.Contains(itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(itemid))
			{
				img_sign.gameObject.SetActive(value: true);
			}
			else
			{
				img_sign.gameObject.SetActive(value: false);
			}
		}
	}

	public void CancelClick()
	{
		if (iscancancle)
		{
			img_notclick.gameObject.SetActive(value: false);
			CloseButton();
			if (img_light != null)
			{
				img_light.gameObject.SetActive(value: false);
			}
			iscanclick = true;
		}
	}

	private void StartButton()
	{
		buttons.GetComponent<RectTransform>().DOKill();
		buttons.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).OnComplete(delegate
		{
			iscancancle = true;
		});
		if (buttons.GetComponent<CanvasGroup>() == null)
		{
			buttons.AddComponent<CanvasGroup>().DOFade(1f, 0.3f);
			return;
		}
		buttons.GetComponent<CanvasGroup>().DOKill();
		buttons.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	private void CloseButton()
	{
		buttons.GetComponent<RectTransform>().DOKill();
		buttons.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f).OnComplete(delegate
		{
			iscancancle = false;
			buttons.SetActive(value: false);
		});
		if (buttons.GetComponent<CanvasGroup>() != null)
		{
			buttons.GetComponent<CanvasGroup>().DOKill();
			buttons.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Exit();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		ShowButtons();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Enter();
	}
}
