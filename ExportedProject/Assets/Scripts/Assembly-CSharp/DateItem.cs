using UnityEngine;
using UnityEngine.UI;

public class DateItem : MonoBehaviour
{
	public int pos;

	private Text txt;

	public Color[] colors;

	private void Start()
	{
		txt = GetComponent<Text>();
	}

	public void SetBlue()
	{
		txt.color = colors[1];
		txt.fontSize = 24;
	}

	public void SetGray()
	{
		txt.color = colors[0];
		txt.fontSize = 22;
	}
}
