using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiplyTextRedImage : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public string itemid;

	public int pos;

	public MultiplyTextRedImage linkImage;

	public MultiplyText multiplyText;

	public Image img_red;

	public string content;

	public bool isshow;

	public GameManager gameManager;

	public Canvas img_click;

	public void SetWhite()
	{
		img_red.color = Color.white;
		if (linkImage != null)
		{
			linkImage.img_red.color = Color.white;
		}
	}

	public void SetCanvasOrder()
	{
		if (gameManager.player.GetEventId().Equals("110000") && gameManager.player.playerdata.isCourse03 == 0)
		{
			if (itemid.Equals("10057"))
			{
				gameManager.homeScene.courseManager.coursepanel03.nameredimage = this;
			}
			else if (itemid.Equals("10068"))
			{
				gameManager.homeScene.courseManager.coursepanel03.tbnicknameredimage = this;
			}
		}
	}

	private void Start()
	{
		SetCanvasOrder();
	}

	public void ShowHadItem()
	{
		img_red.fillAmount = 1f;
	}

	public void Exit()
	{
		if (!multiplyText.ishad && gameManager.iscancollect && !multiplyText.isshow)
		{
			img_red.DOKill();
			isshow = false;
			img_red.fillAmount = 0f;
			multiplyText.CancelClickExit(this);
			if (linkImage != null)
			{
				linkImage.img_red.DOKill();
				linkImage.img_red.fillAmount = 0f;
				multiplyText.CancelClickExit(linkImage);
			}
		}
	}

	public void Enter()
	{
		if (multiplyText.ishad || !gameManager.iscancollect)
		{
			return;
		}
		isshow = true;
		if (!multiplyText.iscanaddtoitem)
		{
			return;
		}
		img_red.color = new Color(1f, 1f, 1f, gameManager.isshowredline ? 1 : 0);
		if (linkImage != null)
		{
			linkImage.img_red.color = new Color(1f, 1f, 1f, gameManager.isshowredline ? 1 : 0);
		}
		if (pos == 0)
		{
			if (multiplyText.ishad)
			{
				return;
			}
			if (gameManager.isshowredline)
			{
				img_red.DOFillAmount(1f, 0.1f).SetEase(Ease.InOutCirc).OnComplete(delegate
				{
					if (linkImage != null)
					{
						linkImage.StartRedEnter();
					}
					else if (multiplyText != null)
					{
						multiplyText.ClickEnter(this);
					}
				});
			}
			else
			{
				img_red.fillAmount = 1f;
				if (linkImage != null)
				{
					linkImage.img_red.fillAmount = 1f;
				}
			}
		}
		else if (pos == 1)
		{
			linkImage.Enter();
		}
	}

	public void Select()
	{
		if (multiplyText.ishad)
		{
			return;
		}
		isshow = true;
		if (!multiplyText.iscanaddtoitem)
		{
			return;
		}
		img_red.color = Color.white;
		if (pos == 0)
		{
			if (!multiplyText.ishad)
			{
				img_red.fillAmount = 1f;
				if (linkImage != null)
				{
					StartSelectRed();
				}
				else if (multiplyText != null)
				{
					multiplyText.ClickEnter(this);
				}
			}
		}
		else if (pos == 1)
		{
			linkImage.Select();
		}
	}

	public void Selected()
	{
		isshow = true;
		img_red.color = Color.white;
		if (pos == 0)
		{
			img_red.fillAmount = 1f;
			if (multiplyText != null)
			{
				multiplyText.ClickEnter(this, isselect: true);
			}
			if (linkImage != null)
			{
				linkImage.img_red.fillAmount = 1f;
				if (multiplyText != null)
				{
					multiplyText.ClickEnter(linkImage, isselect: true);
				}
			}
		}
		else if (pos == 1)
		{
			linkImage.Selected();
		}
	}

	public void Click()
	{
		if (multiplyText.iscanaddtoitem && gameManager.iscancollect && multiplyText != null)
		{
			multiplyText.Click(this);
		}
	}

	public void CancelClick()
	{
		if (img_red.fillAmount == 1f)
		{
			isshow = false;
			img_red.fillAmount = 0f;
			if (linkImage != null)
			{
				linkImage.img_red.fillAmount = 0f;
			}
		}
	}

	public void StartRed()
	{
		img_red.DOFillAmount(1f, 0.1f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			if (multiplyText != null)
			{
				multiplyText.Click(this);
			}
		});
	}

	public void StartRedEnter()
	{
		img_red.DOFillAmount(1f, 0.1f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			if (multiplyText != null)
			{
				multiplyText.ClickEnter(this);
			}
		});
	}

	public void StartSelectRed()
	{
		img_red.fillAmount = 1f;
		if (multiplyText != null)
		{
			multiplyText.ClickEnter(this);
		}
	}

	public void Init(string id, string content)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		itemid = id;
		this.content = content;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.itemlist.Contains(id) || gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			Selected();
		}
		else
		{
			img_red.color = new Color(1f, 1f, 1f, gameManager.isshowredline ? 1 : 0);
		}
	}

	public void Init(string id, string content, int pos)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		itemid = id;
		this.content = content;
		this.pos = pos;
		if (!gameManager.player.playerdata.itemlist.Contains(id) && !gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			img_red.color = new Color(1f, 1f, 1f, gameManager.isshowredline ? 1 : 0);
		}
	}

	public void SetLinkImage(MultiplyTextRedImage linkImage)
	{
		this.linkImage = linkImage;
	}

	public void SetMultiplyText(MultiplyText multiplyText)
	{
		this.multiplyText = multiplyText;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Enter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Exit();
	}

	private void CanClick()
	{
		gameManager.iscancollect = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (img_red.fillAmount == 1f)
		{
			Click();
		}
	}
}
