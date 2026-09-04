using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class CustomInputField : MonoBehaviour
	{
		[Header("ANIMATORS")]
		public Animator inputFieldAnimator;

		[Header("OBJECTS")]
		public GameObject fieldTrigger;

		public InputField inputText;

		private bool isEmpty = true;

		private bool isClicked;

		private string inAnim = "In";

		private string outAnim = "Out";

		public Animator tip01;

		private bool aaa;

		public bool isNewUser;

		public Text warningText;

		private void Start()
		{
			if (inputText.text.Length <= 0)
			{
				isEmpty = true;
			}
			else
			{
				isEmpty = false;
			}
			if (!isEmpty)
			{
				inputFieldAnimator.Play(inAnim);
				tip01.Play("ani_createusertip");
			}
			inputText.onValueChanged.AddListener(ChangedValue);
		}

		private void ChangedValue(string arg0)
		{
			if (arg0.Length == 0)
			{
				return;
			}
			string[] array = new string[7] { "\\", "/", "*", "\"", "<", ">", "|" };
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (arg0.Contains(array[i]))
				{
					flag = true;
				}
			}
			warningText.text = I18N.instance.getValue(flag ? "^F8216D98-47B3-23A4-7122-9766EBD56327" : "^tips01");
		}

		private void Update()
		{
			if (inputText.text.Length == 1 || inputText.text.Length >= 1)
			{
				isEmpty = false;
				inputFieldAnimator.Play(inAnim);
				tip01.Play("ani_createusertip");
			}
			else if (!inputText.isFocused && inputText.text.Length == 0 && tip01.GetComponent<CanvasGroup>().alpha != 0f)
			{
				inputFieldAnimator.Play(outAnim);
				tip01.Play("ani_createusertip2");
			}
			else if (!isClicked)
			{
				inputFieldAnimator.Play(outAnim);
				inputFieldAnimator.GetComponent<InputField>().interactable = false;
				inputFieldAnimator.GetComponent<InputField>().interactable = true;
			}
		}

		public void Animate()
		{
			isClicked = true;
			inputFieldAnimator.Play(inAnim);
			fieldTrigger.SetActive(value: true);
		}

		public void FieldTrigger()
		{
			if (isEmpty && !inputText.isFocused)
			{
				inputFieldAnimator.Play(outAnim);
				fieldTrigger.SetActive(value: false);
				isClicked = false;
			}
			else if (!inputText.isFocused)
			{
				fieldTrigger.SetActive(value: false);
				isClicked = false;
			}
		}
	}
}
