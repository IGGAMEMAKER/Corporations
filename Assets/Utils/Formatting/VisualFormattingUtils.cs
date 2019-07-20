using System;
using UnityEngine;

namespace Assets.Utils
{
    public static class Visuals
    {
        public static string Link(string text)
        {
            return $"<i><b><color=magenta>{text}</color></b></i>";
        }

        private static String HexConverter(Color c)
        {
            return "#" + ToHex(c.r) + ToHex(c.g) + ToHex(c.b);
        }

        static string ToHex(float col)
        {
            return ((int)(col * 255)).ToString("X2"); // ("X2");
        }

        public static string Colorize(string text, Color color)
        {
            var colorName = HexConverter(color);

            //Debug.Log(color.ToString());
            //Debug.Log("Colorize from color, " + text + ": " + colorName);

            return Colorize(text, colorName);
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

        #region gradient

        internal static string Gradient(float min, float max, float value, string text = "")
        {
            var color = GetGradientColor(min, max, value, false);

            var colorName = GetGradientStringName(color);

            return Colorize(text.Length == 0 ? value.ToString() : text, colorName);

            //return $"<color={colorName}>{()}</color>";
        }


        public static Color GetGradientColor(float min, float max, float val, bool reversed = false)
        {
            float percent = (val - min) / (max - min);

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

        static string GetGradientStringName(Color color)
        {
            return VisualConstants.COLOR_POSITIVE;
        }

        #endregion


        public static string Neutral(string text)
        {
            return Colorize(text, VisualConstants.COLOR_NEUTRAL);
        }


        public static Color Color(string color)
        {
            ColorUtility.TryParseHtmlString(color, out Color c);

            return c;
        }

        internal static string Sign(long val)
        {
            return Format.Sign(val);
        }

        internal static string Negative(string text)
        {
            return Colorize(text, VisualConstants.COLOR_NEGATIVE);
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

            text += $"{bonusName}: {Format.Sign(value)}{dimension}";

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

        public static string PositiveOrNegative(long value)
        {
            if (value > 0)
                return Positive(Sign(value));

            if (value == 0)
                return "";

            return Negative(value.ToString());
        }

        public static string Describe(long value, string positiveText, string negativeText, string neutralText = "")
        {
            if (value == 0)
                return Neutral(neutralText);

            if (value > 0)
                return Positive(positiveText);

            return Negative(negativeText);
        }
    }
}