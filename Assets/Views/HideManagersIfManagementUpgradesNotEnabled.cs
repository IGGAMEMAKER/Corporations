using Assets.Core;

public class HideManagersIfManagementUpgradesNotEnabled : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Products.IsUpgradeEnabled(Flagship, ProductUpgrade.CreateManagementTeam);
    }
}
