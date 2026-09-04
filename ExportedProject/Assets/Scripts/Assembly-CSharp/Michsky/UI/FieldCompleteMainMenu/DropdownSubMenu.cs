using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class DropdownSubMenu : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerExitHandler
	{
		private Animator panelAnimator;

		private bool isOpen;

		private void Start()
		{
			panelAnimator = GetComponent<Animator>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!isOpen)
			{
				panelAnimator.Play("Expand");
				isOpen = true;
			}
			else if (isOpen)
			{
				panelAnimator.Play("Minimize");
				isOpen = false;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Loop"))
			{
				panelAnimator.Play("Minimize");
				isOpen = false;
			}
		}

		public void TriggerExit()
		{
			if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Loop"))
			{
				panelAnimator.Play("Minimize");
				isOpen = false;
			}
		}

		public void ButtonClick()
		{
			if (!isOpen)
			{
				panelAnimator.Play("Expand");
				isOpen = true;
			}
			else if (isOpen)
			{
				panelAnimator.Play("Minimize");
				isOpen = false;
			}
		}
	}
}
