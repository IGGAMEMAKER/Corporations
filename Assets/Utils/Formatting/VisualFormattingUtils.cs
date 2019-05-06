using UnityEngine;

public static class VisualFormattingUtils
{
    public static string Link(string text)
    {
        return $"<i><b><color=magenta>{text}</color></b></i>";
    }

    public static string Positive(string text)
    {
        return Colorize(text, VisualConstants.COLOR_POSITIVE);
    }

    public static string Colorize(string text, string colorHTML)
    {
        return $"<color={colorHTML}>{text}</color>";
    }

    public static Color Color(string color)
    {
        ColorUtility.TryParseHtmlString(color, out Color c);

        return c;
    }
}