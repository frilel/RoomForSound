Shader "Unlit/URPSphere"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalText ("Tecture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        // LOD 100

        HLSLINCLUDE

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _Color;
        CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        struct VertexInput
        {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VertextOutput
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

                VertextOutput vert(VertexInput i)
                {
                    VertextOutput o;
                    o.position = TransformObjectToHClip(i.position.xyz);
                    o.uv = i.uv;
                    return o;
                }

                float4 frag(VertextOutput i) : SV_TARGET
                {
                    float4 baseText = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    return baseText * _Color;
                }
            ENDHLSL
           
        }
    }
}
