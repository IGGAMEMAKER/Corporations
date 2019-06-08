using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageTeam(GameEntity company)
    {
        ExpandStartupTeam(company);

        OptimizeStartupTeam(company);
    }

    void ExpandStartupTeam(GameEntity company)
    {
        if (TeamUtils.IsWillOverextendTeam(company))
        {
            TeamUtils.Promote(company);
            return;
        }

        if (IsNeedsProgrammerInStartup(company))
            HireWorker(company, WorkerRole.Programmer);
    }

    void OptimizeStartupTeam(GameEntity company)
    {
        //var needPP = 
    }

    bool IsNeedsProgrammerInStartup(GameEntity company)
    {
        var marketDiff = MarketingUtils.GetMarketDiff(gameContext, company) + 1;
        // + 1 means that we want to become tech leaders

        var concept = GetConceptCost(company);

        var resource = company.companyResource.Resources;
        var change = GetResourceChange(company);

        var goalPP = marketDiff * concept.programmingPoints;
        var goalIP = marketDiff * concept.ideaPoints;

        // time To Complete Goal Ideawise
        var time = (goalIP - resource.ideaPoints) / change.ideaPoints;

        if (time < 0)
            time = 0;

        

        // match idea generation speed
        bool matchesIdeaGenerationSpeed = IsNeedsMoreProgrammersToMatchConceptSpeed(company);

        return matchesIdeaGenerationSpeed;
    }

    bool IsNeedsToManageIdeaOverflow(GameEntity company)
    {
        var concept = GetConceptCost(company);

        var resources = company.companyResource.Resources;

        var change = GetResourceChange(company);

        return resources.ideaPoints >= concept.ideaPoints;
    }

    bool IsNeedsToManageProgrammingPointsOverflow(GameEntity company)
    {
        var concept = GetConceptCost(company);

        var resources = company.companyResource.Resources;

        var change = GetResourceChange(company);

        return resources.programmingPoints >= concept.programmingPoints;
    }

    bool IsNeedsMoreProgrammersToMatchConceptSpeed(GameEntity company)
    {
        var change = GetResourceChange(company);

        if (change.programmingPoints == 0)
            return true;

        var concept = GetConceptCost(company);

        var programmingCompletionTime = concept.programmingPoints / change.programmingPoints;
        var ideaCompletionTime = concept.ideaPoints / change.ideaPoints;

        Print($"IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed pp: {programmingCompletionTime}periods ip: {ideaCompletionTime}periods", company);

        return programmingCompletionTime > ideaCompletionTime;
    }
}
