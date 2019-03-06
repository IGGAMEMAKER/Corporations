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
        float percent = (value - minValue) / (maxValue - minValue);

        if (percent < 0)
            percent = 0;

        if (percent > 1)
            percent = 1;

        if (reversed)
            percent = 1 - percent;

        float r = 1f - percent;
        float g = percent;

        return new Color(r, g, 0, 1);
    }

    public override Color GetColor()
    {
        return GetGradientColor();
    }
}
