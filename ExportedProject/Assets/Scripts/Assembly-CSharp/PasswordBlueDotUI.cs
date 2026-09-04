using UnityEngine;
using UnityEngine.UI;

public class PasswordBlueDotUI : MonoBehaviour
{
	public Image[] dots;

	public Sprite[] sprites;

	private int time;

	private int pos;

	private void Start()
	{
	}

	private void Update()
	{
		time++;
		if (time == 30)
		{
			time = 0;
			dots[pos].sprite = sprites[0];
			pos = ((pos != dots.Length - 1) ? (pos + 1) : 0);
			dots[pos].sprite = sprites[1];
		}
	}
}
