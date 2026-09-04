using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class UIElementInFront : MonoBehaviour
	{
		private void Start()
		{
			base.transform.SetAsFirstSibling();
		}
	}
}
