using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class AICloseUnworthyProductsSystem : OnPeriodChange
{
    public AICloseUnworthyProductsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            CloseCompaniesIfNecessary(c);
    }

    void CloseCompaniesIfNecessary(GameEntity group)
    {
        foreach (var holding in Companies.GetDaughterProductCompanies(gameContext, group))
            CloseUnworthyProducts(holding);
    }

    void CloseUnworthyProducts(GameEntity product)
    {
        var niche = Markets.GetNiche(gameContext, product.product.Niche);

        bool isProfitable = Economy.IsProfitable(gameContext, product.company.Id);
        bool isNicheDead = Markets.GetMarketState(niche) == MarketState.Death;

        if (isNicheDead && !isProfitable)
            Companies.CloseCompany(gameContext, product);
    }
}
public partial class CheckBankruptciesSystem : OnPeriodChange
{
    public CheckBankruptciesSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {


        foreach (var c in Companies.GetIndependentCompanies(gameContext))
        {
            if (Companies.IsFinancialStructure(c))
                continue;

            CheckBankruptcies(c);
        }
    }





    void CheckBankruptcies(GameEntity company)
    {
        var isBankrupt = Companies.BalanceOf(company) < 0;

        if (!isBankrupt)
            return;

        Debug.Log("Bankrupt: " + company.company.Name);

        if (Companies.IsPlayerCompany(gameContext, company))
            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));

        Companies.CloseCompany(gameContext, company);
    }
}
