using System.Collections.Generic;
using DG.Tweening;
using DLC7.Reasoning;
using DLC7.Reasoning._4015;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class Reason4015Middle : ReasoningMiddle
	{
		public List<GameObject> questionGroup;

		public Button nextButton;

		public RotateCardGroup question3RotateCardGroup;

		private int _index;

		private GameObject _curObj;

		private bool _canClick = true;

		private void Start()
		{
			nextButton.onClick.AddListener(ClickNext);
		}

		public override bool IsAllRight()
		{
			return false;
		}

		private void ClickNext()
		{
			_canClick = false;
			_curObj = questionGroup[_index];
			if (_curObj.name == "Question1" || _curObj.name == "Question2" || _curObj.name == "Question4")
			{
				_curObj.GetComponent<ToggleQuestion>().Ok(delegate
				{
					Debug.Log("OK");
					_curObj.GetComponent<RectTransform>().DOAnchorPosY(3000f, 0.5f).SetEase(Ease.Linear);
					_index++;
					_curObj = questionGroup[_index];
					_curObj.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f).SetEase(Ease.Linear)
						.OnComplete(delegate
						{
							_canClick = true;
						});
				});
			}
			else if (_curObj.name == "Question5")
			{
				_curObj.GetComponent<Reason4015Step5>().Ok(delegate
				{
					DLC7.Reasoning.ReasoningManager.Instance.NoticeResult("4015");
				});
			}
			else
			{
				if (!(_curObj.name == "Question3"))
				{
					return;
				}
				question3RotateCardGroup.CheckRight(delegate
				{
					_curObj.GetComponent<RectTransform>().DOAnchorPosY(3000f, 0.5f).SetEase(Ease.Linear);
					_index++;
					_curObj = questionGroup[_index];
					_curObj.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f).SetEase(Ease.Linear)
						.OnComplete(delegate
						{
							_canClick = true;
						});
				});
			}
		}

		private void Ok()
		{
		}
	}
}
