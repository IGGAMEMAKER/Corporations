using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void BecomeProfitable(GameEntity company)
    {
        StayInMarket(company);

        TakeInvestments(company);
    }

    bool IsCompanyNeedsInvestments()
    {
        return true;
    }

    long GetRequiredAmountOfMoney(GameEntity company)
    {
        switch (company.companyGoal.InvestorGoal)
        {
            case InvestorGoal.BecomeProfitable:

                break;

            case InvestorGoal.BecomeMarketFit:

                break;

            case InvestorGoal.Operationing:

                break;
        }

        return 0;
    }

    void TakeMoneyFromExistingInvestors(GameEntity company)
    {
        var balance = company.companyResource.Resources.money;
        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(gameContext, company.company.Id);

        foreach (var s in company.shareholders.Shareholders)
        {

        }
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
