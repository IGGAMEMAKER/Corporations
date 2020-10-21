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

        var goals = new List<InvestorGoalType> { InvestorGoalType.Prototype, InvestorGoalType.FirstUsers, InvestorGoalType.BecomeMarketFit };

        SetItems(goals);
    }
}

public class CompanyGoalButtonView : CompanyUpgradeButton
{
    InvestorGoalType InvestorGoal;

    public void SetEntity(InvestorGoalType goal)
    {
        InvestorGoal = goal;

        ViewRender();
    }

    public override string GetButtonTitle()
    {
        return InvestorGoal.ToString();
    }

    public override string GetBenefits()
    {
        return "???";
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
