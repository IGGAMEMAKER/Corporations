using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyGoalListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyGoalButtonView>().SetEntity((InvestorGoalType)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = new List<InvestorGoalType>();

        foreach (var e in (InvestorGoalType[])System.Enum.GetValues(typeof(InvestorGoalType)))
        {
            if (Investments.IsPickableGoal(MyCompany, Q, e))
            {
                Debug.Log("Pickable goal: " + e);
                goals.Add(e);
            }
        }

        SetItems(goals);
    }
}
