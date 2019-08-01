using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Release(GameEntity company)
    {
        Crunch(company);

        UpgradeSegment(company);

        MarketingUtils.ReleaseApp(company);
    }
}
