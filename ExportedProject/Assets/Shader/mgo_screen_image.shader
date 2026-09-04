Shader "mgo/screen_image" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[Space(100)] [Toggle(_ENABLE_SIGNAL)] _ENABLE_SIGNAL ("EnableSignal", Float) = 0
		[Space(20)] [NoScaleOffset] _DragTex ("DragTex", 2D) = "white" {}
		_DragInterval ("DragInterval", Range(1, 5)) = 2
		_DragStrength ("DragStrength", Range(-0.08, 0.08)) = 0.05
		[Space(20)] _MainColor ("MainColor", Vector) = (0.5725,0.7764,1,1)
		[Space(20)] _StripeColor ("StripeColor", Vector) = (0.8,0.8,0.8,1)
		_StripeWidth ("StripeWidth", Range(1, 10)) = 4
		[Space(20)] [NoScaleOffset] _FlowLightTex ("FlowLightTex", 2D) = "white" {}
		_FlowLightSpeed ("FlowLightSpeed", Range(0.001, 5)) = 1
		_SignalNoise ("SignalNoise", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_MatrixMVP;

			struct Vertex_Stage_Input
			{
				float3 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixMVP, float4(input.pos, 1.0));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;
			float4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, float2(input.uv.x, input.uv.y)) * _Color;
			}

			ENDHLSL
		}
	}
}