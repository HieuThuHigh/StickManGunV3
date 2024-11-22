Shader "Custom/SprSkin"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" { }
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _ColorSkinTint ("ColorSkinTint", Color) = (1, 1, 1, 1)
        _ColorRef ("ColorRef", Color) = (1, 1, 1, 1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1, 1, 1, 1)
        [HideInInspector] _Flip ("Flip", Vector) = (1, 1, 1, 1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" { }
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "SpriteRendererBase.cginc"

            fixed4 _ColorSkinTint;
            fixed4 _ColorRef;

            v2f Vert(appdata_t IN)
            {
                // Sprite Renderer Source

                v2f OUT;

                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color * _RendererColor;

                #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif

                return OUT;

                // Sprite Renderer Source
            }

            float4 frag(v2f i) : SV_TARGET
            {
                // Sprite Renderer Source
                fixed4 col = SampleSpriteTexture(i.texcoord);
                //col.rgb *= col.a;
                // Sprite Renderer Source

                if (abs(col.r / col.g - _ColorRef.r / _ColorRef.g) < 0.1 && abs(
                    col.r / col.b - _ColorRef.r / _ColorRef.b) < 0.1)
                {
                    col.rgb = fixed3(1, 1, 1) * col.r / _ColorRef.r;
                    col = col * _ColorSkinTint;
                }

                return col *= col.a * i.color;
            }
            ENDCG
        }
    }
}