using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _DLC8.Main.Invade
{
	public class InvadeProgress : MonoBehaviour
	{
		public int index;

		[Header("进度图标")]
		public Image progressBg;

		public Image progressIcon;

		public Text progressText;

		[Header("进度点")]
		public List<Image> pointList;

		public Sprite[] pointSprites;

		private float _progress;

		private UnityAction<int> _callback;

		private bool _isPlayAnimation;

		public void ShowAnimation(UnityAction<int> callback)
		{
			_callback = callback;
			if (pointList.Count == 0)
			{
				Invoke("ShowProgressIcon", 0.5f);
				return;
			}
			_isPlayAnimation = true;
			StartCoroutine("ShowProgressPoint");
		}

		private void ShowProgressIcon()
		{
			DOTween.To(() => _progress, delegate(float x)
			{
				_progress = x;
			}, 1f, 1f).SetEase(Ease.Linear).OnUpdate(delegate
			{
				progressBg.fillAmount = _progress;
				progressIcon.fillAmount = _progress;
			})
				.OnComplete(delegate
				{
					progressText.color = Color.white;
					if (_callback != null)
					{
						_callback(index);
					}
				});
		}

		public void ProgressPointSuccess()
		{
			_isPlayAnimation = false;
			StopCoroutine("ShowProgressPoint");
			for (int i = 0; i < pointList.Count; i++)
			{
				pointList[i].sprite = pointSprites[1];
			}
		}

		private IEnumerator ShowProgressPoint()
		{
			yield return new WaitForSeconds(0.5f);
			while (_isPlayAnimation)
			{
				for (int i = 0; i < pointList.Count; i++)
				{
					pointList[i].sprite = pointSprites[1];
					yield return new WaitForSeconds(0.2f);
				}
				yield return new WaitForSeconds(0.3f);
				for (int j = 0; j < pointList.Count; j++)
				{
					pointList[j].sprite = pointSprites[0];
				}
				yield return new WaitForSeconds(0.3f);
			}
		}
	}
}
