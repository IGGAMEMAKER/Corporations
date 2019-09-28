using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Operate(GameEntity company)
    {
        PayDividendsIfPossible(company);

        PromoteToGroupIfPossible(company);

        ManageInvestors(company);
        //InvestmentUtils.CompleteGoal(e, gameContext, false);
    }

    void PromoteToGroupIfPossible(GameEntity company)
    {
        if (!company.isIndependentCompany)
            return;

        var profit = CompanyEconomyUtils.GetBalanceChange(company, gameContext);
        var canGrow = profit > 1000000;

        var ambitions = HumanUtils.GetFounderAmbition(gameContext, company.cEO.HumanId);
        var wantsToGrow = ambitions != Ambition.RuleProductCompany;
            
        if (canGrow && wantsToGrow)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        if (product.isIndependentCompany || product.isAggressiveMarketing)
            return;

        var maintenance = CompanyEconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        var dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }

    bool IsCompanyNeedsInvestments()
    {
        return true;
    }

    void TakeInvestments(GameEntity company)
    {
        // ??????

        bool isInvestmentsAreNecessary = IsCompanyNeedsInvestments();

        var list = CompanyUtils.GetPotentialInvestorsWhoAreReadyToInvest(gameContext, company.company.Id);

        if (list.Length == 0)
            return;

        CompanyUtils.StartInvestmentRound(company, gameContext);

        foreach (var s in list)
        {
            var investorShareholderId = s.shareholder.Id;
            var companyId = company.company.Id;

            var proposal = CompanyUtils.GetInvestmentProposal(gameContext, companyId, investorShareholderId);
            if (proposal == null)
                return;

            Print($"Took investments from {s.shareholder.Name}. Offer: {Format.Money(proposal.Offer)}", company);

            CompanyUtils.AcceptProposal(gameContext, companyId, investorShareholderId);
        }
    }
}
