Shader "Hidden/GrayscaleEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 frag(v2f_img i) : COLOR
            {
                float4 col = tex2D(_MainTex, i.uv);
                // Obliczamy wartoœæ grayscale wg standardowych wag
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                return float4(gray, gray, gray, col.a);
            }
            ENDCG
        }
    }
}
