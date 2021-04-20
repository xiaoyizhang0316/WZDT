Shader "Custom/PBRShader"
{
Properties
{
    _Color ("Color", Color) = (1, 1, 1, 1)
    _MainTex ("Albedo (RGB)", 2D) = "white" {}
_MetallicTex("Metallic(R),Smoothness(A)",2D) = "white"{}
_Metallic ("Metallic", Range(0, 1)) = 1.0
_Glossiness("Smoothness",Range(0,1)) = 1.0
[Normal]_Normal("NormalMap",2D) = "bump"{}
_OcclussionTex("Occlusion",2D) = "white"{}
_AO("AO",Range(0,1)) = 1.0
_Emission("Emission",Color) = (0,0,0,1)

_MainTex ("Terrain Texture Array", 2DArray) = "white" {}

_GridTex ("Grid Texture", 2D) = "white" {}

_Specular ("Specular", Color) = (0.2, 0.2, 0.2)
_BackgroundColor ("Background Color", Color) = (0,0,0)
[Toggle(SHOW_MAP_DATA)]_ShowMapData ("Show Map Data", Float) = 0
}
SubShader
{
    Tags { "RenderType" = "Opaque" }

Pass
{
//因为需要灯光设定和Unity配合，所以要加上前向渲染标签。
Tags { "LightMode" = "ForwardBase" }


CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members worldPos,terrain,visibility,mapData)
#pragma exclude_renderers d3d11

//顶点片段着色器
#pragma surface surf StandardSpecular fullforwardshadows vertex:vert 
#pragma fragment frag
//指定平台，也可以省略
#pragma target 3.5
//雾效和灯光的关键字
#pragma multi_compile_fog
#pragma multi_compile_fwdbase
#pragma multi_compile _ GRID_ON
#pragma multi_compile _ HEX_MAP_EDIT_MODE
#pragma shader_feature SHOW_MAP_DATA
//一些会用到的cginc文件
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"
#include "../HexMetrics.cginc"
#include "../HexCellData.cginc"
//...
UNITY_DECLARE_TEX2DARRAY(_MainTex);
sampler2D _GridTex;
fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _MetallicTex;
fixed _Metallic;
fixed _Glossiness;
half3 _BackgroundColor;
fixed3 _Specular;
sampler2D _OcclussionTex;
fixed _AO;
half3 _Emission;
sampler2D _Normal;
//...

struct v2f
{
    float4 color : COLOR;
    float4 pos:SV_POSITION;//裁剪空间位置输出
    float2 uv: TEXCOORD0; // 贴图UV
    float3 worldPos: TEXCOORD1;//世界坐标
    float3 tSpace0:TEXCOORD2;//TNB矩阵0
    float3 tSpace1:TEXCOORD3;//TNB矩阵1
    float3 tSpace2:TEXCOORD4;//TNB矩阵2
        float3 worldPos;
        float3 terrain;
        float4 visibility;

    UNITY_FOG_COORDS(5)//雾效坐标
    UNITY_SHADOW_COORDS(6)//阴影坐标 _ShadowCoord

    #if defined(SHOW_MAP_DATA)
        float mapData;
        #endif
//如果需要采样球谐函数，则输入球谐函数参数。
#if UNITY_SHOULD_SAMPLE_SH
    half3 sh: TEXCOORD7; // SH
    #endif

};

// vertex shader
//这里没有写appdata结构体，直接采用内置的appdata_Full
v2f vert(appdata_full v)
{
    v2f o;//定义返回v2f 结构体o
    UNITY_INITIALIZE_OUTPUT(v2f, o);//将o初始化。

    float4 cell0 = GetCellData(v, 0);
    float4 cell1 = GetCellData(v, 1);
    float4 cell2 = GetCellData(v, 2);

    o.terrain.x = cell0.w;
    o.terrain.y = cell1.w;
    o.terrain.z = cell2.w;

    o.visibility.x = cell0.x;
    o.visibility.y = cell1.x;
    o.visibility.z = cell2.x;

    o.visibility.xyz = lerp(0.25, 1, o.visibility.xyz);
    o.visibility.w =
    cell0.y * v.color.x + cell1.y * v.color.y + cell2.y * v.color.z;


    o.pos = UnityObjectToClipPos(v.vertex);//计算齐次裁剪空间下的坐标位置
    //这里的uv只定义了两个分量。TranformTex方法加入了贴图的TillingOffset值。
    o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;//世界空间坐标计算。
    float3 worldNormal = UnityObjectToWorldNormal(v.normal);//世界空间法线计算
    half3 worldTangent = UnityObjectToWorldDir(v.tangent);//世界空间切线计算
    //利用切线和法线的叉积来获得副切线，tangent.w分量确定副切线方向正负，unity_WorldTransformParams.w判定模型是否有变形翻转。
    half3 worldBinormal = cross(worldNormal,worldTangent)*v.tangent.w *unity_WorldTransformParams.w;

    //组合TBN矩阵，用于后续的切线空间法线计算。
    o.tSpace0 = float3(worldTangent.x,worldBinormal.x,worldNormal.x);
    o.tSpace1 = float3(worldTangent.y,worldBinormal.y,worldNormal.y);
    o.tSpace2 = float3(worldTangent.z,worldBinormal.z,worldNormal.z);

    // SH/ambient和顶点光照写入o.sh里
    #ifndef LIGHTMAP_ON
    #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
    o.sh = 0;
    // Approximated illumination from non-important point lights
    //如果有顶点光照的情况（超出系统限定的灯光数或者被设置为non-important灯光）
    #ifdef VERTEXLIGHT_ON
    o.sh += Shade4PointLights(
    unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
    unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
    unity_4LightAtten0, o.worldPos, worldNormal);
    #endif
    //球谐光照计算（光照探针，超过顶点光照数量的球谐灯光）
    o.sh = ShadeSHPerVertex(worldNormal, o.sh);
    #endif
    #endif // !LIGHTMAP_ON
    #if defined(SHOW_MAP_DATA)
    o.mapData = cell0.z * v.color.x + cell1.z * v.color.y +
    cell2.z * v.color.z;
    #endif
    UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy); // pass shadow and, possibly, light cookie coordinates to pixel shader
    //在appdata_full结构体里。v.texcoord1就是第二套UV，也就是光照贴图的UV。
    //计算并传递阴影坐标

    UNITY_TRANSFER_FOG(o, o.pos); // pass fog coordinates to pixel shader。计算传递雾效的坐标。

    return o;
}
float4 GetTerrainColor (v2f IN, int index) {
    float3 uvw = float3(
    IN.worldPos.xz * (2 * TILING_SCALE),
    IN.terrain[index]
    );
    float4 c = UNITY_SAMPLE_TEX2DARRAY(_MainTex, uvw);
    return c * (IN.color[index] * IN.visibility[index]);
}

void surf (v2f IN, inout SurfaceOutputStandardSpecular o) {
    fixed4 c =
    GetTerrainColor(IN, 0) +
    GetTerrainColor(IN, 1) +
    GetTerrainColor(IN, 2);

    fixed4 grid = 1;
    #if defined(GRID_ON)
    float2 gridUV = IN.worldPos.xz;
    gridUV.x *= 1 / (4 * 8.66025404/8);
    gridUV.y *= 1 / (2 * 15.0/8);
    grid = tex2D(_GridTex, gridUV);
    #endif

    float explored = IN.visibility.w;
    o.Albedo = c.rgb * grid * _Color * explored;
    o.Normal=UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
    #if defined(SHOW_MAP_DATA)
    o.Albedo = IN.mapData * grid;
    #endif
    o.Specular = _Specular * explored;
    o.Smoothness = _Glossiness;
    o.Occlusion = explored;
    o.Emission = _BackgroundColor * (1 -  explored);
    o.Alpha = c.a;
}

// fragment shader
fixed4 frag(v2f i): SV_Target
{
    fixed4 c =
GetTerrainColor(i, 0) +
GetTerrainColor(i, 1) +
GetTerrainColor(i, 2);

    fixed4 grid = 1;
    #if defined(GRID_ON)
    float2 gridUV = i.worldPos.xz;
    gridUV.x *= 1 / (4 * 8.66025404/8);
    gridUV.y *= 1 / (2 * 15.0/8);
    grid = tex2D(_GridTex, gridUV);
    #endif

    float explored = i.visibility.w;

    half3 normalTex = UnpackNormal(tex2D(_Normal,i.uv));//使用法线的采样方式对法线贴图进行采样。
//切线空间法线（带贴图）转向世界空间法线。
    half3 worldNormal = half3(dot(i.tSpace0,normalTex),dot(i.tSpace1,normalTex),dot(i.tSpace2,normalTex));
    worldNormal = normalize(worldNormal);//所有传入的“向量”最好归一化一下
//计算灯光方向：注意这个方法已经包含了对灯光的判定。
//其实在forwardbase pass中，可以直接用灯光坐标代替这个方法，因为只会计算Directional Light。
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
    float3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));//片段指向摄像机方向viewDir

    SurfaceOutputStandard o;//声明变量
    UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandard,o);//初始化里面的信息。避免有的时候报错干扰
    fixed4 AlbedoColorSampler = tex2D(_MainTex, i.uv) * _Color;//采样颜色贴图，同时乘以控制的TintColor
    o.Albedo = c.rgb * grid * _Color * explored;
    #if defined(SHOW_MAP_DATA)
    o.Albedo = i.mapData * grid;
    #endif
    o.Emission = _BackgroundColor * (1 -  explored);
    fixed4 MetallicSmoothnessSampler = tex2D(_MetallicTex,i.uv);//采样Metallic-Smoothness贴图
    o.Metallic = MetallicSmoothnessSampler.r*_Metallic;//r通道乘以控制色并赋予金属度
    o.Specular = _Specular * explored;
    o.Smoothness = MetallicSmoothnessSampler.a*_Glossiness;//a通道乘以控制色并赋予光滑度
    o.Alpha = AlbedoColorSampler.a;//单独赋予透明度
    o.Occlusion = tex2D(_OcclussionTex,i.uv)*_AO; //赋予AO
    o.Normal = worldNormal;//赋予法线
    o.Alpha = c.a;


// compute lighting & shadowing factor
//计算光照衰减和阴影
    UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos)

    //初始化全局光照，输入直射光参数。间接光参数置零待更新。
    // Setup lighting environment
    UnityGI gi;//声明变量
    UNITY_INITIALIZE_OUTPUT(UnityGI, gi);//初始化归零
    gi.indirect.diffuse = 0;//indirect部分先给0参数，后面需要计算出来。这里只是示意
    gi.indirect.specular = 0;
    gi.light.color = _LightColor0.rgb;//unity内置的灯光颜色变量
    gi.light.dir = lightDir;//赋予之前计算的灯光方向。

//初始化giInput并赋予已有的值。此参数为gi计算所需要的输入参数。
// Call GI (lightmaps/SH/reflections) lighting function
    UnityGIInput giInput;
    UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);//初始化归零
    giInput.light = gi.light;//之前这个light已经给过，这里补到这个结构体即可。
    giInput.worldPos = i.worldPos;//世界坐标
    giInput.worldViewDir = worldViewDir;//摄像机方向
    giInput.atten = atten;//在之前的光照衰减里面已经被计算。其中包含阴影的计算了。

    //球谐光照和环境光照输入（已在顶点着色器里的计算，这里只是输入）
    #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
    giInput.ambient = i.sh;
    #else//假如没有做球谐计算，这里就归零
    giInput.ambient.rgb = 0.0;
    #endif

//反射探针相关
    giInput.probeHDR[0] = unity_SpecCube0_HDR;
    giInput.probeHDR[1] = unity_SpecCube1_HDR;
    #if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
    giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
    #endif
    #ifdef UNITY_SPECCUBE_BOX_PROJECTION
    giInput.boxMax[0] = unity_SpecCube0_BoxMax;
    giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
    giInput.boxMax[1] = unity_SpecCube1_BoxMax;
    giInput.boxMin[1] = unity_SpecCube1_BoxMin;
    giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
    #endif

//基于PBS的全局光照（gi变量）的计算函数。计算结果是gi的参数（Light参数和Indirect参数）。注意这一步还没有做真的光照计算。
    LightingStandard_GI(o, giInput, gi);
    fixed4 c = 0;
// realtime lighting: call lighting function
//PBS计算
    c += LightingStandard(o, worldViewDir, gi);

//叠加雾效。
    UNITY_EXTRACT_FOG(i);//此方法定义了一个片段着色器里的雾效坐标变量，并赋予传入的雾效坐标。
    UNITY_APPLY_FOG(_unity_fogCoord, c); // apply fog
    return c;
}
ENDCG
}
FallBack "Diffuse"
}