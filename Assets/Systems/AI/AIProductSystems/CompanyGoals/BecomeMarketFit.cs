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
        var needsProgrammer = IsNeedsMoreProgrammersToMatchIdeaGenerationSpeed(company);
        var needsProductManager = IsNeedsProductManager(company);

        if (needsProgrammer)
            HireWorker(company, WorkerRole.Programmer);

        if (needsProductManager)
            HireWorker(company, WorkerRole.ProductManager);
    }

    void ImproveConcept(GameEntity company)
    {

    }
}
