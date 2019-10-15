using System;
using UnityEngine;

namespace Assets.Utils
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

        public static string SignMinified(long value)
        {
            return (value > 0 ? "+" : "") + Minify(value);
        }

        public static string Money<T>(T value)
        {
            return $"${Minify(value)}";
        }

        public static string MoneyToInteger<T>(T value)
        {
            return $"${MinifyToInteger(value)}";
        }

        public static string MinifyToInteger<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

            long thousand = 1000;
            long million = 1000 * thousand;
            long billion = 1000 * million;
            long trillion = 1000 * billion;

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

        public static string Minify<T>(T value)
        {
            long.TryParse(value.ToString(), out long val);

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

        public static string NoFormatting<T>(T value)
        {
            return value.ToString();
        }

        //public static string ShortenSigned<T>(T value)
        //{
        //    long.TryParse(value.ToString(), out long val);

        //    return Sign(val);
        //}

        // dates
        public static DateDescription GetDateDescription(int date)
        {
            var year = Constants.START_YEAR + date / 360;
            var day = date % 360;
            var month = day / 30;

            day = day % 30;

            return new DateDescription
            {
                day = day,
                month = month,
                monthLiteral = GetMonthLiteral(month),
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






        public static void Print(string action, GameEntity company)
        {
            //bool isMyCompany = company.isControlledByPlayer || CompanyUtils.IsCompanyRelatedToPlayer(, company);
            //bool isMyCompetitor = false; // player != null && company.product.Niche == player.product.Niche;

            //bool canRenderMyCompany = GetLog(LogTypes.MyProductCompany) && isMyCompany;
            //bool canRenderMyCompetitors = GetLog(LogTypes.MyProductCompanyCompetitors) && isMyCompetitor;

            string companyName = company.company.Name;
            //if (isMyCompany)
            companyName = Visuals.Colorize(company.company.Name, VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO);

            Debug.Log(companyName + " " + action);
        }
    }
}
