using UnityEngine;

public class RenderForGoalController : View
{
    [Tooltip("This components will show up when this goal will be active and hidden before that")]
    public InvestorGoal TargetGoal;

    public bool KeepAliveIfGoalCompleted;
    public bool Disable = false;

    public GameObject[] HideableObjects;

    public override void ViewRender()
    {
        base.ViewRender();
        if (!HasCompany)
            return;

        var decision = show;

        foreach (var obj in HideableObjects)
            obj.SetActive(decision);
    }

    GameEntity ObservableCompany
    {
        get
        {
            // TODO
            //return MyProductEntity;
            return MyCompany;
        }
    }

    bool show
    {
        get
        {
            if (Disable)
                return false;

            var goal = ObservableCompany.companyGoal.InvestorGoal;

            if (goal == TargetGoal)
                return true;

            if (IsGoalWasCompletedEarlier(goal) && KeepAliveIfGoalCompleted)
                return true;

            return false;
        }
    }

    bool IsGoalWasCompletedEarlier(InvestorGoal currentGoal)
    {
        var result = CompareGoals(TargetGoal, currentGoal);

        return result < 0;
    }

    int CompareGoals(InvestorGoal goal1, InvestorGoal goal2)
    {
        return (int)goal1 - (int)goal2;
    }
}
