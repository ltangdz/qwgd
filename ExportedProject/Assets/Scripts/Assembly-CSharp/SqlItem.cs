using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SqlItem : MonoBehaviour
{
	public Text txt_name;

	public Text txt_birth;

	public Text txt_gender;

	public Text txt_tel;

	public Text txt_address;

	public Text txt_email;

	public Text txt_idnumber;

	public Text txt_hobby;

	public string[] itemids;

	public Image img_highlight;

	public GameObject buttons;

	public Button btn_add;

	public Button btn_sign;

	public Image img_notclick;

	private GameManager gameManager;

	public bool iscanclick;

	private bool iscancancle;

	private bool ishascollect;

	private bool ishasclick;

	private void Start()
	{
		btn_add.onClick.AddListener(delegate
		{
			ishascollect = true;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			img_notclick.gameObject.SetActive(value: false);
			int num = int.Parse(gameManager.dataManager.dic1[itemids[0]].role.Substring(1));
			if (num >= 3100036 && num <= 3100047)
			{
				gameManager.homeScene.zhibojiannotebook.gameObject.SetActive(value: true);
				gameManager.homeScene.zhibojiannotebook.AddNewItems(itemids);
			}
			else
			{
				gameManager.homeScene.notebook.gameObject.SetActive(value: true);
				gameManager.homeScene.notebook.AddNewItems(itemids);
			}
			btn_add.interactable = false;
			CloseButton();
			if (gameManager.player.playerdata.isCourse12 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel12.HideCourse();
			}
		});
	}

	public void InitContent(string[] content)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_name.GetComponent<I18NText>().updateTranslation2(content[0]);
		txt_birth.GetComponent<I18NText>().updateTranslation2(content[1]);
		txt_gender.GetComponent<I18NText>().updateTranslation2(content[2]);
		txt_tel.GetComponent<I18NText>().updateTranslation2(content[3]);
		if (content[4].Equals("null"))
		{
			txt_address.text = "null";
		}
		else
		{
			txt_address.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(content[4]));
		}
		if (content[5].Length > 10)
		{
			string key = content[5].Substring(0, 10) + "...";
			txt_email.GetComponent<I18NText>().updateTranslation2(key);
		}
		else
		{
			txt_email.GetComponent<I18NText>().updateTranslation2(content[5]);
		}
		txt_idnumber.GetComponent<I18NText>().updateTranslation2(content[6]);
		txt_hobby.GetComponent<I18NText>().updateTranslation2(content[7]);
		if (content[8].Equals("0"))
		{
			iscanclick = false;
			return;
		}
		itemids = content[8].Split(';');
		bool flag = true;
		for (int i = 0; i < itemids.Length; i++)
		{
			if (!gameManager.player.playerdata.itemlist.Contains(itemids[i]))
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			btn_add.interactable = false;
			ishascollect = true;
			img_highlight.gameObject.SetActive(value: true);
			img_highlight.color = Color.white;
			img_highlight.fillAmount = 1f;
		}
		else
		{
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^highlighttip01");
		}
		iscanclick = true;
	}

	public void Click(bool isshowbutton)
	{
		if (iscanclick)
		{
			ishasclick = true;
			if ((bool)gameManager.homeScene.sqlDialog)
			{
				gameManager.homeScene.sqlDialog.transform.SetAsLastSibling();
			}
			buttons.GetComponent<RectTransform>().anchoredPosition = GetPosition(buttons.GetComponent<RectTransform>());
			if (isshowbutton)
			{
				StartRed();
			}
			else
			{
				img_highlight.fillAmount = 1f;
			}
		}
	}

	private Vector3 GetPosition(RectTransform btnRectTrans)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(btnRectTrans.transform.parent as RectTransform, Input.mousePosition, Camera.main, out var localPoint);
		Vector2 vector = new Vector2(btnRectTrans.rect.width * 0f, btnRectTrans.rect.height * 0f);
		return localPoint + vector;
	}

	public void CancelClick()
	{
		img_notclick.gameObject.SetActive(value: false);
		iscanclick = true;
		ishasclick = false;
		CloseButton();
		if (!gameManager.player.playerdata.ContainItemList(itemids))
		{
			img_highlight.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
			img_highlight.fillAmount = 0f;
		}
	}

	private void StartRed()
	{
		buttons.SetActive(value: true);
		img_notclick.gameObject.SetActive(value: true);
		StartButton();
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

	public void Enter()
	{
		if (!ishascollect)
		{
			img_highlight.gameObject.SetActive(value: true);
			if (gameManager.isshowredline)
			{
				img_highlight.color = Color.white;
			}
			else
			{
				img_highlight.color = new Color(1f, 1f, 1f, 0f);
			}
			img_highlight.DOFillAmount(1f, 0.2f).SetEase(Ease.InOutCirc).OnComplete(delegate
			{
			});
		}
	}

	public void Exit()
	{
		if (!ishasclick && !ishascollect)
		{
			img_highlight.DOKill();
			img_highlight.fillAmount = 0f;
		}
	}
}
