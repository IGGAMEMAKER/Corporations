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

            SupportHolding(holding, group);

            CloseCompanyIfNicheIsDeadAndProfitIsNotPositive(holding);
        }
    }

    void SupportHolding(GameEntity holding, GameEntity group)
    {
        if (holding.hasProduct)
        {
            SupportStartup(holding, group);
            return;
        }


    }

    void SupportStartup(GameEntity product, GameEntity managingCompany)
    {
        var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);
        var phase = niche.nicheState.Phase;

        if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Decay || phase == NicheLifecyclePhase.Idle)
            return;

        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(gameContext, product.company.Id);

        var proposal = new InvestmentProposal {
            Offer = maintenance * 4,
            ShareholderId = managingCompany.shareholder.Id,
            InvestorBonus = InvestorBonus.None,
            Valuation = 0,
            WasAccepted = false
        };

        CompanyUtils.AddInvestmentProposal(gameContext, product.company.Id, proposal);
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
