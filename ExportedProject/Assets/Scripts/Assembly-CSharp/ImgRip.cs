using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImgRip : MonoBehaviour
{
	public float setSize;

	private void Start()
	{
		StartCoroutine(Rip());
	}

	private IEnumerator Rip()
	{
		base.transform.DOScaleX(setSize, 5f);
		base.transform.DOScaleY(setSize, 5f);
		GetComponent<Image>().DOFade(0f, 5f);
		yield return new WaitForSeconds(5f);
		Object.Destroy(base.gameObject);
	}
}
