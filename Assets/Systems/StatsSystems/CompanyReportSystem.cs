using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class CompanyReportSystem : OnMonthChange
{
    public CompanyReportSystem(Contexts contexts) : base(contexts)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] companies = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.CompanyResource, GameMatcher.MetricsHistory));

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        foreach (var e in companies)
        {
            if (CompanyUtils.IsProductCompany(e))
                SaveProductCompanyMetrics(e, date);
            else
                SaveGroupCompanyMetrics(e, date);
        }
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            AudienceSize = 0,
            Date = date,
            Income = EconomyUtils.GetCompanyIncome(e, gameContext),
            Profit = EconomyUtils.GetProfit(e, gameContext),
            Valuation = EconomyUtils.GetCompanyCost(gameContext, e)
        });
    }

    void SaveProductCompanyMetrics (GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            AudienceSize = MarketingUtils.GetClients(e),
            Date = date,
            Income = EconomyUtils.GetCompanyIncome(e, gameContext),
            Profit = EconomyUtils.GetProfit(e, gameContext),
            Valuation = EconomyUtils.GetCompanyCost(gameContext, e)
        });
    }
}
