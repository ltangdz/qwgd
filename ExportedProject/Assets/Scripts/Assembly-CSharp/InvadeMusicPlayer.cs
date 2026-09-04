using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeMusicPlayer : MonoBehaviour
{
	public Button btnClose;

	public Image bk;

	public Image musicImg;

	public Image hightLight;

	public Button btnPlay;

	public Sprite stopSprite;

	public Sprite PlaySprite;

	public AudioClip themeSongcn;

	public AudioClip themeSongen;

	private GameManager gameManager;

	private bool playing;

	private float rotate;

	private bool playEnd = true;

	[SerializeField]
	private float musicTime;

	[SerializeField]
	private float playTime;

	private void Start()
	{
		bk.GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		bk.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<AudioSource>().volume = gameManager.musicManager.GetMusicVoice();
		btnClose.onClick.AddListener(delegate
		{
			gameManager.musicManager.ResumeVol();
			bk.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.3f);
			bk.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		});
		btnPlay.onClick.AddListener(delegate
		{
			if (!playing && playEnd)
			{
				playEnd = false;
				hightLight.GetComponent<RectTransform>().sizeDelta = new Vector2(12f, 11f);
				musicTime = PlayThemeSong();
				GetComponent<AudioSource>().time = 0f;
				PlayMusic(musicTime);
			}
			else if (!playing && !playEnd)
			{
				GetComponent<AudioSource>().Play();
				GetComponent<AudioSource>().time = playTime;
				PlayMusic(musicTime - playTime);
			}
			else
			{
				PauseMusic();
			}
		});
	}

	private void PlayMusic(float time)
	{
		btnPlay.GetComponent<Image>().sprite = PlaySprite;
		playing = true;
		gameManager.musicManager.LowerVol(0f);
		hightLight.GetComponent<RectTransform>().DOSizeDelta(new Vector2(382f, 11f), time).SetEase(Ease.Linear);
		rotate = musicImg.GetComponent<RectTransform>().localEulerAngles.z;
		StartCoroutine(ImgRotate());
		StartCoroutine(ResumeVol(time));
	}

	private void PauseMusic()
	{
		btnPlay.GetComponent<Image>().sprite = stopSprite;
		hightLight.GetComponent<RectTransform>().DOKill();
		musicImg.GetComponent<RectTransform>().DOKill();
		PauseSong();
		playTime = GetComponent<AudioSource>().time;
		gameManager.musicManager.ResumeVol();
		playing = false;
		StopAllCoroutines();
	}

	private IEnumerator ResumeVol(float time)
	{
		yield return new WaitForSeconds(time);
		gameManager.musicManager.ResumeVol();
		PauseMusic();
		playEnd = true;
		gameManager.UnlockAchievements("song");
	}

	private IEnumerator ImgRotate()
	{
		while (playing)
		{
			rotate += 18f;
			if (rotate >= 360f)
			{
				rotate -= 360f;
			}
			musicImg.GetComponent<RectTransform>().DORotate(new Vector3(0f, 0f, rotate), 1f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(1f);
		}
	}

	public float PlayThemeSong()
	{
		float num = 0f;
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			GetComponent<AudioSource>().clip = themeSongcn;
			GetComponent<AudioSource>().Play();
			return themeSongcn.length;
		}
		GetComponent<AudioSource>().clip = themeSongen;
		GetComponent<AudioSource>().Play();
		return themeSongen.length;
	}

	public void PauseSong()
	{
		GetComponent<AudioSource>().Pause();
	}
}
