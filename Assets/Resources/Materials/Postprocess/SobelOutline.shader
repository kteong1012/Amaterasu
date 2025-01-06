Shader "Postprocess/SobelOutline"
{
    Properties
    {
        _EdgeColor("EdgeColor",Color)=(0,0,0,1)
        _Rate("Rate", Float) = 0.5
        _Strength("Strength", Float) = 0.7
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline"
        }
        Cull Off
        Blend Off
        ZTest Off
        ZWrite Off
        Pass
        {
            Name "Outline"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            //这两个头文件包括了大多数需要用到的变量
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            //这个需要自己声明，xy表示纹素的长宽，zw表示整个BlitTexture的长宽，BlitTexture就是当前摄像机的颜色缓冲区
            //float4 _BlitTexture_TexelSize;

            TEXTURE2D(_CopiedColor); SAMPLER(sampler_CopiedColor);

            float2 uvs[9];
            //常规CBUFFER，上面自定义的属性要写在这里
            CBUFFER_START(UnityPerMaterial)
            half4 _EdgeColor;
            half _Rate;
            half _Strength;
            CBUFFER_END
            
            half Sobel()
            {
                const half Gx[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
                const half Gy[9] = {-1, 0, 1, -2, 0, 2, -1, 0, 1};
                half texColor;
                half edgeX = 0, edgeY = 0;
                for (int it = 0; it < 9; it++)
                {
                    //RGB转亮度
                    texColor = Luminance(SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uvs[it]));
                    //计算亮度在XY方向的导数，如果导数越大，越接近一个边缘点
                    edgeX += texColor * Gx[it];
                    edgeY += texColor * Gy[it];
                }
                //edge越小，越可能是个边缘点
                half edge = 1 - abs(edgeX) - abs(edgeY);
                return edge;
            }

            half4 Frag(Varyings i) : SV_TARGET
            {
                half2 uv = i.texcoord;
                uvs[0] = uv + _BlitTexture_TexelSize.xy * half2(-1, -1) * _Rate;
                uvs[1] = uv + _BlitTexture_TexelSize.xy * half2(0, -1) * _Rate;
                uvs[2] = uv + _BlitTexture_TexelSize.xy * half2(1, -1) * _Rate;
                uvs[3] = uv + _BlitTexture_TexelSize.xy * half2(-1, 0) * _Rate;
                uvs[4] = uv + _BlitTexture_TexelSize.xy * half2(0, 0) * _Rate;
                uvs[5] = uv + _BlitTexture_TexelSize.xy * half2(1, 0) * _Rate;
                uvs[6] = uv + _BlitTexture_TexelSize.xy * half2(-1, 1) * _Rate;
                uvs[7] = uv + _BlitTexture_TexelSize.xy * half2(0, 1) * _Rate;
                uvs[8] = uv + _BlitTexture_TexelSize.xy * half2(1, -1) * _Rate;
                half edge = Sobel();
                //根据edge的大小，在边缘颜色和原本颜色之间插值，edge为0时，完全是边缘，edge为1时，完全是原始颜色
                half4 withEdgeColor = lerp(_EdgeColor * _Strength,SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv), edge);
                half4 test = SAMPLE_TEXTURE2D(_CopiedColor,sampler_CopiedColor,uv);
                
                return test;
            }
            ENDHLSL
        }
    }
    //后处理不需要Fallback，不满足的时候不显示即可
    Fallback off
}