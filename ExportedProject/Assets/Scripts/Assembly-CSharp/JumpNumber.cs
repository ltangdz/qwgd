using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class JumpNumber : MonoBehaviour
{
	private int result;

	public int start = 1;

	public int end = 100;

	private int jumpTimes = 99;

	public Text label;

	private void Start()
	{
	}

	public IEnumerator JumpNumber2(float time)
	{
		int delta = (end - start) / jumpTimes;
		float pertime = time / 200f;
		result = 0;
		for (int i = 0; i < jumpTimes; i++)
		{
			result += delta;
			label.GetComponent<I18NText>().updateTranslation2(result.ToString());
			yield return new WaitForSeconds(pertime);
		}
		result = end;
		label.GetComponent<I18NText>().updateTranslation2(result.ToString());
		StopCoroutine(JumpNumber2(time));
	}

	public void StartJump(int end, float time)
	{
		this.end = end;
		jumpTimes = 200;
		StartCoroutine(JumpNumber2(time));
	}
}
