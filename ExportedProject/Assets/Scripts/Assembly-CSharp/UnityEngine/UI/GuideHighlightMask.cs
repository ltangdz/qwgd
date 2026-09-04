using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	public class GuideHighlightMask : MaskableGraphic, ICanvasRaycastFilter
	{
		public RectTransform arrow;

		public Vector2 center = Vector2.zero;

		public Vector2 size = new Vector2(100f, 100f);

		public void DoUpdate()
		{
			if (((bool)arrow && center != arrow.anchoredPosition) || size != arrow.sizeDelta)
			{
				center = arrow.anchoredPosition;
				size = arrow.sizeDelta;
				SetAllDirty();
			}
		}

		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !RectTransformUtility.RectangleContainsScreenPoint(arrow, sp, eventCamera);
		}

		[Obsolete]
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			Vector4 vector = new Vector4((0f - base.rectTransform.pivot.x) * base.rectTransform.rect.width, (0f - base.rectTransform.pivot.y) * base.rectTransform.rect.height, (1f - base.rectTransform.pivot.x) * base.rectTransform.rect.width, (1f - base.rectTransform.pivot.y) * base.rectTransform.rect.height);
			Vector4 vector2 = new Vector4(center.x - size.x / 2f, center.y - size.y / 2f, center.x + size.x * 0.5f, center.y + size.y * 0.5f);
			vbo.Clear();
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.position = new Vector2(vector.x, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector.x, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector2.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector2.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector.z, vector.w);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector.z, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.x, vector2.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector2.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
			simpleVert.position = new Vector2(vector2.z, vector.y);
			simpleVert.color = color;
			vbo.Add(simpleVert);
		}

		private void Update()
		{
			DoUpdate();
		}
	}
}
