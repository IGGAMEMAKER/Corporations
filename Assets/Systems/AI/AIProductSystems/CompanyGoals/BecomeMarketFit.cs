using Assets.Classes;
using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ExpandStartupTeam(company);

        FocusOnIdeas(company);

        ImproveConcept(company);

        Crunch(company);
    }

    TeamResource GetResourceLimits(GameEntity company)
    {
        var concept = GetConceptCost(company);

        return new TeamResource
        {
            ideaPoints = concept.ideaPoints * 4,
            managerPoints = 500,
            money = 0,
            programmingPoints = concept.programmingPoints * 4,
            salesPoints = concept.salesPoints * 4
        };
    }

    void OptimizeStartupTeam(GameEntity company)
    {
        var resources = company.companyResource.Resources;
        var concept = GetConceptCost(company);

        bool overflowByProgrammingPoints = resources.programmingPoints > 2 * concept.programmingPoints;

        if (overflowByProgrammingPoints &&
            IsNeedsMoreProgrammersToMatchConceptSpeed(company))
            TeamUtils.FireWorker(company, gameContext, WorkerRole.Programmer);
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

    bool IsNeedsProgrammerInStartup(GameEntity company)
    {
        // match idea generation speed
        // idea points overflow
        // 
    }

    bool IsNeedsMoreProgrammersToMatchConceptSpeed(GameEntity company)
    {
        var change = CompanyEconomyUtils.GetResourceChange(company, gameContext);

        if (change.programmingPoints == 0)
            return true;

        var concept = GetConceptCost(company);

        var resources = company.companyResource.Resources;

        var programmingCompletionTime = concept.programmingPoints / change.programmingPoints;
        var ideaCompletionTime = concept.ideaPoints / change.ideaPoints;


        Print($"IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed pp: {programmingCompletionTime}periods ip: {ideaCompletionTime}periods", company);

        if (ideaCompletionTime < 0)
            ideaCompletionTime = 0;

        if (programmingCompletionTime < 0)
            return false;

        return programmingCompletionTime > ideaCompletionTime;
    }

    void ImproveConcept(GameEntity company)
    {

    }
}
