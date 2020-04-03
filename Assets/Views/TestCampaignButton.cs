using Assets.Core;

public class TestCampaignButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => "Test Campaign";

    public override long GetCost() => 0;

    public override string GetHint()
    {
        return Visuals.Positive($"Gives you +{100} clients") + " each week";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TestCampaign;
    }
}