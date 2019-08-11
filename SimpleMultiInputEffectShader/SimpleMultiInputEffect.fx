//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- SimpleMultiInputEffect
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float mixInAmount : register(C0);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D input1 : register(S0);
sampler2D input2 : register(S1);


//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 Face = tex2D(input1, uv);
   float4 Logo = tex2D(input2, uv);
   //return Face + mixInAmount * Logo;
   return Logo + mixInAmount * Face;
}
