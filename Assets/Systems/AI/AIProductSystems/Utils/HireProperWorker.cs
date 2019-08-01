using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    bool IsCanAffordWorker(GameEntity company, WorkerRole workerRole)
    {
        var profit = CompanyEconomyUtils.GetBalanceChange(company, gameContext);
        var salary = TeamUtils.GetSalary(workerRole);

        return profit > salary || company.companyResource.Resources.money > salary;
    }

    bool IsNeedsProductManager(GameEntity company)
    {
        bool hasProductManagerAlready = TeamUtils.CountSpecialists(company, WorkerRole.ProductManager) > 0;

        if (hasProductManagerAlready)
            return false;

        return IsCanAffordWorker(company, WorkerRole.ProductManager);
    }
}
