// LightenLighterColors
float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Logo = tex2D(tex1, uv.xy);
	float4 Face = tex2D(input, uv.xy);

	if (Face.r < Logo.r) Face.r = Logo.r * mixInAmount;
	if (Face.g < Logo.g) Face.g = Logo.g * mixInAmount;
	if (Face.b < Logo.b) Face.b = Logo.b * mixInAmount;

	return Face*Logo;
}
