sampler2D input_sampler : register(S0);
float2 center : register(C0);
float radius : register(C1);
float brightness : register(C2);
float aspect_ratio : register(C3);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float2 direction = uv - center;
    direction.y /= aspect_ratio;
    float distance = length(direction);
    float adjusted_brightness = brightness * step(distance, radius);
    float factor = clamp(radius, 0.0, 1.0);
    float4 color = tex2D(input_sampler, uv);
    color.rgb = lerp(color.rgb + adjusted_brightness, color.rgb, factor);
    return color;
}
