using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void BecomeProfitable(GameEntity company)
    {
        ManageSmallTeam(company);

        StayInMarket(company);

        StartTargetingCampaign(company);

        TakeInvestments(company);
    }

    void TakeInvestments(GameEntity company)
    {
        // ??????

        bool isInvestmentsAreNecessary = true;

        var list = CompanyUtils.GetPotentialInvestorsWhoAreReadyToInvest(gameContext, company.company.Id);

        if (list.Length == 0)
            return;

        CompanyUtils.StartInvestmentRound(company);

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
