﻿using UnityEngine;

public class RenderForGoalController : View
{
    [Tooltip("This components will show up when this goal will be active and hidden before that")]
    public InvestorGoalType TargetGoal;

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
            return false;
            //if (Disable)
            //    return false;

            //var goal = ObservableCompany.companyGoal.InvestorGoal;

            //if (goal == TargetGoal)
            //    return true;

            //if (IsGoalWasCompletedEarlier(goal) && KeepAliveIfGoalCompleted)
            //    return true;

            //return false;
        }
    }

    bool IsGoalWasCompletedEarlier(InvestorGoalType currentGoal)
    {
        var result = CompareGoals(TargetGoal, currentGoal);

        return result < 0;
    }

    int CompareGoals(InvestorGoalType goal1, InvestorGoalType goal2)
    {
        return (int)goal1 - (int)goal2;
    }
}
