using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoatPanel : MonoBehaviour
{
	public Image boat;

	public Image sea01;

	public Image sea02;

	public GameObject tiao01;

	public GameObject tiao02;

	public GameObject tiao03;

	private Coroutine lang;

	private GameManager gameManager;

	private void Update()
	{
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusic(5);
		gameManager.homeScene.cameraFilterPack_TV_Noise.enabled = true;
		gameManager.homeScene.cameraFilterPack_Atmosphere_Rain_Pro.enabled = true;
		Invoke("BoatDi", 5f);
		Invoke("StopAllAni", 8f);
	}

	private void BoatDi()
	{
		gameManager.soundManager.PlaySound(29);
	}

	private void StopAllAni()
	{
		GetComponent<Animator>().enabled = false;
		StartCoroutine(ShowOne());
	}

	private IEnumerator ShowOne()
	{
		tiao01.transform.DOLocalMoveX(-265f, 0.5f);
		yield return new WaitForSeconds(3f);
		StartCoroutine(ShowTwo());
	}

	private IEnumerator ShowTwo()
	{
		tiao02.transform.DOLocalMoveX(265f, 0.5f);
		yield return new WaitForSeconds(1f);
		gameManager.soundManager.PlaySound(31);
		tiao02.transform.Find("mask").GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(3f);
		StartCoroutine(ShowThree());
	}

	private IEnumerator ShowThree()
	{
		tiao03.transform.DOLocalMoveX(-265f, 0.5f);
		gameManager.soundManager.PlaySound(32);
		yield return new WaitForSeconds(10f);
		gameManager.homeScene.cameraFilterPack_TV_Noise.enabled = false;
		gameManager.homeScene.cameraFilterPack_Atmosphere_Rain_Pro.enabled = false;
		gameManager.soundManager.Stop();
		StartCoroutine(LowMusic());
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		gameManager.musicManager.Stop();
		Object.Instantiate(Resources.Load<GameObject>("Dialog/thanksPanel"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator LowMusic()
	{
		float vol = PlayerPrefs.GetFloat("musicvol", 1f);
		gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol > 0f)
		{
			vol -= 0.05f;
			yield return new WaitForSeconds(0.05f);
			gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	private IEnumerator Lang()
	{
		int a = 1;
		while (true)
		{
			a++;
			if (a % 2 != 0)
			{
				sea02.GetComponent<CanvasGroup>().DOFade(1f, 2f);
				sea01.GetComponent<CanvasGroup>().DOFade(0f, 2f);
			}
			else
			{
				sea02.GetComponent<CanvasGroup>().DOFade(0f, 2f);
				sea01.GetComponent<CanvasGroup>().DOFade(1f, 2f);
			}
			yield return new WaitForSeconds(3f);
		}
	}
}
