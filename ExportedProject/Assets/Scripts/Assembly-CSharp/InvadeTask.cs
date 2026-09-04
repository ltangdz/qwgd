using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeTask : MonoBehaviour
{
	public Text txtval;

	public Text txtKey;

	public GameObject lightBak;

	public Sprite lightSprite;

	public GameObject choiceLeft;

	public Image leftIcon;

	public void Light()
	{
		leftIcon.sprite = lightSprite;
		lightBak.SetActive(value: true);
		txtval.GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + txtval.text + "</color>");
		txtKey.GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + txtKey.text + "</color>");
	}

	public void Complete()
	{
		leftIcon.gameObject.SetActive(value: false);
		choiceLeft.SetActive(value: true);
		lightBak.SetActive(value: false);
	}

	public void SetLabel(string info)
	{
		txtval.GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + info + "</color>");
	}
}
