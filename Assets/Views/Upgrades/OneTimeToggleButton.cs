using Assets.Core;

public class OneTimeToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() {
        switch (upgrade)
        {
            case ProductUpgrade.CreateManagementTeam:
                return "Create Core Team";

            case ProductUpgrade.CreateSupportTeam:
                return "Create Support Team";

            case ProductUpgrade.CreateQATeam:
                return "Create QA Team";

            case ProductUpgrade.PlatformDesktop:
                return "Desktop version";

            case ProductUpgrade.PlatformMobileAndroid:
                return "Android version";

            case ProductUpgrade.PlatformMobileIOS:
                return "IOS version";

            case ProductUpgrade.PlatformWeb:
                return "WEB version";

            default:
                return "One Time Button? " + upgrade;
        }
    }

    public override string GetBenefits()
    {
        if (upgrade == ProductUpgrade.CreateManagementTeam)
            return Visuals.Positive($"Can hire managers");

        if (upgrade == ProductUpgrade.PlatformDesktop)
            return Visuals.Positive($"Broader audience!");

        if (upgrade == ProductUpgrade.PlatformMobileAndroid)
            return Visuals.Positive($"Broader audience!");

        if (upgrade == ProductUpgrade.PlatformMobileIOS)
            return Visuals.Positive($"Broader audience!");

        if (upgrade == ProductUpgrade.PlatformWeb)
            return Visuals.Positive($"Broader audience!");

        return Visuals.Positive($"New opportunities");
    }

    public override ProductUpgrade upgrade => OneTimeUpgrade;
    public ProductUpgrade OneTimeUpgrade;
}
