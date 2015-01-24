Shader "EndlessBoss/Vertex Lighting" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 10
	}
	SubShader {
		Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
		LOD 200
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			
			uniform float4 _LightColor0;
			
			struct vertexInput {
				float4 pos : POSITION;
				float3 normal : NORMAL;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};
			
			vertexOutput vert(vertexInput i) {
				vertexOutput o;
				
				float4 posWorld = mul(_Object2World, i.pos);
				float3 normalDirection = normalize(mul(float4(i.normal, 0.0), _World2Object).xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
				
				float3 lightDirecton;
				float3 atten;
				if (_WorldSpaceLightPos0.w == 0.0) {
					// Directional lights
					lightDirecton = normalize(_WorldSpaceLightPos0.xyz);
					atten = 1.0;
				} else {
					// Point lights
					lightDirecton = _WorldSpaceLightPos0.xyz - posWorld.xyz;
					atten = 1.0 / length(lightDirecton);
					lightDirecton = normalize(lightDirecton);
				}
				
				float diffuseStrength = saturate(dot(normalDirection, lightDirecton));
				float specularStrength = saturate(dot(reflect(-lightDirecton, lightDirecton), viewDirection));
				float3 diffuseColor = atten * diffuseStrength * _LightColor0.rgb;
				float3 specularColor = diffuseColor * _SpecColor.rgb * pow(specularStrength, _Shininess);
				float3 totalLighting = diffuseColor + specularColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
				
				o.pos = mul(UNITY_MATRIX_MVP, i.pos);
				o.col = float4(totalLighting, 1.0) * _Color;
				return o;
			}
			
			float4 frag(vertexOutput i) : COLOR {
				return i.col;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
