﻿// OverlayHardLight
float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 clr1 = tex2D(tex1, uv.xy);

	float4 Color = tex2D(input , uv.xy);
	if (Color.r < 0.5) {
		Color.r = clr1.r * Color.r;
	}else{
		Color.r = 1 - ((1 - Color.r)*(1 - clr1.r));
	}
	if (Color.g < 0.5) {
		Color.g = clr1.g * Color.g;
	}else{
		Color.g = 1 - ((1 - Color.g)*(1 - clr1.g));
	}
	if (Color.b < 0.5) {
		Color.b = clr1.b * Color.b;
	}else{
		Color.b = 1 - ((1 - Color.b)*(1 - clr1.b));
	}

	Color.r = Color.r * mixInAmount;
	Color.g = Color.g * mixInAmount;
	Color.b = Color.b * mixInAmount;

	return Color+clr1;
}
