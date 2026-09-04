using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Van : MonoBehaviour
{
	public List<GameObject> expression;

	public void ShowExpression(int a)
	{
		for (int i = 0; i < expression.Count; i++)
		{
			if (expression[i].gameObject.GetComponent<CanvasGroup>().alpha == 1f && i != a)
			{
				int s = i;
				expression[i].GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
				{
					expression[s].GetComponent<Animator>().enabled = false;
					expression[s].SetActive(value: false);
				});
			}
		}
		expression[a].SetActive(value: true);
		expression[a].GetComponent<CanvasGroup>().DOFade(1f, 0.3f).OnComplete(delegate
		{
			expression[a].GetComponent<Animator>().enabled = true;
		});
	}
}
