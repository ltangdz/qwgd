using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class TooltipManager : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		[Header("SETTINGS")]
		public Text tooltipTxtObj;

		public string tooltipText;

		private Animator tooltipAnimator;

		private void Start()
		{
			tooltipAnimator = GetComponent<Animator>();
			tooltipTxtObj.text = tooltipText;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			tooltipAnimator.Play("Tooltip In");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			tooltipAnimator.Play("Tooltip Out");
		}
	}
}
