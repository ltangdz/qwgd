using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateCardGroup : MonoBehaviour
{
	public List<RotateCard> rotateCardList;

	public List<bool> answerList;

	private void Start()
	{
		if (rotateCardList.Count != answerList.Count)
		{
			Debug.LogError("Count doesn't match");
		}
	}

	public void CheckRight(UnityAction action)
	{
		bool flag = true;
		for (int i = 0; i < rotateCardList.Count; i++)
		{
			if (rotateCardList[i].isUp != answerList[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			action?.Invoke();
			return;
		}
		for (int j = 0; j < rotateCardList.Count; j++)
		{
			rotateCardList[j].ShowErrorTween();
		}
	}
}
