using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderForGoalController : View
{
    [Tooltip("This components will show up when this goal will be active and hidden before that")]
    public InvestorGoal TargetGoal;

    public bool KeepAliveIfGoalCompleted;

    public GameObject[] HideableObjects;

    public override void ViewRender()
    {
        base.ViewRender();

        var decision = show;
        Debug.Log("RenderForGoal " + SelectedCompany.companyGoal.InvestorGoal + " " + TargetGoal);

        foreach (var obj in HideableObjects)
            obj.SetActive(decision);
    }

    bool show
    {
        get
        {
            var goal = SelectedCompany.companyGoal.InvestorGoal;

            if (goal == TargetGoal)
                return true;

            if (IsGoalWasCompleted(goal) && KeepAliveIfGoalCompleted)
                return true;

            return false;
        }
    }

    bool IsGoalWasCompleted(InvestorGoal currentGoal)
    {
        var result = CompareGoals(TargetGoal, currentGoal);

        return result < 0;
    }

    int CompareGoals(InvestorGoal goal1, InvestorGoal goal2)
    {
        Debug.Log("Goal1 " + (int)goal1);
        Debug.Log("Goal2 " + (int)goal2);

        return (int)goal1 - (int)goal2;
    }
}
