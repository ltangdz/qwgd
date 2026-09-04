using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhonePasswordDialog : CustomDialog
{
	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private Text txt_tip;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private Text placeholder;

	[SerializeField]
	private Transform imgTip;

	[SerializeField]
	private Text lockContent;

	[SerializeField]
	private string pwkey;

	public InvadePhoneDialog invadePhoneDialog;

	[Header("是否是wifi密码设置")]
	public bool setWifi;

	public InvadeSettingWifi invadeSettingWifi;

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}

	public void Init(AppButton pwobj)
	{
		pwkey = pwobj.password;
		if (pwobj.titlekey.IndexOf("^") > -1)
		{
			placeholder.GetComponent<I18NText>().updateTranslation2(pwobj.titlekey);
		}
		else
		{
			placeholder.GetComponent<I18NText>().updateTranslation2("");
			imgTip.gameObject.SetActive(value: true);
			Sprite sprite = Resources.Load<Sprite>("InvadePhoneImage/" + pwobj.titlekey);
			imgTip.Find("Image").GetComponent<Image>().sprite = sprite;
		}
		if (pwobj.lockContent != "")
		{
			lockContent.GetComponent<I18NText>().updateTranslation2(pwobj.lockContent);
		}
	}

	private void Start()
	{
		btn_sure.onClick.AddListener(delegate
		{
			if (inputField.text.Trim().Equals(I18N.instance.getValue(pwkey)) || inputField.text.Trim().Equals(pwkey))
			{
				txt_tip.gameObject.SetActive(value: false);
				if (invadePhoneDialog != null && !setWifi)
				{
					invadePhoneDialog.ShowUnlock();
					Close();
				}
				else if (invadeSettingWifi != null && setWifi)
				{
					bk.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.3f);
					bk.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
					StartCoroutine(WifiSce());
				}
			}
			else
			{
				txt_tip.gameObject.SetActive(value: true);
				txt_tip.GetComponent<I18NText>().updateTranslation2("^invade_phone0204");
			}
		});
	}

	private IEnumerator WifiSce()
	{
		_ = (GameObject)Object.Instantiate(Resources.Load("InvadePhoneImage/invade_tishi04"), gameManager.homeScene.middle);
		yield return new WaitForSeconds(1.5f);
		invadeSettingWifi.LinkSce();
		Debug.Log("链接wifi成功");
		Close();
	}

	private void Update()
	{
		if (!Input.GetKeyUp(KeyCode.Return) && !Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			return;
		}
		if (inputField.text.Trim().Equals(I18N.instance.getValue(pwkey)) || inputField.text.Trim().Equals(pwkey))
		{
			txt_tip.gameObject.SetActive(value: false);
			if (invadePhoneDialog != null && !setWifi)
			{
				invadePhoneDialog.ShowUnlock();
				Close();
			}
			else if (invadeSettingWifi != null && setWifi)
			{
				bk.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.3f);
				bk.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
				StartCoroutine(WifiSce());
			}
		}
		else
		{
			txt_tip.gameObject.SetActive(value: true);
			txt_tip.GetComponent<I18NText>().updateTranslation2("^invade_phone0204");
		}
	}
}
