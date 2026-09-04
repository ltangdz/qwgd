using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class NewsPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public Text txt_title;

	public Button btn_open;

	public I18NText txt_btndetail;

	public Transform contentPanel;

	public GameManager gameManager;

	public List<string> newslist = new List<string>();

	public Button btn_left;

	public Button btn_right;

	public NewsContentItem currentitem;

	public int pos = -1;

	public bool isopen;

	private void LunBoNews()
	{
		if (newslist.Count >= 2 && currentitem != null)
		{
			int num = newslist.IndexOf(currentitem.id);
			if (num == newslist.Count - 1)
			{
				SetNewContent(newslist[0]);
			}
			else
			{
				SetNewContent(newslist[num + 1]);
			}
		}
	}

	public void SetNewContent(string id)
	{
		if (!newslist.Contains(id) && !id.Equals("0"))
		{
			newslist.Add(id);
			SetLeftRightBtn();
		}
		AddItem(id, isleft: false);
	}

	public void LeftNewContent(string id)
	{
		if (!newslist.Contains(id) && !id.Equals("0"))
		{
			newslist.Add(id);
			SetLeftRightBtn();
		}
		AddItem(id, isleft: true);
	}

	private void SetLeftRightBtn()
	{
		btn_left.interactable = newslist.Count > 1;
		btn_right.interactable = newslist.Count > 1;
	}

	private void AddItem(string id, bool isleft)
	{
		if (contentPanel.childCount < 4)
		{
			DATA13 dATA = gameManager.dataManager.dic13[id];
			GameObject gameObject = Object.Instantiate(Resources.Load("newsitem") as GameObject, contentPanel);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(isleft ? (-515) : 515, 0f, 0f);
			gameObject.GetComponent<NewsContentItem>().SetContent(id);
			gameObject.GetComponent<NewsContentItem>().Show();
			if (currentitem != null)
			{
				currentitem.Hide(isleft);
			}
			currentitem = gameObject.GetComponent<NewsContentItem>();
			int num = 1;
			if (currentitem != null)
			{
				num = newslist.IndexOf(currentitem.id) + 1;
			}
			txt_title.DOKill();
			txt_title.text = "";
			txt_title.GetComponent<RectTransform>().DOKill();
			txt_title.GetComponent<RectTransform>().anchoredPosition = new Vector2(478f, 0f);
			float num2 = CalculateLengthOfText(I18N.instance.getValue(dATA.title) + "(" + num + "/" + newslist.Count + ")");
			txt_title.DOText(I18N.instance.getValue(dATA.title) + "(" + num + "/" + newslist.Count + ")", 1f);
			txt_title.GetComponent<RectTransform>().DOLocalMoveX(-478f - num2 - 10f, 10f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					txt_title.GetComponent<RectTransform>().anchoredPosition = new Vector2(478f, 0f);
				})
				.SetLoops(-1);
		}
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txt_title.font;
		font.RequestCharactersInTexture(message, txt_title.fontSize, txt_title.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt_title.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		SetLeftRightBtn();
		InvokeRepeating("LunBoNews", 1f, 60f);
		btn_open.onClick.AddListener(delegate
		{
			OpenNews();
		});
		btn_left.onClick.AddListener(delegate
		{
			if (currentitem != null)
			{
				int num = newslist.IndexOf(currentitem.id);
				if (num == 0)
				{
					LeftNewContent(newslist[newslist.Count - 1]);
				}
				else
				{
					LeftNewContent(newslist[num - 1]);
				}
			}
		});
		btn_right.onClick.AddListener(delegate
		{
			LunBoNews();
		});
	}

	public void OpenNews(int open = -1)
	{
		if (open != -1)
		{
			isopen = ((open != 1) ? true : false);
		}
		if (!isopen)
		{
			StartCoroutine(UpdateLayout(contentPanel.transform.parent.GetComponent<RectTransform>()));
			contentPanel.DOScaleY(1f, 0.3f).SetEase(Ease.InOutCirc);
			txt_btndetail.updateTranslation2("^newspanel02");
			isopen = !isopen;
			gameManager.homeScene.logPanel.OpenAllList(0);
		}
		else
		{
			StartCoroutine(UpdateLayout(contentPanel.transform.parent.GetComponent<RectTransform>()));
			contentPanel.DOScaleY(0f, 0.3f).SetEase(Ease.InOutCirc);
			txt_btndetail.updateTranslation2("^newspanel01");
			isopen = !isopen;
		}
	}

	private IEnumerator UpdateLayout(RectTransform rect)
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
		yield return new WaitForEndOfFrame();
		_ = rect.localScale;
		float scaley = rect.localScale.y;
		while (scaley == 0f)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
			yield return new WaitForEndOfFrame();
		}
		while (scaley == 1f)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
			yield return new WaitForEndOfFrame();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		CancelInvoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CancelInvoke();
		InvokeRepeating("LunBoNews", 1f, 60f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		base.transform.SetAsLastSibling();
	}
}
