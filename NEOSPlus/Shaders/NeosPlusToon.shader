Shader "Custom/ToonMatcap" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _MatcapTex ("Matcap Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(0.01, 1)) = 0.078
        _MatcapIntensity ("Matcap Intensity", Range(0.01, 1)) = 1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MatcapTex;
            float4 _Color;
            float _Shininess;
            float _MatcapIntensity;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Calculate the toon shading
                fixed4 col = tex2D(_MainTex, i.uv);
                float4 toon = step(0.8, col.r);
                col = lerp(col, toon, _Shininess);

                // Apply the matcap texture
                float4 matcap = tex2D(_MatcapTex, i.uv);
                col.rgb = lerp(col.rgb, matcap.rgb, _MatcapIntensity);

                // Apply the color
                col *= _Color;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
