using UnityEngine;

public class GoalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var goal = (InvestmentGoal)(object)entity;

        t.GetComponent<GoalView2>().SetEntity(goal, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = MyCompany.companyGoal.Goals;

        SetItems(goals);
    }
}
