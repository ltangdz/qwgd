using UnityEngine;

namespace Aluba.UI
{
	public class BackgroundAutoScale : MonoBehaviour
	{
		[ExecuteInEditMode]
		public Vector2 textureOriginSize = new Vector2(1920f, 1080f);

		private void Start()
		{
			Scaler();
		}

		private void Scaler()
		{
			Vector2 sizeDelta = base.gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
			float num = sizeDelta.x / sizeDelta.y;
			Vector2 vector = textureOriginSize;
			float num2 = vector.x / vector.y;
			RectTransform rectTransform = (RectTransform)base.transform;
			if (num2 > num)
			{
				int num3 = Mathf.CeilToInt(sizeDelta.y);
				int num4 = Mathf.CeilToInt((float)num3 / vector.y * vector.x);
				rectTransform.sizeDelta = new Vector2(num4, num3);
			}
			else
			{
				int num5 = Mathf.CeilToInt(sizeDelta.x);
				int num6 = Mathf.CeilToInt((float)num5 / vector.x * vector.y);
				rectTransform.sizeDelta = new Vector2(num5, num6);
			}
		}
	}
}
