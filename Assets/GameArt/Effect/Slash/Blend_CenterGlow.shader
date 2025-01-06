// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ERB/Particles/Blend_CenterGlow"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_MainTex("MainTex", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_SpeedMainTexUVNoiseZW("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_Emission("Emission", Float) = 2
		_Color("Color", Color) = (0.5,0.5,0.5,1)
		[ASEEnd]_Opacity("Opacity", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _SpeedMainTexUVNoiseZW;
				uniform sampler2D _Noise;
				uniform float4 _Noise_ST;
				uniform float4 _Color;
				uniform sampler2D _Mask;
				uniform float4 _Mask_ST;
				uniform float _Emission;
				uniform float _Opacity;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 appendResult21 = (float2(_SpeedMainTexUVNoiseZW.x , _SpeedMainTexUVNoiseZW.y));
					float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float2 panner107 = ( 1.0 * _Time.y * appendResult21 + uv_MainTex);
					float4 tex2DNode13 = tex2D( _MainTex, panner107 );
					float2 appendResult22 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
					float2 uv_Noise = i.texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;
					float2 panner108 = ( 1.0 * _Time.y * appendResult22 + uv_Noise);
					float4 tex2DNode14 = tex2D( _Noise, panner108 );
					float2 uv_Mask = i.texcoord.xy * _Mask_ST.xy + _Mask_ST.zw;
					float4 appendResult87 = (float4(( (( tex2DNode13 * tex2DNode14 * _Color * i.color * tex2D( _Mask, uv_Mask ) )).rgb * _Emission ) , ( tex2DNode13.a * tex2DNode14.a * _Color.a * i.color.a * _Opacity )));
					

					fixed4 col = appendResult87;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	
	
	
}
/*ASEBEGIN
Version=18900
-20;150;1920;668;2138.595;-21.12086;1.301085;True;False
Node;AmplifyShaderEditor.CommentaryNode;109;-3401.27,-330.4436;Inherit;False;1037.896;533.6285;Textures movement;7;107;108;29;21;89;22;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;15;-3351.27,-101.4007;Float;False;Property;_SpeedMainTexUVNoiseZW;Speed MainTex U/V + Noise Z/W;3;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,-10,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-2856.788,-280.4436;Inherit;False;0;13;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;21;-2778.501,-153.1786;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-2861.858,-55.04038;Inherit;False;0;14;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;22;-2766.722,70.18491;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;107;-2570.374,-239.5098;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;108;-2577.237,-21.63752;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;13;-1803.192,-66.2159;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;None;887b9e087423e754e838eeced56673a3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-1804.579,119.2214;Inherit;True;Property;_Noise;Noise;1;0;Create;True;0;0;0;False;0;False;-1;None;e199ee8112362864f831425463b09fda;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;32;-1732.769,525.192;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-1821.308,703.5277;Inherit;True;Property;_Mask;Mask;2;0;Create;True;0;0;0;False;0;False;-1;None;6b06ebda3feffed4685826b777e15ab6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;-1735.354,341.3382;Float;False;Property;_Color;Color;5;0;Create;True;0;0;0;False;0;False;0.5,0.5,0.5,1;1,0.3726415,0.9206789,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1135.791,-2.490838;Inherit;False;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-446.0907,153.7209;Float;False;Property;_Emission;Emission;4;0;Create;True;0;0;0;False;0;False;2;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-902.6371,854.3991;Float;False;Property;_Opacity;Opacity;6;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;78;-929.9557,1.479294;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-499.6634,669.4196;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-285.8404,56.42584;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;87;-123.9274,58.99411;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;68;48.80347,59.22049;Float;False;True;-1;2;;0;9;ERB/Particles/Blend_CenterGlow;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;21;0;15;1
WireConnection;21;1;15;2
WireConnection;22;0;15;3
WireConnection;22;1;15;4
WireConnection;107;0;29;0
WireConnection;107;2;21;0
WireConnection;108;0;89;0
WireConnection;108;2;22;0
WireConnection;13;1;107;0
WireConnection;14;1;108;0
WireConnection;30;0;13;0
WireConnection;30;1;14;0
WireConnection;30;2;31;0
WireConnection;30;3;32;0
WireConnection;30;4;33;0
WireConnection;78;0;30;0
WireConnection;88;0;13;4
WireConnection;88;1;14;4
WireConnection;88;2;31;4
WireConnection;88;3;32;4
WireConnection;88;4;62;0
WireConnection;51;0;78;0
WireConnection;51;1;52;0
WireConnection;87;0;51;0
WireConnection;87;3;88;0
WireConnection;68;0;87;0
ASEEND*/
//CHKSM=F61F87F94800A5E382A10A1681D03EDD0B2677A9