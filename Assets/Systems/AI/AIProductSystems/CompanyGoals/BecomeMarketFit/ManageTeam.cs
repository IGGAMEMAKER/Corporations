using Assets.Classes;
using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageSmallTeam(GameEntity company)
    {
        if (TeamUtils.IsWillOverextendTeam(company))
        {
            TeamUtils.Promote(company);
            return;
        }

        ManageProgrammers(company);
    }

    void ManageProgrammers(GameEntity company)
    {
        var programmerNecessity = DoWeNeedProgrammer(company);

        switch (programmerNecessity)
        {
            case 1: HireWorker(company, WorkerRole.Programmer); break;
            case -1: FireWorkerByRole(company, WorkerRole.Programmer); break;
                // case 0: do nothing
        }
    }

    int GetMarketDifference(GameEntity company)
    {
        return ProductUtils.GetMarketDemand(company, gameContext, UserType.Core) - company.product.Concept;
    }

    // this will change for other company goals
    TeamResource GetResourceNecessity(GameEntity company)
    {
        var stayInMarket = GetSegmentCost(company, UserType.Core);

        //// + 1 means that we want to become tech leaders
        var marketDiff = GetMarketDifference(company) + 1;

        return GetSegmentCost(company, UserType.Core) * marketDiff;
    }


    // 1 - we need more programmers
    // 0 - we have good amount of programmers
    // -1 - we have more than we need
    int DoWeNeedProgrammer(GameEntity company)
    {
        var needResources = GetResourceNecessity(company);

        Print($"We need {needResources}", company);

        var resource = company.companyResource.Resources;
        var change = GetResourceChange(company);

        if (change.programmingPoints == 0)
            return 1;

        var iterationTime = ProductUtils.GetSegmentImprovementDuration(gameContext, company);

        

        var goalPP = needResources.programmingPoints;

        var programmingTime = (goalPP - resource.programmingPoints) / change.programmingPoints;

        //if (programmingTime < ideaTime)
        //    return -1;
        //else if (programmingTime == ideaTime)
        //    return 0;
        //else
        //{
        //    // programming time > idea time
        //    return 1;
        //}
    }
}
