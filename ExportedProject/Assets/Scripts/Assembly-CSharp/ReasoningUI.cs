using System.Collections.Generic;
using DG.Tweening;
using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.Events;

public class ReasoningUI : ReasoningMiddle
{
	public List<GameObject> pageList;

	private int pageIndex;

	private void EnterNextPage()
	{
		MovePage(pageIndex, delegate
		{
			pageIndex++;
			MovePage(pageIndex);
		});
	}

	private void MovePage(int index, UnityAction action = null)
	{
		if (index >= pageList.Count)
		{
			DLC7.Reasoning.ReasoningManager.Instance.NoticeResult("4016");
			return;
		}
		float y = pageList[index].transform.localPosition.y;
		((Tween)DOTween.To(() => pageList[index].transform.localPosition, delegate(Vector3 x)
		{
			pageList[index].transform.localPosition = x;
		}, new Vector3(pageList[index].transform.localPosition.x, y + 1080f, 0f), 0.5f)).OnComplete((TweenCallback)delegate
		{
			action?.Invoke();
		});
	}

	private void Awake()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onEnterNextPageNotice += EnterNextPage;
	}

	private void OnDisable()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onEnterNextPageNotice -= EnterNextPage;
	}
}
