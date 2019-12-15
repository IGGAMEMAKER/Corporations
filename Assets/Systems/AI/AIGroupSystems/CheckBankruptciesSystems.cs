using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class CheckBankruptciesSystems : OnPeriodChange
{
    public CheckBankruptciesSystems(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            CloseCompaniesIfNecessary(c);

        foreach (var c in Companies.GetIndependentCompanies(gameContext))
            CheckBankruptcies(c);
    }

    void CloseCompaniesIfNecessary(GameEntity group)
    {
        foreach (var holding in Companies.GetDaughterProductCompanies(gameContext, group))
            CloseCompanyIfNicheIsDeadAndProfitIsNotPositive(holding);
    }

    void CloseCompanyIfNicheIsDeadAndProfitIsNotPositive(GameEntity product)
    {
        var niche = Markets.GetNiche(gameContext, product.product.Niche);

        bool isBankrupt = product.companyResource.Resources.money < 0;

        bool isNotProfitable = !Economy.IsProfitable(gameContext, product.company.Id);
        bool isNicheDead = Markets.GetMarketState(niche) == MarketState.Death;

        if ((isNicheDead && isNotProfitable) || isBankrupt)
            CloseBankruptCompany(product);

        // Support company?
    }

    void CheckBankruptcies(GameEntity company)
    {
        //if (company.hasProduct)
        //    return;

        var isBankrupt = company.companyResource.Resources.money < 0;

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
        //if (CompanyUtils.IsInPlayerSphereOfInterest(product, gameContext))
        //{
        //    //NotificationUtils.AddPopup(gameContext, new PopupMessageCompanyBankrupt(product.company.Id));

        //}
    }
}
