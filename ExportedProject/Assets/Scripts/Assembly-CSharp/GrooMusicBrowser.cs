using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class GrooMusicBrowser : MonoBehaviour
{
	public Image hightLight;

	public Button btnPlay;

	public Sprite stopSprite;

	public Sprite PlaySprite;

	public AudioClip themeSongcn;

	public AudioClip themeSongen;

	public Text time;

	public Button btnPaihang;

	public Button btnGengduo;

	public GameObject alert1;

	public GameObject alert2;

	public Button btnClose1;

	public Button btnClose2;

	private GameManager gameManager;

	private bool playing;

	private float rotate;

	private bool playEnd = true;

	[SerializeField]
	private float musicTime;

	[SerializeField]
	private float playTime;

	private float stopMusicTime;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnPaihang.onClick.AddListener(delegate
		{
			alert1.SetActive(value: true);
		});
		btnGengduo.onClick.AddListener(delegate
		{
			alert1.SetActive(value: true);
		});
		btnClose1.onClick.AddListener(delegate
		{
			alert1.SetActive(value: false);
		});
		btnClose2.onClick.AddListener(delegate
		{
			alert2.SetActive(value: false);
		});
		musicTime = PlayThemeSong();
		SetTime();
		btnPlay.onClick.AddListener(delegate
		{
			if (!playing && playEnd)
			{
				playEnd = false;
				hightLight.GetComponent<RectTransform>().sizeDelta = new Vector2(12f, 5f);
				musicTime = PlayThemeSong();
				GetComponent<AudioSource>().time = 0f;
				PlayMusic(stopMusicTime, musicTime);
			}
			else if (!playing && !playEnd)
			{
				GetComponent<AudioSource>().Play();
				GetComponent<AudioSource>().time = playTime;
				PlayMusic(stopMusicTime - playTime, musicTime - playTime);
			}
			else
			{
				PauseMusic();
			}
		});
	}

	public void Init()
	{
		GetComponent<AudioSource>().enabled = false;
		hightLight.GetComponent<RectTransform>().sizeDelta = new Vector2(12f, 5f);
		btnPlay.GetComponent<Image>().sprite = stopSprite;
		hightLight.GetComponent<RectTransform>().DOKill();
		PauseSong();
		playing = false;
		playEnd = true;
		StopAllCoroutines();
		playTime = 0f;
		SetTime();
	}

	private void PlayMusic(float time, float totalTime)
	{
		if (!GetComponent<AudioSource>().enabled)
		{
			GetComponent<AudioSource>().enabled = true;
		}
		GetComponent<AudioSource>().volume = gameManager.musicManager.GetMusicVoice();
		btnPlay.GetComponent<Image>().sprite = PlaySprite;
		playing = true;
		gameManager.musicManager.LowerVol(0f);
		hightLight.GetComponent<RectTransform>().DOSizeDelta(new Vector2(688f, 5f), totalTime).SetEase(Ease.Linear);
		StartCoroutine(RefreshTime());
		StartCoroutine(ResumeVol(time));
	}

	private void PauseMusic()
	{
		btnPlay.GetComponent<Image>().sprite = stopSprite;
		hightLight.GetComponent<RectTransform>().DOKill();
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
	}

	public float PlayThemeSong()
	{
		float num = 0f;
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			GetComponent<AudioSource>().clip = themeSongcn;
			GetComponent<AudioSource>().Play();
			num = themeSongcn.length;
			stopMusicTime = themeSongcn.length;
		}
		else
		{
			GetComponent<AudioSource>().clip = themeSongen;
			GetComponent<AudioSource>().Play();
			num = themeSongen.length;
			stopMusicTime = themeSongen.length;
		}
		return num;
	}

	public void PauseSong()
	{
		GetComponent<AudioSource>().Pause();
	}

	private IEnumerator RefreshTime()
	{
		while (true)
		{
			playTime = GetComponent<AudioSource>().time;
			SetTime();
			yield return new WaitForSeconds(1f);
		}
	}

	private void SetTime()
	{
		string text = ((((int)playTime / 60).ToString().Length >= 2) ? ((int)playTime / 60).ToString() : ("0" + (int)playTime / 60));
		string text2 = ((((int)playTime % 60).ToString().Length >= 2) ? ((int)playTime % 60).ToString() : ("0" + (int)playTime % 60));
		string text3 = ((((int)musicTime / 60).ToString().Length >= 2) ? ((int)musicTime / 60).ToString() : ("0" + (int)musicTime / 60));
		string text4 = ((((int)musicTime % 60).ToString().Length >= 2) ? ((int)musicTime % 60).ToString() : ("0" + (int)musicTime % 60));
		time.GetComponent<I18NText>().updateTranslation2("<color=#ffffff>" + text + ":" + text2 + "</color>/" + text3 + ":" + text4);
	}
}
