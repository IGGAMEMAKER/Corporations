using Entitas;
using System.Collections.Generic;

//public struct FinancialReport
//{
//    public long ValuationDiff;
//    public long ValuationGrowth;
//    public bool ValuationIsGrowthComputable;

//    public long AudienceDiff;
//    public long AudienceGrowth;
//    public bool AudienceIsGrowthComputable;

//    public long IncomeDiff;
//    public long IncomeGrowth;
//    public bool IncomeIsGrowthComputable;

//    public long ProfitDiff;
//    public long ProfitGrowth;
//    public bool ProfitIsGrowthComputable;
//}


//public static FinancialReport GetAnnualFinancialReport(GameContext context, int companyId)
//{
//    var startYearMetrics = new MetricsInfo { AudienceSize = 1000, Date = 0, Income = 0, Valuation = 100000, Profit = 0 };
//    var endYearMetrics = new MetricsInfo { AudienceSize = 5000, Date = 350, Income = 50000, Valuation = 1000000, Profit = -35000 };

//    var report = new FinancialReport
//    {
//        AudienceDiff = endYearMetrics.AudienceSize - startYearMetrics.AudienceSize,
//        ValuationDiff = endYearMetrics.Valuation - startYearMetrics.Valuation,
//        IncomeDiff = endYearMetrics.Income - startYearMetrics.Income,
//        ProfitDiff = endYearMetrics.Profit - startYearMetrics.Profit,

//        AudienceIsGrowthComputable = startYearMetrics.AudienceSize != 0,
//        ValuationIsGrowthComputable = startYearMetrics.Valuation != 0,
//        IncomeIsGrowthComputable = startYearMetrics.Income != 0,
//        ProfitIsGrowthComputable = startYearMetrics.Profit != 0,
//    };

//    if (report.AudienceIsGrowthComputable)
//        report.AudienceGrowth = endYearMetrics.AudienceSize * 100 / startYearMetrics.AudienceSize;

//    if (report.IncomeIsGrowthComputable)
//        report.IncomeGrowth = endYearMetrics.Income * 100 / startYearMetrics.Income;

//    if (report.ValuationIsGrowthComputable)
//        report.ValuationGrowth = endYearMetrics.Valuation * 100 / startYearMetrics.Valuation;

//    if (report.ProfitIsGrowthComputable)
//        report.ProfitGrowth = endYearMetrics.Profit * 100 / startYearMetrics.Profit;

//    return report;
//}


namespace Assets.Core
{
    public static class CompanyStatisticsUtils
    {
        public static void AddMetrics(GameContext context, GameEntity c, MetricsInfo metricsInfo)
        {
            var metrics = c.metricsHistory;

            metrics.Metrics.Add(metricsInfo);

            c.ReplaceMetricsHistory(metrics.Metrics);
        }


        public static AnnualReport GetMockReport() => new AnnualReport
        {
            Groups = new List<ReportData>(),
            Products = new List<ReportData>(),
            People = new List<ReportData>(),
            Date = 0
        };



        public static AnnualReport GetCurrentAnnualReport(GameContext gameContext)
        {
            var reports = GetAnnualReports(gameContext);

            if (reports.Count == 0)
                return GetMockReport();

            return reports[reports.Count - 1];
        }

        public static AnnualReport GetPreviousAnnualReport(GameContext gameContext)
        {
            var reports = GetAnnualReports(gameContext);

            if (reports.Count <= 1)
                return GetMockReport();

            return reports[reports.Count - 2];
        }

        public static List<AnnualReport> GetAnnualReports(GameContext gameContext)
        {
            var reportContainer = gameContext.GetEntities(GameMatcher.Reports)[0];

            return reportContainer.reports.AnnualReports;
        }


        // phase
        public static long GetAudienceGrowth(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].AudienceSize + 1;
            var now = metrics[len - 1].AudienceSize + 1;

            return (now - was) * 100 / was;
        }

        public static long GetValuationGrowth(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].Valuation + 1;
            var now = metrics[len - 1].Valuation + 1;

            return (now - was) * 100 / was;
        }

        public static long GetIncomeGrowth(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].Income + 1;
            var now = metrics[len - 1].Income + 1;

            return (now - was) * 100 / was;
        }

        public static long GetIncomeGrowthAbsolute(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].Income + 1;
            var now = metrics[len - 1].Income + 1;

            return now - was;
        }


        public static bool GetLastYearMetrics(MetricsInfo metricsInfo, int currentDate)
        {
            return metricsInfo.Date > currentDate - 360;
        }

        public static bool GetLastCalendarYearMetrics(MetricsInfo metricsInfo, int currentDate)
        {
            var year = GetYear(currentDate);
            var date = metricsInfo.Date;

            return date > year * 360 && date <= currentDate;
        }

        public static bool GetLastCalendarQuarterMetrics(MetricsInfo metricsInfo, int currentDate)
        {
            var year = GetYear(currentDate);
            var quarter = (currentDate - year * 360) / 90;

            var date = metricsInfo.Date;

            return date > year * 360 + quarter * 90 && date <= currentDate;
        }


        internal static int GetYear(int currentDate)
        {
            return currentDate / 360;
        }

        internal static int GetMonth(int currentDate)
        {
            var year = GetYear(currentDate);

            return (currentDate - year * 360) / 30;
        }

        internal static int GetTotalMonth(int currentDate)
        {
            return currentDate / 30;
        }
    }
}
