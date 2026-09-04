using System.Collections;
using UnityEngine;

public class HackerBk : MonoBehaviour
{
	private GameManager gameManager;

	public HackerCountDown hackerCountDown;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.hackerBk = this;
	}

	public void Crash(bool isaddcountdown = true)
	{
		if (isaddcountdown)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load("Dialog/Hacker/hackcountdown") as GameObject, base.transform.parent);
			hackerCountDown = gameObject.GetComponent<HackerCountDown>();
		}
		StopAllCoroutines();
		StartCoroutine(StartAnimation());
	}

	private IEnumerator StartAnimation()
	{
		while (true)
		{
			float seconds = Random.Range(0.2f, 2f);
			AddNewUsefulwindow();
			yield return new WaitForSeconds(seconds);
		}
	}

	private void AddNewUsefulwindow()
	{
		int num = Random.Range(1, 4);
		Object.Instantiate(Resources.Load("Dialog/Hacker/usefulwindow" + num) as GameObject, base.transform);
	}

	public void DestroyAll()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).GetComponent<UnUsefulWindow>().HideWindow();
		}
		if (hackerCountDown != null)
		{
			hackerCountDown.Hide();
		}
	}

	public void DestroyAllLast()
	{
		StopAllCoroutines();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).GetComponent<UnUsefulWindow>().HideWindow();
		}
		if (hackerCountDown != null)
		{
			hackerCountDown.DestroyObject();
		}
	}

	public void Stop()
	{
		StopAllCoroutines();
	}
}
