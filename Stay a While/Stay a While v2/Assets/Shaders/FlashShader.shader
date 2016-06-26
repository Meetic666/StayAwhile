Shader "ImageEffects/FlashShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		m_Masks("Mask", 2D) = "white" {}
		m_LightEnabled ("Enabled", int) = 1.0
		m_LightRanges("Range", float) = 10.0

		m_MaskUVSize("Flash size", Range(0.0, 1.0)) = 0.1

		m_FlashIntensity("Flash Intensity", Range(0.0, 5.0)) = 2.0
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

			fixed m_LightPositionX;
			fixed m_LightPositionY;

			fixed m_MaskUVSize;
			float m_FlashIntensity;

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
					float2 maskUv = float2(i.uv.x, i.uv.y);

					float2 distanceUv = float2(maskUv)-m_LightPositions;
					distanceUv.y *= _ScreenParams.y / _ScreenParams.x;

					if (abs(distanceUv.x) <= m_MaskUVSize
						&& abs(distanceUv.y) <= m_MaskUVSize)
					{
						maskUv = ((distanceUv / m_MaskUVSize) + float2(1.0, 1.0)) / 2.0;

						float lightDistance = distance(i.m_WorldPosition.xyz, m_LightPositions);

						fixed4 maskAmount = tex2D(m_Masks, maskUv);

						fixed4 maskThreshold = fixed4(1.0, 1.0, 1.0, 1.0) * (1.0 - (length(distanceUv) / m_LightRanges));

						maskThreshold = clamp(maskThreshold, 0.0, 1.0);

						maskAmount = maskAmount - maskThreshold;

						maskAmount = clamp(maskAmount, 0.0, 1.0);

						lightMask += maskAmount * m_FlashIntensity;
					}
				}

				return col + lightMask;
			}
			ENDCG
		}
	}
}
