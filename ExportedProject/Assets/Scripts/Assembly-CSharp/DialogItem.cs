using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogItem : MonoBehaviour
{
	public Image img_bk;

	public Sprite[] sprites;

	public IEnumerator StartRed()
	{
		img_bk.sprite = sprites[1];
		yield return new WaitForSeconds(0.3f);
		img_bk.sprite = sprites[0];
		yield return new WaitForSeconds(0.3f);
		img_bk.sprite = sprites[1];
		yield return new WaitForSeconds(0.3f);
		img_bk.sprite = sprites[0];
	}

	public void Red()
	{
		StopAllCoroutines();
		StartCoroutine(StartRed());
	}

	private void Start()
	{
	}
}
