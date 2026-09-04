using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SaoleiSucess : MonoBehaviour
{
	public CanvasGroup img_red0;

	public CanvasGroup img_greed0text;

	public Sprite greenSprite;

	public Sprite rightsprite;

	public Image img_icon;

	private void Start()
	{
		StartCoroutine(StartAni());
	}

	private IEnumerator StartAni()
	{
		img_red0.transform.DOScale(1f, 0.5f);
		img_red0.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(1f);
		img_red0.GetComponent<Image>().sprite = greenSprite;
		img_icon.sprite = rightsprite;
		yield return new WaitForSeconds(0.5f);
		img_icon.DOFade(0.6f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_icon.DOFade(1f, 0.1f);
		yield return new WaitForSeconds(0.2f);
		img_greed0text.transform.DOScale(1f, 0.5f);
		img_greed0text.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(2.5f);
		Object.Destroy(base.gameObject);
	}
}
