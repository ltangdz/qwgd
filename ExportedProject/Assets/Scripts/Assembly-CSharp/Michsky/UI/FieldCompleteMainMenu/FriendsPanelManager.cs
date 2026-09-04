using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class FriendsPanelManager : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		private Animator panelAnimator;

		private CanvasGroup cg;

		private bool isOpen;

		public bool isMobile;

		private void Start()
		{
			panelAnimator = GetComponent<Animator>();
			cg = GetComponent<CanvasGroup>();
			if (isMobile)
			{
				cg.interactable = false;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			panelAnimator.Play("Friends Panel In");
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (isOpen)
			{
				panelAnimator.Play("Friends Panel Out");
				isOpen = false;
			}
			else
			{
				panelAnimator.Play("Friends Panel In");
				isOpen = true;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			panelAnimator.Play("Friends Panel Out");
		}
	}
}
