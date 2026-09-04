using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.SignalLight
{
	public class LightDLC7 : TitanLightBase
	{
		public Image lightImage;

		public List<Color> colorList;

		public int number;

		private WaitForSeconds _waitForSeconds;

		public List<AudioClip> audioClips;

		private int _curStep;

		private bool _isFail;

		private GameManager _gameManager;

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		private void Start()
		{
			_waitForSeconds = new WaitForSeconds(0.5f);
			ResetData();
		}

		public void Idle()
		{
			_isFail = false;
			StartCoroutine("Flicker");
		}

		private IEnumerator Flicker()
		{
			lightImage.DOFade(1f, 0f);
			for (int i = 0; i < 3; i++)
			{
				if (_isFail && number == 0)
				{
					GameManager.soundManager.PlayAudioClip(audioClips[1]);
				}
				lightImage.DOFade(0f, 0.5f).SetEase(Ease.Linear);
				yield return _waitForSeconds;
				lightImage.DOFade(1f, 0.5f).SetEase(Ease.Linear);
				yield return _waitForSeconds;
			}
			yield return _waitForSeconds;
			if (_isFail)
			{
				if (number == 0)
				{
					lightImage.color = colorList[0];
					base.EventManager.NoticeResetGame();
				}
				else
				{
					lightImage.DOFade(0f, 0f);
				}
			}
			else
			{
				base.EventManager.NoticeStartGame();
			}
		}

		public void ResetData()
		{
			if (number == 0)
			{
				lightImage.color = colorList[0];
				lightImage.DOFade(1f, 0f);
			}
			else
			{
				lightImage.DOFade(0f, 0f);
			}
		}

		protected override void NoticeIdle(int step)
		{
			_curStep = step;
			if (step == 0 && number == 0)
			{
				lightImage.color = colorList[0];
				lightImage.DOFade(1f, 0f);
			}
			else if (step == number)
			{
				lightImage.color = colorList[0];
				_isFail = false;
				StartCoroutine("Flicker");
			}
		}

		protected override void NoticeStartGame()
		{
			if (_curStep == number)
			{
				lightImage.color = colorList[0];
				lightImage.DOFade(1f, 0f);
			}
		}

		protected override void NoticeSuccess(int step)
		{
			if (step == number)
			{
				GameManager.soundManager.PlayAudioClip(audioClips[0]);
				lightImage.color = colorList[2];
				lightImage.DOFade(1f, 0f);
			}
		}

		protected override void NoticeFail(int step)
		{
			_isFail = true;
			lightImage.color = colorList[1];
			StartCoroutine("Flicker");
		}

		protected override void NoticeResetGame()
		{
			ResetData();
		}

		protected override void NoticeSelectedResult(int step, bool isSuccess)
		{
		}
	}
}
