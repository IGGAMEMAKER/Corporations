using Assets.Classes;
using Assets.Utils;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity company)
    {
        //Debug.Log("Manage Product Company ")

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
        MarketingUtils.SetFinancing(gameContext, company.company.Id, MarketingFinancing.Medium);
        return;

        SetMarketingFinancingLevel(company);

        StartBrandingCampaign(company);

        StartTargetingCampaign(company);

        //GrabTestClients(company);

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
