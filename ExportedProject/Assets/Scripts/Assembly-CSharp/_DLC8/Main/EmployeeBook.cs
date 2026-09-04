using System.Collections.Generic;
using Aluba;
using Honeti;
using UnityEngine.UI;

namespace _DLC8.Main
{
	public class EmployeeBook : DialogAnimation
	{
		public List<EmployeeButton> buttons;

		public Text titleText;

		public Text contentText;

		public Button closeButton;

		public ScrollRect leftScroll;

		private string[] _contentStrings = new string[7] { "^110009_common_124", "^110009_common_129", "^110009_common_136", "^110009_common_141", "^110009_common_144", "^110009_common_147", "^110009_common_150" };

		private void Start()
		{
			ButtonSelected(0);
			closeButton.onClick.AddListener(Close);
			leftScroll.verticalNormalizedPosition = 0f;
		}

		private void Close()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLOSE_DIALOG);
			CloseAnimation();
		}

		public void Show()
		{
			ShowAnimation();
		}

		private void ButtonSelected(int index)
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				buttons[i].Init(this, i, isSelected: false);
			}
			buttons[index].SetSelected(isSelected: true);
			contentText.text = I18N.instance.getValue(_contentStrings[buttons[index].Index]);
		}

		public void ClickButton(EmployeeButton employeeButton)
		{
			ButtonSelected(employeeButton.Index);
			titleText.text = employeeButton.ClickText.text;
		}

		public override void CloseOver()
		{
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
