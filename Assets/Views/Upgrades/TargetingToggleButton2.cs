using Assets.Core;

public class TargetingToggleButton2 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign (II)";
    public override string GetBenefits()
    {
        var clients = Marketing.GetTargetingCampaignGrowth2(Flagship, Q);

        return Visuals.Positive($"+{clients}") + " users";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.TargetingCampaign2;
}
