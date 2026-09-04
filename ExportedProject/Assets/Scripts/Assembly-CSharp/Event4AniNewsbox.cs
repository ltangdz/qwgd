using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class Event4AniNewsbox : MonoBehaviour
{
	public Event4Video event4Video;

	public List<float> xPosition;

	public RectTransform newsBox;

	public Event4NewsZimu zimu;

	public List<AudioSource> news;

	private int crtNews;

	private GameManager gameManager;

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(ShowNews());
	}

	private IEnumerator ShowNews()
	{
		while (crtNews < xPosition.Count)
		{
			news[crtNews].volume = gameManager.soundManager.GetSoundVoice();
			float length;
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				news[crtNews].PlayOneShot(event4Video.audioCN[crtNews]);
				length = event4Video.audioCN[crtNews].length;
			}
			else
			{
				news[crtNews].PlayOneShot(event4Video.audioEN[crtNews]);
				length = event4Video.audioEN[crtNews].length;
			}
			zimu.Init(crtNews, length);
			yield return new WaitForSeconds(length + 0.5f);
			crtNews++;
			if (crtNews <= xPosition.Count - 1)
			{
				newsBox.DOLocalMoveX(xPosition[crtNews], 0.2f);
				yield return new WaitForSeconds(0.2f);
			}
		}
		event4Video.ChangePage1();
	}
}
