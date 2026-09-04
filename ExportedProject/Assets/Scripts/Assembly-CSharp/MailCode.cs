using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MailCode : MonoBehaviour
{
	public string code;

	public Text txt_title;

	public Sprite sprite;

	public Image img_bk;

	public Color redcolor;

	public GameManager gameManager;

	public InputField input;

	public Button btn_sure;

	public DATA15 data15;

	private void Start()
	{
		btn_sure.onClick.AddListener(Sure);
	}

	public void Init(DATA15 item)
	{
		data15 = item;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		code = I18N.instance.getValue(gameManager.dataManager.dic1[item.reasoningID.Substring(1)].message);
	}

	public void Sure()
	{
		Debug.Log("邮件密码:" + code + "\n输入密码为：" + input.text.Trim());
		if (!code.Equals("") && input.text.Trim().Equals(code))
		{
			gameManager.homeScene.browserMail.OpenCodeMail(data15);
			gameManager.homeScene.browserMail.mailInfoBox.GetComponent<MailInfo>().OpenCodeMail();
		}
		else
		{
			img_bk.sprite = sprite;
			txt_title.color = redcolor;
			txt_title.GetComponent<I18NText>().updateTranslation2("^email_code02");
		}
	}

	public void CanSearch()
	{
		if (!gameManager.player.playerdata.itemlist.Contains("10231") && !gameManager.isbug)
		{
			gameManager.homeScene.ShowVideoTip("3700052");
			input.DeactivateInputField();
			input.text = "";
		}
		else
		{
			input.readOnly = false;
			input.ActivateInputField();
		}
	}
}
