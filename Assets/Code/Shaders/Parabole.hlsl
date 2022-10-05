#ifndef CUSTOM_HLSL
#define CUSTOM_HLSL

void Parabole_float(float1 In, out float1 Out)
{
    Out = -4*(0.5 - In)*(0.5 - In) + 1;
}
#endif //CUSTOM_HLSL