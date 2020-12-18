using UnityEngine;

namespace Assets.Core
{
    public static partial class Visuals
    {
        private static string ConvertToHex(Color c)
        {
            return "#" + ToHex(c.r) + ToHex(c.g) + ToHex(c.b);
        }
        private static string ToHex(float col)
        {
            return ((int)(col * 255)).ToString("X2"); // ("X2");
        }

        public static string Colorize(string text, Color color)     => Colorize(text, ConvertToHex(color));
        public static string Colorize(string text, bool isPositive) => Colorize(text, isPositive ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE);
        public static string Colorize(int value, int min = 0, int max = 100, bool reversed = false) => Colorize(value + "", GetGradientColor(min, max, value, reversed));
        public static string Colorize(string text, string colorHTML)
        {
            return $"<color={colorHTML}>{text}</color>";
        }
        public static string Colorize(long value, bool showSign = false)
        {
            if (value < 0)
                return Negative(value.ToString());

            if (value == 0)
                return Neutral(value.ToString());

            return Positive(showSign ? "+" : "" + value);
        }

        public static string Link(string text) => $"<i><b><color=magenta>{text}</color></b></i>";

        public static string Positive(string text)  => Colorize(text, Colors.COLOR_POSITIVE);
        public static string Neutral(string text)   => Colorize(text, Colors.COLOR_NEUTRAL);
        public static string Negative(string text)  => Colorize(text, Colors.COLOR_NEGATIVE);

        public static Color Positive() => GetColorFromString(Colors.COLOR_POSITIVE);
        public static Color Link() => GetColorFromString(Colors.COLOR_LINK);
        public static Color Neutral() => GetColorFromString(Colors.COLOR_NEUTRAL);
        public static Color Negative() => GetColorFromString(Colors.COLOR_NEGATIVE);

        // TODO used twice in same place
        public static Color GetColorPositiveOrNegative(long value) => GetColorPositiveOrNegative(value > 0);
        public static Color GetColorPositiveOrNegative(bool isPositive)
        {
            string col = isPositive ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE;

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