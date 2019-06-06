using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    long NeedsToUpgradeTeam(GameEntity company)
    {
        return 0;
    }

    long GetProgrammerNecessity(GameEntity company)
    {
        var conceptCost = ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);

        var change = CompanyEconomyUtils.GetResourceChange(company, gameContext);

        var ideaCompletionTime = conceptCost.ideaPoints / change.ideaPoints;
        var programmingCompletionTime = conceptCost.programmingPoints / change.programmingPoints;

        var necessity = programmingCompletionTime > ideaCompletionTime;

        var programmerNecessity = necessity ? Constants.COMPANY_SCORING_HIRE_PROGRAMMER : 0;

        return GetConceptUpgradeUrgency(company) + programmerNecessity;
    }

    long GetConceptUpgradeUrgency(GameEntity company)
    {
        var diff = MarketingUtils.GetMarketDiff(gameContext, company);

        if (diff == 0)
            return 0;

        if (diff == 1)
            return Constants.COMPANY_SCORING_CONCEPT_URGENCY;

        return Constants.COMPANY_SCORING_CONCEPT_TOO_URGENT;
    }

    long GetMarketerNecessity(GameEntity company)
    {
        return 500;
    }

    long GetProductManagerNecessity(GameEntity company)
    {
        return 200;
    }

    WorkerRole GetProperWorkerRole(GameEntity company)
    {
        Dictionary<WorkerRole, long> workerVariants = new Dictionary<WorkerRole, long>
        {
            [WorkerRole.Business] = 0,
            [WorkerRole.Manager] = 0,
            [WorkerRole.Marketer] = GetMarketerNecessity(company),
            [WorkerRole.MarketingDirector] = 0,
            [WorkerRole.MarketingLead] = 0,
            [WorkerRole.ProductManager] = 0,
            [WorkerRole.Programmer] = GetProgrammerNecessity(company),
            [WorkerRole.ProjectManager] = 0,
            [WorkerRole.TeamLead] = 0,
            [WorkerRole.TechDirector] = 0,
            [WorkerRole.Universal] = 0
        };

        return PickMostImportantValue(workerVariants);
    }
}
