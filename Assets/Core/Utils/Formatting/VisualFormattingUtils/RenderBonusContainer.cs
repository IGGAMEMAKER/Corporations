namespace Assets.Core
{
    public static partial class Visuals
    {
        public static string RenderBonus(string bonusName, long value, string dimension, bool flipColors, BonusType bonusType, bool minifyValue)
        {
            var text = "";

            if (bonusType == BonusType.Multiplicative)
                text = $"Multiplied by \n{bonusName}: {value}";
            else
                text = $"{bonusName}: {Format.Sign(value, minifyValue)}{dimension}";



            if (!flipColors)
                return RenderNormally(text, value);
            else
                return DescribeReversed(text, value);
        }

        public static string RenderFloatBonus(string bonusName, float value, string dimension, bool flipColors, BonusType bonusType, bool minifyValue)
        {
            var text = "";

            if (bonusType == BonusType.Multiplicative)
                text = $"Multiplied by \n{bonusName}: {value}";
            else
                text = $"{bonusName}: {Format.Sign(value, minifyValue)}{dimension}";



            if (!flipColors)
                return RenderNormally(text, value);
            else
                return DescribeReversed(text, value);
        }

        private static string RenderNormally(string text, long value)
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

        private static string RenderNormally(string text, float value)
        {
            if (value == 0)
                return Neutral(text);

            if (value > 0)
                return Positive(text);

            return Negative(text);
        }

        private static string DescribeReversed(string text, float value)
        {
            if (value == 0)
                return Neutral(text);

            if (value > 0)
                return Negative(text);

            return Positive(text);
        }
    }
}