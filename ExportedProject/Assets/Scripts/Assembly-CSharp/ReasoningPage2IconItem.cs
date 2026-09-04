using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasoningPage2IconItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Image bgImage;

	public int id;

	public Image errorImage;

	private bool selected;

	private float scale = 1.2f;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!ReasoningPage2UI.selectedReasoningPage2IconItems.Contains(this))
		{
			ReasoningPage2UI.selectedReasoningPage2IconItems.Add(this);
		}
		else
		{
			ReasoningPage2UI.selectedReasoningPage2IconItems.Remove(this);
		}
	}

	private void Start()
	{
	}

	public void ShowErrorTween()
	{
		StopCoroutine("ShowErrorTweenDetail");
		StartCoroutine("ShowErrorTweenDetail");
	}

	public IEnumerator ShowErrorTweenDetail()
	{
		yield return new WaitForSeconds(0.1f);
		errorImage.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.1f);
		errorImage.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.1f);
		errorImage.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.1f);
		errorImage.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (ReasoningPage2UI.selectedReasoningPage2IconItems.Contains(this))
		{
			selected = true;
			DOTween.To(() => bgImage.transform.localScale, delegate(Vector3 x)
			{
				bgImage.transform.localScale = x;
			}, new Vector3(scale, scale, 1f), 0.2f);
		}
		else
		{
			selected = false;
			DOTween.To(() => bgImage.transform.localScale, delegate(Vector3 x)
			{
				bgImage.transform.localScale = x;
			}, Vector3.one, 0.2f);
		}
	}
}
