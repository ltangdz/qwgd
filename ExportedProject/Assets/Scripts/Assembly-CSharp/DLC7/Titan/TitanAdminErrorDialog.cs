using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanAdminErrorDialog : MonoBehaviour
	{
		public Button _button;

		public InputField _inputField;

		public Text tipText;

		public Transform content;

		public Button closeButton;

		private bool isAnimation;

		private void Start()
		{
			tipText.DOFade(0f, 0f);
			_button.onClick.AddListener(Error);
			closeButton.onClick.AddListener(delegate
			{
				base.transform.gameObject.SetActive(value: false);
			});
		}

		private void Error()
		{
			_inputField.text = "";
			_inputField.interactable = false;
			_button.interactable = false;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(tipText.DOFade(1f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(0.5f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(1f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(0.5f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(1f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(0.5f, 0.2f).SetEase(Ease.Linear));
			sequence.Append(tipText.DOFade(0f, 0f).OnComplete(delegate
			{
				_inputField.interactable = true;
				_button.interactable = true;
			}));
			sequence.Play();
		}
	}
}
