using UnityEngine;

public static class VisualFormattingUtils
{
    public static string Link(string text)
    {
        return $"<i><b><color=blue>{text}</color></b></i>";
    }

    public static Color Color(string color)
    {
        ColorUtility.TryParseHtmlString(color, out Color c);

        return c;
    }
}