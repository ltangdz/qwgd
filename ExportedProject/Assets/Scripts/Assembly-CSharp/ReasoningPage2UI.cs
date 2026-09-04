using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningPage2UI : MonoBehaviour
{
	public ToggleQuestion toggleQuestion;

	public Button nextBtn1;

	public Button nextBtn2;

	public CanvasGroup nextCg;

	public static List<ReasoningPage2IconItem> selectedReasoningPage2IconItems = new List<ReasoningPage2IconItem>();

	public List<ReasoningPage2IconItem> iconItemList;

	public ContentSizeFitter contentSizeFitter1;

	public ContentSizeFitter contentSizeFitter2;

	public IEnumerator ResetContentSizeFitter(ContentSizeFitter contentSizeFitter)
	{
		contentSizeFitter.enabled = false;
		yield return new WaitForEndOfFrame();
		contentSizeFitter.enabled = true;
	}

	private void Start()
	{
		nextBtn1.onClick.AddListener(delegate
		{
			toggleQuestion.Ok(delegate
			{
				nextBtn1.gameObject.SetActive(value: false);
				for (int i = 0; i < toggleQuestion.toggleItems.Count; i++)
				{
					if (!toggleQuestion.answer.Contains(i))
					{
						toggleQuestion.toggleItems[i].Hide();
					}
					else
					{
						toggleQuestion.toggleItems[i].Reset();
					}
				}
				TweenerCore<float, float, FloatOptions> t = DOTween.To(() => nextCg.alpha, delegate(float x)
				{
					nextCg.alpha = x;
				}, 1f, 1f);
				((Tween)t).OnComplete((TweenCallback)delegate
				{
					nextCg.interactable = true;
					nextCg.blocksRaycasts = true;
				});
				((Tween)t).SetDelay(0.5f);
			});
		});
		nextBtn2.onClick.AddListener(delegate
		{
			int[] array = new int[2] { 5, 3 };
			bool flag = true;
			if (selectedReasoningPage2IconItems.Count != array.Length)
			{
				flag = false;
			}
			else
			{
				foreach (int num in array)
				{
					bool flag2 = false;
					for (int j = 0; j < selectedReasoningPage2IconItems.Count; j++)
					{
						if (selectedReasoningPage2IconItems[j].id == num)
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				DLC7.Reasoning.ReasoningManager.Instance.EnterNextPage();
			}
			else
			{
				for (int k = 0; k < selectedReasoningPage2IconItems.Count; k++)
				{
					selectedReasoningPage2IconItems[k].ShowErrorTween();
				}
			}
		});
		StartCoroutine(ResetContentSizeFitter(contentSizeFitter1));
		StartCoroutine(ResetContentSizeFitter(contentSizeFitter2));
	}
}
