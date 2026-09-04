using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class GameTextItem : MonoBehaviour
{
	public Text text;

	public bool ischange = true;

	public string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public Color[] colors;

	public Material[] materials;

	private string s;

	private void Start()
	{
		int startIndex = Random.Range(0, str.Length);
		s = str.Substring(startIndex, 1);
		Restart();
		base.name = "start";
	}

	public string Stop()
	{
		ischange = false;
		CancelInvoke("Change");
		text.color = colors[1];
		text.material = materials[1];
		text.GetComponent<I18NText>().updateTranslation2(s);
		return s;
	}

	public void Restart()
	{
		ischange = true;
		text.color = colors[0];
		text.material = materials[0];
		InvokeRepeating("Change", 0.1f, 0.1f);
	}

	private void Update()
	{
	}

	private void Change()
	{
		if (ischange)
		{
			int startIndex = Random.Range(0, str.Length);
			text.GetComponent<I18NText>().updateTranslation2(str.Substring(startIndex, 1));
		}
	}
}
