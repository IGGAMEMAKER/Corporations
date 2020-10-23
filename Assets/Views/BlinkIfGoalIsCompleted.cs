using Assets.Core;

public class BlinkIfGoalIsCompleted : BlinkOnSomeCondition
{
    public override bool ConditionCheck()
    {
        return false;
        //return Investments.IsGoalCompleted(MyCompany, Q);
    }
}
