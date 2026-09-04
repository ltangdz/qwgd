using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class CloseLayer : MonoBehaviour
	{
		public Button button;

		private void Start()
		{
			button.onClick.AddListener(delegate
			{
				Object.Destroy(base.gameObject);
			});
		}
	}
}
