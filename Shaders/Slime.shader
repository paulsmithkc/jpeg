Shader "EndlessBoss/Slime" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 10
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DisplacementMagnitude ("Displacement Magnitude", Float) = 0.05
		_DisplacementVerticalPeriod ("Displacement Vertical Period", Float) = 10
		_DisplacementAnimationPeriod ("Displacement Animation Period", Float) = 1
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlinePower ("Outline Power", Float) = 3
		
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
			Name "FORWARD"
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting On
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
			uniform float4 _OutlineColor;
		    uniform float _OutlinePower;
			
			struct vertexInput {
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float4 uv : TEXCOORD0;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normal : TEXCOORD2;
			};
			
			vertexOutput vert(vertexInput i) {
				vertexOutput o;
				
				// Ripple the surface
				float3 displacement = _DisplacementMagnitude * (0.5 + 0.5 * sin(
					i.pos.y * _DisplacementVerticalPeriod + 
					_Time.w * _DisplacementAnimationPeriod
				)) * i.normal;
				i.pos.xyz += displacement;
				
				o.pos = mul(UNITY_MATRIX_MVP, i.pos);
				o.normal = normalize(mul(float4(i.normal, 0.0), _World2Object).xyz);
				o.uv = TRANSFORM_TEX(i.uv, _MainTex);
				o.posWorld = mul(_Object2World, i.pos);
				return o;
			}
			
			float4 frag(vertexOutput i) : COLOR {
			
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
				
				// Lighting
				float diffuseStrength = saturate(dot(i.normal, lightDirecton));
				float specularStrength = pow(saturate(dot(reflect(-lightDirecton, i.normal), viewDirection)), _Shininess);
				float3 diffuseColor = atten * diffuseStrength * float3(1, 1, 1);
				float3 specularColor = diffuseColor * _SpecColor.rgb * specularStrength;
				float3 totalLighting = diffuseColor + specularColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
				
				float4 lightcolor = float4( 
					saturate(totalLighting.r * _Color.r),
					saturate(totalLighting.g * _Color.g),
					saturate(totalLighting.b * _Color.b),
					saturate(0.8 + 0.2 * specularStrength)
				);
				
				// Create the outline
		        float3 normalView = normalize(mul((float3x3)UNITY_MATRIX_MVP, mul(i.normal, (float3x3)_Object2World)));
		        float outline = saturate(pow(length(normalView.xy), _OutlinePower));
		        lightcolor.rgb = lerp(lightcolor.rgb, _OutlineColor.rgb, outline);
		        
				return lightcolor * tex2D(_MainTex, i.uv);
			}
			
			ENDCG
		}
	}
	FallBack "Diffuse"
}
