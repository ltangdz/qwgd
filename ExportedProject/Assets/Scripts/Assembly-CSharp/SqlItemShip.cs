using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SqlItemShip : MonoBehaviour
{
	public Text txt_name;

	public Text txt_gender;

	public Text txt_birth;

	public Text txt_idnumber;

	public Text txt_tel;

	public string[] itemids;

	public Image img_highlight;

	public GameObject buttons;

	public Button btn_add;

	public Button btn_sign;

	public Image img_notclick;

	public HomeScene homeScene;

	private GameManager gameManager;

	public bool iscanclick;

	private bool iscancancle;

	private bool ishascollect;

	private bool ishasclick;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		homeScene = gameManager.homeScene;
		btn_add.onClick.AddListener(delegate
		{
			ishascollect = true;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			img_notclick.gameObject.SetActive(value: false);
			homeScene.notebook.gameObject.SetActive(value: true);
			homeScene.notebook.AddNewItems(itemids);
			btn_add.interactable = false;
			CloseButton();
			if (gameManager.player.playerdata.isCourse12 == 0)
			{
				homeScene.courseManager.coursepanel12.HideCourse();
			}
		});
	}

	public void InitContent(string[] content)
	{
		txt_name.GetComponent<I18NText>().updateTranslation2(content[0]);
		txt_gender.GetComponent<I18NText>().updateTranslation2(content[1]);
		txt_birth.GetComponent<I18NText>().updateTranslation2(content[2]);
		txt_idnumber.GetComponent<I18NText>().updateTranslation2(content[3]);
		txt_tel.GetComponent<I18NText>().updateTranslation2(content[4]);
		if (content[5].Equals("0"))
		{
			iscanclick = false;
			return;
		}
		itemids = content[5].Split(';');
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
			if ((bool)homeScene.sqlDialog)
			{
				homeScene.sqlDialog.transform.SetAsLastSibling();
			}
			Vector2 localPoint = Vector2.one;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(homeScene.GetComponent<Canvas>().transform as RectTransform, Input.mousePosition, homeScene.GetComponent<Canvas>().worldCamera, out localPoint);
			buttons.GetComponent<RectTransform>().position = new Vector3(localPoint.x, localPoint.y, 0f);
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
			img_highlight.color = Color.white;
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
