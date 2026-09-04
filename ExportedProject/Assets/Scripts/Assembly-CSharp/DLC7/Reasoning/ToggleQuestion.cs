using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class ToggleQuestion : ReasonBase
	{
		public List<ToggleItem> toggleItems;

		public List<Sprite> toggleBgList;

		public List<Sprite> toggleCheckList;

		public List<Color> toggleColorList;

		public List<int> answer;

		private bool _isOk;

		private UnityAction _callback;

		public void Ok(UnityAction callback)
		{
			if (_isOk)
			{
				return;
			}
			_callback = callback;
			List<int> list = new List<int>();
			for (int i = 0; i < toggleItems.Count; i++)
			{
				ToggleItem toggleItem = toggleItems[i];
				bool isOn = toggleItem.toggle.isOn;
				toggleItem.toggle.interactable = false;
				if (isOn)
				{
					list.Add(i);
				}
			}
			_isOk = AlubaTools.ListEquals(list, answer);
			for (int j = 0; j < toggleItems.Count; j++)
			{
				ToggleItem toggleItem2 = toggleItems[j];
				Text text = toggleItem2.text;
				Toggle toggle = toggleItem2.toggle;
				Image backgroundImage = toggleItem2.backgroundImage;
				Image checkImage = toggleItem2.checkImage;
				if (toggleItem2.toggle.isOn && checkImage != null)
				{
					ImageAnimation(_isOk, isSelected: true, checkImage, toggleCheckList, delegate
					{
						toggle.interactable = true;
					});
				}
				if (text != null)
				{
					TextAnimation(_isOk, toggleItem2.toggle.isOn, text, toggleColorList, delegate
					{
						toggle.interactable = true;
					});
				}
				if (backgroundImage != null)
				{
					ImageAnimation(_isOk, toggleItem2.toggle.isOn, backgroundImage, toggleBgList, delegate
					{
						toggle.interactable = true;
					});
				}
			}
			if (_isOk)
			{
				Invoke("Next", 0.1f);
			}
		}

		private void Next()
		{
			Debug.Log("Ok");
			if (_callback != null)
			{
				_callback();
			}
		}
	}
}
