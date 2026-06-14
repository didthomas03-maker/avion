using UnityEngine;

public static class Extensions
{
    public static Vector3 RotateTowards(this Vector3 from, Vector3 to, float maxAngle)
    {
        float angle = Vector3.Angle(from, to);
        if (angle == 0f) return from;
        
        float t = Mathf.Min(1f, maxAngle / angle);
        return Vector3.Slerp(from, to, t);
    }
    
    public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
}
