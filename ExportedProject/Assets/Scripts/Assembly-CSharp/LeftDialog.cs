using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeftDialog : MonoBehaviour
{
	public int type;

	public int avatar;

	public string content;

	public Image img_avatar;

	public TextMeshProUGUI txt_name;

	public NormalTypewriterEffect txt_content;

	public ContentSizeFitter sizeFitter;

	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void SetNewContent(string id, string t, bool isani, bool issave = true)
	{
		content = t;
		if (!id.Equals(""))
		{
			SetAward(id);
		}
		if (isani)
		{
			txt_content.StartEffect(t, isbkk: true);
			return;
		}
		txt_content.enabled = false;
		txt_content.GetComponent<TextMeshProUGUI>().text = t;
		int num = 300;
		if (txt_content.GetComponent<TextMeshProUGUI>().preferredWidth > (float)num)
		{
			txt_content.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = new Vector2(num, txt_content.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta.y);
			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		}
		else
		{
			txt_content.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = new Vector2(txt_content.GetComponent<TextMeshProUGUI>().preferredWidth, txt_content.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta.y);
			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
		}
	}

	private void SetAward(string id)
	{
		if (!id.Equals("50100000") && !id.Equals("0"))
		{
			avatar = int.Parse(id);
			Sprite sprite = Resources.Load<Sprite>("wechat/avatar/" + id);
			img_avatar.sprite = sprite;
		}
	}
}
