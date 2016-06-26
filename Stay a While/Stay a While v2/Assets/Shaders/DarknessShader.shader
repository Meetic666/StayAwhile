Shader "ImageEffects/DarknessShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		m_FireColor ("Fire color", Color) = (1.0, 1.0, 1.0, 1.0)
		m_FireIntensity ("Fire intensity", float) = 1.0

		m_MaskThreshold ("Mask threshold", Range(0.0, 1.0)) = 0.0

		m_MaskUVSize ("Mask UV size", float) = 1.0

		m_AmbientLight ("Ambient Light", Color) = (0.1, 0.1, 0.1, 1.0)
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
			float4 m_FireColor;
			float m_FireIntensity;
			fixed m_MaskThreshold;
			fixed m_MaskUVSize;
			fixed4 m_AmbientLight;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 lightMask = m_AmbientLight;

				//for (int j = 0; j < 10; j++)
				{
					float2 maskUv = float2(i.uv.x, 1.0 - i.uv.y);

					float2 distanceUv = float2(maskUv) - m_LightPositions;
					distanceUv.y *= _ScreenParams.y / _ScreenParams.x;

					if (abs(distanceUv.x) <= m_MaskUVSize
						&& abs(distanceUv.y) <= m_MaskUVSize)
					{
						maskUv = ((distanceUv / m_MaskUVSize) + float2(1.0, 1.0)) / 2.0;

						float maskAmount = tex2D(m_Masks, maskUv).x;

						if (maskAmount >= m_MaskThreshold)
						{
							maskAmount -= m_MaskThreshold;

							lightMask += maskAmount * m_FireColor * m_FireIntensity;
						}
					}
				}

				return col * lightMask;
			}
			ENDCG
		}
	}
}
