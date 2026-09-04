using UnityEngine;
using UnityEngine.UI;

public class DiscussItem : MonoBehaviour
{
	public string tkid;

	public GameManager gameManager;

	public RectTransform txt_content;

	public Text txt_name;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void GoToTB()
	{
		if (!tkid.Equals("0"))
		{
			tkid.Equals("");
		}
	}

	public void SetNewWidth(string name)
	{
		float num = CalculateLengthOfText(name);
		txt_content.GetComponent<MultiplyText>().textBkg.GetComponent<RectTransform>().sizeDelta = new Vector2(325f - num, txt_content.sizeDelta.y);
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txt_name.font;
		font.RequestCharactersInTexture(message, txt_name.fontSize, txt_name.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt_name.fontSize);
			num += (float)info.advance;
		}
		return num;
	}
}
