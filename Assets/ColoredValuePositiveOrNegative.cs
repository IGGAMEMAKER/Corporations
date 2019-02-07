using UnityEngine;

public class ColoredValuePositiveOrNegative : ColoredValue
{
    public float neutralValue;

    public override Color GetColor()
    {
        return GetClampedColor();
    }

    Color GetClampedColor()
    {
        if (value > neutralValue)
            return Color.green;

        return Color.red;
    }
}
