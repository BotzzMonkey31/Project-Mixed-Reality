Shader "Custom/URPDistanceFade"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _BeginFadeDistance("Begin Fade Distance", Float) = 5.0
        _EndFadeDistance("End Fade Distance", Float) = 10.0
        _MainTex("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off // Disable writing to the depth buffer for transparency

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; // Added normal for lighting calculations
                float2 uv : TEXCOORD0; // Texture coordinates
                UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float fade : TEXCOORD0; // Store fade value to pass to fragment shader
                float3 normal : TEXCOORD1; // Pass the normal to the fragment shader
                float2 uv : TEXCOORD2; // Pass UV to fragment shader
                UNITY_VERTEX_OUTPUT_STEREO //Insert
            };

            float4 _Color;
            float _BeginFadeDistance;
            float _EndFadeDistance;
            sampler2D _MainTex;

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); // Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // Insert
                o.pos = UnityObjectToClipPos(v.vertex);

                // Calculate the full 3D distance from the camera to the vertex
                float3 cameraPos = _WorldSpaceCameraPos.xyz;
                float3 objectPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float dist = distance(cameraPos, objectPos); // Calculate the Euclidean distance in 3D
                
                // Calculate fade factor based on distance
                float fadeFactor = saturate((dist - _BeginFadeDistance) / (_EndFadeDistance - _BeginFadeDistance));
                
                o.fade = 1.0 - fadeFactor; // Invert the fade for correct transparency
                o.normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal)); // Transform normal to world space
                o.uv = v.uv; // Pass UV coordinates
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample texture color
                fixed4 texCol = tex2D(_MainTex, i.uv);
                fixed4 col = texCol * _Color;

                // Basic lighting calculation
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz); // Get the main light direction
                float diff = max(0.0, dot(i.normal, lightDir)); // Calculate diffuse lighting

                // Combine the light contribution with the color
                col.rgb += diff * col.rgb; // Add light to the color

                // Apply distance fade
                col.a *= i.fade; // Use the fade value to adjust alpha

                return col;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
