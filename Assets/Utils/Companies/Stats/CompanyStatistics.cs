public struct FinancialReport
{
    public long ValuationDiff;
    public long ValuationGrowth;
    public bool ValuationIsGrowthComputable;

    public long AudienceDiff;
    public long AudienceGrowth;
    public bool AudienceIsGrowthComputable;

    public long IncomeDiff;
    public long IncomeGrowth;
    public bool IncomeIsGrowthComputable;

    public long ProfitDiff;
    public long ProfitGrowth;
    public bool ProfitIsGrowthComputable;
}

namespace Assets.Utils
{
    public static class CompanyStatisticsUtils
    {
        public static void AddMetrics(GameContext context, int companyId, MetricsInfo metricsInfo)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            var metrics = c.metricsHistory;

            metrics.Metrics.Add(metricsInfo);

            c.ReplaceMetricsHistory(metrics.Metrics);
        }

        public static FinancialReport GetAnnualFinancialReport(GameContext context, int companyId)
        {
            var startYearMetrics = new MetricsInfo { AudienceSize = 1000, Date = 0, Income = 0, Valuation = 100000, Profit = 0 };
            var endYearMetrics = new MetricsInfo { AudienceSize = 5000, Date = 350, Income = 50000, Valuation = 1000000, Profit = -35000 };

            var report = new FinancialReport
            {
                AudienceDiff = endYearMetrics.AudienceSize - startYearMetrics.AudienceSize,
                ValuationDiff = endYearMetrics.Valuation - startYearMetrics.Valuation,
                IncomeDiff = endYearMetrics.Income - startYearMetrics.Income,
                ProfitDiff = endYearMetrics.Profit - startYearMetrics.Profit,

                AudienceIsGrowthComputable = startYearMetrics.AudienceSize != 0,
                ValuationIsGrowthComputable = startYearMetrics.Valuation != 0,
                IncomeIsGrowthComputable = startYearMetrics.Income != 0,
                ProfitIsGrowthComputable = startYearMetrics.Profit != 0,
            };

            if (report.AudienceIsGrowthComputable)
                report.AudienceGrowth = endYearMetrics.AudienceSize * 100 / startYearMetrics.AudienceSize;

            if (report.IncomeIsGrowthComputable)
                report.IncomeGrowth = endYearMetrics.Income * 100 / startYearMetrics.Income;

            if (report.ValuationIsGrowthComputable)
                report.ValuationGrowth = endYearMetrics.Valuation * 100 / startYearMetrics.Valuation;

            if (report.ProfitIsGrowthComputable)
                report.ProfitGrowth = endYearMetrics.Profit * 100 / startYearMetrics.Profit;

            return report;
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
    }
}
