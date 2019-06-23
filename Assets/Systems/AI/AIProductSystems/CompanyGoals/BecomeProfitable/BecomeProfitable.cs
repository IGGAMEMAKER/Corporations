using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void BecomeProfitable(GameEntity company)
    {
        ManageTeam(company);

        StayInMarket(company);

        StartTargetingCampaign(company);

        TakeInvestments(company);
    }

    void TakeInvestments(GameEntity company)
    {
        // ??????

        //InvestmentUtils.
    }
}
