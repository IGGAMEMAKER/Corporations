using Assets.Utils;
using System.Linq;
using UnityEngine;

public partial class AIProductSystems
{
    void Operate(GameEntity product)
    {
        PickTeamUpgrades(product);


        PayDividendsIfPossible(product);

        PromoteToGroupIfPossible(product);
    }

    void PromoteToGroupIfPossible(GameEntity product)
    {
        if (!product.isIndependentCompany)
            return;

        var canGrow = GetProfit(product) > 1000000;

        var ambitions = HumanUtils.GetFounderAmbition(gameContext, product.cEO.HumanId);
        var wantsToGrow = ambitions != Ambition.RuleProductCompany;
            
        if (canGrow && wantsToGrow)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, product.company.Id);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;

        if (EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        var maintenance = EconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        long dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
