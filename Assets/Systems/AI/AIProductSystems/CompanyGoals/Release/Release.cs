using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Release(GameEntity company)
    {
        Crunch(company);

        ImproveSegments(company);

        MarketingUtils.ReleaseApp(company);

        // we need more marketers to release app
        ManageMarketingTeam(company);
    }

    void ManageMarketingTeam(GameEntity company)
    {
        if (TeamUtils.GetMarketers(company) < 2)
        {
            HireWorker(company, WorkerRole.Marketer);
        }
    }
}
