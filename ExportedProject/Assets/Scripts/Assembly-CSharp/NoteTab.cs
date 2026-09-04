using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class NoteTab : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public GameManager gameManager;

	public I18NText txt_name;

	public Image img_bk;

	public Sprite[] sprites;

	public Image img_avatar;

	public RectTransform groupRect;

	public string playerid;

	public NotePanel notePanel;

	public Transform tabgroup;

	public string photo;

	public Image img_black;

	public float firsttabposx;

	public bool isselected;

	public int index;

	public DATA31 data_31;

	private bool haveRealName;

	private bool haveRealAvatar;

	private bool haveWhiteAvatar;

	public ItemBox ownitembox;

	public void Init(DATA31 data31, Transform panelgroup, Transform tabgroup, bool istruename, ItemBox ownitembox, string pname = "")
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		data_31 = data31;
		this.tabgroup = tabgroup;
		playerid = data31.ID.ToString();
		this.ownitembox = ownitembox;
		string text = "";
		text = ((!istruename || pname.Equals("")) ? data31.name : pname);
		txt_name.updateTranslation2(text);
		photo = data31.photo;
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetNotePanelName()), panelgroup);
		gameObject.GetComponent<NotePanel>().gameManager = gameManager;
		notePanel = gameObject.GetComponent<NotePanel>();
		notePanel.ownitembox = ownitembox;
		if (gameManager.player.GetEventId().Equals("110004"))
		{
			if (!data31.signs.Equals("#0"))
			{
				gameObject.GetComponent<NotePanel>().SetGrayTitle(data31.signs.Substring(1));
			}
			else
			{
				gameObject.GetComponent<NotePanel>().SetGrayTitle(data31.sign.Substring(1));
			}
		}
		else if (playerid.Equals("3100047"))
		{
			notePanel.ChangeTitle();
			gameObject.GetComponent<NotePanel>().SetGrayTitle(data31.sign.Substring(1));
		}
		else
		{
			gameObject.GetComponent<NotePanel>().SetGrayTitle(data31.sign.Substring(1));
		}
		groupRect.anchoredPosition = new Vector2(firsttabposx + (float)(tabgroup.childCount - 1) * 100f, 0f);
		tabgroup.GetComponent<RectTransform>().sizeDelta = new Vector2(154f + 100f * (float)(tabgroup.childCount - 1), 162f);
		Click();
		OpenAvatar(istruename);
		index = base.transform.GetSiblingIndex();
		base.name = "tab" + index;
		ownitembox.alltablist.Add(this);
	}

	public void CenterOnItem(RectTransform target, ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		Vector3 worldPointInWidget = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(target));
		Vector3 vector = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(scrollRect.viewport)) - worldPointInWidget;
		vector.z = 0f;
		Vector2 vector2 = new Vector2(vector.x / (scrollRect.content.rect.width - scrollRect.viewport.rect.width), vector.y / (scrollRect.content.rect.height - scrollRect.viewport.rect.height));
		vector2 = scrollRect.normalizedPosition - vector2;
		vector2.x = Mathf.Clamp01(vector2.x);
		vector2.y = Mathf.Clamp01(vector2.y);
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, vector2, 1f);
		Canvas.ForceUpdateCanvases();
	}

	private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
	{
		return target.InverseTransformPoint(worldPoint);
	}

	private Vector3 GetWidgetWorldPoint(RectTransform target)
	{
		Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
		Vector3 position = target.localPosition + vector;
		return target.parent.TransformPoint(position);
	}

	public void OpenAvatar(bool istruename)
	{
		if (istruename || (Resources.Load<Sprite>("touxiang/" + photo + "ib") != null && data_31.fakephoto != 1))
		{
			img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + photo + "ib");
			haveRealAvatar = true;
		}
		else if (gameManager.Is_Dlc6())
		{
			img_avatar.sprite = Resources.Load<Sprite>("touxiang/dangan_25");
		}
		else if (gameManager.Is_Dlc6())
		{
			img_avatar.sprite = Resources.Load<Sprite>("touxiang/_dlc7_dangan_02");
		}
		else
		{
			img_avatar.sprite = Resources.Load<Sprite>("touxiang/moren");
		}
		img_avatar.DOFillAmount(1f, 1f).SetEase(Ease.InOutCirc);
	}

	public void Click()
	{
		if (isselected || !ownitembox.iscanchangetab)
		{
			return;
		}
		if (gameManager.Is_Dlc6())
		{
			NoteDragManager.Instance.ChangePlayer(playerid);
		}
		gameManager._selectedPlayerId = playerid;
		ownitembox.iscanchangetab = false;
		for (int i = 0; i < ownitembox.alltablist.Count; i++)
		{
			if (!ownitembox.alltablist[i].gameObject.name.Equals(base.gameObject.name))
			{
				ownitembox.alltablist[i].isselected = true;
				ownitembox.alltablist[i].SetGray();
			}
		}
		base.transform.SetAsLastSibling();
		SetWhite();
	}

	public void SetGray()
	{
		base.transform.SetSiblingIndex(index);
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			img_bk.sprite = sprites[0];
		}
		img_black.gameObject.SetActive(value: true);
		img_bk.SetNativeSize();
		groupRect.DOScale(new Vector3(0.85f, 0.85f, 0.85f), 0.2f);
		if (notePanel != null && isselected)
		{
			notePanel.Hide();
		}
		isselected = false;
	}

	public void SetWhite()
	{
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			img_bk.sprite = sprites[1];
		}
		img_black.gameObject.SetActive(value: false);
		img_bk.SetNativeSize();
		groupRect.DOScale(gameManager.IsAllDlc() ? new Vector3(0.92f, 0.92f, 0.92f) : Vector3.one, 0.2f);
		if (notePanel != null && !isselected)
		{
			notePanel.Show();
		}
		isselected = true;
		CenterOnItem(GetComponent<RectTransform>(), tabgroup.transform.parent.parent.GetComponent<ScrollRect>());
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isselected)
		{
			groupRect.DOKill();
			groupRect.DOScale(new Vector3(0.92f, 0.92f, 0.92f), 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isselected)
		{
			img_black.gameObject.SetActive(value: true);
			groupRect.DOKill();
			groupRect.DOScale(new Vector3(0.85f, 0.85f, 0.85f), 0.2f);
		}
	}

	public void UpdateName(string changename)
	{
		if (!haveRealName)
		{
			haveRealName = true;
			txt_name.GetComponent<Text>().text = "";
			txt_name.updateTranslation2(changename);
		}
	}

	public void UpdateAvatar()
	{
		if (!haveRealAvatar)
		{
			haveRealAvatar = true;
			if (Resources.Load<Sprite>("touxiang/" + photo + "ib") != null)
			{
				img_avatar.fillAmount = 0f;
				img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + photo + "ib");
				img_avatar.DOFillAmount(1f, 0.5f);
			}
		}
	}

	public void UpdateWhiteAvatar()
	{
		if (!haveWhiteAvatar)
		{
			haveWhiteAvatar = true;
			if (Resources.Load<Sprite>("touxiang/" + photo + "dead") != null)
			{
				img_avatar.fillAmount = 0f;
				img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + photo + "dead");
				img_avatar.DOFillAmount(1f, 0.5f);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}
}
