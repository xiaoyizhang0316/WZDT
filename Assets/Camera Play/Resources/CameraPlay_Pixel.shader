////////////////////////////////////////////
///// CameraPlay - by VETASOFT 2018    /////
////////////////////////////////////////////

Shader "CameraPlay/Pixel" {
Properties 
{
_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader 
{
Pass
{
	Cull Off ZWrite Off ZTest Always
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"


uniform sampler2D _MainTex;
uniform float _TimeX;
uniform float _Fade;
uniform float _PixelSize;
uniform float4 _ScreenResolution;

struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};   

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;

return OUT;
}


half4 _MainTex_ST;
float4 frag(v2f i) : COLOR
{
float2 uvst = UnityStereoScreenSpaceUVAdjust(i.texcoord, _MainTex_ST);
_PixelSize *= _Fade+0.01;
float2 uv = uvst.xy;
float2 p = _ScreenResolution/ _PixelSize;
uv = floor(uv * p) / p;
float4 txt = tex2D(_MainTex, uv);
return  txt;
}

ENDCG
}

}
}