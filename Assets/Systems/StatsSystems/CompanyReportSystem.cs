﻿using Assets.Core;
using Entitas;
using System.Collections.Generic;

class CompanyReportSystem : OnPeriodChange
{
    public CompanyReportSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] companies = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.CompanyResource, GameMatcher.MetricsHistory));

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        var products = Companies.GetProductCompanies(gameContext);
        //var groups

        return;
        //foreach (var e in companies)
        //{
        //    if (Companies.IsProductCompany(e))
        //        SaveProductCompanyMetrics(e, date);
        //    else
        //        SaveGroupCompanyMetrics(e, date);
        //}
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            Date = date,
            AudienceSize = 0,
            Income = Economy.GetIncome(gameContext, e),
            Profit = Economy.GetProfit(gameContext, e),
            Valuation = Economy.CostOf(e, gameContext)
        });
    }

    void SaveProductCompanyMetrics (GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            Date = date,
            AudienceSize = Marketing.GetUsers(e),
            Income = Economy.GetIncome(gameContext, e),
            Profit = Economy.GetProfit(gameContext, e),
            Valuation = Economy.CostOf(e, gameContext)
        });
    }
}
