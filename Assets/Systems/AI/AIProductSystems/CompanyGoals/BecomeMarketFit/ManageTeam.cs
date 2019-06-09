using Assets.Classes;
using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageTeam(GameEntity company)
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
        }
    }

    // this will change for other company goals
    TeamResource GetResourceNecessity(GameEntity company)
    {
        var marketDiff = MarketingUtils.GetMarketDiff(gameContext, company) + 1;
        // + 1 means that we want to become tech leaders

        var concept = GetConceptCost(company);

        return concept * marketDiff;

        var resource = company.companyResource.Resources;
        var change = GetResourceChange(company);

        return new TeamResource
        {
            programmingPoints = marketDiff * concept.programmingPoints,
            ideaPoints = marketDiff * concept.ideaPoints
        };
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

        var goalIP = needResources.ideaPoints;
        var goalPP = needResources.programmingPoints;

        // time To Complete Goal Ideawise
        var ideaTime = (goalIP - resource.ideaPoints) / change.ideaPoints;

        if (ideaTime < 0)
            ideaTime = 0;

        var programmingTime = (goalPP - resource.programmingPoints) / change.programmingPoints;

        if (programmingTime < ideaTime)
        {
            return -1;
        }
        else if (programmingTime == ideaTime)
        {
            return 0;
        }
        else
        {
            // programming time > idea time
            return 1;
        }
    }
}
