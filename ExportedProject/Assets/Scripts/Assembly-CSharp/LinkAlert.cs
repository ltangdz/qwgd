using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LinkAlert : MonoBehaviour
{
	public Text label;

	public Button cancel;

	public Button sure;

	private void Start()
	{
		cancel.onClick.AddListener(CancelAlert);
	}

	public void Reset(string alertInfo, string sureBtn, string cancelBtn = "")
	{
		label.GetComponent<I18NText>().updateTranslation2(alertInfo);
		sure.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(sureBtn);
		if (cancelBtn.Trim() != "")
		{
			cancel.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(cancelBtn);
		}
		else
		{
			cancel.gameObject.SetActive(value: false);
		}
	}

	private void CancelAlert()
	{
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
