using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class TopPanelButton : MonoBehaviour
	{
		private Animator buttonAnimator;

		private void Start()
		{
			buttonAnimator = GetComponent<Animator>();
		}

		public void HoverButton()
		{
			if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("TB Hover to Pressed"))
			{
				buttonAnimator.Play("TB Hover");
			}
		}

		public void NormalizeButton()
		{
			if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("TB Hover to Pressed"))
			{
				buttonAnimator.Play("TB Hover to Normal");
			}
		}
	}
}
