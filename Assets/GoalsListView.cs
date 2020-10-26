using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var goal = (InvestmentGoal)(object)entity;

        var title = goal.GetFormattedName();
        var description = goal.GetFormattedRequirements(MyCompany, Q);

        bool completed = Investments.CanCompleteGoal(MyCompany, Q, goal);

        t.GetComponent<GoalView2>().SetEntity(goal, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = MyCompany.companyGoal.Goals;

        SetItems(goals);
    }
}
