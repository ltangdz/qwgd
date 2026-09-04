using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class Question5Answer : DragBagGrid<string>, IPointerClickHandler, IEventSystemHandler
	{
		public Text contentText;

		public Image frameImage;

		public Color[] colors;

		public Sprite[] sprites;

		[Header("0xu区  1填题区")]
		public QuestionType questionType;

		private bool _isUsed;

		private bool _canDrag = true;

		private bool _isAnimation;

		public void Cancel()
		{
			_isUsed = false;
			ResetUI(0);
		}

		public void Used()
		{
			_isUsed = true;
			ResetUI(0);
		}

		public void Init(string key)
		{
			base.DataItem = key;
			ResetUI(0);
		}

		public void PlayAnimation(bool isSuccess)
		{
			_canDrag = false;
			_isAnimation = true;
			if (questionType != QuestionType.QUESTION)
			{
				ResetUI(isSuccess ? 1 : 2);
			}
		}

		private void ClearAnswer()
		{
			ReasoningManager.Instance.RemoveAnswer(base.DataItem);
			base.DataItem = "";
			ResetUI(0);
		}

		public void InitData(string key)
		{
			ReasoningManager.Instance.RemoveAnswer(base.DataItem);
			base.DataItem = key;
			ResetUI(0);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!string.IsNullOrEmpty(base.DataItem) && questionType != QuestionType.QUESTION)
			{
				ReasoningManager.Instance.RemoveAnswer(base.DataItem);
				base.DataItem = "";
				ResetUI(0);
			}
		}

		private void ResetUI(int status)
		{
			Color color = colors[status];
			Sprite sprite = sprites[status];
			contentText.color = color;
			frameImage.sprite = sprite;
			contentText.text = (string.IsNullOrEmpty(base.DataItem) ? "" : I18N.instance.getValue(base.DataItem));
			if (status == 0)
			{
				contentText.DOFade(_isUsed ? 0.3f : 1f, 0f);
				return;
			}
			bool isSuccess = status == 1;
			contentText.DOFade(1f, 0f);
			ImageAnimation(isSuccess, base.DataItem != "", frameImage, sprites, delegate
			{
				if (!isSuccess && questionType == QuestionType.ANSWER)
				{
					ReasoningManager.Instance.RemoveAnswer(base.DataItem);
					base.DataItem = "";
					ReasoningManager.Instance.NoticeReset();
				}
			});
			TextAnimation(status == 1, base.DataItem != "", contentText, colors, null);
		}

		protected override void InitUI()
		{
		}

		protected override void StartDrag()
		{
			ResetUI(0);
		}

		protected override void EndDrag()
		{
		}

		protected override bool CanDrag()
		{
			if (questionType == QuestionType.ANSWER || _isUsed || !_canDrag || _isAnimation)
			{
				return false;
			}
			Debug.Log("CanDrag222");
			return true;
		}

		private void Awake()
		{
			_groupKey = "Question5";
			if (questionType == QuestionType.QUESTION)
			{
				ReasoningManager.Instance.onRemoveAnswer += RemoveAnswer;
			}
			ReasoningManager.Instance.onNoticeReset += NoticeReset;
		}

		private void OnDestroy()
		{
			if (questionType == QuestionType.QUESTION)
			{
				ReasoningManager.Instance.onRemoveAnswer -= RemoveAnswer;
			}
			ReasoningManager.Instance.onNoticeReset -= NoticeReset;
		}

		private void NoticeReset()
		{
			_canDrag = true;
			_isUsed = false;
			_isAnimation = false;
			ResetUI(0);
		}

		private void RemoveAnswer(string obj)
		{
			if (obj == base.DataItem)
			{
				_isUsed = false;
				ResetUI(0);
			}
		}

		protected void ImageAnimation(bool isSuccess, bool isSelected, Image image, Sprite[] list, UnityAction callback)
		{
			if (isSuccess)
			{
				if (isSelected)
				{
					image.sprite = list[1];
				}
				return;
			}
			image.sprite = list[2];
			Sequence sequence = DOTween.Sequence();
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(image.DOFade(1f, 0.5f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				image.sprite = list[0];
				if (callback != null)
				{
					callback();
				}
			});
			sequence.Play();
		}

		protected void TextAnimation(bool isSuccess, bool isSelected, Text text, Color[] list, UnityAction callback)
		{
			if (isSuccess)
			{
				if (isSelected)
				{
					text.color = list[1];
				}
				return;
			}
			text.color = list[2];
			Sequence sequence = DOTween.Sequence();
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(text.DOFade(1f, 0.5f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				text.color = list[0];
				if (callback != null)
				{
					callback();
				}
			});
			sequence.Play();
		}
	}
}
