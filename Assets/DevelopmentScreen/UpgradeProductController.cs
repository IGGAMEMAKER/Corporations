using Assets.Utils;

public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        ProductUtils.UpgradeConcept(MyProductEntity, GameContext);
    }
}