using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AppFunBox : MonoBehaviour
{
	public Button bak;

	public Text appName;

	private PhishPhone phishPhone;

	private void Start()
	{
		bak.onClick.AddListener(CloseScene);
	}

	public void Reset(PhishPhone phish, string appNameInfo)
	{
		phishPhone = phish;
		phish.sceneCount++;
		appName.GetComponent<I18NText>().updateTranslation2(appNameInfo);
		phish.phoneSignal.sprite = phish.signal[1];
		phish.btn_close.GetComponent<Image>().sprite = phish.closeSprite[1];
		phish.introPhone.GetComponent<Text>().color = phish.titleColor[1];
		phish.titleInfoBox.transform.SetAsLastSibling();
	}

	private void CloseScene()
	{
		if (phishPhone.sceneCount == 1)
		{
			phishPhone.phoneSignal.sprite = phishPhone.signal[0];
			phishPhone.btn_close.GetComponent<Image>().sprite = phishPhone.closeSprite[0];
			phishPhone.introPhone.GetComponent<Text>().color = phishPhone.titleColor[0];
		}
		phishPhone.sceneCount--;
		Object.Destroy(base.gameObject);
	}
}
