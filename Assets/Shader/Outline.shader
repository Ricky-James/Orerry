//Using tutorial from N3K EN : https://www.youtube.com/watch?v=SlTkBe4YNbo

//Todo: learn what this code does. I need an introduction to shader language...
Shader "Unlit/Outline"
{
    Properties
    {
		_Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)
        _MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline color", Color) = (0,0,0,1)
		_OutlineWidth("Outline width", Range(1.0, 1.2)) = 1.1
			
    }

CGINCLUDE
#include "UnityCG.cginc"

//Structs etc. usually inside a pass, but outside like this lets us re-use it
	struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		float4 color : COLOR;
		float3 normal : NORMAL;
	};

	float _OutlineWidth;
	float4 _OutlineColor;

	v2f vert(appdata v)
	{
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f, o); //Seems unnecessary but clears up a warning
		v.vertex.xyz *= _OutlineWidth;
		o.pos = UnityObjectToClipPos(v.vertex);

		return o;
	};


ENDCG
    SubShader
    {
		//Adjust render queue to 3000
		Tags{"Queue" = "Transparent"}

		Pass //Render outline
		{
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}

		Pass //Normal render
		{
			ZWrite On

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}

			Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}

        
    }
}
