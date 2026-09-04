using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CaiDan03 : MonoBehaviour
{
	public Transform imgHand;

	public Image imgPerson;

	public Transform imgHandId;

	public Transform imgHandHuoji;

	public List<Sprite> personList;

	public List<float> time;

	public List<Sprite> huojiList;

	public CaiDan04 page04;

	public GameObject zimu;

	private IEnumerator VanSaying;

	private GameManager gameManager;

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		VanSaying = Saying();
		StartCoroutine(MoveHand());
	}

	private IEnumerator MoveHand()
	{
		yield return new WaitForSeconds(1f);
		imgHand.DOLocalMove(new Vector3(-350f, -904f, 0f), 1.2f);
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(VanSaying);
		zimu.SetActive(value: true);
		string eventId = gameManager.player.GetEventId();
		float seconds = gameManager.soundManager.PlayEventFinished(eventId, 70);
		yield return new WaitForSeconds(seconds);
		StopCoroutine(VanSaying);
		imgPerson.sprite = personList[1];
		yield return new WaitForSeconds(0.2f);
		imgPerson.sprite = personList[0];
		yield return new WaitForSeconds(0.2f);
		zimu.SetActive(value: false);
		imgHandId.DOLocalMove(new Vector3(-282f, -26f, 0f), 1f);
		yield return new WaitForSeconds(1.5f);
		imgHandHuoji.DOLocalMove(new Vector3(376f, -123.5f, 0f), 1f);
		yield return new WaitForSeconds(1f);
		gameManager.soundManager.PlaySound(43);
		for (int i = 0; i < huojiList.Count; i++)
		{
			imgHandHuoji.GetComponent<Image>().sprite = huojiList[i];
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(1f);
		GetComponent<CanvasGroup>().DOFade(0f, 2f);
		page04.gameObject.SetActive(value: true);
		gameManager.soundManager.StopLoop();
		gameManager.musicManager.PlayMusicLoop(6);
		page04.GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(delegate
		{
			page04.Init();
		});
	}

	private IEnumerator Saying()
	{
		while (true)
		{
			for (int i = 2; i < personList.Count; i++)
			{
				imgPerson.sprite = personList[i];
				yield return new WaitForSeconds(time[i]);
			}
		}
	}
}
