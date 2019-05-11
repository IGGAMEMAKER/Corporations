using System;
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

    public static string Describe(string text, int value)
    {
        if (value > 0)
            return Positive(text + ": +" + value);

        if (value == 0)
            return Neutral(text + ": 0");

        return Negative(text + ": " + value);
    }

    public static string Bonus(string text, int value)
    {
        return "\n" + Describe(text, value);
    }

    public static string Describe(string positiveText, string negativeText, int value)
    {
        if (value > 0)
            return Positive(positiveText + ": +" + value);

        if (value == 0)
            return "";

        return Negative(negativeText + ": " + value);
    }
}