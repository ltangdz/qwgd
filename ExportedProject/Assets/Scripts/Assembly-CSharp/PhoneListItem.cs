using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhoneListItem : MonoBehaviour
{
	public Image avatar;

	public Text userName;

	public Text phoneNum;

	public Button callBtn;

	public Image callListBtn;

	public Sprite calledSprite;

	[HideInInspector]
	public PhoneNumList parObj;

	private string id;

	private GameManager gameManager;

	private bool isCalling;

	private Image _bgImage;

	private int callType;

	public string getID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	private void Start()
	{
		_bgImage = GetComponent<Image>();
	}

	public void Init(string userID, PhoneNumList par, GameManager gm)
	{
		id = userID;
		parObj = par;
		gameManager = gm;
		string head = gameManager.dataManager.dic37[userID].head;
		string key = gameManager.dataManager.dic37[userID].name;
		string phone = gameManager.dataManager.dic37[userID].phone;
		if (gameManager.GameType == GameTypeEnum.BASIC)
		{
			avatar.sprite = Resources.Load<Sprite>("phone/" + head);
		}
		userName.GetComponent<I18NText>().updateTranslation2(key);
		phoneNum.GetComponent<I18NText>().updateTranslation2(phone);
		Debug.Log("包含的id：" + id + ":" + gameManager.player.playerdata.phoneCall.Contains(id));
		if (gameManager.player.playerdata.phoneCall.Contains(id))
		{
			callType = 1;
			callListBtn.GetComponent<Image>().sprite = calledSprite;
		}
		GetComponent<Image>().type = Image.Type.Sliced;
		callBtn.onClick.AddListener(delegate
		{
			Click(isshow: true);
		});
	}

	public void Click(bool isshow)
	{
		if (!isCalling)
		{
			isCalling = true;
			if (callType == 0)
			{
				parObj.parObj.ShowCourse();
			}
			parObj.parObj.PhoneCalling(id, callType);
		}
	}
}
