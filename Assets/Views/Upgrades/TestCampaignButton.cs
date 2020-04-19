using Assets.Core;

public class TestCampaignButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => "Test Campaign";
    public override string GetBenefits()
    {
        return Visuals.Positive($"+{C.TEST_CAMPAIGN_CLIENT_GAIN}") + " clients weekly";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.TestCampaign;
}