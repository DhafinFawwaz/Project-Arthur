public static class Global
{
    public static float DelayDuration = 1f;

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    => (value - from1) / (to1 - from1) * (to2 - from2) + from2;

    
}
