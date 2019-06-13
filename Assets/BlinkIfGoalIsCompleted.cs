using Assets.Utils;

public class BlinkIfGoalIsCompleted : BlinkOnSomeCondition
{
    public override bool ConditionCheck()
    {
        return InvestmentUtils.IsGoalCompleted(MyProductEntity, GameContext);
    }
}
