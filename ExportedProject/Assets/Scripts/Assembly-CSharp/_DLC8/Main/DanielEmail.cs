using Aluba;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _DLC8.Main
{
	public class DanielEmail : DialogAnimation
	{
		public Button closeButton;

		public UnityAction closeCallback;

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
		}

		private void Close()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLOSE_DIALOG);
			CloseAnimation();
		}

		public override void CloseOver()
		{
			closeCallback?.Invoke();
		}

		public override void WillClose()
		{
		}

		public override void ShowOver()
		{
		}

		public override void WillShow()
		{
		}
	}
}
