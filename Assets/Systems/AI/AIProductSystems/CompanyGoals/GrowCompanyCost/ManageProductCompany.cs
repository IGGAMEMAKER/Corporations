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
    }

    TeamResource GetRequiredMarketingResources (GameEntity company)
    {
        var requiredMarketingCost = new TeamResource();

        var testSpeed = MarketingUtils.GetTestCampaignDuration(gameContext, company);
        var brandingSpeed = MarketingUtils.GetBrandingCampaignCooldownDuration(gameContext, company);
        var targetSpeed = 1;

        var testCost = MarketingUtils.GetTestCampaignCost(gameContext, company);
        var brandingCost = MarketingUtils.GetBrandingCost(gameContext, company);
        var targetCost = MarketingUtils.GetTargetingCost(gameContext, company.company.Id);

        requiredMarketingCost = (brandingCost * 100 / brandingSpeed) + (targetCost * 100 / targetSpeed) + (testCost * 100 / testSpeed);
        requiredMarketingCost /= 100;

        Print("ManageExpandedMarketingTeam: " + requiredMarketingCost.ToString(), company);

        return requiredMarketingCost;
    }

    void ManageExpandedMarketingTeam(GameEntity company)
    {
        var requiredMarketingCost = GetRequiredMarketingResources(company);

        var change = GetResourceChange(company);

        var period = GetResourcePeriod();

        if (requiredMarketingCost.salesPoints > change.salesPoints)
        {
            if (IsCanAffordWorker(company, WorkerRole.Marketer))
                HireWorker(company, WorkerRole.Marketer);
        }
    }

    int GetResourcePeriod()
    {
        return CompanyEconomyUtils.GetPeriodDuration();
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
        SetMarketingFinancingLevel(company);

        StartTargetingCampaign(company);

        GrabTestClients(company);

        if (company.branding.BrandPower < 90)
            StartBrandingCampaign(company);
    }

    // TODO WHAT THE FUUUCK
    // IT DOES NO CALCULATIONS
    // JUST GREEDY APPROACH
    public void SetMarketingFinancingLevel(GameEntity company)
    {
        var level = MarketingUtils.GetAppropriateFinancingLevel(company, gameContext);

        MarketingUtils.SetFinancing(gameContext, company.company.Id, level);
    }
}
