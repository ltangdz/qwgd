using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteAnimation : MonoBehaviour
{
	public int state;

	public Image ImageSource;

	private int mCurFrame;

	private float mDelta;

	public float FPS = 5f;

	public List<Sprite> normal_frames;

	public List<Sprite> speak_frames;

	public List<Sprite> hate_frames;

	public List<Sprite> angry_frames;

	public List<Sprite> happy_frames;

	public List<Sprite> contempt_frames;

	public List<Sprite> abnormal_frames;

	public List<Sprite> abnormal_ani;

	public List<Sprite> wake_frames;

	public List<Sprite> wake_ani;

	public bool IsPlaying;

	public bool Foward = true;

	public bool AutoPlay;

	public bool Loop;

	public int delay;

	public int count_down = 2;

	private bool abnormalAniShowed;

	private IEnumerator motou;

	public int FrameCount
	{
		get
		{
			switch (state)
			{
			case 0:
				return normal_frames.Count;
			case 1:
				return speak_frames.Count;
			case 2:
				return hate_frames.Count;
			case 3:
				return angry_frames.Count;
			case 4:
				return happy_frames.Count;
			case 5:
				return contempt_frames.Count;
			case 6:
				return abnormal_frames.Count;
			case 7:
				return wake_frames.Count;
			default:
				return normal_frames.Count;
			}
		}
	}

	private void Delay()
	{
		if (count_down > 0)
		{
			count_down--;
			return;
		}
		count_down = delay;
		if (Loop)
		{
			IsPlaying = true;
		}
		CancelInvoke();
	}

	public void SetState(int s, bool showAni = false)
	{
		state = s;
		abnormalAniShowed = showAni;
		if (s == 6 && !abnormalAniShowed)
		{
			abnormalAniShowed = true;
			StartCoroutine(ShowAbnormal());
			return;
		}
		if (motou != null)
		{
			StopCoroutine(motou);
		}
		if (!IsPlaying)
		{
			Play();
		}
	}

	private IEnumerator ShowAbnormal()
	{
		for (int i = 0; i < abnormal_ani.Count; i++)
		{
			ImageSource.sprite = abnormal_ani[i];
			if (i == abnormal_ani.Count - 3)
			{
				yield return new WaitForSeconds(1.3f);
			}
			else if (i == abnormal_ani.Count - 2)
			{
				yield return new WaitForSeconds(0.05f);
			}
			else
			{
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield return new WaitForSeconds(1.5f);
		if (!IsPlaying)
		{
			Play();
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		if (AutoPlay)
		{
			Play();
		}
		else
		{
			IsPlaying = false;
		}
		motou = Motou();
	}

	private void SetSprite(int idx)
	{
		switch (state)
		{
		case 0:
			ImageSource.sprite = normal_frames[idx];
			break;
		case 1:
			ImageSource.sprite = speak_frames[idx];
			break;
		case 2:
			ImageSource.sprite = hate_frames[idx];
			break;
		case 3:
			ImageSource.sprite = angry_frames[idx];
			break;
		case 4:
			ImageSource.sprite = happy_frames[idx];
			break;
		case 5:
			ImageSource.sprite = contempt_frames[idx];
			break;
		case 6:
			ImageSource.sprite = abnormal_frames[idx];
			break;
		case 7:
			ImageSource.sprite = wake_frames[idx];
			break;
		default:
			ImageSource.sprite = normal_frames[idx];
			break;
		}
	}

	public void Play()
	{
		IsPlaying = true;
		Foward = true;
	}

	public void PlayReverse()
	{
		IsPlaying = true;
		Foward = false;
	}

	private void Update()
	{
		if (!IsPlaying || FrameCount == 0)
		{
			return;
		}
		mDelta += Time.deltaTime;
		if (!(mDelta > 1f / FPS))
		{
			return;
		}
		mDelta = 0f;
		if (Foward)
		{
			mCurFrame++;
		}
		else
		{
			mCurFrame--;
		}
		if (mCurFrame >= FrameCount)
		{
			if (!Loop)
			{
				IsPlaying = false;
				return;
			}
			if (delay > 0)
			{
				IsPlaying = false;
				count_down = delay;
				InvokeRepeating("Delay", 0f, 0.1f);
			}
			else
			{
				IsPlaying = true;
			}
			mCurFrame = 0;
		}
		else if (mCurFrame < 0)
		{
			if (!Loop)
			{
				IsPlaying = false;
				return;
			}
			mCurFrame = FrameCount - 1;
		}
		SetSprite(mCurFrame);
	}

	public void Pause()
	{
		IsPlaying = false;
	}

	public void Resume()
	{
		if (!IsPlaying)
		{
			IsPlaying = true;
		}
	}

	public void Stop()
	{
		if (state == 7)
		{
			StartCoroutine(motou);
		}
		else
		{
			mCurFrame = 0;
			SetSprite(mCurFrame);
		}
		IsPlaying = false;
	}

	public void Rewind()
	{
		mCurFrame = 0;
		SetSprite(mCurFrame);
		Play();
	}

	private IEnumerator Motou()
	{
		while (true)
		{
			for (int i = 0; i < wake_ani.Count; i++)
			{
				ImageSource.sprite = wake_ani[i];
				yield return new WaitForSeconds(0.2f);
			}
		}
	}
}
