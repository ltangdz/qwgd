using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public List<AudioClip> musics;

	private AudioSource audiosource;

	private bool isStop = true;

	public GameManager gameManager;

	public List<AudioClip> animations;

	public AudioSource Audiosource => audiosource;

	private void Awake()
	{
		audiosource = GetComponent<AudioSource>();
		audiosource.volume = PlayerPrefs.GetFloat("musicvol", 1f);
	}

	public void PlayNormalMusic(int id, int last)
	{
		if (I18N.instance.gameLang == LanguageCode.EN && id == 9)
		{
			id = 19;
		}
		audiosource.clip = musics[id];
		DOTween.To(() => audiosource.volume, delegate(float x)
		{
			audiosource.volume = x;
		}, 0f, 1f).OnComplete(delegate
		{
			audiosource.Stop();
			audiosource.PlayOneShot(musics[id]);
			ResumeVol();
			StartCoroutine(AudioCallBack(delegate
			{
				PlayMusicLoop(last);
			}));
		});
	}

	private IEnumerator AudioCallBack(Action action)
	{
		while (audiosource.isPlaying)
		{
			yield return new WaitForSecondsRealtime(0.1f);
		}
		if (!gameManager.player.GetEventId().Equals("110004") && !gameManager.player.playerdata.islast4 && !gameManager.isbug)
		{
			action();
		}
	}

	public void PlayMusic(int id)
	{
		if (isStop)
		{
			Debug.Log("playMusic:" + id);
			isStop = false;
			DOTween.To(() => audiosource.volume, delegate(float x)
			{
				audiosource.volume = x;
			}, 0f, 1f).OnComplete(delegate
			{
				audiosource.clip = musics[id];
				audiosource.Play();
				ResumeVol();
			});
		}
	}

	public void PlayMusicLoop(int id, bool isneedlow = false)
	{
		Debug.Log("PlayMusicLoop:" + id);
		if (id == 3)
		{
			if (gameManager.player.playerdata.Eventid == 1 || gameManager.player.playerdata.Eventid == 2 || gameManager.player.playerdata.Eventid == 3)
			{
				id = 3;
			}
			else if (gameManager.player.playerdata.Eventid == 4 || gameManager.player.playerdata.Eventid == 5)
			{
				id = 2;
			}
			else if (gameManager.player.playerdata.Eventid == 6)
			{
				id = 13;
			}
			else if (gameManager.player.playerdata.Eventid == 7)
			{
				id = 20;
			}
			else if (gameManager.player.playerdata.Eventid == 8)
			{
				id = 26;
			}
		}
		else if (id == 4)
		{
			if (gameManager.player.playerdata.Eventid == 1 || gameManager.player.playerdata.Eventid == 2)
			{
				id = 1;
			}
			else if (gameManager.player.playerdata.Eventid == 3 || gameManager.player.playerdata.Eventid == 4 || gameManager.player.playerdata.Eventid == 5)
			{
				id = 4;
			}
			else if (gameManager.player.playerdata.Eventid == 6)
			{
				id = 14;
			}
		}
		if (!(audiosource.clip != musics[id]) && !isStop)
		{
			return;
		}
		isStop = false;
		DOTween.To(() => audiosource.volume, delegate(float x)
		{
			audiosource.volume = x;
		}, 0f, 1f).OnComplete(delegate
		{
			audiosource.clip = musics[id];
			audiosource.Play();
			audiosource.loop = true;
			if (isneedlow && PlayerPrefs.GetFloat("musicvol", 1f) > 0.2f)
			{
				LowerVol();
			}
			else
			{
				ResumeVol();
			}
		});
	}

	public void SetLoop(bool isloop)
	{
		audiosource.loop = isloop;
	}

	public void Stop()
	{
		isStop = true;
		DOTween.To(() => audiosource.volume, delegate(float x)
		{
			audiosource.volume = x;
		}, 0f, 1f).OnComplete(delegate
		{
			audiosource.Stop();
			ResumeVol();
		});
	}

	public void LowerVol(float vol = 0.15f)
	{
		if (PlayerPrefs.GetFloat("musicvol", 1f) > vol)
		{
			DOTween.To(() => audiosource.volume, delegate(float x)
			{
				audiosource.volume = x;
			}, vol, 1f);
		}
	}

	public void ResumeVol()
	{
		DOTween.To(() => audiosource.volume, delegate(float x)
		{
			audiosource.volume = x;
		}, PlayerPrefs.GetFloat("musicvol", 1f), 1f);
	}

	public float GetMusicVoice()
	{
		return audiosource.volume;
	}

	public void PlayAnimationSound(int id)
	{
		if (id < animations.Count)
		{
			audiosource.PlayOneShot(animations[id]);
		}
	}
}
