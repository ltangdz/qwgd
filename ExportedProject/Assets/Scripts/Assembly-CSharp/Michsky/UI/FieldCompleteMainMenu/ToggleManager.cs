using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class ToggleManager : MonoBehaviour
	{
		[Header("TOGGLE")]
		public Toggle toggleObject;

		[Header("ANIMATORS")]
		public Animator toggleAnimator;

		private string toggleOn = "Toggle On";

		private string toggleOff = "Toggle Off";

		private void Start()
		{
			toggleObject.GetComponent<Toggle>();
			toggleObject.onValueChanged.AddListener(TaskOnClick);
			if (toggleObject.isOn)
			{
				toggleAnimator.Play(toggleOn);
			}
			else
			{
				toggleAnimator.Play(toggleOff);
			}
		}

		private void TaskOnClick(bool value)
		{
			if (toggleObject.isOn)
			{
				toggleAnimator.Play(toggleOn);
			}
			else
			{
				toggleAnimator.Play(toggleOff);
			}
		}
	}
}
