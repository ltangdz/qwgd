using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private GameObject card0;

	[SerializeField]
	private GameObject card1;

	public bool isup = true;

	[SerializeField]
	private bool iscanclick = true;

	[SerializeField]
	private Image img_bkA;

	[SerializeField]
	private Image img_bkB;

	public Reasoning4009Step02 reasoning4009Step02;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private Color[] colors;

	public void StartRed()
	{
		StartCoroutine(StartRedAni());
	}

	private IEnumerator StartRedAni()
	{
		img_bkA.sprite = sprites[2];
		img_bkB.sprite = sprites[3];
		yield return new WaitForSeconds(0.2f);
		img_bkA.sprite = sprites[0];
		img_bkB.sprite = sprites[1];
		yield return new WaitForSeconds(0.2f);
		img_bkA.sprite = sprites[2];
		img_bkB.sprite = sprites[3];
		yield return new WaitForSeconds(0.2f);
		img_bkA.sprite = sprites[0];
		img_bkB.sprite = sprites[1];
		yield return new WaitForSeconds(0.2f);
		reasoning4009Step02.iscanclick = true;
	}

	public void Click()
	{
		if (!iscanclick)
		{
			return;
		}
		iscanclick = false;
		if (isup)
		{
			base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
			{
				card0.SetActive(value: true);
				card1.SetActive(value: false);
				base.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0.1f).OnComplete(delegate
				{
					iscanclick = true;
				});
			});
			isup = false;
			return;
		}
		base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
		{
			card0.SetActive(value: false);
			card1.SetActive(value: true);
			base.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).OnComplete(delegate
			{
				iscanclick = true;
			});
		});
		isup = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}
}
