using UnityEngine;

public static class VisualUtils
{
    public static string Link(string text)
    {
        return $"<i><b><color=magenta>{text}</color></b></i>";
    }

    public static string Colorize(string text, string colorHTML)
    {
        return $"<color={colorHTML}>{text}</color>";
    }

    public static string Colorize(string text, bool isPositive)
    {
        string col = isPositive ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE;

        return Colorize(text, col);
    }

    public static string Positive(string text)
    {
        return Colorize(text, VisualConstants.COLOR_POSITIVE);
    }

    public static string Neutral(string text)
    {
        return Colorize(text, VisualConstants.COLOR_NEUTRAL);
    }


    public static Color Color(string color)
    {
        ColorUtility.TryParseHtmlString(color, out Color c);

        return c;
    }

    internal static string Negative(string text)
    {
        return Colorize(text, VisualConstants.COLOR_NEGATIVE);
    }

    public static string Sign(long value)
    {
        return value > 0 ? $"+{value}" : value.ToString();
    }


    static string DescribeNormally(string text, long value)
    {
        if (value == 0)
            return Neutral(text);

        if (value > 0)
            return Positive(text);

        return Negative(text);
    }

    static string DescribeReversed(string text, long value)
    {
        if (value == 0)
            return Neutral(text);

        if (value > 0)
            return Negative(text);

        return Positive(text);
    }

    public static string Describe(string bonusName, long value, string dimension, bool flipColors, BonusType bonusType)
    {
        var text = "";

        if (bonusType == BonusType.Multiplicative)
            text = "Multiplied by ";

        text += $"{bonusName}: {Sign(value)}{dimension}";

        if (!flipColors)
            return DescribeNormally(text, value);
        else
            return DescribeReversed(text, value);
    }

    public static string Describe(BonusDescription bonus, bool flipColors)
    {
        return Describe(bonus.Name, bonus.Value, bonus.Dimension, flipColors, bonus.BonusType);
    }

    public static string Describe(BonusDescription bonus)
    {
        return Describe(bonus.Name, bonus.Value, bonus.Dimension, false, bonus.BonusType);
    }

    public static string Describe(long value, string positiveText, string negativeText)
    {
        if (value == 0)
            return "";

        if (value > 0)
            return Positive(positiveText);

        return Negative(negativeText);
    }
}