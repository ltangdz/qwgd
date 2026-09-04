using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class CustomInputFieldDLC8 : MonoBehaviour
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

		public CanvasGroup tipGroup;

		public Text warningText;

		public bool isShowTip;

		private bool isFocused;

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
			}
			inputText.onValueChanged.AddListener(ChangedValue);
		}

		private void TipAnimation(bool isShow)
		{
			tipGroup.DOFade(isShow ? 1 : 0, 0.2f).SetEase(Ease.Linear);
			tipGroup.GetComponent<RectTransform>().DOAnchorPosY(isShow ? 58 : 0, 0.2f).SetEase(Ease.Linear);
		}

		private void ChangedValue(string arg0)
		{
			Debug.Log(arg0);
			if (arg0.Length == 0)
			{
				inputFieldAnimator.Play(outAnim);
				TipAnimation(isShow: false);
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
			isShowTip = false;
			if (flag)
			{
				warningText.text = I18N.instance.getValue("^F8216D98-47B3-23A4-7122-9766EBD56327");
				isShowTip = true;
			}
			else
			{
				string[] word = DLCNameUtil.Instance.GetWord();
				for (int j = 0; j < word.Length; j++)
				{
					if (arg0.Contains(word[j]))
					{
						warningText.text = I18N.instance.getValue("^110008_common_93");
						isShowTip = true;
					}
				}
			}
			if (arg0.Length > 0)
			{
				inputFieldAnimator.Play(inAnim);
			}
			else
			{
				inputFieldAnimator.Play(outAnim);
			}
			TipAnimation(isShowTip);
		}

		private void Update()
		{
			if (inputText.isFocused != isFocused)
			{
				isFocused = inputText.isFocused;
				if (isFocused)
				{
					inputFieldAnimator.Play(inAnim);
				}
				else if (inputText.text.Length <= 0)
				{
					inputFieldAnimator.Play(outAnim);
				}
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
