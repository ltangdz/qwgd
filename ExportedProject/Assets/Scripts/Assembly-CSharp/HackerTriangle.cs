using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerTriangle : MonoBehaviour
{
	[SerializeField]
	private Text txt_status;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Image img_trigle;

	[SerializeField]
	private Image img_circle;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	public List<HackerItem> hackerItems = new List<HackerItem>();

	[SerializeField]
	private string result;

	[SerializeField]
	private List<string> correntanswer = new List<string>();

	public bool iswending;

	private void Start()
	{
		check();
	}

	public bool check()
	{
		result = "";
		for (int i = 0; i < hackerItems.Count; i++)
		{
			result += hackerItems[i].type;
		}
		for (int j = 0; j < correntanswer.Count; j++)
		{
			if (result.Equals(correntanswer[j]))
			{
				img_bk.sprite = sprites[0];
				img_trigle.sprite = sprites[1];
				img_circle.sprite = sprites[2];
				txt_status.GetComponent<I18NText>().updateTranslation2("^hacker09");
				iswending = true;
				return true;
			}
		}
		img_bk.sprite = sprites[3];
		img_trigle.sprite = sprites[4];
		img_circle.sprite = sprites[5];
		txt_status.GetComponent<I18NText>().updateTranslation2("^hacker08");
		iswending = false;
		return false;
	}
}
