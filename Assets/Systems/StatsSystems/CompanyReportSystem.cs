using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class CompanyReportSystem : OnPeriodChange
{
    public CompanyReportSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] companies = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.CompanyResource, GameMatcher.MetricsHistory));

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        foreach (var e in companies)
        {
            if (Companies.IsProductCompany(e))
                SaveProductCompanyMetrics(e, date);
            else
                SaveGroupCompanyMetrics(e, date);
        }
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            Date = date,
            AudienceSize = 0,
            Income = Economy.GetCompanyIncome(e, gameContext),
            Profit = Economy.GetProfit(e, gameContext),
            Valuation = Economy.GetCompanyCost(gameContext, e)
        });
    }

    void SaveProductCompanyMetrics (GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            Date = date,
            AudienceSize = MarketingUtils.GetClients(e),
            Income = Economy.GetCompanyIncome(e, gameContext),
            Profit = Economy.GetProfit(e, gameContext),
            Valuation = Economy.GetCompanyCost(gameContext, e)
        });
    }
}
