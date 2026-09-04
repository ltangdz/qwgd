using UnityEngine;

namespace Anima2D
{
	public class SpriteMesh : ScriptableObject
	{
		public const int api_version = 4;

		[SerializeField]
		[HideInInspector]
		private int m_ApiVersion;

		[SerializeField]
		private Sprite m_Sprite;

		[SerializeField]
		private Mesh m_SharedMesh;

		[SerializeField]
		[HideInInspector]
		private Material[] m_SharedMaterials;

		public Sprite sprite
		{
			get
			{
				return m_Sprite;
			}
			private set
			{
				m_Sprite = value;
			}
		}

		public Mesh sharedMesh
		{
			get
			{
				return m_SharedMesh;
			}
			private set
			{
				m_SharedMesh = value;
			}
		}
	}
}
