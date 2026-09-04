using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ChatFrdInfo : MonoBehaviour
{
	public Image headImg;

	public Text nameLabel;

	public Text sexLabel;

	public Text emailLabel;

	public Text mobileLabel;

	public Text toothLabel;

	public Text proLabel;

	public Text addressLabel;

	public void Reset(string headImgName, string name, string age, string email, string mobile, string tooth, string pro, string address)
	{
		headImg.sprite = Resources.Load<Sprite>("Chat/" + headImgName);
		nameLabel.GetComponent<I18NText>().updateTranslation2(name);
		sexLabel.GetComponent<I18NText>().updateTranslation2(age);
		emailLabel.GetComponent<I18NText>().updateTranslation2(email);
		mobileLabel.GetComponent<I18NText>().updateTranslation2(mobile);
		toothLabel.GetComponent<I18NText>().updateTranslation2(tooth);
		proLabel.GetComponent<I18NText>().updateTranslation2(pro);
		addressLabel.GetComponent<I18NText>().updateTranslation2(address);
	}

	public void HideBox()
	{
		base.gameObject.SetActive(value: false);
	}
}
