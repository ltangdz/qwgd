using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event4Video : MonoBehaviour
{
	public Image imgWange;

	public Image imgWange1;

	public Image imgSaomiao;

	public Image imgCircle01;

	public Image imgCircle02;

	public Image imgCircle03;

	public Image imgPoint;

	public GameObject black;

	public GameObject page01;

	public GameObject page02;

	public GameObject page03;

	public Event4AniNewsbox event4AniNewsBox;

	public List<AudioClip> audioCN;

	public List<AudioClip> audioEN;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(7);
		black.GetComponent<Image>().DOFade(0f, 2f).SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				Debug.Log("startgame");
				event4AniNewsBox.Init();
			});
		StartCoroutine(StartAni());
		InvokeRepeating("SaomiaoAni", 0f, 6f);
		StartCoroutine(RotateAni());
		StartCoroutine(RotateAni1());
		StartCoroutine(PointAni());
	}

	public void ChangePage1()
	{
		gameManager.musicManager.PlayMusicLoop(2);
		StartCoroutine(ChangeToPage(page01, page02));
	}

	public void ChangePage2()
	{
		StartCoroutine(ChangeToPage(page02, page03));
	}

	private IEnumerator ChangeToPage(GameObject hideObj, GameObject showObj)
	{
		black.GetComponent<Image>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		hideObj.SetActive(value: false);
		showObj.SetActive(value: true);
		black.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(2f);
		showObj.GetComponent<Animator>().enabled = true;
	}

	private IEnumerator StartAni()
	{
		while (true)
		{
			imgWange.GetComponent<RectTransform>().DOLocalMoveX(-1920f, 20f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					imgWange.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
				});
			imgWange1.GetComponent<RectTransform>().DOLocalMoveX(0f, 20f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					imgWange1.GetComponent<RectTransform>().localPosition = new Vector3(1920f, 0f, 0f);
				});
			yield return new WaitForSeconds(20f);
		}
	}

	private void SaomiaoAni()
	{
		imgSaomiao.GetComponent<RectTransform>().localPosition = new Vector3(-1238f, 0f, 0f);
		imgSaomiao.GetComponent<RectTransform>().DOLocalMoveX(1238f, 5f).SetEase(Ease.Linear);
	}

	private IEnumerator RotateAni()
	{
		while (true)
		{
			imgCircle01.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 180f), 25f).SetEase(Ease.Linear);
			imgCircle02.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, -180f), 25f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(25f);
			imgCircle01.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 360f), 25f).SetEase(Ease.Linear);
			imgCircle02.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 0f), 25f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(25f);
		}
	}

	private IEnumerator RotateAni1()
	{
		while (true)
		{
			imgCircle03.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 180f), 10f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(10f);
			imgCircle03.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 360f), 10f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(10f);
		}
	}

	private IEnumerator PointAni()
	{
		while (true)
		{
			imgPoint.GetComponent<Image>().DOFade(0.2f, 3f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(3f);
			imgPoint.GetComponent<Image>().DOFade(1f, 3f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(3f);
		}
	}
}
