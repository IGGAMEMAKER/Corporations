using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    long GetProgrammerNecessity(GameEntity company)
    {
        var conceptCost = ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);

        var change = CompanyEconomyUtils.GetResourceChange(company, gameContext);



        return 500;
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

        var conceptCost = ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);

        var change = CompanyEconomyUtils.GetResourceChange(company, gameContext);

        var ideaGenerationSpeed = change.ideaPoints;
        var ppGenerationSpeed = change.programmingPoints;
        var spGenerationSpeed = change.salesPoints;
    }
}
