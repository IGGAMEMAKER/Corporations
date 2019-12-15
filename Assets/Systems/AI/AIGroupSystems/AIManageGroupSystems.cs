using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public partial class AIManageGroupSystems : OnQuarterChange
{
    public AIManageGroupSystems(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            ManageGroup(c);
    }

    void ManageGroup(GameEntity group)
    {
        ExpandSphereOfInfluence(group);
        FillUnoccupiedMarkets(group);
    }

    void SupportStartup(GameEntity product, GameEntity managingCompany)
    {
        if (product.companyResource.Resources.money > 0 && Economy.IsProfitable(gameContext, product.company.Id))
            return;

        Debug.Log("Support Startup");

        var niche = Markets.GetNiche(gameContext, product.product.Niche);
        var phase = Markets.GetMarketState(niche);

        
        if (!Markets.IsPlayableNiche(gameContext, product.product.Niche))
            return;

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
