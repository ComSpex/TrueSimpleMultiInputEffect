// Be forewarned: FXC is finicky about encoding types for the Input
// file. It prefers ASCII and doesn't like Unicode encoded text files.

float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);

float4 redTint(float2 uv:TEXCOORD) :COLOR{
	float4 Logo = tex2D(tex1, uv.xy);
	float4 Face = tex2D(input,uv.xy);

	Face.r += 1 + uv.y;
	Logo.r += 1 + uv.y;
	Face *= Logo * mixInAmount;
	return Face;
}
float4 greenTint(float2 uv:TEXCOORD) :COLOR{
	float4 Logo = tex2D(tex1, uv.xy);
	float4 Face = tex2D(input,uv.xy);

	Face.g += 1 + uv.y;
	Logo.g += 1 + uv.y;
	Face *= Logo * mixInAmount;
	return Face;
}
float4 blueTint(float2 uv:TEXCOORD) :COLOR{
	float4 Logo = tex2D(tex1, uv.xy);
	float4 Face = tex2D(input, uv.xy);

	Face.b += 1 + uv.y;
	Logo.b += 1 + uv.y;
	Face *= Logo * mixInAmount;
	return Face;
}
float4 bluesy(float2 uv:TEXCOORD) :COLOR{
	float4 color = tex2D(input,uv);
	float4 invertedColor = float4(color.a - color.rgb,color.a);
	return invertedColor;
}
float4 main(float2 uv:TEXCOORD) : COLOR{
	//return blueTint(uv);
//return redTint(uv);
	return greenTint(uv);
//return bluesy(uv);
}
