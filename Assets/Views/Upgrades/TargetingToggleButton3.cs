using Assets.Core;

public class TargetingToggleButton3 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign (III)";
    public override string GetBenefits()
    {
        var clients = Marketing.GetTargetingCampaignGrowth(Flagship, Q);

        return Visuals.Positive($"+{clients}") + " users weekly";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.TargetingCampaign3;
}
