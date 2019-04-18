using Assets.Utils;
using Assets.Utils.Economy;
using Entitas;
using System.Collections.Generic;

class CompanyReportSystem : OnMonthChange
{
    public CompanyReportSystem(Contexts contexts) : base(contexts)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.CompanyResource));

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        foreach (var e in Products)
        {
            if (CompanyUtils.IsProductCompany(e))
                SaveProductCompanyMetrics(e, date);
            else
                SaveGroupCompanyMetrics(e, date);
        }
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e.company.Id, new MetricsInfo
        {
            AudienceSize = 0,
            Date = date,
            Income = CompanyEconomyUtils.GetCompanyIncome(e, gameContext),
            Profit = CompanyEconomyUtils.GetBalanceChange(e, gameContext),
            Valuation = CompanyEconomyUtils.GetCompanyCost(gameContext, e.company.Id)
        });
    }

    void SaveProductCompanyMetrics (GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e.company.Id, new MetricsInfo
        {
            AudienceSize = e.marketing.Clients,
            Date = date,
            Income = CompanyEconomyUtils.GetCompanyIncome(e, gameContext),
            Profit = CompanyEconomyUtils.GetBalanceChange(e, gameContext),
            Valuation = CompanyEconomyUtils.GetCompanyCost(gameContext, e.company.Id)
        });
    }
}
