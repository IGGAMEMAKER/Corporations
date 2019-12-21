using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class AISupportProductsSystem : OnPeriodChange
{
    public AISupportProductsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var p in Companies.GetDependentProducts(gameContext))
            SupportStartup(p);
    }

    public long GetNecessaryAmountOfMoney(GameEntity product)
    {
        return Economy.GetCompanyMaintenance(gameContext, product.company.Id);
    }

    void SupportStartup(GameEntity product)
    {
        Debug.Log("Support Startup");

        var managingCompany = Companies.GetManagingCompanyOf(product, gameContext);


        // calculate startup goal

        // if it's vital interest
        // give them a lot of money

        // otherwise
        // give them decent amount of money

        var proposal = new InvestmentProposal
        {
            Offer = GetNecessaryAmountOfMoney(product),
            ShareholderId = managingCompany.shareholder.Id,
            InvestorBonus = InvestorBonus.None,
            Valuation = 0,
            WasAccepted = false
        };

        Companies.AddInvestmentProposal(gameContext, product.company.Id, proposal);
        Companies.AcceptInvestmentProposal(gameContext, product.company.Id, managingCompany.shareholder.Id);
    }
}
