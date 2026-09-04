using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CaiDan04 : MonoBehaviour
{
	public Image imgPerson;

	public List<Sprite> imgList;

	public List<float> waitTime;

	public GameObject cardspawn;

	public CaiDan05 page05;

	private GameManager gameManager;

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(DropCard());
	}

	private IEnumerator DropCard()
	{
		for (int i = 0; i < imgList.Count; i++)
		{
			imgPerson.sprite = imgList[i];
			if (i == imgList.Count - 1)
			{
				cardspawn.SetActive(value: true);
				yield return new WaitForSeconds(1f);
				page05.gameObject.SetActive(value: true);
				page05.GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(delegate
				{
					page05.Init();
				});
			}
			yield return new WaitForSeconds(waitTime[i]);
		}
	}
}
