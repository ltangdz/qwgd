using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CaiDan02 : MonoBehaviour
{
	public Transform goodNews;

	public Transform badNews;

	public Image hand;

	public List<Sprite> handList;

	public CaiDan03 page03;

	private Transform newsPage;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.completeHideGame)
		{
			newsPage = goodNews;
		}
		else
		{
			newsPage = badNews;
		}
		newsPage.gameObject.SetActive(value: true);
	}

	public void Init()
	{
		StartCoroutine(ChangePage());
	}

	private IEnumerator ChangePage()
	{
		for (int j = 0; j < 2; j++)
		{
			yield return new WaitForSeconds(5f);
			for (int i = 0; i < handList.Count; i++)
			{
				hand.sprite = handList[i];
				if (i == 6)
				{
					TurnPage();
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield return new WaitForSeconds(5f);
		GetComponent<CanvasGroup>().DOFade(0f, 2f);
		page03.gameObject.SetActive(value: true);
		page03.GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(delegate
		{
			page03.Init();
		});
	}

	private void TurnPage()
	{
		float x = newsPage.localPosition.x;
		newsPage.DOLocalMoveX(x + 494f, 0.5f);
	}
}
