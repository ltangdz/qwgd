using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningPage1UI : MonoBehaviour
{
	public static ReasoningPage1LeftItemJudgeUI selectedReasoningPage1LeftItemJudgeUI;

	public List<ReasoningPage1LeftItem> leftItemList;

	public List<ReasoningPage1ResultItem> resultItemList;

	public CanvasGroup nextCg;

	public Button nextBtn;

	public ToggleQuestion toggleQuestion;

	private int curLeftIndex;

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
		for (int i = 0; i < leftItemList.Count; i++)
		{
			leftItemList[i].EmptyContent();
			if (i == 0)
			{
				leftItemList[i].Show();
			}
			else
			{
				leftItemList[i].gameObject.SetActive(value: false);
			}
		}
		nextBtn.onClick.AddListener(delegate
		{
			toggleQuestion.Ok(delegate
			{
				DLC7.Reasoning.ReasoningManager.Instance.EnterNextPage();
			});
		});
		StartCoroutine(ResetContentSizeFitter(contentSizeFitter1));
		StartCoroutine(ResetContentSizeFitter(contentSizeFitter2));
	}

	private void ConfirmResult(int id)
	{
		resultItemList[id].SetUsed();
		if (curLeftIndex < leftItemList.Count - 1)
		{
			leftItemList[curLeftIndex].ShowConfirmTween(delegate
			{
				curLeftIndex++;
				leftItemList[curLeftIndex].Show();
			});
			return;
		}
		((Tween)DOTween.To(() => nextCg.alpha, delegate(float x)
		{
			nextCg.alpha = x;
		}, 1f, 1f)).OnComplete((TweenCallback)delegate
		{
			nextCg.interactable = true;
			nextCg.blocksRaycasts = true;
		});
	}

	private void ResetResult(int id)
	{
		resultItemList[id].Reset();
	}

	private void Awake()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onConfirmResultNotice += ConfirmResult;
		DLC7.Reasoning.ReasoningManager.Instance.onResetResultNotice += ResetResult;
	}

	private void OnDisable()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onConfirmResultNotice -= ConfirmResult;
		DLC7.Reasoning.ReasoningManager.Instance.onResetResultNotice -= ResetResult;
	}
}
