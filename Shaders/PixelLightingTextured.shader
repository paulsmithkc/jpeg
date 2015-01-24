Shader "EndlessBoss/Pixel Lighting Textured" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Gloss ("Gloss", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
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
			uniform sampler2D _MainTex;
			uniform sampler2D _BumpMap;
			uniform sampler2D _Gloss;
			uniform sampler2D _Ramp;
			uniform float4 _MainTex_ST;
            uniform float4 _BumpMap_ST;
            uniform float4 _Gloss_ST;
			uniform float _Shininess;
			
			uniform float4 _LightColor0;
			
			struct vertexInput {
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float4 uv : TEXCOORD0;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float4 posWorld : TEXCOORD0;
				float2 uv  : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float2 uv3 : TEXCOORD3;
			};
			
			vertexOutput vert(vertexInput i) {
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, i.pos);
				o.normal = normalize(mul(float4(i.normal, 0.0), _World2Object).xyz);
				o.posWorld = mul(_Object2World, i.pos);
				o.uv = TRANSFORM_TEX(i.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(i.uv, _BumpMap);
				o.uv3 = TRANSFORM_TEX(i.uv, _Gloss);
				return o;
			}
			
			float4 frag(vertexOutput i) : COLOR {
				float3 normalDirection = i.normal; 
				//normalDirection = UnpackNormal(tex2D(_BumpMap, i.uv2));
				//normalDirection = normalize(mul(float4(normalDirection, 0.0), _World2Object).xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				
				float3 lightDirecton;
				float3 atten;
				if (_WorldSpaceLightPos0.w == 0.0) {
					// Directional lights
					lightDirecton = normalize(_WorldSpaceLightPos0.xyz);
					atten = 1.0;
				} else {
					// Point lights
					lightDirecton = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					atten = 1.0 / length(lightDirecton);
					lightDirecton = normalize(lightDirecton);
				}
				
				float diffuseStrength = saturate(dot(normalDirection, lightDirecton));
				float specularStrength = saturate(dot(reflect(-lightDirecton, normalDirection), viewDirection));
				float ramp = tex2D(_Ramp, float2(diffuseStrength * 0.5 + 0.5, 0.5)).r;
				float3 diffuseColor = atten * ramp	 * _LightColor0.rgb;
				float3 specularColor = atten * diffuseStrength * tex2D(_Gloss, i.uv3) * pow(specularStrength, _Shininess);
				float3 totalLighting = diffuseColor + specularColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
				
				return float4(totalLighting, 1.0) * tex2D(_MainTex, i.uv);
			}
			
			ENDCG
		}
	} 
	FallBack "EndlessBoss/Vertex Lighting"
}
