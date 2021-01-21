using Assets.Core;
using System.Linq;
using UnityEngine;

public class CurrentGoalRequirementsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var req = (GoalRequirements)(object)entity;
        var bar = t.GetComponent<ProgressBar>();

        bar.SetDescription(req.description);
        bar.SetValue(req.have, req.need);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = MyCompany.companyGoal.Goals; //.Where()

        if (goals.Count == 0)
            SetItems(goals);
        else
        {
            var goal = goals.First();
            var executor = Investments.GetExecutor(goal, MyCompany, Q);

            var requirements = Investments.GetGoalRequirements(goal, executor, Q);

            SetItems(requirements.Where(r => !Investments.IsRequirementMet(r, executor, Q)));
        }
    }
}
