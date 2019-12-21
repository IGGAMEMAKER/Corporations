using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class AISupportProductsSystem : OnPeriodChange
{
    public AISupportProductsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
        {
            foreach (var p in Companies.GetDaughterProductCompanies(gameContext, c))
                SupportStartup(p, c);
        }
    }


    void SupportStartup(GameEntity product, GameEntity managingCompany)
    {
        if (Companies.BalanceOf(product) > 0 && Economy.IsProfitable(gameContext, product.company.Id))
            return;

        if (!Markets.IsPlayableNiche(gameContext, product.product.Niche))
            return;

        Debug.Log("Support Startup");


        var maintenance = Economy.GetCompanyMaintenance(gameContext, product.company.Id);

        var proposal = new InvestmentProposal
        {
            Offer = maintenance * 4,
            ShareholderId = managingCompany.shareholder.Id,
            InvestorBonus = InvestorBonus.None,
            Valuation = 0,
            WasAccepted = false
        };

        Companies.AddInvestmentProposal(gameContext, product.company.Id, proposal);
    }
}
