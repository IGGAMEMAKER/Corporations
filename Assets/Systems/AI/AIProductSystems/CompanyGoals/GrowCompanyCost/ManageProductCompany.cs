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
        ImproveSegments(company);
        //StayInMarket(company);
    }

    void ManageExpandedMarketingTeam(GameEntity company)
    {
        var requiredMarketingCost = new TeamResource();

        var testSpeed = MarketingUtils.GetTestCampaignDuration(gameContext, company);
        var brandingSpeed = MarketingUtils.GetBrandingCampaignCooldownDuration(gameContext, company);

        var testCost = MarketingUtils.GetTestCampaignCost(gameContext, company);
        var brandingCost = MarketingUtils.GetBrandingCost(gameContext, company);

        requiredMarketingCost += testCost + brandingCost;

        var change = GetResourceChange(company);

        if (requiredMarketingCost.salesPoints < change.salesPoints)
        {
            if (IsCanAffordWorker(company, WorkerRole.Marketer))
                HireWorker(company, WorkerRole.Marketer);
        }
    }

    void ManageInvestors(GameEntity company)
    {
        // taking investments
        TakeInvestments(company);

        // loyalties
        // 
    }

    // TODO WHAT THE FUUUCK
    // IT DOES NO CALCULATIONS
    // JUST GREEDY APPROACH
    bool TryFinancing(MarketingFinancing marketingFinancing, GameEntity company, long balance, long maintenance)
    {
        MarketingUtils.SetFinancing(gameContext, company.company.Id, marketingFinancing);

        var cost = MarketingUtils.GetTargetingCost(gameContext, company.company.Id).money;

        return balance - maintenance - cost > 0;
    }

    // TODO WHAT THE FUUUCK
    // IT DOES NO CALCULATIONS
    // JUST GREEDY APPROACH
    void SetMarketingFinancingLevel(GameEntity company)
    {
        var balance = company.companyResource.Resources.money;

        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(company, gameContext);

        if (TryFinancing(MarketingFinancing.High, company, balance, maintenance))
            return;

        if (TryFinancing(MarketingFinancing.Medium, company, balance, maintenance))
            return;

        if (TryFinancing(MarketingFinancing.Low, company, balance, maintenance))
            return;
    }

    void ManageCompanyMarketing(GameEntity company)
    {
        SetMarketingFinancingLevel(company);

        StartTargetingCampaign(company);

        GrabTestClients(company);

        StartBrandingCampaign(company);
    }
}
