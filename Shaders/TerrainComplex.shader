Shader "Custom/ComplexTerrain" 
{
Properties 
{
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _HeightMin ("Height Min", Float) = -1
    _HeightMax ("Height Max", Float) = 1
    _MiddleBot ("Mid 1", Range(0.001, 0.999)) = 0.25
    _MiddleTop ("Mid 2", Range(0.001, 0.999)) = 0.75
    _ColorBot ("Tint Color At Min", Color) = (0,0,0,1)
    _ColorMidBot ("Tint Color At Mid 1", Color) = (1,1,1,1)
    _ColorMidTop ("Tint Color At Mid 2", Color) = (1,1,1,1)
    _ColorTop ("Tint Color At Max", Color) = (1,1,1,1)
}
  
SubShader
{
    Tags { "RenderType"="Opaque" }
  
    CGPROGRAM
    #pragma surface surf Lambert
  
    sampler2D _MainTex;
    fixed4 _ColorBot;
    fixed4 _ColorMidBot;
    fixed4 _ColorMidTop;
    fixed4 _ColorTop;
    float _MiddleBot;
    float _MiddleTop;
    float _HeightMin;
    float _HeightMax;
  
    struct Input
    {
    float2 uv_MainTex;
    float3 worldPos;
    };
  
    void surf (Input IN, inout SurfaceOutput o) 
    {
    half4 c = tex2D (_MainTex, IN.uv_MainTex);
    float h = (_HeightMax-IN.worldPos.y) / (_HeightMax-_HeightMin);

    fixed4 col = lerp(_ColorBot, _ColorMidBot, h / _MiddleBot) * step(h, _MiddleBot);
    col += lerp(_ColorMidBot, _ColorMidTop, (h - _MiddleBot) / (_MiddleTop - _MiddleBot)) * step(_MiddleBot, h) * step(h, _MiddleTop);
    col += lerp(_ColorMidTop, _ColorTop, (h - _MiddleTop) / (1 - _MiddleTop)) * step(_MiddleTop, h);
    col.a = 1;

    //o.Albedo = c.rgb * col.rgb;
    //o.Alpha = c.a * col.a;

    o.Albedo = col.rgb;
    o.Alpha = 1;
    }
    ENDCG
} 
Fallback "Diffuse"
}