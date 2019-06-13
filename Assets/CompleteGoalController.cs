using Assets.Utils;

public class CompleteGoalController : ButtonController
{
    public override void Execute()
    {
        InvestmentUtils.CompleteGoal(MyProductEntity, GameContext);
    }
}
