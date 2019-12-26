using Assets.Core;

public class BlinkIfGoalIsCompleted : BlinkOnSomeCondition
{
    public override bool ConditionCheck()
    {
        return Investments.IsGoalCompleted(MyCompany, GameContext);
    }
}
