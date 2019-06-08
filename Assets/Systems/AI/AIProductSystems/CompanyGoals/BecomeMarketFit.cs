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

    void ExpandStartupTeam(GameEntity company)
    {
        if (TeamUtils.IsWillOverextendTeam(company))
        {
            TeamUtils.Promote(company);
            return;
        }

        var needsProgrammer = IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed(company);
        var needsProductManager = IsNeedsProductManager(company);
        

        if (needsProgrammer)
            HireWorker(company, WorkerRole.Programmer);

        if (needsProductManager)
            HireWorker(company, WorkerRole.ProductManager);
    }

    bool IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed(GameEntity company)
    {
        var change = CompanyEconomyUtils.GetResourceChange(company, gameContext);

        if (change.programmingPoints == 0)
            return true;

        var conceptCost = ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);

        var resources = company.companyResource.Resources;

        var programmingCompletionTime = (conceptCost.programmingPoints - resources.programmingPoints) / change.programmingPoints;

        if (programmingCompletionTime <= 0)
            return false;

        var ideaCompletionTime = (conceptCost.ideaPoints - resources.ideaPoints) / change.ideaPoints;

        if (ideaCompletionTime < 0)
            ideaCompletionTime = 0;

        Print($"IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed pp: {programmingCompletionTime}periods ip: {ideaCompletionTime}periods", company);

        return programmingCompletionTime > ideaCompletionTime;
    }

    void ImproveConcept(GameEntity company)
    {

    }
}
