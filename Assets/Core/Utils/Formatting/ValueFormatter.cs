using System;

namespace Assets.Core
{
    public struct DateDescription
    {
        public int day;
        public int month;
        public string monthLiteral;
        public int year;
    }
    public static class Format
    {
        public static string ShowChange(float value)
        {
            return value.ToString("+0.0;-#");
        }
        
        public static string Money<T>(T value, bool minifyToInteger = false)
        {
            if (minifyToInteger)
                return $"${MinifyToInteger(value)}";
            else
                return $"${Minify(value)}";
        }
        
        public static string Sign(float value)
        {
            return SignOf(value) + value.ToString("0.00");
        }

        public static string Sign(long value, bool minify = false)
        {
            return SignOf(value) + Minify(value, minify);
        }

        public static string SignOf<T>(T value)
        {
            return (dynamic)value > 0 ? "+" : "";
        }

        public static string Minify<T>(T value, bool minify = true)
        {
            long.TryParse(value.ToString(), out long val);

            if (!minify)
                return val.ToString();

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

            if (Math.Abs(val) >= trillion)
                return ShowMeaningfulValue(val, trillion, "T");

            if (Math.Abs(val) >= billion)
                return ShowMeaningfulValue(val, billion, "B");

            if (Math.Abs(val) >= million)
                return ShowMeaningfulValue(val, million, "M");

            if (Math.Abs(val) >= thousand)
                return ShowMeaningfulValue(val, thousand, "K");

            return val.ToString();
        }
        
        private static string ShowMeaningfulValue(long value, long divisor, string litera)
        {
            int shortened = Convert.ToInt32(value * 10 / divisor);

            return shortened / 10f + litera;
        }
        
        public static string MinifyToInteger<T>(T value, bool minify = true)
        {
            long.TryParse(value.ToString(), out long val);

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

            if (!minify)
                return val.ToString();

            if (Math.Abs(val) >= trillion)
                return ShowPrettyValue(val, trillion, "T");

            if (Math.Abs(val) >= billion)
                return ShowPrettyValue(val, billion, "B");

            if (Math.Abs(val) >= million)
                return ShowPrettyValue(val, million, "M");

            if (Math.Abs(val) >= thousand)
                return ShowPrettyValue(val, thousand, "K");

            return val.ToString();
        }
        
        private static string ShowPrettyValue(long value, long divisor, string litera)
        {
            int shortened = Convert.ToInt32(value / divisor);

            return shortened + litera;
        }


        // dates
        public static DateDescription GetDateDescription(int date)
        {
            var year = C.START_YEAR + date / 360;
            var day = date % 360;
            var month = day / 30;

            day = day % 30;

            return new DateDescription
            {
                day = day,
                month = month,
                monthLiteral = GetMonthLiteral(month).Substring(0, 3),
                year = year
            };
        }

        public static string FormatDate(int date, bool withYear = true)
        {
            var description = GetDateDescription(date);

            var dateString = $"{description.day + 1:00} {description.monthLiteral:000000000} ";

            if (withYear)
                dateString += $"{description.year:0000}";

            return dateString;
        }

        public static string GetMonthLiteral(int month)
        {
            switch (month)
            {
                case 0: return "January";
                case 1: return "February";
                case 2: return "March";
                case 3: return "April";
                case 4: return "May";
                case 5: return "June";
                case 6: return "July";
                case 7: return "August";
                case 8: return "September";
                case 9: return "October";
                case 10: return "November";
                case 11: return "December";
                default: return "UNKNOWN MONTH";
            }
        }
    }
}
