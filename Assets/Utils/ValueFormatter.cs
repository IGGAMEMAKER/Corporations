namespace Assets.Utils
{
    public static class ValueFormatter
    {
        public static string ShortenValueMockup<T>(T value)
        {
            return NoFormatting(value);
            return Shorten(value);
        }

        public static string Shorten<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

            if (val >= trillion)
                return (int)(val / trillion) + "T";

            if (val >= billion)
                return (int)(val / billion) + "B";

            if (val >= million)
                return (int)(val / million) + "M";

            if (val >= thousand * 10)
                return (int)(val / thousand) + "K";

            return val.ToString();
        }

        public static string NoFormatting<T>(T value)
        {
            return value.ToString();
        }
    }

}
