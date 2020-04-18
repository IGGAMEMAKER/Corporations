using Assets.Core;

public class TargetingToggleButton2 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign (II)";

    public override long GetCost()
    {
        return Products.GetUpgradeCost(Flagship, Q, upgrade);
    }

    public override string GetBenefits()
    {
        var clients = Marketing.GetTargetingCampaignGrowth(Flagship, Q);

        return Visuals.Positive($"+{clients}") + " users weekly";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.TargetingCampaign2;
}
