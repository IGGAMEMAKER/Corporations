using Assets.Core;

public class TestCampaignButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => "Test Campaign";

    public override long GetCost() => 0;

    public override string GetBenefits()
    {
        return Visuals.Positive($"+{Balance.TEST_CAMPAIGN_CLIENT_GAIN}") + " clients weekly";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TestCampaign;
    }
}