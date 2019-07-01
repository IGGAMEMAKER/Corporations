using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    bool IsCanAffordWorker(GameEntity company, WorkerRole workerRole)
    {
        return CompanyEconomyUtils.GetBalanceChange(company, gameContext) > TeamUtils.GetSalary(workerRole);
    }

    bool IsNeedsProductManager(GameEntity company)
    {
        bool hasProductManagerAlready = TeamUtils.CountSpecialists(company, WorkerRole.ProductManager) > 0;

        if (hasProductManagerAlready)
            return false;

        return IsCanAffordWorker(company, WorkerRole.ProductManager);
    }
}
