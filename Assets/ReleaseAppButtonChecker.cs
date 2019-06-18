using Assets.Classes;
using Assets.Utils;

public class ReleaseAppButtonChecker : CheckButton
{
    public override string GetBaseHint()
    {
        return "Gives you significant amount of users and 20 brand power";
    }

    public override TeamResource GetRequiredResources()
    {
        return MarketingUtils.GetReleaseCost();
    }
}

public abstract class CheckButton : ResourceChecker
{
    public abstract string GetBaseHint();
}
