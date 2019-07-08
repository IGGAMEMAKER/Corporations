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
            if (!holding.hasProduct)
                continue;

            PayDividends(holding);

            CloseCompanyIfNicheIsDeadAndProfitIsNotPositive(holding);
        }
    }

    void DemandMoneyFromInvestors(GameEntity group)
    {

    }

    void CloseCompanyIfNicheIsDeadAndProfitIsNotPositive (GameEntity product)
    {
        var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

        if (niche.nicheState.Phase == NicheLifecyclePhase.Death && !CompanyEconomyUtils.IsProfitable(gameContext, product.company.Id))
            CloseCompany(product);
    }

    void CloseCompany(GameEntity product)
    {
        CompanyUtils.CloseCompany(gameContext, product);
    }

    void PayDividends(GameEntity product)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        var dividends = product.companyResource.Resources.money * 75 / 100;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
