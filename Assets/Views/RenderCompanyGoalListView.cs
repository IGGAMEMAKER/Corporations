using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyGoalListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        AddIfAbsent<CompanyGoalButtonView>(t.gameObject).SetEntity((InvestorGoalType)(object)entity);
        //t.gameObject.AddComponent<CompanyGoalButtonView>().SetEntity((InvestorGoal)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = new List<InvestorGoalType>(); // { InvestorGoalType.Prototype, InvestorGoalType.FirstUsers, InvestorGoalType.BecomeMarketFit };

        foreach (var e in (InvestorGoalType[])System.Enum.GetValues(typeof(InvestorGoalType)))
        {
            if (Investments.IsPickableGoal(Flagship, Q, e))
            {
                goals.Add(e);
            }
        }

        SetItems(goals);
    }
}

public class CompanyGoalButtonView : CompanyUpgradeButton
{
    InvestorGoalType InvestorGoal;
    InvestmentGoal Goal;

    public void SetEntity(InvestorGoalType goal)
    {
        InvestorGoal = goal;

        Goal = Investments.GetInvestmentGoal(Flagship, Q, goal);

        ViewRender();
    }

    public override string GetButtonTitle()
    {
        return Goal.GetFormattedName();
    }

    public override string GetBenefits()
    {
        return Goal.GetFormattedRequirements(Flagship, Q);
    }

    public override bool GetState()
    {
        return true;
    }

    public override string GetHint()
    {
        return "";
    }

    public override void Execute()
    {
        FindObjectOfType<InestmentProposalScreen>().SetGoal(InvestorGoal);
    }
}
