using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCalling : MonoBehaviour
{
	public GameObject ripBox;

	public Image avatar;

	public Text userName;

	public Text phoneNum;

	public Text txtCalling;

	public Button hungDown;

	public GameObject btnNoClose;

	private string id;

	private PhoneCallDialog parObj;

	private GameManager gameManager;

	public string GetID => id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(string userID, PhoneCallDialog par, GameManager gm)
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
		StartCoroutine(RipRun());
		StartCoroutine(TextCalling());
	}

	public void StopRip(bool empty, bool video)
	{
		if (empty && video)
		{
			return;
		}
		if (!empty && !video)
		{
			base.gameObject.SetActive(value: false);
		}
		else if (empty)
		{
			txtCalling.GetComponent<I18NText>().updateTranslation2("^no_phone01");
		}
		else if (video)
		{
			txtCalling.GetComponent<I18NText>().updateTranslation2("^no_phone02");
			btnNoClose.gameObject.SetActive(value: false);
			hungDown.onClick.RemoveAllListeners();
			hungDown.onClick.AddListener(delegate
			{
				gameManager.CanShowSetting(-1);
				parObj.Hide();
			});
		}
		StopAllCoroutines();
	}

	private IEnumerator TextCalling()
	{
		int a = 0;
		string callLabel = I18N.instance.getValue("^calling");
		while (true)
		{
			a++;
			if (a <= 3)
			{
				callLabel += ".";
			}
			else
			{
				a = 0;
				callLabel = I18N.instance.getValue("^calling");
			}
			txtCalling.GetComponent<I18NText>().updateTranslation2(callLabel);
			yield return new WaitForSeconds(0.3f);
		}
	}

	private IEnumerator RipRun()
	{
		while (true)
		{
			Object.Instantiate(Resources.Load<GameObject>("img_rip"), ripBox.transform);
			yield return new WaitForSeconds(1f);
		}
	}
}
