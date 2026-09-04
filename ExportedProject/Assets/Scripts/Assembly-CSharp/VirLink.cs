using System.Collections;
using UnityEngine;

public class VirLink : CustomDialog
{
	public Transform loadBox;

	private bool running;

	private float loadStep;

	private void Start()
	{
		StartCoroutine(Pishing());
	}

	private IEnumerator Pishing()
	{
		float all = loadBox.childCount;
		float len = Mathf.Round(Random.Range(3, 5));
		for (int i = 1; (float)i <= len; i++)
		{
			float runVal = (((float)i == len) ? all : Mathf.Round(Random.Range(all / len * (float)(i - 1), all / len * (float)i)));
			StartCoroutine(RunTo(runVal));
			float seconds = Mathf.Round(Random.Range(1f, 3f));
			yield return new WaitForSeconds(seconds);
		}
	}

	private IEnumerator RunTo(float runVal)
	{
		for (int i = (int)loadStep; (float)i < runVal; i++)
		{
			loadBox.GetChild(i).gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.01f);
		}
		loadStep = runVal;
		if (runVal == (float)loadBox.childCount)
		{
			running = false;
			yield return new WaitForSeconds(1f);
			Hide();
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
