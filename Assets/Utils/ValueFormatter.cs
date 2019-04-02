namespace Assets.Utils
{
    public static class ValueFormatter
    {
        public static string ShortenValue<T>(T value)
        {
            return value.ToString();

            long.TryParse(value.ToString(), out long val);

            long trillion = 1000000000000;
            long billion = 1000000000;
            long million = 1000000;
            long thousand = 1000;

            if (val > trillion)
                return (int)(val / trillion) + "T";

            if (val > billion)
                return (int)(val / billion) + "B";

            if (val > million)
                return (int)(val / million) + "M";

            if (val > thousand * 10)
                return (int)(val / thousand) + "k";

            return val.ToString();
        }

        public static string Shorten<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

            long trillion = 1000000000000;
            long billion = 1000000000;
            long million = 1000000;
            long thousand = 1000;

            if (val > trillion)
                return (int)(val / trillion) + "T";

            if (val > billion)
                return (int)(val / billion) + "B";

            if (val > million)
                return (int)(val / million) + "M";

            if (val > thousand * 10)
                return (int)(val / thousand) + "k";

            return val.ToString();
        }

        public static string NoFormatting<T>(T value)
        {
            return value.ToString();
        }
    }

}
