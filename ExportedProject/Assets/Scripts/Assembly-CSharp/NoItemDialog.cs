using Honeti;
using UnityEngine.UI;

public class NoItemDialog : CustomDialog
{
	public Text txt_title;

	public Text txt_content;

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}

	public void Init(string title, string content)
	{
		txt_title.GetComponent<I18NText>().updateTranslation2(title);
		txt_content.GetComponent<I18NText>().updateTranslation2(content);
	}
}
