float Percentage;

sampler TextureSampler: register(s0);

float4 PixelShaderFunction(float2 inCoord: TEXCOORD0) : COLOR
{

    float4 color = tex2D(TextureSampler, inCoord);
    return color;
}


technique hit 
{
	pass Pass1 {
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}