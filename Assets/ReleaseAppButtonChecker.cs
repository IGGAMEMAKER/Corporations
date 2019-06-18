using Assets.Classes;
using Assets.Utils;

public class ReleaseAppButtonChecker : ResourceChecker
{
    public override string GetBaseHint()
    {
        return $"Gives you significant amount of users and {MarketingUtils.GetReleaseBrandPowerGain()} brand power";
    }

    public override TeamResource GetRequiredResources()
    {
        return MarketingUtils.GetReleaseCost();
    }
}
