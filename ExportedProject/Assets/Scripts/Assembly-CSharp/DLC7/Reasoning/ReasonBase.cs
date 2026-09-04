using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class ReasonBase : MonoBehaviour
	{
		protected void ImageAnimation(bool isSuccess, bool isSelected, Image image, List<Sprite> list, UnityAction callback)
		{
			if (isSuccess)
			{
				if (isSelected)
				{
					image.sprite = list[1];
				}
				return;
			}
			image.sprite = list[2];
			Sequence sequence = DOTween.Sequence();
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				image.sprite = list[0];
				if (callback != null)
				{
					callback();
				}
			});
			sequence.Play();
		}

		protected void TextAnimation(bool isSuccess, bool isSelected, Text text, List<Color> list, UnityAction callback)
		{
			if (isSuccess)
			{
				if (isSelected)
				{
					text.color = list[1];
				}
				return;
			}
			text.color = list[2];
			Sequence sequence = DOTween.Sequence();
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				text.color = list[0];
				if (callback != null)
				{
					callback();
				}
			});
			sequence.Play();
		}
	}
}
