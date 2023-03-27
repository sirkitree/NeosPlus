Shader "Custom/SoftOcclusion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutout ("Alpha cutoff", Range(0,1)) = 0.5
        _Softness ("Softness", Range(0,1)) = 0.1
        _ShadowColor ("Shadow Color", Color) = (0,0,0,0.8)
        _ShadowAmount ("Shadow Amount", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="TransparentCutout" }
        LOD 100

        CGPROGRAM
        #pragma surface surf TransparentCutout

        sampler2D _MainTex;
        float _Cutout;
        float _Softness;
        float4 _ShadowColor;
        float _ShadowAmount;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate the shadow factor based on the angle between the surface normal and the direction to the sky
            float shadow = dot(normalize(IN.worldNormal), Unity_SpecularLight0);

            // Apply softness to the shadow factor
            shadow = 1 - pow(1 - shadow, 1 / _Softness);

            // Apply shadow color and amount to the final color
            o.Albedo = lerp(_ShadowColor.rgb, tex2D(_MainTex, IN.uv_MainTex).rgb, shadow * _ShadowAmount);
            o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
            clip(o.Alpha - _Cutout);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
