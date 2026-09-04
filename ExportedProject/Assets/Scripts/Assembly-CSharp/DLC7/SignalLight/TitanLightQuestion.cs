using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.SignalLight
{
	public class TitanLightQuestion : TitanLightBase
	{
		private bool _isLeft;

		[SerializeField]
		private int _number;

		public Image leftImage;

		public Image rightImage;

		public Image numberImage;

		public List<Sprite> numberSpriteList;

		private int _curStep;

		public void InitData(int number, bool canSeeNumber)
		{
			_number = number;
			_isLeft = Random.Range(0, 2) == 0;
			numberImage.sprite = numberSpriteList[canSeeNumber ? number : 4];
		}

		private void Show()
		{
			leftImage.DOFade(_isLeft ? 1 : 0, 0.5f);
			rightImage.DOFade((!_isLeft) ? 1 : 0, 0.5f);
			numberImage.DOFade(1f, 0.5f);
		}

		private void Hide()
		{
			leftImage.DOFade(0f, 0.5f);
			rightImage.DOFade(0f, 0.5f);
			numberImage.DOFade(0f, 0.5f);
		}

		public void Selected(bool isLeft)
		{
			base.EventManager.NoticeSelectedResult(_number, _isLeft == isLeft);
		}

		protected override void NoticeIdle(int step)
		{
			_curStep = step;
			Show();
		}

		protected override void NoticeStartGame()
		{
			Invoke("Hide", 0.5f);
		}

		protected override void NoticeSelectedResult(int step, bool isSuccess)
		{
		}

		protected override void NoticeSuccess(int step)
		{
			Invoke("InitData", 1f);
		}

		protected override void NoticeFail(int step)
		{
			InitData();
		}

		protected override void NoticeResetGame()
		{
			InitData();
		}

		private void InitData()
		{
			leftImage.DOFade(0f, 0f);
			rightImage.DOFade(0f, 0f);
			numberImage.DOFade(0f, 0f);
		}
	}
}
