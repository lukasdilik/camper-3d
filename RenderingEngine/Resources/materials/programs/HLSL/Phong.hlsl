struct vertexIn
{
	float4 position	: POSITION;
	float3 normal	: NORMAL;
};

struct vertexOut
{
	float4 position  		: POSITION;
	float3 normal	 		: TEXCOORD0;
	float3 viewDir   		: TEXCOORD1;
	float3 lightDir[lightCount]	: TEXCOORD2;
};

struct pixelIn
{
	float3 normal			: TEXCOORD0;
	float3 viewDir			: TEXCOORD1;
	float3 lightDir[lightCount]	: TEXCOORD2;
};

vertexOut multiLightVS(vertexIn input,
	uniform	float4x4 worldViewProj_m,
	uniform	float4x4 world_m,
	uniform	float4 	cameraPos,
	uniform	float4 	lightPoses[lightCount]
	)
{
	vertexOut output = (vertexOut)0;

	output.position = mul(worldViewProj_m, input.position);
	output.normal = mul(world_m, input.normal);
	float3 worldpos = mul(world_m, input.position);
		output.viewDir = cameraPos - worldpos;

	for (int i = 0; i < lightCount; i++)
	{
		output.lightDir[i] = lightPoses[i] - worldpos;
	}

	return output;
}

float4 multiLightPS(pixelIn input,
	uniform	float4	diffColors[lightCount],
	uniform	float4 	specColors[lightCount],
	uniform	float4	ambientColor
	) : COLOR
{
	input.viewDir = normalize(input.viewDir);
	input.normal = normalize(input.normal);
	float4 	diff = float4(0, 0, 0, 0);
		float4 	spec = float4(0, 0, 0, 0);

	for (int i = 1; i<lightCount; i++)
	{
		input.lightDir[i] = normalize(input.lightDir[i]);

		float 	dotNL = dot(input.lightDir[i], input.normal);
		float3 	ref = (input.normal * 2 * dotNL) - input.lightDir[i];
	    float	dotRV = dot(ref, input.viewDir);

		spec += pow(saturate(dotRV), 25) * specColors[i];
		diff +=  diffColors[i];
	}

	return ambientColor + diff;
}