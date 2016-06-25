Shader "ImageEffects/FlashShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D m_Masks;
			float3 m_LightPositions;
			float m_LightRanges;
			int m_LightEnabled;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 m_WorldPosition : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.m_WorldPosition = v.vertex;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 lightMask = fixed4(0.0, 0.0, 0.0, 0.0);

				if (m_LightEnabled != 0)
				{
					float2 maskUv = float2(i.uv.x, 1.0 - i.uv.y);

					float lightDistance = distance(i.m_WorldPosition.xyz, m_LightPositions);

					fixed4 maskAmount = tex2D(m_Masks, maskUv);

					fixed4 maskThreshold = fixed4(1.0, 1.0, 1.0, 1.0) * (1.0 - (lightDistance / m_LightRanges));

					maskThreshold = clamp(maskThreshold, 0.0, 1.0);

					maskAmount = maskAmount - maskThreshold;

					lightMask += maskAmount * 10;
				}

				return col * lightMask;
			}
			ENDCG
		}
	}
}
