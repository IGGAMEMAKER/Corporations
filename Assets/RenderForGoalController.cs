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

        foreach (var obj in HideableObjects)
            obj.SetActive(show);
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

        return result == 1;
    }

    int CompareGoals(InvestorGoal goal1, InvestorGoal goal2)
    {
        if (goal1 == InvestorGoal.IPO)
            return -1;

        if (goal2 == InvestorGoal.IPO)
            return 1;


        if (goal1 == InvestorGoal.GrowCompanyCost)
            return -1;

        if (goal2 == InvestorGoal.GrowCompanyCost)
            return 1;


        if (goal1 == InvestorGoal.BecomeProfitable)
            return -1;

        if (goal2 == InvestorGoal.BecomeProfitable)
            return 1;


        if (goal1 == InvestorGoal.Release)
            return -1;

        if (goal2 == InvestorGoal.Release)
            return 1;


        if (goal1 == InvestorGoal.BecomeMarketFit)
            return -1;

        if (goal2 == InvestorGoal.BecomeMarketFit)
            return 1;


        if (goal1 == InvestorGoal.FirstUsers)
            return -1;

        if (goal2 == InvestorGoal.FirstUsers)
            return 1;


        if (goal1 == InvestorGoal.Prototype)
            return -1;

        if (goal2 == InvestorGoal.Prototype)
            return 1;

        return 0;
    }
}
