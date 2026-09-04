using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExtraFoundation.Components
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Effects/UILineRenderer")]
	public class UILineRenderer2 : MaskableGraphic
	{
		public enum PositionType
		{
			Relative = 0,
			Absolute = 1
		}

		public enum JoinType
		{
			Bevel = 0,
			Miter = 1
		}

		[Serializable]
		private struct Point
		{
			[SerializeField]
			private Vector2 position;

			[SerializeField]
			private Transform target;

			[SerializeField]
			private bool isTarget;

			public Vector2 Position
			{
				get
				{
					return position;
				}
				set
				{
					position = value;
				}
			}

			public Transform Target
			{
				get
				{
					return target;
				}
				set
				{
					target = value;
				}
			}

			public bool IsTarget
			{
				get
				{
					return isTarget;
				}
				set
				{
					isTarget = value;
				}
			}
		}

		private enum SegmentType
		{
			Start = 0,
			Middle = 1,
			End = 2
		}

		private const float MinMiterJoin = (float)Math.PI / 12f;

		private const float MinBevelNiceJoin = 0f;

		[SerializeField]
		private Texture texture;

		[SerializeField]
		private Rect uvRect = new Rect(0f, 0f, 1f, 1f);

		[SerializeField]
		private float lineWidth = 2f;

		[SerializeField]
		private List<Point> points = new List<Point>();

		[SerializeField]
		private bool lineList;

		[SerializeField]
		private bool lineCaps;

		[SerializeField]
		private JoinType lineJoin;

		[SerializeField]
		private PositionType posType;

		private Vector3 cachePos;

		private static readonly Vector2 UVTopLeft = Vector2.zero;

		private static readonly Vector2 UVBottomLeft = new Vector2(0f, 1f);

		private static readonly Vector2 UVTopCenter = new Vector2(0.5f, 0f);

		private static readonly Vector2 UVBottomCenter = new Vector2(0.5f, 1f);

		private static readonly Vector2 UVTopRight = new Vector2(1f, 0f);

		private static readonly Vector2 UVBottomRight = new Vector2(1f, 1f);

		private static readonly Vector2[] StartUvs = new Vector2[4] { UVTopLeft, UVBottomLeft, UVBottomCenter, UVTopCenter };

		private static readonly Vector2[] MiddleUvs = new Vector2[4] { UVTopCenter, UVBottomCenter, UVBottomCenter, UVTopCenter };

		private static readonly Vector2[] EndUvs = new Vector2[4] { UVTopCenter, UVBottomCenter, UVBottomRight, UVTopRight };

		public Texture MainTexture
		{
			get
			{
				return texture;
			}
			set
			{
				texture = value;
			}
		}

		public float LineWidth
		{
			get
			{
				return lineWidth;
			}
			set
			{
				lineWidth = value;
			}
		}

		public PositionType Space
		{
			get
			{
				return posType;
			}
			set
			{
				posType = value;
			}
		}

		public bool LineList
		{
			get
			{
				return lineList;
			}
			set
			{
				lineList = value;
			}
		}

		public bool LineCaps
		{
			get
			{
				return lineCaps;
			}
			set
			{
				lineCaps = value;
			}
		}

		public JoinType LineJoin
		{
			get
			{
				return lineJoin;
			}
			set
			{
				lineJoin = value;
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (!(texture == null))
				{
					return texture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		public Texture Texture
		{
			get
			{
				return texture;
			}
			set
			{
				if (!(texture == value))
				{
					texture = value;
					SetVerticesDirty();
					SetMaterialDirty();
				}
			}
		}

		public Rect UVRect
		{
			get
			{
				return uvRect;
			}
			set
			{
				if (!(uvRect == value))
				{
					uvRect = value;
					SetVerticesDirty();
				}
			}
		}

		public void AddVector2Point(Vector2 pos)
		{
			Point item = new Point
			{
				Position = pos,
				IsTarget = false
			};
			points.Add(item);
		}

		public void AddTransformPoint(Transform trans)
		{
			Point item = new Point
			{
				Target = trans,
				IsTarget = true
			};
			points.Add(item);
		}

		public void InsertVector2PointAt(int index, Vector2 pos)
		{
			Point item = new Point
			{
				Position = pos,
				IsTarget = false
			};
			points.Insert(index, item);
		}

		public void RemovePointAt(int index)
		{
			if (index >= 0 && points.Count > index)
			{
				points.RemoveAt(index);
			}
		}

		public void ClearPoints()
		{
			points.Clear();
		}

		private void Update()
		{
			if (Application.isEditor && base.transform.position != cachePos)
			{
				cachePos = base.transform.position;
			}
		}

		private List<Vector2> GetTruePoins()
		{
			List<Vector2> list = new List<Vector2>();
			if (posType == PositionType.Absolute)
			{
				Vector2 vector = base.transform.position;
				int i = 0;
				for (int count = points.Count; i < count; i++)
				{
					Point point = points[i];
					if (point.IsTarget)
					{
						if ((bool)point.Target)
						{
							list.Add((Vector2)point.Target.position - vector);
						}
					}
					else
					{
						list.Add(point.Position - vector);
					}
				}
			}
			else
			{
				int j = 0;
				for (int count2 = points.Count; j < count2; j++)
				{
					Point point2 = points[j];
					if (point2.IsTarget)
					{
						if ((bool)point2.Target)
						{
							list.Add(point2.Target.position - base.transform.position);
						}
					}
					else
					{
						list.Add(point2.Position);
					}
				}
			}
			return list;
		}

		private List<UIVertex[]> LineListSegments(List<Vector2> points)
		{
			List<UIVertex[]> list = new List<UIVertex[]>();
			int i = 1;
			for (int count = points.Count; i < count; i += 2)
			{
				Vector2 start = points[i - 1];
				Vector2 end = points[i];
				if (lineCaps)
				{
					list.Add(CreateLineCap(start, end, SegmentType.Start));
				}
				list.Add(CreateLineSegment(start, end, SegmentType.Middle));
				if (lineCaps)
				{
					list.Add(CreateLineCap(start, end, SegmentType.End));
				}
			}
			return list;
		}

		private List<UIVertex[]> StraightLineSegments(List<Vector2> points)
		{
			List<UIVertex[]> list = new List<UIVertex[]>();
			int i = 1;
			for (int count = points.Count; i < count; i++)
			{
				Vector2 start = points[i - 1];
				Vector2 end = points[i];
				if (lineCaps && i == 1)
				{
					list.Add(CreateLineCap(start, end, SegmentType.Start));
				}
				list.Add(CreateLineSegment(start, end, SegmentType.Middle));
				if (lineCaps && i == count - 1)
				{
					list.Add(CreateLineCap(start, end, SegmentType.End));
				}
			}
			return list;
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (points == null)
			{
				return;
			}
			vh.Clear();
			List<Vector2> truePoins = GetTruePoins();
			List<UIVertex[]> list = null;
			list = ((!lineList) ? StraightLineSegments(truePoins) : LineListSegments(truePoins));
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				if (!lineList && i < list.Count - 1)
				{
					Vector3 vector = list[i][1].position - list[i][2].position;
					Vector3 vector2 = list[i + 1][2].position - list[i + 1][1].position;
					float num = Vector2.Angle(vector, vector2) * ((float)Math.PI / 180f);
					float num2 = Mathf.Sign(Vector3.Cross(vector.normalized, vector2.normalized).z);
					float num3 = lineWidth * 0.5f / Mathf.Tan(num * 0.5f);
					Vector3 vector3 = vector.normalized * num3 * num2;
					Vector3 position = list[i][2].position - vector3;
					Vector3 position2 = list[i][3].position + vector3;
					JoinType joinType = lineJoin;
					if (joinType == JoinType.Miter)
					{
						if (num3 < vector.magnitude * 0.5f && num3 < vector2.magnitude * 0.5f && num > (float)Math.PI / 12f)
						{
							list[i][2].position = position;
							list[i][3].position = position2;
							list[i + 1][0].position = position2;
							list[i + 1][1].position = position;
						}
						else
						{
							joinType = JoinType.Bevel;
						}
					}
					if (joinType == JoinType.Bevel)
					{
						if (num3 < vector.magnitude * 0.5f && num3 < vector2.magnitude * 0.5f && num > 0f)
						{
							if (num2 < 0f)
							{
								list[i][2].position = position;
								list[i + 1][1].position = position;
							}
							else
							{
								list[i][3].position = position2;
								list[i + 1][0].position = position2;
							}
						}
						UIVertex[] verts = new UIVertex[4]
						{
							list[i][2],
							list[i][3],
							list[i + 1][0],
							list[i + 1][1]
						};
						vh.AddUIVertexQuad(verts);
					}
				}
				vh.AddUIVertexQuad(list[i]);
			}
		}

		private UIVertex[] CreateLineCap(Vector2 start, Vector2 end, SegmentType type)
		{
			switch (type)
			{
			case SegmentType.Start:
			{
				Vector2 start2 = start - (end - start).normalized * lineWidth * 0.5f;
				return CreateLineSegment(start2, start, SegmentType.Start);
			}
			case SegmentType.End:
			{
				Vector2 end2 = end + (end - start).normalized * lineWidth * 0.5f;
				return CreateLineSegment(end, end2, SegmentType.End);
			}
			default:
				Debug.LogError("Bad SegmentType passed in to CreateLineCap. Must be SegmentType.Start or SegmentType.End");
				return null;
			}
		}

		private UIVertex[] CreateLineSegment(Vector2 start, Vector2 end, SegmentType type)
		{
			Vector2[] uvs = MiddleUvs;
			switch (type)
			{
			case SegmentType.Start:
				uvs = StartUvs;
				break;
			case SegmentType.End:
				uvs = EndUvs;
				break;
			}
			Vector2 vector = new Vector2(start.y - end.y, end.x - start.x).normalized * lineWidth * 0.5f;
			Vector2 vector2 = start - vector;
			Vector2 vector3 = start + vector;
			Vector2 vector4 = end + vector;
			Vector2 vector5 = end - vector;
			return SetVbo(new Vector2[4] { vector2, vector3, vector4, vector5 }, uvs);
		}

		protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = color;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				array[i] = simpleVert;
			}
			return array;
		}
	}
}
