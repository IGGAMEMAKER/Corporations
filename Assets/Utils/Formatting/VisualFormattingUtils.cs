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

        // TODO used twice in same place
        public static Color GetColorPositiveOrNegative(long value)
        {
            string col = value > 0 ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE;

            return GetColorFromString(col);
        }


        public static string Positive(string text) => Colorize(text, VisualConstants.COLOR_POSITIVE);
        public static string Neutral(string text) => Colorize(text, VisualConstants.COLOR_NEUTRAL);
        public static string Negative(string text) => Colorize(text, VisualConstants.COLOR_NEGATIVE);

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



        public static Color GetColorFromString(string color)
        {
            ColorUtility.TryParseHtmlString(color, out Color c);

            return c;
        }

        internal static string Sign(long val) => Format.Sign(val);


        public static string RenderBonus(string bonusName, long value, string dimension, bool flipColors, BonusType bonusType)
        {
            var text = "";

            if (bonusType == BonusType.Multiplicative)
                text = $"Multiplied by \n{bonusName}: {value}";
            else
                text += $"{bonusName}: {Format.Sign(value)}{dimension}";



            if (!flipColors)
                return DescribeNormally(text, value);
            else
                return DescribeReversed(text, value);
        }

        public static string DescribeValueWithText(long value, string positiveText, string negativeText, string neutralText = "")
        {
            if (value == 0)
                return Neutral(neutralText);

            if (value > 0)
                return Positive(positiveText);

            return Negative(negativeText);
        }


        private static string DescribeNormally(string text, long value)
        {
            if (value == 0)
                return Neutral(text);

            if (value > 0)
                return Positive(text);

            return Negative(text);
        }
        private static string DescribeReversed(string text, long value)
        {
            if (value == 0)
                return Neutral(text);

            if (value > 0)
                return Negative(text);

            return Positive(text);
        }

        public static string PositiveOrNegativeMinified(long value)
        {
            if (value > 0)
                return Positive(Format.SignMinified(value));

            if (value == 0)
                return "0";

            return Negative(Format.SignMinified(value));
        }

    }
}