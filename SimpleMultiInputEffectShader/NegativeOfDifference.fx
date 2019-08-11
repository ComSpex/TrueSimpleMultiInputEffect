// NegativeOfDifference
float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Logo = tex2D(tex1, uv.xy);
	float4 Face = tex2D(input , uv.xy);

	Face.r = 1 - (1 - Logo.r - Face.r) * mixInAmount;
	Face.g = 1 - (1 - Logo.g - Face.g) * mixInAmount;
	Face.b = 1 - (1 - Logo.b - Face.b) * mixInAmount;

	return Face*Logo;
}
