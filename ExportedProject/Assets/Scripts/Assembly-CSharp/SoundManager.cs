using System;
using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public List<AudioClip> sounds;

	public List<AudioClip> failed;

	public List<AudioClip> failed_en;

	public List<AudioClip> teach;

	public List<AudioClip> teach_en;

	public List<AudioClip> hacker;

	public List<AudioClip> livevan_cn;

	public List<AudioClip> livevan_en;

	public List<AudioClip> liveorzanswer_cn;

	public List<AudioClip> liveorzanswer_en;

	public List<AudioClip> event01;

	public List<AudioClip> event01_en;

	public List<AudioClip> event02;

	public List<AudioClip> event02_en;

	public List<AudioClip> event03;

	public List<AudioClip> event03_en;

	public List<AudioClip> event04;

	public List<AudioClip> event04_en;

	public List<AudioClip> event05;

	public List<AudioClip> event05_en;

	public List<AudioClip> event06;

	public List<AudioClip> event06_en;

	public List<AudioClip> event07;

	public List<AudioClip> event07_en;

	public List<AudioClip> catchList;

	public AudioSource audiosource;

	public AudioSource audiosourceloop;

	public AudioSource catchsourceloop;

	private void Awake()
	{
		audiosource = GetComponent<AudioSource>();
		audiosource.volume = PlayerPrefs.GetFloat("soundvol", 1f);
	}

	public float PlayLiveVan(int id)
	{
		if (id >= livevan_cn.Count)
		{
			return 0f;
		}
		if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
		{
			audiosource.PlayOneShot(livevan_cn[id]);
			return livevan_cn[id].length;
		}
		if (I18N.instance.gameLang.Equals(LanguageCode.EN))
		{
			audiosource.PlayOneShot(livevan_en[id]);
			return livevan_en[id].length;
		}
		return 0f;
	}

	public float PlayLiveOrzQuestion(int id)
	{
		if (id >= liveorzanswer_cn.Count)
		{
			return 0f;
		}
		if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
		{
			audiosource.PlayOneShot(liveorzanswer_cn[id]);
			return liveorzanswer_cn[id].length;
		}
		if (I18N.instance.gameLang.Equals(LanguageCode.EN))
		{
			audiosource.PlayOneShot(liveorzanswer_en[id]);
			return liveorzanswer_en[id].length;
		}
		return 0f;
	}

	public void PlayCatchSound(int id)
	{
		if (id < sounds.Count)
		{
			audiosource.clip = sounds[id];
			audiosource.Play();
		}
	}

	public void PlaySound(int id)
	{
		if (id < sounds.Count)
		{
			audiosource.PlayOneShot(sounds[id]);
		}
	}

	public void PlaySound(int id, Action action)
	{
		if (id < sounds.Count)
		{
			audiosource.PlayOneShot(sounds[id]);
			StartCoroutine(AudioCallBack(delegate
			{
				action();
			}));
		}
	}

	private IEnumerator AudioCallBack(Action action)
	{
		while (audiosource.isPlaying)
		{
			yield return new WaitForSecondsRealtime(0.1f);
		}
		action();
	}

	public void PlayHackerSound(int id)
	{
		if (id < hacker.Count)
		{
			audiosource.PlayOneShot(hacker[id]);
		}
	}

	public void PlayHackerSoundLoop(int id)
	{
		if (id < hacker.Count)
		{
			audiosourceloop.clip = hacker[id];
			audiosourceloop.Play();
		}
	}

	public void PlayEvent(string eventID, int id)
	{
		List<AudioClip> list = new List<AudioClip>();
		switch (eventID)
		{
		case "110000":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = teach;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = teach_en;
			}
			break;
		case "110001":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event01;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event01_en;
			}
			break;
		case "110002":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event02;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event02_en;
			}
			break;
		case "110003":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event03;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event03_en;
			}
			break;
		case "110004":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event04;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event04_en;
			}
			break;
		case "110005":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event05;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event05_en;
			}
			break;
		case "110006":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event06;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event06_en;
			}
			break;
		case "110008":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event07;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event07_en;
			}
			break;
		}
		if (id < list.Count)
		{
			Debug.Log("播放ID" + id);
			if (id != -1)
			{
				audiosource.PlayOneShot(list[id]);
			}
		}
	}

	public float PlayEventFinished(string eventID, int id, bool playAudio = true)
	{
		List<AudioClip> list = new List<AudioClip>();
		switch (eventID)
		{
		case "110000":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = teach;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = teach_en;
			}
			break;
		case "110001":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event01;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event01_en;
			}
			break;
		case "110002":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event02;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event02_en;
			}
			break;
		case "110003":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event03;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event03_en;
			}
			break;
		case "110004":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event04;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event04_en;
			}
			break;
		case "110005":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event05;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event05_en;
			}
			break;
		case "110006":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event06;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event06_en;
			}
			break;
		case "110008":
			if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
			{
				list = event07;
			}
			else if (I18N.instance.gameLang == LanguageCode.EN)
			{
				list = event07_en;
			}
			break;
		}
		if (id >= list.Count)
		{
			return 0f;
		}
		if (id != -1 && playAudio)
		{
			audiosource.PlayOneShot(list[id]);
		}
		return list[id].length;
	}

	public float PlayDLCEventSound(string eventID, string group, string name, bool playAudio = true)
	{
		string text = "cn";
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			text = "en";
		}
		AudioClip audioClip = Resources.Load<AudioClip>($"Sound/{eventID.ToString()}/{text}/{group}/{name}");
		if (audioClip == null)
		{
			return 1f;
		}
		if (playAudio)
		{
			audiosource.PlayOneShot(audioClip);
		}
		return audioClip.length;
	}

	public float PlayFailed(int i, int id)
	{
		Debug.Log(2 * i + id - 2);
		if (i > 3)
		{
			i = 3;
		}
		if (id >= failed.Count)
		{
			return 0f;
		}
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			audiosource.PlayOneShot(failed[2 * i + id - 2]);
			return failed[2 * i + id - 2].length;
		}
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			audiosource.PlayOneShot(failed_en[2 * i + id - 2]);
			return failed_en[2 * i + id - 2].length;
		}
		return 0f;
	}

	public void PlayAudioClip(AudioClip audioClip)
	{
		audiosource.PlayOneShot(audioClip);
	}

	public void PlaySoundLoop(int id)
	{
		if (id < sounds.Count)
		{
			audiosource.clip = sounds[id];
			audiosource.Play();
			audiosource.loop = true;
		}
	}

	public void PlayCatchSoundLoop(int id)
	{
		if (id < sounds.Count)
		{
			catchsourceloop.clip = catchList[id];
			catchsourceloop.Play();
			catchsourceloop.loop = true;
		}
	}

	public void Stop()
	{
		audiosource.Stop();
	}

	public void StopLoop()
	{
		audiosourceloop.Stop();
	}

	public float GetSoundVoice()
	{
		return audiosource.volume;
	}

	public void SetSoundVoice(float voice)
	{
		audiosource.volume = voice;
		audiosourceloop.volume = voice;
	}

	public void PlaySoundsLoop(int id)
	{
		if (id < sounds.Count)
		{
			audiosourceloop.clip = sounds[id];
			audiosourceloop.Play();
		}
	}

	public void PlayCatchLoop(int id)
	{
		audiosourceloop.clip = catchList[id];
		audiosourceloop.loop = true;
		audiosourceloop.Play();
	}

	public void PlayCatch(int id)
	{
		audiosource.clip = catchList[id];
		audiosource.loop = false;
		audiosource.Play();
	}
}
