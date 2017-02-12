Shader "Custom/Tiling" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Scale("Scale", float) = 0.5

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float3 worldPos;
			float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
		float _Scale;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			float2 UV;
			float4 c;

			if (abs(IN.worldNormal.x) > 0.5)
				UV = IN.worldPos.zy;
			else if (abs(IN.worldNormal.z) > 0.5)
				UV = IN.worldPos.xy;
			else
				UV = IN.worldPos.xz;

			c = tex2D(_MainTex, UV * _Scale);
			o.Albedo = c.rgb * _Color;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
