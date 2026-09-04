using UnityEngine;
using UnityEngine.UI;

public class MailList : MonoBehaviour
{
	public Text mailName;

	public Text mailTitle;

	public Text mailTime;

	public Text mailInfo;

	public Color graycolor;

	public GameObject newIcon;

	public string id;

	private int read;

	private string mail;

	private int mailType;

	private GameManager gameManager;

	public string getID => id;

	private void Start()
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
	}

	public void ResetList(string listId, string listMailName, string listMailTitle, string listMailTime, int haveRead, string listMailInfo, string mailAddress, int type)
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		mailType = type;
		id = listId;
		if (haveRead == 1 || haveRead == 2)
		{
			base.transform.Find("img_mailBlur").gameObject.SetActive(value: true);
			mailName.color = graycolor;
		}
		else
		{
			base.transform.Find("img_mail").gameObject.SetActive(value: true);
			newIcon.SetActive(value: true);
		}
		string value = ((listMailName.Split('(')[0].Length > 16) ? (listMailName.Split('(')[0].Substring(0, 16) + "...") : listMailName.Split('(')[0]);
		if (gameManager.GameType == GameTypeEnum.DLC6 && string.IsNullOrEmpty(value))
		{
			value = gameManager.player.playerdata.nickname;
		}
		GameManager.SetTextWithEllipsis(mailName, value);
		GameManager.SetTextWithEllipsis(mailTime, listMailTime);
		GameManager.SetTextWithEllipsis(mailTitle, listMailTitle);
		GameManager.SetTextWithEllipsis(mailInfo, listMailInfo);
		mail = mailAddress;
	}

	public void Blur()
	{
		base.transform.Find("img_choice").GetComponent<CanvasGroup>().alpha = 0f;
		mailName.color = new Color(0.4f, 0.5f, 0.8f, 1f);
		mailTitle.color = new Color(0.45f, 0.45f, 0.45f, 1f);
		mailTime.color = new Color(0.67f, 0.67f, 0.67f, 1f);
		mailInfo.color = new Color(0.67f, 0.67f, 0.67f, 1f);
		JudgeRead();
	}

	public void Focus()
	{
		GetComponent<HoverColorChange>().KillEnterObj();
		base.transform.Find("img_choice").GetComponent<CanvasGroup>().alpha = 1f;
		mailName.color = new Color(1f, 1f, 1f, 1f);
		mailTitle.color = new Color(1f, 1f, 1f, 1f);
		mailTime.color = new Color(1f, 1f, 1f, 1f);
		mailInfo.color = new Color(1f, 1f, 1f, 1f);
		base.transform.Find("img_mailFocus").gameObject.SetActive(value: true);
		base.transform.Find("img_mailBlur").gameObject.SetActive(value: false);
		base.transform.Find("img_mail").gameObject.SetActive(value: false);
		newIcon.SetActive(value: false);
	}

	public void Read()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.maillist[mail][0][id] == 1)
		{
			base.transform.Find("img_mailFocus").gameObject.SetActive(value: true);
			base.transform.Find("img_mail").gameObject.SetActive(value: false);
			newIcon.SetActive(value: false);
		}
	}

	private void JudgeRead()
	{
		if (!newIcon.gameObject.activeInHierarchy)
		{
			base.transform.Find("img_mailBlur").gameObject.SetActive(value: true);
			mailName.color = graycolor;
			base.transform.Find("img_mail").gameObject.SetActive(value: false);
			base.transform.Find("img_mailFocus").gameObject.SetActive(value: false);
		}
	}
}
