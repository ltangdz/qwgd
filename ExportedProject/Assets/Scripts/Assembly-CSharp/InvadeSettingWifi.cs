using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeSettingWifi : MonoBehaviour
{
	public Text linkType;

	public List<GameObject> canLink;

	public List<GameObject> noLink;

	private GameManager gameManager;

	private bool linked;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.homeScene.invadePhoneDialog.isWifi)
		{
			LinkSce();
		}
		for (int i = 0; i < canLink.Count; i++)
		{
			canLink[i].GetComponent<Button>().onClick.AddListener(ShowSettingPanel);
		}
		for (int j = 0; j < noLink.Count; j++)
		{
			noLink[j].GetComponent<Button>().onClick.AddListener(NoSetting);
		}
	}

	public void LinkSce()
	{
		linkType.GetComponent<I18NText>().updateTranslation2("^invadesetting_label06");
		linked = true;
		gameManager.homeScene.invadePhoneDialog.isWifi = true;
	}

	private void ShowSettingPanel()
	{
		if (!linked)
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/phonewifiDialog"), gameManager.homeScene.middle);
			obj.GetComponent<PhonePasswordDialog>().Show();
			obj.GetComponent<PhonePasswordDialog>().invadeSettingWifi = this;
		}
	}

	private void NoSetting()
	{
		if (!linked)
		{
			Object.Instantiate(Resources.Load<GameObject>("InvadePhoneImage/invade_tishi02"), gameManager.homeScene.middle);
		}
	}

	private void Update()
	{
	}
}
