using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReasoningPage1LeftItem : MonoBehaviour
{
	public ReasoningPage1LeftItemJudgeUI judgeUI;

	public Image lineImage;

	public Image dotImage;

	public List<Sprite> dotSprite;

	public void EmptyContent()
	{
		judgeUI.SetContent();
	}

	public void ShowConfirmTween(UnityAction action)
	{
		dotImage.sprite = dotSprite[0];
		((Tween)DOTween.To(() => lineImage.fillAmount, delegate(float x)
		{
			lineImage.fillAmount = x;
		}, 1f, 0.5f)).OnComplete((TweenCallback)delegate
		{
			action?.Invoke();
		});
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		dotImage.sprite = dotSprite[1];
		judgeUI.Show();
	}
}
