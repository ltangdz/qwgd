using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
	public Image img_fill;

	public int percent;

	private float fillv = 777f;

	public float offv;

	public Text txt_percent;

	private float fenfillv;

	private GameManager gameManager;

	private float _height;

	public int topercent;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Vector2 sizeDelta = GetComponent<RectTransform>().sizeDelta;
		fillv = sizeDelta.x + offv;
		_height = sizeDelta.y;
		fenfillv = fillv / 100f;
	}

	public void SetPercent(int c)
	{
		topercent = c;
		InvokeRepeating("StartPercent", 0.01f, 0.008f);
	}

	private void StartPercent()
	{
		bool flag = ((percent < topercent) ? true : false);
		if (percent == topercent)
		{
			if (percent >= 100)
			{
				img_fill.GetComponent<RectTransform>().sizeDelta = new Vector2(fillv, gameManager.Is_Dlc7() ? _height : img_fill.GetComponent<RectTransform>().sizeDelta.y);
			}
			CancelInvoke();
			return;
		}
		if (flag)
		{
			percent++;
		}
		else
		{
			percent--;
		}
		Vector2 sizeDelta = img_fill.GetComponent<RectTransform>().sizeDelta;
		img_fill.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x + (flag ? fenfillv : (0f - fenfillv)), gameManager.Is_Dlc7() ? _height : sizeDelta.y);
		txt_percent.GetComponent<I18NText>().updateTranslation2(percent + " %");
	}
}
