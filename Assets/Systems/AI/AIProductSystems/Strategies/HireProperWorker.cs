using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    bool IsCanAffordWorker(GameEntity company, WorkerRole workerRole)
    {
        return CompanyEconomyUtils.GetBalanceChange(company, gameContext) > GetSalary(workerRole);
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

        return programmingCompletionTime > ideaCompletionTime;
    }

    bool IsNeedsProductManager(GameEntity company)
    {
        bool hasProductManagerAlready = TeamUtils.CountSpecialists(company, WorkerRole.ProductManager) > 0;

        if (hasProductManagerAlready)
            return false;

        return IsCanAffordWorker(company, WorkerRole.ProductManager);
    }

    long GetSalary(WorkerRole workerRole)
    {
        // TODO GET PROPER SALARIES FOR ALL ROLES

        switch (workerRole)
        {
            case WorkerRole.Business: return Constants.SALARIES_CEO;
            case WorkerRole.Manager: return Constants.SALARIES_MANAGER;
            case WorkerRole.Marketer: return Constants.SALARIES_MARKETER;

            case WorkerRole.ProductManager: return Constants.SALARIES_PRODUCT_PROJECT_MANAGER;
            case WorkerRole.ProjectManager: return Constants.SALARIES_PRODUCT_PROJECT_MANAGER;

            default: return Constants.SALARIES_DIRECTOR;
        }
    }
}
