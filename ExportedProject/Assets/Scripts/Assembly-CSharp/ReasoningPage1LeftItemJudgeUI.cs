using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasoningPage1LeftItemJudgeUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public int id;

	public Image backgroundImage;

	public Text showText;

	public List<Sprite> backgroundSpriteList;

	public Image maskBg;

	public bool isEnable = true;

	public void Show()
	{
		DOTween.To(() => maskBg.GetComponent<RectTransform>().sizeDelta, delegate(Vector2 x)
		{
			maskBg.GetComponent<RectTransform>().sizeDelta = x;
		}, new Vector2(349f, 42f), 0.5f);
		DOTween.To(() => backgroundImage.color, delegate(Color x)
		{
			backgroundImage.color = x;
		}, Color.white, 0.5f);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isEnable && DragManager.instance.reasoningPage1ResultItem.id != -1)
		{
			ReasoningPage1UI.selectedReasoningPage1LeftItemJudgeUI = this;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isEnable && DragManager.instance.reasoningPage1ResultItem.id != -1)
		{
			ReasoningPage1UI.selectedReasoningPage1LeftItemJudgeUI = null;
		}
	}

	public void JudgeResult(int resultId)
	{
		if (resultId == id)
		{
			showText.gameObject.SetActive(value: true);
			backgroundImage.gameObject.SetActive(value: false);
			DLC7.Reasoning.ReasoningManager.Instance.ConfirmResult(resultId);
			DLC7.Reasoning.ReasoningManager.Instance.ResetResult(resultId);
			isEnable = false;
		}
		else
		{
			backgroundImage.sprite = backgroundSpriteList[1];
			StopCoroutine("ResetBackgounrdImage");
			StartCoroutine("ResetBackgounrdImage");
			DLC7.Reasoning.ReasoningManager.Instance.ResetResult(resultId);
		}
	}

	private IEnumerator ResetBackgounrdImage()
	{
		yield return new WaitForSeconds(0.1f);
		backgroundImage.sprite = backgroundSpriteList[0];
		yield return new WaitForSeconds(0.1f);
		backgroundImage.sprite = backgroundSpriteList[1];
		yield return new WaitForSeconds(0.1f);
		backgroundImage.sprite = backgroundSpriteList[0];
	}

	public void SetContent()
	{
		showText.gameObject.SetActive(value: false);
		backgroundImage.gameObject.SetActive(value: true);
	}
}
