using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBrowser : MonoBehaviour
{
	public Image img_bk;

	public Image img_close;

	public Text txt_content;

	public Sprite[] sprites;

	public GameManager gameManager;

	public GameObject browserPanel;

	public string contentLabel;

	private bool isactive;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void InitButton(string content, GameObject panel)
	{
		browserPanel = panel;
		contentLabel = ((content.IndexOf(">") > -1) ? content.Split('>')[1] : content);
		txt_content.GetComponent<I18NText>().updateTranslation2(content);
	}

	public void SetShow(bool ia)
	{
		isactive = ia;
		img_bk.sprite = sprites[isactive ? 1 : 0];
	}

	public void Close()
	{
	}

	public void ClickPanel()
	{
		SetShow(ia: true);
	}
}
