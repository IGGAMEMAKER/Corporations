using Assets.Classes;
using Assets.Utils;
using UnityEngine;

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
        var requiredMarketingCostPerMonth = new TeamResource();

        var testSpeed = MarketingUtils.GetTestCampaignDuration(gameContext, company);
        var brandingSpeed = MarketingUtils.GetBrandingCampaignCooldownDuration(gameContext, company);
        var targetSpeed = 30;

        var testCost = MarketingUtils.GetTestCampaignCost(gameContext, company);
        var brandingCost = MarketingUtils.GetBrandingCost(gameContext, company);
        var targetCost = MarketingUtils.GetTargetingCost(gameContext, company.company.Id);

        requiredMarketingCostPerMonth = brandingCost * 30 / brandingSpeed + targetCost * 30 / targetSpeed; // + (testCost * 100 / testSpeed);

        //requiredMarketingCostPerMonth = (brandingCost * 100 / brandingSpeed) + (targetCost * 100 / targetSpeed); // + (testCost * 100 / testSpeed);
        //requiredMarketingCostPerMonth /= 100;

        Print("ManageExpandedMarketingTeam: " + requiredMarketingCostPerMonth.ToString(), company);

        return requiredMarketingCostPerMonth;
    }

    void ManageExpandedMarketingTeam(GameEntity company)
    {
        var doWeNeed = DoWeNeedMarketer(company);

        switch (doWeNeed)
        {
            case 1:
                if (IsCanAffordWorker(company, WorkerRole.Marketer))
                    HireWorker(company, WorkerRole.Marketer);
                break;
            case -1:
                FireWorkerByRole(company, WorkerRole.Marketer);
                break;
        }
    }

    int DoWeNeedMarketer(GameEntity company)
    {
        var requiredMarketingCost = GetRequiredMarketingResources(company);

        var change = GetResourceChange(company);

        var period = GetResourcePeriod();



        var need = requiredMarketingCost.salesPoints;
        var production = change.salesPoints;

        Debug.Log($"Company {company.company.Name} >>> Required SP: {need}, Have SP: {production}");

        if (production < need)
            return 1;

        if (production > need + Constants.DEVELOPMENT_PRODUCTION_MARKETER)
            return -1;

        return 0;
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

        //GrabTestClients(company);

        if (company.branding.BrandPower < 90)
            StartBrandingCampaign(company);
    }

    // TODO WHAT THE FUUUCK
    // IT DOES NO CALCULATIONS
    // JUST GREEDY APPROACH
    public void SetMarketingFinancingLevel(GameEntity company)
    {
        var level = MarketingUtils.GetAppropriateFinancingLevel(company, gameContext);

        // this line is pointless, because proper financing level is already set in GetAppropriateFinancingLevel() function
        MarketingUtils.SetFinancing(gameContext, company.company.Id, level);
    }
}
