using UnityEngine;

namespace Aluba.UI.Text
{
	public class HorizontalScrollingText : MonoBehaviour
	{
		public float speed;

		public RectTransform maskRec;

		public RectTransform rec;

		[Header("文本如果没有超过mask是否滚动，true代表不滚动，false代表无论如何都会滚动")]
		public bool smallNotScrolling;

		private float localX;

		private float localY;

		private float localZ;

		private float txtWidth;

		private void OnEnable()
		{
			localY = base.transform.localPosition.y;
			localZ = base.transform.localPosition.z;
			rec.anchoredPosition = new Vector2(maskRec.rect.width, 0f);
		}

		private void Update()
		{
			if (speed == 0f)
			{
				return;
			}
			txtWidth = rec.rect.width;
			if (smallNotScrolling && maskRec.rect.width > txtWidth)
			{
				rec.anchoredPosition = Vector2.zero;
				return;
			}
			if (rec.anchoredPosition.x < 0f - txtWidth)
			{
				rec.anchoredPosition = new Vector2(maskRec.rect.width, 0f);
			}
			localX = base.transform.localPosition.x - speed * Time.deltaTime;
			base.transform.localPosition = new Vector3(localX, localY, localZ);
		}
	}
}
