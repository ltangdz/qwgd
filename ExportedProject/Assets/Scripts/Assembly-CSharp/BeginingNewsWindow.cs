using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class BeginingNewsWindow : MonoBehaviour
{
	public I18NText txt_bigtitle;

	public I18NText txt_littletitle;

	public string[] zimus;

	public GameObject img_scanline;

	public float musicTime;

	public float waitTime;

	public AudioClip audioCN;

	public AudioClip audioEN;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		InvokeRepeating("MoveScan", 0.2f, 20f);
	}

	public void PlayMusic()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<AudioSource>().enabled = true;
		GetComponent<AudioSource>().volume = gameManager.soundManager.GetSoundVoice();
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			GetComponent<AudioSource>().PlayOneShot(audioCN);
		}
		else
		{
			Debug.Log("英文");
			GetComponent<AudioSource>().PlayOneShot(audioEN);
		}
		StartCoroutine(EndMusic());
	}

	private void MoveScan()
	{
		img_scanline.transform.DOLocalMoveY(152f, 10f).OnComplete(delegate
		{
			img_scanline.transform.DOLocalMoveY(-149f, 10f).OnComplete(delegate
			{
			});
		});
	}

	private IEnumerator EndMusic()
	{
		yield return new WaitForSeconds(musicTime - waitTime);
		StartCoroutine(LowMusic());
	}

	private IEnumerator LowMusic()
	{
		float vol = gameManager.soundManager.GetSoundVoice();
		GetComponent<AudioSource>().volume = vol;
		while (vol > 0f)
		{
			vol -= 0.02f;
			yield return new WaitForSeconds(0.02f);
			GetComponent<AudioSource>().volume = vol;
		}
	}
}
