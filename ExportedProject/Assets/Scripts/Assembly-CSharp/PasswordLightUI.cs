using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PasswordLightUI : MonoBehaviour
{
	public Image[] lights;

	public Sprite[] sprites;

	public float delay;

	private int time;

	public bool isstart = true;

	public int t;

	private int ttime;

	private int pos;

	private void Start()
	{
		pos = lights.Length - 1;
	}

	private void RandomLight()
	{
		int num = 1;
		for (int i = 0; i < num; i++)
		{
			int num2 = Random.Range(0, lights.Length);
			StartCoroutine(StartAnimation(num2));
		}
	}

	private IEnumerator StartAnimation(int pos)
	{
		lights[pos].sprite = sprites[0];
		yield return new WaitForSeconds(0.1f);
		lights[pos].sprite = sprites[1];
	}

	public void SetAllGray()
	{
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].sprite = sprites[1];
		}
	}

	private void Update()
	{
		time++;
		if (time == t && isstart)
		{
			RandomLight();
			time = 0;
		}
	}
}
