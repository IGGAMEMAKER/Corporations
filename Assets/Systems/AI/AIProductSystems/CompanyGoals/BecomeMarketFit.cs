using Assets.Classes;
using Assets.Utils;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ExpandStartupTeam(company);

        FocusOnIdeas(company);

        ImproveConcept(company);

        Crunch(company);
    }

    //TeamResource GetResourceLimits(GameEntity company)
    //{
    //    var concept = GetConceptCost(company);

    //    return new TeamResource
    //    {
    //        ideaPoints = concept.ideaPoints * 4,
    //        managerPoints = 500,
    //        money = 0,
    //        programmingPoints = concept.programmingPoints * 4,
    //        salesPoints = concept.salesPoints * 4
    //    };
    //}

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

    bool IsNeedsToManageIdeaOverflow (GameEntity company)
    {
        var concept = GetConceptCost(company);

        var resources = company.companyResource.Resources;

        var change = GetResourceChange(company);

        return resources.ideaPoints  >= concept.ideaPoints;
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

    void ImproveConcept(GameEntity company)
    {

    }

    TeamResource GetResourceChange(GameEntity company)
    {
        return CompanyEconomyUtils.GetResourceChange(company, gameContext);
    }

    void Print(string action, GameEntity company)
    {
        if (company.isControlledByPlayer)
            Debug.Log($"{action} : {company.company.Name}");
    }

    TeamResource GetConceptCost(GameEntity company)
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);
    }
}
