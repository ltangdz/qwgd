using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class FrameAnimation2D : MonoBehaviour
	{
		public List<Sprite> frameSprites;

		public Image image;

		public float interval;

		public float[] intervalArray;

		public int times = -1;

		public string animationKeyStr = "";

		public float delayTime;

		private int _curFrame;

		public bool isNativeSize = true;

		public bool isAutoPlay;

		public bool isFinishedHide = true;

		private Material _imageMaterial;

		private string _animationGuidKey = "";

		private void Start()
		{
			if (isAutoPlay)
			{
				Play();
			}
			if (image == null)
			{
				image = GetComponent<Image>();
			}
			_imageMaterial = image.material;
		}

		public void Play()
		{
			image.DOFade(1f, 0f);
			StopCoroutine("FrameAnimationCoroutine");
			StartCoroutine("FrameAnimationCoroutine");
		}

		public void Play(string guid)
		{
			_animationGuidKey = $"{guid}{animationKeyStr}";
			image.DOFade(1f, 0f);
			StartCoroutine("FrameAnimationCoroutine");
		}

		public void Stop()
		{
			StopCoroutine("FrameAnimationCoroutine");
			base.gameObject.SetActive(value: false);
		}

		private IEnumerator FrameAnimationCoroutine()
		{
			if (interval < 0f && intervalArray.Length < frameSprites.Count)
			{
				yield return new WaitForSeconds(0f);
				yield break;
			}
			yield return new WaitForSeconds(delayTime);
			int curTimes = 0;
			while (times < 0 || curTimes < times)
			{
				image.sprite = frameSprites[0];
				if (!string.IsNullOrEmpty(animationKeyStr))
				{
					FrameAnimationEvent.Instance.FrameFinished(string.IsNullOrEmpty(_animationGuidKey) ? animationKeyStr : _animationGuidKey, 0, frameSprites.Count);
				}
				for (int i = 1; i < frameSprites.Count; i++)
				{
					if (intervalArray.Length != 0 && intervalArray.Length >= frameSprites.Count)
					{
						interval = intervalArray[i];
					}
					yield return new WaitForSeconds(interval);
					image.sprite = frameSprites[i];
					image.material = _imageMaterial;
					if (isNativeSize)
					{
						image.SetNativeSize();
					}
					FrameAnimationEvent.Instance.FrameFinished(animationKeyStr, i, frameSprites.Count);
				}
				yield return new WaitForSeconds(interval);
				if (times >= 0)
				{
					curTimes++;
				}
			}
			if (isFinishedHide)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}
}
