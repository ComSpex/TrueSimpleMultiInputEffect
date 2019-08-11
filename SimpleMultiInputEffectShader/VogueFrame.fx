// Time POY
float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);

float4 main(float2 uv : TEXCOORD) : Color
{
	float4 Logo=tex2D(tex1, uv.xy);
	float4 Face=tex2D(input, uv.xy);

	Face*= Logo * mixInAmount;

	return Face;
}
