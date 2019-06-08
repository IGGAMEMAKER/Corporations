using Assets.Classes;
using Assets.Utils;
using UnityEngine;

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

    }


    bool IsNeedsProgrammerInStartup(GameEntity company)
    {
        // match idea generation speed
        bool matchesIdeaGenerationSpeed = IsNeedsMoreProgrammersToMatchConceptSpeed(company);

        // idea points overflow
        bool hasEnoughPointsForNewConceptAlready = IsNeedsToManageIdeaOverflow(company);

        return matchesIdeaGenerationSpeed || hasEnoughPointsForNewConceptAlready;
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
