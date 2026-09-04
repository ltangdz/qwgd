using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class MailLink : MonoBehaviour
{
	private void Start()
	{
		SetURLUnderLine();
	}

	public void Reset(string label)
	{
		base.transform.GetComponent<I18NText>().updateTranslation2(label);
	}

	private void SetURLUnderLine()
	{
		float preferredWidth = base.transform.GetComponent<Text>().preferredWidth;
		Vector2 sizeDelta = base.transform.Find("img_line").GetComponent<RectTransform>().sizeDelta;
		base.transform.Find("img_line").GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, sizeDelta.y);
	}
}
