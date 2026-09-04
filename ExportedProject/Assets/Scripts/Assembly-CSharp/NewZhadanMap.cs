using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NewZhadanMap : MonoBehaviour
{
	public Image red;

	public RectTransform map;

	private void Start()
	{
		FreshType();
	}

	private IEnumerator NoBoom()
	{
		while (true)
		{
			red.DOFade(0.3f, 1f);
			yield return new WaitForSeconds(0.5f);
			red.DOFade(0.1f, 1f);
			yield return new WaitForSeconds(1f);
		}
	}

	public void FreshType()
	{
		red.gameObject.SetActive(value: true);
		StartCoroutine(NoBoom());
	}

	public void HaveBoom()
	{
		StopAllCoroutines();
		red.gameObject.SetActive(value: false);
	}
}
