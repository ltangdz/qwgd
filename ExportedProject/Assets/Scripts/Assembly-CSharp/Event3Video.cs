using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event3Video : MonoBehaviour
{
	public GameObject black;

	public List<GameObject> fireLight;

	public GameObject white;

	public GameObject deadbox;

	public GameObject dead;

	public GameObject parobj;

	public GameObject light01;

	public GameObject light02;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		StartCoroutine(FireLight());
		StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		yield return new WaitForSeconds(1f);
		gameManager.musicManager.Stop();
		yield return new WaitForSeconds(2f);
		black.GetComponent<CanvasGroup>().DOFade(0f, 3f);
		yield return new WaitForSeconds(1f);
		gameManager.musicManager.PlayAnimationSound(2);
		yield return new WaitForSeconds(3.5f);
		GetComponent<Animator>().Play("ani_event3fire");
		yield return new WaitForSeconds(2.8f);
		GetComponent<RectTransform>().DOScale(new Vector3(2.2f, 2.2f, 2.2f), 3f);
		yield return new WaitForSeconds(0.2f);
		deadbox.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		GetComponent<CanvasGroup>().alpha = 0f;
		light01.SetActive(value: false);
		light02.SetActive(value: true);
		dead.GetComponent<Image>().enabled = true;
		white.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(4.5f);
		black.SetActive(value: true);
		black.GetComponent<Image>().DOFade(1f, 2.5f);
	}

	private IEnumerator FireLight()
	{
		while (true)
		{
			float num = Random.Range(20f, 30f) * 0.01f;
			for (int i = 0; i < fireLight.Count; i++)
			{
				float endValue = (float)i * 0.1f + num;
				fireLight[i].GetComponent<Image>().DOFade(endValue, 0.18f);
			}
			yield return new WaitForSeconds(0.18f);
			float num2 = Random.Range(10f, 20f) * 0.01f;
			for (int j = 0; j < fireLight.Count; j++)
			{
				float endValue2 = (float)j * 0.1f + num2;
				fireLight[j].GetComponent<Image>().DOFade(endValue2, 0.15f);
			}
			yield return new WaitForSeconds(0.15f);
		}
	}
}
