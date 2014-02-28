float PositionX;
float PositionY;
float Procentage;

sampler TextureSampler: register(s0);

float4 PixelShaderFunction(float2 inCoord: TEXCOORD0) : COLOR
{

	float4 color = tex2D(TextureSampler, inCoord);

	float disPosX = PositionX / 1600;
	float disPosY = PositionY / 900;

	float a = disPosX - inCoord.x;
	float b = disPosY - inCoord.y;
	
	if (a < 0) {
		a = inCoord.x - disPosX;
	}
	if (b < 0) {
		b = inCoord.y - disPosY;
	}

	float f = sqrt(a * a + b * b);

	return color * f * Procentage;
}


technique hit 
{
	pass Pass1 {
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}