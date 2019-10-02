using Assets.Utils;
using UnityEngine;

public partial class AIProductSystems
{
    void Operate(GameEntity product)
    {
        ScaleTeamIfPossible(product);

        PickTeamUpgrades(product);


        PayDividendsIfPossible(product);

        PromoteToGroupIfPossible(product);

        ManageInvestors(product);

        //InvestmentUtils.CompleteGoal(e, gameContext, false);
    }

    void PromoteToGroupIfPossible(GameEntity product)
    {
        if (!product.isIndependentCompany)
            return;

        var profit = GetProfit(product);
        var canGrow = profit > 1000000;

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

    bool IsCompanyNeedsInvestments()
    {
        return true;
    }

    void TakeInvestments(GameEntity product)
    {
        // ??????

        bool isInvestmentsAreNecessary = IsCompanyNeedsInvestments();

        var list = CompanyUtils.GetPotentialInvestorsWhoAreReadyToInvest(gameContext, product.company.Id);

        if (list.Length == 0)
            return;

        CompanyUtils.StartInvestmentRound(product, gameContext);

        foreach (var s in list)
        {
            var investorShareholderId = s.shareholder.Id;
            var companyId = product.company.Id;

            var proposal = CompanyUtils.GetInvestmentProposal(gameContext, companyId, investorShareholderId);
            if (proposal == null)
                return;

            Print($"Took investments from {s.shareholder.Name}. Offer: {Format.Money(proposal.Offer)}", product);

            CompanyUtils.AcceptProposal(gameContext, companyId, investorShareholderId);
        }
    }
}
