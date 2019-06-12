using Assets.Utils;
using Assets.Utils.Tutorial;

public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        ProductUtils.UpgradeConcept(MyProductEntity, GameContext);

        TutorialUtils.Unlock(GameContext, TutorialFunctionality.Prototype);
    }
}