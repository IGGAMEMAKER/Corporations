using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class CheckBankruptciesSystem : OnPeriodChange
{
    public CheckBankruptciesSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            CloseCompaniesIfNecessary(c);

        foreach (var c in Companies.GetIndependentCompanies(gameContext))
        {
            if (Companies.IsFinancialStructure(c))
                continue;

            CheckBankruptcies(c);
        }
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
            CloseBankruptCompany(product);

        // Support company?
    }

    void CheckBankruptcies(GameEntity company)
    {
        //if (company.hasProduct)
        //    return;

        var isBankrupt = Companies.BalanceOf(company) < 0;

        if (!isBankrupt)
            return;

        Debug.Log("Check Bankruptcy for " + company.company.Name);

        if (Companies.IsPlayerCompany(gameContext, company))
            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));

        CloseBankruptCompany(company);
    }

    void CloseBankruptCompany(GameEntity company)
    {
        Companies.CloseCompany(gameContext, company);

        NotificationUtils.AddNotification(gameContext, new NotificationMessageBankruptcy(company.company.Id));

        if (Companies.IsInPlayerSphereOfInterest(company, gameContext))
            NotificationUtils.AddPopup(gameContext, new PopupMessageCompanyBankrupt(company.company.Id));
    }
}
