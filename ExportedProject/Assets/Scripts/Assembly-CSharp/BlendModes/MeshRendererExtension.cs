using UnityEngine;

namespace BlendModes
{
	[ExtendedComponent(typeof(MeshRenderer))]
	public class MeshRendererExtension : RendererExtension<MeshRenderer>
	{
		private static ShaderProperty[] cachedDefaultProperties;

		public override string[] GetSupportedShaderFamilies()
		{
			return new string[2] { "UnlitTransparent", "DiffuseTransparent" };
		}

		public override ShaderProperty[] GetDefaultShaderProperties()
		{
			object obj = cachedDefaultProperties;
			if (obj == null)
			{
				obj = new ShaderProperty[2]
				{
					new ShaderProperty("_MainTex", ShaderPropertyType.Texture, Texture2D.whiteTexture),
					new ShaderProperty("_Color", ShaderPropertyType.Color, Color.white)
				};
				cachedDefaultProperties = (ShaderProperty[])obj;
			}
			return (ShaderProperty[])obj;
		}

		protected override string GetDefaultShaderName()
		{
			return "Legacy Shaders/Diffuse";
		}
	}
}
