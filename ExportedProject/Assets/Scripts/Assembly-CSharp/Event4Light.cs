using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event4Light : MonoBehaviour
{
	public Image light;

	public Image personLight;

	public void Start()
	{
		StartCoroutine(ShowLight());
	}

	private IEnumerator ShowLight()
	{
		while (true)
		{
			int lightTime = Random.Range(1, 3);
			for (int i = 0; i <= lightTime; i++)
			{
				light.DOFade(0.7f, 0.05f);
				personLight.DOFade(0.7f, 0.05f);
				yield return new WaitForSeconds(0.05f);
				light.DOFade(1f, 0.05f);
				personLight.DOFade(1f, 0.05f);
				yield return new WaitForSeconds(0.05f);
			}
			int num = Random.Range(1, 4);
			yield return new WaitForSeconds(num);
		}
	}
}
