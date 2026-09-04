using Honeti;
using UnityEngine;

public class ResultItem : MonoBehaviour
{
	public I18NText txt_title;

	public I18NText txt_content;

	public void Init(string title, string content)
	{
		txt_title.updateTranslation2(title);
		txt_content.updateTranslation2(content);
	}

	private void Start()
	{
	}
}
