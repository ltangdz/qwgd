using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
	private Text txt_update;

	public float TextContentChangeTime = 0.2f;

	private bool isadd;

	private string old = "ABDJKSKFJLSDJIUOEU";

	private string str2 = "COMPUTER";

	private int pos = -1;

	private void Start()
	{
		txt_update = GetComponent<Text>();
		Change("THIS IS COMPUTER");
	}

	public void SetContent()
	{
	}

	private void ChangeText()
	{
		string text = "";
		if (isadd)
		{
			pos++;
		}
		for (int i = 0; i < str2.Length; i++)
		{
			if (pos < 0)
			{
				int startIndex = Random.Range(0, old.Length);
				text += old.Substring(startIndex, 1);
			}
			else if (i <= pos)
			{
				text += str2.Substring(i, 1);
			}
			else
			{
				int startIndex2 = Random.Range(0, old.Length);
				text += old.Substring(startIndex2, 1);
			}
		}
		txt_update.DOText(text, TextContentChangeTime);
		if (pos > str2.Length)
		{
			CancelInvoke();
		}
	}

	private void Update()
	{
	}

	public void Change(string content0)
	{
		str2 = content0;
		InvokeRepeating("ChangeText", 0.01f, 0.05f);
		Invoke("Show", 0.7f);
	}

	public void Show()
	{
		isadd = true;
	}
}
