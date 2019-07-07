using System;

namespace Assets.Utils
{
    public static class Format
    {
        private static string ShowMeaningfulValue(long value, long divisor, string litera)
        {
            int shortened = Convert.ToInt32(value * 10 / divisor);

            return "" + shortened / 10f + litera;
        }

        private static string ShowPrettyValue(long value, long divisor, string litera)
        {
            int shortened = Convert.ToInt32(value / divisor);

            return "" + shortened + litera;
        }

        public static string Sign(long value)
        {
            return value > 0 ? $"+{value}" : value.ToString();
        }

        public static string Money<T>(T value)
        {
            return $"${Minify(value)}";
        }

        public static string MinifyToInteger<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

            if (val >= trillion)
                return ShowPrettyValue(val, trillion, "T");

            if (val >= billion)
                return ShowPrettyValue(val, billion, "B");

            if (val >= million)
                return ShowPrettyValue(val, million, "M");

            if (val >= thousand)
                return ShowPrettyValue(val, thousand, "K");

            return val.ToString();
        }

        public static string Minify<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

            if (val >= trillion)
                return ShowMeaningfulValue(val, trillion, "T");

            if (val >= billion)
                return ShowMeaningfulValue(val, billion, "B");

            if (val >= million)
                return ShowMeaningfulValue(val, million, "M");

            if (val >= thousand)
                return ShowMeaningfulValue(val, thousand, "K");

            return val.ToString();
        }

        public static string NoFormatting<T>(T value)
        {
            return value.ToString();
        }

        //public static string ShortenSigned<T>(T value)
        //{
        //    long.TryParse(value.ToString(), out long val);

        //    return Sign(val);
        //}
    }
}
