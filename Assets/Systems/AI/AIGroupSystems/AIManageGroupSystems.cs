using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

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
        DefendMarkets(group);
        ExpandSphereOfInfluence(group);
        FillUnoccupiedMarkets(group);

        CloseCompaniesIfNecessary(group);
    }

    void CloseCompaniesIfNecessary(GameEntity group)
    {
        foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
        {
            if (holding.hasProduct)
                CloseCompanyIfNicheIsDeadAndProfitIsNotPositive(holding);
        }
    }

    void CloseCompanyIfNicheIsDeadAndProfitIsNotPositive (GameEntity product)
    {
        var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

        bool isBankrupt = product.companyResource.Resources.money < 0;

        bool isNotProfitable = !CompanyEconomyUtils.IsProfitable(gameContext, product.company.Id);
        bool isNicheDead = NicheUtils.GetMarketState(niche) == NicheLifecyclePhase.Death;

        if ((isNicheDead && isNotProfitable) || isBankrupt)
            CompanyUtils.CloseCompany(gameContext, product);
    }


    void SupportStartup(GameEntity product, GameEntity managingCompany)
    {
        if (product.companyResource.Resources.money > 0 && CompanyEconomyUtils.IsProfitable(gameContext, product.company.Id))
            return;

        Debug.Log("Support Startup");

        var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);
        var phase = NicheUtils.GetMarketState(niche);

        if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Decay || phase == NicheLifecyclePhase.Idle)
            return;

        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(gameContext, product.company.Id);

        var proposal = new InvestmentProposal
        {
            Offer = maintenance * 4,
            ShareholderId = managingCompany.shareholder.Id,
            InvestorBonus = InvestorBonus.None,
            Valuation = 0,
            WasAccepted = false
        };

        CompanyUtils.AddInvestmentProposal(gameContext, product.company.Id, proposal);
    }
}
