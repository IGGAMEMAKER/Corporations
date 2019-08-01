using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Release(GameEntity company)
    {
        Crunch(company);

        ImproveSegments(company);

        MarketingUtils.ReleaseApp(company);
    }
}
