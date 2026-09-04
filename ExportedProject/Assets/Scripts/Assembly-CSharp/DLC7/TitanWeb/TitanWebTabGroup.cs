using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.TitanWeb
{
	public class TitanWebTabGroup : MonoBehaviour
	{
		private List<Button> _buttons;

		private TitanWebController _webController;

		private void Start()
		{
			_webController = GetComponentInParent<TitanWebController>();
			_buttons = GetComponentsInChildren<Button>().ToList();
			for (int i = 0; i < _buttons.Count; i++)
			{
				int j = i;
				_buttons[i].onClick.AddListener(delegate
				{
					Click(j);
				});
			}
		}

		private void Click(int index)
		{
			_webController.ShowTab(index);
			for (int i = 0; i < _webController.panelList.Count; i++)
			{
				_webController.panelList[i].SetActive(index == i);
			}
		}
	}
}
