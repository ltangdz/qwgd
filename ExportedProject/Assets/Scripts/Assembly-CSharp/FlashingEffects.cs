using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlashingEffects : MonoBehaviour
{
	public Image[] _images;

	public float interval = 3f;

	private void Start()
	{
		StartCoroutine(ShowLight());
	}

	private IEnumerator ShowLight()
	{
		int a = 0;
		while (true)
		{
			_images[a].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			_images[a].GetComponent<CanvasGroup>().alpha = 1f;
			_images[a].GetComponent<RectTransform>().DOScale(new Vector3(2f, 2f, 2f), interval);
			_images[a].GetComponent<CanvasGroup>().DOFade(0f, interval);
			a = ((a + 1 < _images.Length) ? (a + 1) : 0);
			yield return new WaitForSeconds(interval / (float)_images.Length);
		}
	}
}
