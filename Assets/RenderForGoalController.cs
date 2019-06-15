﻿using UnityEngine;

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

        foreach (var obj in HideableObjects)
            obj.SetActive(decision);
    }

    GameEntity ObservableCompany
    {
        get
        {
            return MyProductEntity;
        }
    }

    bool show
    {
        get
        {
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
