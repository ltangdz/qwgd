using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class RotateLoader : MonoBehaviour
	{
		public float time = 1f;

		public float repeatRate = 1f;

		private void Start()
		{
			InvokeRepeating("LoaderRotate", time, repeatRate);
		}

		private void LoaderRotate()
		{
			GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -30f));
		}
	}
}
