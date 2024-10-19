using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp)
    {
        float newValue = min2 + (max2 - min2) * (value - min1) / (max1 - min1);

        return clamp ? newValue : Mathf.Clamp(newValue, Mathf.Min(min2, max2), Mathf.Max(min2, max2));
    }

    public static Vector2 RandomizePosition(ABorder border)
    {
        return (new Vector2(Random.Range(border.leftBorder, border.rightBorder), Random.Range(border.bottomBorder, border.topBorder)));
    }
}
