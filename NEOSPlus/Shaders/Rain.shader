Shader "xLinka/Rain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(0.01, 10.0)) = 1.0
        _Density ("Density", Range(0.1, 10.0)) = 1.0
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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
            float _Speed;
            float _Density;
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
                // Generate a random noise value using the screen position
                float noise = tex2D(_MainTex, i.uv * _Density + _Time.y * _Speed).r;

                // If the noise value is below a certain threshold, set the pixel to transparent
                if (noise < 0.3)
                {
                    return float4(0,0,0,0);
                }

                // Otherwise, return the rain color
                return _Color;
            }
            ENDCG
        }
    }
}