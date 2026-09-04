using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class MultiplyText : MonoBehaviour
{
	public TextBkg textBkg;

	public Dictionary<string, Image> dic = new Dictionary<string, Image>();

	public string keyword;

	public GameObject buttons;

	public bool iscancancle;

	public bool iscanclick;

	public Image img_notclick;

	public Button btn_add;

	public Button btn_sign;

	public MultiplyTextRedImage currentmultiplyTextRedImage;

	public GameManager gameManager;

	public Color color;

	public Transform contentPanel;

	public bool iscanaddtoitem = true;

	public bool ishad;

	public string[] otheritem;

	public bool isneeditemboxhighlight;

	public bool isneedloadfile;

	[SerializeField]
	private bool isneedinit;

	[SerializeField]
	private bool isneedsetnewwidth;

	[SerializeField]
	private string contentkey = "";

	[SerializeField]
	private string itemtid = "";

	public bool isneedmouseposver;

	public bool isInvade;

	public bool isshow;

	public void AudoWidth(float maxlength, string content)
	{
		if (CalculateLengthOfText(content) < maxlength)
		{
			SetNewWidth2(content);
		}
		else
		{
			textBkg.GetComponent<RectTransform>().sizeDelta = new Vector2(maxlength, textBkg.GetComponent<RectTransform>().sizeDelta.y);
		}
	}

	public void SetIscanaddtoItem(bool iscan)
	{
		iscanaddtoitem = iscan;
	}

	public void SetHadColor()
	{
		ishad = true;
	}

	public void SetContent(string content, bool iswarp = false, bool istypeeffect = false)
	{
		if (!istypeeffect)
		{
			textBkg.i18ntext.updateTranslation2(content);
		}
		else
		{
			textBkg.text.DOText(I18N.instance.getValue(content), 0.5f);
		}
		if (textBkg.i18ntext.GetComponent<NonBreakingSpaceTextComponent>() != null)
		{
			textBkg.i18ntext.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
		}
		if (iswarp)
		{
			textBkg.text.horizontalOverflow = HorizontalWrapMode.Wrap;
		}
	}

	public void SetContent2(string content, string itemid, string key, bool istypeeffect = false, bool isaddblank = false)
	{
		StartCoroutine(StartAddContent((isaddblank ? "\u3000\u3000" : "") + I18N.instance.getValue(content), itemid, key, istypeeffect));
	}

	public void SetContent3(string content, string itemid, string key, bool istypeeffect = false)
	{
		StartCoroutine(StartAddContent(content, itemid, key, istypeeffect));
	}

	public void SetNewWidth(string content)
	{
		if (textBkg.issetnewwidth)
		{
			float x = CalculateLengthOfText(content);
			GetComponent<RectTransform>().sizeDelta = new Vector2(x, GetComponent<RectTransform>().sizeDelta.y);
			textBkg.GetComponent<RectTransform>().sizeDelta = new Vector2(x, textBkg.GetComponent<RectTransform>().sizeDelta.y);
		}
	}

	public void SetNewWidth2(string content)
	{
		float x = CalculateLengthOfText(content);
		GetComponent<RectTransform>().sizeDelta = new Vector2(x, GetComponent<RectTransform>().sizeDelta.y);
		textBkg.GetComponent<RectTransform>().sizeDelta = new Vector2(x, textBkg.GetComponent<RectTransform>().sizeDelta.y);
	}

	private float CalculateLengthOfText(string message)
	{
		TextGenerationSettings generationSettings = textBkg.text.GetGenerationSettings(Vector2.zero);
		generationSettings.scaleFactor = 1f;
		return textBkg.text.cachedTextGeneratorForLayout.GetPreferredWidth(message, generationSettings);
	}

	public void SetContentPanel(Transform contentPanel)
	{
		this.contentPanel = contentPanel;
	}

	private IEnumerator StartAddContent(string content, string itemid, string key, bool istypeeffect)
	{
		yield return new WaitForSeconds(0f);
		AddContent(content, itemid, key, istypeeffect);
		RefreshContent();
	}

	public void Click(MultiplyTextRedImage multiplyTextRedImage)
	{
		isshow = true;
		MultiplyTextRedImage img = null;
		currentmultiplyTextRedImage = multiplyTextRedImage;
		multiplyTextRedImage.SetWhite();
		if (multiplyTextRedImage.linkImage != null)
		{
			multiplyTextRedImage.linkImage.SetWhite();
		}
		if (multiplyTextRedImage.pos == 0 && multiplyTextRedImage.linkImage == null)
		{
			img = multiplyTextRedImage;
			textBkg.text.text = textBkg.oldstr.Replace(currentmultiplyTextRedImage.content, "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + currentmultiplyTextRedImage.content + "</color>");
		}
		else if (multiplyTextRedImage.pos == 0 && multiplyTextRedImage.linkImage != null)
		{
			img = multiplyTextRedImage.linkImage;
			if (!gameManager.isshowredline)
			{
				multiplyTextRedImage.linkImage.Click();
			}
		}
		else if (multiplyTextRedImage.pos == 1)
		{
			img = multiplyTextRedImage;
			textBkg.text.text = textBkg.oldstr.Replace(multiplyTextRedImage.linkImage.content + "\n" + currentmultiplyTextRedImage.content, "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + multiplyTextRedImage.linkImage.content + "\n" + currentmultiplyTextRedImage.content + "</color>");
		}
		ShowButton(img);
	}

	public void ClickEnter(MultiplyTextRedImage multiplyTextRedImage, bool isselect = false)
	{
		currentmultiplyTextRedImage = multiplyTextRedImage;
		if (gameManager.isshowredline || isselect)
		{
			if (multiplyTextRedImage.pos == 0 && multiplyTextRedImage.linkImage == null)
			{
				textBkg.text.text = textBkg.oldstr.Replace(currentmultiplyTextRedImage.content, "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + currentmultiplyTextRedImage.content + "</color>");
			}
			else if ((multiplyTextRedImage.pos != 0 || !(multiplyTextRedImage.linkImage != null)) && multiplyTextRedImage.pos == 1)
			{
				textBkg.text.text = textBkg.oldstr.Replace(multiplyTextRedImage.linkImage.content + "\n" + currentmultiplyTextRedImage.content, "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + multiplyTextRedImage.linkImage.content + "\n" + currentmultiplyTextRedImage.content + "</color>");
			}
		}
	}

	public void CancelClickExit(MultiplyTextRedImage multiplyTextRedImage)
	{
		currentmultiplyTextRedImage = multiplyTextRedImage;
		if (!ishad)
		{
			textBkg.text.text = textBkg.oldstr.Replace("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + currentmultiplyTextRedImage.content + "</color>", currentmultiplyTextRedImage.content);
			if (currentmultiplyTextRedImage != null)
			{
				currentmultiplyTextRedImage.CancelClick();
			}
		}
	}

	private Vector3 GetPosition(RectTransform btnRectTrans)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(btnRectTrans.transform.parent.parent as RectTransform, Input.mousePosition, Camera.main, out var localPoint);
		return localPoint;
	}

	public void ShowButton(MultiplyTextRedImage img)
	{
		gameManager.iscancollect = false;
		if (isneedmouseposver)
		{
			buttons.GetComponent<RectTransform>().anchoredPosition = GetPosition(buttons.GetComponent<RectTransform>());
		}
		else
		{
			buttons.GetComponent<RectTransform>().localPosition = new Vector2(img.GetComponent<RectTransform>().localPosition.x + img.GetComponent<RectTransform>().sizeDelta.x, img.GetComponent<RectTransform>().localPosition.y);
		}
		buttons.SetActive(value: true);
		if (gameManager.player.playerdata.itemlist.Contains(img.itemid) || gameManager.player.playerdata.temporaryhopelist.Contains(img.itemid) || (gameManager.homeScene.invadeDialog != null && gameManager.homeScene.invadeDialog.downloadDialog.loadingFile.Contains(img.itemid)))
		{
			btn_add.interactable = false;
			if (isneedloadfile)
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^file_load01");
			}
			else
			{
				CancelClick2();
				ishad = true;
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			}
		}
		else
		{
			btn_add.interactable = true;
			if (isneedloadfile)
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^file_load04");
			}
			else
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^highlighttip01");
			}
		}
		buttons.GetComponent<CanvasGroup>().DOKill();
		buttons.GetComponent<RectTransform>().DOKill();
		buttons.GetComponent<CanvasGroup>().alpha = 0f;
		buttons.GetComponent<RectTransform>().localScale = Vector3.zero;
		buttons.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		buttons.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutCirc)
			.OnComplete(delegate
			{
				iscancancle = true;
				gameManager.iscancollect = true;
				img_notclick.gameObject.SetActive(value: true);
			});
	}

	public void CancelClick()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "multiplytext")
			{
				return;
			}
		}
		gameManager.iscancollect = true;
		buttons.SetActive(value: false);
		isshow = false;
		CancelClick2();
		if (!ishad)
		{
			textBkg.text.text = textBkg.oldstr.Replace("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + currentmultiplyTextRedImage.content + "</color>", currentmultiplyTextRedImage.content);
			if (currentmultiplyTextRedImage != null)
			{
				currentmultiplyTextRedImage.CancelClick();
			}
		}
	}

	public void CancelClick2()
	{
		if (iscancancle)
		{
			img_notclick.gameObject.SetActive(value: false);
			iscanclick = true;
			buttons.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
			buttons.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutCirc)
				.OnComplete(delegate
				{
					CancelInvoke();
					iscancancle = false;
					buttons.SetActive(value: false);
				});
		}
	}

	public void RefreshContent()
	{
		if (contentPanel != null)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel as RectTransform);
		}
	}

	public void AddContent(string content, string itemid, string strFragment, bool istypeeffect)
	{
		textBkg.SetContent(content, itemid, strFragment, istypeeffect);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_add.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(24);
			if (isneedloadfile)
			{
				btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^file_load01");
			}
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			gameManager.homeScene.notebook.gameObject.SetActive(value: true);
			if (isneedloadfile)
			{
				if (!gameManager.homeScene.invadeDialog.gameObject.activeInHierarchy)
				{
					return;
				}
				gameManager.homeScene.invadeDialog.downloadDialog.StartLoad(itemtid);
			}
			else
			{
				if (otheritem.Length != 0)
				{
					gameManager.homeScene.notebook.AddNewItems(otheritem);
				}
				else
				{
					DATA1 dATA = gameManager.dataManager.dic1[currentmultiplyTextRedImage.itemid];
					int num = int.Parse(dATA.role.Substring(1));
					if (num >= 3100036 && num <= 3100047)
					{
						if (gameManager.homeScene.liveBroadcastingDialog != null)
						{
							gameManager.homeScene.liveBroadcastingDialog.StartGame(currentmultiplyTextRedImage.itemid);
						}
						if (!gameManager.player.playerdata.islivecourse)
						{
							gameManager.player.playerdata.islivecourse = true;
							gameManager.saveManager.SavePlayerData();
							if (gameManager.homeScene.liveBroadingChatBox != null)
							{
								gameManager.homeScene.liveBroadingChatBox.HideCourse1();
							}
						}
						else if (gameManager.homeScene.liveBroadingChatBox != null)
						{
							gameManager.homeScene.liveBroadingChatBox.Hide();
						}
						if (!dATA.ID.ToString().Equals("10559"))
						{
							if (gameManager.homeScene.liveBroadcastingDialog != null)
							{
								gameManager.homeScene.liveBroadcastingDialog.Hide();
							}
						}
						else if (gameManager.homeScene.liveBroadcastingDialog != null)
						{
							gameManager.homeScene.liveBroadcastingDialog.BossStart();
						}
						gameManager.homeScene.zhibojiannotebook.AddNewItem(currentmultiplyTextRedImage.itemid, isneeditemboxhighlight);
					}
					else
					{
						gameManager.homeScene.notebook.AddNewItem(currentmultiplyTextRedImage.itemid, isneeditemboxhighlight);
					}
				}
				if (isInvade)
				{
					gameManager.homeScene.invadeDialog.listBox.CompleteTask();
				}
				if (gameManager.player.playerdata.isCourse03 == 0 && currentmultiplyTextRedImage.itemid.Equals("10057"))
				{
					gameManager.homeScene.courseManager.coursepanel03.HideCourse0();
				}
			}
			CancelClick2();
			gameManager.iscancollect = true;
			ishad = true;
		});
		if (isneedinit)
		{
			SetContent2(contentkey, itemtid, I18N.instance.getValue(keyword));
		}
		if (isneedsetnewwidth)
		{
			SetNewWidth(I18N.instance.getValue(contentkey));
		}
	}
}
