using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var goal = (InvestmentGoal)(object)entity;

        var title = goal.GetFormattedName(); // Investments.GetFormattedInvestorGoal(goal.InvestorGoalType);
        var description = goal.GetFormattedRequirements(Flagship, Q);

        t.GetComponent<MockText>().SetEntity($"<b>{title}</b>\n{description}");
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = Flagship.companyGoal.Goals;

        SetItems(goals);
    }
}
