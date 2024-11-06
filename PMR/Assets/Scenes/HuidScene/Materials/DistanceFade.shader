Shader "Custom/DistanceFade" {

    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _Threshold("Threshold", float) = 5
    }

    SubShader{
        Tags { "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha    // <-- enable blending
        LOD 200

       Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                // make float3 for an extra value (can be done in other ways)
                float3 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Threshold;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord.xy = v.texcoord.xy;

                // get the distance from the camera to the vertex
                // (we do it in world space)
                float dist = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex));

                // map the distance to an fade interval
                float beginfade = 5;
                float endfade = 10;
                float alpha = min(max(dist, beginfade), endfade) - beginfade;
                alpha = 1 - alpha / (endfade - beginfade);

                // put alpha somewhere unused to deliver it to the fragment shader
                o.texcoord.z = alpha ;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.texcoord);

                // use our fade value 
                col.a = i.texcoord.z;
                return col * _Color;
            }
            ENDCG
       }
    }
    FallBack "Diffuse"
}