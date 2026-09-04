using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class ListContentSizeFix : MonoBehaviour
	{
		public Scrollbar scrollbar;

		public bool isReversed;

		private void Start()
		{
			if (isReversed)
			{
				scrollbar.value = 1f;
			}
			else
			{
				scrollbar.value = 0f;
			}
		}

		public void FixListSize()
		{
			if (isReversed)
			{
				scrollbar.value = 1f;
			}
			else
			{
				scrollbar.value = 0f;
			}
		}
	}
}
