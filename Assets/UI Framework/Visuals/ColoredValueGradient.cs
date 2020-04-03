using Assets.Core;
using UnityEngine;

public class ColoredValueGradient : ColoredValue
{
    [Tooltip("min < max")]
    public float minValue;
    public float maxValue;

    [Header("Reversed")]
    [Tooltip("If you want to flip gradient border colors, set this flag")]
    public bool reversed = false;

    Color GetGradientColor()
    {
        return Visuals.GetGradientColor(minValue, maxValue, value, reversed);
    }

    public override Color GetColor()
    {
        return GetGradientColor();
    }
}
