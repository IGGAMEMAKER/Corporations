using UnityEngine;

public class ColoredValueGradient : ColoredValue
{
    public float minValue;
    public float maxValue;

    Color GetGradientColor()
    {
        float percent = (value - minValue) / (maxValue - minValue);

        if (percent < 0)
            percent = 0;
        else if (percent > 1)
            percent = 1;

        return new Color(255 - percent * 255, percent * 255, 0, 1);
    }

    public override Color GetColor()
    {
        return GetGradientColor();
    }
}
