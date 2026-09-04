using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class ListTabsButton : MonoBehaviour
	{
		private Animator buttonAnimator;

		private void Start()
		{
			buttonAnimator = GetComponent<Animator>();
		}

		public void HoverButton()
		{
			if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("PLB Pressed"))
			{
				buttonAnimator.Play("PLB Hover");
			}
		}

		public void NormalizeButton()
		{
			if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("PLB Pressed"))
			{
				buttonAnimator.Play("PLB Normal");
			}
		}
	}
}
