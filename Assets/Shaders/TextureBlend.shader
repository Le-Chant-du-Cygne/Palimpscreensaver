Shader "Custom/Palimpseste Textures Blender" {
	Properties {
		_Color1 ("Background Color", Color) = (1,1,1,1)
		_Color2 ("Pattern 1 Color", Color) = (1,1,1,1)
		_Color3 ("Pattern 2 Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Illum ("Pattern 1 (A)", 2D) = "white" {}
		_Illum2 ("Pattern 2 (A)", 2D) = "white" {}
		_Blend ("Blend", Range (0, 1)) = 0.5
		_Display("Display", Range (0, 1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _Illum;
		sampler2D _Illum2;
		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;
		float _Blend;
		float _Display;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
			float2 uv_Illum2;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c1 = tex2D (_MainTex, IN.uv_MainTex);
			half mask = tex2D (_Illum, IN.uv_Illum).a;
			half mask2 = tex2D (_Illum2, IN.uv_Illum2).a;
			half3 fc = (c1 * (1 - _Color1.a) + (_Color1.rgb * _Color1.a)) * mask * mask2 + (c1 * (1 - _Color2.a) + (_Color2.rgb * _Color2.a)) * (1 - mask) + (c1 * (1 - _Color3.a) + (_Color3.rgb * _Color3.a)) * (1 - mask2);
			half3 fc2 = c1 + (_Color1.rgb * _Color1.a) * mask * mask2 + (_Color2.rgb * _Color2.a) * (1 - mask) + (_Color3.rgb * _Color3.a) * (1 - mask2); //+ (_Color3.rgb * _Color3.a) * mask2
			
			half3 final = lerp(fc2, fc, _Blend);
			o.Emission = lerp(half3(0, 0, 0), final, _Display);
			o.Alpha = 1.0;
		}
		ENDCG
	} 
	FallBack off
}
