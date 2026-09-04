using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CaiDan01 : MonoBehaviour
{
	public Image person;

	public List<Sprite> spritelist;

	public CaiDan02 caiDan02;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(VanAni());
		gameManager.musicManager.Stop();
		gameManager.soundManager.PlaySoundsLoop(44);
		if (gameManager.homeScene != null)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_1.GetComponent<CameraFilterPack_TV_Distorted>().enabled = true;
		}
	}

	private IEnumerator VanAni()
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < spritelist.Count; i++)
		{
			person.sprite = spritelist[i];
			yield return new WaitForSeconds(0.12f);
		}
		yield return new WaitForSeconds(2f);
		GetComponent<CanvasGroup>().DOFade(0f, 2f);
		caiDan02.gameObject.SetActive(value: true);
		caiDan02.GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(delegate
		{
			caiDan02.Init();
		});
	}
}
