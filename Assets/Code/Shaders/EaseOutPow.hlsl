#ifndef CUSTOM_HLSL
#define CUSTOM_HLSL

void EaseOutPow_float(float4 In, float Pow, out float4 Out)
{
    Out = -pow(1-In, Pow) + 1;
}
#endif //CUSTOM_HLSL