using Assets.Core;

public class SupportToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Product support";

    public override long GetCost()
    {
        return Products.GetUpgradeCost(Flagship, Q, upgrade);
    }

    public override string GetBenefits()
    {
        var clients = Marketing.GetTargetingCampaignGrowth(Flagship, Q);

        return Visuals.Positive($"-1% Brand Decay");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Support;
}
