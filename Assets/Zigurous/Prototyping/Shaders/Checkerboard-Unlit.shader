Shader "Zigurous/Checkerboard/Unlit"
{
    Properties
    {
        _Tiling("Tiling", Range(2, 64)) = 2
        _Color1("Color 1", Color) = (1, 1, 1, 1)
        _Color2("Color 2", Color) = (0, 0, 0, 1)
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            fixed4 _Color1;
            fixed4 _Color2;

            half _Tiling;

            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(pos);
                o.uv = uv * _Tiling;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = i.uv;
                c = floor(c) / 2.0;
                float checker = frac(c.x + c.y) * 2.0;
                return checker == 0.0 ? _Color1 : _Color2;
            }

            ENDCG
        }

    }

}
