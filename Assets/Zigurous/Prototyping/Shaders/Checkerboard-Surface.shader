Shader "Zigurous/Checkerboard/Surface"
{
    Properties
    {
        _Tiling("Tiling", Range(2, 64)) = 2
        _Color1("Color 1", Color) = (1, 1, 1, 1)
        _Color2("Color 2", Color) = (0, 0, 0, 1)
        _MainTex("Texture", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert fullforwardshadows
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _MainTex_ST;

        struct Input
        {
            float2 custom_uv;
        };

        fixed4 _Color1;
        fixed4 _Color2;

        half _Tiling;
        half _Glossiness;
        half _Metallic;

        void vert (inout appdata_full v, out Input o)
        {
            o.custom_uv = v.texcoord.xy;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.custom_uv * _Tiling;
            float2 c = floor(uv) / 2.0;
            float checker = frac(c.x + c.y) * 2.0;
            fixed4 color = checker == 0.0 ? _Color1 : _Color2;

            uv = TRANSFORM_TEX(IN.custom_uv, _MainTex);
            color = tex2D(_MainTex, uv) * color;
            o.Albedo = color.rgb;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = color.a;
        }

        ENDCG
    }

    FallBack "Diffuse"
}
