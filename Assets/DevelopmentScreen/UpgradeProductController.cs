using Assets.Utils;
using Assets.Utils.Tutorial;

// TODO REMOVE
public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        TutorialUtils.Unlock(GameContext, TutorialFunctionality.GoalPrototype);
    }
}