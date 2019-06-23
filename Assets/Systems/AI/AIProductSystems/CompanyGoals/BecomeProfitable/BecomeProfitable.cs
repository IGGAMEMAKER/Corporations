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

        if (list.Length > 0)
        {
            CompanyUtils.StartInvestmentRound(company);

            foreach (var s in list)
                CompanyUtils.AcceptProposal(gameContext, company.company.Id, s.shareholder.Id);
        }
    }
}
