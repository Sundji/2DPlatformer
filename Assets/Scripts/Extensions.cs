using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static float RandomValue(this Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }

    public static bool WithinCircle(this Vector2 point, Vector2 center, float radius)
    {
        float result = Mathf.Sqrt(Mathf.Pow(point.x - center.x, 2) + Mathf.Pow(point.y - center.y, 2));
        return result <= radius;
    }

}
