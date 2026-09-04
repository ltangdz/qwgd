using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _DLC8
{
	public class ConfirmDialog : MonoBehaviour
	{
		public Button sureButton;

		public Button cancelButton;

		public CanvasGroup canvasGroup;

		public Animator animator;

		public UnityAction _sureCallback;

		private void Start()
		{
			sureButton.onClick.AddListener(Sure);
			cancelButton.onClick.AddListener(Cancel);
		}

		private void Cancel()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			Hide();
		}

		private void Sure()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			_sureCallback?.Invoke();
		}

		public void Show(UnityAction sureCallback)
		{
			_sureCallback = sureCallback;
			base.gameObject.SetActive(value: true);
			animator.Play("Exit Panel In");
		}

		public void Hide()
		{
			animator.Play("Exit Panel Out");
			canvasGroup.DOFade(0f, 1f).OnComplete(delegate
			{
				base.gameObject.SetActive(value: false);
			});
		}
	}
}
