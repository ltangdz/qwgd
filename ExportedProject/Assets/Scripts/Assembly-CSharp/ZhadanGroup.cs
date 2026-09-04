using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanGroup : MonoBehaviour
{
	[Tooltip("有组队")]
	public bool haveSaveTeam;

	public GameObject sameTeam;

	[Tooltip("可以点击")]
	public bool isCanClick = true;

	[Tooltip("移动速度")]
	public float moveSpeed = 1f;

	public RectTransform leader;

	public RectTransform destination;

	public List<RectTransform> lines;

	public List<RectTransform> brokenLines;

	public Sprite redSprite;

	public ZhadanDialog prtObj;

	public List<ZhadanDoor> door;

	public int step;

	private void Start()
	{
		prtObj.mapNum += 1f;
	}

	public IEnumerator LeaderMove()
	{
		if (prtObj.isGameOver)
		{
			yield break;
		}
		isCanClick = false;
		int lineLength = lines.Count;
		int i;
		for (i = step; i < lineLength; i++)
		{
			step = i;
			float num = lines[i].sizeDelta.x / (moveSpeed * 10f);
			if (i < brokenLines.Count)
			{
				leader.DOLocalMove(brokenLines[i].localPosition, num).SetEase(Ease.Linear).OnComplete(delegate
				{
					brokenLines[i].gameObject.SetActive(value: false);
				});
			}
			else
			{
				leader.DOLocalMove(destination.localPosition, num).SetEase(Ease.Linear).OnComplete(delegate
				{
					step = -1;
					prtObj.MoveEnd();
				});
			}
			lines[i].DOSizeDelta(new Vector2(0f, 3f), num).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num);
		}
	}

	public void StopMoving()
	{
		isCanClick = true;
		StopAllCoroutines();
		leader.DOKill();
		for (int i = 0; i < lines.Count; i++)
		{
			lines[i].DOKill();
		}
	}

	public void ShowRedAni(Image obj01, Image obj02)
	{
		Debug.Log("gameover1");
		prtObj.Trigger();
		prtObj.isGameOver = true;
		leader.DOKill();
		for (int i = 0; i < lines.Count; i++)
		{
			lines[i].DOKill();
		}
		Sequence sequence = DOTween.Sequence();
		if (obj01.gameObject.name == "leader" && obj02.gameObject.name == "leader")
		{
			obj01.sprite = redSprite;
			obj02.sprite = redSprite;
			sequence.Append(obj01.DOFade(0.5f, 0.2f));
			sequence.Join(obj02.DOFade(0.5f, 0.2f));
			sequence.Append(obj01.DOFade(1f, 0.2f));
			sequence.Join(obj02.DOFade(1f, 0.2f));
		}
		else if (obj01.gameObject.name == "leader")
		{
			obj01.sprite = redSprite;
			sequence.Append(obj01.DOFade(0.8f, 0.2f));
			sequence.Append(obj01.DOFade(1f, 0.2f));
		}
		else if (obj02.gameObject.name == "leader")
		{
			obj02.sprite = redSprite;
			sequence.Append(obj02.DOFade(0.8f, 0.2f));
			sequence.Append(obj02.DOFade(1f, 0.2f));
		}
		sequence.Play().SetLoops(2).OnComplete(delegate
		{
			prtObj.ReStartGame();
		});
	}

	public void ChangeType()
	{
		if (door.Count != 0)
		{
			for (int i = 0; i < door.Count; i++)
			{
				door[i].ChangeType();
			}
		}
	}
}
