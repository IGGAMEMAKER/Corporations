using System;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class Visuals
    {
        public static string Link(string text)
        {
            return $"<i><b><color=magenta>{text}</color></b></i>";
        }

        private static String HexConverter(Color c)
        {
            return "#" + ToHex(c.r) + ToHex(c.g) + ToHex(c.b);
        }
        private static string ToHex(float col)
        {
            return ((int)(col * 255)).ToString("X2"); // ("X2");
        }

        public static string Colorize(string text, Color color) => Colorize(text, HexConverter(color));
        public static string Colorize(string text, bool isPositive) => Colorize(text, isPositive ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE);
        public static string Colorize(string text, string colorHTML)
        {
            return $"<color={colorHTML}>{text}</color>";
        }

        public static string Positive(string text)  => Colorize(text, VisualConstants.COLOR_POSITIVE);
        public static string Neutral(string text)   => Colorize(text, VisualConstants.COLOR_NEUTRAL);
        public static string Negative(string text)  => Colorize(text, VisualConstants.COLOR_NEGATIVE);

        // TODO used twice in same place
        public static Color GetColorPositiveOrNegative(long value)
        {
            string col = value > 0 ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE;

            return GetColorFromString(col);
        }

        public static Color GetColorFromString(string color)
        {
            ColorUtility.TryParseHtmlString(color, out Color c);

            return c;
        }

        public static string DescribeValueWithText(long value, string positiveText, string negativeText, string neutralText)
        {
            if (value == 0)
                return Neutral(neutralText);

            if (value > 0)
                return Positive(positiveText);

            return Negative(negativeText);
        }

        public static string PositiveOrNegativeMinified(long value)
        {
            var minified = Format.Sign(value, true);

            return DescribeValueWithText(
                value,
                minified,
                minified,
                "0"
                );
            //if (value > 0)
            //    return Positive(Format.SignMinified(value));

            //if (value == 0)
            //    return "0";

            //return Negative(Format.SignMinified(value));
        }

    }
}