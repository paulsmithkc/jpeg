Shader "EndlessBoss/Slime" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 10
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DisplacementMagnitude("Displacement Magnitude", Float) = 0.05
		_DisplacementVerticalPeriod("Displacement Vertical Period", Float) = 10
		_DisplacementAnimationPeriod("Displacement Animation Period", Float) = 1
		
		//_Thickness ("Thickness (R)", 2D) = "bump" {}
		//_Power ("Subsurface Power", Float) = 1.0
		//_Distortion ("Subsurface Distortion", Float) = 0.0
		//_Scale ("Subsurface Scale", Float) = 0.5
		//_SubColor ("Subsurface Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" "Queue" = "Transparent" }
		LOD 200
		
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			//Cull Off
			//ZWrite On
			//ZTest Always
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _DisplacementMagnitude;
			uniform float _DisplacementVerticalPeriod;
			uniform float _DisplacementAnimationPeriod;
			
			uniform float4 _LightColor0;
			
			struct vertexInput {
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float4 uv : TEXCOORD0;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 lightcolor : COLOR;
				float2 uv : TEXCOORD0;
			};
			
			vertexOutput vert(vertexInput i) {
				vertexOutput o;
				
				float3 normalDirection = normalize(mul(float4(i.normal, 0.0), _World2Object).xyz);
				float3 displacement = _DisplacementMagnitude * (0.5 + 0.5 * sin(
					i.pos.y * _DisplacementVerticalPeriod + 
					_Time.w * _DisplacementAnimationPeriod
				)) * i.normal;
				i.pos.xyz += displacement;
				
				float4 posWorld = mul(_Object2World, i.pos);
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
				float specularStrength = pow(saturate(dot(reflect(-lightDirecton, normalDirection), viewDirection)), _Shininess);
				
				float3 diffuseColor = atten * diffuseStrength * _LightColor0.rgb;
				float3 specularColor = diffuseColor * _SpecColor.rgb * specularStrength;
				float3 totalLighting = diffuseColor + specularColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
				
				o.pos = mul(UNITY_MATRIX_MVP, i.pos);
				o.lightcolor.rgb = totalLighting * _Color.rgb;
				o.lightcolor.a = clamp(0.8 + 0.2 * specularStrength, 0.8, 1.0);
				o.uv = TRANSFORM_TEX(i.uv, _MainTex);
				return o;
			}
			
			float4 frag(vertexOutput i) : COLOR {
				return i.lightcolor * tex2D(_MainTex, i.uv);
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
