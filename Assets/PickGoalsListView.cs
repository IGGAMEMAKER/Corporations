using Assets.Core;
using UnityEngine;

public class PickGoalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var goal = (InvestmentGoal)(object)entity;

        t.GetComponent<NewGoalView>().SetEntity(goal, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = Investments.GetNewGoals(MyCompany, Q);

        SetItems(goals);
    }
}
