using Assets.Core;

public class RoleRelatedButtons : View
{
    internal bool HasWorker(WorkerRole workerRole, GameEntity company)
    {
        return !Teams.HasFreePlaceForWorker(company, workerRole);
    }

    internal bool CanHireManager(WorkerRole role, GameEntity company)
    {
        return company.isRelease && Teams.HasFreePlaceForWorker(company, role);
    }

    internal bool CanEnable(GameEntity company, ProductUpgrade upgrade)
    {
        return false; // Products.CanEnable(company, Q, upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship; // GetFollowableCompany();

        if (company == null)
            return;

        Render(company);
    }

    internal virtual void Render(GameEntity company) {}
}
