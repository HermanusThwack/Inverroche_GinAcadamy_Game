Shader "Custom/FoggyUi" {
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Speed;

            struct Input {
                float2 uv_MainTex;
                float2 uv_NoiseTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex * _Speed);
                o.Albedo = col.rgb * noise.rgb;
                o.Alpha = col.a;
            }
            ENDCG
    }
        FallBack "Diffuse"
}
