using Assets.Classes;
using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity company)
    {
        ManageProductTeam(company);

        ManageProductDevelopment(company);

        ManageInvestors(company);

        ManageCompanyMarketing(company);
    }

    void ManageProductTeam(GameEntity company)
    {
        DisableCrunches(company);

        ManageSmallTeam(company);

        ManageExpandedMarketingTeam(company);
    }

    void ManageProductDevelopment(GameEntity company)
    {
        //ImproveSegments(company);
        StayInMarket(company);
    }

    void ManageExpandedMarketingTeam(GameEntity company)
    {
        var requiredMarketingCost = new TeamResource();

        var testSpeed = MarketingUtils.GetTestCampaignDuration(gameContext, company);
        var brandingSpeed = MarketingUtils.GetBrandingCampaignCooldownDuration(gameContext, company);

        var testCost = MarketingUtils.GetTestCampaignCost(gameContext, company);
        var brandingCost = MarketingUtils.GetBrandingCost(gameContext, company);

        requiredMarketingCost += testCost + brandingCost;
    }

    void ManageInvestors(GameEntity company)
    {
        // taking investments
        TakeInvestments(company);

        // loyalties
        // 
    }

    void ManageCompanyMarketing(GameEntity company)
    {
        StartTargetingCampaign(company);

        GrabTestClients(company);

        StartBrandingCampaign(company);
    }
}
