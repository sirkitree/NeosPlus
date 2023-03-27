Shader "xlinka/Water"
{
    Properties
    {
        _Color ("Water Color", Color) = (1, 1, 1, 1)
        _MainTex ("Water Texture", 2D) = "white" {}
        _CuttingPlane ("Cutting Plane", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float _CuttingPlane;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alpha = step(i.uv.y, _CuttingPlane);
                fixed4 texColor = tex2D(_MainTex, i.uv);
                return lerp(texColor, _Color, alpha);
            }
            ENDCG
        }
    }
}
