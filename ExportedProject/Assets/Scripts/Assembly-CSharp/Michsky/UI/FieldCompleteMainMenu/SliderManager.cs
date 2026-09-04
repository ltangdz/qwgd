using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class SliderManager : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		[Header("TEXTS")]
		public Text valueText;

		public Text mainValueText;

		[Header("SETTINGS")]
		public bool showValue = true;

		public bool showMainValue = true;

		public bool useRoundValue;

		private Slider mainSlider;

		private Animator sliderAnimator;

		private void Start()
		{
			mainSlider = GetComponent<Slider>();
			sliderAnimator = GetComponent<Animator>();
			if (!showValue)
			{
				Object.Destroy(valueText);
			}
		}

		private void Update()
		{
			if (useRoundValue)
			{
				valueText.text = Mathf.Round(mainSlider.value * 1f).ToString();
				mainValueText.text = Mathf.Round(mainSlider.value * 1f).ToString();
			}
			else
			{
				valueText.text = mainSlider.value.ToString("F1");
				mainValueText.text = mainSlider.value.ToString("F1");
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			sliderAnimator.Play("Value In");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			sliderAnimator.Play("Value Out");
		}
	}
}
