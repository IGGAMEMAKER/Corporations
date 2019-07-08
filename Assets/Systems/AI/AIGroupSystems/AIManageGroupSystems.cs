using System;
using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public partial class AIManageGroupSystems : OnQuarterChange
{
    public AIManageGroupSystems(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            ManageGroup(c);
    }

    void ManageGroup(GameEntity group)
    {
        foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
        {
            PayDividends(holding, group);
        }
    }

    void DemandMoneyFromInvestors(GameEntity group)
    {

    }

    void CloseCompany(GameEntity product, GameEntity group)
    {
        CompanyUtils.CloseCompany(gameContext, product);
    }

    void PayDividends(GameEntity product, GameEntity group)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, group, product))
            return;

        var dividends = product.companyResource.Resources.money * 75 / 100;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
