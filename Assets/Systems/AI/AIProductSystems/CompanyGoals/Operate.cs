using Assets.Utils;
using System.Linq;
using UnityEngine;

public partial class AIProductSystems
{
    void Operate(GameEntity product)
    {
        //ScaleTeamIfPossible(product);

        PickTeamUpgrades(product);


        PayDividendsIfPossible(product);

        PromoteToGroupIfPossible(product);

        // taking investments
        TakeInvestments(product);

        //InvestmentUtils.CompleteGoal(e, gameContext, false);
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

    void TakeInvestments(GameEntity product)
    {
        // ??????

        var list = CompanyUtils.GetPotentialInvestors(gameContext, product.company.Id);

        var investors = string.Join(",", list.Select(l => l.shareholder.Name));
        Print("Take Investments: " + list.Length + " " + investors , product);

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
