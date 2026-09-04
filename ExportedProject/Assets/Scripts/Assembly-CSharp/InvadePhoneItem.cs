using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadePhoneItem : MonoBehaviour
{
	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Text txt_percent;

	[SerializeField]
	private Image img_percent;

	private bool isupdatepercent;

	[SerializeField]
	private Color redcolor;

	private GameManager gameManager;

	public InvadePhoneDialog invadePhoneDialog;

	[SerializeField]
	private Color graycolor;

	private int pos;

	public bool secMenu;

	public void StartAnimation(string contentkey, float percent = 1f)
	{
		txt_content.GetComponent<I18NText>().updateTranslation2(contentkey);
		isupdatepercent = true;
		img_percent.DOFade(percent, 0.5f).OnComplete(delegate
		{
			isupdatepercent = false;
			txt_percent.text = ((percent == 1f) ? "100%" : "0%");
		});
		if (percent == 0f)
		{
			txt_content.color = redcolor;
			txt_percent.color = redcolor;
		}
	}

	private void Update()
	{
		if (isupdatepercent)
		{
			txt_percent.text = (int)(img_percent.color.a * 100f) + "%";
		}
	}

	public void InitItem(AppButton btnObj, string contentkey, string browserpath, int pos, string btnName = "", bool freshReadType = true)
	{
		this.pos = pos;
		txt_content.GetComponent<I18NText>().updateTranslation2(contentkey);
		if (btnName != "")
		{
			txt_percent.GetComponent<I18NText>().updateTranslation2(btnName);
		}
		if (browserpath.Equals("0"))
		{
			GetComponent<Button>().interactable = false;
			txt_percent.GetComponent<I18NText>().updateTranslation2("^invade_phone04");
			return;
		}
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			Debug.Log(browserpath);
			if (!secMenu)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("InvadePhoneImage/" + browserpath), gameManager.homeScene.invadePhoneDialog.transform);
				if (gameObject.GetComponent<ReasonPic>() != null)
				{
					gameObject.GetComponent<ReasonPic>().Show();
				}
				invadePhoneDialog.littlewindow = gameObject;
				SetGray();
			}
			else
			{
				for (int i = 0; i < base.transform.parent.childCount; i++)
				{
					Object.Destroy(base.transform.parent.GetChild(i).gameObject);
				}
				_ = (GameObject)Object.Instantiate(Resources.Load("InvadePhoneImage/" + browserpath), base.transform.parent);
			}
			if (freshReadType)
			{
				btnObj.readTypes[this.pos] = 1;
			}
		});
	}

	public void SetGray()
	{
		for (int i = 1; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).GetComponent<Text>().color = graycolor;
		}
	}
}
