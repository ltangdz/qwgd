using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PasswordNumUI : MonoBehaviour
{
	public Text[] texts;

	public Color[] colors;

	public Text txt_password;

	public PasswordLightUI passwordlightui;

	public PasswordDialog2 passwordDialog2;

	public string goalnum = "";

	private string[] strings = new string[24]
	{
		"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
		"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
		"W", "X", "Y", "Z"
	};

	private bool isstart;

	public int totaltime;

	private int time;

	private int currenttime;

	private void Start()
	{
	}

	public void SetNum(string n)
	{
		goalnum = n;
		isstart = true;
	}

	public void SetAllGray()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].color = colors[0];
		}
	}

	private void Update()
	{
		time++;
		if (time >= totaltime && isstart)
		{
			isstart = false;
			txt_password.GetComponent<I18NText>().updateTranslation2(goalnum);
			txt_password.color = colors[1];
			int num = Random.Range(0, texts.Length);
			texts[num].GetComponent<I18NText>().updateTranslation2(goalnum);
			passwordlightui.isstart = false;
			passwordlightui.SetAllGray();
			passwordlightui.lights[num].sprite = passwordlightui.sprites[0];
			passwordDialog2.AddCount();
		}
		currenttime++;
		if (currenttime != 3 || !isstart)
		{
			return;
		}
		currenttime = 0;
		int num2 = Random.Range(0, texts.Length);
		for (int i = 0; i < texts.Length; i++)
		{
			int num3 = Random.Range(0, strings.Length);
			if (i == num2)
			{
				txt_password.GetComponent<I18NText>().updateTranslation2(strings[num3]);
			}
			texts[i].GetComponent<I18NText>().updateTranslation2(strings[num3]);
		}
	}
}
